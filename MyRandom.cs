using System;

// Token: 0x0200000E RID: 14
public class MyRandom
{
	// Token: 0x06000065 RID: 101 RVA: 0x000038FB File Offset: 0x00001AFB
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000390E File Offset: 0x00001B0E
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000391B File Offset: 0x00001B1B
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003929 File Offset: 0x00001B29
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x0400001F RID: 31
	public Random r;
}
