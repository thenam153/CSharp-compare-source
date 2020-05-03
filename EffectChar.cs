using System;

// Token: 0x0200004E RID: 78
public class EffectChar
{
	// Token: 0x060002C4 RID: 708 RVA: 0x00004B11 File Offset: 0x00002D11
	public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x0400049A RID: 1178
	public static EffectTemplate[] effTemplates;

	// Token: 0x0400049B RID: 1179
	public static sbyte EFF_ME;

	// Token: 0x0400049C RID: 1180
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x0400049D RID: 1181
	public int timeStart;

	// Token: 0x0400049E RID: 1182
	public int timeLenght;

	// Token: 0x0400049F RID: 1183
	public short param;

	// Token: 0x040004A0 RID: 1184
	public EffectTemplate template;
}
