using System;

// Token: 0x02000020 RID: 32
public class Message
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000415D File Offset: 0x0000235D
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00004178 File Offset: 0x00002378
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000418B File Offset: 0x0000238B
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000041A5 File Offset: 0x000023A5
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000041C0 File Offset: 0x000023C0
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000041CD File Offset: 0x000023CD
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x000041D5 File Offset: 0x000023D5
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000041DD File Offset: 0x000023DD
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00003584 File Offset: 0x00001784
	public void cleanup()
	{
	}

	// Token: 0x04000114 RID: 276
	public sbyte command;

	// Token: 0x04000115 RID: 277
	private myReader dis;

	// Token: 0x04000116 RID: 278
	private myWriter dos;
}
