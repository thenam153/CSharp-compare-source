using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class GameMidlet
{
	// Token: 0x06000932 RID: 2354 RVA: 0x000076DF File Offset: 0x000058DF
	public GameMidlet()
	{
		this.initGame();
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x000838EC File Offset: 0x00081AEC
	public void initGame()
	{
		GameMidlet.instance = this;
		MotherCanvas.instance = new MotherCanvas();
		Session_ME.gI().setHandler(Controller.gI());
		Session_ME2.gI().setHandler(Controller.gI());
		Session_ME2.isMainSession = false;
		GameMidlet.instance = this;
		GameMidlet.gameCanvas = new GameCanvas();
		GameMidlet.gameCanvas.start();
		SplashScr.loadImg();
		SplashScr.loadSplashScr();
		GameCanvas.currentScreen = new SplashScr();
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0000770D File Offset: 0x0000590D
	public void exit()
	{
		if (Main.typeClient == 6)
		{
			mSystem.exitWP();
		}
		else
		{
			GameCanvas.bRun = false;
			mSystem.gcc();
			this.notifyDestroyed();
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00007735 File Offset: 0x00005935
	public static void sendSMS(string data, string to, Command successAction, Command failAction)
	{
		Cout.println("SEND SMS");
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00007741 File Offset: 0x00005941
	public static void flatForm(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00007759 File Offset: 0x00005959
	public void notifyDestroyed()
	{
		Main.exit();
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00007760 File Offset: 0x00005960
	public void platformRequest(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x04001124 RID: 4388
	public static string IP = "112.213.94.23";

	// Token: 0x04001125 RID: 4389
	public static int PORT = 14445;

	// Token: 0x04001126 RID: 4390
	public static string IP2;

	// Token: 0x04001127 RID: 4391
	public static int PORT2;

	// Token: 0x04001128 RID: 4392
	public static sbyte PROVIDER;

	// Token: 0x04001129 RID: 4393
	public static string VERSION = "1.6.6";

	// Token: 0x0400112A RID: 4394
	public static GameCanvas gameCanvas;

	// Token: 0x0400112B RID: 4395
	public static GameMidlet instance;

	// Token: 0x0400112C RID: 4396
	public static bool isConnect2;

	// Token: 0x0400112D RID: 4397
	public static bool isBackWindowsPhone;
}
