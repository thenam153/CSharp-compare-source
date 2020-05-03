using System;

// Token: 0x0200005D RID: 93
public class ItemTemplate
{
	// Token: 0x06000335 RID: 821 RVA: 0x00018F50 File Offset: 0x00017150
	public ItemTemplate(short templateID, sbyte type, sbyte gender, string name, string description, sbyte level, int strRequire, short iconID, short part, bool isUpToUp)
	{
		this.id = templateID;
		this.type = type;
		this.gender = gender;
		this.name = name;
		this.name = Res.changeString(this.name);
		this.description = description;
		this.description = Res.changeString(this.description);
		this.level = level;
		this.strRequire = strRequire;
		this.iconID = iconID;
		this.part = part;
		this.isUpToUp = isUpToUp;
	}

	// Token: 0x04000555 RID: 1365
	public short id;

	// Token: 0x04000556 RID: 1366
	public sbyte type;

	// Token: 0x04000557 RID: 1367
	public sbyte gender;

	// Token: 0x04000558 RID: 1368
	public string name;

	// Token: 0x04000559 RID: 1369
	public string[] subName;

	// Token: 0x0400055A RID: 1370
	public string description;

	// Token: 0x0400055B RID: 1371
	public sbyte level;

	// Token: 0x0400055C RID: 1372
	public short iconID;

	// Token: 0x0400055D RID: 1373
	public short part;

	// Token: 0x0400055E RID: 1374
	public bool isUpToUp;

	// Token: 0x0400055F RID: 1375
	public int w;

	// Token: 0x04000560 RID: 1376
	public int h;

	// Token: 0x04000561 RID: 1377
	public int strRequire;
}
