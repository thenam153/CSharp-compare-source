using System;

// Token: 0x0200003C RID: 60
public class FireWorkMn
{
	// Token: 0x06000273 RID: 627 RVA: 0x0001363C File Offset: 0x0001183C
	public FireWorkMn(int x, int y, int goc, int n)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
		this.n = n;
		for (int i = 0; i < n; i++)
		{
			this.fw.addElement(new Firework(x, y, global::Math.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[global::Math.abs(this.rd.nextInt() % this.color.Length)]));
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00013708 File Offset: 0x00011908
	public void paint(mGraphics g)
	{
		for (int i = 0; i < this.fw.size(); i++)
		{
			Firework firework = (Firework)this.fw.elementAt(i);
			if (firework.y < -200)
			{
				this.fw.removeElementAt(i);
			}
			firework.paint(g);
		}
	}

	// Token: 0x040002DC RID: 732
	private int x;

	// Token: 0x040002DD RID: 733
	private int y;

	// Token: 0x040002DE RID: 734
	private int goc = 1;

	// Token: 0x040002DF RID: 735
	private int n = 360;

	// Token: 0x040002E0 RID: 736
	private MyRandom rd = new MyRandom();

	// Token: 0x040002E1 RID: 737
	private MyVector fw = new MyVector();

	// Token: 0x040002E2 RID: 738
	private int[] color = new int[]
	{
		16711680,
		16776960,
		65280,
		16777215,
		255,
		65535,
		15790320,
		12632256
	};
}
