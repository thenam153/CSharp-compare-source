using System;

// Token: 0x02000038 RID: 56
public class EffectData
{
	// Token: 0x0600025B RID: 603 RVA: 0x00012988 File Offset: 0x00010B88
	public ImageInfo getImageInfo(sbyte id)
	{
		for (int i = 0; i < this.imgInfo.Length; i++)
		{
			if (this.imgInfo[i].ID == (int)id)
			{
				return this.imgInfo[i];
			}
		}
		return null;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x000129CC File Offset: 0x00010BCC
	public void readData(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception ex)
		{
			return;
		}
		this.readData(dataInputStream.r);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00012A10 File Offset: 0x00010C10
	public void readData2(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception ex)
		{
			return;
		}
		this.readEffect(dataInputStream.r);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00012A54 File Offset: 0x00010C54
	public void readEffect(myReader msg)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			Res.outz("size IMG==========" + b);
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)msg.readByte();
				this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].w = (int)msg.readUnsignedByte();
				this.imgInfo[i].h = (int)msg.readUnsignedByte();
			}
			short num5 = msg.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < this.frame.Length; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = msg.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = msg.readShort();
					this.frame[j].dy[k] = msg.readShort();
					this.frame[j].idImg[k] = msg.readByte();
					if (j == 0)
					{
						if (num > (int)this.frame[j].dx[k])
						{
							num = (int)this.frame[j].dx[k];
						}
						if (num2 > (int)this.frame[j].dy[k])
						{
							num2 = (int)this.frame[j].dy[k];
						}
						if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			this.arrFrame = new short[(int)msg.readShort()];
			for (int l = 0; l < this.arrFrame.Length; l++)
			{
				this.arrFrame[l] = msg.readShort();
			}
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
			Res.outz("1");
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00012DBC File Offset: 0x00010FBC
	public void readData(myReader iss)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = iss.readByte();
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)iss.readByte();
				this.imgInfo[i].x0 = (int)iss.readByte();
				this.imgInfo[i].y0 = (int)iss.readByte();
				this.imgInfo[i].w = (int)iss.readByte();
				this.imgInfo[i].h = (int)iss.readByte();
			}
			short num5 = iss.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < (int)num5; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = iss.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = iss.readShort();
					this.frame[j].dy[k] = iss.readShort();
					this.frame[j].idImg[k] = iss.readByte();
					if (j == 0)
					{
						if (num > (int)this.frame[j].dx[k])
						{
							num = (int)this.frame[j].dx[k];
						}
						if (num2 > (int)this.frame[j].dy[k])
						{
							num2 = (int)this.frame[j].dy[k];
						}
						if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			short num6 = iss.readShort();
			this.arrFrame = new short[(int)num6];
			for (int l = 0; l < (int)num6; l++)
			{
				this.arrFrame[l] = iss.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readData cua EffectDAta" + ex.ToString());
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0001310C File Offset: 0x0001130C
	public void readData(sbyte[] data)
	{
		myReader iss = new myReader(data);
		this.readData(iss);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00013128 File Offset: 0x00011328
	public void paintFrame(mGraphics g, int f, int x, int y, int trans, int layer)
	{
		if (this.frame != null && this.frame.Length != 0)
		{
			Frame frame = this.frame[f];
			for (int i = 0; i < frame.dx.Length; i++)
			{
				ImageInfo imageInfo = this.getImageInfo(frame.idImg[i]);
				try
				{
					if (trans == 0)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), 0);
					}
					if (trans == 1)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 2, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.TOP_RIGHT);
					}
					if (trans == 2)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 7, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer >= 4 || layer <= 0) ? 0 : GameCanvas.transY), StaticObj.VCENTER_HCENTER);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}
	}

	// Token: 0x040002B0 RID: 688
	public Image img;

	// Token: 0x040002B1 RID: 689
	public ImageInfo[] imgInfo;

	// Token: 0x040002B2 RID: 690
	public Frame[] frame;

	// Token: 0x040002B3 RID: 691
	public short[] arrFrame;

	// Token: 0x040002B4 RID: 692
	public int ID;

	// Token: 0x040002B5 RID: 693
	public int width;

	// Token: 0x040002B6 RID: 694
	public int height;
}
