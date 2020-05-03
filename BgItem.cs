using System;

// Token: 0x02000048 RID: 72
public class BgItem
{
	// Token: 0x060002A9 RID: 681 RVA: 0x00003584 File Offset: 0x00001784
	public static void clearHashTable()
	{
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00014D78 File Offset: 0x00012F78
	public static bool isExistKeyNews(string keyNew)
	{
		for (int i = 0; i < BgItem.vKeysNew.size(); i++)
		{
			string text = (string)BgItem.vKeysNew.elementAt(i);
			if (text.Equals(keyNew))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00014DC0 File Offset: 0x00012FC0
	public static bool isExistKeyLast(string keyLast)
	{
		for (int i = 0; i < BgItem.vKeysLast.size(); i++)
		{
			string text = (string)BgItem.vKeysLast.elementAt(i);
			if (text.Equals(keyLast))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00014E08 File Offset: 0x00013008
	public bool isNotBlend()
	{
		if (mGraphics.zoomLevel == 1)
		{
			return true;
		}
		if (TileMap.isInAirMap())
		{
			return true;
		}
		for (int i = 0; i < BgItem.idNotBlend.Length; i++)
		{
			if ((int)this.idImage == BgItem.idNotBlend[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00014E5C File Offset: 0x0001305C
	public bool isMiniBg()
	{
		for (int i = 0; i < BgItem.isMiniBgz.Length; i++)
		{
			if ((int)this.idImage == BgItem.isMiniBgz[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00014E98 File Offset: 0x00013098
	public void changeColor()
	{
		if (!this.isNotBlend() && (int)this.layer != 2 && (int)this.layer != 4 && !BgItem.imgNew.containsKey(this.idImage + "blend" + this.layer))
		{
			Image image = (Image)BgItem.imgNew.get(this.idImage + string.Empty);
			if (image != null && image.getRealImageWidth() > 4)
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"blend",
					this.idImage,
					"layer",
					this.layer
				}));
				if (array == null)
				{
					BgItem.imgNew.put(this.idImage + "blend" + this.layer, BgItemMn.blendImage(image, (int)this.layer, (int)this.idImage));
				}
				else
				{
					Image v = Image.createImage(array, 0, array.Length);
					BgItem.imgNew.put(this.idImage + "blend" + this.layer, v);
				}
			}
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x00015000 File Offset: 0x00013200
	public void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.idImage == 279 && GameScr.gI().tMabuEff >= 110)
		{
			return;
		}
		int cmx = GameScr.cmx;
		int cmy = GameScr.cmy;
		Image image;
		if ((int)this.layer == 2 || (int)this.layer == 4)
		{
			image = (Image)BgItem.imgNew.get(this.idImage + string.Empty);
		}
		else if (!this.isNotBlend())
		{
			image = (Image)BgItem.imgNew.get(this.idImage + "blend" + this.layer);
		}
		else
		{
			image = (Image)BgItem.imgNew.get(this.idImage + string.Empty);
		}
		if (image != null)
		{
			if (this.idImage == 96)
			{
				return;
			}
			if ((int)this.layer == 4)
			{
				this.transX = -cmx / 2 + 100;
			}
			if (this.idImage == 28 && (int)this.layer == 3)
			{
				this.transX = -cmx / 3 + 200;
			}
			if ((this.idImage == 67 || this.idImage == 68 || this.idImage == 69 || this.idImage == 70) && (int)this.layer == 3)
			{
				this.transX = -cmx / 3 + 200;
			}
			if (this.isMiniBg() && (int)this.layer < 4)
			{
				this.transX = -(cmx >> 4) + 50;
				this.transY = (cmy >> 5) - 15;
			}
			int num = this.x + this.dx + this.transX;
			int num2 = this.y + this.dy + this.transY;
			if (this.x + this.dx + image.getWidth() + this.transX >= cmx && this.x + this.dx + this.transX <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h)
			{
				g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), this.trans, this.x + this.dx + this.transX, this.y + this.dy + this.transY, 0);
				if (this.idImage == 11 && TileMap.mapID != 122)
				{
					g.setClip(num, num2 + 24, 48, 14);
					for (int i = 0; i < 2; i++)
					{
						g.drawRegion(TileMap.imgWaterflow, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, num + i * 24, num2 + 24, 0);
					}
					g.setClip(GameScr.cmx, GameScr.cmy, GameScr.gW, GameScr.gH);
				}
			}
			if (TileMap.isDoubleMap() && this.idImage > 137 && this.idImage != 156 && this.idImage != 159 && this.idImage != 157 && this.idImage != 165 && this.idImage != 167 && this.idImage != 168 && this.idImage != 169 && this.idImage != 170 && this.idImage != 238 && TileMap.pxw - (this.x + this.dx + this.transX) >= cmx && TileMap.pxw - (this.x + this.dx + this.transX + image.getWidth()) <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h && (this.idImage < 241 || this.idImage >= 266))
			{
				g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 2, TileMap.pxw - (this.x + this.dx + this.transX), this.y + this.dy + this.transY, StaticObj.TOP_RIGHT);
			}
		}
	}

	// Token: 0x04000344 RID: 836
	public int id;

	// Token: 0x04000345 RID: 837
	public int trans;

	// Token: 0x04000346 RID: 838
	public short idImage;

	// Token: 0x04000347 RID: 839
	public int x;

	// Token: 0x04000348 RID: 840
	public int y;

	// Token: 0x04000349 RID: 841
	public int dx;

	// Token: 0x0400034A RID: 842
	public int dy;

	// Token: 0x0400034B RID: 843
	public sbyte layer;

	// Token: 0x0400034C RID: 844
	public int nTilenotMove;

	// Token: 0x0400034D RID: 845
	public int[] tileX;

	// Token: 0x0400034E RID: 846
	public int[] tileY;

	// Token: 0x0400034F RID: 847
	public static MyHashTable imgNew = new MyHashTable();

	// Token: 0x04000350 RID: 848
	public static MyVector vKeysNew = new MyVector();

	// Token: 0x04000351 RID: 849
	public static MyVector vKeysLast = new MyVector();

	// Token: 0x04000352 RID: 850
	private bool isBlur;

	// Token: 0x04000353 RID: 851
	public int transX;

	// Token: 0x04000354 RID: 852
	public int transY;

	// Token: 0x04000355 RID: 853
	public static int[] idNotBlend = new int[]
	{
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		95,
		144,
		99,
		100,
		101,
		102,
		103,
		104,
		105,
		106,
		107,
		108,
		109,
		110,
		111,
		112,
		113,
		114,
		115,
		117,
		118,
		119,
		120,
		121,
		122,
		123,
		124,
		125,
		126,
		127,
		132,
		133,
		134,
		139,
		140,
		141,
		142,
		143,
		144,
		145,
		146,
		147,
		171,
		121,
		122,
		229,
		218
	};

	// Token: 0x04000356 RID: 854
	public static int[] isMiniBgz = new int[]
	{
		79,
		80,
		81,
		85,
		86,
		90,
		91,
		92,
		99,
		100,
		101,
		102,
		103,
		104,
		105,
		106,
		107,
		108
	};

	// Token: 0x04000357 RID: 855
	public static sbyte[] newSmallVersion;
}
