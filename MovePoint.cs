using System;

// Token: 0x02000065 RID: 101
public class MovePoint
{
	// Token: 0x06000358 RID: 856 RVA: 0x00004FB3 File Offset: 0x000031B3
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00004FD8 File Offset: 0x000031D8
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x040005BC RID: 1468
	public int xEnd;

	// Token: 0x040005BD RID: 1469
	public int yEnd;

	// Token: 0x040005BE RID: 1470
	public int dir;

	// Token: 0x040005BF RID: 1471
	public int cvx;

	// Token: 0x040005C0 RID: 1472
	public int cvy;

	// Token: 0x040005C1 RID: 1473
	public int status;
}
