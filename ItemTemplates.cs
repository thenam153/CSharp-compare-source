using System;

// Token: 0x0200005E RID: 94
public class ItemTemplates
{
	// Token: 0x06000338 RID: 824 RVA: 0x00004F05 File Offset: 0x00003105
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00004F1D File Offset: 0x0000311D
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00004F34 File Offset: 0x00003134
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00004F41 File Offset: 0x00003141
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x04000562 RID: 1378
	public static MyHashTable itemTemplates = new MyHashTable();
}
