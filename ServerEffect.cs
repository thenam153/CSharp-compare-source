using System;

// Token: 0x02000041 RID: 65
public class ServerEffect : Effect2
{
	// Token: 0x06000287 RID: 647 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffect(int id, int cx, int cy, int loopCount, int trans)
	{
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffect(int id, Mob m, int loopCount)
	{
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffect(int id, global::Char c, int loopCount)
	{
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffect(int id, global::Char c, int loopCount, int trans)
	{
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffectWithTime(int id, int cx, int cy, int timeLengthInSecond)
	{
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00003584 File Offset: 0x00001784
	public static void addServerEffectWithTime(int id, global::Char c, int timeLengthInSecond)
	{
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00003584 File Offset: 0x00001784
	public override void paint(mGraphics g)
	{
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00003584 File Offset: 0x00001784
	public override void update()
	{
	}

	// Token: 0x04000314 RID: 788
	public EffectCharPaint eff;

	// Token: 0x04000315 RID: 789
	private int i0;

	// Token: 0x04000316 RID: 790
	private int dx0;

	// Token: 0x04000317 RID: 791
	private int dy0;

	// Token: 0x04000318 RID: 792
	private int x;

	// Token: 0x04000319 RID: 793
	private int y;

	// Token: 0x0400031A RID: 794
	private global::Char c;

	// Token: 0x0400031B RID: 795
	private Mob m;

	// Token: 0x0400031C RID: 796
	private short loopCount;

	// Token: 0x0400031D RID: 797
	private long endTime;

	// Token: 0x0400031E RID: 798
	private int trans;
}
