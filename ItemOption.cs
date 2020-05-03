using System;

// Token: 0x0200005B RID: 91
public class ItemOption
{
	// Token: 0x0600032E RID: 814 RVA: 0x0000357C File Offset: 0x0000177C
	public ItemOption()
	{
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00004E74 File Offset: 0x00003074
	public ItemOption(int optionTemplateId, int param)
	{
		this.param = param;
		this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00004E95 File Offset: 0x00003095
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00004EC1 File Offset: 0x000030C1
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00004EDD File Offset: 0x000030DD
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00018B50 File Offset: 0x00016D50
	public string getOptionShopstring()
	{
		string result = string.Empty;
		if (this.optionTemplate.id == 0 || this.optionTemplate.id == 1 || this.optionTemplate.id == 21 || this.optionTemplate.id == 22 || this.optionTemplate.id == 23 || this.optionTemplate.id == 24 || this.optionTemplate.id == 25 || this.optionTemplate.id == 26)
		{
			int num = this.param - 50 + 1;
			result = string.Concat(new object[]
			{
				NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty),
				" (",
				num,
				"-",
				this.param,
				")"
			});
		}
		else if (this.optionTemplate.id == 6 || this.optionTemplate.id == 7 || this.optionTemplate.id == 8 || this.optionTemplate.id == 9 || this.optionTemplate.id == 19)
		{
			int num = this.param - 10 + 1;
			result = string.Concat(new object[]
			{
				NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty),
				" (",
				num,
				"-",
				this.param,
				")"
			});
		}
		else if (this.optionTemplate.id == 2 || this.optionTemplate.id == 3 || this.optionTemplate.id == 4 || this.optionTemplate.id == 5 || this.optionTemplate.id == 10 || this.optionTemplate.id == 11 || this.optionTemplate.id == 12 || this.optionTemplate.id == 13 || this.optionTemplate.id == 14 || this.optionTemplate.id == 15 || this.optionTemplate.id == 17 || this.optionTemplate.id == 18 || this.optionTemplate.id == 20)
		{
			int num = this.param - 5 + 1;
			result = string.Concat(new object[]
			{
				NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty),
				" (",
				num,
				"-",
				this.param,
				")"
			});
		}
		else if (this.optionTemplate.id == 16)
		{
			int num = this.param - 3 + 1;
			result = string.Concat(new object[]
			{
				NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty),
				" (",
				num,
				"-",
				this.param,
				")"
			});
		}
		else
		{
			result = NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty);
		}
		return result;
	}

	// Token: 0x0400054F RID: 1359
	public int param;

	// Token: 0x04000550 RID: 1360
	public sbyte active;

	// Token: 0x04000551 RID: 1361
	public ItemOptionTemplate optionTemplate;
}
