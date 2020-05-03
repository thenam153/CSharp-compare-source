using System;

// Token: 0x02000071 RID: 113
public class Position
{
	// Token: 0x060003B0 RID: 944 RVA: 0x00005409 File Offset: 0x00003609
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x0000541F File Offset: 0x0000361F
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0000543C File Offset: 0x0000363C
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00005452 File Offset: 0x00003652
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001D034 File Offset: 0x0001B234
	public int translate()
	{
		if (this.x == (int)this.xTo && this.y == (int)this.yTo)
		{
			return -1;
		}
		if (global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1)
		{
			this.x = (int)this.xTo;
			this.y = (int)this.yTo;
			return 0;
		}
		if (this.x != (int)this.xTo)
		{
			this.x += ((int)this.xTo - this.x) / 2;
		}
		if (this.y != (int)this.yTo)
		{
			this.y += ((int)this.yTo - this.y) / 2;
		}
		if (Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5))
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00005488 File Offset: 0x00003688
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00005495 File Offset: 0x00003695
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000640 RID: 1600
	public int x;

	// Token: 0x04000641 RID: 1601
	public int y;

	// Token: 0x04000642 RID: 1602
	public int anchor;

	// Token: 0x04000643 RID: 1603
	public int g;

	// Token: 0x04000644 RID: 1604
	public int v;

	// Token: 0x04000645 RID: 1605
	public int w;

	// Token: 0x04000646 RID: 1606
	public int h;

	// Token: 0x04000647 RID: 1607
	public int color;

	// Token: 0x04000648 RID: 1608
	public int limitY;

	// Token: 0x04000649 RID: 1609
	public Layer layer;

	// Token: 0x0400064A RID: 1610
	public short yTo;

	// Token: 0x0400064B RID: 1611
	public short xTo;

	// Token: 0x0400064C RID: 1612
	public short distant;
}
