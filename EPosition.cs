using System;

// Token: 0x02000034 RID: 52
public class EPosition
{
	// Token: 0x0600023C RID: 572 RVA: 0x0000489B File Offset: 0x00002A9B
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600023D RID: 573 RVA: 0x000048BF File Offset: 0x00002ABF
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x000048EB File Offset: 0x00002AEB
	public EPosition()
	{
	}

	// Token: 0x04000269 RID: 617
	public int x;

	// Token: 0x0400026A RID: 618
	public int y;

	// Token: 0x0400026B RID: 619
	public int anchor;

	// Token: 0x0400026C RID: 620
	public sbyte follow;

	// Token: 0x0400026D RID: 621
	public sbyte count;

	// Token: 0x0400026E RID: 622
	public sbyte dir = 1;

	// Token: 0x0400026F RID: 623
	public short index = -1;
}
