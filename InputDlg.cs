using System;

// Token: 0x020000A1 RID: 161
public class InputDlg : Dialog
{
	// Token: 0x060006F8 RID: 1784 RVA: 0x0005CDD0 File Offset: 0x0005AFD0
	public InputDlg()
	{
		this.padLeft = 40;
		if (GameCanvas.w <= 176)
		{
			this.padLeft = 10;
		}
		this.tfInput = new TField();
		this.tfInput.x = this.padLeft + 10;
		this.tfInput.y = GameCanvas.h - mScreen.ITEM_HEIGHT - 43;
		this.tfInput.width = GameCanvas.w - 2 * (this.padLeft + 10);
		this.tfInput.height = mScreen.ITEM_HEIGHT + 2;
		this.tfInput.isFocus = true;
		this.right = this.tfInput.cmdClear;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0005CE88 File Offset: 0x0005B088
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x000067EC File Offset: 0x000049EC
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00006802 File Offset: 0x00004A02
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00006815 File Offset: 0x00004A15
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00005A72 File Offset: 0x00003C72
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000D0C RID: 3340
	protected string[] info;

	// Token: 0x04000D0D RID: 3341
	public TField tfInput;

	// Token: 0x04000D0E RID: 3342
	private int padLeft;
}
