using System;

namespace Assets.src.g
{
	// Token: 0x02000092 RID: 146
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0003BC08 File Offset: 0x00039E08
		public BigBoss(int id, short px, short py, int templateID, int hp, int maxhp, int s)
		{
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.mobId = id;
			if (s == 0)
			{
				this.getDataB();
			}
			if (s == 1)
			{
				this.getDataB2();
			}
			if (s == 2)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.hp = hp;
			this.maxHp = maxhp;
			this.templateId = templateID;
			this.status = 2;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0003BDBC File Offset: 0x00039FBC
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				100,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100 + "/img.png");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0003BE98 File Offset: 0x0003A098
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				101,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00005ADD File Offset: 0x00003CDD
		public new void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00005AED File Offset: 0x00003CED
		public new void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0003B160 File Offset: 0x00039360
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

		// Token: 0x0600051C RID: 1308 RVA: 0x00005C86 File Offset: 0x00003E86
		public new void checkFrameTick(int[] array)
		{
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0003BF78 File Offset: 0x0003A178
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

		// Token: 0x0600051E RID: 1310 RVA: 0x0003C0B0 File Offset: 0x0003A2B0
		private void paintShadow(mGraphics g)
		{
			int num = (int)TileMap.size;
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00003584 File Offset: 0x00001784
		public new void updateSuperEff()
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0003C104 File Offset: 0x0003A304
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

		// Token: 0x06000521 RID: 1313 RVA: 0x0003C200 File Offset: 0x0003A400
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0003C2E4 File Offset: 0x0003A4E4
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

		// Token: 0x06000523 RID: 1315 RVA: 0x00003584 File Offset: 0x00001784
		public new void setInjure()
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0003C3D4 File Offset: 0x0003A5D4
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

		// Token: 0x06000525 RID: 1317 RVA: 0x00005B2B File Offset: 0x00003D2B
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00003584 File Offset: 0x00001784
		private void updateInjure()
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0003C4B4 File Offset: 0x0003A6B4
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			if (this.x != this.xFirst || this.y != this.yFirst)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00005CBB File Offset: 0x00003EBB
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0003C540 File Offset: 0x0003A740
		public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			if ((int)type < 3)
			{
				this.status = 3;
			}
			if ((int)type == 3)
			{
				this.flyUp = true;
				this.status = 4;
			}
			if ((int)type == 4)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				}
			}
			if ((int)type == 7)
			{
				this.status = 3;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
		public new void updateMobAttack()
		{
			if ((int)this.type == 7)
			{
				if (this.tick > 8)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			if ((int)this.type == 0)
			{
				if (this.tick == this.attack1.Length - 1)
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick(this.attack1);
				if (this.tick == 8)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, this.dameHP[i], 0, this.charAttack[i], 24);
					}
				}
			}
			if ((int)this.type == 1)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				if (this.tick == 18)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			if ((int)this.type == 8)
			{
			}
			if ((int)this.type == 2)
			{
				if (this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1)))
				{
					this.status = 2;
				}
				this.dir = ((this.x >= this.charAttack[0].cx) ? -1 : 1);
				this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
				if (this.tick == 13)
				{
					GameScr.shock_scr = 10;
					this.shock = true;
					for (int k = 0; k < this.charAttack.Length; k++)
					{
						this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					}
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00003584 File Offset: 0x00001784
		public new void updateMobWalk()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0003B8A8 File Offset: 0x00039AA8
		public new bool isPaint()
		{
			return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && this.status != 0;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00005B92 File Offset: 0x00003D92
		public new bool isUpdate()
		{
			return this.status != 0;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00005BA2 File Offset: 0x00003DA2
		public new bool checkIsBoss()
		{
			return this.isBoss || (int)this.levelBoss > 0;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0003C91C File Offset: 0x0003AB1C
		public override void paint(mGraphics g)
		{
			if (BigBoss.data == null)
			{
				return;
			}
			if (this.isShadown && this.status != 0)
			{
				this.paintShadow(g);
			}
			g.translate(0, GameCanvas.transY);
			BigBoss.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
				Res.outz("type= " + this.type);
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

		// Token: 0x06000530 RID: 1328 RVA: 0x00005BBF File Offset: 0x00003DBF
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00005CCB File Offset: 0x00003ECB
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

		// Token: 0x06000532 RID: 1330 RVA: 0x0003CAFC File Offset: 0x0003ACFC
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

		// Token: 0x06000533 RID: 1331 RVA: 0x00005C00 File Offset: 0x00003E00
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00005D05 File Offset: 0x00003F05
		public new int getY()
		{
			return (!this.haftBody) ? (this.y - 60) : (this.y - 20);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00005C13 File Offset: 0x00003E13
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00005D29 File Offset: 0x00003F29
		public new int getW()
		{
			return 60;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0003CBDC File Offset: 0x0003ADDC
		public new void stopMoving()
		{
			if (this.status == 5)
			{
				this.status = 2;
				this.p1 = (this.p2 = (this.p3 = 0));
				this.forceWait = 50;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00005C17 File Offset: 0x00003E17
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00005C30 File Offset: 0x00003E30
		public new void removeHoldEff()
		{
			if (this.holdEffID != 0)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00005D2D File Offset: 0x00003F2D
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00005D36 File Offset: 0x00003F36
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x04000905 RID: 2309
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x04000906 RID: 2310
		public static EffectData data;

		// Token: 0x04000907 RID: 2311
		public int xTo;

		// Token: 0x04000908 RID: 2312
		public int yTo;

		// Token: 0x04000909 RID: 2313
		public bool haftBody;

		// Token: 0x0400090A RID: 2314
		public bool change;

		// Token: 0x0400090B RID: 2315
		public new int xSd;

		// Token: 0x0400090C RID: 2316
		public new int ySd;

		// Token: 0x0400090D RID: 2317
		private bool isOutMap;

		// Token: 0x0400090E RID: 2318
		private int wCount;

		// Token: 0x0400090F RID: 2319
		public new bool isShadown = true;

		// Token: 0x04000910 RID: 2320
		private int tick;

		// Token: 0x04000911 RID: 2321
		private int frame;

		// Token: 0x04000912 RID: 2322
		public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

		// Token: 0x04000913 RID: 2323
		private bool wy;

		// Token: 0x04000914 RID: 2324
		private int wt;

		// Token: 0x04000915 RID: 2325
		private int fy;

		// Token: 0x04000916 RID: 2326
		private int ty;

		// Token: 0x04000917 RID: 2327
		public new int typeSuperEff;

		// Token: 0x04000918 RID: 2328
		private global::Char focus;

		// Token: 0x04000919 RID: 2329
		private bool flyUp;

		// Token: 0x0400091A RID: 2330
		private bool flyDown;

		// Token: 0x0400091B RID: 2331
		private int dy;

		// Token: 0x0400091C RID: 2332
		public bool changePos;

		// Token: 0x0400091D RID: 2333
		private int tShock;

		// Token: 0x0400091E RID: 2334
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x0400091F RID: 2335
		private int tA;

		// Token: 0x04000920 RID: 2336
		private global::Char[] charAttack;

		// Token: 0x04000921 RID: 2337
		private int[] dameHP;

		// Token: 0x04000922 RID: 2338
		private sbyte type;

		// Token: 0x04000923 RID: 2339
		public new int[] stand = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		};

		// Token: 0x04000924 RID: 2340
		public int[] stand_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			38,
			39,
			39,
			40,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			38
		};

		// Token: 0x04000925 RID: 2341
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

		// Token: 0x04000926 RID: 2342
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

		// Token: 0x04000927 RID: 2343
		public new int[] attack1 = new int[]
		{
			0,
			0,
			34,
			34,
			35,
			35,
			36,
			36,
			2,
			2,
			1,
			1
		};

		// Token: 0x04000928 RID: 2344
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			6,
			6,
			9,
			9,
			10,
			10,
			13,
			13,
			15,
			15,
			17,
			17,
			19,
			19,
			21,
			21,
			23,
			23
		};

		// Token: 0x04000929 RID: 2345
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

		// Token: 0x0400092A RID: 2346
		public int[] attack2_1 = new int[]
		{
			37,
			37,
			5,
			5,
			7,
			7,
			11,
			11,
			14,
			14,
			16,
			16,
			18,
			18,
			20,
			20,
			22,
			22,
			24,
			24
		};

		// Token: 0x0400092B RID: 2347
		public int[] attack3_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			5,
			5,
			7,
			7,
			11,
			11,
			27,
			27,
			29,
			29,
			31,
			31,
			33,
			33,
			38,
			38
		};

		// Token: 0x0400092C RID: 2348
		public int[] fly = new int[]
		{
			8,
			8,
			9,
			9,
			10,
			10,
			12,
			12
		};

		// Token: 0x0400092D RID: 2349
		public int[] hitground = new int[]
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

		// Token: 0x0400092E RID: 2350
		private bool shock;

		// Token: 0x0400092F RID: 2351
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04000930 RID: 2352
		public new global::Char injureBy;

		// Token: 0x04000931 RID: 2353
		public new bool injureThenDie;

		// Token: 0x04000932 RID: 2354
		public new Mob mobToAttack;

		// Token: 0x04000933 RID: 2355
		public new int forceWait;

		// Token: 0x04000934 RID: 2356
		public new bool blindEff;

		// Token: 0x04000935 RID: 2357
		public new bool sleepEff;
	}
}
