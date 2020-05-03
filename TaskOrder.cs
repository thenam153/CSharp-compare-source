using System;

// Token: 0x02000086 RID: 134
public class TaskOrder
{
	// Token: 0x06000421 RID: 1057 RVA: 0x0000597B File Offset: 0x00003B7B
	public TaskOrder(sbyte taskId, short count, short maxCount, string name, string description, sbyte killId, sbyte mapId)
	{
		this.count = (int)count;
		this.maxCount = maxCount;
		this.taskId = (int)taskId;
		this.name = name;
		this.description = description;
		this.killId = (int)killId;
		this.mapId = (int)mapId;
	}

	// Token: 0x040006E3 RID: 1763
	public const sbyte TASK_DAY = 0;

	// Token: 0x040006E4 RID: 1764
	public const sbyte TASK_BOSS = 1;

	// Token: 0x040006E5 RID: 1765
	public int taskId;

	// Token: 0x040006E6 RID: 1766
	public int count;

	// Token: 0x040006E7 RID: 1767
	public short maxCount;

	// Token: 0x040006E8 RID: 1768
	public string name;

	// Token: 0x040006E9 RID: 1769
	public string description;

	// Token: 0x040006EA RID: 1770
	public int killId;

	// Token: 0x040006EB RID: 1771
	public int mapId;
}
