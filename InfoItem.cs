using System;

// Token: 0x0200009F RID: 159
public class InfoItem
{
	// Token: 0x060006EC RID: 1772 RVA: 0x0000676F File Offset: 0x0000496F
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00006799 File Offset: 0x00004999
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x04000CEB RID: 3307
	public string s;

	// Token: 0x04000CEC RID: 3308
	private mFont f;

	// Token: 0x04000CED RID: 3309
	public int speed = 70;

	// Token: 0x04000CEE RID: 3310
	public global::Char charInfo;

	// Token: 0x04000CEF RID: 3311
	public bool isChatServer;

	// Token: 0x04000CF0 RID: 3312
	public bool isOnline;

	// Token: 0x04000CF1 RID: 3313
	public int timeCount;

	// Token: 0x04000CF2 RID: 3314
	public int maxTime;

	// Token: 0x04000CF3 RID: 3315
	public long last;

	// Token: 0x04000CF4 RID: 3316
	public long curr;
}
