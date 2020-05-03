using System;
using System.Collections;

// Token: 0x0200000C RID: 12
public class MyHashTable
{
	// Token: 0x0600005A RID: 90 RVA: 0x0000387E File Offset: 0x00001A7E
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000388C File Offset: 0x00001A8C
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00003899 File Offset: 0x00001A99
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000038A6 File Offset: 0x00001AA6
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000038B3 File Offset: 0x00001AB3
	public void put(object k, object v)
	{
		if (this.h.ContainsKey(k))
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000038DF File Offset: 0x00001ADF
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000038DF File Offset: 0x00001ADF
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000038ED File Offset: 0x00001AED
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x0400001D RID: 29
	public Hashtable h = new Hashtable();
}
