using System;

// Token: 0x02000076 RID: 118
public class SkillOption
{
	// Token: 0x060003C7 RID: 967 RVA: 0x0001DEAC File Offset: 0x0001C0AC
	public string getOptionString()
	{
		if (this.optionString == null)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param);
		}
		return this.optionString;
	}

	// Token: 0x0400068D RID: 1677
	public int param;

	// Token: 0x0400068E RID: 1678
	public SkillOptionTemplate optionTemplate;

	// Token: 0x0400068F RID: 1679
	public string optionString;
}
