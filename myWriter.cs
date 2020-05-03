using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class myWriter
{
	// Token: 0x060001EA RID: 490 RVA: 0x000111E4 File Offset: 0x0000F3E4
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00011214 File Offset: 0x0000F414
	public void writeSByteUncheck(sbyte value)
	{
		this.buffer[this.posWrite++] = value;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00004709 File Offset: 0x00002909
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00004712 File Offset: 0x00002912
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000471C File Offset: 0x0000291C
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00004712 File Offset: 0x00002912
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0001123C File Offset: 0x0000F43C
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00011270 File Offset: 0x0000F470
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x000112A4 File Offset: 0x0000F4A4
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000112DC File Offset: 0x0000F4DC
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000112A4 File Offset: 0x0000F4A4
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00011314 File Offset: 0x0000F514
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0001134C File Offset: 0x0000F54C
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000472D File Offset: 0x0000292D
	public void writeBoolean(bool value)
	{
		this.writeSByte((!value) ? 0 : 1);
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0000472D File Offset: 0x0000292D
	public void writeBool(bool value)
	{
		this.writeSByte((!value) ? 0 : 1);
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00011384 File Offset: 0x0000F584
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x000113CC File Offset: 0x0000F5CC
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		Encoding encoding = Encoding.GetEncoding(65001);
		byte[] bytes = unicode.GetBytes(value);
		byte[] array = Encoding.Convert(unicode, encoding, bytes);
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		foreach (sbyte value2 in array)
		{
			this.writeSByteUncheck(value2);
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00004709 File Offset: 0x00002909
	public void write(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00011438 File Offset: 0x0000F638
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			this.writeSByte(data[i + arg1]);
			if (this.posWrite > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00004742 File Offset: 0x00002942
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00011480 File Offset: 0x0000F680
	public sbyte[] getData()
	{
		if (this.posWrite <= 0)
		{
			return null;
		}
		sbyte[] array = new sbyte[this.posWrite];
		for (int i = 0; i < this.posWrite; i++)
		{
			array[i] = this.buffer[i];
		}
		return array;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000114CC File Offset: 0x0000F6CC
	public void checkLenght(int ltemp)
	{
		if (this.posWrite + ltemp > this.lenght)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00011544 File Offset: 0x0000F744
	private static void convertString(string[] args)
	{
		string path = args[0];
		string path2 = args[1];
		using (StreamReader streamReader = new StreamReader(path, Encoding.Unicode))
		{
			using (StreamWriter streamWriter = new StreamWriter(path2, false, Encoding.UTF8))
			{
				myWriter.CopyContents(streamReader, streamWriter);
			}
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000115BC File Offset: 0x0000F7BC
	private static void CopyContents(TextReader input, TextWriter output)
	{
		char[] array = new char[8192];
		int count;
		while ((count = input.Read(array, 0, array.Length)) != 0)
		{
			output.Write(array, 0, count);
		}
		output.Flush();
		string message = output.ToString();
		Debug.Log(message);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000474B File Offset: 0x0000294B
	public byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00011608 File Offset: 0x0000F808
	public byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00004761 File Offset: 0x00002961
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00004761 File Offset: 0x00002961
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x040001D3 RID: 467
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x040001D4 RID: 468
	private int posWrite;

	// Token: 0x040001D5 RID: 469
	private int lenght = 2048;
}
