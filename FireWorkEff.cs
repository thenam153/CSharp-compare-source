using System;

// Token: 0x0200003B RID: 59
public class FireWorkEff
{
	// Token: 0x0600026D RID: 621 RVA: 0x0001332C File Offset: 0x0001152C
	public static void preDraw()
	{
		if (FireWorkEff.st)
		{
			FireWorkEff.animate();
		}
		if (FireWorkEff.t > 32 && FireWorkEff.st)
		{
			FireWorkEff.st = false;
			FireWorkEff.mg.removeAllElements();
			FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x000133A4 File Offset: 0x000115A4
	public static void paint(mGraphics g)
	{
		FireWorkEff.preDraw();
		g.setColor(0);
		g.fillRect(0, 0, FireWorkEff.w, FireWorkEff.h);
		g.setColor(16711680);
		for (int i = 0; i < FireWorkEff.mg.size(); i++)
		{
			((FireWorkMn)FireWorkEff.mg.elementAt(i)).paint(g);
		}
		if (!FireWorkEff.st)
		{
			FireWorkEff.keyPressed(-(global::Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00013430 File Offset: 0x00011630
	public static void keyPressed(int k)
	{
		if (k == -5 && !FireWorkEff.st)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -7 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 60;
			FireWorkEff.x0 = 0;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else if (k == -6 && !FireWorkEff.st)
		{
			FireWorkEff.ag = 120;
			FireWorkEff.x0 = FireWorkEff.w;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x000134D0 File Offset: 0x000116D0
	public static void add()
	{
		FireWorkEff.y0 = 0;
		FireWorkEff.v = 16;
		FireWorkEff.t = 0;
		FireWorkEff.a = 0f;
		for (int i = 0; i < 3; i++)
		{
			FireWorkEff.mang_y[i] = 0;
			FireWorkEff.mang_x[i] = FireWorkEff.x0;
		}
		FireWorkEff.st = true;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00013528 File Offset: 0x00011728
	public static void animate()
	{
		FireWorkEff.mang_y[2] = FireWorkEff.mang_y[1];
		FireWorkEff.mang_x[2] = FireWorkEff.mang_x[1];
		FireWorkEff.mang_y[1] = FireWorkEff.mang_y[0];
		FireWorkEff.mang_x[1] = FireWorkEff.mang_x[0];
		FireWorkEff.mang_y[0] = FireWorkEff.y;
		FireWorkEff.mang_x[0] = FireWorkEff.x;
		FireWorkEff.x = Res.cos((int)((double)FireWorkEff.ag * 3.1415926535897931 / 180.0)) * FireWorkEff.v * FireWorkEff.t + FireWorkEff.x0;
		FireWorkEff.y = (int)((float)(FireWorkEff.v * Res.sin((int)((double)FireWorkEff.ag * 3.1415926535897931 / 180.0)) * FireWorkEff.t) - FireWorkEff.a * (float)FireWorkEff.t * (float)FireWorkEff.t / 2f) + FireWorkEff.y0;
		if (FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay)
		{
			FireWorkEff.t++;
			FireWorkEff.last = FireWorkEff.time();
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x000049A5 File Offset: 0x00002BA5
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x040002C9 RID: 713
	private static int w;

	// Token: 0x040002CA RID: 714
	private static int h;

	// Token: 0x040002CB RID: 715
	private static MyRandom r = new MyRandom();

	// Token: 0x040002CC RID: 716
	private static MyVector mg = new MyVector();

	// Token: 0x040002CD RID: 717
	private static int f = 17;

	// Token: 0x040002CE RID: 718
	private static int x;

	// Token: 0x040002CF RID: 719
	private static int y;

	// Token: 0x040002D0 RID: 720
	private static int ag;

	// Token: 0x040002D1 RID: 721
	private static int x0;

	// Token: 0x040002D2 RID: 722
	private static int y0;

	// Token: 0x040002D3 RID: 723
	private static int t;

	// Token: 0x040002D4 RID: 724
	private static int v;

	// Token: 0x040002D5 RID: 725
	private static int ymax = 269;

	// Token: 0x040002D6 RID: 726
	private static float a;

	// Token: 0x040002D7 RID: 727
	private static int[] mang_x = new int[3];

	// Token: 0x040002D8 RID: 728
	private static int[] mang_y = new int[3];

	// Token: 0x040002D9 RID: 729
	private static bool st = false;

	// Token: 0x040002DA RID: 730
	private static long last = 0L;

	// Token: 0x040002DB RID: 731
	private static long delay = 150L;
}
