using System;

// Token: 0x02000002 RID: 2
public class ArrayCast
{
	// Token: 0x06000002 RID: 2 RVA: 0x00007854 File Offset: 0x00005A54
	public static sbyte[] cast(byte[] data)
	{
		sbyte[] array = new sbyte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)data[i];
		}
		return array;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00007888 File Offset: 0x00005A88
	public static byte[] cast(sbyte[] data)
	{
		byte[] array = new byte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)data[i];
		}
		return array;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000078BC File Offset: 0x00005ABC
	public static char[] ToCharArray(sbyte[] data)
	{
		char[] array = new char[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (char)data[i];
		}
		return array;
	}
}
