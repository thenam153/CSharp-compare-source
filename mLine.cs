using System;

// Token: 0x02000029 RID: 41
public class mLine
{
	// Token: 0x060001CB RID: 459 RVA: 0x00004623 File Offset: 0x00002823
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00010E98 File Offset: 0x0000F098
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x040001C6 RID: 454
	public int x1;

	// Token: 0x040001C7 RID: 455
	public int x2;

	// Token: 0x040001C8 RID: 456
	public int y1;

	// Token: 0x040001C9 RID: 457
	public int y2;

	// Token: 0x040001CA RID: 458
	public float r;

	// Token: 0x040001CB RID: 459
	public float b;

	// Token: 0x040001CC RID: 460
	public float g;

	// Token: 0x040001CD RID: 461
	public float a;
}
