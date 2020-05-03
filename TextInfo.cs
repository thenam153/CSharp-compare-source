using System;

// Token: 0x02000089 RID: 137
public class TextInfo
{
	// Token: 0x0600042B RID: 1067 RVA: 0x000059EB File Offset: 0x00003BEB
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00023718 File Offset: 0x00021918
	public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
	{
		if (TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str))
		{
			TextInfo.laststring = str;
			TextInfo.dx = 0;
			TextInfo.wStr = f.getWidth(str);
			TextInfo.isBack = false;
			TextInfo.tx = 0;
		}
		g.setClip(x, y, w, h);
		if (TextInfo.wStr > w)
		{
			f.drawString(g, str, x - TextInfo.dx, y, 0);
		}
		else
		{
			f.drawString(g, str, x + w / 2, y, 2);
		}
		GameCanvas.resetTrans(g);
		if (TextInfo.wStr > w)
		{
			if (!TextInfo.isBack)
			{
				TextInfo.tx++;
				if (TextInfo.tx > 50)
				{
					TextInfo.dx++;
					if (TextInfo.dx >= TextInfo.wStr)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
			}
			else
			{
				if (TextInfo.dx < 0)
				{
					int num = w + TextInfo.dx >> 1;
					TextInfo.dx += num;
				}
				if (TextInfo.dx > 0)
				{
					TextInfo.dx = 0;
				}
				if (TextInfo.dx == 0)
				{
					TextInfo.tx++;
					if (TextInfo.tx == 50)
					{
						TextInfo.tx = 0;
						TextInfo.isBack = false;
					}
				}
			}
		}
	}

	// Token: 0x04000706 RID: 1798
	public static int dx;

	// Token: 0x04000707 RID: 1799
	public static int tx;

	// Token: 0x04000708 RID: 1800
	public static int wStr;

	// Token: 0x04000709 RID: 1801
	public static bool isBack;

	// Token: 0x0400070A RID: 1802
	public static string laststring = string.Empty;
}
