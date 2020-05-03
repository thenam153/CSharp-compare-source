using System;

// Token: 0x020000B1 RID: 177
public class SplashScr : mScreen
{
	// Token: 0x0600089D RID: 2205 RVA: 0x00007132 File Offset: 0x00005332
	public SplashScr()
	{
		SplashScr.instance = this;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0000714E File Offset: 0x0000534E
	public static void loadSplashScr()
	{
		SplashScr.splashScrStat = 0;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0007DB4C File Offset: 0x0007BD4C
	public override void update()
	{
		if (SplashScr.splashScrStat == 30 && !this.isCheckConnect)
		{
			this.isCheckConnect = true;
			if (Rms.loadRMSInt("isPlaySound") != -1)
			{
				GameCanvas.isPlaySound = (Rms.loadRMSInt("isPlaySound") == 1);
			}
			if (GameCanvas.isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
			}
			SoundMn.gI().getStrOption();
			if (Rms.loadRMSInt("svselect") == -1)
			{
				ServerListScreen.loadIP();
			}
			else
			{
				ServerListScreen.loadIP();
			}
		}
		SplashScr.splashScrStat++;
		ServerListScreen.updateDeleteData();
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0007DBF8 File Offset: 0x0007BDF8
	public static void loadIP()
	{
		if (Rms.loadRMSInt("svselect") == -1)
		{
			int num = 0;
			if ((int)mResources.language > 0)
			{
				for (int i = 0; i < (int)mResources.language; i++)
				{
					num += ServerListScreen.lengthServer[i];
				}
			}
			if ((int)ServerListScreen.serverPriority == -1)
			{
				ServerListScreen.ipSelect = num + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
			}
			else
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
			}
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
		else
		{
			ServerListScreen.ipSelect = Rms.loadRMSInt("svselect");
			string text = Rms.loadRMSString("acc");
			string text2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			if ((text == null || text.Equals(string.Empty)) && (text2 == null || text2.Equals(string.Empty)))
			{
				int num2 = 0;
				if ((int)mResources.language > 0)
				{
					for (int j = 0; j < (int)mResources.language; j++)
					{
						num2 += ServerListScreen.lengthServer[j];
					}
				}
				if ((int)ServerListScreen.serverPriority == -1)
				{
					ServerListScreen.ipSelect = num2 + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
				}
				else
				{
					ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
				}
			}
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0007DDE4 File Offset: 0x0007BFE4
	public override void paint(mGraphics g)
	{
		if (SplashScr.imgLogo != null && SplashScr.splashScrStat < 30)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(SplashScr.imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		if (SplashScr.nData != -1)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + SplashScr.nData * 100 / SplashScr.maxData + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}
		else if (SplashScr.splashScrStat >= 30)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
			if (ServerListScreen.cmdDeleteRMS != null)
			{
				mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
		}
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00007156 File Offset: 0x00005356
	public static void loadImg()
	{
		SplashScr.imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}

	// Token: 0x04000FAF RID: 4015
	public static int splashScrStat;

	// Token: 0x04000FB0 RID: 4016
	private bool isCheckConnect;

	// Token: 0x04000FB1 RID: 4017
	private bool isSwitchToLogin;

	// Token: 0x04000FB2 RID: 4018
	public static int nData = -1;

	// Token: 0x04000FB3 RID: 4019
	public static int maxData = -1;

	// Token: 0x04000FB4 RID: 4020
	public static SplashScr instance;

	// Token: 0x04000FB5 RID: 4021
	public static Image imgLogo;
}
