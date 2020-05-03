using System;

// Token: 0x02000047 RID: 71
public class Arrowpaint
{
	// Token: 0x060002A3 RID: 675 RVA: 0x00014814 File Offset: 0x00012A14
	public void update()
	{
		if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
		{
			this.endMe();
		}
		else
		{
			if (this.charBelong.mobFocus != null)
			{
				this.axTo = this.charBelong.mobFocus.x;
				this.ayTo = this.charBelong.mobFocus.y - this.charBelong.mobFocus.h / 4;
			}
			else if (this.charBelong.charFocus != null)
			{
				this.axTo = this.charBelong.charFocus.cx;
				this.ayTo = this.charBelong.charFocus.cy - this.charBelong.charFocus.ch / 4;
			}
			int num = this.axTo - this.ax;
			int num2 = this.ayTo - this.ay;
			int num3 = 5;
			int num4 = 4;
			if (num + num2 < 60)
			{
				num4 = 3;
			}
			else if (num + num2 < 30)
			{
				num4 = 2;
			}
			if (this.ax != this.axTo)
			{
				if (num > 0 && num < num3)
				{
					this.ax = this.axTo;
				}
				else if (num < 0 && num > -num3)
				{
					this.ax = this.axTo;
				}
				else
				{
					this.avx = this.axTo - this.ax << 2;
					this.adx += this.avx;
					this.ax += this.adx >> num4;
					this.adx &= 15;
				}
			}
			if (this.ay != this.ayTo)
			{
				if (num2 > 0 && num2 < num3)
				{
					this.ay = this.ayTo;
				}
				else if (num2 < 0 && num2 > -num3)
				{
					this.ay = this.ayTo;
				}
				else
				{
					this.avy = this.ayTo - this.ay << 2;
					this.ady += this.avy;
					this.ay += this.ady >> num4;
					this.ady &= 15;
				}
			}
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			if (this.charBelong.mobFocus != null)
			{
				num5 = this.axTo - this.charBelong.mobFocus.w / 4;
				num7 = this.axTo + this.charBelong.mobFocus.w / 4;
				num6 = this.ayTo - this.charBelong.mobFocus.h / 4;
				num8 = this.ayTo + this.charBelong.mobFocus.h / 4;
			}
			else if (this.charBelong.charFocus != null)
			{
				num5 = this.axTo - this.charBelong.charFocus.cw / 4;
				num7 = this.axTo + this.charBelong.charFocus.cw / 4;
				num6 = this.ayTo - this.charBelong.charFocus.ch / 4;
				num8 = this.ayTo + this.charBelong.charFocus.ch / 4;
			}
			if (this.life > 0)
			{
				this.life--;
			}
			if (this.life == 0 || (this.ax >= num5 && this.ax <= num7 && this.ay >= num6 && this.ay <= num8))
			{
				this.endMe();
			}
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x00014BD8 File Offset: 0x00012DD8
	private void endMe()
	{
		this.charBelong.arr = null;
		this.ax = (this.ay = (this.axTo = (this.ayTo = (this.avx = (this.avy = (this.adx = (this.ady = 0)))))));
		this.charBelong.setAttack();
		if (this.charBelong.me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x00014C60 File Offset: 0x00012E60
	public void paint(mGraphics g)
	{
		int dx = this.axTo - this.ax;
		int num = this.ayTo - this.ay;
		int num2 = Arrowpaint.findDirIndexFromAngle(Res.angle(dx, -num));
		SmallImage.drawSmallImage(g, this.imgId[(int)Arrowpaint.FRAME[num2]], this.ax, this.ay, Arrowpaint.TRANSFORM[num2], mGraphics.VCENTER | mGraphics.HCENTER);
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x00014CCC File Offset: 0x00012ECC
	public static int findDirIndexFromAngle(int angle)
	{
		int i = 0;
		while (i < Arrowpaint.ARROWINDEX.Length - 1)
		{
			if (angle >= Arrowpaint.ARROWINDEX[i] && angle <= Arrowpaint.ARROWINDEX[i + 1])
			{
				if (i >= 16)
				{
					return 0;
				}
				return i;
			}
			else
			{
				i++;
			}
		}
		return 0;
	}

	// Token: 0x04000335 RID: 821
	public int id;

	// Token: 0x04000336 RID: 822
	public int life;

	// Token: 0x04000337 RID: 823
	public int ax;

	// Token: 0x04000338 RID: 824
	public int ay;

	// Token: 0x04000339 RID: 825
	public int axTo;

	// Token: 0x0400033A RID: 826
	public int ayTo;

	// Token: 0x0400033B RID: 827
	public int avx;

	// Token: 0x0400033C RID: 828
	public int avy;

	// Token: 0x0400033D RID: 829
	public int adx;

	// Token: 0x0400033E RID: 830
	public int ady;

	// Token: 0x0400033F RID: 831
	public global::Char charBelong;

	// Token: 0x04000340 RID: 832
	public int[] imgId = new int[3];

	// Token: 0x04000341 RID: 833
	public static sbyte[] FRAME = new sbyte[]
	{
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0
	};

	// Token: 0x04000342 RID: 834
	public static int[] ARROWINDEX = new int[]
	{
		0,
		15,
		37,
		52,
		75,
		105,
		127,
		142,
		165,
		195,
		217,
		232,
		255,
		285,
		307,
		322,
		345,
		370
	};

	// Token: 0x04000343 RID: 835
	public static int[] TRANSFORM = new int[]
	{
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		2,
		2,
		3,
		3,
		4,
		5,
		5,
		5,
		1
	};
}
