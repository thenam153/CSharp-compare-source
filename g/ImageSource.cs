using System;

namespace Assets.src.g
{
	// Token: 0x0200009D RID: 157
	internal class ImageSource
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x000066AA File Offset: 0x000048AA
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0005C1C4 File Offset: 0x0005A3C4
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			if (array == null)
			{
				Service.gI().imageSource(myVector);
				return;
			}
			ImageSource.vRms = new MyVector();
			DataInputStream dataInputStream = new DataInputStream(array);
			if (dataInputStream == null)
			{
				return;
			}
			try
			{
				short num = dataInputStream.readShort();
				string[] array2 = new string[(int)num];
				sbyte[] array3 = new sbyte[(int)num];
				for (int i = 0; i < (int)num; i++)
				{
					array2[i] = dataInputStream.readUTF();
					array3[i] = dataInputStream.readByte();
					ImageSource.vRms.addElement(new ImageSource(array2[i], array3[i]));
				}
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			Res.outz(string.Concat(new object[]
			{
				"vS size= ",
				ImageSource.vSource.size(),
				" vRMS size= ",
				ImageSource.vRms.size()
			}));
			for (int j = 0; j < ImageSource.vSource.size(); j++)
			{
				ImageSource imageSource = (ImageSource)ImageSource.vSource.elementAt(j);
				if (!ImageSource.isExistID(imageSource.id))
				{
					myVector.addElement(imageSource);
				}
			}
			for (int k = 0; k < ImageSource.vRms.size(); k++)
			{
				ImageSource imageSource2 = (ImageSource)ImageSource.vRms.elementAt(k);
				if ((int)ImageSource.getVersionRMSByID(imageSource2.id) != (int)ImageSource.getCurrVersionByID(imageSource2.id))
				{
					myVector.addElement(imageSource2);
				}
			}
			Service.gI().imageSource(myVector);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0005C38C File Offset: 0x0005A58C
		public static sbyte getVersionRMSByID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vRms.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		public static sbyte getCurrVersionByID(string id)
		{
			for (int i = 0; i < ImageSource.vSource.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vSource.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vSource.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0005C44C File Offset: 0x0005A64C
		public static bool isExistID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0005C498 File Offset: 0x0005A698
		public static void saveRMS()
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeShort((short)ImageSource.vSource.size());
				for (int i = 0; i < ImageSource.vSource.size(); i++)
				{
					dataOutputStream.writeUTF(((ImageSource)ImageSource.vSource.elementAt(i)).id);
					dataOutputStream.writeByte(((ImageSource)ImageSource.vSource.elementAt(i)).version);
				}
				Rms.saveRMS("ImageSource", dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x04000CE2 RID: 3298
		public sbyte version;

		// Token: 0x04000CE3 RID: 3299
		public string id;

		// Token: 0x04000CE4 RID: 3300
		public static MyVector vSource = new MyVector();

		// Token: 0x04000CE5 RID: 3301
		public static MyVector vRms = new MyVector();
	}
}
