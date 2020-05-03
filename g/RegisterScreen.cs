using System;

namespace Assets.src.g
{
	// Token: 0x020000AC RID: 172
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00079940 File Offset: 0x00077B40
		public RegisterScreen(sbyte haveName)
		{
			this.yLog = 130;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			if (TileMap.bgID == 5 || TileMap.bgID == 6)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
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
			this.tfSodt = new TField();
			this.tfSodt.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfSodt.width = 220;
			this.tfSodt.height = mScreen.ITEM_HEIGHT + 2;
			this.tfSodt.name = "Số điện thoại/ địa chỉ email";
			if ((int)haveName == 1)
			{
				this.tfSodt.setText("01234567890");
			}
			this.tfUser = new TField();
			this.tfUser.width = 220;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.name = "Họ và tên";
			if ((int)haveName == 1)
			{
				this.tfUser.setText("Nguyễn Văn A");
			}
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgay = new TField();
			this.tfNgay.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgay.width = 70;
			this.tfNgay.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgay.name = "Ngày sinh";
			if ((int)haveName == 1)
			{
				this.tfNgay.setText("01");
			}
			this.tfThang = new TField();
			this.tfThang.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfThang.width = 70;
			this.tfThang.height = mScreen.ITEM_HEIGHT + 2;
			this.tfThang.name = "Tháng sinh";
			if ((int)haveName == 1)
			{
				this.tfThang.setText("01");
			}
			this.tfNam = new TField();
			this.tfNam.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNam.width = 70;
			this.tfNam.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNam.name = "Năm sinh";
			if ((int)haveName == 1)
			{
				this.tfNam.setText("1990");
			}
			this.tfDiachi = new TField();
			this.tfDiachi.setIputType(TField.INPUT_TYPE_ANY);
			this.tfDiachi.width = 220;
			this.tfDiachi.height = mScreen.ITEM_HEIGHT + 2;
			this.tfDiachi.name = "Địa chỉ đăng ký thường trú";
			if ((int)haveName == 1)
			{
				this.tfDiachi.setText("123 đường số 1, Quận 1, TP.HCM");
			}
			this.tfCMND = new TField();
			this.tfCMND.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfCMND.width = 220;
			this.tfCMND.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCMND.name = "Số Chứng minh nhân dân hoặc số hộ chiếu";
			if ((int)haveName == 1)
			{
				this.tfCMND.setText("123456789");
			}
			this.tfNgayCap = new TField();
			this.tfNgayCap.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgayCap.width = 220;
			this.tfNgayCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgayCap.name = "Ngày cấp";
			if ((int)haveName == 1)
			{
				this.tfNgayCap.setText("01/01/2005");
			}
			this.tfNoiCap = new TField();
			this.tfNoiCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNoiCap.width = 220;
			this.tfNoiCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNoiCap.name = "Nơi cấp";
			if ((int)haveName == 1)
			{
				this.tfNoiCap.setText("TP.HCM");
			}
			this.yt += 35;
			this.isCheck = true;
			this.focus = 0;
			this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
			this.cmdCheck = new Command(mResources.remember, this, 2001, null);
			this.cmdRes = new Command(mResources.register, this, 2002, null);
			this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
			this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
			if (GameCanvas.isTouch)
			{
				this.cmdLogin.x = GameCanvas.w / 2 - 100;
				this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
				if (GameCanvas.h >= 200)
				{
					this.cmdLogin.y = GameCanvas.h / 2 - 40;
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
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			if (num3 >= GameCanvas.w)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.yLog = 5;
			this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null);
			this.cmdOK.x = 260;
			this.cmdOK.y = GameCanvas.h - 60;
			this.cmdFogetPass = new Command("Thoát", this, 1003, null);
			this.cmdFogetPass.x = 260;
			this.cmdFogetPass.y = GameCanvas.h - 30;
			if (GameCanvas.w < 250)
			{
				this.cmdOK.x = GameCanvas.w / 2 - 80;
				this.cmdFogetPass.x = GameCanvas.w / 2 + 10;
				this.cmdFogetPass.y = (this.cmdOK.y = GameCanvas.h - 25);
			}
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0007A140 File Offset: 0x00078340
		public new void switchToMe()
		{
			Res.outz("Res switch");
			SoundMn.gI().stopAll();
			this.focus = 0;
			this.tfUser.isFocus = true;
			this.tfNgay.isFocus = false;
			if (GameCanvas.isTouch)
			{
				this.tfUser.isFocus = false;
				this.focus = -1;
			}
			base.switchToMe();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0007A1A4 File Offset: 0x000783A4
		protected void doMenu()
		{
			MyVector myVector = new MyVector("vMenu Login");
			myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
			if (!this.isLogin2)
			{
				myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
			}
			myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
			myVector.addElement(new Command(mResources.website, this, 1005, null));
			int num = Rms.loadRMSInt("lowGraphic");
			if (num == 1)
			{
				myVector.addElement(new Command(mResources.increase_vga, this, 10041, null));
			}
			else
			{
				myVector.addElement(new Command(mResources.decrease_vga, this, 10042, null));
			}
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0007A290 File Offset: 0x00078490
		protected void doRegister()
		{
			if (this.tfUser.getText().Equals(string.Empty))
			{
				GameCanvas.startOKDlg(mResources.userBlank);
				return;
			}
			char[] array = this.tfUser.getText().ToCharArray();
			if (this.tfNgay.getText().Equals(string.Empty))
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
					this.tfNgay.getText()
				}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
			}
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00003584 File Offset: 0x00001784
		protected void doRegister(string user)
		{
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0007A500 File Offset: 0x00078700
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

		// Token: 0x0600084B RID: 2123 RVA: 0x0007A554 File Offset: 0x00078754
		protected void doSelectServer()
		{
			MyVector myVector = new MyVector("vServer");
			if (RegisterScreen.isLocal)
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

		// Token: 0x0600084C RID: 2124 RVA: 0x00006841 File Offset: 0x00004A41
		protected void saveIndexServer(int index)
		{
			Rms.saveRMSInt("indServer", index);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0000684E File Offset: 0x00004A4E
		protected int loadIndexServer()
		{
			return Rms.loadRMSInt("indServer");
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00003584 File Offset: 0x00001784
		public void doLogin()
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00003584 File Offset: 0x00001784
		public void savePass()
		{
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0007A604 File Offset: 0x00078804
		public override void update()
		{
			this.tfUser.update();
			this.tfNgay.update();
			this.tfThang.update();
			this.tfNam.update();
			this.tfDiachi.update();
			this.tfCMND.update();
			this.tfNoiCap.update();
			this.tfSodt.update();
			this.tfNgayCap.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				effect.update();
			}
			if (RegisterScreen.isUpdateAll && !RegisterScreen.isUpdateData && !RegisterScreen.isUpdateItem && !RegisterScreen.isUpdateMap && !RegisterScreen.isUpdateSkill)
			{
				RegisterScreen.isUpdateAll = false;
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
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0007A8A0 File Offset: 0x00078AA0
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

		// Token: 0x06000852 RID: 2130 RVA: 0x00006E84 File Offset: 0x00005084
		public void updateLogo()
		{
			if (this.defYL != this.yL)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0007A908 File Offset: 0x00078B08
		public override void keyPress(int keyCode)
		{
			if (this.tfUser.isFocus)
			{
				this.tfUser.keyPressed(keyCode);
			}
			else if (this.tfNgay.isFocus)
			{
				this.tfNgay.keyPressed(keyCode);
			}
			else if (this.tfThang.isFocus)
			{
				this.tfThang.keyPressed(keyCode);
			}
			else if (this.tfNam.isFocus)
			{
				this.tfNam.keyPressed(keyCode);
			}
			else if (this.tfDiachi.isFocus)
			{
				this.tfDiachi.keyPressed(keyCode);
			}
			else if (this.tfCMND.isFocus)
			{
				this.tfCMND.keyPressed(keyCode);
			}
			else if (this.tfNoiCap.isFocus)
			{
				this.tfNoiCap.keyPressed(keyCode);
			}
			else if (this.tfSodt.isFocus)
			{
				this.tfSodt.keyPressed(keyCode);
			}
			else if (this.tfNgayCap.isFocus)
			{
				this.tfNgayCap.keyPressed(keyCode);
			}
			base.keyPress(keyCode);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00006889 File Offset: 0x00004A89
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0007AA4C File Offset: 0x00078C4C
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
				this.xLog = 5;
				int num2 = 233;
				if (GameCanvas.w < 260)
				{
					this.xLog = (GameCanvas.w - 240) / 2;
				}
				this.yLog = (GameCanvas.h - num2) / 2;
				int num3 = (GameCanvas.w < 200) ? 160 : 180;
				PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num2, -1, true);
				if (GameCanvas.h > 160 && RegisterScreen.imgTitle != null)
				{
					g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, num, 3);
				}
				GameCanvas.debug("PLG4", 1);
				int num4 = 4;
				int num5 = num4 * 32 + 23 + 33;
				if (num5 >= GameCanvas.w)
				{
					num4--;
					num5 = num4 * 32 + 23 + 33;
				}
				this.tfSodt.x = this.xLog + 10;
				this.tfSodt.y = this.yLog + 15;
				this.tfUser.x = this.tfSodt.x;
				this.tfUser.y = this.tfSodt.y + 30;
				this.tfNgay.x = this.xLog + 10;
				this.tfNgay.y = this.tfUser.y + 30;
				this.tfThang.x = this.tfNgay.x + 75;
				this.tfThang.y = this.tfNgay.y;
				this.tfNam.x = this.tfThang.x + 75;
				this.tfNam.y = this.tfThang.y;
				this.tfDiachi.x = this.tfUser.x;
				this.tfDiachi.y = this.tfNgay.y + 30;
				this.tfCMND.x = this.tfUser.x;
				this.tfCMND.y = this.tfDiachi.y + 30;
				this.tfNgayCap.x = this.tfUser.x;
				this.tfNgayCap.y = this.tfCMND.y + 30;
				this.tfNoiCap.x = this.tfUser.x;
				this.tfNoiCap.y = this.tfNgayCap.y + 30;
				this.tfUser.paint(g);
				this.tfNgay.paint(g);
				this.tfThang.paint(g);
				this.tfNam.paint(g);
				this.tfDiachi.paint(g);
				this.tfCMND.paint(g);
				this.tfNgayCap.paint(g);
				this.tfNoiCap.paint(g);
				this.tfSodt.paint(g);
				if (GameCanvas.w < 176)
				{
					mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfNgay.x - 35, this.tfNgay.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.server + ": " + RegisterScreen.serverName, GameCanvas.w / 2, this.tfNgay.y + 32, 2);
					if (this.isRes)
					{
					}
				}
			}
			string version = GameMidlet.VERSION;
			g.setColor(GameCanvas.skyColor);
			g.fillRect(GameCanvas.w - 40, 4, 36, 11);
			mFont.tahoma_7_grey.drawString(g, version, GameCanvas.w - 22, 4, mFont.CENTER);
			GameCanvas.resetTrans(g);
			if (GameCanvas.currentDialog == null)
			{
				if (GameCanvas.w > 250)
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi", 260, 10, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "chỉ có thể chơi", 260, 25, 0, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", 260, 40, 0, mFont.tahoma_7b_dark);
				}
				else
				{
					mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi chỉ có thể chơi", GameCanvas.w / 2, 5, 2, mFont.tahoma_7b_dark);
					mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", GameCanvas.w / 2, 15, 2, mFont.tahoma_7b_dark);
				}
			}
			base.paint(g);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0007AF64 File Offset: 0x00079164
		private void turnOffFocus()
		{
			this.tfUser.isFocus = false;
			this.tfNgay.isFocus = false;
			this.tfThang.isFocus = false;
			this.tfNam.isFocus = false;
			this.tfDiachi.isFocus = false;
			this.tfCMND.isFocus = false;
			this.tfNgayCap.isFocus = false;
			this.tfNoiCap.isFocus = false;
			this.tfSodt.isFocus = false;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0007AFE0 File Offset: 0x000791E0
		private void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUser.isFocus = true;
				break;
			case 1:
				this.tfNgay.isFocus = true;
				break;
			case 2:
				this.tfThang.isFocus = true;
				break;
			case 3:
				this.tfNam.isFocus = true;
				break;
			case 4:
				this.tfDiachi.isFocus = true;
				break;
			case 5:
				this.tfCMND.isFocus = true;
				break;
			case 6:
				this.tfNgayCap.isFocus = true;
				break;
			case 7:
				this.tfNoiCap.isFocus = true;
				break;
			case 8:
				this.tfSodt.isFocus = true;
				break;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0007B0C4 File Offset: 0x000792C4
		public override void updateKey()
		{
			if (RegisterScreen.isContinueToLogin)
			{
				return;
			}
			if (!GameCanvas.isTouch)
			{
				if (this.tfUser.isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else if (this.tfNgay.isFocus)
				{
					this.right = this.tfNgay.cmdClear;
				}
				else if (this.tfThang.isFocus)
				{
					this.right = this.tfThang.cmdClear;
				}
				else if (this.tfNam.isFocus)
				{
					this.right = this.tfNam.cmdClear;
				}
				else if (this.tfDiachi.isFocus)
				{
					this.right = this.tfDiachi.cmdClear;
				}
				else if (this.tfCMND.isFocus)
				{
					this.right = this.tfCMND.cmdClear;
				}
				else if (this.tfNgayCap.isFocus)
				{
					this.right = this.tfNgayCap.cmdClear;
				}
				else if (this.tfNoiCap.isFocus)
				{
					this.right = this.tfNoiCap.cmdClear;
				}
				else if (this.tfSodt.isFocus)
				{
					this.right = this.tfSodt.cmdClear;
				}
			}
			if (GameCanvas.keyPressed[21])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = 8;
				}
				this.processFocus();
			}
			else if (GameCanvas.keyPressed[22])
			{
				this.focus++;
				if (this.focus > 8)
				{
					this.focus = 0;
				}
				this.processFocus();
			}
			if (GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22])
			{
				GameCanvas.clearKeyPressed();
				if (!this.isLogin2 || this.isRes)
				{
					if (this.focus == 1)
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = true;
					}
					else if (this.focus == 0)
					{
						this.tfUser.isFocus = true;
						this.tfNgay.isFocus = false;
					}
					else
					{
						this.tfUser.isFocus = false;
						this.tfNgay.isFocus = false;
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
				if (GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height))
				{
					this.focus = 0;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgay.x, this.tfNgay.y, this.tfNgay.width, this.tfNgay.height))
				{
					this.focus = 1;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfThang.x, this.tfThang.y, this.tfThang.width, this.tfThang.height))
				{
					this.focus = 2;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNam.x, this.tfNam.y, this.tfNam.width, this.tfNam.height))
				{
					this.focus = 3;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfDiachi.x, this.tfDiachi.y, this.tfDiachi.width, this.tfDiachi.height))
				{
					this.focus = 4;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfCMND.x, this.tfCMND.y, this.tfCMND.width, this.tfCMND.height))
				{
					this.focus = 5;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNgayCap.x, this.tfNgayCap.y, this.tfNgayCap.width, this.tfNgayCap.height))
				{
					this.focus = 6;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfNoiCap.x, this.tfNoiCap.y, this.tfNoiCap.width, this.tfNoiCap.height))
				{
					this.focus = 7;
					this.processFocus();
				}
				else if (GameCanvas.isPointerHoldIn(this.tfSodt.x, this.tfSodt.y, this.tfSodt.width, this.tfSodt.height))
				{
					this.focus = 8;
					this.processFocus();
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00006EB3 File Offset: 0x000050B3
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0007B670 File Offset: 0x00079870
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
						ex.StackTrace.ToString();
					}
					GameCanvas.endDlg();
					break;
				case 1001:
					GameCanvas.endDlg();
					this.isRes = false;
					break;
				case 1002:
					break;
				case 1003:
					Session_ME.gI().close();
					GameCanvas.serverScreen.switchToMe();
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
						ex2.StackTrace.ToString();
					}
					break;
				default:
					if (idAction != 10041)
					{
						if (idAction != 10042)
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
					}
					break;
				}
				break;
			case 2008:
				if (this.tfNgay.getText().Equals(string.Empty) || this.tfThang.getText().Equals(string.Empty) || this.tfNam.getText().Equals(string.Empty) || this.tfDiachi.getText().Equals(string.Empty) || this.tfCMND.getText().Equals(string.Empty) || this.tfNgayCap.getText().Equals(string.Empty) || this.tfNoiCap.getText().Equals(string.Empty) || this.tfSodt.getText().Equals(string.Empty) || this.tfUser.getText().Equals(string.Empty))
				{
					GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
				}
				else
				{
					GameCanvas.startOKDlg(mResources.PLEASEWAIT);
					Service.gI().charInfo(this.tfNgay.getText(), this.tfThang.getText(), this.tfNam.getText(), this.tfDiachi.getText(), this.tfCMND.getText(), this.tfNgayCap.getText(), this.tfNoiCap.getText(), this.tfSodt.getText(), this.tfUser.getText());
				}
				break;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00006EBD File Offset: 0x000050BD
		public void actRegisterLeft()
		{
			if (this.isLogin2)
			{
				this.doLogin();
				return;
			}
			this.isRes = false;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00006EFC File Offset: 0x000050FC
		public void actRegister()
		{
			GameCanvas.endDlg();
			GameCanvas.startOKDlg(mResources.regNote);
			this.isRes = true;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0007B9A8 File Offset: 0x00079BA8
		public void backToRegister()
		{
			if (GameCanvas.loginScr.isLogin2)
			{
				GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
				return;
			}
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
			Session_ME.gI().close();
		}

		// Token: 0x04000F2F RID: 3887
		public TField tfUser;

		// Token: 0x04000F30 RID: 3888
		public TField tfNgay;

		// Token: 0x04000F31 RID: 3889
		public TField tfThang;

		// Token: 0x04000F32 RID: 3890
		public TField tfNam;

		// Token: 0x04000F33 RID: 3891
		public TField tfDiachi;

		// Token: 0x04000F34 RID: 3892
		public TField tfCMND;

		// Token: 0x04000F35 RID: 3893
		public TField tfNgayCap;

		// Token: 0x04000F36 RID: 3894
		public TField tfNoiCap;

		// Token: 0x04000F37 RID: 3895
		public TField tfSodt;

		// Token: 0x04000F38 RID: 3896
		public static bool isContinueToLogin = false;

		// Token: 0x04000F39 RID: 3897
		private int focus;

		// Token: 0x04000F3A RID: 3898
		private int wC;

		// Token: 0x04000F3B RID: 3899
		private int yL;

		// Token: 0x04000F3C RID: 3900
		private int defYL;

		// Token: 0x04000F3D RID: 3901
		public bool isCheck;

		// Token: 0x04000F3E RID: 3902
		public bool isRes;

		// Token: 0x04000F3F RID: 3903
		private Command cmdLogin;

		// Token: 0x04000F40 RID: 3904
		private Command cmdCheck;

		// Token: 0x04000F41 RID: 3905
		private Command cmdFogetPass;

		// Token: 0x04000F42 RID: 3906
		private Command cmdRes;

		// Token: 0x04000F43 RID: 3907
		private Command cmdMenu;

		// Token: 0x04000F44 RID: 3908
		private Command cmdBackFromRegister;

		// Token: 0x04000F45 RID: 3909
		public string listFAQ = string.Empty;

		// Token: 0x04000F46 RID: 3910
		public string titleFAQ;

		// Token: 0x04000F47 RID: 3911
		public string subtitleFAQ;

		// Token: 0x04000F48 RID: 3912
		private string numSupport = string.Empty;

		// Token: 0x04000F49 RID: 3913
		private string strUser;

		// Token: 0x04000F4A RID: 3914
		private string strPass;

		// Token: 0x04000F4B RID: 3915
		public static bool isLocal = false;

		// Token: 0x04000F4C RID: 3916
		public static bool isUpdateAll;

		// Token: 0x04000F4D RID: 3917
		public static bool isUpdateData;

		// Token: 0x04000F4E RID: 3918
		public static bool isUpdateMap;

		// Token: 0x04000F4F RID: 3919
		public static bool isUpdateSkill;

		// Token: 0x04000F50 RID: 3920
		public static bool isUpdateItem;

		// Token: 0x04000F51 RID: 3921
		public static string serverName;

		// Token: 0x04000F52 RID: 3922
		public static Image imgTitle;

		// Token: 0x04000F53 RID: 3923
		public int plX;

		// Token: 0x04000F54 RID: 3924
		public int plY;

		// Token: 0x04000F55 RID: 3925
		public int lY;

		// Token: 0x04000F56 RID: 3926
		public int lX;

		// Token: 0x04000F57 RID: 3927
		public int logoDes;

		// Token: 0x04000F58 RID: 3928
		public int lineX;

		// Token: 0x04000F59 RID: 3929
		public int lineY;

		// Token: 0x04000F5A RID: 3930
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04000F5B RID: 3931
		public static bool isTryGetIPFromWap;

		// Token: 0x04000F5C RID: 3932
		public static short timeLogin;

		// Token: 0x04000F5D RID: 3933
		public static long lastTimeLogin;

		// Token: 0x04000F5E RID: 3934
		public static long currTimeLogin;

		// Token: 0x04000F5F RID: 3935
		private int yt;

		// Token: 0x04000F60 RID: 3936
		private Command cmdSelect;

		// Token: 0x04000F61 RID: 3937
		private Command cmdOK;

		// Token: 0x04000F62 RID: 3938
		private int xLog;

		// Token: 0x04000F63 RID: 3939
		private int yLog;

		// Token: 0x04000F64 RID: 3940
		private int xP;

		// Token: 0x04000F65 RID: 3941
		private int yP;

		// Token: 0x04000F66 RID: 3942
		private int wP;

		// Token: 0x04000F67 RID: 3943
		private int hP;

		// Token: 0x04000F68 RID: 3944
		private string passRe = string.Empty;

		// Token: 0x04000F69 RID: 3945
		public bool isFAQ;

		// Token: 0x04000F6A RID: 3946
		private int tipid = -1;

		// Token: 0x04000F6B RID: 3947
		public bool isLogin2;

		// Token: 0x04000F6C RID: 3948
		private int v = 2;

		// Token: 0x04000F6D RID: 3949
		private int g;

		// Token: 0x04000F6E RID: 3950
		private int ylogo = -40;

		// Token: 0x04000F6F RID: 3951
		private int dir = 1;

		// Token: 0x04000F70 RID: 3952
		public static bool isLoggingIn;
	}
}
