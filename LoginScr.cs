using System;

// Token: 0x020000A4 RID: 164
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x06000704 RID: 1796 RVA: 0x0005CF50 File Offset: 0x0005B150
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		if (GameCanvas.h > 200)
		{
			this.defYL = GameCanvas.hh - 80;
		}
		else
		{
			this.defYL = GameCanvas.hh - 65;
		}
		this.resetLogo();
		int num = (GameCanvas.w < 200) ? 140 : 160;
		this.wC = num;
		this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
		if (GameCanvas.h <= 160)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num2 = Rms.loadRMSInt("check");
		if (num2 == 1)
		{
			this.isCheck = true;
		}
		else if (num2 == 2)
		{
			this.isCheck = false;
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
		if (this.cmdCallHotline == null)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				int num3 = 2;
				this.cmdCallHotline.y = num3 + 6;
			}
		}
		this.focus = 0;
		this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
		this.cmdCheck = new Command(mResources.remember, this, 2001, null);
		this.cmdRes = new Command(mResources.register, this, 2002, null);
		this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
		this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
		this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
		if (GameCanvas.isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			if (GameCanvas.h >= 200)
			{
				this.cmdLogin.y = this.yLog + 110;
				this.cmdMenu.y = this.yLog + 110;
			}
			this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
			this.cmdBackFromRegister.y = this.yLog + 110;
			this.cmdRes.x = GameCanvas.w / 2 - 84;
			this.cmdRes.y = this.cmdMenu.y;
		}
		this.wP = 170;
		this.hP = ((!this.isRes) ? 100 : 110);
		this.xP = GameCanvas.hw - this.wP / 2;
		this.yP = this.tfUser.y - 15;
		int num4 = 4;
		int num5 = num4 * 32 + 23 + 33;
		if (num5 >= GameCanvas.w)
		{
			num4--;
			num5 = num4 * 32 + 23 + 33;
		}
		this.xLog = GameCanvas.w / 2 - num5 / 2;
		this.yLog = GameCanvas.hh - 30;
		this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
		this.tfUser.x = this.xLog + 10;
		this.tfUser.y = this.yLog + 20;
		this.cmdOK = new Command(mResources.OK, this, 2008, null);
		this.cmdOK.x = GameCanvas.w / 2 - 84;
		this.cmdOK.y = this.cmdLogin.y;
		this.cmdFogetPass = new Command(mResources.forgetPass, this, 1003, null);
		this.cmdFogetPass.x = GameCanvas.w / 2 + 3;
		this.cmdFogetPass.y = this.cmdLogin.y;
		this.center = this.cmdOK;
		this.left = this.cmdFogetPass;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0005D570 File Offset: 0x0005B770
	public static void getServerLink()
	{
		try
		{
			if (!LoginScr.isTryGetIPFromWap)
			{
				Command command = new Command();
				ActionChat actionChat = delegate(string str)
				{
					try
					{
						if (str == null)
						{
							return;
						}
						if (str == string.Empty)
						{
							return;
						}
						Rms.saveIP(str);
						if (!str.Contains(":"))
						{
							return;
						}
						int num = str.IndexOf(":");
						string text = str.Substring(0, num);
						string s = str.Substring(num + 1);
						GameMidlet.IP = text;
						GameMidlet.PORT = int.Parse(s);
						Session_ME.gI().connect(text, int.Parse(s));
						LoginScr.isTryGetIPFromWap = true;
					}
					catch (Exception ex)
					{
					}
				};
				command.actionChat = actionChat;
				Net.connectHTTP("http://27.0.14.75/game/ngocrong031_t.php", command);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		if (GameCanvas.isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		base.switchToMe();
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0005D638 File Offset: 0x0005B838
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		if (text != null && !text.Equals(string.Empty))
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		if (text2 != null && !text2.Equals(string.Empty))
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00003584 File Offset: 0x00001784
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0005D6A0 File Offset: 0x0005B8A0
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		if (!this.isLogin2)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		if (Main.isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0005D74C File Offset: 0x0005B94C
	protected void doRegister()
	{
		if (this.tfUser.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.userBlank);
			return;
		}
		char[] array = this.tfUser.getText().ToCharArray();
		if (this.tfPass.getText().Equals(string.Empty))
		{
			GameCanvas.startOKDlg(mResources.passwordBlank);
			return;
		}
		if (this.tfUser.getText().Length < 5)
		{
			GameCanvas.startOKDlg(mResources.accTooShort);
			return;
		}
		int num = 0;
		string text = null;
		if ((int)mResources.language == 2)
		{
			if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
			{
				text = mResources.emailInvalid;
			}
			num = 0;
		}
		else
		{
			try
			{
				long num2 = long.Parse(this.tfUser.getText());
				if (this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84")))
				{
					text = mResources.phoneInvalid;
				}
				num = 1;
			}
			catch (Exception ex)
			{
				if (this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1)
				{
					text = mResources.emailInvalid;
				}
				num = 0;
			}
		}
		if (text != null)
		{
			GameCanvas.startOKDlg(text);
		}
		else
		{
			GameCanvas.msgdlg.setInfo(string.Concat(new string[]
			{
				mResources.plsCheckAcc,
				(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
				this.tfUser.getText(),
				"\n",
				mResources.password,
				": ",
				this.tfPass.getText()
			}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0005D9BC File Offset: 0x0005BBBC
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0005DA78 File Offset: 0x0005BC78
	public void doViewFAQ()
	{
		if (!this.listFAQ.Equals(string.Empty) || !this.listFAQ.Equals(string.Empty))
		{
		}
		if (!Session_ME.connected)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0005DACC File Offset: 0x0005BCCC
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		if (LoginScr.isLocal)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		if (this.loadIndexServer() != -1 && !GameCanvas.isTouch)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00006841 File Offset: 0x00004A41
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0000684E File Offset: 0x00004A4E
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0005DB78 File Offset: 0x0005BD78
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		if (text != null && !text.Equals(string.Empty))
		{
			this.isLogin2 = false;
		}
		else if (Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty))
		{
			this.isLogin2 = true;
		}
		else
		{
			this.isLogin2 = false;
		}
		if ((text == null || text.Equals(string.Empty)) && this.isLogin2)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			text2 = "a";
		}
		if (text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty))
		{
			return;
		}
		if (text2.Equals(string.Empty))
		{
			this.focus = 1;
			this.tfUser.isFocus = false;
			this.tfPass.isFocus = true;
			if (!GameCanvas.isTouch)
			{
				this.right = this.tfPass.cmdClear;
			}
			return;
		}
		GameCanvas.connect();
		Res.outz(string.Concat(new object[]
		{
			"ccccccc ",
			text,
			" ",
			text2,
			" ",
			GameMidlet.VERSION,
			" ",
			(!this.isLogin2) ? 0 : 1
		}));
		Service.gI().login(text, text2, GameMidlet.VERSION, (!this.isLogin2) ? 0 : 1);
		if (Session_ME.connected)
		{
			GameCanvas.startWaitDlg();
		}
		else
		{
			GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
		}
		this.focus = 0;
		if (!this.isLogin2)
		{
			this.actRegisterLeft();
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0005DD84 File Offset: 0x0005BF84
	public void savePass()
	{
		if (this.isCheck)
		{
			Rms.saveRMSInt("check", 1);
			Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
		}
		else
		{
			Rms.saveRMSInt("check", 2);
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0005DE14 File Offset: 0x0005C014
	public override void update()
	{
		if (Main.isWindowsPhone && this.isRegistering)
		{
			if (this.t < 0)
			{
				GameCanvas.endDlg();
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				this.isRegistering = false;
			}
			else
			{
				this.t--;
			}
		}
		if (LoginScr.timeLogin > 0)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			if (LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L)
			{
				LoginScr.timeLogin -= 1;
				if (LoginScr.timeLogin == 0)
				{
					Session_ME.gI().close();
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (TouchScreenKeyboard.visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			effect.update();
		}
		if (LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		GameCanvas.debug("LGU1", 0);
		GameCanvas.debug("LGU2", 0);
		GameCanvas.debug("LGU3", 0);
		this.updateLogo();
		GameCanvas.debug("LGU4", 0);
		GameCanvas.debug("LGU5", 0);
		if (this.g >= 0)
		{
			this.ylogo += this.dir * this.g;
			this.g += this.dir * this.v;
			if (this.g <= 0)
			{
				this.dir *= -1;
			}
			if (this.ylogo > 0)
			{
				this.dir *= -1;
				this.g -= 2 * this.v;
			}
		}
		GameCanvas.debug("LGU6", 0);
		if (this.tipid >= 0 && GameCanvas.gameTick % 100 == 0)
		{
			this.doChangeTip();
		}
		if (this.isLogin2 && !this.isRes)
		{
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = (((int)mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (!Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone)
		{
			string text = this.tfUser.getText().ToLower().Trim();
			string text2 = this.tfPass.getText().ToLower().Trim();
			if (!text.Equals(string.Empty) && !text2.Equals(string.Empty))
			{
				this.doLogin();
			}
			Main.isMiniApp = true;
		}
		this.updateTfWhenOpenKb();
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0005E338 File Offset: 0x0005C538
	private void doChangeTip()
	{
		this.tipid++;
		if (this.tipid >= mResources.tips.Length)
		{
			this.tipid = 0;
		}
		if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0000685A File Offset: 0x00004A5A
	public void updateLogo()
	{
		if (this.defYL != this.yL)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0005E3A0 File Offset: 0x0005C5A0
	public override void keyPress(int keyCode)
	{
		if (this.tfUser.isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else if (this.tfPass.isFocus)
		{
			this.tfPass.keyPressed(keyCode);
		}
		base.keyPress(keyCode);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00006889 File Offset: 0x00004A89
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0005E3F4 File Offset: 0x0005C5F4
	public override void paint(mGraphics g)
	{
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		if (GameCanvas.h <= 220)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		if (mSystem.clientType == 1 && !GameCanvas.isTouch)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		if (ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (GameCanvas.currentDialog == null)
		{
			int h = 105;
			int w = (GameCanvas.w < 200) ? 160 : 180;
			PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w, h, -1, true);
			if (GameCanvas.h > 160 && LoginScr.imgTitle != null)
			{
				g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
			}
			GameCanvas.debug("PLG4", 1);
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.tfPass.x = this.xLog + 10;
			this.tfPass.y = this.yLog + 55;
			this.tfUser.paint(g);
			this.tfPass.paint(g);
			if (GameCanvas.w < 176)
			{
				mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
				mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
			}
		}
		base.paint(g);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0005E6C0 File Offset: 0x0005C8C0
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
		}
		else if (mSystem.clientType == 1 && GameCanvas.keyPressed[13])
		{
			GameCanvas.keyPressed[13] = false;
			this.cmdCallHotline.performAction();
		}
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		if (!GameCanvas.isTouch)
		{
			if (this.tfUser.isFocus)
			{
				this.right = this.tfUser.cmdClear;
			}
			else
			{
				this.right = this.tfPass.cmdClear;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			this.focus--;
			if (this.focus < 0)
			{
				this.focus = 1;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			this.focus++;
			if (this.focus > 1)
			{
				this.focus = 0;
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16])
		{
			GameCanvas.clearKeyPressed();
			if (!this.isLogin2 || this.isRes)
			{
				if (this.focus == 1)
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = true;
				}
				else if (this.focus == 0)
				{
					this.tfUser.isFocus = true;
					this.tfPass.isFocus = false;
				}
				else
				{
					this.tfUser.isFocus = false;
					this.tfPass.isFocus = false;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			if (this.isRes)
			{
				this.center = this.cmdRes;
				this.left = this.cmdBackFromRegister;
			}
			else
			{
				this.center = this.cmdOK;
				this.left = this.cmdFogetPass;
			}
		}
		else if (this.isRes)
		{
			this.center = this.cmdRes;
			this.left = this.cmdBackFromRegister;
		}
		else
		{
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (!this.isLogin2 || this.isRes)
			{
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
				}
				else if (GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height))
				{
					this.focus = 1;
				}
			}
		}
		if (Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null)
		{
			this.right.performAction();
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00006891 File Offset: 0x00004A91
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0005EA54 File Offset: 0x0005CC54
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 2000:
			break;
		case 2001:
			if (this.isCheck)
			{
				this.isCheck = false;
			}
			else
			{
				this.isCheck = true;
			}
			break;
		case 2002:
			this.doRegister();
			break;
		case 2003:
			this.doMenu();
			break;
		case 2004:
			this.actRegister();
			break;
		default:
			switch (idAction)
			{
			case 1000:
				try
				{
					GameMidlet.instance.platformRequest((string)p);
				}
				catch (Exception ex)
				{
				}
				GameCanvas.endDlg();
				break;
			case 1001:
				GameCanvas.endDlg();
				this.isRes = false;
				break;
			case 1002:
			{
				GameCanvas.startWaitDlg();
				string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
				if (text == null || text.Equals(string.Empty))
				{
					Service.gI().login2(string.Empty);
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				break;
			}
			case 1003:
				GameCanvas.startOKDlg(mResources.goToWebForPassword);
				break;
			case 1004:
				ServerListScreen.doUpdateServer();
				GameCanvas.serverScreen.switchToMe();
				break;
			case 1005:
				try
				{
					GameMidlet.instance.platformRequest("http://ngocrongonline.com");
				}
				catch (Exception ex2)
				{
				}
				break;
			default:
				if (idAction != 10041)
				{
					if (idAction != 10042)
					{
						if (idAction != 13)
						{
							if (idAction != 4000)
							{
								if (idAction == 10021)
								{
									this.actRegisterLeft();
								}
							}
							else
							{
								this.doRegister(this.tfUser.getText());
							}
						}
						else
						{
							switch (mSystem.clientType)
							{
							case 1:
								mSystem.callHotlineJava();
								break;
							case 3:
							case 5:
								mSystem.callHotlineIphone();
								break;
							case 4:
								mSystem.callHotlinePC();
								break;
							case 6:
								mSystem.callHotlineWindowsPhone();
								break;
							}
						}
					}
					else
					{
						Rms.saveRMSInt("lowGraphic", 1);
						GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					}
				}
				else
				{
					Rms.saveRMSInt("lowGraphic", 0);
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				}
				break;
			}
			break;
		case 2008:
			Rms.saveRMSString("acc", this.tfUser.getText().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().Trim());
			if (ServerListScreen.loadScreen)
			{
				GameCanvas.serverScreen.switchToMe();
			}
			else
			{
				GameCanvas.serverScreen.show2();
			}
			break;
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0000689B File Offset: 0x00004A9B
	public void actRegisterLeft()
	{
		if (this.isLogin2)
		{
			this.doLogin();
			return;
		}
		this.isRes = false;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
		this.left = this.cmdMenu;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x000068DA File Offset: 0x00004ADA
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0005ED64 File Offset: 0x0005CF64
	public void backToRegister()
	{
		if (GameCanvas.loginScr.isLogin2)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
			return;
		}
		if (Main.isWindowsPhone)
		{
			GameMidlet.isBackWindowsPhone = true;
		}
		GameCanvas.instance.resetToLoginScr = false;
		GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
		Session_ME.gI().close();
	}

	// Token: 0x04000D24 RID: 3364
	public TField tfUser;

	// Token: 0x04000D25 RID: 3365
	public TField tfPass;

	// Token: 0x04000D26 RID: 3366
	public static bool isContinueToLogin = false;

	// Token: 0x04000D27 RID: 3367
	private int focus;

	// Token: 0x04000D28 RID: 3368
	private int wC;

	// Token: 0x04000D29 RID: 3369
	private int yL;

	// Token: 0x04000D2A RID: 3370
	private int defYL;

	// Token: 0x04000D2B RID: 3371
	public bool isCheck;

	// Token: 0x04000D2C RID: 3372
	public bool isRes;

	// Token: 0x04000D2D RID: 3373
	public Command cmdLogin;

	// Token: 0x04000D2E RID: 3374
	public Command cmdCheck;

	// Token: 0x04000D2F RID: 3375
	public Command cmdFogetPass;

	// Token: 0x04000D30 RID: 3376
	public Command cmdRes;

	// Token: 0x04000D31 RID: 3377
	public Command cmdMenu;

	// Token: 0x04000D32 RID: 3378
	public Command cmdBackFromRegister;

	// Token: 0x04000D33 RID: 3379
	public string listFAQ = string.Empty;

	// Token: 0x04000D34 RID: 3380
	public string titleFAQ;

	// Token: 0x04000D35 RID: 3381
	public string subtitleFAQ;

	// Token: 0x04000D36 RID: 3382
	private string numSupport = string.Empty;

	// Token: 0x04000D37 RID: 3383
	public static bool isLocal = false;

	// Token: 0x04000D38 RID: 3384
	public static bool isUpdateAll;

	// Token: 0x04000D39 RID: 3385
	public static bool isUpdateData;

	// Token: 0x04000D3A RID: 3386
	public static bool isUpdateMap;

	// Token: 0x04000D3B RID: 3387
	public static bool isUpdateSkill;

	// Token: 0x04000D3C RID: 3388
	public static bool isUpdateItem;

	// Token: 0x04000D3D RID: 3389
	public static string serverName;

	// Token: 0x04000D3E RID: 3390
	public static Image imgTitle;

	// Token: 0x04000D3F RID: 3391
	public int plX;

	// Token: 0x04000D40 RID: 3392
	public int plY;

	// Token: 0x04000D41 RID: 3393
	public int lY;

	// Token: 0x04000D42 RID: 3394
	public int lX;

	// Token: 0x04000D43 RID: 3395
	public int logoDes;

	// Token: 0x04000D44 RID: 3396
	public int lineX;

	// Token: 0x04000D45 RID: 3397
	public int lineY;

	// Token: 0x04000D46 RID: 3398
	public static int[] bgId = new int[]
	{
		0,
		8,
		2,
		6,
		9
	};

	// Token: 0x04000D47 RID: 3399
	public static bool isTryGetIPFromWap;

	// Token: 0x04000D48 RID: 3400
	public static short timeLogin;

	// Token: 0x04000D49 RID: 3401
	public static long lastTimeLogin;

	// Token: 0x04000D4A RID: 3402
	public static long currTimeLogin;

	// Token: 0x04000D4B RID: 3403
	private int yt;

	// Token: 0x04000D4C RID: 3404
	private Command cmdSelect;

	// Token: 0x04000D4D RID: 3405
	private Command cmdOK;

	// Token: 0x04000D4E RID: 3406
	private int xLog;

	// Token: 0x04000D4F RID: 3407
	private int yLog;

	// Token: 0x04000D50 RID: 3408
	public static GameMidlet m;

	// Token: 0x04000D51 RID: 3409
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000D52 RID: 3410
	private int freeAreaHeight;

	// Token: 0x04000D53 RID: 3411
	private int xP;

	// Token: 0x04000D54 RID: 3412
	private int yP;

	// Token: 0x04000D55 RID: 3413
	private int wP;

	// Token: 0x04000D56 RID: 3414
	private int hP;

	// Token: 0x04000D57 RID: 3415
	private int t = 20;

	// Token: 0x04000D58 RID: 3416
	private bool isRegistering;

	// Token: 0x04000D59 RID: 3417
	private string passRe = string.Empty;

	// Token: 0x04000D5A RID: 3418
	public bool isFAQ;

	// Token: 0x04000D5B RID: 3419
	private int tipid = -1;

	// Token: 0x04000D5C RID: 3420
	public bool isLogin2;

	// Token: 0x04000D5D RID: 3421
	private int v = 2;

	// Token: 0x04000D5E RID: 3422
	private int g;

	// Token: 0x04000D5F RID: 3423
	private int ylogo = -40;

	// Token: 0x04000D60 RID: 3424
	private int dir = 1;

	// Token: 0x04000D61 RID: 3425
	private Command cmdCallHotline;

	// Token: 0x04000D62 RID: 3426
	public static bool isLoggingIn;
}
