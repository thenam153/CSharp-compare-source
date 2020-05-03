using System;

// Token: 0x02000051 RID: 81
public class EffectPaint
{
	// Token: 0x060002C9 RID: 713 RVA: 0x00004B4B File Offset: 0x00002D4B
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x040004A6 RID: 1190
	public int index;

	// Token: 0x040004A7 RID: 1191
	public Mob eMob;

	// Token: 0x040004A8 RID: 1192
	public global::Char eChar;

	// Token: 0x040004A9 RID: 1193
	public EffectCharPaint effCharPaint;

	// Token: 0x040004AA RID: 1194
	public bool isFly;
}
