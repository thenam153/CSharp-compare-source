using System;

// Token: 0x0200006A RID: 106
public class Paint
{
	// Token: 0x06000368 RID: 872 RVA: 0x0001B17C File Offset: 0x0001937C
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1) + ".png");
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0001B1C4 File Offset: 0x000193C4
	public void paintDefaultBg(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgBg, GameCanvas.w / 2, GameCanvas.h / 2 - Paint.hTab / 2 - 1, 3);
		g.drawImage(Paint.imgLT, 0, 0, 0);
		g.drawImage(Paint.imgRT, GameCanvas.w, 0, mGraphics.TOP | mGraphics.RIGHT);
		g.drawImage(Paint.imgLB, 0, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.LEFT);
		g.drawImage(Paint.imgRB, GameCanvas.w, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(16774843);
		g.drawRect(0, 0, GameCanvas.w, 0);
		g.drawRect(0, GameCanvas.h - Paint.hTab - 2, GameCanvas.w, 0);
		g.drawRect(0, 0, 0, GameCanvas.h - Paint.hTab);
		g.drawRect(GameCanvas.w - 1, 0, 0, GameCanvas.h - Paint.hTab);
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0000504B File Offset: 0x0000324B
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00003584 File Offset: 0x00001784
	public void repaintCircleBg()
	{
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00003584 File Offset: 0x00001784
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0000506A File Offset: 0x0000326A
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0000509A File Offset: 0x0000329A
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001B2EC File Offset: 0x000194EC
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001B348 File Offset: 0x00019548
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
		int num = 3;
		if (left != null)
		{
			Paint.lenCaption = mFont.getWidth(left.caption);
			if (Paint.lenCaption > 0)
			{
				if (left.x >= 0 && left.y > 0)
				{
					left.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 0) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 1, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, left.caption, 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (center != null)
		{
			Paint.lenCaption = mFont.getWidth(center.caption);
			if (Paint.lenCaption > 0)
			{
				if (center.x > 0 && center.y > 0)
				{
					center.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.hw - 35, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, center.caption, GameCanvas.hw, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (right != null)
		{
			Paint.lenCaption = mFont.getWidth(right.caption);
			if (Paint.lenCaption > 0)
			{
				if (right.x > 0 && right.y > 0)
				{
					right.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00003584 File Offset: 0x00001784
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000050CE File Offset: 0x000032CE
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000050E7 File Offset: 0x000032E7
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00003584 File Offset: 0x00001784
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001B54C File Offset: 0x0001974C
	public void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(16646144);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16770612);
		}
		else
		{
			g.setColor(16775097);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16775097);
		}
		g.fillRoundRect(x + 3, y + 3, w - 6, h - 6, 10, 10);
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00003584 File Offset: 0x00001784
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00003584 File Offset: 0x00001784
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000050F7 File Offset: 0x000032F7
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		if (index == 1)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00005125 File Offset: 0x00003325
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00005138 File Offset: 0x00003338
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001B5CC File Offset: 0x000197CC
	public void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus)
	{
		if (focus)
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 1 : 3) * 18, 20, 18, 0, x, y, 0);
		}
		else
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 0 : 2) * 18, 20, 18, 0, x, y, 0);
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001B630 File Offset: 0x00019830
	public void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str)
	{
		this.paintFrame(x, y, w, h, g);
		int num = y + 20 - mFont.tahoma_8b.getHeight();
		int i = 0;
		int num2 = num;
		while (i < str.Length)
		{
			mFont.tahoma_8b.drawString(g, str[i], x + w / 2, num2, 2);
			i++;
			num2 += mFont.tahoma_8b.getHeight();
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00003584 File Offset: 0x00001784
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00005141 File Offset: 0x00003341
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0000515A File Offset: 0x0000335A
	public void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(13132288);
			g.fillRect(x + 2, y + 2, w - 3, w - 3);
		}
		g.setColor(3502080);
		g.drawRect(x, y, w, w);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00005199 File Offset: 0x00003399
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000051B1 File Offset: 0x000033B1
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000051B9 File Offset: 0x000033B9
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001B698 File Offset: 0x00019898
	public void paintTextLogin(mGraphics g, bool isRes)
	{
		int num = 0;
		if (!isRes && GameCanvas.h <= 240)
		{
			num = 15;
		}
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[0], GameCanvas.hw, GameCanvas.hh + 60 - num, 2);
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[1], GameCanvas.hw, GameCanvas.hh + 73 - num, 2);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000051F2 File Offset: 0x000033F2
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00003868 File Offset: 0x00001A68
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00005204 File Offset: 0x00003404
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0000520B File Offset: 0x0000340B
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00005234 File Offset: 0x00003434
	public string getUrlUpdateGame()
	{
		return string.Concat(new object[]
		{
			"http://wap.teamobi.com?info=checkupdate&game=3&version=",
			GameMidlet.VERSION,
			"&provider=",
			GameMidlet.PROVIDER
		});
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00003584 File Offset: 0x00001784
	public void doSelect(int focus)
	{
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001B704 File Offset: 0x00019904
	public void paintPopUp(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(9340251);
		g.drawRect(x + 18, y, (w - 36) / 2 - 32, h);
		g.drawRect(x + 18 + (w - 36) / 2 + 32, y, (w - 36) / 2 - 22, h);
		g.drawRect(x, y + 8, w, h - 17);
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x + 18, y + 3, (w - 36) / 2 - 32, h - 4);
		g.fillRect(x + 18 + (w - 36) / 2 + 31, y + 3, (w - 38) / 2 - 22, h - 4);
		g.fillRect(x + 1, y + 6, w - 1, h - 11);
		g.setColor(14667919);
		g.fillRect(x + 18, y + 1, (w - 36) / 2 - 32, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + 1, (w - 36) / 2 - 12, 2);
		g.fillRect(x + 18, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 1, y + 11, 2, h - 18);
		g.fillRect(x + w - 2, y + 11, 2, h - 18);
		g.drawImage(Paint.goc[0], x - 3, y - 2, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(Paint.goc[2], x + w + 3, y - 2, StaticObj.TOP_RIGHT);
		g.drawImage(Paint.goc[1], x - 3, y + h + 3, StaticObj.BOTTOM_LEFT);
		g.drawImage(Paint.goc[3], x + w + 4, y + h + 2, StaticObj.BOTTOM_RIGHT);
		g.drawImage(Paint.goc[4], x + w / 2, y, StaticObj.TOP_CENTER);
		g.drawImage(Paint.goc[5], x + w / 2, y + h + 1, StaticObj.BOTTOM_HCENTER);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0001B924 File Offset: 0x00019B24
	public void paintFrame(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(13524492);
		g.drawRect(x + 6, y, w - 12, h);
		g.drawRect(x, y + 6, w, h - 12);
		g.drawRect(x + 7, y + 1, w - 14, h - 2);
		g.drawRect(x + 1, y + 7, w - 2, h - 14);
		g.setColor(14338484);
		g.fillRect(x + 8, y + 2, w - 16, h - 3);
		g.fillRect(x + 2, y + 8, w - 3, h - 14);
		g.drawImage(GameCanvas.imgBorder[2], x, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 2, x + w + 1, y, StaticObj.TOP_RIGHT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 1, x, y + h + 1, StaticObj.BOTTOM_LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 3, x + w + 1, y + h + 1, StaticObj.BOTTOM_RIGHT);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00005266 File Offset: 0x00003466
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000052A0 File Offset: 0x000034A0
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000052AF File Offset: 0x000034AF
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000052C9 File Offset: 0x000034C9
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x040005E6 RID: 1510
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x040005E7 RID: 1511
	public static int COLORLIGHT = 16383818;

	// Token: 0x040005E8 RID: 1512
	public static int COLORDARK = 3937280;

	// Token: 0x040005E9 RID: 1513
	public static int COLORBORDER = 15224576;

	// Token: 0x040005EA RID: 1514
	public static int COLORFOCUS = 16777215;

	// Token: 0x040005EB RID: 1515
	public static Image imgBg;

	// Token: 0x040005EC RID: 1516
	public static Image imgLogo;

	// Token: 0x040005ED RID: 1517
	public static Image imgLB;

	// Token: 0x040005EE RID: 1518
	public static Image imgLT;

	// Token: 0x040005EF RID: 1519
	public static Image imgRB;

	// Token: 0x040005F0 RID: 1520
	public static Image imgRT;

	// Token: 0x040005F1 RID: 1521
	public static Image imgChuong;

	// Token: 0x040005F2 RID: 1522
	public static Image imgSelectBoard;

	// Token: 0x040005F3 RID: 1523
	public static Image imgtoiSmall;

	// Token: 0x040005F4 RID: 1524
	public static Image imgTayTren;

	// Token: 0x040005F5 RID: 1525
	public static Image imgTayDuoi;

	// Token: 0x040005F6 RID: 1526
	public static Image[] imgTick = new Image[2];

	// Token: 0x040005F7 RID: 1527
	public static Image[] imgMsg = new Image[2];

	// Token: 0x040005F8 RID: 1528
	public static Image[] goc = new Image[6];

	// Token: 0x040005F9 RID: 1529
	public static int hTab = 24;

	// Token: 0x040005FA RID: 1530
	public static int lenCaption = 0;

	// Token: 0x040005FB RID: 1531
	public int[] color = new int[]
	{
		15970400,
		13479911,
		2250052,
		16374659,
		15906669,
		12931125,
		3108954
	};

	// Token: 0x040005FC RID: 1532
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}
