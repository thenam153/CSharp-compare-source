using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
internal class Net
{
	// Token: 0x0600007C RID: 124 RVA: 0x000087E8 File Offset: 0x000069E8
	public static void update()
	{
		if (Net.www != null && Net.www.isDone)
		{
			string str = string.Empty;
			if (Net.www.error == null || Net.www.error.Equals(string.Empty))
			{
				str = Net.www.text;
			}
			Net.www = null;
			if (Net.h != null)
			{
				Net.h.perform(str);
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003A95 File Offset: 0x00001C95
	public static void connectHTTP(string link, Command h)
	{
		if (Net.www != null)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003ABC File Offset: 0x00001CBC
	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		if (link != null)
		{
			h.perform(link);
		}
	}

	// Token: 0x04000021 RID: 33
	public static WWW www;

	// Token: 0x04000022 RID: 34
	public static Command h;
}
