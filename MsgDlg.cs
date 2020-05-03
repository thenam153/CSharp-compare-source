using System;

// Token: 0x020000A9 RID: 169
public class MsgDlg : Dialog
{
	// Token: 0x06000769 RID: 1897 RVA: 0x00006A92 File Offset: 0x00004C92
	public MsgDlg()
	{
		this.padLeft = 35;
		if (GameCanvas.w <= 176)
		{
			this.padLeft = 10;
		}
		if (GameCanvas.w > 320)
		{
			this.padLeft = 80;
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00006AD0 File Offset: 0x00004CD0
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00006815 File Offset: 0x00004A15
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x000632E0 File Offset: 0x000614E0
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00063344 File Offset: 0x00061544
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.left = left;
		this.center = center;
		this.right = right;
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
		if (GameCanvas.isTouch)
		{
			if (left != null)
			{
				this.left.x = GameCanvas.w / 2 - 68 - 5;
				this.left.y = GameCanvas.h - 50;
			}
			if (right != null)
			{
				this.right.x = GameCanvas.w / 2 + 5;
				this.right.y = GameCanvas.h - 50;
			}
			if (center != null)
			{
				this.center.x = GameCanvas.w / 2 - 35;
				this.center.y = GameCanvas.h - 50;
			}
		}
		this.isWait = false;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00063458 File Offset: 0x00061658
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		int num = GameCanvas.h - this.h - 38;
		int w = GameCanvas.w - this.padLeft * 2;
		GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
		int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
		if (this.isWait)
		{
			num2 += 8;
			GameCanvas.paintShukiren(GameCanvas.hw, num2 - 12, g);
		}
		int i = 0;
		int num3 = num2;
		while (i < this.info.Length)
		{
			mFont.tahoma_7b_dark.drawString(g, this.info[i], GameCanvas.hw, num3, 2);
			i++;
			num3 += mFont.tahoma_8b.getHeight();
		}
		base.paint(g);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00006AE6 File Offset: 0x00004CE6
	public override void update()
	{
		base.update();
	}

	// Token: 0x04000E0E RID: 3598
	public string[] info;

	// Token: 0x04000E0F RID: 3599
	public bool isWait;

	// Token: 0x04000E10 RID: 3600
	private int h;

	// Token: 0x04000E11 RID: 3601
	private int padLeft;
}
