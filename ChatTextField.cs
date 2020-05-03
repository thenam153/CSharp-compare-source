using System;

// Token: 0x02000095 RID: 149
public class ChatTextField : IActionListener
{
	// Token: 0x060005F5 RID: 1525 RVA: 0x00049D5C File Offset: 0x00047F5C
	public ChatTextField()
	{
		this.tfChat = new TField();
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		if (Main.isWindowsPhone)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		if (Main.isPC && this.tfChat.width > 250)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00049E70 File Offset: 0x00048070
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		if (Main.isPC && this.w > 320)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		if (GameCanvas.isTouch)
		{
			this.tfChat.y -= 5;
			this.y -= 20;
			this.h += 30;
			this.left.x = GameCanvas.w / 2 - 68 - 5;
			this.right.x = GameCanvas.w / 2 + 5;
			this.left.y = GameCanvas.h - 30;
			this.right.y = GameCanvas.h - 30;
		}
		this.cmdChat = new Command();
		ActionChat actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.tfChat.setText(str);
			this.parentScreen.onChatFromMe(str, this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		};
		this.cmdChat.actionChat = actionChat;
		this.cmdChat2 = new Command();
		this.cmdChat2.actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			if (this.parentScreen != null)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00003584 File Offset: 0x00001784
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0004A0A4 File Offset: 0x000482A4
	public void keyPressed(int keyCode)
	{
		if (this.isShow)
		{
			this.tfChat.keyPressed(keyCode);
		}
		if (this.tfChat.getText().Equals(string.Empty))
		{
			this.right.caption = mResources.CLOSE;
		}
		else
		{
			this.right.caption = mResources.DELETE;
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00006303 File Offset: 0x00004503
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0004A108 File Offset: 0x00048308
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		if (!this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0004A198 File Offset: 0x00048398
	public void startChat(IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0004A258 File Offset: 0x00048458
	public void startChat2(IChatable parentScreen, string to)
	{
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			if (!Main.isPC)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00003584 File Offset: 0x00001784
	public void updateKey()
	{
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0004A310 File Offset: 0x00048510
	public void update()
	{
		if (!this.isShow)
		{
			return;
		}
		this.tfChat.update();
		if (Main.isWindowsPhone)
		{
			this.updateWhenKeyBoardVisible();
		}
		if (this.tfChat.justReturnFromTextBox)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		}
		if (Main.isPC)
		{
			if (GameCanvas.keyPressed[15])
			{
				if (this.left != null && this.tfChat.getText() != string.Empty)
				{
					this.left.performAction();
				}
				GameCanvas.keyPressed[15] = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			}
			if (GameCanvas.keyPressed[14])
			{
				if (this.right != null)
				{
					this.right.performAction();
				}
				GameCanvas.keyPressed[14] = false;
			}
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00006324 File Offset: 0x00004524
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0004A434 File Offset: 0x00048634
	public void paint(mGraphics g)
	{
		if (!this.isShow)
		{
			return;
		}
		if (Main.isIPhone)
		{
			return;
		}
		int num = (!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5);
		int num2 = (!Main.isWindowsPhone) ? this.x : 0;
		int num3 = (!Main.isWindowsPhone) ? this.w : GameCanvas.w;
		PopUp.paintPopUp(g, num2, num, num3, this.h, -1, true);
		if (Main.isPC)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}
		this.tfChat.paint(g);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0004A540 File Offset: 0x00048740
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 8000:
			Cout.LogError("perform chat 8000");
			if (this.parentScreen != null)
			{
				long num = mSystem.currentTimeMillis();
				if (num - this.lastChatTime < 1000L)
				{
					return;
				}
				this.lastChatTime = num;
				this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
				this.tfChat.setText(string.Empty);
				this.right.caption = mResources.CLOSE;
				this.tfChat.clearKb();
			}
			break;
		case 8001:
			Cout.LogError("perform chat 8001");
			if (this.tfChat.getText().Equals(string.Empty))
			{
				this.isShow = false;
				this.parentScreen.onCancelChat();
			}
			this.tfChat.clear();
			break;
		}
	}

	// Token: 0x04000ACD RID: 2765
	private static ChatTextField instance;

	// Token: 0x04000ACE RID: 2766
	public TField tfChat;

	// Token: 0x04000ACF RID: 2767
	public bool isShow;

	// Token: 0x04000AD0 RID: 2768
	public IChatable parentScreen;

	// Token: 0x04000AD1 RID: 2769
	private long lastChatTime;

	// Token: 0x04000AD2 RID: 2770
	public Command left;

	// Token: 0x04000AD3 RID: 2771
	public Command cmdChat;

	// Token: 0x04000AD4 RID: 2772
	public Command right;

	// Token: 0x04000AD5 RID: 2773
	public Command center;

	// Token: 0x04000AD6 RID: 2774
	private int x;

	// Token: 0x04000AD7 RID: 2775
	private int y;

	// Token: 0x04000AD8 RID: 2776
	private int w;

	// Token: 0x04000AD9 RID: 2777
	private int h;

	// Token: 0x04000ADA RID: 2778
	private bool isPublic;

	// Token: 0x04000ADB RID: 2779
	public Command cmdChat2;

	// Token: 0x04000ADC RID: 2780
	public int yBegin;

	// Token: 0x04000ADD RID: 2781
	public int yUp;

	// Token: 0x04000ADE RID: 2782
	public int KC;

	// Token: 0x04000ADF RID: 2783
	public string to;

	// Token: 0x04000AE0 RID: 2784
	public string strChat = "Chat ";
}
