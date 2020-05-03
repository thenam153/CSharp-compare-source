using System;

// Token: 0x0200003D RID: 61
public class Firework
{
	// Token: 0x06000275 RID: 629 RVA: 0x00013768 File Offset: 0x00011968
	public Firework(int x0, int y0, int v, int angle, int cl)
	{
		this.y0 = y0;
		this.x0 = x0;
		this.a = 1f;
		this.v = v;
		this.angle = angle;
		this.w = GameCanvas.w;
		this.h = GameCanvas.h;
		this.last = this.time();
		for (int i = 0; i < 2; i++)
		{
			this.arr_x[i] = x0;
			this.arr_y[i] = y0;
		}
		this.cl = cl;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00013828 File Offset: 0x00011A28
	public void preDraw()
	{
		if (this.time() - this.last >= this.delay)
		{
			this.t++;
			this.last = this.time();
			this.arr_x[1] = this.arr_x[0];
			this.arr_y[1] = this.arr_y[0];
			this.arr_x[0] = this.x;
			this.arr_y[0] = this.y;
			this.x = Res.cos((int)((double)this.angle * 3.1415926535897931 / 180.0)) * this.v * this.t + this.x0;
			this.y = (int)((float)(this.v * Res.sin((int)((double)this.angle * 3.1415926535897931 / 180.0)) * this.t) - this.a * (float)this.t * (float)this.t / 2f) + this.y0;
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0001393C File Offset: 0x00011B3C
	public void paint(mGraphics g)
	{
		this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
		for (int i = 0; i < 2; i++)
		{
			this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
		}
		if (this.act)
		{
			this.preDraw();
		}
	}

	// Token: 0x06000278 RID: 632 RVA: 0x000049A5 File Offset: 0x00002BA5
	public long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x06000279 RID: 633 RVA: 0x000049AC File Offset: 0x00002BAC
	public void Drawline(mGraphics g, int x, int y, int color)
	{
		g.setColor(color);
		g.fillRect(x, y, 1, 2);
	}

	// Token: 0x040002E3 RID: 739
	public int w;

	// Token: 0x040002E4 RID: 740
	public int h;

	// Token: 0x040002E5 RID: 741
	public int v;

	// Token: 0x040002E6 RID: 742
	public int x0;

	// Token: 0x040002E7 RID: 743
	public int x;

	// Token: 0x040002E8 RID: 744
	public int y;

	// Token: 0x040002E9 RID: 745
	public int y0;

	// Token: 0x040002EA RID: 746
	public int angle;

	// Token: 0x040002EB RID: 747
	public int t;

	// Token: 0x040002EC RID: 748
	public int cl = 16711680;

	// Token: 0x040002ED RID: 749
	private float a;

	// Token: 0x040002EE RID: 750
	private long last;

	// Token: 0x040002EF RID: 751
	private long delay = 150L;

	// Token: 0x040002F0 RID: 752
	private bool act = true;

	// Token: 0x040002F1 RID: 753
	private int[] arr_x = new int[2];

	// Token: 0x040002F2 RID: 754
	private int[] arr_y = new int[2];
}
