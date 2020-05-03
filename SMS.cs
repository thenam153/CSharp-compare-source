using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class SMS
{
	// Token: 0x0600009B RID: 155 RVA: 0x00003B3E File Offset: 0x00001D3E
	public static int send(string content, string to)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return SMS.__send(content, to);
		}
		return SMS._send(content, to);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00008D7C File Offset: 0x00006F7C
	private static int _send(string content, string to)
	{
		if (SMS.status != 0)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (SMS.status == 0)
				{
					break;
				}
			}
			if (SMS.status != 0)
			{
				Cout.LogError("CANNOT SEND SMS " + content + " WHEN SENDING " + SMS._content);
				return -1;
			}
		}
		SMS._content = content;
		SMS._to = to;
		SMS._result = -1;
		SMS.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			if (SMS.status == 0)
			{
				break;
			}
		}
		if (j == 500)
		{
			Debug.LogError("TOO LONG FOR SEND SMS " + content);
			SMS.status = 0;
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Send SMS ",
				content,
				" done in ",
				j * 5,
				"ms"
			}));
		}
		return SMS._result;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00008E8C File Offset: 0x0000708C
	private static int __send(string content, string to)
	{
		int num = iOSPlugins.Check();
		Cout.println("vao sms ko " + num);
		if (num >= 0)
		{
			SMS.f = true;
			SMS.sendEnable = true;
			iOSPlugins.SMSsend(to, content, num);
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		return num;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00008ED8 File Offset: 0x000070D8
	public static void update()
	{
		float num = Time.time;
		if (num - (float)SMS.time > 1f)
		{
			SMS.time++;
		}
		if (SMS.f)
		{
			SMS.OnSMS();
		}
		if (SMS.status == 2)
		{
			SMS.status = 1;
			try
			{
				SMS._result = SMS.__send(SMS._content, SMS._to);
			}
			catch (Exception ex)
			{
				Debug.Log("CANNOT SEND SMS");
			}
			SMS.status = 0;
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00008F68 File Offset: 0x00007168
	private static void OnSMS()
	{
		if (SMS.sendEnable)
		{
			if (iOSPlugins.checkRotation() == 1)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			else if (iOSPlugins.checkRotation() == -1)
			{
				Screen.orientation = ScreenOrientation.Portrait;
			}
			else if (iOSPlugins.checkRotation() == 0)
			{
				Screen.orientation = ScreenOrientation.AutoRotation;
			}
			else if (iOSPlugins.checkRotation() == 2)
			{
				Screen.orientation = ScreenOrientation.LandscapeRight;
			}
			else if (iOSPlugins.checkRotation() == 3)
			{
				Screen.orientation = ScreenOrientation.PortraitUpsideDown;
			}
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				iOSPlugins.Send();
				SMS.sendEnable = false;
				SMS.time0 = 0;
			}
		}
		if (iOSPlugins.unpause() == 1)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			if (SMS.time0 < 5)
			{
				SMS.time0++;
			}
			else
			{
				SMS.f = false;
				iOSPlugins.back();
				SMS.time0 = 0;
			}
		}
	}

	// Token: 0x0400004B RID: 75
	private const int INTERVAL = 5;

	// Token: 0x0400004C RID: 76
	private const int MAXTIME = 500;

	// Token: 0x0400004D RID: 77
	private static int status;

	// Token: 0x0400004E RID: 78
	private static int _result;

	// Token: 0x0400004F RID: 79
	private static string _to;

	// Token: 0x04000050 RID: 80
	private static string _content;

	// Token: 0x04000051 RID: 81
	private static bool f;

	// Token: 0x04000052 RID: 82
	private static int time;

	// Token: 0x04000053 RID: 83
	public static bool sendEnable;

	// Token: 0x04000054 RID: 84
	private static int time0;
}
