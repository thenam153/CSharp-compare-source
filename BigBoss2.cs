using System;

// Token: 0x02000093 RID: 147
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x0600053C RID: 1340 RVA: 0x0003CC20 File Offset: 0x0003AE20
	public BigBoss2(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		if (BigBoss2.shadowBig == null)
		{
			BigBoss2.shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");
		}
		this.mobId = id;
		this.xTo = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yTo = (int)py;
		this.yFirst = (int)py;
		this.getDataB();
		this.hp = hp;
		this.maxHp = maxHp;
		this.templateId = templateID;
		this.status = 2;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0003CD84 File Offset: 0x0003AF84
	public void getDataB()
	{
		BigBoss2.data = null;
		BigBoss2.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			109,
			"/data"
		});
		try
		{
			BigBoss2.data.readData2(patch);
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109 + "/img.png");
		}
		catch (Exception ex)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00005ADD File Offset: 0x00003CDD
	public new void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00005AED File Offset: 0x00003CED
	public new void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0003B160 File Offset: 0x00039360
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

	// Token: 0x06000542 RID: 1346 RVA: 0x00005D50 File Offset: 0x00003F50
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0003CE58 File Offset: 0x0003B058
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

	// Token: 0x06000544 RID: 1348 RVA: 0x0003CF90 File Offset: 0x0003B190
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00003584 File Offset: 0x00001784
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0003CFE4 File Offset: 0x0003B1E4
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
		case 4:
			this.timeStatus = 0;
			this.updateMobFly();
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

	// Token: 0x06000547 RID: 1351 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
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

	// Token: 0x06000548 RID: 1352 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
	private void updateMobFly()
	{
		if (this.flyUp)
		{
			this.dy++;
			this.y -= this.dy;
			this.checkFrameTick(this.fly);
			if (this.y <= -500)
			{
				this.flyUp = false;
				this.flyDown = true;
				this.dy = 0;
			}
		}
		if (this.flyDown)
		{
			this.x = this.xTo;
			this.dy += 2;
			this.y += this.dy;
			this.checkFrameTick(this.hitground);
			if (this.y > this.yFirst)
			{
				this.y = this.yFirst;
				this.flyDown = false;
				this.dy = 0;
				this.status = 2;
				GameScr.shock_scr = 10;
				this.shock = true;
			}
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00003584 File Offset: 0x00001784
	public new void setInjure()
	{
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
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

	// Token: 0x0600054B RID: 1355 RVA: 0x00005B2B File Offset: 0x00003D2B
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00003584 File Offset: 0x00001784
	private void updateInjure()
	{
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0003D380 File Offset: 0x0003B580
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		if (this.x != this.xTo || this.y != this.yTo)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00005D85 File Offset: 0x00003F85
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00005D95 File Offset: 0x00003F95
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
	public new void updateMobAttack()
	{
		if ((int)this.type == 0)
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
		if ((int)this.type == 1)
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
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, this.dameHP[j], 0, this.charAttack[j], 24);
				}
			}
		}
		if ((int)this.type == 2)
		{
			if (this.tick == this.fly.Length - 1)
			{
				this.status = 2;
			}
			this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			if (this.tick == 12)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00003584 File Offset: 0x00001784
	public new void updateMobWalk()
	{
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0003B8A8 File Offset: 0x00039AA8
	public new bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00005B92 File Offset: 0x00003D92
	public new bool isUpdate()
	{
		return this.status != 0;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00005BA2 File Offset: 0x00003DA2
	public new bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
	public override void paint(mGraphics g)
	{
		if (BigBoss2.data == null)
		{
			return;
		}
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		g.translate(0, GameCanvas.transY);
		BigBoss2.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		g.translate(0, -GameCanvas.transY);
		int num = (int)((long)this.hp * 50L / (long)this.maxHp);
		if (num != 0)
		{
			g.setColor(0);
			g.fillRect(this.x - 27, this.y - 112, 54, 8);
			g.setColor(16711680);
			g.setClip(this.x - 25, this.y - 110, num, 4);
			g.fillRect(this.x - 25, this.y - 110, 50, 4);
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

	// Token: 0x06000556 RID: 1366 RVA: 0x00005BBF File Offset: 0x00003DBF
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00005DBA File Offset: 0x00003FBA
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

	// Token: 0x06000558 RID: 1368 RVA: 0x0003D888 File Offset: 0x0003BA88
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

	// Token: 0x06000559 RID: 1369 RVA: 0x00005C00 File Offset: 0x00003E00
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00005DF4 File Offset: 0x00003FF4
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00005C13 File Offset: 0x00003E13
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00005DFF File Offset: 0x00003FFF
	public new int getW()
	{
		return 50;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0003D968 File Offset: 0x0003BB68
	public new void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00005C17 File Offset: 0x00003E17
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00005C30 File Offset: 0x00003E30
	public new void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00005E03 File Offset: 0x00004003
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00005E0C File Offset: 0x0000400C
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x04000936 RID: 2358
	public static Image shadowBig;

	// Token: 0x04000937 RID: 2359
	public static EffectData data;

	// Token: 0x04000938 RID: 2360
	public int xTo;

	// Token: 0x04000939 RID: 2361
	public int yTo;

	// Token: 0x0400093A RID: 2362
	public bool haftBody;

	// Token: 0x0400093B RID: 2363
	public bool change;

	// Token: 0x0400093C RID: 2364
	private Mob mob1;

	// Token: 0x0400093D RID: 2365
	public new int xSd;

	// Token: 0x0400093E RID: 2366
	public new int ySd;

	// Token: 0x0400093F RID: 2367
	private bool isOutMap;

	// Token: 0x04000940 RID: 2368
	private int wCount;

	// Token: 0x04000941 RID: 2369
	public new bool isShadown = true;

	// Token: 0x04000942 RID: 2370
	private int tick;

	// Token: 0x04000943 RID: 2371
	private int frame;

	// Token: 0x04000944 RID: 2372
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000945 RID: 2373
	private bool wy;

	// Token: 0x04000946 RID: 2374
	private int wt;

	// Token: 0x04000947 RID: 2375
	private int fy;

	// Token: 0x04000948 RID: 2376
	private int ty;

	// Token: 0x04000949 RID: 2377
	public new int typeSuperEff;

	// Token: 0x0400094A RID: 2378
	private global::Char focus;

	// Token: 0x0400094B RID: 2379
	private int timeDead;

	// Token: 0x0400094C RID: 2380
	private bool flyUp;

	// Token: 0x0400094D RID: 2381
	private bool flyDown;

	// Token: 0x0400094E RID: 2382
	private int dy;

	// Token: 0x0400094F RID: 2383
	public bool changePos;

	// Token: 0x04000950 RID: 2384
	private int tShock;

	// Token: 0x04000951 RID: 2385
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000952 RID: 2386
	private int tA;

	// Token: 0x04000953 RID: 2387
	private global::Char[] charAttack;

	// Token: 0x04000954 RID: 2388
	private int[] dameHP;

	// Token: 0x04000955 RID: 2389
	private sbyte type;

	// Token: 0x04000956 RID: 2390
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

	// Token: 0x04000957 RID: 2391
	public new int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x04000958 RID: 2392
	public new int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000959 RID: 2393
	public new int[] attack1 = new int[]
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
		9
	};

	// Token: 0x0400095A RID: 2394
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		10,
		10,
		10,
		11,
		11,
		11,
		12,
		12,
		12
	};

	// Token: 0x0400095B RID: 2395
	public int[] attack3 = new int[]
	{
		0,
		0,
		1,
		1,
		4,
		4,
		6,
		6,
		8,
		8,
		25,
		25,
		26,
		26,
		28,
		28,
		30,
		30,
		32,
		32,
		2,
		2,
		1,
		1
	};

	// Token: 0x0400095C RID: 2396
	public int[] fly = new int[]
	{
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6,
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x0400095D RID: 2397
	public int[] hitground = new int[]
	{
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x0400095E RID: 2398
	private bool shock;

	// Token: 0x0400095F RID: 2399
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000960 RID: 2400
	public new global::Char injureBy;

	// Token: 0x04000961 RID: 2401
	public new bool injureThenDie;

	// Token: 0x04000962 RID: 2402
	public new Mob mobToAttack;

	// Token: 0x04000963 RID: 2403
	public new int forceWait;

	// Token: 0x04000964 RID: 2404
	public new bool blindEff;

	// Token: 0x04000965 RID: 2405
	public new bool sleepEff;
}
