using System;
using Assets.src.e;

// Token: 0x0200007C RID: 124
public class SmallImage
{
	// Token: 0x060003D7 RID: 983 RVA: 0x000055BC File Offset: 0x000037BC
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001E03C File Offset: 0x0001C23C
	public static void loadBigRMS()
	{
		if (SmallImage.imgbig == null)
		{
			SmallImage.imgbig = new Image[]
			{
				GameCanvas.loadImageRMS("/img/Big0.png"),
				GameCanvas.loadImageRMS("/img/Big1.png"),
				GameCanvas.loadImageRMS("/img/Big2.png"),
				GameCanvas.loadImageRMS("/img/Big3.png"),
				GameCanvas.loadImageRMS("/img/Big4.png")
			};
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000055DC File Offset: 0x000037DC
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000055E9 File Offset: 0x000037E9
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000055FE File Offset: 0x000037FE
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00003584 File Offset: 0x00001784
	public void readData(byte[] data)
	{
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
	public void readImage()
	{
		int num = 0;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NR_image"));
			short num2 = dataInputStream.readShort();
			SmallImage.smallImg = new int[(int)num2][];
			for (int i = 0; i < SmallImage.smallImg.Length; i++)
			{
				SmallImage.smallImg[i] = new int[5];
			}
			for (int j = 0; j < (int)num2; j++)
			{
				num++;
				SmallImage.smallImg[j][0] = dataInputStream.readUnsignedByte();
				SmallImage.smallImg[j][1] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][2] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][3] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][4] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError3(string.Concat(new object[]
			{
				"Loi readImage: ",
				ex.ToString(),
				"i= ",
				num
			}));
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00003584 File Offset: 0x00001784
	public static void clearHastable()
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
	public static void createImage(int id)
	{
		Res.outz(string.Concat(new object[]
		{
			"is request =",
			id,
			" zoom=",
			mGraphics.zoomLevel
		}));
		if (mGraphics.zoomLevel == 1)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			if (image != null)
			{
				SmallImage.imgNew[id] = new Small(image, id);
			}
			else
			{
				SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
				Service.gI().requestIcon(id);
			}
		}
		else
		{
			Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			if (image2 != null)
			{
				SmallImage.imgNew[id] = new Small(image2, id);
			}
			else
			{
				bool flag = false;
				sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "Small" + id);
				if (array != null)
				{
					if (SmallImage.newSmallVersion != null && array.Length % 127 != (int)SmallImage.newSmallVersion[id])
					{
						flag = true;
					}
					if (!flag)
					{
						Image image3 = Image.createImage(array, 0, array.Length);
						if (image3 != null)
						{
							SmallImage.imgNew[id] = new Small(image3, id);
						}
						else
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
					Service.gI().requestIcon(id);
				}
			}
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001E328 File Offset: 0x0001C528
	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		if (SmallImage.imgbig == null)
		{
			Small small = SmallImage.imgNew[id];
			if (small == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small, 0, 0, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img), transform, x, y, anchor);
			}
			return;
		}
		if (SmallImage.smallImg != null)
		{
			if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
			{
				Small small2 = SmallImage.imgNew[id];
				if (small2 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small2.paint(g, transform, x, y, anchor);
				}
			}
			else if (SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
			{
				g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small3 = SmallImage.imgNew[id];
			if (small3 == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				small3.paint(g, transform, x, y, anchor);
			}
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001E49C File Offset: 0x0001C69C
	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		if (SmallImage.imgbig == null)
		{
			Small small = SmallImage.imgNew[id];
			if (small == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
			}
			return;
		}
		if (SmallImage.smallImg != null)
		{
			if (id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256)
			{
				Small small2 = SmallImage.imgNew[id];
				if (small2 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small2.paint(g, transform, f, x, y, w, h, anchor);
				}
			}
			else if (SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null)
			{
				g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
			}
			else
			{
				Small small3 = SmallImage.imgNew[id];
				if (small3 == null)
				{
					SmallImage.createImage(id);
				}
				else
				{
					small3.paint(g, transform, f, x, y, w, h, anchor);
				}
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small4 = SmallImage.imgNew[id];
			if (small4 == null)
			{
				SmallImage.createImage(id);
			}
			else
			{
				small4.paint(g, transform, f, x, y, w, h, anchor);
			}
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001E644 File Offset: 0x0001C844
	public static void update()
	{
		int num = 0;
		if (GameCanvas.gameTick % 1000 == 0)
		{
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				if (SmallImage.imgNew[i] != null)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			if (num > 200 && GameCanvas.lowGraphic)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}
	}

	// Token: 0x040006A6 RID: 1702
	public static int[][] smallImg;

	// Token: 0x040006A7 RID: 1703
	public static SmallImage instance;

	// Token: 0x040006A8 RID: 1704
	public static Image[] imgbig;

	// Token: 0x040006A9 RID: 1705
	public static Small[] imgNew;

	// Token: 0x040006AA RID: 1706
	public static MyVector vKeys = new MyVector();

	// Token: 0x040006AB RID: 1707
	public static Image imgEmpty = null;

	// Token: 0x040006AC RID: 1708
	public static sbyte[] newSmallVersion;

	// Token: 0x040006AD RID: 1709
	public static int smallCount;

	// Token: 0x040006AE RID: 1710
	public static short maxSmall;
}
