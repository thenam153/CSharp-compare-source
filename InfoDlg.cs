using System;

// Token: 0x0200009E RID: 158
public class InfoDlg
{
	// Token: 0x060006E6 RID: 1766 RVA: 0x000066D6 File Offset: 0x000048D6
	public static void show(string title, string subtitle, int delay)
	{
		if (title == null)
		{
			return;
		}
		InfoDlg.isShow = true;
		InfoDlg.title = title;
		InfoDlg.subtitke = subtitle;
		InfoDlg.delay = delay;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x000066F7 File Offset: 0x000048F7
	public static void showWait()
	{
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0000670F File Offset: 0x0000490F
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0005C54C File Offset: 0x0005A74C
	public static void paint(mGraphics g)
	{
		if (!InfoDlg.isShow)
		{
			return;
		}
		if (InfoDlg.isLock && InfoDlg.delay > 4990)
		{
			return;
		}
		if (GameScr.isPaintAlert)
		{
			return;
		}
		int num = 10;
		GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
		if (InfoDlg.isLock)
		{
			GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
		}
		else if (InfoDlg.subtitke != null)
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
			mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
		}
		else
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
		}
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00006723 File Offset: 0x00004923
	public static void update()
	{
		if (InfoDlg.delay > 0)
		{
			InfoDlg.delay--;
			if (InfoDlg.delay == 0)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0000674B File Offset: 0x0000494B
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x04000CE6 RID: 3302
	public static bool isShow;

	// Token: 0x04000CE7 RID: 3303
	private static string title;

	// Token: 0x04000CE8 RID: 3304
	private static string subtitke;

	// Token: 0x04000CE9 RID: 3305
	public static int delay;

	// Token: 0x04000CEA RID: 3306
	public static bool isLock;
}
