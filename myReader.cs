using System;
using System.Text;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class myReader
{
	// Token: 0x060001CD RID: 461 RVA: 0x0000357C File Offset: 0x0000177C
	public myReader()
	{
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00004650 File Offset: 0x00002850
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00010EF8 File Offset: 0x0000F0F8
	public myReader(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.buffer = mSystem.convertToSbyte(textAsset.bytes);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00010F34 File Offset: 0x0000F134
	public sbyte readSByte()
	{
		if (this.posRead < this.buffer.Length)
		{
			return this.buffer[this.posRead++];
		}
		this.posRead = this.buffer.Length;
		throw new Exception(" loi doc sbyte eof ");
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000465F File Offset: 0x0000285F
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000465F File Offset: 0x0000285F
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00004667 File Offset: 0x00002867
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00004675 File Offset: 0x00002875
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00004683 File Offset: 0x00002883
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00010F88 File Offset: 0x0000F188
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (short)(num << 8);
			num |= (short)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00010FD4 File Offset: 0x0000F1D4
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (ushort)(num << 8);
			num |= (ushort)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00011020 File Offset: 0x0000F220
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num <<= 8;
			num |= (255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00011068 File Offset: 0x0000F268
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			num <<= 8;
			num |= (long)(255 & (int)this.buffer[this.posRead++]);
		}
		return num;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00004690 File Offset: 0x00002890
	public bool readBool()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00004690 File Offset: 0x00002890
	public bool readBoolean()
	{
		return (int)this.readSByte() > 0;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x000110B4 File Offset: 0x0000F2B4
	public string readString()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060001DE RID: 478 RVA: 0x000110B4 File Offset: 0x0000F2B4
	public string readStringUTF()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060001DF RID: 479 RVA: 0x000046A6 File Offset: 0x000028A6
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x000046AE File Offset: 0x000028AE
	public int read()
	{
		if (this.posRead < this.buffer.Length)
		{
			return (int)this.readSByte();
		}
		return -1;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x000110FC File Offset: 0x0000F2FC
	public int read(ref sbyte[] data)
	{
		if (data == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return -1;
			}
			num++;
		}
		return num;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00011150 File Offset: 0x0000F350
	public void readFully(ref sbyte[] data)
	{
		if (data == null || data.Length + this.posRead > this.buffer.Length)
		{
			return;
		}
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000046CC File Offset: 0x000028CC
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00003740 File Offset: 0x00001940
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00007ABC File Offset: 0x00005CBC
	public static byte[] convertSbyteToByte(sbyte[] var)
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

	// Token: 0x060001E6 RID: 486 RVA: 0x000046DD File Offset: 0x000028DD
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x000046DD File Offset: 0x000028DD
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0001119C File Offset: 0x0000F39C
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			data[i + arg1] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return;
			}
		}
	}

	// Token: 0x040001CE RID: 462
	public sbyte[] buffer;

	// Token: 0x040001CF RID: 463
	private int posRead;

	// Token: 0x040001D0 RID: 464
	private int posMark;

	// Token: 0x040001D1 RID: 465
	private static string fileName;

	// Token: 0x040001D2 RID: 466
	private static int status;
}
