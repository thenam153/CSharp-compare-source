using System;

// Token: 0x020000AD RID: 173
public class Res
{
	// Token: 0x06000860 RID: 2144 RVA: 0x0007BA14 File Offset: 0x00079C14
	public static void init()
	{
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			if (Res.cosz[i] == 0)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0007BA94 File Offset: 0x00079C94
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.sinz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)Res.sinz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.sinz[a - 180]);
		}
		return (int)(-(int)Res.sinz[360 - a]);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0007BB14 File Offset: 0x00079D14
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.cosz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)(-(int)Res.cosz[180 - a]);
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.cosz[a - 180]);
		}
		return (int)Res.cosz[360 - a];
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0007BB94 File Offset: 0x00079D94
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return Res.tanz[a];
		}
		if (a >= 90 && a < 180)
		{
			return -Res.tanz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return Res.tanz[a - 180];
		}
		return -Res.tanz[360 - a];
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0007BC14 File Offset: 0x00079E14
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (Res.tanz[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0007BC44 File Offset: 0x00079E44
	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			int a = global::Math.abs((dy << 10) / dx);
			num = Res.atan(a);
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00006F59 File Offset: 0x00005159
	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle -= 360;
		}
		if (angle < 0)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00003584 File Offset: 0x00001784
	public static void outz(string s)
	{
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00003584 File Offset: 0x00001784
	public static void outz2(string s)
	{
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00003584 File Offset: 0x00001784
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00003584 File Offset: 0x00001784
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00006F80 File Offset: 0x00005180
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00005A85 File Offset: 0x00003C85
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00006F83 File Offset: 0x00005183
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00006F95 File Offset: 0x00005195
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00006FA7 File Offset: 0x000051A7
	public static int random(int a, int b)
	{
		if (a == b)
		{
			return a;
		}
		return a + Res.r.nextInt(b - a);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0007BCC8 File Offset: 0x00079EC8
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		if (currentTimeMillis * 16 % 1000 >= 5)
		{
			num++;
		}
		return num;
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00006FC1 File Offset: 0x000051C1
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0007BCF8 File Offset: 0x00079EF8
	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (global::Math.abs(num2 - num) > 1);
		return num;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00006FD7 File Offset: 0x000051D7
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000037F5 File Offset: 0x000019F5
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00006FE4 File Offset: 0x000051E4
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0007BD30 File Offset: 0x00079F30
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		string[] array;
		if (num >= 0)
		{
			array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0007BD88 File Offset: 0x00079F88
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 100000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0007BEBC File Offset: 0x0007A0BC
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 10000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000L)
		{
			text2 = "k";
			long num3 = number % 1000L / 10L;
			number /= 1000L;
			text = number + string.Empty;
			if (num3 > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num3,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x04000F71 RID: 3953
	private static short[] sinz = new short[]
	{
		0,
		18,
		36,
		54,
		71,
		89,
		107,
		125,
		143,
		160,
		178,
		195,
		213,
		230,
		248,
		265,
		282,
		299,
		316,
		333,
		350,
		367,
		384,
		400,
		416,
		433,
		449,
		465,
		481,
		496,
		512,
		527,
		543,
		558,
		573,
		587,
		602,
		616,
		630,
		644,
		658,
		672,
		685,
		698,
		711,
		724,
		737,
		749,
		761,
		773,
		784,
		796,
		807,
		818,
		828,
		839,
		849,
		859,
		868,
		878,
		887,
		896,
		904,
		912,
		920,
		928,
		935,
		943,
		949,
		956,
		962,
		968,
		974,
		979,
		984,
		989,
		994,
		998,
		1002,
		1005,
		1008,
		1011,
		1014,
		1016,
		1018,
		1020,
		1022,
		1023,
		1023,
		1024,
		1024
	};

	// Token: 0x04000F72 RID: 3954
	private static short[] cosz;

	// Token: 0x04000F73 RID: 3955
	private static int[] tanz;

	// Token: 0x04000F74 RID: 3956
	public static int count;

	// Token: 0x04000F75 RID: 3957
	public static bool isIcon;

	// Token: 0x04000F76 RID: 3958
	public static bool isBig;

	// Token: 0x04000F77 RID: 3959
	public static MyVector debug = new MyVector();

	// Token: 0x04000F78 RID: 3960
	public static MyRandom r = new MyRandom();
}
