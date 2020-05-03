using System;

// Token: 0x02000085 RID: 133
public class Task
{
	// Token: 0x06000420 RID: 1056 RVA: 0x00022EAC File Offset: 0x000210AC
	public Task(short taskId, sbyte index, string name, string detail, string[] subNames, short[] counts, short count, string[] contentInfo)
	{
		this.taskId = taskId;
		this.index = (int)index;
		this.names = mFont.tahoma_7b_green2.splitFontArray(name, Panel.WIDTH_PANEL - 20);
		this.details = mFont.tahoma_7.splitFontArray(detail, Panel.WIDTH_PANEL - 20);
		this.subNames = subNames;
		this.counts = counts;
		this.count = count;
		this.contentInfo = contentInfo;
	}

	// Token: 0x040006DA RID: 1754
	public int index;

	// Token: 0x040006DB RID: 1755
	public int max;

	// Token: 0x040006DC RID: 1756
	public short[] counts;

	// Token: 0x040006DD RID: 1757
	public short taskId;

	// Token: 0x040006DE RID: 1758
	public string[] names;

	// Token: 0x040006DF RID: 1759
	public string[] details;

	// Token: 0x040006E0 RID: 1760
	public string[] subNames;

	// Token: 0x040006E1 RID: 1761
	public string[] contentInfo;

	// Token: 0x040006E2 RID: 1762
	public short count;
}
