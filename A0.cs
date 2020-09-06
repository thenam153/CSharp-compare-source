using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

public class A0
{
	public static string[][] listItem;

	public static int zone;

	public static int lengthItem;

	public static int server;

	public static string username;

	public static string password;

	public static bool beingLogin;

	public static bool isAutoTtnl;

	public static void muaDo()
	{
		try
		{
			string[] array = File.ReadAllLines("item.ini");
			listItem = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				listItem[i] = array[i].Split('|');
				zone = int.Parse(listItem[i][0]);
			}
		}
		catch (Exception)
		{
		}
	}

	public static void update()
	{
		try
		{
			if (TileMap.zoneID != zone && GameCanvas.gameTick % 2000 == 0)
			{
				Service.gI().requestChangeZone(zone, -1);
			}
			if (GameCanvas.gameTick % 100 == 0)
			{
				lengthItem++;
				if (lengthItem >= listItem.Length)
				{
					lengthItem = 0;
				}
				int num = int.Parse(listItem[lengthItem][1]);
				string text = listItem[lengthItem][2];
				int num2 = Char.myCharz().arrItemBag.Length - 1;
				Item item;
				while (true)
				{
					if (num2 < 0)
					{
						return;
					}
					item = Char.myCharz().arrItemBag[num2];
					if (item != null && item.template.id == num)
					{
						break;
					}
					num2--;
				}
				Service.gI().chat("Mua " + text + " x" + (99 - item.quantity) + " gia " + NinjaUtil.getMoneys(long.Parse(listItem[lengthItem][3])) + " x1" + " ban gd \n" + Char.myCharz().cName);
			}
		}
		catch (Exception)
		{
		}
	}

	static A0()
	{
		zone = -1;
		lengthItem = 0;
	}

	public static void cache()
	{
		lengthItem++;
		if (lengthItem >= listItem.Length)
		{
			lengthItem = 0;
		}
		int num = int.Parse(listItem[lengthItem][1]);
		string text = listItem[lengthItem][2];
		int num2 = Char.myCharz().arrItemBag.Length - 1;
		Item item;
		while (true)
		{
			if (num2 >= 0)
			{
				item = Char.myCharz().arrItemBag[num2];
				if (item != null && item.template.id == num)
				{
					break;
				}
				num2--;
				continue;
			}
			return;
		}
		Service.gI().chat("Mua " + text + " x" + (99 - item.quantity) + " gia " + NinjaUtil.getMoneys(long.Parse(listItem[lengthItem][3])) + " x1" + " ban gd \n" + Char.myCharz().cName);
	}

	public static void getData()
	{
		username = File.ReadAllText("acc.ini").Split('|')[0];
		password = File.ReadAllText("acc.ini").Split('|')[1];
		server = int.Parse(File.ReadAllText("acc.ini").Split('|')[2]) - 1;
		muaDo();
	}

	public static void login()
	{
		Thread.Sleep(3000);
	}

	public static void autoTTNL()
	{
		try
		{
			new Thread((ThreadStart)delegate
			{
				while (!isAutoTtnl)
				{
					Service.gI().skill_not_focus(1);
					Thread.Sleep(Skills.get(62).coolDown + 10000);
				}
			}).Start();
		}
		catch (Exception)
		{
		}
	}

	public static void autoCaptcha()
	{
		try
		{
			WebClient webClient = new WebClient();
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				["key"] = File.ReadAllText("captcha.nt")
			};
			byte[] inArray = GameScr.imgCapcha.texture.EncodeToPNG();
			nameValueCollection["image"] = Convert.ToBase64String(inArray);
			byte[] bytes = webClient.UploadValues("http://api.nroshopvn.com/postnow", nameValueCollection);
			string @string = Encoding.Default.GetString(bytes);
			Thread.Sleep(1500);
			if (@string != "error")
			{
				for (int i = 0; i < @string.Length; i++)
				{
					Service.gI().mobCapcha(@string[i]);
					Thread.Sleep(300);
				}
			}
			else
			{
				isAutoTtnl = false;
				new Thread(outAndLogin).Start();
			}
		}
		catch (Exception)
		{
			isAutoTtnl = false;
			new Thread(outAndLogin).Start();
		}
	}

	public static void outAndLogin()
	{
		Thread.Sleep(2000);
		GameCanvas.gI().onDisconnected();
		Thread.Sleep(2000);
		for (int num = 26; num >= 0; num--)
		{
			GameCanvas.startOKDlg("Tự vào game sau " + num + "s");
			Thread.Sleep(1000);
		}
		while (!ServerListScreen.loadScreen)
		{
			Thread.Sleep(10);
		}
		GameCanvas.serverScreen.selectServer();
		while (!Session_ME.gI().isConnected())
		{
			Thread.Sleep(100);
		}
		Thread.Sleep(2000);
		while (!ServerListScreen.loadScreen)
		{
			Thread.Sleep(10);
		}
		if (GameCanvas.loginScr == null)
		{
			GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			GameCanvas.loginScr.doLogin();
		}
		Thread.Sleep(4000);
		isAutoTtnl = true;
	}
}
