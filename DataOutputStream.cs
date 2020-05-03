using System;

// Token: 0x02000005 RID: 5
public class DataOutputStream
{
	// Token: 0x06000022 RID: 34 RVA: 0x00003683 File Offset: 0x00001883
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00003691 File Offset: 0x00001891
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000369F File Offset: 0x0000189F
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000036AD File Offset: 0x000018AD
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000036BA File Offset: 0x000018BA
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000036C7 File Offset: 0x000018C7
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000036D5 File Offset: 0x000018D5
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000036E3 File Offset: 0x000018E3
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x04000008 RID: 8
	private myWriter w = new myWriter();
}
