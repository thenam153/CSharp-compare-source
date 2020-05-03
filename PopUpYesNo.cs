using System;

// Token: 0x02000070 RID: 112
public class PopUpYesNo : IActionListener
{
	// Token: 0x060003AC RID: 940 RVA: 0x0001CC88 File Offset: 0x0001AE88
	public void setPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.info = new string[]
		{
			info
		};
		this.H = 29;
		this.cmdYes = cmdYes;
		this.cmdNo = cmdNo;
		this.cmdYes.img = (this.cmdNo.img = GameScr.imgNut);
		this.cmdYes.imgFocus = (this.cmdNo.imgFocus = GameScr.imgNutF);
		this.cmdYes.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdNo.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdYes.h = mGraphics.getImageHeight(cmdYes.img);
		this.cmdNo.h = mGraphics.getImageHeight(cmdYes.img);
		this.last = mSystem.currentTimeMillis();
		this.dem = this.info[0].Length / 3;
		if (this.dem < 15)
		{
			this.dem = 15;
		}
		TextInfo.reset();
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001CD8C File Offset: 0x0001AF8C
	public void paint(mGraphics g)
	{
		PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + (GameCanvas.isTouch ? 0 : 10), 16777215, false);
		if (this.info != null)
		{
			TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
			if (GameCanvas.isTouch)
			{
				this.cmdYes.paint(g);
				mFont.tahoma_7_yellow.drawString(g, this.dem + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
			}
			else if (TField.isQwerty)
			{
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
			}
			else
			{
				mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
			}
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001CF30 File Offset: 0x0001B130
	public void update()
	{
		if (this.info != null)
		{
			this.X = GameCanvas.w - 5 - this.W;
			this.Y = 45;
			if (GameCanvas.w - 50 > 155 + this.W)
			{
				this.X = GameCanvas.w - 55 - this.W;
				this.Y = 5;
			}
			this.cmdYes.x = this.X - 35;
			this.cmdYes.y = this.Y;
			this.curr = mSystem.currentTimeMillis();
			Res.outz("curr - last= " + (this.curr - this.last));
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.dem--;
			}
			if (this.dem == 0)
			{
				GameScr.gI().popUpYesNo = null;
			}
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00003584 File Offset: 0x00001784
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000636 RID: 1590
	public Command cmdYes;

	// Token: 0x04000637 RID: 1591
	public Command cmdNo;

	// Token: 0x04000638 RID: 1592
	public string[] info;

	// Token: 0x04000639 RID: 1593
	private int X;

	// Token: 0x0400063A RID: 1594
	private int Y;

	// Token: 0x0400063B RID: 1595
	private int W = 120;

	// Token: 0x0400063C RID: 1596
	private int H;

	// Token: 0x0400063D RID: 1597
	private int dem;

	// Token: 0x0400063E RID: 1598
	private long last;

	// Token: 0x0400063F RID: 1599
	private long curr;
}
