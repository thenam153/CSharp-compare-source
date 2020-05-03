using System;

// Token: 0x02000079 RID: 121
public class SkillTemplate
{
	// Token: 0x060003CB RID: 971 RVA: 0x0000552A File Offset: 0x0000372A
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0000553B File Offset: 0x0000373B
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0000554C File Offset: 0x0000374C
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x04000697 RID: 1687
	public sbyte id;

	// Token: 0x04000698 RID: 1688
	public int classId;

	// Token: 0x04000699 RID: 1689
	public string name;

	// Token: 0x0400069A RID: 1690
	public int maxPoint;

	// Token: 0x0400069B RID: 1691
	public int manaUseType;

	// Token: 0x0400069C RID: 1692
	public int type;

	// Token: 0x0400069D RID: 1693
	public int iconId;

	// Token: 0x0400069E RID: 1694
	public string[] description;

	// Token: 0x0400069F RID: 1695
	public Skill[] skills;

	// Token: 0x040006A0 RID: 1696
	public string damInfo;
}
