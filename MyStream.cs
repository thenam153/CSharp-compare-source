using System;

// Token: 0x0200000F RID: 15
public class MyStream
{
	// Token: 0x0600006A RID: 106 RVA: 0x0000879C File Offset: 0x0000699C
	public static DataInputStream readFile(string path)
	{
		path = Main.res + path;
		DataInputStream result;
		try
		{
			result = DataInputStream.getResourceAsStream(path);
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}
}
