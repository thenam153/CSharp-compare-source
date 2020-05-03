using System;
using System.Text;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class mSystem
{
	// Token: 0x06000105 RID: 261 RVA: 0x00003FA1 File Offset: 0x000021A1
	public static void callHotlinePC()
	{
		Application.OpenURL("http://ngocrongonline.com/");
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00003584 File Offset: 0x00001784
	public static void callHotlineJava()
	{
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00003584 File Offset: 0x00001784
	public static void callHotlineIphone()
	{
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00003584 File Offset: 0x00001784
	public static void callHotlineWindowsPhone()
	{
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00003584 File Offset: 0x00001784
	public static void closeBanner()
	{
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00003584 File Offset: 0x00001784
	public static void showBanner()
	{
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00003584 File Offset: 0x00001784
	public static void createAdmob()
	{
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00003584 File Offset: 0x00001784
	public static void checkAdComlete()
	{
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00003FAD File Offset: 0x000021AD
	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00003FC0 File Offset: 0x000021C0
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000B6EC File Offset: 0x000098EC
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		if (scr == null || dest == null || scrPos + lenght > scr.Length)
		{
			return;
		}
		sbyte[] array = new sbyte[dest.Length + lenght];
		for (int i = 0; i < destPos; i++)
		{
			array[i] = dest[i];
		}
		for (int j = destPos; j < destPos + lenght; j++)
		{
			array[j] = scr[scrPos + j - destPos];
		}
		for (int k = destPos + lenght; k < array.Length; k++)
		{
			array[k] = dest[destPos + k - lenght];
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000B77C File Offset: 0x0000997C
	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00003FCD File Offset: 0x000021CD
	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000B7BC File Offset: 0x000099BC
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000B7F0 File Offset: 0x000099F0
	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(scr);
		return mSystem.convertToSbyte(bytes);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00007ABC File Offset: 0x00005CBC
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			if ((int)scr[i] > 0)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000B814 File Offset: 0x00009A14
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000B848 File Offset: 0x00009A48
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00003FDA File Offset: 0x000021DA
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00003FCD File Offset: 0x000021CD
	public static void gcc()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00003FE2 File Offset: 0x000021E2
	public static mSystem gI()
	{
		if (mSystem.instance == null)
		{
			mSystem.instance = new mSystem();
		}
		return mSystem.instance;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00003FFD File Offset: 0x000021FD
	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00004005 File Offset: 0x00002205
	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000400D File Offset: 0x0000220D
	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00003584 File Offset: 0x00001784
	public static void exitWP()
	{
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000B864 File Offset: 0x00009A64
	public static void paintFlyText(mGraphics g)
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1)
			{
				if (GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]))
				{
					if (GameScr.flyTextColor[i] == mFont.RED)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.YELLOW)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.GREEN)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL_ME)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
					}
					else if (GameScr.flyTextColor[i] == mFont.ORANGE)
					{
						mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.ADDMONEY)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS_ME)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.HP)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MP)
					{
						mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
				}
			}
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00003584 File Offset: 0x00001784
	public static void endKey()
	{
	}

	// Token: 0x040000E6 RID: 230
	public const int JAVA = 1;

	// Token: 0x040000E7 RID: 231
	public const int ANDROID = 2;

	// Token: 0x040000E8 RID: 232
	public const int IP_JB = 3;

	// Token: 0x040000E9 RID: 233
	public const int PC = 4;

	// Token: 0x040000EA RID: 234
	public const int IP_APPSTORE = 5;

	// Token: 0x040000EB RID: 235
	public const int WINDOWS_PHONE = 6;

	// Token: 0x040000EC RID: 236
	public const int GOOGLE_PLAY = 7;

	// Token: 0x040000ED RID: 237
	public static string strAdmob;

	// Token: 0x040000EE RID: 238
	public static bool loadAdOk;

	// Token: 0x040000EF RID: 239
	public static string publicID;

	// Token: 0x040000F0 RID: 240
	public static string android_pack;

	// Token: 0x040000F1 RID: 241
	public static int clientType = 4;

	// Token: 0x040000F2 RID: 242
	public static mSystem instance;
}
