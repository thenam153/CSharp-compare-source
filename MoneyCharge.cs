using System;

// Token: 0x020000A8 RID: 168
public class MoneyCharge : mScreen, IActionListener
{
	// Token: 0x0600075F RID: 1887 RVA: 0x00062B78 File Offset: 0x00060D78
	public MoneyCharge()
	{
		this.w = GameCanvas.w - 20;
		if (this.w > 320)
		{
			this.w = 320;
		}
		this.strPaint = mFont.tahoma_7b_green2.splitFontArray(mResources.pay_card, this.w - 20);
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = GameCanvas.h - 150 - (this.strPaint.Length - 1) * 20;
		this.h = 110 + (this.strPaint.Length - 1) * 20;
		this.yP = this.y;
		this.tfSerial = new TField();
		this.tfSerial.name = mResources.SERI_NUM;
		this.tfSerial.x = this.x + 10;
		this.tfSerial.y = this.y + 35 + (this.strPaint.Length - 1) * 20;
		this.yt = this.tfSerial.y;
		this.tfSerial.width = this.w - 20;
		this.tfSerial.height = mScreen.ITEM_HEIGHT + 2;
		if (GameCanvas.isTouch)
		{
			this.tfSerial.isFocus = false;
		}
		else
		{
			this.tfSerial.isFocus = true;
		}
		this.tfSerial.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.tfSerial.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfSerial.isPaintMouse = false;
		}
		if (!GameCanvas.isTouch)
		{
			this.right = this.tfSerial.cmdClear;
		}
		this.tfCode = new TField();
		this.tfCode.name = mResources.CARD_CODE;
		this.tfCode.x = this.x + 10;
		this.tfCode.y = this.tfSerial.y + 35;
		this.tfCode.width = this.w - 20;
		this.tfCode.height = mScreen.ITEM_HEIGHT + 2;
		this.tfCode.isFocus = false;
		this.tfCode.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.tfCode.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfCode.isPaintMouse = false;
		}
		this.left = new Command(mResources.CLOSE, this, 1, null);
		this.center = new Command(mResources.pay_card2, this, 2, null);
		if (GameCanvas.isTouch)
		{
			this.center.x = GameCanvas.w / 2 + 18;
			this.left.x = GameCanvas.w / 2 - 85;
			this.center.y = (this.left.y = this.y + this.h + 5);
		}
		this.freeAreaHeight = this.tfSerial.y - (4 * this.tfSerial.height - 10);
		this.yP = this.tfSerial.y;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00006A3D File Offset: 0x00004C3D
	public static MoneyCharge gI()
	{
		if (MoneyCharge.instance == null)
		{
			MoneyCharge.instance = new MoneyCharge();
		}
		return MoneyCharge.instance;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00062EAC File Offset: 0x000610AC
	public override void switchToMe()
	{
		try
		{
			if ((int)mResources.language == 0)
			{
				GameMidlet.instance.platformRequest("http://ngocrongonline.com/");
			}
			if ((int)mResources.language == 2)
			{
				GameMidlet.instance.platformRequest("http://dragonball.indonaga.com/");
			}
			if ((int)mResources.language == 1)
			{
				GameMidlet.instance.platformRequest("http://world.teamobi.com/games-page-0.html");
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00003584 File Offset: 0x00001784
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00062F24 File Offset: 0x00061124
	public override void paint(mGraphics g)
	{
		GameScr.gI().paint(g);
		PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
		for (int i = 0; i < this.strPaint.Length; i++)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
		}
		this.tfSerial.paint(g);
		this.tfCode.paint(g);
		base.paint(g);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00006A58 File Offset: 0x00004C58
	public override void update()
	{
		GameScr.gI().update();
		this.tfSerial.update();
		this.tfCode.update();
		if (Main.isWindowsPhone)
		{
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00062FC4 File Offset: 0x000611C4
	public override void keyPress(int keyCode)
	{
		if (this.tfSerial.isFocus)
		{
			this.tfSerial.keyPressed(keyCode);
		}
		else if (this.tfCode.isFocus)
		{
			this.tfCode.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00063018 File Offset: 0x00061218
	public override void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 1;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			GameCanvas.clearKeyPressed();
			if (this.focus == 1)
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = true;
				if (!GameCanvas.isTouch)
				{
					this.right = this.tfCode.cmdClear;
				}
			}
			else if (this.focus == 0)
			{
				this.tfSerial.isFocus = true;
				this.tfCode.isFocus = false;
				if (!GameCanvas.isTouch)
				{
					this.right = this.tfSerial.cmdClear;
				}
			}
			else
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = false;
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (GameCanvas.isPointerHoldIn(this.tfSerial.x, this.tfSerial.y, this.tfSerial.width, this.tfSerial.height))
			{
				this.focus = 0;
			}
			else if (GameCanvas.isPointerHoldIn(this.tfCode.x, this.tfCode.y, this.tfCode.width, this.tfCode.height))
			{
				this.focus = 1;
			}
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00006A8A File Offset: 0x00004C8A
	public void clearScreen()
	{
		MoneyCharge.instance = null;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00063218 File Offset: 0x00061418
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
		if (idAction == 2)
		{
			if (this.tfSerial.getText() == null || this.tfSerial.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.serial_blank);
				return;
			}
			if (this.tfCode.getText() == null || this.tfCode.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.card_code_blank);
				return;
			}
			Service.gI().sendCardInfo(this.tfSerial.getText(), this.tfCode.getText());
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
	}

	// Token: 0x04000E01 RID: 3585
	public static MoneyCharge instance;

	// Token: 0x04000E02 RID: 3586
	public TField tfSerial;

	// Token: 0x04000E03 RID: 3587
	public TField tfCode;

	// Token: 0x04000E04 RID: 3588
	private int x;

	// Token: 0x04000E05 RID: 3589
	private int y;

	// Token: 0x04000E06 RID: 3590
	private int w;

	// Token: 0x04000E07 RID: 3591
	private int h;

	// Token: 0x04000E08 RID: 3592
	private string[] strPaint;

	// Token: 0x04000E09 RID: 3593
	private int focus;

	// Token: 0x04000E0A RID: 3594
	private int yt;

	// Token: 0x04000E0B RID: 3595
	private int freeAreaHeight;

	// Token: 0x04000E0C RID: 3596
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000E0D RID: 3597
	private int yP;
}
