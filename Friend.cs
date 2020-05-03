using System;

// Token: 0x02000053 RID: 83
public class Friend
{
	// Token: 0x060002CB RID: 715 RVA: 0x00004B64 File Offset: 0x00002D64
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00004B7A File Offset: 0x00002D7A
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x040004AF RID: 1199
	public string friendName;

	// Token: 0x040004B0 RID: 1200
	public sbyte type;
}
