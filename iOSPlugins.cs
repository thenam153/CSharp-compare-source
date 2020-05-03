using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class iOSPlugins
{
	// Token: 0x060000F1 RID: 241
	[DllImport("__Internal")]
	private static extern void _SMSsend(string tophone, string withtext, int n);

	// Token: 0x060000F2 RID: 242
	[DllImport("__Internal")]
	private static extern int _unpause();

	// Token: 0x060000F3 RID: 243
	[DllImport("__Internal")]
	private static extern int _checkRotation();

	// Token: 0x060000F4 RID: 244
	[DllImport("__Internal")]
	private static extern int _back();

	// Token: 0x060000F5 RID: 245
	[DllImport("__Internal")]
	private static extern int _Send();

	// Token: 0x060000F6 RID: 246
	[DllImport("__Internal")]
	private static extern void _purchaseItem(string itemID, string userName, string gameID);

	// Token: 0x060000F7 RID: 247 RVA: 0x0000B530 File Offset: 0x00009730
	public static int Check()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return iOSPlugins.checkCanSendSMS();
		}
		iOSPlugins.devide = iPhoneSettings.generation.ToString();
		string a = string.Empty + iOSPlugins.devide[2];
		if (a == "h" && iOSPlugins.devide.Length > 6)
		{
			iOSPlugins.Myname = SystemInfo.operatingSystem.ToString();
			string a2 = string.Empty + iOSPlugins.Myname[10];
			if (a2 != "2" && a2 != "3")
			{
				return 0;
			}
			return 1;
		}
		else
		{
			Cout.println(iOSPlugins.devide + "  loai");
			if (iOSPlugins.devide == "Unknown" && ScaleGUI.WIDTH * ScaleGUI.HEIGHT < 786432f)
			{
				return 0;
			}
			return -1;
		}
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00003EE6 File Offset: 0x000020E6
	public static int checkCanSendSMS()
	{
		if (iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation == iPhoneGeneration.iPhone4S || iPhoneSettings.generation == iPhoneGeneration.iPhone5)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00003F19 File Offset: 0x00002119
	public static void SMSsend(string phonenumber, string bodytext, int n)
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._SMSsend(phonenumber, bodytext, n);
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00003F2D File Offset: 0x0000212D
	public static void back()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._back();
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00003F3F File Offset: 0x0000213F
	public static void Send()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._Send();
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00003F51 File Offset: 0x00002151
	public static int unpause()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			return iOSPlugins._unpause();
		}
		return 0;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00003F64 File Offset: 0x00002164
	public static int checkRotation()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			return iOSPlugins._checkRotation();
		}
		return 0;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00003F77 File Offset: 0x00002177
	public static void purchaseItem(string itemID, string userName, string gameID)
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._purchaseItem(itemID, userName, gameID);
		}
	}

	// Token: 0x040000DF RID: 223
	public static string devide;

	// Token: 0x040000E0 RID: 224
	public static string Myname;
}
