using System;

// Token: 0x02000049 RID: 73
public class BgItemMn
{
	// Token: 0x060002B1 RID: 689 RVA: 0x000154E0 File Offset: 0x000136E0
	public static Image blendImage(Image img, int layer, int idImage)
	{
		int num = TileMap.tileID - 1;
		Image image = img;
		if (num == 0 && layer == 1)
		{
			image = mGraphics.blend(img, 0.3f, 807956);
		}
		if (num == 1 && layer == 1)
		{
			image = mGraphics.blend(img, 0.35f, 739339);
		}
		if (num == 2 && layer == 1)
		{
			image = mGraphics.blend(img, 0.1f, 3977975);
		}
		if (num == 3)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.2f, 15265992);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.1f, 15265992);
			}
		}
		if (num == 4)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 1330178);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.1f, 1330178);
			}
		}
		if (num == 6)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 420382);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 420382);
			}
		}
		if (num == 5)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.35f, 3270903);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 3270903);
			}
		}
		if (num == 8)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 7094528);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 7094528);
			}
		}
		if (num == 9)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 12113627);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 12113627);
			}
		}
		if (num == 10 && layer == 1)
		{
			image = mGraphics.blend(img, 0.3f, 14938312);
		}
		if (num == 10 && layer == 1)
		{
			image = mGraphics.blend(img, 0.2f, 14938312);
		}
		if (num == 11)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		if (num > 11)
		{
			if (layer == 1 || layer == 2)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		byte[] byteArray = BgItemMn.getByteArray(image);
		Rms.saveRMS(string.Concat(new object[]
		{
			"x",
			mGraphics.zoomLevel,
			"blend",
			idImage,
			"layer",
			layer
		}), ArrayCast.cast(byteArray));
		return image;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0001579C File Offset: 0x0001399C
	public static byte[] getByteArray(Image img)
	{
		byte[] result;
		try
		{
			byte[] array = img.texture.EncodeToPNG();
			result = array;
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x000157E0 File Offset: 0x000139E0
	public static void blendcurrBg(short id, Image img)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.idImage == id && !bgItem.isNotBlend() && (int)bgItem.layer != 2 && (int)bgItem.layer != 4 && !BgItem.imgNew.containsKey(bgItem.idImage + "blend" + bgItem.layer))
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"blend",
					id,
					"layer",
					bgItem.layer
				}));
				if (array == null)
				{
					BgItem.imgNew.put(bgItem.idImage + "blend" + bgItem.layer, BgItemMn.blendImage(img, (int)bgItem.layer, (int)bgItem.idImage));
				}
				else
				{
					Image v = Image.createImage(array, 0, array.Length);
					BgItem.imgNew.put(bgItem.idImage + "blend" + bgItem.layer, v);
				}
			}
		}
	}
}
