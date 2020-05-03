using System;

// Token: 0x0200008F RID: 143
public class NinjaUtil
{
	// Token: 0x0600044F RID: 1103 RVA: 0x00005A72 File Offset: 0x00003C72
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00005A79 File Offset: 0x00003C79
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00035C68 File Offset: 0x00033E68
	public static int randomNumber(int max)
	{
		MyRandom myRandom = new MyRandom();
		return myRandom.nextInt(max);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00035C84 File Offset: 0x00033E84
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			sbyte[] result = new sbyte[num];
			msg.reader().read(ref result);
			return result;
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI DOC readByteArray NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00035CE4 File Offset: 0x00033EE4
	public static sbyte[] readByteArray(myReader dos)
	{
		try
		{
			int num = dos.readInt();
			sbyte[] result = new sbyte[num];
			dos.read(ref result);
			return result;
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00005A85 File Offset: 0x00003C85
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00035D38 File Offset: 0x00033F38
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string str = string.Empty;
		if (number.Equals(string.Empty))
		{
			return text;
		}
		if (number[0] == '-')
		{
			str = "-";
			number = number.Substring(1);
		}
		for (int i = number.Length - 1; i >= 0; i--)
		{
			if ((number.Length - 1 - i) % 3 == 0 && number.Length - 1 - i > 0)
			{
				text = number[i] + "." + text;
			}
			else
			{
				text = number[i] + text;
			}
		}
		return str + text;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00035DF4 File Offset: 0x00033FF4
	public static string getDate(int second)
	{
		int num = second * 1000;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan((long)num * 10000L)).ToLocalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		int day = dateTime2.Day;
		int month = dateTime2.Month;
		int year = dateTime2.Year;
		return string.Concat(new object[]
		{
			day,
			"/",
			month,
			"/",
			year,
			" ",
			hour,
			"h"
		});
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00035EB8 File Offset: 0x000340B8
	public static string getDate2(long second)
	{
		long num = second + 25200000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan(num * 10000L)).ToLocalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		return string.Concat(new object[]
		{
			hour,
			"h",
			minute,
			"m"
		});
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00035F3C File Offset: 0x0003413C
	public static string getTime(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num > 9)
			{
				text += num;
			}
			else
			{
				text = text + "0" + num;
			}
			text += ":";
			if (timeRemainS > 9)
			{
				text += timeRemainS;
			}
			else
			{
				text = text + "0" + timeRemainS;
			}
		}
		return text;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00036060 File Offset: 0x00034260
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			if (m < 1000L)
			{
				text = m + text;
				break;
			}
			long num3 = m % 1000L;
			if (num3 == 0L)
			{
				text = ".000" + text;
			}
			else if (num3 < 10L)
			{
				text = ".00" + num3 + text;
			}
			else if (num3 < 100L)
			{
				text = ".0" + num3 + text;
			}
			else
			{
				text = "." + num3 + text;
			}
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00036134 File Offset: 0x00034334
	public static string getTimeAgo(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num == 0)
			{
				num = 1;
			}
			text += num;
			text += "ph";
		}
		return text;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00036214 File Offset: 0x00034414
	public static string[] split(string original, string separator)
	{
		MyVector myVector = new MyVector();
		for (int i = original.IndexOf(separator); i >= 0; i = original.IndexOf(separator))
		{
			myVector.addElement(original.Substring(0, i));
			original = original.Substring(i + separator.Length);
		}
		myVector.addElement(original);
		string[] array = new string[myVector.size()];
		if (myVector.size() > 0)
		{
			for (int j = 0; j < myVector.size(); j++)
			{
				array[j] = (string)myVector.elementAt(j);
			}
		}
		return array;
	}
}
