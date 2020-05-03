using System;

// Token: 0x0200000A RID: 10
public class Math
{
	// Token: 0x06000052 RID: 82 RVA: 0x000037F5 File Offset: 0x000019F5
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003806 File Offset: 0x00001A06
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003816 File Offset: 0x00001A16
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00008254 File Offset: 0x00006454
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000019 RID: 25
	public const double PI = 3.1415926535897931;
}
