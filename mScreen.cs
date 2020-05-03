using System;

// Token: 0x020000AE RID: 174
public class mScreen
{
	// Token: 0x0600087C RID: 2172 RVA: 0x00007033 File Offset: 0x00005233
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		if (GameCanvas.currentScreen != null)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		Cout.LogError3("cur Screen: " + GameCanvas.currentScreen);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void unLoad()
	{
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00003584 File Offset: 0x00001784
	public static void initPos()
	{
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void update()
	{
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0007C070 File Offset: 0x0007A270
	public virtual void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
		}
		if (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left))
		{
			GameCanvas.keyPressed[12] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().left != null)
				{
					ChatTextField.gI().left.performAction();
				}
			}
			else if (this.left != null)
			{
				this.left.performAction();
			}
		}
		if (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right))
		{
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
				}
			}
			else if (this.right != null)
			{
				this.right.performAction();
			}
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0007C1E8 File Offset: 0x0007A3E8
	public static bool getCmdPointerLast(Command cmd)
	{
		if (cmd == null)
		{
			return false;
		}
		if (cmd.x >= 0 && cmd.y != 0)
		{
			return cmd.isPointerPressInside();
		}
		if (GameCanvas.currentDialog != null)
		{
			if (GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if ((cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		else
		{
			if (cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if ((cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0007C484 File Offset: 0x0007A684
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		if (!ChatTextField.gI().isShow || !Main.isPC)
		{
			if (GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}
	}

	// Token: 0x04000F79 RID: 3961
	public Command left;

	// Token: 0x04000F7A RID: 3962
	public Command center;

	// Token: 0x04000F7B RID: 3963
	public Command right;

	// Token: 0x04000F7C RID: 3964
	public Command cmdClose;

	// Token: 0x04000F7D RID: 3965
	public static int ITEM_HEIGHT;

	// Token: 0x04000F7E RID: 3966
	public static int yOpenKeyBoard = 100;

	// Token: 0x04000F7F RID: 3967
	public static int cmdW = 68;

	// Token: 0x04000F80 RID: 3968
	public static int cmdH = 26;

	// Token: 0x04000F81 RID: 3969
	public static int keyTouch = -1;

	// Token: 0x04000F82 RID: 3970
	public static int keyMouse = -1;
}
