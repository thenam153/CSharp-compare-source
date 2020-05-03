using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class Main : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x0000BB54 File Offset: 0x00009D54
	private void Start()
	{
		if (!Main.started)
		{
			Time.timeScale = 2f;
			if (Thread.CurrentThread.Name != "Main")
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Main.isPC = true;
			Main.started = true;
			if (Main.isPC)
			{
				this.level = Rms.loadRMSInt("levelScreenKN");
				if (this.level == 1)
				{
					Screen.SetResolution(200, 200, false);
					return;
				}
				Screen.SetResolution(1024, 600, false);
			}
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00004047 File Offset: 0x00002247
	private void SetInit()
	{
		base.enabled = true;
		Debug.Log("Facebook PublishInstall!");
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000405A File Offset: 0x0000225A
	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
			return;
		}
		Time.timeScale = 1f;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000BBF8 File Offset: 0x00009DF8
	private void OnGUI()
	{
		if (this.count < 10)
		{
			return;
		}
		this.checkInput();
		Session_ME.update();
		Session_ME2.update();
		if (Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
		{
			GameMidlet.gameCanvas.paint(Main.g);
			this.paintCount++;
			Main.g.reset();
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000BC78 File Offset: 0x00009E78
	public void setsizeChange()
	{
		if (!this.isRun)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Application.runInBackground = true;
			Application.targetFrameRate = 30;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			if (Main.main == null)
			{
				Main.main = this;
			}
			this.isRun = true;
			ScaleGUI.initScaleGUI();
			if (Main.isPC)
			{
				Main.IMEI = SystemInfo.deviceUniqueIdentifier;
			}
			else
			{
				Main.IMEI = this.GetMacAddress();
			}
			Main.isPC = true;
			if (Main.isPC)
			{
				Screen.fullScreen = false;
			}
			if (Main.isWindowsPhone)
			{
				Main.typeClient = 6;
			}
			if (Main.isPC)
			{
				Main.typeClient = 4;
			}
			if (Main.IphoneVersionApp)
			{
				Main.typeClient = 5;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
			{
				Main.isIpod = true;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
			{
				Main.isIphone4 = true;
			}
			Main.g = new mGraphics();
			Main.midlet = new GameMidlet();
			TileMap.loadBg();
			Paint.loadbg();
			PopUp.loadBg();
			GameScr.loadBg();
			InfoMe.gI().loadCharId();
			Panel.loadBg();
			Menu.loadBg();
			Key.mapKeyPC();
			SoundMn.gI().loadSound(TileMap.mapID);
			Main.g.CreateLineMaterial();
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00003584 File Offset: 0x00001784
	public static void setBackupIcloud(string path)
	{
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000BDA4 File Offset: 0x00009FA4
	public string GetMacAddress()
	{
		string empty = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		for (int i = 0; i < allNetworkInterfaces.Length; i++)
		{
			PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
			if (physicalAddress.ToString() != string.Empty)
			{
				return physicalAddress.ToString();
			}
		}
		return string.Empty;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00004074 File Offset: 0x00002274
	public void doClearRMS()
	{
		if (!Main.isPC)
		{
			return;
		}
		if (Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
		{
			Rms.clearAll();
			Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
			Rms.saveRMSInt("levelScreenKN", this.level);
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000040B3 File Offset: 0x000022B3
	public static void closeKeyBoard()
	{
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000BDF4 File Offset: 0x00009FF4
	private void FixedUpdate()
	{
		Rms.update();
		this.count++;
		if (this.count < 10)
		{
			return;
		}
		this.setsizeChange();
		this.updateCount++;
		ipKeyboard.update();
		GameMidlet.gameCanvas.update();
		Image.update();
		DataInputStream.update();
		SMS.update();
		Net.update();
		Main.f++;
		if (Main.f > 8)
		{
			Main.f = 0;
		}
		if (!Main.isPC)
		{
			int num = 1 / Main.a;
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00003584 File Offset: 0x00001784
	private void Update()
	{
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000BE80 File Offset: 0x0000A080
	private void checkInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition3 = Input.mousePosition;
			this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				KeyCode keyCode = Event.current.keyCode;
				if (keyCode != KeyCode.Minus)
				{
					if (keyCode == KeyCode.Alpha2)
					{
						num = 64;
					}
				}
				else
				{
					num = 95;
				}
			}
			if (num != 0)
			{
				GameMidlet.gameCanvas.keyPressedz(num);
			}
		}
		if (Event.current.type == EventType.KeyUp)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				GameMidlet.gameCanvas.keyReleasedz(num2);
			}
		}
		if (Main.isPC)
		{
			GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
			int num3 = (int)Input.mousePosition.x;
			float y = Input.mousePosition.y;
			int x = num3 / mGraphics.zoomLevel;
			int y2 = (Screen.height - (int)y) / mGraphics.zoomLevel;
			GameMidlet.gameCanvas.pointerMouse(x, y2);
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000040CD File Offset: 0x000022CD
	private void OnApplicationQuit()
	{
		Debug.LogWarning("APP QUIT");
		GameCanvas.bRun = false;
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Main.isPC)
		{
			Application.Quit();
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
	private void OnApplicationPause(bool paused)
	{
		Main.isResume = false;
		if (paused)
		{
			if (GameCanvas.isWaiting())
			{
				Main.isQuitApp = true;
			}
		}
		else
		{
			Main.isResume = true;
		}
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
		if (Main.isQuitApp)
		{
			Application.Quit();
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000040FF File Offset: 0x000022FF
	public static void exit()
	{
		if (Main.isPC)
		{
			Main.main.OnApplicationQuit();
			return;
		}
		Main.a = 0;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00004119 File Offset: 0x00002319
	public static bool detectCompactDevice()
	{
		return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00004140 File Offset: 0x00002340
	public static bool checkCanSendSMS()
	{
		return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
	}

	// Token: 0x040000F3 RID: 243
	public const sbyte PC_VERSION = 4;

	// Token: 0x040000F4 RID: 244
	public const sbyte IP_APPSTORE = 5;

	// Token: 0x040000F5 RID: 245
	public const sbyte WINDOWSPHONE = 6;

	// Token: 0x040000F6 RID: 246
	public const sbyte IP_JB = 3;

	// Token: 0x040000F7 RID: 247
	public static Main main;

	// Token: 0x040000F8 RID: 248
	public static mGraphics g;

	// Token: 0x040000F9 RID: 249
	public static GameMidlet midlet;

	// Token: 0x040000FA RID: 250
	public static string res = "res";

	// Token: 0x040000FB RID: 251
	public static string mainThreadName;

	// Token: 0x040000FC RID: 252
	public static bool started;

	// Token: 0x040000FD RID: 253
	public static bool isIpod;

	// Token: 0x040000FE RID: 254
	public static bool isIphone4;

	// Token: 0x040000FF RID: 255
	public static bool isPC;

	// Token: 0x04000100 RID: 256
	public static bool isWindowsPhone;

	// Token: 0x04000101 RID: 257
	public static bool isIPhone;

	// Token: 0x04000102 RID: 258
	public static bool IphoneVersionApp;

	// Token: 0x04000103 RID: 259
	public static string IMEI;

	// Token: 0x04000104 RID: 260
	public static int versionIp;

	// Token: 0x04000105 RID: 261
	public static int numberQuit = 1;

	// Token: 0x04000106 RID: 262
	public static int typeClient = 4;

	// Token: 0x04000107 RID: 263
	private int level;

	// Token: 0x04000108 RID: 264
	private int updateCount;

	// Token: 0x04000109 RID: 265
	private int paintCount;

	// Token: 0x0400010A RID: 266
	private int count;

	// Token: 0x0400010B RID: 267
	private bool isRun;

	// Token: 0x0400010C RID: 268
	public static int waitTick;

	// Token: 0x0400010D RID: 269
	public static int f;

	// Token: 0x0400010E RID: 270
	public static bool isResume;

	// Token: 0x0400010F RID: 271
	public static bool isMiniApp = true;

	// Token: 0x04000110 RID: 272
	public static bool isQuitApp;

	// Token: 0x04000111 RID: 273
	private Vector2 lastMousePos;

	// Token: 0x04000112 RID: 274
	public static int a = 1;

	// Token: 0x04000113 RID: 275
	public static bool isCompactDevice = true;
}
