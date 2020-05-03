using System;

// Token: 0x02000058 RID: 88
public class Item
{
	// Token: 0x06000309 RID: 777 RVA: 0x00004C7A File Offset: 0x00002E7A
	public void getCompare()
	{
		this.compare = GameCanvas.panel.getCompare(this);
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00017950 File Offset: 0x00015B50
	public string getPrice()
	{
		string result = string.Empty;
		if (this.buyCoin <= 0 && this.buyGold <= 0)
		{
			return null;
		}
		if (this.buyCoin > 0 && this.buyGold <= 0)
		{
			result = this.buyCoin + mResources.XU;
		}
		else if (this.buyGold > 0 && this.buyCoin <= 0)
		{
			result = this.buyGold + mResources.LUONG;
		}
		else if (this.buyCoin > 0 && this.buyGold > 0)
		{
			result = string.Concat(new object[]
			{
				this.buyCoin,
				mResources.XU,
				"/",
				this.buyGold,
				mResources.LUONG
			});
		}
		return result;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00017A3C File Offset: 0x00015C3C
	public void paintUpgradeEffect(int x, int y, int upgrade, mGraphics g)
	{
		int num = GameScr.indexSize - 2;
		int num2 = 0;
		int num3 = (upgrade >= 4) ? ((upgrade >= 8) ? ((upgrade >= 12) ? ((upgrade > 14) ? 4 : 3) : 2) : 1) : 0;
		for (int i = num2; i < this.size.Length; i++)
		{
			int num4 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick * 1 - i * 4);
			int num5 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick * 1 - i * 4);
			g.setColor(this.colorBorder[num3][i]);
			g.fillRect(num4 - this.size[i] / 2, num5 - this.size[i] / 2, this.size[i], this.size[i]);
		}
		if (upgrade == 4 || upgrade == 8)
		{
			for (int j = num2; j < this.size.Length; j++)
			{
				int num6 = x - num / 2 + this.upgradeEffectX((GameCanvas.gameTick - num * 2) * 1 - j * 4);
				int num7 = y - num / 2 + this.upgradeEffectY((GameCanvas.gameTick - num * 2) * 1 - j * 4);
				g.setColor(this.colorBorder[num3 - 1][j]);
				g.fillRect(num6 - this.size[j] / 2, num7 - this.size[j] / 2, this.size[j], this.size[j]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8)
		{
			for (int k = num2; k < this.size.Length; k++)
			{
				int num8 = x - num / 2 + this.upgradeEffectX((GameCanvas.gameTick - num * 2) * 1 - k * 4);
				int num9 = y - num / 2 + this.upgradeEffectY((GameCanvas.gameTick - num * 2) * 1 - k * 4);
				g.setColor(this.colorBorder[num3][k]);
				g.fillRect(num8 - this.size[k] / 2, num9 - this.size[k] / 2, this.size[k], this.size[k]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9)
		{
			for (int l = num2; l < this.size.Length; l++)
			{
				int num10 = x - num / 2 + this.upgradeEffectX((GameCanvas.gameTick - num) * 1 - l * 4);
				int num11 = y - num / 2 + this.upgradeEffectY((GameCanvas.gameTick - num) * 1 - l * 4);
				g.setColor(this.colorBorder[num3][l]);
				g.fillRect(num10 - this.size[l] / 2, num11 - this.size[l] / 2, this.size[l], this.size[l]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9 && upgrade != 13 && upgrade != 3 && upgrade != 6 && upgrade != 10 && upgrade != 15)
		{
			for (int m = num2; m < this.size.Length; m++)
			{
				int num12 = x - num / 2 + this.upgradeEffectX((GameCanvas.gameTick - num * 3) * 1 - m * 4);
				int num13 = y - num / 2 + this.upgradeEffectY((GameCanvas.gameTick - num * 3) * 1 - m * 4);
				g.setColor(this.colorBorder[num3][m]);
				g.fillRect(num12 - this.size[m] / 2, num13 - this.size[m] / 2, this.size[m], this.size[m]);
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00017E48 File Offset: 0x00016048
	private int upgradeEffectY(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		if (0 <= num2 && num2 < num)
		{
			return 0;
		}
		if (num <= num2 && num2 < num * 2)
		{
			return num2 % num;
		}
		if (num * 2 <= num2 && num2 < num * 3)
		{
			return num;
		}
		return num - num2 % num;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00017EA0 File Offset: 0x000160A0
	private int upgradeEffectX(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		if (0 <= num2 && num2 < num)
		{
			return num2 % num;
		}
		if (num <= num2 && num2 < num * 2)
		{
			return num;
		}
		if (num * 2 <= num2 && num2 < num * 3)
		{
			return num - num2 % num;
		}
		return 0;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00017EF8 File Offset: 0x000160F8
	public bool isHaveOption(int id)
	{
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			ItemOption itemOption = this.itemOption[i];
			if (itemOption != null && itemOption.optionTemplate.id == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00017F44 File Offset: 0x00016144
	public Item clone()
	{
		Item item = new Item();
		item.template = this.template;
		if (this.options != null)
		{
			item.options = new MyVector();
			for (int i = 0; i < this.options.size(); i++)
			{
				ItemOption itemOption = new ItemOption();
				itemOption.optionTemplate = ((ItemOption)this.options.elementAt(i)).optionTemplate;
				itemOption.param = ((ItemOption)this.options.elementAt(i)).param;
				item.options.addElement(itemOption);
			}
		}
		item.itemId = this.itemId;
		item.playerId = this.playerId;
		item.indexUI = this.indexUI;
		item.quantity = this.quantity;
		item.isLock = this.isLock;
		item.sys = this.sys;
		item.upgrade = this.upgrade;
		item.buyCoin = this.buyCoin;
		item.buyCoinLock = this.buyCoinLock;
		item.buyGold = this.buyGold;
		item.buyGoldLock = this.buyGoldLock;
		item.saleCoinLock = this.saleCoinLock;
		item.typeUI = this.typeUI;
		item.isExpires = this.isExpires;
		return item;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00004C8D File Offset: 0x00002E8D
	public bool isTypeBody()
	{
		return (0 <= (int)this.template.type && (int)this.template.type < 6) || (int)this.template.type == 32;
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00004CC9 File Offset: 0x00002EC9
	public string getLockstring()
	{
		return (!this.isLock) ? mResources.NOLOCK : mResources.LOCKED;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00004CE5 File Offset: 0x00002EE5
	public string getUpgradestring()
	{
		if ((int)this.template.level < 10 || (int)this.template.type >= 10)
		{
			return mResources.NOTUPGRADE;
		}
		if (this.upgrade == 0)
		{
			return mResources.NOUPGRADE;
		}
		return null;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00004D25 File Offset: 0x00002F25
	public bool isTypeUIMe()
	{
		return this.typeUI == 5 || this.typeUI == 3 || this.typeUI == 4;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00004D4E File Offset: 0x00002F4E
	public bool isTypeUIShopView()
	{
		return this.isTypeUIShop() || (this.isTypeUIStore() || this.isTypeUIBook() || this.isTypeUIFashion());
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00018088 File Offset: 0x00016288
	public bool isTypeUIShop()
	{
		return this.typeUI == 20 || this.typeUI == 21 || this.typeUI == 22 || this.typeUI == 23 || this.typeUI == 24 || this.typeUI == 25 || this.typeUI == 26 || this.typeUI == 27 || this.typeUI == 28 || this.typeUI == 29 || this.typeUI == 16 || this.typeUI == 17 || this.typeUI == 18 || this.typeUI == 19 || this.typeUI == 2 || this.typeUI == 6 || this.typeUI == 8;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00004D81 File Offset: 0x00002F81
	public bool isTypeUIShopLock()
	{
		return this.typeUI == 7 || this.typeUI == 9;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00004D9F File Offset: 0x00002F9F
	public bool isTypeUIStore()
	{
		return this.typeUI == 14;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00004DB1 File Offset: 0x00002FB1
	public bool isTypeUIBook()
	{
		return this.typeUI == 15;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00004DC3 File Offset: 0x00002FC3
	public bool isTypeUIFashion()
	{
		return this.typeUI == 32;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00004DD5 File Offset: 0x00002FD5
	public bool isUpMax()
	{
		return this.getUpMax() == this.upgrade;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00018174 File Offset: 0x00016374
	public int getUpMax()
	{
		if ((int)this.template.level >= 1 && (int)this.template.level < 20)
		{
			return 4;
		}
		if ((int)this.template.level >= 20 && (int)this.template.level < 40)
		{
			return 8;
		}
		if ((int)this.template.level >= 40 && (int)this.template.level < 50)
		{
			return 12;
		}
		if ((int)this.template.level >= 50 && (int)this.template.level < 60)
		{
			return 14;
		}
		return 16;
	}

	// Token: 0x040004DD RID: 1245
	public const int TYPE_BODY_MIN = 0;

	// Token: 0x040004DE RID: 1246
	public const int TYPE_BODY_MAX = 6;

	// Token: 0x040004DF RID: 1247
	public const int TYPE_AO = 0;

	// Token: 0x040004E0 RID: 1248
	public const int TYPE_QUAN = 1;

	// Token: 0x040004E1 RID: 1249
	public const int TYPE_GANGTAY = 2;

	// Token: 0x040004E2 RID: 1250
	public const int TYPE_GIAY = 3;

	// Token: 0x040004E3 RID: 1251
	public const int TYPE_RADA = 4;

	// Token: 0x040004E4 RID: 1252
	public const int TYPE_HAIR = 5;

	// Token: 0x040004E5 RID: 1253
	public const int TYPE_DAUTHAN = 6;

	// Token: 0x040004E6 RID: 1254
	public const int TYPE_NGOCRONG = 12;

	// Token: 0x040004E7 RID: 1255
	public const int TYPE_SACH = 7;

	// Token: 0x040004E8 RID: 1256
	public const int TYPE_NHIEMVU = 8;

	// Token: 0x040004E9 RID: 1257
	public const int TYPE_GOLD = 9;

	// Token: 0x040004EA RID: 1258
	public const int TYPE_DIAMOND = 10;

	// Token: 0x040004EB RID: 1259
	public const int TYPE_BALO = 11;

	// Token: 0x040004EC RID: 1260
	public const sbyte UI_WEAPON = 2;

	// Token: 0x040004ED RID: 1261
	public const sbyte UI_BAG = 3;

	// Token: 0x040004EE RID: 1262
	public const sbyte UI_BOX = 4;

	// Token: 0x040004EF RID: 1263
	public const sbyte UI_BODY = 5;

	// Token: 0x040004F0 RID: 1264
	public const sbyte UI_STACK = 6;

	// Token: 0x040004F1 RID: 1265
	public const sbyte UI_STACK_LOCK = 7;

	// Token: 0x040004F2 RID: 1266
	public const sbyte UI_GROCERY = 8;

	// Token: 0x040004F3 RID: 1267
	public const sbyte UI_GROCERY_LOCK = 9;

	// Token: 0x040004F4 RID: 1268
	public const sbyte UI_UPGRADE = 10;

	// Token: 0x040004F5 RID: 1269
	public const sbyte UI_UPPEARL = 11;

	// Token: 0x040004F6 RID: 1270
	public const sbyte UI_UPPEARL_LOCK = 12;

	// Token: 0x040004F7 RID: 1271
	public const sbyte UI_SPLIT = 13;

	// Token: 0x040004F8 RID: 1272
	public const sbyte UI_STORE = 14;

	// Token: 0x040004F9 RID: 1273
	public const sbyte UI_BOOK = 15;

	// Token: 0x040004FA RID: 1274
	public const sbyte UI_LIEN = 16;

	// Token: 0x040004FB RID: 1275
	public const sbyte UI_NHAN = 17;

	// Token: 0x040004FC RID: 1276
	public const sbyte UI_NGOCBOI = 18;

	// Token: 0x040004FD RID: 1277
	public const sbyte UI_PHU = 19;

	// Token: 0x040004FE RID: 1278
	public const sbyte UI_NONNAM = 20;

	// Token: 0x040004FF RID: 1279
	public const sbyte UI_NONNU = 21;

	// Token: 0x04000500 RID: 1280
	public const sbyte UI_AONAM = 22;

	// Token: 0x04000501 RID: 1281
	public const sbyte UI_AONU = 23;

	// Token: 0x04000502 RID: 1282
	public const sbyte UI_GANGTAYNAM = 24;

	// Token: 0x04000503 RID: 1283
	public const sbyte UI_GANGTAYNU = 25;

	// Token: 0x04000504 RID: 1284
	public const sbyte UI_QUANNAM = 26;

	// Token: 0x04000505 RID: 1285
	public const sbyte UI_QUANNU = 27;

	// Token: 0x04000506 RID: 1286
	public const sbyte UI_GIAYNAM = 28;

	// Token: 0x04000507 RID: 1287
	public const sbyte UI_GIAYNU = 29;

	// Token: 0x04000508 RID: 1288
	public const sbyte UI_TRADE = 30;

	// Token: 0x04000509 RID: 1289
	public const sbyte UI_UPGRADE_GOLD = 31;

	// Token: 0x0400050A RID: 1290
	public const sbyte UI_FASHION = 32;

	// Token: 0x0400050B RID: 1291
	public const sbyte UI_CONVERT = 33;

	// Token: 0x0400050C RID: 1292
	public ItemOption[] itemOption;

	// Token: 0x0400050D RID: 1293
	public ItemTemplate template;

	// Token: 0x0400050E RID: 1294
	public MyVector options;

	// Token: 0x0400050F RID: 1295
	public int itemId;

	// Token: 0x04000510 RID: 1296
	public int playerId;

	// Token: 0x04000511 RID: 1297
	public bool isSelect;

	// Token: 0x04000512 RID: 1298
	public int indexUI;

	// Token: 0x04000513 RID: 1299
	public int quantity;

	// Token: 0x04000514 RID: 1300
	public int quantilyToBuy;

	// Token: 0x04000515 RID: 1301
	public long powerRequire;

	// Token: 0x04000516 RID: 1302
	public bool isLock;

	// Token: 0x04000517 RID: 1303
	public int sys;

	// Token: 0x04000518 RID: 1304
	public int upgrade;

	// Token: 0x04000519 RID: 1305
	public int buyCoin;

	// Token: 0x0400051A RID: 1306
	public int buyCoinLock;

	// Token: 0x0400051B RID: 1307
	public int buyGold;

	// Token: 0x0400051C RID: 1308
	public int buyGoldLock;

	// Token: 0x0400051D RID: 1309
	public int saleCoinLock;

	// Token: 0x0400051E RID: 1310
	public sbyte buyType = -1;

	// Token: 0x0400051F RID: 1311
	public int typeUI;

	// Token: 0x04000520 RID: 1312
	public bool isExpires;

	// Token: 0x04000521 RID: 1313
	public EffectCharPaint eff;

	// Token: 0x04000522 RID: 1314
	public int indexEff;

	// Token: 0x04000523 RID: 1315
	public Image img;

	// Token: 0x04000524 RID: 1316
	public string info;

	// Token: 0x04000525 RID: 1317
	public string content;

	// Token: 0x04000526 RID: 1318
	public int compare;

	// Token: 0x04000527 RID: 1319
	public sbyte isMe;

	// Token: 0x04000528 RID: 1320
	public bool newItem;

	// Token: 0x04000529 RID: 1321
	private int[] color = new int[]
	{
		0,
		0,
		0,
		0,
		600841,
		600841,
		667658,
		667658,
		3346944,
		3346688,
		4199680,
		5052928,
		3276851,
		3932211,
		4587571,
		5046280,
		6684682,
		3359744
	};

	// Token: 0x0400052A RID: 1322
	private int[][] colorBorder = new int[][]
	{
		new int[]
		{
			18687,
			16869,
			15052,
			13235,
			11161,
			9344
		},
		new int[]
		{
			45824,
			39168,
			32768,
			26112,
			19712,
			13056
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16711705,
			15007767,
			13369364,
			11730962,
			10027023,
			8388621
		}
	};

	// Token: 0x0400052B RID: 1323
	private int[] size = new int[]
	{
		2,
		1,
		1,
		1,
		1,
		1
	};
}
