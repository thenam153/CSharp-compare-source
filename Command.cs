using System;

// Token: 0x0200004C RID: 76
public class Command
{
	// Token: 0x060002B6 RID: 694 RVA: 0x00015944 File Offset: 0x00013B44
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00004A6E File Offset: 0x00002C6E
	public Command()
	{
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x000159AC File Offset: 0x00013BAC
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00015A04 File Offset: 0x00013C04
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00004A9E File Offset: 0x00002C9E
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00015A54 File Offset: 0x00013C54
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00004ADC File Offset: 0x00002CDC
	public void perform(string str)
	{
		if (this.actionChat != null)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00015AAC File Offset: 0x00013CAC
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		if (this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null))
		{
			SoundMn.gI().buttonClick();
		}
		if (this.idAction > 0)
		{
			if (this.actionListener != null)
			{
				this.actionListener.perform(this.idAction, this.p);
			}
			else
			{
				GameScr.gI().actionPerform(this.idAction, this.p);
			}
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00004AF5 File Offset: 0x00002CF5
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00015B5C File Offset: 0x00013D5C
	public void paint(mGraphics g)
	{
		if (this.img != null)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			if (this.isFocus)
			{
				if (this.imgFocus == null)
				{
					if (this.cmdClosePanel)
					{
						g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
					}
					else
					{
						g.drawImage(ItemMap.imageFlare, this.x - ((!this.img.Equals(GameScr.imgMenu)) ? 0 : 10), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				else
				{
					g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				}
			}
			if (this.caption != "menu" && this.caption != null)
			{
				if (!this.isFocus)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
			}
			return;
		}
		if (this.caption != string.Empty)
		{
			if (this.type == 1)
			{
				if (!this.isFocus)
				{
					Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 160, g);
				}
				else
				{
					Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 160, g);
				}
			}
			else if (!this.isFocus)
			{
				Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 76, g);
			}
			else
			{
				Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 76, g);
			}
		}
		int num;
		if (this.type == 1)
		{
			num = this.x + this.hw;
		}
		else
		{
			num = this.x + 38;
		}
		if (!this.isFocus)
		{
			mFont.tahoma_7b_dark.drawString(g, this.caption, num, this.y + 7, 2);
		}
		else
		{
			mFont.tahoma_7b_green2.drawString(g, this.caption, num, this.y + 7, 2);
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00015E2C File Offset: 0x0001402C
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		if (num > 0)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00015EA4 File Offset: 0x000140A4
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
		{
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00015F04 File Offset: 0x00014104
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h))
		{
			Res.outz("w= " + this.w);
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000477 RID: 1143
	public ActionChat actionChat;

	// Token: 0x04000478 RID: 1144
	public string caption;

	// Token: 0x04000479 RID: 1145
	public string[] subCaption;

	// Token: 0x0400047A RID: 1146
	public IActionListener actionListener;

	// Token: 0x0400047B RID: 1147
	public int idAction;

	// Token: 0x0400047C RID: 1148
	public bool isPlaySoundButton = true;

	// Token: 0x0400047D RID: 1149
	public Image img;

	// Token: 0x0400047E RID: 1150
	public Image imgFocus;

	// Token: 0x0400047F RID: 1151
	public int x;

	// Token: 0x04000480 RID: 1152
	public int y;

	// Token: 0x04000481 RID: 1153
	public int w = mScreen.cmdW;

	// Token: 0x04000482 RID: 1154
	public int h = mScreen.cmdH;

	// Token: 0x04000483 RID: 1155
	public int hw;

	// Token: 0x04000484 RID: 1156
	private int lenCaption;

	// Token: 0x04000485 RID: 1157
	public bool isFocus;

	// Token: 0x04000486 RID: 1158
	public object p;

	// Token: 0x04000487 RID: 1159
	public int type;

	// Token: 0x04000488 RID: 1160
	public string caption2 = string.Empty;

	// Token: 0x04000489 RID: 1161
	public static Image btn0left;

	// Token: 0x0400048A RID: 1162
	public static Image btn0mid;

	// Token: 0x0400048B RID: 1163
	public static Image btn0right;

	// Token: 0x0400048C RID: 1164
	public static Image btn1left;

	// Token: 0x0400048D RID: 1165
	public static Image btn1mid;

	// Token: 0x0400048E RID: 1166
	public static Image btn1right;

	// Token: 0x0400048F RID: 1167
	public bool cmdClosePanel;
}
