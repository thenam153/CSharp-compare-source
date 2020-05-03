using System;

// Token: 0x020000B4 RID: 180
public class TileMap
{
	// Token: 0x060008B3 RID: 2227 RVA: 0x000071EA File Offset: 0x000053EA
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		if (mGraphics.zoomLevel == 1 || Main.isIpod || Main.isIphone4)
		{
			return;
		}
		TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0000722A File Offset: 0x0000542A
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0007EAE0 File Offset: 0x0007CCE0
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			if (bgItem.id == id)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0007EB28 File Offset: 0x0007CD28
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.offlineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0007EB64 File Offset: 0x0007CD64
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.highterId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0007EBA0 File Offset: 0x0007CDA0
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.toOfflineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00007253 File Offset: 0x00005453
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00003584 File Offset: 0x00001784
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0007EBDC File Offset: 0x0007CDDC
	public static bool isExistMoreOne(int id)
	{
		if (id == 156 || id == 330 || id == 345 || id == 334)
		{
			return false;
		}
		if (TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103)
		{
			return false;
		}
		int num = 0;
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.id == id)
			{
				num++;
			}
		}
		return num > 2;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0007ECB8 File Offset: 0x0007CEB8
	public static void loadTileImage()
	{
		if (TileMap.imgWaterfall == null)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		if (TileMap.imgTopWaterfall == null)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		if (TileMap.imgWaterflow == null)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		if (TileMap.imgWaterlowN == null)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		if (TileMap.imgWaterlowN2 == null)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
		mSystem.gcc();
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0007ED48 File Offset: 0x0007CF48
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			if (TileMap.maps[index] == mapsArr[i])
			{
				TileMap.types[index] |= type;
				return;
			}
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0007ED8C File Offset: 0x0007CF8C
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID);
		int num = tileId - 1;
		try
		{
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				for (int j = 0; j < TileMap.tileType[num].Length; j++)
				{
					TileMap.setTile(i, TileMap.tileIndex[num][j], TileMap.tileType[num][j]);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Error Load Map");
			GameMidlet.instance.exit();
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00007260 File Offset: 0x00005460
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0007EE58 File Offset: 0x0007D058
	public static bool isDoubleMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0007EF3C File Offset: 0x0007D13C
	public static void getTile()
	{
		if (Main.typeClient == 3 || Main.typeClient == 5)
		{
			if (mGraphics.zoomLevel == 1)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID + ".png");
			}
			else
			{
				TileMap.imgTile = new Image[100];
				for (int i = 0; i < TileMap.imgTile.Length; i++)
				{
					TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/",
						i + 1,
						".png"
					}));
				}
			}
		}
		else
		{
			if (mGraphics.zoomLevel == 1)
			{
				if (TileMap.imgTile != null)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						if (TileMap.imgTile[j] != null)
						{
							TileMap.imgTile[j].texture = null;
							TileMap.imgTile[j] = null;
						}
					}
					mSystem.gcc();
				}
				TileMap.imgTile = new Image[100];
				string path = string.Empty;
				for (int k = 0; k < TileMap.imgTile.Length; k++)
				{
					if (k < 9)
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_0",
							k + 1
						});
					}
					else
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_",
							k + 1
						});
					}
					TileMap.imgTile[k] = GameCanvas.loadImage(path);
				}
				return;
			}
			Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + "$1.png");
			if (image != null)
			{
				Rms.DeleteStorage(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"t",
					TileMap.tileID
				}));
				TileMap.imgTile = new Image[100];
				for (int l = 0; l < TileMap.imgTile.Length; l++)
				{
					TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"$",
						l + 1,
						".png"
					}));
				}
			}
			else
			{
				image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + ".png");
				if (image != null)
				{
					Rms.DeleteStorage("$");
					TileMap.imgTile = new Image[1];
					TileMap.imgTile[0] = image;
				}
			}
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0007F22C File Offset: 0x0007D42C
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0007F2A8 File Offset: 0x0007D4A8
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], x, y, 0);
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0007F2FC File Offset: 0x0007D4FC
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				if (num != -1)
				{
					TileMap.paintTile(g, num, i, j);
				}
				if ((TileMap.tileTypeAt(i, j) & 32) == 32)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				if ((TileMap.tileTypeAt(i, j) & 2048) == 2048)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0007F510 File Offset: 0x0007D710
	public static void paintTilemap(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameScr.gI().paintBgItem(g, 1);
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
		}
		for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
		{
			for (int k = GameScr.gssy; k < GameScr.gssye; k++)
			{
				if (j != 0)
				{
					if (j != TileMap.tmw - 1)
					{
						int num = TileMap.maps[k * TileMap.tmw + j] - 1;
						if ((TileMap.tileTypeAt(j, k) & 256) != 256)
						{
							if ((TileMap.tileTypeAt(j, k) & 32) == 32)
							{
								g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if ((TileMap.tileTypeAt(j, k) & 128) == 128)
							{
								g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if (TileMap.tileID == 13)
							{
								if (!GameCanvas.lowGraphic)
								{
									return;
								}
								if (num != -1)
								{
									TileMap.paintTile(g, 0, j, k);
								}
							}
							else
							{
								if (TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1)
								{
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
								}
								if (TileMap.tileID == 3)
								{
								}
								if ((TileMap.tileTypeAt(j, k) & 16) == 16)
								{
									TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
									TileMap.dbx = TileMap.bx - GameScr.gW2;
									TileMap.dfx = ((int)TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
									TileMap.fx = TileMap.dfx + GameScr.gW2;
									TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
								}
								else if ((TileMap.tileTypeAt(j, k) & 512) == 512)
								{
									if (num != -1)
									{
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
									}
								}
								else if (num != -1)
								{
									TileMap.paintTile(g, num, j, k);
								}
							}
						}
					}
				}
			}
		}
		if (GameScr.cmx < 24)
		{
			for (int l = GameScr.gssy; l < GameScr.gssye; l++)
			{
				int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
				if (num2 != -1)
				{
					TileMap.paintTile(g, num2, 0, l);
				}
			}
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			int num3 = TileMap.tmw - 2;
			for (int m = GameScr.gssy; m < GameScr.gssye; m++)
			{
				int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
				if (num4 != -1)
				{
					TileMap.paintTile(g, num4, num3 + 1, m);
				}
			}
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0007F8B4 File Offset: 0x0007DAB4
	public static bool isWaterEff()
	{
		return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0007F904 File Offset: 0x0007DB04
	public static void paintOutTilemap(mGraphics g)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		int num = 0;
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				num++;
				if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					if (!TileMap.isWaterEff())
					{
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
					if (TileMap.yWater == 0 && TileMap.isWaterEff())
					{
						TileMap.yWater = j * (int)TileMap.size - 12;
						int color = 16777215;
						if (GameCanvas.typeBg == 2)
						{
							color = 10871287;
						}
						else if (GameCanvas.typeBg == 4)
						{
							color = 8111470;
						}
						else if (GameCanvas.typeBg == 7)
						{
							color = 5693125;
						}
						BackgroudEffect.addWater(color, TileMap.yWater + 15);
					}
				}
			}
		}
		BackgroudEffect.paintWaterAll(g);
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0007FA94 File Offset: 0x0007DC94
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + TileMap.mapID);
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0007FB20 File Offset: 0x0007DD20
	public static int tileAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.maps[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0007FB6C File Offset: 0x0007DD6C
	public static int tileTypeAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.types[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0007FBB8 File Offset: 0x0007DDB8
	public static int tileTypeAtPixel(int px, int py)
	{
		int result;
		try
		{
			result = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0007FC10 File Offset: 0x0007DE10
	public static bool tileTypeAt(int px, int py, int t)
	{
		bool result;
		try
		{
			result = ((TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t);
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00007289 File Offset: 0x00005489
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x000072B1 File Offset: 0x000054B1
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x000072C3 File Offset: 0x000054C3
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000072EC File Offset: 0x000054EC
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x000072EC File Offset: 0x000054EC
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x000072FD File Offset: 0x000054FD
	public static void loadMainTile()
	{
		if (TileMap.lastTileID != TileMap.tileID)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x04000FF5 RID: 4085
	public const int T_EMPTY = 0;

	// Token: 0x04000FF6 RID: 4086
	public const int T_TOP = 2;

	// Token: 0x04000FF7 RID: 4087
	public const int T_LEFT = 4;

	// Token: 0x04000FF8 RID: 4088
	public const int T_RIGHT = 8;

	// Token: 0x04000FF9 RID: 4089
	public const int T_TREE = 16;

	// Token: 0x04000FFA RID: 4090
	public const int T_WATERFALL = 32;

	// Token: 0x04000FFB RID: 4091
	public const int T_WATERFLOW = 64;

	// Token: 0x04000FFC RID: 4092
	public const int T_TOPFALL = 128;

	// Token: 0x04000FFD RID: 4093
	public const int T_OUTSIDE = 256;

	// Token: 0x04000FFE RID: 4094
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x04000FFF RID: 4095
	public const int T_BRIDGE = 1024;

	// Token: 0x04001000 RID: 4096
	public const int T_UNDERWATER = 2048;

	// Token: 0x04001001 RID: 4097
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x04001002 RID: 4098
	public const int T_BOTTOM = 8192;

	// Token: 0x04001003 RID: 4099
	public const int T_DIE = 16384;

	// Token: 0x04001004 RID: 4100
	public const int T_HEBI = 32768;

	// Token: 0x04001005 RID: 4101
	public const int T_BANG = 65536;

	// Token: 0x04001006 RID: 4102
	public const int T_JUM8 = 131072;

	// Token: 0x04001007 RID: 4103
	public const int T_NT0 = 262144;

	// Token: 0x04001008 RID: 4104
	public const int T_NT1 = 524288;

	// Token: 0x04001009 RID: 4105
	public const int T_CENTER = 1;

	// Token: 0x0400100A RID: 4106
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x0400100B RID: 4107
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x0400100C RID: 4108
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x0400100D RID: 4109
	public const int TRAIDAT_DADO = 3;

	// Token: 0x0400100E RID: 4110
	public const int NAMEK_DOINUI = 4;

	// Token: 0x0400100F RID: 4111
	public const int NAMEK_RUNG = 6;

	// Token: 0x04001010 RID: 4112
	public const int NAMEK_DAO = 7;

	// Token: 0x04001011 RID: 4113
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x04001012 RID: 4114
	public const int SAYAI_DOINUI = 8;

	// Token: 0x04001013 RID: 4115
	public const int SAYAI_RUNG = 9;

	// Token: 0x04001014 RID: 4116
	public const int SAYAI_CITY = 10;

	// Token: 0x04001015 RID: 4117
	public const int SAYAI_NIGHT = 11;

	// Token: 0x04001016 RID: 4118
	public const int KAMISAMA = 12;

	// Token: 0x04001017 RID: 4119
	public const int TIME_ROOM = 13;

	// Token: 0x04001018 RID: 4120
	public const int ARENA = 14;

	// Token: 0x04001019 RID: 4121
	public const int DOCNHAN_1 = 15;

	// Token: 0x0400101A RID: 4122
	public const int DOCNHAN_3 = 16;

	// Token: 0x0400101B RID: 4123
	public const int DOCNHAN_2 = 17;

	// Token: 0x0400101C RID: 4124
	public const int FIZE = 18;

	// Token: 0x0400101D RID: 4125
	public const int FIZE2 = 19;

	// Token: 0x0400101E RID: 4126
	public const int FIZE3 = 20;

	// Token: 0x0400101F RID: 4127
	public const int CITY1 = 21;

	// Token: 0x04001020 RID: 4128
	public const int CITY2 = 22;

	// Token: 0x04001021 RID: 4129
	public const int HELL = 15;

	// Token: 0x04001022 RID: 4130
	public static int tmw;

	// Token: 0x04001023 RID: 4131
	public static int tmh;

	// Token: 0x04001024 RID: 4132
	public static int pxw;

	// Token: 0x04001025 RID: 4133
	public static int pxh;

	// Token: 0x04001026 RID: 4134
	public static int tileID;

	// Token: 0x04001027 RID: 4135
	public static int lastTileID = -1;

	// Token: 0x04001028 RID: 4136
	public static int[] maps;

	// Token: 0x04001029 RID: 4137
	public static int[] types;

	// Token: 0x0400102A RID: 4138
	public static Image[] imgTile;

	// Token: 0x0400102B RID: 4139
	public static Image imgTileSmall;

	// Token: 0x0400102C RID: 4140
	public static Image imgMiniMap;

	// Token: 0x0400102D RID: 4141
	public static Image imgWaterfall;

	// Token: 0x0400102E RID: 4142
	public static Image imgTopWaterfall;

	// Token: 0x0400102F RID: 4143
	public static Image imgWaterflow;

	// Token: 0x04001030 RID: 4144
	public static Image imgWaterlowN;

	// Token: 0x04001031 RID: 4145
	public static Image imgWaterlowN2;

	// Token: 0x04001032 RID: 4146
	public static Image imgWaterF;

	// Token: 0x04001033 RID: 4147
	public static Image imgLeaf;

	// Token: 0x04001034 RID: 4148
	public static sbyte size = 24;

	// Token: 0x04001035 RID: 4149
	private static int bx;

	// Token: 0x04001036 RID: 4150
	private static int dbx;

	// Token: 0x04001037 RID: 4151
	private static int fx;

	// Token: 0x04001038 RID: 4152
	private static int dfx;

	// Token: 0x04001039 RID: 4153
	public static string[] instruction;

	// Token: 0x0400103A RID: 4154
	public static int[] iX;

	// Token: 0x0400103B RID: 4155
	public static int[] iY;

	// Token: 0x0400103C RID: 4156
	public static int[] iW;

	// Token: 0x0400103D RID: 4157
	public static int iCount;

	// Token: 0x0400103E RID: 4158
	public static string mapName = string.Empty;

	// Token: 0x0400103F RID: 4159
	public static sbyte versionMap = 1;

	// Token: 0x04001040 RID: 4160
	public static int mapID;

	// Token: 0x04001041 RID: 4161
	public static int lastBgID = -1;

	// Token: 0x04001042 RID: 4162
	public static int zoneID;

	// Token: 0x04001043 RID: 4163
	public static int bgID;

	// Token: 0x04001044 RID: 4164
	public static int bgType;

	// Token: 0x04001045 RID: 4165
	public static int lastType = -1;

	// Token: 0x04001046 RID: 4166
	public static int typeMap;

	// Token: 0x04001047 RID: 4167
	public static sbyte planetID;

	// Token: 0x04001048 RID: 4168
	public static sbyte lastPlanetId = -1;

	// Token: 0x04001049 RID: 4169
	public static long timeTranMini;

	// Token: 0x0400104A RID: 4170
	public static MyVector vGo = new MyVector();

	// Token: 0x0400104B RID: 4171
	public static MyVector vItemBg = new MyVector();

	// Token: 0x0400104C RID: 4172
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x0400104D RID: 4173
	public static string[] mapNames;

	// Token: 0x0400104E RID: 4174
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x0400104F RID: 4175
	public static Image bong;

	// Token: 0x04001050 RID: 4176
	public static Image[] bgItem = new Image[8];

	// Token: 0x04001051 RID: 4177
	public static MyVector vObject = new MyVector();

	// Token: 0x04001052 RID: 4178
	public static int[] offlineId = new int[]
	{
		21,
		22,
		23,
		39,
		40,
		41
	};

	// Token: 0x04001053 RID: 4179
	public static int[] highterId = new int[]
	{
		21,
		22,
		23,
		24,
		25,
		26
	};

	// Token: 0x04001054 RID: 4180
	public static int[] toOfflineId = new int[]
	{
		0,
		7,
		14
	};

	// Token: 0x04001055 RID: 4181
	public static int[][] tileType;

	// Token: 0x04001056 RID: 4182
	public static int[][][] tileIndex;

	// Token: 0x04001057 RID: 4183
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x04001058 RID: 4184
	public static int sizeMiniMap = 2;

	// Token: 0x04001059 RID: 4185
	public static int gssx;

	// Token: 0x0400105A RID: 4186
	public static int gssxe;

	// Token: 0x0400105B RID: 4187
	public static int gssy;

	// Token: 0x0400105C RID: 4188
	public static int gssye;

	// Token: 0x0400105D RID: 4189
	public static int countx;

	// Token: 0x0400105E RID: 4190
	public static int county;

	// Token: 0x0400105F RID: 4191
	private static int[] colorMini = new int[]
	{
		5257738,
		8807192
	};

	// Token: 0x04001060 RID: 4192
	public static int yWater = 0;
}
