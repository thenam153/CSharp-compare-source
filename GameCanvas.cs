using System;
using System.IO;
using System.Threading;
using Assets.src.g;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class GameCanvas : IActionListener
{
	// Token: 0x060008DD RID: 2269 RVA: 0x0008022C File Offset: 0x0007E42C
	public GameCanvas()
	{
		this.g = new mGraphics();
		this.count = 1;
		int num = Rms.loadRMSInt("languageVersion");
		if (num == -1)
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		else if (num != 2)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		if (GameCanvas.clearOldData != 1)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00007360 File Offset: 0x00005560
	public static string getPlatformName()
	{
		return "iphone platform xxx";
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x000803C4 File Offset: 0x0007E5C4
	public void initGame()
	{
		MotherCanvas.instance.setChildCanvas(this);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.isTouch = true;
		if (GameCanvas.w >= 240)
		{
			GameCanvas.isTouchControl = true;
		}
		if (GameCanvas.w < 320)
		{
			GameCanvas.isTouchControlSmallScreen = true;
		}
		if (GameCanvas.w >= 320)
		{
			GameCanvas.isTouchControlLargeScreen = true;
		}
		GameCanvas.msgdlg = new MsgDlg();
		if (GameCanvas.h <= 160)
		{
			Paint.hTab = 15;
			mScreen.cmdH = 17;
		}
		GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
		GameCanvas.instance = this;
		mFont.init();
		mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
		this.initPaint();
		this.loadDust();
		this.loadWaterSplash();
		GameCanvas.panel = new Panel();
		GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
		int num = Rms.loadRMSInt("clienttype");
		if (num != -1)
		{
			if (num > 7)
			{
				Rms.saveRMSInt("clienttype", mSystem.clientType);
			}
			else
			{
				mSystem.clientType = num;
			}
		}
		if (mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty))
		{
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
		}
		GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		GameCanvas.debugUpdate = new MyVector();
		GameCanvas.debugPaint = new MyVector();
		GameCanvas.debugSession = new MyVector();
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i + ".png");
		}
		GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
		GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
		GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
		GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
		Panel.graphics = 1;
		GameCanvas.lowGraphic = true;
		GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
		Res.init();
		SmallImage.loadBigImage();
		Panel.WIDTH_PANEL = 176;
		if (Panel.WIDTH_PANEL > GameCanvas.w)
		{
			Panel.WIDTH_PANEL = GameCanvas.w;
		}
		InfoMe.gI().loadCharId();
		Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
		Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
		Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
		Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
		Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
		Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
		GameCanvas.serverScreen = new ServerListScreen();
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		for (int j = 0; j < 7; j++)
		{
			GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j + ".png");
			GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j + ".png");
		}
		ServerListScreen.createDeleteRMS();
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00007367 File Offset: 0x00005567
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0000736E File Offset: 0x0000556E
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0000737A File Offset: 0x0000557A
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00080704 File Offset: 0x0007E904
	public void update()
	{
		if (GameCanvas.gameTick % 5 == 0)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			if (global::TouchScreenKeyboard.visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				if (GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num = mSystem.currentTimeMillis();
			if (num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			if (num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			if (GameCanvas.taskTick > 0)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			if (GameCanvas.gameTick > 10000)
			{
				GameCanvas.gameTick = 0;
			}
			if (GameCanvas.currentScreen != null)
			{
				if (!this.isLoadData)
				{
					this.data = this.loadData();
					Rms.saveRMSString("acc", this.data[1]);
					Rms.saveRMSString("pass", this.data[2]);
					Rms.saveRMSInt("svselect", int.Parse(this.data[3]) - 1);
					if (this.data[5] == "creat")
					{
						this.isCreat = true;
					}
					this.isLoadData = true;
				}
				if (GameCanvas.currentScreen == GameCanvas.serverScreen && GameCanvas.gameTick % 20 == 0)
				{
					if (GameCanvas.loginScr == null)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.currentScreen = GameCanvas.loginScr;
				}
				if (GameCanvas.currentScreen == GameCanvas.loginScr && !this.isLogin)
				{
					if (!this.isCreat)
					{
						Rms.saveRMSString("acc", this.data[1]);
						Rms.saveRMSString("pass", this.data[2]);
						Rms.saveRMSInt("svselect", int.Parse(this.data[3]) - 1);
						GameCanvas.loginScr.doLogin();
					}
					else
					{
						Service.gI().login2(string.Empty);
					}
					this.isLogin = true;
				}
				if (GameCanvas.currentScreen == CreateCharScr.instance && this.isCreat)
				{
					Service.gI().createChar(GameCanvas.GetRandomString(), int.Parse(this.data[4]), 0);
					this.userAo = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
					this.passAo = Rms.loadRMSString("passAo" + ServerListScreen.ipSelect);
					this.isCreat = false;
				}
				if (GameCanvas.currentScreen == GameCanvas.loginScr && GameCanvas.loginScr.isRes && !this.isRegis)
				{
					GameCanvas.loginScr.isFAQ = false;
					GameCanvas.startWaitDlg(mResources.CONNECTING);
					GameCanvas.connect();
					GameCanvas.startWaitDlg(mResources.REGISTERING);
					Service.gI().requestRegister(this.data[1], this.data[2], this.userAo, this.passAo, GameMidlet.VERSION);
					Rms.saveRMSString("acc", this.data[1]);
					Rms.saveRMSString("pass", this.data[2]);
					Rms.saveRMSInt("svselect", int.Parse(this.data[3]) - 1);
					this.timeRegis = mSystem.currentTimeMillis() + 30000L;
					this.isRegis = true;
				}
				if (GameCanvas.currentScreen == GameCanvas.loginScr && this.timeRegis != 0L && this.timeRegis - mSystem.currentTimeMillis() < 1000L)
				{
					this.isLogin = false;
					this.timeRegis = 0L;
				}
				if (ChatPopup.serverChatPopUp != null)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else if (ChatPopup.currChatPopup != null)
				{
					ChatPopup.currChatPopup.update();
					ChatPopup.currChatPopup.updateKey();
				}
				else if (GameCanvas.currentDialog != null)
				{
					GameCanvas.debug("B", 0);
					GameCanvas.currentDialog.update();
				}
				else if (GameCanvas.menu.showMenu)
				{
					GameCanvas.debug("C", 0);
					GameCanvas.menu.updateMenu();
					GameCanvas.debug("D", 0);
					GameCanvas.menu.updateMenuKey();
				}
				else if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.update();
					if (GameCanvas.panel2 != null)
					{
						if (GameCanvas.isFocusPanel2)
						{
							GameCanvas.panel2.updateKey();
						}
						else
						{
							GameCanvas.panel.updateKey();
						}
						if (GameCanvas.panel2.isShow)
						{
							GameCanvas.panel2.update();
							if (GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H))
							{
								GameCanvas.panel2.updateKey();
							}
						}
					}
					else
					{
						GameCanvas.panel.updateKey();
					}
					if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
					{
						GameCanvas.panel.chatTFUpdateKey();
					}
					else if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
					{
						GameCanvas.panel2.chatTFUpdateKey();
					}
					if (GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine)
					{
						GameCanvas.panel.hide();
					}
				}
				GameCanvas.debug("E", 0);
				if (!GameCanvas.isLoading)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				if (!GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			global::Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			if (this.resetToLoginScr)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.serverScreen);
			}
			GameCanvas.debug("Zzz", 0);
			if (Controller.isConnectOK)
			{
				if (Controller.isMain)
				{
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
					Cout.println("Connect ok");
					ServerListScreen.testConnect = 2;
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					Rms.saveIP(GameMidlet.IP + ":" + GameMidlet.PORT);
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			if (Controller.isDisconnected)
			{
				Debug.Log("disconnect");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession)
					{
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			if (Controller.isConnectionFail)
			{
				Debug.Log("connect fail");
				if (!Controller.isMain)
				{
					if (GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
					{
						GameCanvas.serverScreen.cancel();
					}
					if (GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession)
					{
						this.onConnectionFail();
					}
				}
				else
				{
					this.onConnectionFail();
				}
				Controller.isConnectionFail = false;
			}
			if (Main.isResume)
			{
				Main.isResume = false;
				if (GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00080EEC File Offset: 0x0007F0EC
	public void onDisconnected()
	{
		if (Controller.isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		if (Controller.isLoadingData)
		{
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
			return;
		}
		global::Char.isLoadingMap = false;
		if (Controller.isMain)
		{
			ServerListScreen.testConnect = 0;
		}
		GameCanvas.instance.resetToLoginScrz();
		if (Main.typeClient == 6)
		{
			if (GameCanvas.currentScreen != GameCanvas.serverScreen && GameCanvas.currentScreen != GameCanvas.loginScr)
			{
				GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				new Thread(new ThreadStart(this.loginAgain)).Start();
			}
		}
		else
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			new Thread(new ThreadStart(this.loginAgain)).Start();
		}
		mSystem.endKey();
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00080FCC File Offset: 0x0007F1CC
	public void onConnectionFail()
	{
		if (GameCanvas.currentScreen.Equals(SplashScr.instance))
		{
			if (ServerListScreen.hasConnected == null)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				return;
			}
			if (!ServerListScreen.hasConnected[0])
			{
				ServerListScreen.hasConnected[0] = true;
				ServerListScreen.ipSelect = 0;
				GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
				GameCanvas.connect();
				return;
			}
			if (!ServerListScreen.hasConnected[2])
			{
				ServerListScreen.hasConnected[2] = true;
				ServerListScreen.ipSelect = 2;
				GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
				GameCanvas.connect();
				return;
			}
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			return;
		}
		else
		{
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			if (Controller.isLoadingData)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isConnectionFail = false;
				return;
			}
			GameCanvas.isResume = true;
			LoginScr.isContinueToLogin = false;
			if (GameCanvas.loginScr != null)
			{
				GameCanvas.instance.resetToLoginScrz();
			}
			else
			{
				GameCanvas.loginScr = new LoginScr();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			if (GameCanvas.currentScreen != GameCanvas.serverScreen)
			{
				GameCanvas.startOK(mResources.lost_connection + LoginScr.serverName, 888395, null);
			}
			else
			{
				GameCanvas.endDlg();
			}
			global::Char.isLoadingMap = false;
			if (Controller.isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			mSystem.endKey();
			return;
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0000738D File Offset: 0x0000558D
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000073C1 File Offset: 0x000055C1
	public static void connect()
	{
		if (!Session_ME.gI().isConnected())
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00081144 File Offset: 0x0007F344
	public static void connect2()
	{
		if (!Session_ME2.gI().isConnected())
		{
			Res.outz(string.Concat(new object[]
			{
				"IP2= ",
				GameMidlet.IP2,
				" PORT 2= ",
				GameMidlet.PORT2
			}));
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000073E3 File Offset: 0x000055E3
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x000811A8 File Offset: 0x0007F3A8
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00003584 File Offset: 0x00001784
	public void start()
	{
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0000740B File Offset: 0x0000560B
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00007413 File Offset: 0x00005613
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00003584 File Offset: 0x00001784
	public static void debug(string s, int type)
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x000812BC File Offset: 0x0007F4BC
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = ((!GameCanvas.isTouch) ? 0 : -1);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			if (GameCanvas.panel.tabIcon != null)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
			screen.switchToMe();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00003584 File Offset: 0x00001784
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00003584 File Offset: 0x00001784
	public static void updateBG()
	{
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00081468 File Offset: 0x0007F668
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		if (cmy > GameCanvas.h)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY == 0) ? 0 : (cmy >> detalY)), w, h + ((detalY == 0) ? 0 : (cmy >> detalY)));
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x000814BC File Offset: 0x0007F6BC
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			if (num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks))
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				if (GameScr.gI().isFireWorks && !GameCanvas.lowGraphic)
				{
					FireWorkEff.paint(g);
				}
			}
			else if (GameCanvas.imgBG != null && GameCanvas.imgBG[num] != null)
			{
				if (GameCanvas.moveX[num] != 0)
				{
					GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
				}
				int cmy = GameScr.cmy;
				if (cmy > GameCanvas.h)
				{
					cmy = GameCanvas.h;
				}
				if (GameCanvas.layerSpeed[num] != 0)
				{
					for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
					}
				}
				else
				{
					for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
					{
						g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY <= 0) ? 0 : (cmy >> deltaY)), 0);
					}
				}
				if (color1 != -1)
				{
					if (num == GameCanvas.nBg - 1)
					{
						GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
					}
				}
				if (color2 != -1)
				{
					if (num == 0)
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
					}
					else
					{
						GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
					}
				}
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					if (layer == 1 && GameCanvas.typeBg == 11)
					{
						g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
					}
					if (layer == 1 && GameCanvas.typeBg == 13)
					{
						g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
					}
					if (layer == 3 && TileMap.mapID == 1)
					{
						for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
						{
							g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
						}
					}
				}
				int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
				EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000818DC File Offset: 0x0007FADC
	public static void drawSun1(mGraphics g)
	{
		if (GameCanvas.imgSun != null)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		if (GameCanvas.isBoltEff)
		{
			if (GameCanvas.gameTick % 200 == 0)
			{
				GameCanvas.boltActive = true;
			}
			if (GameCanvas.boltActive)
			{
				GameCanvas.tBolt++;
				if (GameCanvas.tBolt == 10)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				if (GameCanvas.tBolt % 2 == 0)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00003584 File Offset: 0x00001784
	public static void drawSun2(mGraphics g)
	{
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0000741B File Offset: 0x0000561B
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00081970 File Offset: 0x0007FB70
	public static void paintBGGameScr(mGraphics g)
	{
		if (!GameCanvas.isLoadBGok)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		if (global::Char.isLoadingMap)
		{
			return;
		}
		int gW = GameScr.gW;
		int gH = GameScr.gH;
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setColor(13238271);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00003584 File Offset: 0x00001784
	public static void resetBg()
	{
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x000819E4 File Offset: 0x0007FBE4
	public static void getYBackground(int typeBg)
	{
		int gH = GameScr.gH23;
		switch (typeBg)
		{
		case 0:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
			return;
		case 1:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
			GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
			GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
			return;
		case 2:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
			GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
			return;
		case 3:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
			GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
			return;
		case 4:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
			GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
			return;
		case 5:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
			return;
		case 6:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
			GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
			return;
		case 7:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
			GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
			return;
		case 8:
			GameCanvas.yb[0] = gH - 103 + 150;
			if (TileMap.mapID == 103)
			{
				GameCanvas.yb[0] -= 100;
			}
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
			return;
		case 9:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
			return;
		case 10:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
			return;
		case 11:
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
			return;
		case 12:
			GameCanvas.yb[0] = gH + 40;
			GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
			GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
			return;
		case 13:
			GameCanvas.yb[0] = gH - 80;
			GameCanvas.yb[1] = GameCanvas.yb[0];
			return;
		case 14:
			return;
		case 15:
			GameCanvas.yb[0] = gH - 20;
			GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
			return;
		default:
			return;
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00081EE4 File Offset: 0x000800E4
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
		}
		catch (Exception)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00003584 File Offset: 0x00001784
	private static void randomRaintEff(int typeBG)
	{
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00081F14 File Offset: 0x00080114
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00081F64 File Offset: 0x00080164
	public void mapKeyPress(int keyCode)
	{
		if (GameCanvas.currentDialog != null)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		GameCanvas.currentScreen.keyPress(keyCode);
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = true;
			GameCanvas.keyPressed[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[1] = true;
				GameCanvas.keyPressed[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[2] = true;
				GameCanvas.keyPressed[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[3] = true;
				GameCanvas.keyPressed[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[4] = true;
				GameCanvas.keyPressed[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[5] = true;
				GameCanvas.keyPressed[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[6] = true;
				GameCanvas.keyPressed[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = true;
			GameCanvas.keyPressed[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[8] = true;
				GameCanvas.keyPressed[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = true;
			GameCanvas.keyPressed[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = true;
				GameCanvas.keyPressed[14] = true;
				return;
			case 1:
				goto IL_3D7;
			case 2:
				goto IL_3C4;
			case 3:
				goto IL_37C;
			case 4:
				if (GameCanvas.currentScreen is GameScr && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[24] = true;
				GameCanvas.keyPressed[24] = true;
				return;
			case 5:
				if (GameCanvas.currentScreen is GameScr && global::Char.myCharz().isAttack)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[23] = true;
				GameCanvas.keyPressed[23] = true;
				return;
			case 6:
				goto IL_346;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_346;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_3D7;
					}
					if (keyCode == -21)
					{
						goto IL_3C4;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_37C;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = true;
					GameCanvas.keyPressed[17] = true;
					return;
				}
				break;
			}
			if (GameCanvas.currentScreen is GameScr && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[21] = true;
			GameCanvas.keyPressed[21] = true;
			return;
			IL_346:
			if (GameCanvas.currentScreen is GameScr && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[22] = true;
			GameCanvas.keyPressed[22] = true;
			return;
			IL_37C:
			if (GameCanvas.currentScreen is GameScr && global::Char.myCharz().isAttack)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			GameCanvas.keyHold[25] = true;
			GameCanvas.keyPressed[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_3C4:
			GameCanvas.keyHold[12] = true;
			GameCanvas.keyPressed[12] = true;
			return;
			IL_3D7:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
			return;
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00007425 File Offset: 0x00005625
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0008235C File Offset: 0x0008055C
	public void mapKeyRelease(int keyCode)
	{
		switch (keyCode)
		{
		case 48:
			GameCanvas.keyHold[0] = false;
			GameCanvas.keyReleased[0] = true;
			return;
		case 49:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyReleased[1] = true;
			}
			return;
		case 50:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[2] = false;
				GameCanvas.keyReleased[2] = true;
			}
			return;
		case 51:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyReleased[3] = true;
			}
			return;
		case 52:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[4] = false;
				GameCanvas.keyReleased[4] = true;
			}
			return;
		case 53:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[5] = false;
				GameCanvas.keyReleased[5] = true;
			}
			return;
		case 54:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[6] = false;
				GameCanvas.keyReleased[6] = true;
			}
			return;
		case 55:
			GameCanvas.keyHold[7] = false;
			GameCanvas.keyReleased[7] = true;
			return;
		case 56:
			if (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow)
			{
				GameCanvas.keyHold[8] = false;
				GameCanvas.keyReleased[8] = true;
			}
			return;
		case 57:
			GameCanvas.keyHold[9] = false;
			GameCanvas.keyReleased[9] = true;
			return;
		default:
			switch (keyCode + 8)
			{
			case 0:
				GameCanvas.keyHold[14] = false;
				return;
			case 1:
				goto IL_2C8;
			case 2:
				goto IL_2B5;
			case 3:
				goto IL_290;
			case 4:
				GameCanvas.keyHold[24] = false;
				return;
			case 5:
				GameCanvas.keyHold[23] = false;
				return;
			case 6:
				goto IL_286;
			case 7:
				break;
			default:
				if (keyCode == -39)
				{
					goto IL_286;
				}
				if (keyCode != -38)
				{
					if (keyCode == -22)
					{
						goto IL_2C8;
					}
					if (keyCode == -21)
					{
						goto IL_2B5;
					}
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = false;
						return;
					}
					if (keyCode == 10)
					{
						goto IL_290;
					}
					if (keyCode == 35)
					{
						GameCanvas.keyHold[11] = false;
						GameCanvas.keyReleased[11] = true;
						return;
					}
					if (keyCode == 42)
					{
						GameCanvas.keyHold[10] = false;
						GameCanvas.keyReleased[10] = true;
						return;
					}
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
				break;
			}
			GameCanvas.keyHold[21] = false;
			return;
			IL_286:
			GameCanvas.keyHold[22] = false;
			return;
			IL_290:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
			IL_2B5:
			GameCanvas.keyHold[12] = false;
			GameCanvas.keyReleased[12] = true;
			return;
			IL_2C8:
			GameCanvas.keyHold[13] = false;
			GameCanvas.keyReleased[13] = true;
			return;
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00007434 File Offset: 0x00005634
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00007442 File Offset: 0x00005642
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		if (GameCanvas.panel != null && GameCanvas.panel.isShow)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00082644 File Offset: 0x00080844
	public void pointerDragged(int x, int y)
	{
		if (Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10)
		{
			GameCanvas.isPointerClick = false;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		if (GameCanvas.curPos > 3)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00007468 File Offset: 0x00005668
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x000826B0 File Offset: 0x000808B0
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = true;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00007480 File Offset: 0x00005680
	public void pointerReleased(int x, int y)
	{
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustRelease = true;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000074A0 File Offset: 0x000056A0
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x000074D9 File Offset: 0x000056D9
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00082704 File Offset: 0x00080904
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00082734 File Offset: 0x00080934
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0008275C File Offset: 0x0008095C
	public static void checkBackButton()
	{
		if (ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x000827AC File Offset: 0x000809AC
	public void paintChangeMap(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? string.Empty : (" " + LoginScr.timeLogin + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0008285C File Offset: 0x00080A5C
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			if (GameCanvas.currentScreen != null)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (GameCanvas.panel.isShow)
			{
				GameCanvas.panel.paint(this.g);
				if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.paint(this.g);
				}
				if (GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				if (GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			if (GameCanvas.currentDialog != null)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else if (GameCanvas.menu.showMenu)
			{
				GameCanvas.debug("PD", 1);
				GameCanvas.menu.paintMenu(this.g);
			}
			if (GameScr.gI().popUpYesNo != null)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			if (ChatPopup.currChatPopup != null)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			if (ChatPopup.serverChatPopUp != null)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
				{
					effect.paint(this.g);
				}
			}
			if (global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait)
			{
				this.paintChangeMap(this.g);
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			if (mResources.language == 0 && GameCanvas.open3Hour && !GameCanvas.isLoading)
			{
				if (GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				if (GameCanvas.currentScreen == CreateCharScr.instance)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00007504 File Offset: 0x00005704
	public static void endDlg()
	{
		if (GameCanvas.inputDlg != null)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0000752C File Offset: 0x0000572C
	public static void startOKDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0000755F File Offset: 0x0000575F
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0000755F File Offset: 0x0000575F
	public static void startOKDlg(string info, bool isError)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0000759D File Offset: 0x0000579D
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000075AA File Offset: 0x000057AA
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000075DD File Offset: 0x000057DD
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00082B60 File Offset: 0x00080D60
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0000760C File Offset: 0x0000580C
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00082BBC File Offset: 0x00080DBC
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			if (m < 1000)
			{
				text = m + text;
				break;
			}
			int num2 = m % 1000;
			if (num2 == 0)
			{
				text = ".000" + text;
			}
			else if (num2 < 10)
			{
				text = ".00" + num2 + text;
			}
			else if (num2 < 100)
			{
				text = ".0" + num2 + text;
			}
			else
			{
				text = "." + num2 + text;
			}
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0000762B File Offset: 0x0000582B
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00007636 File Offset: 0x00005836
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00003584 File Offset: 0x00001784
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00006472 File Offset: 0x00004672
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00082C6C File Offset: 0x00080E6C
	public static Image loadImageRMS(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				sbyte[] array2 = Rms.loadRMS("x" + mGraphics.zoomLevel + array[array.Length - 1]);
				if (array2 != null)
				{
					result = Image.createImage(array2, 0, array2.Length);
				}
			}
			catch (Exception)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
			}
		}
		return result;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00082D30 File Offset: 0x00080F30
	public static Image loadImage(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image result = null;
		try
		{
			result = Image.createImage(path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00082D94 File Offset: 0x00080F94
	public static string cutPng(string str)
	{
		string result = str;
		if (str.Contains(".png"))
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00007641 File Offset: 0x00005841
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00082DC4 File Offset: 0x00080FC4
	public bool startDust(int dir, int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (dir != 1) ? 1 : 0;
		if (this.dustState[num] != -1)
		{
			return false;
		}
		this.dustState[num] = 0;
		this.dustX[num] = x;
		this.dustY[num] = y;
		return true;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00082E0C File Offset: 0x0008100C
	public void loadWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		GameCanvas.imgWS = new Image[3];
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i + ".png");
		}
		GameCanvas.wsX = new int[2];
		GameCanvas.wsY = new int[2];
		GameCanvas.wsState = new int[2];
		GameCanvas.wsF = new int[2];
		GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00082E98 File Offset: 0x00081098
	public bool startWaterSplash(int x, int y)
	{
		if (GameCanvas.lowGraphic)
		{
			return false;
		}
		int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
		if (GameCanvas.wsState[num] != -1)
		{
			return false;
		}
		GameCanvas.wsState[num] = 0;
		GameCanvas.wsX[num] = x;
		GameCanvas.wsY[num] = y;
		return true;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00082EE4 File Offset: 0x000810E4
	public void updateWaterSplash()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (GameCanvas.wsState[i] != -1)
			{
				GameCanvas.wsY[i]--;
				if (GameCanvas.gameTick % 2 == 0)
				{
					GameCanvas.wsState[i]++;
					if (GameCanvas.wsState[i] > 2)
					{
						GameCanvas.wsState[i] = -1;
					}
					else
					{
						GameCanvas.wsF[i] = GameCanvas.wsState[i];
					}
				}
			}
		}
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00082F5C File Offset: 0x0008115C
	public void updateDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1)
			{
				this.dustState[i]++;
				if (this.dustState[i] >= 5)
				{
					this.dustState[i] = -1;
				}
				if (i == 0)
				{
					this.dustX[i]--;
				}
				else
				{
					this.dustX[i]++;
				}
				this.dustY[i]--;
			}
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00007652 File Offset: 0x00005852
	public static bool isPaint(int x, int y)
	{
		return x >= GameScr.cmx && x <= GameScr.cmx + GameScr.gW && y >= GameScr.cmy && y <= GameScr.cmy + GameScr.gH + 30;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00082FE8 File Offset: 0x000811E8
	public void paintDust(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]))
			{
				g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
			}
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00083054 File Offset: 0x00081254
	public void loadDust()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (GameCanvas.imgDust == null)
		{
			GameCanvas.imgDust = new Image[2][];
			for (int i = 0; i < GameCanvas.imgDust.Length; i++)
			{
				GameCanvas.imgDust[i] = new Image[5];
			}
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					GameCanvas.imgDust[j][k] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/e/d",
						j,
						k,
						".png"
					}));
				}
			}
		}
		this.dustX = new int[2];
		this.dustY = new int[2];
		this.dustState = new int[2];
		this.dustState[0] = (this.dustState[1] = -1);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00083128 File Offset: 0x00081328
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00007688 File Offset: 0x00005888
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x000074A0 File Offset: 0x000056A0
	public static bool isPointer(int x, int y, int w, int h)
	{
		return (GameCanvas.isPointerDown || GameCanvas.isPointerJustRelease) && GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0008315C File Offset: 0x0008135C
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 88810:
		{
			int playerMapId = (int)p;
			GameCanvas.endDlg();
			Service.gI().acceptInviteTrade(playerMapId);
			return;
		}
		case 88811:
			GameCanvas.endDlg();
			Service.gI().cancelInviteTrade();
			return;
		case 88812:
		case 88813:
		case 88816:
		case 88830:
		case 88831:
		case 88832:
		case 88833:
		case 88834:
		case 88835:
		case 88838:
			goto IL_3DE;
		case 88814:
		{
			Item[] items = (Item[])p;
			GameCanvas.endDlg();
			Service.gI().crystalCollectLock(items);
			return;
		}
		case 88815:
			return;
		case 88817:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88818:
		{
			short menuId = (short)p;
			Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
			GameCanvas.endDlg();
			return;
		}
		case 88819:
		{
			short menuId2 = (short)p;
			Service.gI().menuId(menuId2);
			return;
		}
		case 88820:
		{
			string[] array = (string[])p;
			if (global::Char.myCharz().npcFocus == null)
			{
				return;
			}
			int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
			if (array.Length > 1)
			{
				MyVector myVector = new MyVector();
				for (int i = 0; i < array.Length - 1; i++)
				{
					myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
				}
				GameCanvas.menu.startAt(myVector, 3);
				return;
			}
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
			return;
		}
		case 88821:
		{
			int menuId3 = (int)p;
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
			return;
		}
		case 88822:
			ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
			Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 88823:
			GameCanvas.startOKDlg(mResources.SENTMSG);
			return;
		case 88824:
			GameCanvas.startOKDlg(mResources.NOSENDMSG);
			return;
		case 88825:
			GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
			return;
		case 88826:
			GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
			return;
		case 88827:
			GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
			return;
		case 88828:
			GameCanvas.startOKDlg(mResources.sendMsgFail);
			return;
		case 88829:
		{
			string text = GameCanvas.inputDlg.tfInput.getText();
			if (text.Equals(string.Empty))
			{
				return;
			}
			Service.gI().changeName(text, (int)p);
			InfoDlg.showWait();
			return;
		}
		case 88836:
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
			GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
			return;
		case 88837:
		{
			string text2 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			try
			{
				Service.gI().openLockAccProtect(int.Parse(text2.Trim()));
				return;
			}
			catch (Exception ex)
			{
				Cout.println("Loi tai 88837 " + ex.ToString());
				return;
			}
			break;
		}
		case 88839:
			break;
		default:
			goto IL_3DE;
		}
		string text3 = GameCanvas.inputDlg.tfInput.getText();
		GameCanvas.endDlg();
		if (text3.Length < 6 || text3.Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			return;
		}
		try
		{
			GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text3, 8882, null);
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
		}
		return;
		IL_3DE:
		switch (idAction)
		{
		case 8881:
		{
			string url = (string)p;
			try
			{
				GameMidlet.instance.platformRequest(url);
			}
			catch (Exception)
			{
			}
			GameCanvas.currentDialog = null;
			return;
		}
		case 8882:
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
			return;
		case 8884:
			GameCanvas.endDlg();
			GameCanvas.loginScr.switchToMe();
			return;
		case 8885:
			GameMidlet.instance.exit();
			return;
		case 8886:
		{
			GameCanvas.endDlg();
			string name = (string)p;
			Service.gI().addFriend(name);
			return;
		}
		case 8887:
		{
			GameCanvas.endDlg();
			int charId = (int)p;
			Service.gI().addPartyAccept(charId);
			return;
		}
		case 8888:
		{
			int charId2 = (int)p;
			Service.gI().addPartyCancel(charId2);
			GameCanvas.endDlg();
			return;
		}
		case 8889:
		{
			string str = (string)p;
			GameCanvas.endDlg();
			Service.gI().acceptPleaseParty(str);
			return;
		}
		}
		switch (idAction)
		{
		case 888391:
		{
			string s = (string)p;
			GameCanvas.endDlg();
			Service.gI().clearAccProtect(int.Parse(s));
			return;
		}
		case 888392:
			Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
			return;
		case 888393:
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.doLogin();
			Main.closeKeyBoard();
			return;
		case 888394:
			GameCanvas.endDlg();
			return;
		case 888395:
			GameCanvas.endDlg();
			return;
		case 888396:
			GameCanvas.endDlg();
			return;
		case 888397:
		{
			string text4 = (string)p;
			return;
		}
		default:
			if (idAction == 999)
			{
				mSystem.closeBanner();
				GameCanvas.endDlg();
				return;
			}
			if (idAction != 9000)
			{
				if (idAction != 9999)
				{
					if (idAction != 101023)
					{
						if (idAction != 888361)
						{
							return;
						}
						string text5 = GameCanvas.inputDlg.tfInput.getText();
						GameCanvas.endDlg();
						if (text5.Length < 6 || text5.Equals(string.Empty))
						{
							GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
							return;
						}
						try
						{
							Service.gI().activeAccProtect(int.Parse(text5));
							break;
						}
						catch (Exception ex2)
						{
							GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
							Cout.println("Loi tai 888361 Gamescavas " + ex2.ToString());
							break;
						}
					}
					Main.numberQuit = 0;
					return;
				}
				GameCanvas.endDlg();
				GameCanvas.connect();
				Service.gI().setClientType();
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				return;
			}
			else
			{
				GameCanvas.endDlg();
				SplashScr.imgLogo = null;
				SmallImage.loadBigRMS();
				mSystem.gcc();
				ServerListScreen.bigOk = true;
				ServerListScreen.loadScreen = true;
				GameScr.gI().loadGameScr();
				if (GameCanvas.currentScreen != GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe2();
					return;
				}
				return;
			}
			break;
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00007691 File Offset: 0x00005891
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00003584 File Offset: 0x00001784
	public static void backToRegister()
	{
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00083824 File Offset: 0x00081A24
	public string[] loadData()
	{
		FileStream fileStream = new FileStream("Dragonboy166_Data//log", FileMode.Open, FileAccess.Read);
		StreamReader streamReader = new StreamReader(fileStream);
		string[] result = streamReader.ReadLine().Split(new char[]
		{
			'|'
		});
		streamReader.Close();
		fileStream.Close();
		return result;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000076C2 File Offset: 0x000058C2
	public static string GetRandomString()
	{
		return Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00083868 File Offset: 0x00081A68
	public void loginAgain()
	{
		GameCanvas.currentScreen = GameCanvas.loginScr;
		Thread.Sleep(1000);
		GameCanvas.startOKDlg("Login lại sau 25s");
		Thread.Sleep(25000);
		Rms.saveRMSString("acc", this.data[1]);
		Rms.saveRMSString("pass", this.data[2]);
		Rms.saveRMSInt("svselect", int.Parse(this.data[3]) - 1);
		Thread.Sleep(50);
		GameCanvas.loginScr.doLogin();
	}

	// Token: 0x04001077 RID: 4215
	public static bool open3Hour;

	// Token: 0x04001078 RID: 4216
	public static bool lowGraphic = false;

	// Token: 0x04001079 RID: 4217
	public static bool isMoveNumberPad = true;

	// Token: 0x0400107A RID: 4218
	public static bool isLoading;

	// Token: 0x0400107B RID: 4219
	public static bool isTouch = false;

	// Token: 0x0400107C RID: 4220
	public static bool isTouchControl;

	// Token: 0x0400107D RID: 4221
	public static bool isTouchControlSmallScreen;

	// Token: 0x0400107E RID: 4222
	public static bool isTouchControlLargeScreen;

	// Token: 0x0400107F RID: 4223
	public static bool isConnectFail;

	// Token: 0x04001080 RID: 4224
	public static GameCanvas instance;

	// Token: 0x04001081 RID: 4225
	public static bool bRun;

	// Token: 0x04001082 RID: 4226
	public static bool[] keyPressed = new bool[30];

	// Token: 0x04001083 RID: 4227
	public static bool[] keyReleased = new bool[30];

	// Token: 0x04001084 RID: 4228
	public static bool[] keyHold = new bool[30];

	// Token: 0x04001085 RID: 4229
	public static bool isPointerDown;

	// Token: 0x04001086 RID: 4230
	public static bool isPointerClick;

	// Token: 0x04001087 RID: 4231
	public static bool isPointerJustRelease;

	// Token: 0x04001088 RID: 4232
	public static int px;

	// Token: 0x04001089 RID: 4233
	public static int py;

	// Token: 0x0400108A RID: 4234
	public static int pxFirst;

	// Token: 0x0400108B RID: 4235
	public static int pyFirst;

	// Token: 0x0400108C RID: 4236
	public static int pxLast;

	// Token: 0x0400108D RID: 4237
	public static int pyLast;

	// Token: 0x0400108E RID: 4238
	public static int pxMouse;

	// Token: 0x0400108F RID: 4239
	public static int pyMouse;

	// Token: 0x04001090 RID: 4240
	public static Position[] arrPos = new Position[4];

	// Token: 0x04001091 RID: 4241
	public static int gameTick;

	// Token: 0x04001092 RID: 4242
	public static int taskTick;

	// Token: 0x04001093 RID: 4243
	public static bool isEff1;

	// Token: 0x04001094 RID: 4244
	public static bool isEff2;

	// Token: 0x04001095 RID: 4245
	public static long timeTickEff1;

	// Token: 0x04001096 RID: 4246
	public static long timeTickEff2;

	// Token: 0x04001097 RID: 4247
	public static int w;

	// Token: 0x04001098 RID: 4248
	public static int h;

	// Token: 0x04001099 RID: 4249
	public static int hw;

	// Token: 0x0400109A RID: 4250
	public static int hh;

	// Token: 0x0400109B RID: 4251
	public static int wd3;

	// Token: 0x0400109C RID: 4252
	public static int hd3;

	// Token: 0x0400109D RID: 4253
	public static int w2d3;

	// Token: 0x0400109E RID: 4254
	public static int h2d3;

	// Token: 0x0400109F RID: 4255
	public static int w3d4;

	// Token: 0x040010A0 RID: 4256
	public static int h3d4;

	// Token: 0x040010A1 RID: 4257
	public static int wd6;

	// Token: 0x040010A2 RID: 4258
	public static int hd6;

	// Token: 0x040010A3 RID: 4259
	public static mScreen currentScreen;

	// Token: 0x040010A4 RID: 4260
	public static Menu menu = new Menu();

	// Token: 0x040010A5 RID: 4261
	public static Panel panel;

	// Token: 0x040010A6 RID: 4262
	public static Panel panel2;

	// Token: 0x040010A7 RID: 4263
	public static LoginScr loginScr;

	// Token: 0x040010A8 RID: 4264
	public static RegisterScreen registerScr;

	// Token: 0x040010A9 RID: 4265
	public static Dialog currentDialog;

	// Token: 0x040010AA RID: 4266
	public static MsgDlg msgdlg;

	// Token: 0x040010AB RID: 4267
	public static InputDlg inputDlg;

	// Token: 0x040010AC RID: 4268
	public static MyVector currentPopup = new MyVector();

	// Token: 0x040010AD RID: 4269
	public static int requestLoseCount;

	// Token: 0x040010AE RID: 4270
	public static MyVector listPoint;

	// Token: 0x040010AF RID: 4271
	public static Paint paintz;

	// Token: 0x040010B0 RID: 4272
	public static bool isGetResFromServer;

	// Token: 0x040010B1 RID: 4273
	public static Image[] imgBG;

	// Token: 0x040010B2 RID: 4274
	public static int skyColor;

	// Token: 0x040010B3 RID: 4275
	public static int curPos = 0;

	// Token: 0x040010B4 RID: 4276
	public static int[] bgW;

	// Token: 0x040010B5 RID: 4277
	public static int[] bgH;

	// Token: 0x040010B6 RID: 4278
	public static int planet = 0;

	// Token: 0x040010B7 RID: 4279
	private mGraphics g;

	// Token: 0x040010B8 RID: 4280
	public static Image img12;

	// Token: 0x040010B9 RID: 4281
	public static Image[] imgBlue = new Image[7];

	// Token: 0x040010BA RID: 4282
	public static Image[] imgViolet = new Image[7];

	// Token: 0x040010BB RID: 4283
	public static bool isPlaySound = true;

	// Token: 0x040010BC RID: 4284
	private static int clearOldData;

	// Token: 0x040010BD RID: 4285
	public static int timeOpenKeyBoard;

	// Token: 0x040010BE RID: 4286
	public static bool isFocusPanel2;

	// Token: 0x040010BF RID: 4287
	public bool isPaintCarret;

	// Token: 0x040010C0 RID: 4288
	public static MyVector debugUpdate;

	// Token: 0x040010C1 RID: 4289
	public static MyVector debugPaint;

	// Token: 0x040010C2 RID: 4290
	public static MyVector debugSession;

	// Token: 0x040010C3 RID: 4291
	private static bool isShowErrorForm = false;

	// Token: 0x040010C4 RID: 4292
	public static bool paintBG;

	// Token: 0x040010C5 RID: 4293
	public static int gsskyHeight;

	// Token: 0x040010C6 RID: 4294
	public static int gsgreenField1Y;

	// Token: 0x040010C7 RID: 4295
	public static int gsgreenField2Y;

	// Token: 0x040010C8 RID: 4296
	public static int gshouseY;

	// Token: 0x040010C9 RID: 4297
	public static int gsmountainY;

	// Token: 0x040010CA RID: 4298
	public static int bgLayer0y;

	// Token: 0x040010CB RID: 4299
	public static int bgLayer1y;

	// Token: 0x040010CC RID: 4300
	public static Image imgCloud;

	// Token: 0x040010CD RID: 4301
	public static Image imgSun;

	// Token: 0x040010CE RID: 4302
	public static Image imgSun2;

	// Token: 0x040010CF RID: 4303
	public static Image imgClear;

	// Token: 0x040010D0 RID: 4304
	public static Image[] imgBorder = new Image[3];

	// Token: 0x040010D1 RID: 4305
	public static int borderConnerW;

	// Token: 0x040010D2 RID: 4306
	public static int borderConnerH;

	// Token: 0x040010D3 RID: 4307
	public static int borderCenterW;

	// Token: 0x040010D4 RID: 4308
	public static int borderCenterH;

	// Token: 0x040010D5 RID: 4309
	public static int[] cloudX;

	// Token: 0x040010D6 RID: 4310
	public static int[] cloudY;

	// Token: 0x040010D7 RID: 4311
	public static int sunX;

	// Token: 0x040010D8 RID: 4312
	public static int sunY;

	// Token: 0x040010D9 RID: 4313
	public static int sunX2;

	// Token: 0x040010DA RID: 4314
	public static int sunY2;

	// Token: 0x040010DB RID: 4315
	public static int[] layerSpeed;

	// Token: 0x040010DC RID: 4316
	public static int[] moveX;

	// Token: 0x040010DD RID: 4317
	public static int[] moveXSpeed;

	// Token: 0x040010DE RID: 4318
	public static bool isBoltEff;

	// Token: 0x040010DF RID: 4319
	public static bool boltActive;

	// Token: 0x040010E0 RID: 4320
	public static int tBolt;

	// Token: 0x040010E1 RID: 4321
	public static int typeBg = -1;

	// Token: 0x040010E2 RID: 4322
	public static int transY;

	// Token: 0x040010E3 RID: 4323
	public static int[] yb = new int[5];

	// Token: 0x040010E4 RID: 4324
	public static int[] colorTop;

	// Token: 0x040010E5 RID: 4325
	public static int[] colorBotton;

	// Token: 0x040010E6 RID: 4326
	public static int yb1;

	// Token: 0x040010E7 RID: 4327
	public static int yb2;

	// Token: 0x040010E8 RID: 4328
	public static int yb3;

	// Token: 0x040010E9 RID: 4329
	public static int nBg = 0;

	// Token: 0x040010EA RID: 4330
	public static int lastBg = -1;

	// Token: 0x040010EB RID: 4331
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x040010EC RID: 4332
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x040010ED RID: 4333
	public static Image imgCaycot;

	// Token: 0x040010EE RID: 4334
	public static Image tam;

	// Token: 0x040010EF RID: 4335
	public static int typeBackGround = -1;

	// Token: 0x040010F0 RID: 4336
	public static int saveIDBg = -10;

	// Token: 0x040010F1 RID: 4337
	public static bool isLoadBGok;

	// Token: 0x040010F2 RID: 4338
	private static long lastTimePress = 0L;

	// Token: 0x040010F3 RID: 4339
	public static int keyAsciiPress;

	// Token: 0x040010F4 RID: 4340
	public static int pXYScrollMouse;

	// Token: 0x040010F5 RID: 4341
	private static Image imgSignal;

	// Token: 0x040010F6 RID: 4342
	public static MyVector flyTexts = new MyVector();

	// Token: 0x040010F7 RID: 4343
	public int longTime;

	// Token: 0x040010F8 RID: 4344
	public static bool isPointerJustDown = false;

	// Token: 0x040010F9 RID: 4345
	private int count;

	// Token: 0x040010FA RID: 4346
	public static bool csWait;

	// Token: 0x040010FB RID: 4347
	public static MyRandom r = new MyRandom();

	// Token: 0x040010FC RID: 4348
	public static bool isBlackScreen;

	// Token: 0x040010FD RID: 4349
	public static int[] bgSpeed;

	// Token: 0x040010FE RID: 4350
	public static int cmdBarX;

	// Token: 0x040010FF RID: 4351
	public static int cmdBarY;

	// Token: 0x04001100 RID: 4352
	public static int cmdBarW;

	// Token: 0x04001101 RID: 4353
	public static int cmdBarH;

	// Token: 0x04001102 RID: 4354
	public static int cmdBarLeftW;

	// Token: 0x04001103 RID: 4355
	public static int cmdBarRightW;

	// Token: 0x04001104 RID: 4356
	public static int cmdBarCenterW;

	// Token: 0x04001105 RID: 4357
	public static int hpBarX;

	// Token: 0x04001106 RID: 4358
	public static int hpBarY;

	// Token: 0x04001107 RID: 4359
	public static int hpBarW;

	// Token: 0x04001108 RID: 4360
	public static int expBarW;

	// Token: 0x04001109 RID: 4361
	public static int lvPosX;

	// Token: 0x0400110A RID: 4362
	public static int moneyPosX;

	// Token: 0x0400110B RID: 4363
	public static int hpBarH;

	// Token: 0x0400110C RID: 4364
	public static int girlHPBarY;

	// Token: 0x0400110D RID: 4365
	public int timeOut;

	// Token: 0x0400110E RID: 4366
	public int[] dustX;

	// Token: 0x0400110F RID: 4367
	public int[] dustY;

	// Token: 0x04001110 RID: 4368
	public int[] dustState;

	// Token: 0x04001111 RID: 4369
	public static int[] wsX;

	// Token: 0x04001112 RID: 4370
	public static int[] wsY;

	// Token: 0x04001113 RID: 4371
	public static int[] wsState;

	// Token: 0x04001114 RID: 4372
	public static int[] wsF;

	// Token: 0x04001115 RID: 4373
	public static Image[] imgWS;

	// Token: 0x04001116 RID: 4374
	public static Image imgShuriken;

	// Token: 0x04001117 RID: 4375
	public static Image[][] imgDust;

	// Token: 0x04001118 RID: 4376
	public static bool isResume;

	// Token: 0x04001119 RID: 4377
	public static ServerListScreen serverScreen;

	// Token: 0x0400111A RID: 4378
	public bool resetToLoginScr;

	// Token: 0x0400111B RID: 4379
	public static long timeNow;

	// Token: 0x0400111C RID: 4380
	public bool isLoadData;

	// Token: 0x0400111D RID: 4381
	public string[] data;

	// Token: 0x0400111E RID: 4382
	public bool isCreat;

	// Token: 0x0400111F RID: 4383
	public bool isLogin;

	// Token: 0x04001120 RID: 4384
	public string userAo;

	// Token: 0x04001121 RID: 4385
	public string passAo;

	// Token: 0x04001122 RID: 4386
	public bool isRegis;

	// Token: 0x04001123 RID: 4387
	public long timeRegis;
}
