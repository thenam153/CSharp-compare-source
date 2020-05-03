using System;

// Token: 0x0200006B RID: 107
public class Part
{
	// Token: 0x06000390 RID: 912 RVA: 0x0001BA40 File Offset: 0x00019C40
	public Part(int type)
	{
		this.type = type;
		if (type == 0)
		{
			this.pi = new PartImage[3];
		}
		if (type == 1)
		{
			this.pi = new PartImage[17];
		}
		if (type == 2)
		{
			this.pi = new PartImage[14];
		}
		if (type == 3)
		{
			this.pi = new PartImage[2];
		}
	}

	// Token: 0x040005FD RID: 1533
	public int type;

	// Token: 0x040005FE RID: 1534
	public PartImage[] pi;
}
