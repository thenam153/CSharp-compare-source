using System;

// Token: 0x020000A6 RID: 166
public class Menu
{
	// Token: 0x0600072A RID: 1834 RVA: 0x0000691B File Offset: 0x00004B1B
	public static void loadBg()
	{
		Menu.imgMenu1 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu1.png");
		Menu.imgMenu2 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu2.png");
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0000693B File Offset: 0x00004B3B
	public void startWithoutCloseButton(MyVector menuItems, int pos)
	{
		this.startAt(menuItems, pos);
		this.disableClose = true;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0005F700 File Offset: 0x0005D900
	public void startAt(MyVector menuItems, int x, int y)
	{
		this.startAt(menuItems, 0);
		this.menuX = x;
		this.menuY = y;
		while (this.menuY + this.menuH > GameCanvas.h)
		{
			this.menuY -= 2;
		}
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0005F750 File Offset: 0x0005D950
	public void startAt(MyVector menuItems, int pos)
	{
		if (this.showMenu)
		{
			return;
		}
		this.isClose = false;
		this.touch = false;
		this.close = false;
		this.tDelay = 0;
		if (menuItems.size() == 1)
		{
			this.menuSelectedItem = 0;
			Command command = (Command)menuItems.elementAt(0);
			if (command != null && command.caption.Equals(mResources.saying))
			{
				command.performAction();
				this.showMenu = false;
				InfoDlg.showWait();
				return;
			}
		}
		SoundMn.gI().openMenu();
		this.isNotClose = new bool[menuItems.size()];
		for (int i = 0; i < this.isNotClose.Length; i++)
		{
			this.isNotClose[i] = false;
		}
		this.disableClose = false;
		ChatPopup.currChatPopup = null;
		Effect2.vEffect2.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		InfoDlg.hide();
		if (menuItems.size() == 0)
		{
			return;
		}
		this.menuItems = menuItems;
		this.menuW = 60;
		this.menuH = 60;
		for (int j = 0; j < menuItems.size(); j++)
		{
			Command command2 = (Command)menuItems.elementAt(j);
			command2.isPlaySoundButton = false;
			int width = mFont.tahoma_7_yellow.getWidth(command2.caption);
			command2.subCaption = mFont.tahoma_7_yellow.splitFontArray(command2.caption, this.menuW - 10);
			Res.outz("c caption= " + command2.caption);
		}
		Menu.menuTemY = new int[menuItems.size()];
		this.menuX = (GameCanvas.w - menuItems.size() * this.menuW) / 2;
		if (this.menuX < 1)
		{
			this.menuX = 1;
		}
		this.menuY = GameCanvas.h - this.menuH - (Paint.hTab + 1) - 1;
		if (GameCanvas.isTouch)
		{
			this.menuY -= 3;
		}
		this.menuY += 27;
		for (int k = 0; k < Menu.menuTemY.Length; k++)
		{
			Menu.menuTemY[k] = GameCanvas.h;
		}
		this.showMenu = true;
		this.menuSelectedItem = 0;
		Menu.cmxLim = this.menuItems.size() * this.menuW - GameCanvas.w;
		if (Menu.cmxLim < 0)
		{
			Menu.cmxLim = 0;
		}
		Menu.cmtoX = 0;
		Menu.cmx = 0;
		Menu.xc = 50;
		this.w = menuItems.size() * this.menuW - 1;
		if (this.w > GameCanvas.w - 2)
		{
			this.w = GameCanvas.w - 2;
		}
		if (GameCanvas.isTouch && !Main.isPC)
		{
			this.menuSelectedItem = -1;
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0005FA14 File Offset: 0x0005DC14
	public bool isScrolling()
	{
		return (!this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] > this.menuY) || (this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] < GameCanvas.h);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0005FA70 File Offset: 0x0005DC70
	public void updateMenuKey()
	{
		if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
		{
			return;
		}
		if (!this.showMenu)
		{
			return;
		}
		if (this.isScrolling())
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			flag = true;
			this.menuSelectedItem--;
			if (this.menuSelectedItem < 0)
			{
				this.menuSelectedItem = this.menuItems.size() - 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
		{
			flag = true;
			this.menuSelectedItem++;
			if (this.menuSelectedItem > this.menuItems.size() - 1)
			{
				this.menuSelectedItem = 0;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
		{
			if (this.center != null)
			{
				if (this.center.idAction > 0)
				{
					if (this.center.actionListener == GameScr.gI())
					{
						GameScr.gI().actionPerform(this.center.idAction, this.center.p);
					}
					else
					{
						this.perform(this.center.idAction, this.center.p);
					}
				}
			}
			else
			{
				this.waitToPerform = 2;
			}
		}
		else if (GameCanvas.keyPressed[12] && !GameScr.gI().isRongThanMenu())
		{
			if (this.isScrolling())
			{
				return;
			}
			if (this.left.idAction > 0)
			{
				this.perform(this.left.idAction, this.left.p);
			}
			else
			{
				this.waitToPerform = 2;
			}
			SoundMn.gI().buttonClose();
		}
		else if (!GameScr.gI().isRongThanMenu() && !this.disableClose && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			if (this.isScrolling())
			{
				return;
			}
			if (!this.close)
			{
				this.close = true;
			}
			this.isClose = true;
			SoundMn.gI().buttonClose();
		}
		if (flag)
		{
			Menu.cmtoX = this.menuSelectedItem * this.menuW + this.menuW - GameCanvas.w / 2;
			if (Menu.cmtoX > Menu.cmxLim)
			{
				Menu.cmtoX = Menu.cmxLim;
			}
			if (Menu.cmtoX < 0)
			{
				Menu.cmtoX = 0;
			}
			if (this.menuSelectedItem == this.menuItems.size() - 1 || this.menuSelectedItem == 0)
			{
				Menu.cmx = Menu.cmtoX;
			}
		}
		if (this.disableClose || !GameCanvas.isPointerJustRelease || GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH) || this.pointerIsDowning || GameScr.gI().isRongThanMenu())
		{
			if (GameCanvas.isPointerDown)
			{
				if (!this.pointerIsDowning && GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH))
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.px;
					}
					this.pointerDownFirstX = GameCanvas.px;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else if (this.pointerIsDowning)
				{
					this.pointerDownTime++;
					if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
					{
						this.pointerDownFirstX = -1000;
						this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
					}
					int num = GameCanvas.px - this.pointerDownLastX[0];
					if (num != 0 && this.menuSelectedItem != -1)
					{
						this.menuSelectedItem = -1;
					}
					for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
					{
						this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.px;
					Menu.cmtoX -= num;
					if (Menu.cmtoX < 0)
					{
						Menu.cmtoX = 0;
					}
					if (Menu.cmtoX > Menu.cmxLim)
					{
						Menu.cmtoX = Menu.cmxLim;
					}
					if (Menu.cmx < 0 || Menu.cmx > Menu.cmxLim)
					{
						num /= 2;
					}
					Menu.cmx -= num;
					if (Menu.cmx < -(GameCanvas.h / 3))
					{
						this.wantUpdateList = true;
					}
					else
					{
						this.wantUpdateList = false;
					}
				}
			}
			if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
			{
				int i2 = GameCanvas.px - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				if (Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
				{
					this.cmRun = 0;
					Menu.cmtoX = Menu.cmx;
					this.pointerDownFirstX = -1000;
					this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
					this.pointerDownTime = 0;
					this.waitToPerform = 10;
				}
				else if (this.menuSelectedItem != -1 && this.pointerDownTime > 5)
				{
					this.pointerDownTime = 0;
					this.waitToPerform = 1;
				}
				else if (this.menuSelectedItem == -1 && !this.isDownWhenRunning)
				{
					if (Menu.cmx < 0)
					{
						Menu.cmtoX = 0;
					}
					else if (Menu.cmx > Menu.cmxLim)
					{
						Menu.cmtoX = Menu.cmxLim;
					}
					else
					{
						int num2 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
						if (num2 > 10)
						{
							num2 = 10;
						}
						else if (num2 < -10)
						{
							num2 = -10;
						}
						else
						{
							num2 = 0;
						}
						this.cmRun = -num2 * 100;
					}
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
			GameCanvas.clearKeyPressed();
			GameCanvas.clearKeyHold();
			return;
		}
		if (this.isScrolling())
		{
			return;
		}
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		GameCanvas.clearAllPointerEvent();
		Res.outz("menu select= " + this.menuSelectedItem);
		this.isClose = true;
		this.close = true;
		SoundMn.gI().buttonClose();
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x000601B8 File Offset: 0x0005E3B8
	public void moveCamera()
	{
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			Menu.cmtoX += this.cmRun / 100;
			if (Menu.cmtoX < 0)
			{
				Menu.cmtoX = 0;
			}
			else if (Menu.cmtoX > Menu.cmxLim)
			{
				Menu.cmtoX = Menu.cmxLim;
			}
			else
			{
				Menu.cmx = Menu.cmtoX;
			}
			this.cmRun = this.cmRun * 9 / 10;
			if (this.cmRun < 100 && this.cmRun > -100)
			{
				this.cmRun = 0;
			}
		}
		if (Menu.cmx != Menu.cmtoX && !this.pointerIsDowning)
		{
			this.cmvx = Menu.cmtoX - Menu.cmx << 2;
			this.cmdx += this.cmvx;
			Menu.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000602C4 File Offset: 0x0005E4C4
	public void paintMenu(mGraphics g)
	{
		if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
		{
			return;
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.translate(-Menu.cmx, 0);
		for (int i = 0; i < this.menuItems.size(); i++)
		{
			if (i == this.menuSelectedItem)
			{
				g.drawImage(Menu.imgMenu2, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
			}
			else
			{
				g.drawImage(Menu.imgMenu1, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
			}
			string[] array = ((Command)this.menuItems.elementAt(i)).subCaption;
			if (array == null)
			{
				array = new string[]
				{
					((Command)this.menuItems.elementAt(i)).caption
				};
			}
			int num = Menu.menuTemY[i] + (this.menuH - array.Length * 14) / 2 + 1;
			for (int j = 0; j < array.Length; j++)
			{
				if (i == this.menuSelectedItem)
				{
					mFont.tahoma_7b_green2.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
				}
				else
				{
					mFont.tahoma_7b_dark.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00060484 File Offset: 0x0005E684
	public void doCloseMenu()
	{
		Res.outz("CLOSE MENU");
		this.isClose = false;
		this.showMenu = false;
		InfoDlg.hide();
		if (this.close)
		{
			GameCanvas.panel.cp = null;
			global::Char.chatPopup = null;
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		else if (this.touch)
		{
			GameCanvas.panel.cp = null;
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
			if (this.menuSelectedItem >= 0)
			{
				Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
				if (command != null)
				{
					SoundMn.gI().buttonClose();
					command.performAction();
				}
			}
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00060568 File Offset: 0x0005E768
	public void performSelect()
	{
		InfoDlg.hide();
		if (this.menuSelectedItem >= 0)
		{
			Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
			if (command != null)
			{
				command.performAction();
			}
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x000605AC File Offset: 0x0005E7AC
	public void updateMenu()
	{
		this.moveCamera();
		if (!this.isClose)
		{
			this.tDelay++;
			for (int i = 0; i < Menu.menuTemY.Length; i++)
			{
				if (Menu.menuTemY[i] > this.menuY)
				{
					int num = Menu.menuTemY[i] - this.menuY >> 1;
					if (num < 1)
					{
						num = 1;
					}
					if (this.tDelay > i)
					{
						Menu.menuTemY[i] -= num;
					}
				}
			}
			if (Menu.menuTemY[Menu.menuTemY.Length - 1] <= this.menuY)
			{
				this.tDelay = 0;
			}
		}
		else
		{
			this.tDelay++;
			for (int j = 0; j < Menu.menuTemY.Length; j++)
			{
				if (Menu.menuTemY[j] < GameCanvas.h)
				{
					int num2 = (GameCanvas.h - Menu.menuTemY[j] >> 1) + 2;
					if (num2 < 1)
					{
						num2 = 1;
					}
					if (this.tDelay > j)
					{
						Menu.menuTemY[j] += num2;
					}
				}
			}
			if (Menu.menuTemY[Menu.menuTemY.Length - 1] >= GameCanvas.h)
			{
				this.tDelay = 0;
				this.doCloseMenu();
			}
		}
		if (Menu.xc != 0)
		{
			Menu.xc >>= 1;
			if (Menu.xc < 0)
			{
				Menu.xc = 0;
			}
		}
		if (this.isScrolling())
		{
			return;
		}
		if (this.waitToPerform > 0)
		{
			this.waitToPerform--;
			if (this.waitToPerform == 0)
			{
				if (this.menuSelectedItem >= 0 && !this.isNotClose[this.menuSelectedItem])
				{
					this.isClose = true;
					this.touch = true;
					GameCanvas.panel.cp = null;
				}
				else
				{
					this.performSelect();
				}
			}
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00003584 File Offset: 0x00001784
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000D7A RID: 3450
	public bool showMenu;

	// Token: 0x04000D7B RID: 3451
	public MyVector menuItems;

	// Token: 0x04000D7C RID: 3452
	public int menuSelectedItem;

	// Token: 0x04000D7D RID: 3453
	public int menuX;

	// Token: 0x04000D7E RID: 3454
	public int menuY;

	// Token: 0x04000D7F RID: 3455
	public int menuW;

	// Token: 0x04000D80 RID: 3456
	public int menuH;

	// Token: 0x04000D81 RID: 3457
	public static int[] menuTemY;

	// Token: 0x04000D82 RID: 3458
	public static int cmtoX;

	// Token: 0x04000D83 RID: 3459
	public static int cmx;

	// Token: 0x04000D84 RID: 3460
	public static int cmdy;

	// Token: 0x04000D85 RID: 3461
	public static int cmvy;

	// Token: 0x04000D86 RID: 3462
	public static int cmxLim;

	// Token: 0x04000D87 RID: 3463
	public static int xc;

	// Token: 0x04000D88 RID: 3464
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000D89 RID: 3465
	private Command right = new Command(mResources.CLOSE, 0, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH + 1);

	// Token: 0x04000D8A RID: 3466
	private Command center;

	// Token: 0x04000D8B RID: 3467
	public static Image imgMenu1;

	// Token: 0x04000D8C RID: 3468
	public static Image imgMenu2;

	// Token: 0x04000D8D RID: 3469
	private bool disableClose;

	// Token: 0x04000D8E RID: 3470
	public int tDelay;

	// Token: 0x04000D8F RID: 3471
	public int w;

	// Token: 0x04000D90 RID: 3472
	private int pa;

	// Token: 0x04000D91 RID: 3473
	private bool trans;

	// Token: 0x04000D92 RID: 3474
	private int pointerDownTime;

	// Token: 0x04000D93 RID: 3475
	private int pointerDownFirstX;

	// Token: 0x04000D94 RID: 3476
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000D95 RID: 3477
	private bool pointerIsDowning;

	// Token: 0x04000D96 RID: 3478
	private bool isDownWhenRunning;

	// Token: 0x04000D97 RID: 3479
	private bool wantUpdateList;

	// Token: 0x04000D98 RID: 3480
	private int waitToPerform;

	// Token: 0x04000D99 RID: 3481
	private int cmRun;

	// Token: 0x04000D9A RID: 3482
	private bool touch;

	// Token: 0x04000D9B RID: 3483
	private bool close;

	// Token: 0x04000D9C RID: 3484
	private int cmvx;

	// Token: 0x04000D9D RID: 3485
	private int cmdx;

	// Token: 0x04000D9E RID: 3486
	private bool isClose;

	// Token: 0x04000D9F RID: 3487
	public bool[] isNotClose;
}
