using System;

// Token: 0x0200002D RID: 45
public class ClanImage
{
	// Token: 0x06000209 RID: 521 RVA: 0x000047A6 File Offset: 0x000029A6
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00011658 File Offset: 0x0000F858
	public static ClanImage getClanImage(sbyte ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == (int)ID)
			{
				return clanImage;
			}
		}
		return null;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x000116A4 File Offset: 0x0000F8A4
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == ID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040001E1 RID: 481
	public int ID;

	// Token: 0x040001E2 RID: 482
	public string name;

	// Token: 0x040001E3 RID: 483
	public short[] idImage;

	// Token: 0x040001E4 RID: 484
	public int xu;

	// Token: 0x040001E5 RID: 485
	public int luong;

	// Token: 0x040001E6 RID: 486
	public static MyVector vClanImage = new MyVector();

	// Token: 0x040001E7 RID: 487
	public static MyHashTable idImages = new MyHashTable();
}
