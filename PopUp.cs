using System;

// Token: 0x0200006F RID: 111
public class PopUp
{
	// Token: 0x0600039D RID: 925 RVA: 0x0001C238 File Offset: 0x0001A438
	public PopUp(string info, int x, int y)
	{
		this.sayWidth = 100;
		if (info.Length < 10)
		{
			this.sayWidth = 60;
		}
		if (GameCanvas.w == 128)
		{
			this.sayWidth = 128;
		}
		this.says = mFont.tahoma_7b_dark.splitFontArray(info, this.sayWidth - 10);
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		if (x >= 0 && x <= 24)
		{
			this.cx += this.cw / 2 + 30;
		}
		if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001C3F0 File Offset: 0x0001A5F0
	public static void loadBg()
	{
		if (PopUp.goc == null)
		{
			PopUp.goc = GameCanvas.loadImage("/mainImage/myTexture2dbd3.png");
		}
		if (PopUp.imgPopUp == null)
		{
			PopUp.imgPopUp = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup.png");
		}
		if (PopUp.imgPopUp2 == null)
		{
			PopUp.imgPopUp2 = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup2.png");
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001C448 File Offset: 0x0001A648
	public void updateXYWH(string[] info, int x, int y)
	{
		this.sayWidth = 0;
		for (int i = 0; i < info.Length; i++)
		{
			if (this.sayWidth < mFont.tahoma_7b_dark.getWidth(info[i]))
			{
				this.sayWidth = mFont.tahoma_7b_dark.getWidth(info[i]);
			}
		}
		this.sayWidth += 20;
		this.says = info;
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		if (x >= 0 && x <= 24)
		{
			this.cx += this.cw / 2 + 30;
		}
		if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00005391 File Offset: 0x00003591
	public static void addPopUp(int x, int y, string info)
	{
		PopUp.vPopups.addElement(new PopUp(info, x, y));
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x000053A5 File Offset: 0x000035A5
	public static void addPopUp(PopUp p)
	{
		PopUp.vPopups.addElement(p);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x000053B2 File Offset: 0x000035B2
	public static void removePopUp(PopUp p)
	{
		PopUp.vPopups.removeElement(p);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000053BF File Offset: 0x000035BF
	public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
	{
		if (color == 1)
		{
			g.fillRect(x, y, w, h, 16777215, 90);
		}
		else
		{
			g.fillRect(x, y, w, h, 0, 77);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001C5FC File Offset: 0x0001A7FC
	public static void paintPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isButton)
	{
		if (!isButton)
		{
			g.setColor(0);
			g.fillRect(x + 6, y, w - 14 + 1, h);
			g.fillRect(x, y + 6, w, h - 12 + 1);
			g.setColor(color);
			g.fillRect(x + 6, y + 1, w - 12, h - 2);
			g.fillRect(x + 1, y + 6, w - 2, h - 12);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 0, x, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 2, x + w - 7, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 1, x, y + h - 6, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 3, x + w - 7, y + h - 6, 0);
		}
		else
		{
			Image arg = (color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2;
			g.drawRegion(arg, 0, 0, 10, 10, 0, x, y, 0);
			g.drawRegion(arg, 0, 20, 10, 10, 0, x + w - 10, y, 0);
			g.drawRegion(arg, 0, 50, 10, 10, 0, x, y + h - 10, 0);
			g.drawRegion(arg, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
			int num = ((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10);
			int num2 = ((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10);
			for (int i = 0; i < num; i++)
			{
				g.drawRegion(arg, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
			}
			for (int j = 0; j < num2; j++)
			{
				g.drawRegion(arg, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
			}
			for (int k = 0; k < num; k++)
			{
				g.drawRegion(arg, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
			}
			for (int l = 0; l < num2; l++)
			{
				g.drawRegion(arg, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
			}
			g.setColor((color != 1) ? 16770503 : 12052656);
			g.fillRect(x + 10, y + 10, w - 20, h - 20);
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001C888 File Offset: 0x0001AA88
	public void paint(mGraphics g)
	{
		if (!this.isPaint)
		{
			return;
		}
		if (this.says == null)
		{
			return;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		if (!this.isHide)
		{
			this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
			for (int i = 0; i < this.says.Length; i++)
			{
				((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
			}
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0001C974 File Offset: 0x0001AB74
	private void update()
	{
		if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0)
		{
			if (this.cx + this.cw >= GameScr.cmx && this.cx <= GameCanvas.w + GameScr.cmx && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		if (global::Char.myCharz().taskMaint == null || (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId != 0))
		{
			if (this.cx + this.cw / 2 >= global::Char.myCharz().cx - 100 && this.cx + this.cw / 2 <= global::Char.myCharz().cx + 100 && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		if (this.timeDelay > 0)
		{
			this.timeDelay--;
			if (this.timeDelay == 0 && this.command != null)
			{
				this.command.performAction();
			}
		}
		if (this.isWayPoint)
		{
			if (global::Char.myCharz().taskMaint != null)
			{
				if (global::Char.myCharz().taskMaint.taskId == 0)
				{
					if (global::Char.myCharz().taskMaint.index == 0)
					{
						this.isPaint = false;
					}
					if (global::Char.myCharz().taskMaint.index == 1)
					{
						this.isPaint = true;
					}
					if (global::Char.myCharz().taskMaint.index > 1 && global::Char.myCharz().taskMaint.index < 6)
					{
						this.isPaint = false;
					}
				}
				else if (!this.isPaint)
				{
					this.tDelay++;
					if (this.tDelay == 50)
					{
						this.isPaint = true;
					}
				}
			}
			else if (!this.isPaint)
			{
				Hint.isPaint = false;
				this.tDelay++;
				if (this.tDelay == 50)
				{
					this.isPaint = true;
					Hint.isPaint = true;
				}
			}
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000053F0 File Offset: 0x000035F0
	public void doClick(int timeDelay)
	{
		this.timeDelay = timeDelay;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001CC08 File Offset: 0x0001AE08
	public static void paintAll(mGraphics g)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001CC48 File Offset: 0x0001AE48
	public static void updateAll()
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).update();
		}
	}

	// Token: 0x04000620 RID: 1568
	public static MyVector vPopups = new MyVector();

	// Token: 0x04000621 RID: 1569
	public int sayWidth;

	// Token: 0x04000622 RID: 1570
	public int sayRun;

	// Token: 0x04000623 RID: 1571
	public string[] says;

	// Token: 0x04000624 RID: 1572
	public int cx;

	// Token: 0x04000625 RID: 1573
	public int cy;

	// Token: 0x04000626 RID: 1574
	public int cw;

	// Token: 0x04000627 RID: 1575
	public int ch;

	// Token: 0x04000628 RID: 1576
	public static int f;

	// Token: 0x04000629 RID: 1577
	public static int tF;

	// Token: 0x0400062A RID: 1578
	public static int dir;

	// Token: 0x0400062B RID: 1579
	public bool isWayPoint;

	// Token: 0x0400062C RID: 1580
	public int tDelay;

	// Token: 0x0400062D RID: 1581
	private int timeDelay;

	// Token: 0x0400062E RID: 1582
	public Command command;

	// Token: 0x0400062F RID: 1583
	public bool isPaint = true;

	// Token: 0x04000630 RID: 1584
	public bool isHide;

	// Token: 0x04000631 RID: 1585
	public static Image goc;

	// Token: 0x04000632 RID: 1586
	public static Image imgPopUp;

	// Token: 0x04000633 RID: 1587
	public static Image imgPopUp2;

	// Token: 0x04000634 RID: 1588
	public Image imgFocus;

	// Token: 0x04000635 RID: 1589
	public Image imgUnFocus;
}
