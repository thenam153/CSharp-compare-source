using System;

// Token: 0x0200006E RID: 110
public class PlayerInfo
{
	// Token: 0x06000398 RID: 920 RVA: 0x0000532B File Offset: 0x0000352B
	public string getName()
	{
		return this.name;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00005333 File Offset: 0x00003533
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0000534D File Offset: 0x0000354D
	public void setName(string name)
	{
		this.name = name;
		if (name.Length > 9)
		{
			this.showName = name.Substring(0, 8);
		}
		else
		{
			this.showName = name;
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00003584 File Offset: 0x00001784
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0000537D File Offset: 0x0000357D
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x04000612 RID: 1554
	public string name;

	// Token: 0x04000613 RID: 1555
	public string showName;

	// Token: 0x04000614 RID: 1556
	public string status;

	// Token: 0x04000615 RID: 1557
	public int IDDB;

	// Token: 0x04000616 RID: 1558
	private int exp;

	// Token: 0x04000617 RID: 1559
	public bool isReady;

	// Token: 0x04000618 RID: 1560
	public int xu;

	// Token: 0x04000619 RID: 1561
	public int gold;

	// Token: 0x0400061A RID: 1562
	public string strMoney = string.Empty;

	// Token: 0x0400061B RID: 1563
	public sbyte finishPosition;

	// Token: 0x0400061C RID: 1564
	public bool isMaster;

	// Token: 0x0400061D RID: 1565
	public static Image[] imgStart;

	// Token: 0x0400061E RID: 1566
	public sbyte[] indexLv;

	// Token: 0x0400061F RID: 1567
	public int onlineTime;
}
