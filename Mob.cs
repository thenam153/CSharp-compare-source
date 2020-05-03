using System;
using Assets.src.g;

// Token: 0x020000A7 RID: 167
public class Mob : IMapObject
{
	// Token: 0x06000736 RID: 1846 RVA: 0x0006078C File Offset: 0x0005E98C
	public Mob()
	{
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00060854 File Offset: 0x0005EA54
	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, int hp, sbyte level, int maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		this.pointx = pointx;
		this.x = (int)pointx;
		this.xFirst = (int)pointx;
		this.pointy = pointy;
		this.y = (int)pointy;
		this.yFirst = (int)pointy;
		this.status = (int)status;
		if (templateId != 70)
		{
			this.checkData();
			this.getData();
		}
		if (!Mob.isExistNewMob(templateId + string.Empty))
		{
			Mob.newMob.addElement(templateId + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		if (templateId >= 58 && templateId <= 65)
		{
			this.stand = new int[]
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
			this.move = new int[]
			{
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4,
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4
			};
			this.moveFast = new int[]
			{
				2,
				2,
				3,
				3,
				2,
				2,
				4,
				4
			};
			this.attack1 = new int[]
			{
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12
			};
			this.attack2 = new int[]
			{
				5,
				12,
				13,
				14
			};
		}
		else
		{
			this.stand = new int[]
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
			this.move = new int[]
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
			this.moveFast = new int[]
			{
				1,
				1,
				2,
				2,
				3,
				3,
				2
			};
			this.attack1 = new int[]
			{
				4,
				5,
				6
			};
			this.attack2 = new int[]
			{
				7,
				8,
				9
			};
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00006971 File Offset: 0x00004B71
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00060B1C File Offset: 0x0005ED1C
	public void getData()
	{
		if (Mob.arrMobTemplate[this.templateId].data == null)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId;
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			Mob.lastMob.addElement(this.templateId + string.Empty);
		}
		else
		{
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00005ADD File Offset: 0x00003CDD
	public void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00005AED File Offset: 0x00003CED
	public void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0003B160 File Offset: 0x00039360
	public static bool isExistNewMob(string id)
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

	// Token: 0x0600073E RID: 1854 RVA: 0x00060C30 File Offset: 0x0005EE30
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			if (Mob.arrMobTemplate[i].data != null)
			{
				num++;
			}
		}
		if (num >= 10)
		{
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				if (Mob.arrMobTemplate[j].data != null && num > 5)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00006995 File Offset: 0x00004B95
	public void checkFrameTick(int[] array)
	{
		this.tick++;
		if (this.tick > array.Length - 1)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00060CB4 File Offset: 0x0005EEB4
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

	// Token: 0x06000741 RID: 1857 RVA: 0x00060DEC File Offset: 0x0005EFEC
	private void paintShadow(mGraphics g)
	{
		int num = (int)TileMap.size;
		if (TileMap.tileTypeAt(this.xSd + num / 2, this.ySd + 1, 4))
		{
			g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt((this.xSd - num / 2) / num, (this.ySd + 1) / num) == 0)
		{
			g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, 100, 100);
		}
		else if (TileMap.tileTypeAt((this.xSd + num / 2) / num, (this.ySd + 1) / num) == 0)
		{
			g.setClip(this.xSd / num * num, (this.ySd - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt(this.xSd - num / 2, this.ySd + 1, 8))
		{
			g.setClip(this.xSd / 24 * num, (this.ySd - 30) / num * num, num, 100);
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00060F4C File Offset: 0x0005F14C
	public void updateSuperEff()
	{
		if (this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		if (this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00060FC4 File Offset: 0x0005F1C4
	public virtual void update()
	{
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this.x, this.y, 1);
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
		}
		if (!GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
				{
					global::Char char2 = new global::Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					if (@char.cgender == 0)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char2, 25);
					}
				}
			}
			if (global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32)
			{
				global::Char char3 = new global::Char();
				char3.cx = global::Char.myCharz().cx;
				char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
				if (global::Char.myCharz().cgender == 0)
				{
					MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char3, 25);
				}
			}
		}
		if (this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
		{
			EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			long num = mSystem.currentTimeMillis();
			if (num - this.last >= 1000L)
			{
				this.seconds--;
				this.last = num;
				if (this.seconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
				}
			}
			if (this.templateId >= 58 && this.templateId <= 65)
			{
				if (GameCanvas.gameTick % 20 > 5)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (GameCanvas.gameTick % 20 > 5)
			{
				this.frame = 11;
			}
			else
			{
				this.frame = 10;
			}
		}
		if (!this.isUpdate())
		{
			return;
		}
		if (this.isShadown)
		{
			this.updateShadown();
		}
		if (this.vMobMove == null && (int)Mob.arrMobTemplate[this.templateId].rangeMove != 0)
		{
			return;
		}
		if (this.status != 3 && this.isBusyAttackSomeOne)
		{
			if (this.cFocus != null)
			{
				this.cFocus.doInjure(this.dame, this.dameMp, false, true);
			}
			else if (this.mobToAttack != null)
			{
				this.mobToAttack.setInjure();
			}
			this.isBusyAttackSomeOne = false;
		}
		if ((int)this.levelBoss > 0)
		{
			this.updateSuperEff();
		}
		switch (this.status)
		{
		case 1:
			this.isDisable = false;
			this.isDontMove = false;
			this.isFire = false;
			this.isIce = false;
			this.isWind = false;
			this.y += this.p1;
			if (GameCanvas.gameTick % 2 == 0)
			{
				if (this.p2 > 1)
				{
					this.p2--;
				}
				else if (this.p2 < -1)
				{
					this.p2++;
				}
			}
			this.x += this.p2;
			if (this.templateId >= 58 && this.templateId <= 65)
			{
				this.frame = 15;
			}
			else
			{
				this.frame = 11;
			}
			if (this.isDie)
			{
				this.isDie = false;
				if (this.isMobMe)
				{
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						if (((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId)
						{
							GameScr.vMob.removeElementAt(j);
						}
					}
				}
				this.p1 = 0;
				this.p2 = 0;
				this.x = (this.y = 0);
				this.hp = this.getTemplate().hp;
				this.status = 0;
				this.timeStatus = 0;
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2)
			{
				this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
				if (this.p3 == 0)
				{
					this.p3 = 16;
				}
			}
			else
			{
				this.p1++;
			}
			if (this.p3 > 0)
			{
				this.p3--;
				if (this.p3 == 0)
				{
					this.isDie = true;
				}
			}
			break;
		case 2:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			this.timeStatus = 0;
			this.updateMobStandWait();
			break;
		case 3:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			this.updateMobAttack();
			break;
		case 4:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				return;
			}
			this.timeStatus = 0;
			this.p1++;
			if (this.p1 > 40 + this.mobId % 5)
			{
				this.y -= 2;
				this.status = 5;
				this.p1 = 0;
			}
			break;
		case 5:
			if (this.holdEffID != 0)
			{
				return;
			}
			if (this.blindEff)
			{
				return;
			}
			if (this.sleepEff)
			{
				return;
			}
			if (this.isFreez)
			{
				if ((int)Mob.arrMobTemplate[this.templateId].type == 4)
				{
					this.ty++;
					this.wt++;
					this.fy += (this.wy ? -1 : 1);
					if (this.wt == 10)
					{
						this.wt = 0;
						this.wy = !this.wy;
					}
				}
				return;
			}
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

	// Token: 0x06000744 RID: 1860 RVA: 0x000617A4 File Offset: 0x0005F9A4
	public void setInjure()
	{
		if (this.hp > 0 && this.status != 3)
		{
			this.timeStatus = 4;
			this.status = 7;
			if ((int)this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00061818 File Offset: 0x0005FA18
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00061864 File Offset: 0x0005FA64
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss2)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x000618B0 File Offset: 0x0005FAB0
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BachTuoc)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x000618FC File Offset: 0x0005FAFC
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob is BigBoss)
			{
				GameScr.vMob.removeElement(mob);
				return;
			}
		}
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0006194C File Offset: 0x0005FB4C
	public void setAttack(global::Char cFocus)
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

	// Token: 0x0600074A RID: 1866 RVA: 0x00061A2C File Offset: 0x0005FC2C
	private void updateInjure()
	{
		if (!this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
		{
			if (this.templateId >= 58 && this.templateId <= 65)
			{
				if (this.frame != 1)
				{
					this.frame = 1;
				}
				else
				{
					this.frame = 15;
				}
			}
			else if (this.frame != 10)
			{
				this.frame = 10;
			}
			else
			{
				this.frame = 11;
			}
		}
		this.timeStatus--;
		if (this.timeStatus <= 0 && ((this.templateId >= 58 && this.templateId <= 65 && this.frame == 15) || (this.templateId < 58 && this.frame == 11)))
		{
			if ((this.injureBy != null && this.injureThenDie) || this.hp == 0)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				if (this.injureBy != null)
				{
					this.dir = -this.injureBy.cdir;
					if (Res.abs(this.x - this.injureBy.cx) < 24)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
			return;
		}
		if ((int)Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null)
		{
			int num = -this.injureBy.cdir << 1;
			if (this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.x -= num;
			}
		}
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00061C54 File Offset: 0x0005FE54
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		switch (Mob.arrMobTemplate[this.templateId].type)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			this.p1++;
			if (this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
			break;
		case 4:
		case 5:
			this.p1++;
			if (this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80))
			{
				this.status = 5;
			}
			break;
		}
		if (this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.cFocus.cx > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else if (this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0)
		{
			if (this.mobToAttack.x > this.x)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		if (this.forceWait > 0)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00061E60 File Offset: 0x00060060
	public void updateMobAttack()
	{
		if (this.tick < 2)
		{
			this.checkFrameTick((this.p3 != 0) ? this.attack2 : this.attack1);
			if (this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		if (this.p1 == 0)
		{
			int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
			int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
			if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.p1 = 1;
			}
			if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
			{
				this.p1 = 1;
			}
			if (((int)Mob.arrMobTemplate[this.templateId].type == 4 || (int)Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			if (this.p2 > 3 || this.p1 == 1)
			{
				this.p1 = 1;
				if (this.p3 == 0)
				{
					if (this.cFocus != null)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						this.mobToAttack.setInjure();
					}
					this.isBusyAttackSomeOne = false;
				}
				else
				{
					if (this.cFocus != null)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
					}
					else
					{
						global::Char @char = new global::Char();
						@char.cx = this.mobToAttack.x;
						@char.cy = this.mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
					}
					this.isBusyAttackSomeOne = false;
				}
			}
			this.dir = ((this.x >= num) ? -1 : 1);
		}
		else if (this.p1 == 1)
		{
			if ((int)Mob.arrMobTemplate[this.templateId].type != 0 && !this.isDontMove && !this.isIce && !this.isWind)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
			if (Res.abs(this.xFirst - this.x) < 5 && Res.abs(this.yFirst - this.y) < 5 && this.tick == 2)
			{
				this.status = 2;
				this.p1 = 0;
				this.p2 = 0;
				this.tick = 0;
			}
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00062220 File Offset: 0x00060420
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			if (this.injureThenDie)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			if (!this.isIce)
			{
				if (this.isDontMove || this.isWind)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
						this.frame = 0;
						num = 2;
						break;
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						if ((int)b == 1)
						{
							if (GameCanvas.gameTick % 2 == 1)
							{
								break;
							}
						}
						else if ((int)b > 2)
						{
							b = (sbyte)((int)b + (int)((sbyte)(this.mobId % 2)));
						}
						else if (GameCanvas.gameTick % 2 == 1)
						{
							b -= 1;
						}
						this.x += (int)b * this.dir;
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
						}
						if (Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : -1);
							if (Res.abs(this.x - global::Char.myCharz().cx) < 20)
							{
								this.x -= this.dir * 10;
							}
							this.status = 2;
							this.forceWait = 20;
						}
						this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
						break;
					}
					case 4:
					{
						num = 4;
						sbyte b2 = Mob.arrMobTemplate[this.templateId].speed;
						b2 = (sbyte)((int)b2 + (int)((sbyte)(this.mobId % 2)));
						this.x += (int)b2 * this.dir;
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 = (sbyte)((int)b2 + (int)((sbyte)((GameCanvas.gameTick + this.mobId) % 2)));
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed;
						b3 = (sbyte)((int)b3 + (int)((sbyte)(this.mobId % 2)));
						this.x += (int)b3 * this.dir;
						b3 = (sbyte)((int)b3 + (int)((sbyte)((GameCanvas.gameTick + this.mobId) % 2)));
						if (GameCanvas.gameTick % 10 > 2)
						{
							this.y += (int)b3 * this.dirV;
						}
						if (this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else if (this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove)
						{
							this.dir = 1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						if (this.y > this.yFirst + 24)
						{
							this.dirV = -1;
						}
						else if (this.y < this.yFirst - (20 + GameCanvas.gameTick % 10))
						{
							this.dirV = 1;
						}
						if (TileMap.tileTypeAt(this.x, this.y, 2))
						{
							if (GameCanvas.gameTick % 10 > 5)
							{
								this.y = TileMap.tileYofPixel(this.y);
								this.status = 4;
								this.p1 = 0;
								this.dirV = -1;
							}
							else
							{
								this.dirV = -1;
							}
						}
						break;
					}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("lineee: " + num);
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000069CA File Offset: 0x00004BCA
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x000627D0 File Offset: 0x000609D0
	public bool isPaint()
	{
		return this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameScr.gW && this.y >= GameScr.cmy && this.y <= GameScr.cmy + GameScr.gH + 30 && Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.img != null && this.status != 0;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x000069D8 File Offset: 0x00004BD8
	public bool isUpdate()
	{
		return Mob.arrMobTemplate[this.templateId] != null && Mob.arrMobTemplate[this.templateId].data != null && this.status != 0;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00005BA2 File Offset: 0x00003DA2
	public bool checkIsBoss()
	{
		return this.isBoss || (int)this.levelBoss > 0;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0006288C File Offset: 0x00060A8C
	public virtual void paint(mGraphics g)
	{
		if (this.isShadown && this.status != 0)
		{
			this.paintShadow(g);
		}
		if (!this.isPaint())
		{
			return;
		}
		if (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0)
		{
			return;
		}
		g.translate(0, GameCanvas.transY);
		if (!this.changBody)
		{
			Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 14, 0, 3);
		}
		g.translate(0, -GameCanvas.transY);
		if (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.Equals(this) && this.status != 1)
		{
			int num = (int)((long)this.hp * 100L / (long)this.maxHp) / 10 - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 9)
			{
				num = 9;
			}
			g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, this.x, this.y - this.h - 10, 3);
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00005BBF File Offset: 0x00003DBF
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00062A04 File Offset: 0x00060C04
	public void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00062A54 File Offset: 0x00060C54
	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x <= this.x) ? -1 : 1);
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		if (Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2)
		{
			if (this.x < num)
			{
				this.x = num - this.w;
			}
			else
			{
				this.x = num + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00005C00 File Offset: 0x00003E00
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00006A13 File Offset: 0x00004C13
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00006A1B File Offset: 0x00004C1B
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00006A23 File Offset: 0x00004C23
	public int getW()
	{
		return this.w;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00062B34 File Offset: 0x00060D34
	public void stopMoving()
	{
		if (this.status == 5)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00005C17 File Offset: 0x00003E17
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00005C30 File Offset: 0x00003E30
	public void removeHoldEff()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00006A2B File Offset: 0x00004C2B
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00006A34 File Offset: 0x00004C34
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x04000DA0 RID: 3488
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000DA1 RID: 3489
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000DA2 RID: 3490
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000DA3 RID: 3491
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000DA4 RID: 3492
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000DA5 RID: 3493
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000DA6 RID: 3494
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000DA7 RID: 3495
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000DA8 RID: 3496
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000DA9 RID: 3497
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000DAA RID: 3498
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000DAB RID: 3499
	public const sbyte MA_WALK = 5;

	// Token: 0x04000DAC RID: 3500
	public const sbyte MA_FALL = 6;

	// Token: 0x04000DAD RID: 3501
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000DAE RID: 3502
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000DAF RID: 3503
	public bool changBody;

	// Token: 0x04000DB0 RID: 3504
	public short smallBody;

	// Token: 0x04000DB1 RID: 3505
	public bool isHintFocus;

	// Token: 0x04000DB2 RID: 3506
	public string flystring;

	// Token: 0x04000DB3 RID: 3507
	public int flyx;

	// Token: 0x04000DB4 RID: 3508
	public int flyy;

	// Token: 0x04000DB5 RID: 3509
	public int flyIndex;

	// Token: 0x04000DB6 RID: 3510
	public bool isFreez;

	// Token: 0x04000DB7 RID: 3511
	public int seconds;

	// Token: 0x04000DB8 RID: 3512
	public long last;

	// Token: 0x04000DB9 RID: 3513
	public long cur;

	// Token: 0x04000DBA RID: 3514
	public int holdEffID;

	// Token: 0x04000DBB RID: 3515
	public int hp;

	// Token: 0x04000DBC RID: 3516
	public int maxHp;

	// Token: 0x04000DBD RID: 3517
	public int x;

	// Token: 0x04000DBE RID: 3518
	public int y;

	// Token: 0x04000DBF RID: 3519
	public int dir = 1;

	// Token: 0x04000DC0 RID: 3520
	public int dirV = 1;

	// Token: 0x04000DC1 RID: 3521
	public int status;

	// Token: 0x04000DC2 RID: 3522
	public int p1;

	// Token: 0x04000DC3 RID: 3523
	public int p2;

	// Token: 0x04000DC4 RID: 3524
	public int p3;

	// Token: 0x04000DC5 RID: 3525
	public int xFirst;

	// Token: 0x04000DC6 RID: 3526
	public int yFirst;

	// Token: 0x04000DC7 RID: 3527
	public int vy;

	// Token: 0x04000DC8 RID: 3528
	public int exp;

	// Token: 0x04000DC9 RID: 3529
	public int w;

	// Token: 0x04000DCA RID: 3530
	public int h;

	// Token: 0x04000DCB RID: 3531
	public int hpInjure;

	// Token: 0x04000DCC RID: 3532
	public int charIndex;

	// Token: 0x04000DCD RID: 3533
	public int timeStatus;

	// Token: 0x04000DCE RID: 3534
	public int mobId;

	// Token: 0x04000DCF RID: 3535
	public bool isx;

	// Token: 0x04000DD0 RID: 3536
	public bool isy;

	// Token: 0x04000DD1 RID: 3537
	public bool isDisable;

	// Token: 0x04000DD2 RID: 3538
	public bool isDontMove;

	// Token: 0x04000DD3 RID: 3539
	public bool isFire;

	// Token: 0x04000DD4 RID: 3540
	public bool isIce;

	// Token: 0x04000DD5 RID: 3541
	public bool isWind;

	// Token: 0x04000DD6 RID: 3542
	public bool isDie;

	// Token: 0x04000DD7 RID: 3543
	public MyVector vMobMove = new MyVector();

	// Token: 0x04000DD8 RID: 3544
	public bool isGo;

	// Token: 0x04000DD9 RID: 3545
	public string mobName;

	// Token: 0x04000DDA RID: 3546
	public int templateId;

	// Token: 0x04000DDB RID: 3547
	public short pointx;

	// Token: 0x04000DDC RID: 3548
	public short pointy;

	// Token: 0x04000DDD RID: 3549
	public global::Char cFocus;

	// Token: 0x04000DDE RID: 3550
	public int dame;

	// Token: 0x04000DDF RID: 3551
	public int dameMp;

	// Token: 0x04000DE0 RID: 3552
	public int sys;

	// Token: 0x04000DE1 RID: 3553
	public sbyte levelBoss;

	// Token: 0x04000DE2 RID: 3554
	public sbyte level;

	// Token: 0x04000DE3 RID: 3555
	public bool isBoss;

	// Token: 0x04000DE4 RID: 3556
	public bool isMobMe;

	// Token: 0x04000DE5 RID: 3557
	public static MyVector lastMob = new MyVector();

	// Token: 0x04000DE6 RID: 3558
	public static MyVector newMob = new MyVector();

	// Token: 0x04000DE7 RID: 3559
	public int xSd;

	// Token: 0x04000DE8 RID: 3560
	public int ySd;

	// Token: 0x04000DE9 RID: 3561
	private bool isOutMap;

	// Token: 0x04000DEA RID: 3562
	private int wCount;

	// Token: 0x04000DEB RID: 3563
	public bool isShadown = true;

	// Token: 0x04000DEC RID: 3564
	private int tick;

	// Token: 0x04000DED RID: 3565
	private int frame;

	// Token: 0x04000DEE RID: 3566
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000DEF RID: 3567
	private bool wy;

	// Token: 0x04000DF0 RID: 3568
	private int wt;

	// Token: 0x04000DF1 RID: 3569
	private int fy;

	// Token: 0x04000DF2 RID: 3570
	private int ty;

	// Token: 0x04000DF3 RID: 3571
	public int typeSuperEff;

	// Token: 0x04000DF4 RID: 3572
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04000DF5 RID: 3573
	public int[] stand = new int[]
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

	// Token: 0x04000DF6 RID: 3574
	public int[] move = new int[]
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

	// Token: 0x04000DF7 RID: 3575
	public int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000DF8 RID: 3576
	public int[] attack1 = new int[]
	{
		4,
		5,
		6
	};

	// Token: 0x04000DF9 RID: 3577
	public int[] attack2 = new int[]
	{
		7,
		8,
		9
	};

	// Token: 0x04000DFA RID: 3578
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000DFB RID: 3579
	public global::Char injureBy;

	// Token: 0x04000DFC RID: 3580
	public bool injureThenDie;

	// Token: 0x04000DFD RID: 3581
	public Mob mobToAttack;

	// Token: 0x04000DFE RID: 3582
	public int forceWait;

	// Token: 0x04000DFF RID: 3583
	public bool blindEff;

	// Token: 0x04000E00 RID: 3584
	public bool sleepEff;
}
