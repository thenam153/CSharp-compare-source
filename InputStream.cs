using System;

// Token: 0x02000009 RID: 9
public class InputStream : myReader
{
	// Token: 0x0600004E RID: 78 RVA: 0x000037D5 File Offset: 0x000019D5
	public InputStream()
	{
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000037DD File Offset: 0x000019DD
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000037EC File Offset: 0x000019EC
	public InputStream(string filename) : base(filename)
	{
	}
}
