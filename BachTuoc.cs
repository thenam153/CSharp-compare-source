using System;

// Token: 0x02000091 RID: 145
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x060004EF RID: 1263 RVA: 0x0003AF78 File Offset: 0x00039178
	public BachTuoc(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.xFirst = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yFirst = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.getDataB();
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.status = 2;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0003B08C File Offset: 0x0003928C
	public void getDataB()
	{
		BachTuoc.data = null;
		BachTuoc.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			108,
			"/data"
		});
		try
		{
			BachTuoc.data.readData2(patch);
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00005ADD File Offset: 0x00003CDD
	public new void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00005AED File Offset: 0x00003CED
	public new void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0003B160 File Offset: 0x00039360
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			string text = (string)Mob.newMob.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00005AF6 File Offset: 0x00003CF6
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0003B1A8 File Offset: 0x000393A8
	private void updateShadown()
	{
		int num = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		if (this.ySd <= 0)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			return;
		}
		if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) == 0)
		{
			this.isOutMap = true;
		}
		else if (TileMap.tileTypeAt(this.xSd / num, this.ySd / num) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2))
		{
			this.xSd = this.x;
			this.ySd = this.y;
			this.isOutMap = false;
		}
		while (this.isOutMap && this.wCount < 10)
		{
			this.wCount++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
				}
				return;
			}
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0003B2E0 File Offset: 0x000394E0
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00003584 File Offset: 0x00001784
	public new void updateSuperEff()
	{
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0003B334 File Offset: 0x00039534
	public override void update()
	{
		if (!this.isUpdate())
		{
			return;
		}
		this.updateShadown();
		switch (this.status)
		{
		case 0:
		case 1:
			this.updateDead();
			break;
		case 2:
			this.updateMobStandWait();
			break;
		case 3:
			this.updateMobAttack();
			break;
		case 5:
			this.timeStatus = 0;
			this.updateMobWalk();
			break;
		case 6:
			this.timeStatus = 0;
			this.p1++;
			this.y += this.p1;
			if (this.y >= this.yFirst)
			{
				this.y = this.yFirst;
				this.p1 = 0;
				this.status = 5;
			}
			break;
		case 7:
			this.updateInjure();
			break;
		}
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0003B41C File Offset: 0x0003961C
	private void updateDead()
	{
		this.checkFrameTick(this.stand);
		if (GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
		}
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00003584 File Offset: 0x00001784
	public new void setInjure()
	{
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0003B4EC File Offset: 0x000396EC
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx <= this.x) ? -1 : 1);
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		if (Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2)
		{
			if (this.x < cx)
			{
				this.x = cx - this.w;
			}
			else
			{
				this.x = cx + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00005B2B File Offset: 0x00003D2B
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00003584 File Offset: 0x00001784
	private void updateInjure()
	{
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0003B5CC File Offset: 0x000397CC
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00005B64 File Offset: 0x00003D64
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00005B74 File Offset: 0x00003D74
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0003B640 File Offset: 0x00039840
	public new void updateMobAttack()
	{
		if ((int)this.type == 3)
		{
			if (this.tick == this.attack1.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			if (this.tick == 8)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		if ((int)this.type == 4)
		{
			if (this.tick == this.attack2.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.attack2);
			if (this.tick == 8)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0003B814 File Offset: 0x00039A14
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? -2 : 2);
		this.y = this.yTo;
		this.dir = ((this.x >= this.xTo) ? -1 : 1);
		if (Res.abs(this.x - this.xTo) <= 1)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0003B8A8 File Offset: 0x00039AA8
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00005B92 File Offset: 0x00003D92
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00005BA2 File Offset: 0x00003DA2
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0003B91C File Offset: 0x00039B1C
	public override void paint(mGraphics g)
	{
		if (BachTuoc.data == null)
		{
			return;
		}
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		g.translate(0, GameCanvas.transY);
		BachTuoc.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		g.translate(0, -GameCanvas.transY);
		int num = (int)((long)this.hp * 50L / (long)this.maxHp);
		if (num != 0)
		{
			g.setColor(0);
			g.fillRect(this.x - 27, this.y - 82, 54, 8);
			g.setColor(16711680);
			g.setClip(this.x - 25, this.y - 80, num, 4);
			g.fillRect(this.x - 25, this.y - 80, 50, 4);
			g.setClip(0, 0, 3000, 3000);
		}
		if (this.shock)
		{
			this.tShock++;
			Effect me = new Effect(((int)this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me);
			Effect me2 = new Effect(((int)this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1);
			EffecMn.addEff(me2);
			if (this.tShock == 50)
			{
				this.tShock = 0;
				this.shock = false;
			}
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00005BBF File Offset: 0x00003DBF
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00005BC6 File Offset: 0x00003DC6
	public new void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0003BAE4 File Offset: 0x00039CE4
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int x = mobToAttack.x;
		int y = mobToAttack.y;
		if (Res.abs(x - this.x) < this.w * 2 && Res.abs(y - this.y) < this.h * 2)
		{
			if (this.x < x)
			{
				this.x = x - this.w;
			}
			else
			{
				this.x = x + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00005C00 File Offset: 0x00003E00
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00005C08 File Offset: 0x00003E08
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00005C13 File Offset: 0x00003E13
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00005C13 File Offset: 0x00003E13
	public new int getW()
	{
		return 40;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0003BBC4 File Offset: 0x00039DC4
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00005C17 File Offset: 0x00003E17
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00005C30 File Offset: 0x00003E30
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00005C44 File Offset: 0x00003E44
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00005C4D File Offset: 0x00003E4D
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00005C56 File Offset: 0x00003E56
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x040008D9 RID: 2265
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x040008DA RID: 2266
	public static EffectData data;

	// Token: 0x040008DB RID: 2267
	public int xTo;

	// Token: 0x040008DC RID: 2268
	public int yTo;

	// Token: 0x040008DD RID: 2269
	public bool haftBody;

	// Token: 0x040008DE RID: 2270
	public bool change;

	// Token: 0x040008DF RID: 2271
	private Mob mob1;

	// Token: 0x040008E0 RID: 2272
	public new int xSd;

	// Token: 0x040008E1 RID: 2273
	public new int ySd;

	// Token: 0x040008E2 RID: 2274
	private bool isOutMap;

	// Token: 0x040008E3 RID: 2275
	private int wCount;

	// Token: 0x040008E4 RID: 2276
	public new bool isShadown = true;

	// Token: 0x040008E5 RID: 2277
	private int tick;

	// Token: 0x040008E6 RID: 2278
	private int frame;

	// Token: 0x040008E7 RID: 2279
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x040008E8 RID: 2280
	private bool wy;

	// Token: 0x040008E9 RID: 2281
	private int wt;

	// Token: 0x040008EA RID: 2282
	private int fy;

	// Token: 0x040008EB RID: 2283
	private int ty;

	// Token: 0x040008EC RID: 2284
	public new int typeSuperEff;

	// Token: 0x040008ED RID: 2285
	private global::Char focus;

	// Token: 0x040008EE RID: 2286
	private bool flyUp;

	// Token: 0x040008EF RID: 2287
	private bool flyDown;

	// Token: 0x040008F0 RID: 2288
	private int dy;

	// Token: 0x040008F1 RID: 2289
	public bool changePos;

	// Token: 0x040008F2 RID: 2290
	private int tShock;

	// Token: 0x040008F3 RID: 2291
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x040008F4 RID: 2292
	private int tA;

	// Token: 0x040008F5 RID: 2293
	private global::Char[] charAttack;

	// Token: 0x040008F6 RID: 2294
	private int[] dameHP;

	// Token: 0x040008F7 RID: 2295
	private sbyte type;

	// Token: 0x040008F8 RID: 2296
	public new int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x040008F9 RID: 2297
	public int[] movee = new int[]
	{
		0,
		0,
		0,
		2,
		2,
		2,
		3,
		3,
		3,
		4,
		4,
		4
	};

	// Token: 0x040008FA RID: 2298
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6
	};

	// Token: 0x040008FB RID: 2299
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		7,
		7,
		7,
		8,
		8,
		8,
		9,
		9,
		9,
		10,
		10,
		10,
		11,
		11
	};

	// Token: 0x040008FC RID: 2300
	public int[] hurt = new int[]
	{
		1,
		1,
		7,
		7
	};

	// Token: 0x040008FD RID: 2301
	private bool shock;

	// Token: 0x040008FE RID: 2302
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x040008FF RID: 2303
	public new global::Char injureBy;

	// Token: 0x04000900 RID: 2304
	public new bool injureThenDie;

	// Token: 0x04000901 RID: 2305
	public new Mob mobToAttack;

	// Token: 0x04000902 RID: 2306
	public new int forceWait;

	// Token: 0x04000903 RID: 2307
	public new bool blindEff;

	// Token: 0x04000904 RID: 2308
	public new bool sleepEff;
}
