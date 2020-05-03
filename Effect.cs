using System;

// Token: 0x02000036 RID: 54
public class Effect
{
	// Token: 0x0600024A RID: 586 RVA: 0x00012898 File Offset: 0x00010A98
	public Effect()
	{
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00012898 File Offset: 0x00010A98
	public Effect(int id, int x, int y, int layer, int loop, int loopCount)
	{
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00003584 File Offset: 0x00001784
	public static void removeEffData(int id)
	{
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00003584 File Offset: 0x00001784
	public static void addEffData(EffectData eff)
	{
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00003AD1 File Offset: 0x00001CD1
	public static EffectData getEffDataById(int id)
	{
		return null;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00003868 File Offset: 0x00001A68
	public static bool isExistNewEff(string id)
	{
		return false;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00004957 File Offset: 0x00002B57
	public bool isPaintz()
	{
		return this.isPaint;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0001292C File Offset: 0x00010B2C
	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		if (!this.isPaintz())
		{
			return;
		}
		if (Effect.getEffDataById(this.effId).img != null)
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00003584 File Offset: 0x00001784
	public void getFrameKhangia()
	{
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00003584 File Offset: 0x00001784
	public void paint(mGraphics g)
	{
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00003584 File Offset: 0x00001784
	public void update()
	{
	}

	// Token: 0x04000271 RID: 625
	public const int NEAR_PLAYER = 0;

	// Token: 0x04000272 RID: 626
	public const int LOOP_NORMAL = 1;

	// Token: 0x04000273 RID: 627
	public const int LOOP_TRANS = 2;

	// Token: 0x04000274 RID: 628
	public const int BACKGROUND = 3;

	// Token: 0x04000275 RID: 629
	public const int FIRE_TD = 0;

	// Token: 0x04000276 RID: 630
	public const int BIRD = 1;

	// Token: 0x04000277 RID: 631
	public const int FIRE_NAMEK = 2;

	// Token: 0x04000278 RID: 632
	public const int FIRE_SAYAI = 3;

	// Token: 0x04000279 RID: 633
	public const int FROG = 5;

	// Token: 0x0400027A RID: 634
	public const int CA = 4;

	// Token: 0x0400027B RID: 635
	public const int ECH = 6;

	// Token: 0x0400027C RID: 636
	public const int TACKE = 7;

	// Token: 0x0400027D RID: 637
	public const int RAN = 8;

	// Token: 0x0400027E RID: 638
	public const int KHI = 9;

	// Token: 0x0400027F RID: 639
	public const int GACON = 10;

	// Token: 0x04000280 RID: 640
	public const int DANONG = 11;

	// Token: 0x04000281 RID: 641
	public const int DANBUOM = 12;

	// Token: 0x04000282 RID: 642
	public const int QUA = 13;

	// Token: 0x04000283 RID: 643
	public const int THIENTHACH = 14;

	// Token: 0x04000284 RID: 644
	public const int CAVOI = 15;

	// Token: 0x04000285 RID: 645
	public const int NAM = 16;

	// Token: 0x04000286 RID: 646
	public const int RONGTHAN = 17;

	// Token: 0x04000287 RID: 647
	public const int BUOMBAY = 26;

	// Token: 0x04000288 RID: 648
	public const int KHUCGO = 27;

	// Token: 0x04000289 RID: 649
	public const int DOIBAY = 28;

	// Token: 0x0400028A RID: 650
	public const int CONMEO = 29;

	// Token: 0x0400028B RID: 651
	public const int LUATAT = 30;

	// Token: 0x0400028C RID: 652
	public const int ONGCONG = 31;

	// Token: 0x0400028D RID: 653
	public const int KHANGIA1 = 42;

	// Token: 0x0400028E RID: 654
	public const int KHANGIA2 = 43;

	// Token: 0x0400028F RID: 655
	public const int KHANGIA3 = 44;

	// Token: 0x04000290 RID: 656
	public const int KHANGIA4 = 45;

	// Token: 0x04000291 RID: 657
	public const int KHANGIA5 = 46;

	// Token: 0x04000292 RID: 658
	public int effId;

	// Token: 0x04000293 RID: 659
	public int typeEff;

	// Token: 0x04000294 RID: 660
	public int indexFrom;

	// Token: 0x04000295 RID: 661
	public int indexTo;

	// Token: 0x04000296 RID: 662
	public bool isNearPlayer;

	// Token: 0x04000297 RID: 663
	public int t;

	// Token: 0x04000298 RID: 664
	public int currFrame;

	// Token: 0x04000299 RID: 665
	public int x;

	// Token: 0x0400029A RID: 666
	public int y;

	// Token: 0x0400029B RID: 667
	public int loop;

	// Token: 0x0400029C RID: 668
	public int tLoop;

	// Token: 0x0400029D RID: 669
	public int tLoopCount;

	// Token: 0x0400029E RID: 670
	private bool isPaint = true;

	// Token: 0x0400029F RID: 671
	public int layer;

	// Token: 0x040002A0 RID: 672
	public static MyVector vEffData = new MyVector();

	// Token: 0x040002A1 RID: 673
	public int trans;

	// Token: 0x040002A2 RID: 674
	public static MyVector lastEff = new MyVector();

	// Token: 0x040002A3 RID: 675
	public static MyVector newEff = new MyVector();

	// Token: 0x040002A4 RID: 676
	private int[] khangia1 = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x040002A5 RID: 677
	private int[] khangia2 = new int[]
	{
		2,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		3
	};

	// Token: 0x040002A6 RID: 678
	private int[] khangia3 = new int[]
	{
		4,
		4,
		4,
		4,
		4,
		5,
		5,
		5,
		5,
		5
	};

	// Token: 0x040002A7 RID: 679
	private int[] khangia4 = new int[]
	{
		6,
		6,
		6,
		6,
		6,
		7,
		7,
		7,
		7,
		7
	};

	// Token: 0x040002A8 RID: 680
	private int[] khangia5 = new int[]
	{
		8,
		8,
		8,
		8,
		8,
		9,
		9,
		9,
		9,
		9
	};

	// Token: 0x040002A9 RID: 681
	private bool isGetTime;
}
