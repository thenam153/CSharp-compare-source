using System;
using System.Collections;

// Token: 0x02000010 RID: 16
public class MyVector
{
	// Token: 0x0600006B RID: 107 RVA: 0x00003938 File Offset: 0x00001B38
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003938 File Offset: 0x00001B38
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000394B File Offset: 0x00001B4B
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000395A File Offset: 0x00001B5A
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003969 File Offset: 0x00001B69
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000397F File Offset: 0x00001B7F
	public int size()
	{
		if (this.a == null)
		{
			return 0;
		}
		return this.a.Count;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003999 File Offset: 0x00001B99
	public object elementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			return this.a[index];
		}
		return null;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000039C1 File Offset: 0x00001BC1
	public void set(int index, object obj)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000039E8 File Offset: 0x00001BE8
	public void setElementAt(object obj, int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003A0F File Offset: 0x00001C0F
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003A1D File Offset: 0x00001C1D
	public void removeElementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003A43 File Offset: 0x00001C43
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003A51 File Offset: 0x00001C51
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003A5E File Offset: 0x00001C5E
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003A6D File Offset: 0x00001C6D
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003A7B File Offset: 0x00001C7B
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000020 RID: 32
	private ArrayList a;
}
