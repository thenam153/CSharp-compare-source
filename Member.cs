using System;

// Token: 0x02000031 RID: 49
public class Member
{
	// Token: 0x06000215 RID: 533 RVA: 0x000047F7 File Offset: 0x000029F7
	public static string getRole(int r)
	{
		if (r == 0)
		{
			return mResources.clan_leader;
		}
		if (r == 1)
		{
			return mResources.clan_coleader;
		}
		if (r == 2)
		{
			return mResources.member;
		}
		return string.Empty;
	}

	// Token: 0x040001F9 RID: 505
	public int ID;

	// Token: 0x040001FA RID: 506
	public short head;

	// Token: 0x040001FB RID: 507
	public short leg;

	// Token: 0x040001FC RID: 508
	public short body;

	// Token: 0x040001FD RID: 509
	public string name;

	// Token: 0x040001FE RID: 510
	public sbyte role;

	// Token: 0x040001FF RID: 511
	public string powerPoint;

	// Token: 0x04000200 RID: 512
	public int donate;

	// Token: 0x04000201 RID: 513
	public int receive_donate;

	// Token: 0x04000202 RID: 514
	public int clanPoint;

	// Token: 0x04000203 RID: 515
	public int lastRequest;

	// Token: 0x04000204 RID: 516
	public string joinTime;
}
