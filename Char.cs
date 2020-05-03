using System;
using Assets.src.g;

// Token: 0x02000094 RID: 148
public class Char : IMapObject
{
	// Token: 0x06000562 RID: 1378 RVA: 0x0003D9AC File Offset: 0x0003BBAC
	public Char()
	{
		this.statusMe = 6;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0003E7D0 File Offset: 0x0003C9D0
	public void applyCharLevelPercent()
	{
		try
		{
			long num = 1L;
			long num2 = 0L;
			int num3 = 0;
			for (int i = GameScr.exps.Length - 1; i >= 0; i--)
			{
				if (this.cPower >= GameScr.exps[i])
				{
					if (i == GameScr.exps.Length - 1)
					{
						num = 1L;
					}
					else
					{
						num = GameScr.exps[i + 1] - GameScr.exps[i];
					}
					num2 = this.cPower - GameScr.exps[i];
					num3 = i;
					break;
				}
			}
			this.clevel = num3;
			this.cLevelPercent = (long)((int)(num2 * 10000L / num));
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi char level percent: " + ex.ToString());
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00005E15 File Offset: 0x00004015
	public int getdxSkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dx;
		}
		return 0;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00005E2C File Offset: 0x0000402C
	public int getdySkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dy;
		}
		return 0;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0003E884 File Offset: 0x0003CA84
	public static void taskAction(bool isNextStep)
	{
		Task task = global::Char.myCharz().taskMaint;
		if (task.index > task.contentInfo.Length - 1)
		{
			task.index = task.contentInfo.Length - 1;
		}
		string text = task.contentInfo[task.index];
		if (text != null && !text.Equals(string.Empty))
		{
			if (text.StartsWith("#"))
			{
				text = NinjaUtil.replace(text, "#", string.Empty);
				Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				npc.cx = (npc.cy = -100);
				npc.avatar = GameScr.info1.charId[global::Char.myCharz().cgender][2];
				npc.charID = 5;
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					ChatPopup.addNextPopUpMultiLine(text, npc);
				}
			}
			else if (isNextStep)
			{
				GameScr.info1.addInfo(text, 0);
			}
		}
		GameScr.isHaveSelectSkill = true;
		Cout.println("TASKx " + global::Char.myCharz().taskMaint.taskId);
		if (global::Char.myCharz().taskMaint.taskId <= 2)
		{
			global::Char.myCharz().canFly = false;
		}
		else
		{
			global::Char.myCharz().canFly = true;
		}
		GameScr.gI().left = null;
		if (task.taskId == 0)
		{
			Hint.isViewMap = false;
			GameScr.gI().right = null;
			GameScr.isHaveSelectSkill = false;
			GameScr.gI().left = null;
			if (task.index < 4)
			{
				MagicTree.isPaint = false;
				GameScr.isPaintRada = -1;
			}
			if (task.index == 4)
			{
				GameScr.isPaintRada = 1;
				MagicTree.isPaint = true;
			}
			if (task.index >= 5)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
			}
		}
		if (task.taskId == 1)
		{
			GameScr.isHaveSelectSkill = true;
		}
		if (task.taskId >= 1)
		{
			GameScr.gI().right = GameScr.gI().cmdFocus;
			GameScr.gI().left = GameScr.gI().cmdMenu;
		}
		if (task.taskId >= 0)
		{
			Panel.isPaintMap = true;
		}
		else
		{
			Panel.isPaintMap = false;
		}
		if (task.taskId < 12)
		{
			GameCanvas.panel.mainTabName = mResources.mainTab1;
		}
		else
		{
			GameCanvas.panel.mainTabName = mResources.mainTab2;
		}
		GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
		if (global::Char.myChar.taskMaint.taskId > 10)
		{
			Rms.saveRMSString("fake", "aa");
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0003EB08 File Offset: 0x0003CD08
	public string getStrLevel()
	{
		return string.Concat(new object[]
		{
			this.strLevel[this.clevel],
			"+",
			this.cLevelPercent / 100L,
			".",
			this.cLevelPercent % 100L,
			"%"
		});
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00005E43 File Offset: 0x00004043
	public int avatarz()
	{
		return this.getAvatar(this.head);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0003EB6C File Offset: 0x0003CD6C
	public int getAvatar(int headId)
	{
		for (int i = 0; i < global::Char.idHead.Length; i++)
		{
			if (headId == (int)global::Char.idHead[i])
			{
				return (int)global::Char.idAvatar[i];
			}
		}
		return -1;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
	public void setPowerInfo(string info, short p, short maxP, short sc)
	{
		this.powerPoint = p;
		this.strInfo = info;
		this.maxPowerPoint = maxP;
		this.secondPower = sc;
		this.lastS = (this.currS = mSystem.currentTimeMillis());
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00003584 File Offset: 0x00001784
	public void addInfo(string info)
	{
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0003EBE0 File Offset: 0x0003CDE0
	public int getSys()
	{
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			return 1;
		}
		if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			return 2;
		}
		if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			return 3;
		}
		return 0;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00005E51 File Offset: 0x00004051
	public static global::Char myCharz()
	{
		if (global::Char.myChar == null)
		{
			global::Char.myChar = new global::Char();
			global::Char.myChar.me = true;
			global::Char.myChar.cmtoChar = true;
		}
		return global::Char.myChar;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00005E7F File Offset: 0x0000407F
	public static global::Char myPetz()
	{
		if (global::Char.myPet == null)
		{
			global::Char.myPet = new global::Char();
			global::Char.myPet.me = false;
		}
		return global::Char.myPet;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00005EA2 File Offset: 0x000040A2
	public static void clearMyChar()
	{
		global::Char.myChar = null;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0003EC48 File Offset: 0x0003CE48
	public void bagSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				Item item = this.arrItemBag[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBag[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBag.Length; l++)
			{
				if (this.arrItemBag[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBag[m] == null)
						{
							this.arrItemBag[m] = this.arrItemBag[l];
							this.arrItemBag[m].indexUI = m;
							this.arrItemBag[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.bagSort()");
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
	public void boxSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBox.Length; i++)
			{
				Item item = this.arrItemBox[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBox[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBox.Length; l++)
			{
				if (this.arrItemBox[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBox[m] == null)
						{
							this.arrItemBox[m] = this.arrItemBox[l];
							this.arrItemBox[m].indexUI = m;
							this.arrItemBox[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.boxSort()");
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0003EF60 File Offset: 0x0003D160
	public void useItem(int indexUI)
	{
		Item item = this.arrItemBag[indexUI];
		if (item.isTypeBody())
		{
			item.isLock = true;
			item.typeUI = 5;
			Item item2 = this.arrItemBody[(int)item.template.type];
			this.arrItemBag[indexUI] = null;
			if (item2 != null)
			{
				item2.typeUI = 3;
				this.arrItemBody[(int)item.template.type] = null;
				item2.indexUI = indexUI;
				this.arrItemBag[indexUI] = item2;
			}
			item.indexUI = (int)item.template.type;
			this.arrItemBody[item.indexUI] = item;
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				Item item3 = this.arrItemBody[i];
				if (item3 != null)
				{
					if (item3.template.type == 0)
					{
						this.body = (int)item3.template.part;
					}
					else if (item3.template.type == 1)
					{
						this.leg = (int)item3.template.part;
					}
				}
			}
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0003F058 File Offset: 0x0003D258
	public Skill getSkill(SkillTemplate skillTemplate)
	{
		for (int i = 0; i < this.vSkill.size(); i++)
		{
			if (((Skill)this.vSkill.elementAt(i)).template.id == skillTemplate.id)
			{
				return (Skill)this.vSkill.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0003F0B4 File Offset: 0x0003D2B4
	public Waypoint isInEnterOfflinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
			{
				return null;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline)
			{
				return waypoint;
			}
			b += 1;
		}
		return null;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0003F184 File Offset: 0x0003D384
	public Waypoint isInEnterOnlinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
			{
				return null;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline)
			{
				return waypoint;
			}
			b += 1;
		}
		return null;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0003F254 File Offset: 0x0003D454
	public bool isInWaypoint()
	{
		if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
		{
			return true;
		}
		if (this.isTeleport || this.isUsePlane)
		{
			return false;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if ((TileMap.mapID == 47 || TileMap.isInAirMap()) && this.cy <= (int)(waypoint.minY + waypoint.maxY) && this.cx > (int)waypoint.minX && this.cx < (int)waypoint.maxX)
			{
				return !TileMap.isInAirMap() || this.cTypePk == 0;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && !waypoint.isEnter)
			{
				return true;
			}
			b += 1;
		}
		return false;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0003F354 File Offset: 0x0003D554
	public bool isPunchKickSkill()
	{
		return this.skillPaint != null && ((this.skillPaint.id >= 0 && this.skillPaint.id <= 6) || (this.skillPaint.id >= 14 && this.skillPaint.id <= 20) || (this.skillPaint.id >= 28 && this.skillPaint.id <= 34) || (this.skillPaint.id >= 63 && this.skillPaint.id <= 69));
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
	public void soundUpdate()
	{
		if (this.me && this.statusMe == 10 && this.cf == 8 && this.ty > 20 && GameCanvas.gameTick % 20 == 0)
		{
			SoundMn.gI().charFly();
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length && this.isPunchKickSkill() && (this.me || (!this.me && this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
		{
			if (this.cf == 9 || this.cf == 10 || this.cf == 11)
			{
				SoundMn.gI().charPunch(true, (!this.me) ? 0.05f : 0.1f);
				return;
			}
			SoundMn.gI().charPunch(false, (!this.me) ? 0.05f : 0.1f);
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00003584 File Offset: 0x00001784
	public void updateChargeSkill()
	{
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0003F4FC File Offset: 0x0003D6FC
	public virtual void update()
	{
		if (this.isHide)
		{
			return;
		}
		if (this.isMabuHold)
		{
			return;
		}
		if ((this.isCopy || this.clevel >= 14) && this.statusMe != 1)
		{
			int num = this.statusMe;
		}
		if (this.petFollow != null)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					this.petFollow.cmtoX = this.cx - 20;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					this.petFollow.cmtoX = this.cx + 20;
				}
				this.petFollow.cmtoY = this.cy - 40;
				if (this.petFollow.cmx > this.cx)
				{
					this.petFollow.dir = -1;
				}
				else
				{
					this.petFollow.dir = 1;
				}
				if (this.petFollow.cmtoX < 100)
				{
					this.petFollow.cmtoX = 100;
				}
				if (this.petFollow.cmtoX > TileMap.pxw - 100)
				{
					this.petFollow.cmtoX = TileMap.pxw - 100;
				}
			}
			this.petFollow.update();
		}
		if (!this.me && this.cHP <= 0 && this.clanID != -100 && this.statusMe != 14 && this.statusMe != 5)
		{
			this.startDie((short)this.cx, (short)this.cy);
		}
		if (this.isInjureHp)
		{
			this.twHp++;
			if (this.twHp == 20)
			{
				this.twHp = 0;
				this.isInjureHp = false;
			}
		}
		else if (this.dHP > this.cHP)
		{
			int num2 = this.dHP - this.cHP >> 1;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.dHP -= num2;
		}
		else
		{
			this.dHP = this.cHP;
		}
		if (this.secondPower != 0)
		{
			this.currS = mSystem.currentTimeMillis();
			if (this.currS - this.lastS >= 1000L)
			{
				this.lastS = mSystem.currentTimeMillis();
				this.secondPower -= 1;
			}
		}
		if (!this.me && GameScr.notPaint)
		{
			return;
		}
		if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
		{
			EffecMn.addEff(new Effect(41, this.cx, this.cy, 3, 1, 1));
		}
		if (this.huytSao)
		{
			this.huytSao = false;
			EffecMn.addEff(new Effect(39, this.cx, this.cy, 3, 3, 1));
		}
		if (this.blindEff && GameCanvas.gameTick % 5 == 0)
		{
			ServerEffect.addServerEffect(113, this, 1);
		}
		if (this.protectEff)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				this.eProtect = new Effect(33, this.cx, this.cy + 37, 3, 3, 1);
			}
			if (this.eProtect != null)
			{
				this.eProtect.update();
				this.eProtect.x = this.cx;
				this.eProtect.y = this.cy + 37;
			}
		}
		if (this.charFocus != null && this.charFocus.cy < 0)
		{
			this.charFocus = null;
		}
		if (this.isFusion)
		{
			this.tFusion++;
		}
		if (this.isNhapThe && GameCanvas.gameTick % 25 == 0)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		if (this.isSetPos)
		{
			this.tpos++;
			if (this.tpos == 1)
			{
				this.tpos = 0;
				this.isSetPos = false;
				this.cx = (int)this.xPos;
				this.cy = (int)this.yPos;
				this.cp1 = (this.cp2 = (this.cp3 = 0));
				if (this.typePos == 1)
				{
					if (this.me)
					{
						this.cxSend = this.cx;
						this.cySend = this.cy;
					}
					this.currentMovePoint = null;
					this.telePortSkill = false;
					ServerEffect.addServerEffect(173, this.cx, this.cy, 1);
				}
				else
				{
					ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
				}
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
					return;
				}
				this.statusMe = 4;
			}
			return;
		}
		this.soundUpdate();
		if (this.stone)
		{
			return;
		}
		if (this.isFreez)
		{
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this.cx, this.cy, 1);
			}
			this.cf = 23;
			long num3 = mSystem.currentTimeMillis();
			if (num3 - this.lastFreez >= 1000L)
			{
				this.freezSeconds--;
				this.lastFreez = num3;
				if (this.freezSeconds < 0)
				{
					this.isFreez = false;
					this.seconds = 0;
					if (this.me)
					{
						global::Char.myCharz().isLockMove = false;
						GameScr.gI().dem = 0;
						GameScr.gI().isFreez = false;
					}
				}
			}
			if (TileMap.tileTypeAt(this.cx / (int)TileMap.size, this.cy / (int)TileMap.size) == 0)
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
		if (this.isWaitMonkey)
		{
			this.isLockMove = true;
			this.cf = 17;
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(154, this.cx, this.cy - 10, 2);
			}
			if (GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(1, this.cx, this.cy + 10, 1);
			}
			this.chargeCount++;
			if (this.chargeCount == 500)
			{
				this.isWaitMonkey = false;
				this.isLockMove = false;
			}
			return;
		}
		if (this.isStandAndCharge)
		{
			this.chargeCount++;
			bool flag = !TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
			this.updateEffect();
			this.updateSkillPaint();
			this.moveFast = null;
			this.currentMovePoint = null;
			this.cf = 17;
			if (flag && this.cgender != 2)
			{
				this.cf = 12;
			}
			if (this.cgender == 2)
			{
				if (GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(154, this.cx, this.cy - this.ch / 2 + 10, 1);
				}
				if (GameCanvas.gameTick % 5 == 0)
				{
					ServerEffect.addServerEffect(114, this.cx + Res.random(-20, 20), this.cy + Res.random(-20, 20), 1);
				}
			}
			if (this.cgender == 1)
			{
				int num4 = GameCanvas.gameTick % 4;
				if (GameCanvas.gameTick % 2 == 0)
				{
					if (this.cdir == 1)
					{
						ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
						ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
					}
					else
					{
						ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
						ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
					}
				}
			}
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
			{
				this.stopUseChargeSkill();
				if (this.me)
				{
					GameScr.gI().auto = 0;
					if (this.cgender == 2)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
						Service.gI().skill_not_focus(8);
					}
					if (this.cgender == 1)
					{
						Res.outz("set skipp paint");
						this.isCreateDark = true;
						global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
					}
				}
				else if (this.cgender == 2)
				{
					this.setAutoSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
				}
				if (this.cgender == 2 && this.statusMe != 14 && this.statusMe != 5)
				{
					GameScr.gI().activeSuperPower(this.cx, this.cy);
				}
			}
			this.chargeCount++;
			if (this.chargeCount == 500)
			{
				this.stopUseChargeSkill();
			}
			return;
		}
		if (this.isFlyAndCharge)
		{
			this.updateEffect();
			this.updateSkillPaint();
			this.moveFast = null;
			this.currentMovePoint = null;
			this.posDisY++;
			if (TileMap.tileTypeAt(this.cx, this.cy - this.ch, 8192))
			{
				this.stopUseChargeSkill();
				return;
			}
			if (this.posDisY == 20)
			{
				this.last = mSystem.currentTimeMillis();
			}
			if (this.posDisY <= 20)
			{
				if (this.statusMe != 14)
				{
					this.statusMe = 3;
				}
				this.cvy = -3;
				this.cy += this.cvy;
				this.cf = 7;
				return;
			}
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
			{
				this.isFlyAndCharge = false;
				if (this.me)
				{
					this.isCreateDark = true;
					bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
					this.isUseSkillAfterCharge = true;
					global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag2) ? 1 : 0);
				}
				return;
			}
			this.cf = 32;
			if (this.cgender == 0 && GameCanvas.gameTick % 3 == 0)
			{
				ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
			}
			this.chargeCount++;
			if (this.chargeCount == 500)
			{
				this.stopUseChargeSkill();
			}
			return;
		}
		else
		{
			if (this.me && GameCanvas.isTouch)
			{
				if (this.charFocus != null && this.charFocus.charID >= 0 && this.charFocus.cx > 100 && this.charFocus.cx < TileMap.pxw - 100 && this.isInEnterOnlinePoint() == null && this.isInEnterOfflinePoint() == null && !this.isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
				{
					int num5 = global::Math.abs(this.cx - this.charFocus.cx);
					int num6 = global::Math.abs(this.cy - this.charFocus.cy);
					if (num5 < 60 && num6 < 40)
					{
						if (this.cmdMenu == null)
						{
							this.cmdMenu = new Command(mResources.MENU, 11111);
							this.cmdMenu.isPlaySoundButton = false;
						}
						this.cmdMenu.x = this.charFocus.cx - GameScr.cmx;
						this.cmdMenu.y = this.charFocus.cy - this.charFocus.ch - 30 - GameScr.cmy;
					}
					else
					{
						this.cmdMenu = null;
					}
				}
				else
				{
					this.cmdMenu = null;
				}
			}
			if (this.isShadown)
			{
				this.updateShadown();
			}
			if (this.isTeleport)
			{
				return;
			}
			if (this.chatInfo != null)
			{
				this.chatInfo.update();
			}
			if (this.shadowLife > 0)
			{
				this.shadowLife--;
			}
			if (this.resultTest > 0 && GameCanvas.gameTick % 2 == 0)
			{
				this.resultTest -= 1;
				if (this.resultTest == 30 || this.resultTest == 60)
				{
					this.resultTest = 0;
				}
			}
			this.updateSkillPaint();
			if (this.mobMe != null)
			{
				this.updateMobMe();
			}
			if (this.arr != null)
			{
				this.arr.update();
			}
			if (this.dart != null)
			{
				this.dart.update();
			}
			this.updateEffect();
			if (this.holdEffID != 0)
			{
				if (GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(32, this.cx, this.cy + 24, 3, 5, 1));
				}
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
			if (!this.holder)
			{
				if (this.cHP > 0)
				{
					for (int i = 0; i < this.vEff.size(); i++)
					{
						EffectChar effectChar = (EffectChar)this.vEff.elementAt(i);
						if (effectChar.template.type == 0 || effectChar.template.type == 12)
						{
							if (GameCanvas.isEff1)
							{
								this.cHP += (int)effectChar.param;
								this.cMP += (int)effectChar.param;
							}
						}
						else if (effectChar.template.type == 4 || effectChar.template.type == 17)
						{
							if (GameCanvas.isEff1)
							{
								this.cHP += (int)effectChar.param;
							}
						}
						else if (effectChar.template.type == 13 && GameCanvas.isEff1)
						{
							this.cHP -= this.cHPFull * 3 / 100;
							if (this.cHP < 1)
							{
								this.cHP = 1;
							}
						}
					}
					if (this.eff5BuffHp > 0 && GameCanvas.isEff2)
					{
						this.cHP += this.eff5BuffHp;
					}
					if (this.eff5BuffMp > 0 && GameCanvas.isEff2)
					{
						this.cMP += this.eff5BuffMp;
					}
					if (this.cHP > this.cHPFull)
					{
						this.cHP = this.cHPFull;
					}
					if (this.cMP > this.cMPFull)
					{
						this.cMP = this.cMPFull;
					}
				}
				if (this.cmtoChar)
				{
					GameScr.cmtoX = this.cx - GameScr.gW2;
					GameScr.cmtoY = this.cy - GameScr.gH23;
					if (!GameCanvas.isTouchControl)
					{
						GameScr.cmtoX += GameScr.gW6 * this.cdir;
					}
				}
				this.tick = (this.tick + 1) % 100;
				if (this.me)
				{
					if (this.charFocus != null && !GameScr.vCharInMap.contains(this.charFocus))
					{
						this.charFocus = null;
					}
					if (this.cx < 10)
					{
						this.cvx = 0;
						this.cx = 10;
					}
					else if (this.cx > TileMap.pxw - 10)
					{
						this.cx = TileMap.pxw - 10;
						this.cvx = 0;
					}
					if (this.me && !global::Char.ischangingMap && this.isInWaypoint())
					{
						Service.gI().charMove();
						if (TileMap.isTrainingMap())
						{
							Service.gI().getMapOffline();
							global::Char.ischangingMap = true;
						}
						else
						{
							Service.gI().requestChangeMap();
						}
						global::Char.isLockKey = true;
						global::Char.ischangingMap = true;
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						InfoDlg.showWait();
						return;
					}
					if (this.statusMe != 4 && Res.abs(this.cx - this.cxSend) + Res.abs(this.cy - this.cySend) >= 70 && this.cy - this.cySend <= 0 && this.me)
					{
						Service.gI().charMove();
					}
					if (this.isLockMove)
					{
						this.currentMovePoint = null;
					}
					if (this.currentMovePoint != null)
					{
						if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 16 && global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 16)
						{
							this.cx = (this.currentMovePoint.xEnd + this.cx) / 2;
							this.cy = this.currentMovePoint.yEnd;
							this.currentMovePoint = null;
							GameScr.instance.clickMoving = false;
							this.checkPerformEndMovePointAction();
							this.cvx = (this.cvy = 0);
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
							{
								this.statusMe = 1;
							}
							else
							{
								this.setCharFallFromJump();
							}
							Service.gI().charMove();
						}
						else
						{
							this.cdir = ((this.currentMovePoint.xEnd <= this.cx) ? -1 : 1);
							if (TileMap.tileTypeAt(this.cx, this.cy, 2))
							{
								this.statusMe = 2;
								if (this.currentMovePoint != null)
								{
									this.cvx = this.cspeed * this.cdir;
									this.cvy = 0;
								}
								if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
								{
									if (this.currentMovePoint.yEnd > this.cy)
									{
										this.currentMovePoint = null;
										GameScr.instance.clickMoving = false;
										this.statusMe = 1;
										this.cvx = (this.cvy = 0);
										this.checkPerformEndMovePointAction();
									}
									else
									{
										SoundMn.gI().charJump();
										this.cx = this.currentMovePoint.xEnd;
										this.statusMe = 10;
										this.cvy = -5;
										this.cvx = 0;
									}
								}
								if (this.cdir == 1)
								{
									if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
									{
										this.cvx = this.cspeed * this.cdir;
										this.statusMe = 10;
										this.cvy = -5;
									}
								}
								else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
								{
									this.cvx = this.cspeed * this.cdir;
									this.statusMe = 10;
									this.cvy = -5;
								}
							}
							else
							{
								if (this.currentMovePoint.yEnd < this.cy + 10)
								{
									this.statusMe = 10;
									this.cvy = -5;
									if (global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 10)
									{
										this.cy = this.currentMovePoint.yEnd;
										this.cvy = 0;
									}
									if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
									{
										this.cvx = 0;
									}
									else
									{
										this.cvx = this.cspeed * this.cdir;
									}
								}
								else if (TileMap.tileTypeAt(this.cx, this.cy, 2))
								{
									this.currentMovePoint = null;
									GameScr.instance.clickMoving = false;
									this.statusMe = 1;
									this.cvx = (this.cvy = 0);
									this.checkPerformEndMovePointAction();
								}
								else
								{
									if (this.statusMe == 10 || this.statusMe == 2)
									{
										this.cvy = 0;
									}
									this.statusMe = 4;
								}
								if (this.currentMovePoint.yEnd > this.cy)
								{
									if (this.cdir == 1)
									{
										if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
										{
											this.cvx = (this.cvy = 0);
											this.statusMe = 4;
											this.currentMovePoint = null;
											GameScr.instance.clickMoving = false;
											this.checkPerformEndMovePointAction();
										}
									}
									else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
									{
										this.cvx = (this.cvy = 0);
										this.statusMe = 4;
										this.currentMovePoint = null;
										GameScr.instance.clickMoving = false;
										this.checkPerformEndMovePointAction();
									}
								}
							}
						}
					}
					this.searchFocus();
				}
				else
				{
					this.checkHideCharName();
					if (this.statusMe == 1 || this.statusMe == 6)
					{
						this.updateSuperEff();
						bool flag3 = false;
						if (this.currentMovePoint != null)
						{
							if (global::Char.abs(this.currentMovePoint.xEnd - this.cx) < 17 && global::Char.abs(this.currentMovePoint.yEnd - this.cy) < 25)
							{
								this.cx = this.currentMovePoint.xEnd;
								this.cy = this.currentMovePoint.yEnd;
								this.currentMovePoint = null;
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
								{
									this.statusMe = 1;
									this.cp3 = 0;
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
								}
								else
								{
									this.statusMe = 4;
									this.cvy = 0;
									this.cp1 = 0;
								}
								flag3 = true;
							}
							else if ((this.statusBeforeNothing == 10 || this.cf == 8) && this.vMovePoints.size() > 0)
							{
								flag3 = true;
							}
							else if (this.cy == this.currentMovePoint.yEnd)
							{
								if (this.cx != this.currentMovePoint.xEnd)
								{
									this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
									this.cf = GameCanvas.gameTick % 5 + 2;
								}
							}
							else if (this.cy < this.currentMovePoint.yEnd)
							{
								this.cf = 12;
								this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
								if (this.cvy < 0)
								{
									this.cvy = 0;
								}
								this.cy += this.cvy;
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
								{
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
								}
								this.cvy++;
								if (this.cvy > 16)
								{
									this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
								}
							}
							else
							{
								this.cf = 7;
								this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
								this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
							}
						}
						else
						{
							flag3 = true;
						}
						if (flag3 && this.vMovePoints.size() > 0)
						{
							this.currentMovePoint = (MovePoint)this.vMovePoints.firstElement();
							this.vMovePoints.removeElementAt(0);
							if (this.currentMovePoint.status == 2)
							{
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 12) & 2) != 2)
								{
									this.statusMe = 10;
									this.cp1 = 0;
									this.cp2 = 0;
									this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
									this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
								}
								else
								{
									this.statusMe = 2;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
									this.cvx = this.cspeed * this.cdir;
									this.cvy = 0;
								}
							}
							else if (this.currentMovePoint.status == 3)
							{
								if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 23) & 2) != 2)
								{
									this.statusMe = 10;
									this.cp1 = 0;
									this.cp2 = 0;
									this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
									this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
								}
								else
								{
									this.statusMe = 3;
									GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
									GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
									if (this.cx - this.currentMovePoint.xEnd > 0)
									{
										this.cdir = -1;
									}
									else if (this.cx - this.currentMovePoint.xEnd < 0)
									{
										this.cdir = 1;
									}
									this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 10 * this.cdir;
									this.cvy = -10;
								}
							}
							else if (this.currentMovePoint.status == 4)
							{
								this.statusMe = 4;
								if (this.cx - this.currentMovePoint.xEnd > 0)
								{
									this.cdir = -1;
								}
								else if (this.cx - this.currentMovePoint.xEnd < 0)
								{
									this.cdir = 1;
								}
								this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 9 * this.cdir;
								this.cvy = 0;
							}
							else
							{
								this.cx = this.currentMovePoint.xEnd;
								this.cy = this.currentMovePoint.yEnd;
								this.currentMovePoint = null;
							}
						}
					}
				}
				switch (this.statusMe)
				{
				case 1:
					this.updateCharStand();
					break;
				case 2:
					this.updateCharRun();
					break;
				case 3:
					this.updateCharJump();
					break;
				case 4:
					this.updateCharFall();
					break;
				case 5:
					this.updateCharDeadFly();
					break;
				case 6:
					if (this.isInjure <= 0)
					{
						this.cf = 0;
					}
					else if (this.statusBeforeNothing == 10)
					{
						this.cx += this.cvx;
					}
					else if (this.cf <= 1)
					{
						this.cp1++;
						if (this.cp1 > 6)
						{
							this.cf = 0;
						}
						else
						{
							this.cf = 1;
						}
						if (this.cp1 > 10)
						{
							this.cp1 = 0;
						}
					}
					if (this.cf != 7 && this.cf != 12 && (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
					{
						this.cvx = 0;
						this.cvy = 0;
						this.statusMe = 4;
						this.cf = 7;
					}
					if (!this.me)
					{
						this.cp3++;
						if (this.cp3 > 10)
						{
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
							{
								this.cy += 5;
							}
							else
							{
								this.cf = 0;
							}
						}
						if (this.cp3 > 50)
						{
							this.cp3 = 0;
							this.currentMovePoint = null;
						}
					}
					break;
				case 9:
					this.updateCharAutoJump();
					break;
				case 10:
					this.updateCharFly();
					break;
				case 12:
					this.updateSkillStand();
					break;
				case 13:
					this.updateSkillFall();
					break;
				case 14:
					this.cp1++;
					if (this.cp1 > 30)
					{
						this.cp1 = 0;
					}
					if (this.cp1 % 15 < 5)
					{
						this.cf = 0;
					}
					else
					{
						this.cf = 1;
					}
					break;
				case 16:
					this.updateResetPoint();
					break;
				}
				if (this.isInjure > 0)
				{
					this.cf = 23;
					this.isInjure -= 1;
				}
				if (this.wdx != 0 || this.wdy != 0)
				{
					this.startDie(this.wdx, this.wdy);
					this.wdx = 0;
					this.wdy = 0;
				}
				if (this.moveFast != null)
				{
					if (this.moveFast[0] == 0)
					{
						short[] array = this.moveFast;
						int num7 = 0;
						int num8 = num7;
						array[num8] += 1;
						ServerEffect.addServerEffect(60, this, 1);
					}
					else if (this.moveFast[0] < 10)
					{
						short[] array2 = this.moveFast;
						int num9 = 0;
						int num10 = num9;
						array2[num10] += 1;
					}
					else
					{
						this.cx = (int)this.moveFast[1];
						this.cy = (int)this.moveFast[2];
						this.moveFast = null;
						ServerEffect.addServerEffect(60, this, 1);
						if (this.me)
						{
							if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
							{
								this.statusMe = 4;
								global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
							}
							else
							{
								Service.gI().charMove();
								global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
							}
						}
					}
				}
				if (this.statusMe != 10)
				{
					this.fy = 0;
				}
				if (this.isCharge)
				{
					this.cf = 17;
					if (GameCanvas.gameTick % 4 == 0)
					{
						ServerEffect.addServerEffect(1, this.cx, this.cy + GameCanvas.transY, 1);
					}
					if (this.me)
					{
						long num11 = mSystem.currentTimeMillis();
						if (num11 - this.last >= 1000L)
						{
							Res.outz("%= " + this.myskill.damage);
							this.last = num11;
							this.cHP += this.cHPFull * (int)this.myskill.damage / 100;
							this.cMP += this.cMPFull * (int)this.myskill.damage / 100;
							if (this.cHP < this.cHPFull)
							{
								GameScr.startFlyText(string.Concat(new object[]
								{
									"+",
									this.cHPFull * (int)this.myskill.damage / 100,
									" ",
									mResources.HP
								}), this.cx, this.cy - this.ch - 20, 0, -1, mFont.HP);
							}
							if (this.cMP < this.cMPFull)
							{
								GameScr.startFlyText(string.Concat(new object[]
								{
									"+",
									this.cMPFull * (int)this.myskill.damage / 100,
									" ",
									mResources.KI
								}), this.cx, this.cy - this.ch - 20, 0, -2, mFont.MP);
							}
							Service.gI().skill_not_focus(2);
						}
					}
				}
				if (this.isFlyUp)
				{
					if (this.me)
					{
						global::Char.isLockKey = true;
						this.statusMe = 3;
						this.cvy = -8;
						if (this.cy <= TileMap.pxh - 240)
						{
							this.isFlyUp = false;
							global::Char.isLockKey = false;
							this.statusMe = 4;
						}
					}
					else
					{
						this.statusMe = 3;
						this.cvy = -8;
						if (this.cy <= TileMap.pxh - 240)
						{
							this.cvy = 0;
							this.isFlyUp = false;
							this.cvy = 0;
							this.statusMe = 1;
						}
					}
				}
				this.updateMount();
				return;
			}
			if (this.charHold != null && (this.charHold.statusMe == 14 || this.charHold.statusMe == 5))
			{
				this.removeHoleEff();
			}
			if (this.mobHold != null && this.mobHold.status == 1)
			{
				this.removeHoleEff();
			}
			if (this.me && this.statusMe == 2 && this.currentMovePoint != null)
			{
				this.holder = false;
				this.charHold = null;
				this.mobHold = null;
			}
			if (TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.cf = 16;
				return;
			}
			this.cf = 31;
			return;
		}
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00041550 File Offset: 0x0003F750
	private void updateEffect()
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						if (!this.effPaints[i].isFly)
						{
							this.effPaints[i].eMob.setInjure();
							this.effPaints[i].eMob.injureBy = this;
							if (this.me)
							{
								this.effPaints[i].eMob.hpInjure = global::Char.myCharz().cDamFull / 2 - global::Char.myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
							}
							int num = this.effPaints[i].eMob.h >> 1;
							if (this.effPaints[i].eMob.isBigBoss())
							{
								num = this.effPaints[i].eMob.getY() + 20;
							}
							GameScr.startSplash(this.effPaints[i].eMob.x, this.effPaints[i].eMob.y - num, this.cdir);
							this.effPaints[i].isFly = true;
						}
					}
					else if (this.effPaints[i].eChar != null && !this.effPaints[i].isFly)
					{
						if (this.effPaints[i].eChar.charID >= 0)
						{
							this.effPaints[i].eChar.doInjure();
						}
						GameScr.startSplash(this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy - (this.effPaints[i].eChar.ch >> 1), this.cdir);
						this.effPaints[i].isFly = true;
					}
					this.effPaints[i].index++;
					if (this.effPaints[i].index >= this.effPaints[i].effCharPaint.arrEfInfo.Length)
					{
						this.effPaints[i] = null;
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEff++;
			if (this.indexEff >= this.eff.arrEfInfo.Length)
			{
				this.indexEff = -1;
				this.eff = null;
			}
		}
		if (this.indexEffTask >= 0 && this.effTask != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEffTask++;
			if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
			{
				this.indexEffTask = -1;
				this.effTask = null;
			}
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00005EAA File Offset: 0x000040AA
	private void checkPerformEndMovePointAction()
	{
		if (this.endMovePointCommand != null)
		{
			Command command = this.endMovePointCommand;
			this.endMovePointCommand = null;
			command.performAction();
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0004180C File Offset: 0x0003FA0C
	private void checkHideCharName()
	{
		if (GameCanvas.gameTick % 20 == 0 && this.charID >= 0)
		{
			this.paintName = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = null;
				try
				{
					@char = (global::Char)GameScr.vCharInMap.elementAt(i);
				}
				catch (Exception)
				{
				}
				if (@char != null && !@char.Equals(this) && ((@char.cy == this.cy && Res.abs(@char.cx - this.cx) < 35) || (this.cy - @char.cy < 32 && this.cy - @char.cy > 0 && Res.abs(@char.cx - this.cx) < 24)))
				{
					this.paintName = false;
				}
			}
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc = null;
				try
				{
					npc = (Npc)GameScr.vNpc.elementAt(j);
				}
				catch (Exception)
				{
				}
				if (npc != null && npc.cy == this.cy && Res.abs(npc.cx - this.cx) < 24)
				{
					this.paintName = false;
				}
			}
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00041954 File Offset: 0x0003FB54
	private void updateMobMe()
	{
		if (this.tMobMeBorn != 0)
		{
			this.tMobMeBorn--;
		}
		if (this.tMobMeBorn == 0)
		{
			this.mobMe.xFirst = ((this.cdir != 1) ? (this.cx + 30) : (this.cx - 30));
			this.mobMe.yFirst = this.cy - 60;
			int num = this.mobMe.xFirst - this.mobMe.x;
			int num2 = this.mobMe.yFirst - this.mobMe.y;
			this.mobMe.x += num / 4;
			this.mobMe.y += num2 / 4;
			this.mobMe.dir = this.cdir;
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00041A2C File Offset: 0x0003FC2C
	private void updateSkillPaint()
	{
		if (this.statusMe == 14 || this.statusMe == 5)
		{
			return;
		}
		if (this.skillPaint != null && ((this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus) && this.charFocus.statusMe == 14) || (this.mobFocus != null && this.mobFocus.status == 0)))
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.mobFocus = null;
			this.charFocus = null;
			this.effPaints = null;
			this.currentMovePoint = null;
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		if (this.skillPaint != null && this.arr == null && this.skillInfoPaint() != null && this.indexSkill >= this.skillInfoPaint().Length)
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			Res.outz("remove 2");
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		SkillInfoPaint[] array = this.skillInfoPaint();
		if (array != null && this.indexSkill >= 0 && this.indexSkill <= array.Length - 1)
		{
			if (array[this.indexSkill].effS0Id != 0)
			{
				this.eff0 = GameScr.efs[array[this.indexSkill].effS0Id - 1];
				this.i0 = (this.dx0 = (this.dy0 = 0));
			}
			if (array[this.indexSkill].effS1Id != 0)
			{
				this.eff1 = GameScr.efs[array[this.indexSkill].effS1Id - 1];
				this.i1 = (this.dx1 = (this.dy1 = 0));
			}
			if (array[this.indexSkill].effS2Id != 0)
			{
				this.eff2 = GameScr.efs[array[this.indexSkill].effS2Id - 1];
				this.i2 = (this.dx2 = (this.dy2 = 0));
			}
			SkillInfoPaint[] array2 = array;
			int num = this.indexSkill;
			if (array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0)
			{
				int arrowId = array2[num].arrowId;
				if (arrowId >= 100)
				{
					IMapObject mapObject;
					if (this.mobFocus == null)
					{
						mapObject = this.charFocus;
					}
					else
					{
						mapObject = this.mobFocus;
					}
					IMapObject mapObject2 = mapObject;
					if (mapObject2 != null)
					{
						int num2 = Res.abs(mapObject2.getX() - this.cx);
						int num3 = Res.abs(mapObject2.getY() - this.cy);
						int num4;
						if (num2 > 4 * num3)
						{
							num4 = 0;
						}
						else
						{
							if (mapObject2.getY() < this.cy)
							{
								num4 = -3;
							}
							else
							{
								num4 = 3;
							}
							if (mapObject2 is BigBoss && ((BigBoss)mapObject2).haftBody)
							{
								num4 = -20;
							}
						}
						this.dart = new PlayerDart(this, arrowId - 100, this.skillPaintRandomPaint, this.cx + (array2[num].adx - 10) * this.cdir, this.cy + array2[num].ady + num4);
						if (this.myskill != null)
						{
							if (this.myskill.template.id == 1)
							{
								SoundMn.gI().traidatKame();
							}
							else if (this.myskill.template.id == 3)
							{
								SoundMn.gI().namekKame();
							}
							else if (this.myskill.template.id == 5)
							{
								SoundMn.gI().xaydaKame();
							}
							else if (this.myskill.template.id == 11)
							{
								SoundMn.gI().nameLazer();
							}
						}
					}
					else if (this.isFlyAndCharge || this.isUseSkillAfterCharge)
					{
						this.stopUseChargeSkill();
					}
				}
				else
				{
					Res.outz("g");
					this.arr = new Arrow(this, GameScr.arrs[arrowId - 1]);
					this.arr.life = 10;
					this.arr.ax = this.cx + array2[num].adx;
					this.arr.ay = this.cy + array2[num].ady;
				}
			}
			if ((this.mobFocus != null || (!this.me && this.charFocus != null) || (this.me && this.charFocus != null && (this.isMeCanAttackOtherPlayer(this.charFocus) || this.isSelectingSkillBuffToPlayer()) && this.arr == null && this.dart == null)) && this.indexSkill == array.Length - 1)
			{
				this.setAttack();
				if (this.me && this.myskill.template.isAttackSkill())
				{
					this.saveLoadPreviousSkill();
				}
			}
			if (!this.me)
			{
				IMapObject mapObject3 = null;
				if (this.mobFocus != null)
				{
					mapObject3 = this.mobFocus;
				}
				else if (this.charFocus != null)
				{
					mapObject3 = this.charFocus;
				}
				if (mapObject3 != null)
				{
					if (Res.abs(mapObject3.getX() - this.cx) < 10)
					{
						if (mapObject3.getX() > this.cx)
						{
							this.cx -= 10;
						}
						else
						{
							this.cx += 10;
						}
					}
					if (mapObject3.getX() > this.cx)
					{
						this.cdir = 1;
						return;
					}
					this.cdir = -1;
				}
			}
		}
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00003584 File Offset: 0x00001784
	public void saveLoadPreviousSkill()
	{
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x0004206C File Offset: 0x0004026C
	public void setResetPoint(int x, int y)
	{
		InfoDlg.hide();
		this.currentMovePoint = null;
		int num = this.cx;
		if (this.cy - y == 0)
		{
			this.cx = x;
			global::Char.ischangingMap = false;
			global::Char.isLockKey = false;
			return;
		}
		this.statusMe = 16;
		this.cp2 = x;
		this.cp3 = y;
		this.cp1 = 0;
		global::Char.myCharz().cxSend = x;
		global::Char.myCharz().cySend = y;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x000420E0 File Offset: 0x000402E0
	private void updateCharDeadFly()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 14;
			if (this.me)
			{
				GameScr.gI().resetButton();
				Service.gI().charMove();
			}
		}
		this.cf = 23;
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x000421F0 File Offset: 0x000403F0
	private void updateResetPoint()
	{
		InfoDlg.hide();
		GameCanvas.clearAllPointerEvent();
		this.currentMovePoint = null;
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 1;
			this.cp3 = 0;
			global::Char.ischangingMap = false;
			Service.gI().charMove();
		}
		this.cf = 23;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00003584 File Offset: 0x00001784
	public void updateSkillFall()
	{
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x000422E0 File Offset: 0x000404E0
	public void updateSkillStand()
	{
		this.ty = 0;
		this.cp1++;
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - this.chh) & 4) == 4)
			{
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - this.chh) & 8) == 8)
		{
			this.cvx = 0;
		}
		if (this.cy > this.ch && TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192))
		{
			if (!TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
			}
			else
			{
				this.cy = TileMap.tileYofPixel(this.cy);
			}
		}
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = (this.cvy = 0);
		}
		if (this.cvy == 0)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.statusMe = 4;
				this.cvx = (this.cspeed >> 1) * this.cdir;
				this.cp1 = (this.cp2 = 0);
			}
		}
		else if (this.cvy < 0)
		{
			this.cvy++;
			if (this.cvy == 0)
			{
				this.cvy = 1;
			}
		}
		else
		{
			if (this.cvy < 20 && this.cp1 % 5 == 0)
			{
				this.cvy++;
			}
			if (this.cvy > 3)
			{
				this.cvy = 3;
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2 && this.cy <= TileMap.tileXofPixel(this.cy + 3))
			{
				this.cvx = (this.cvy = 0);
				this.cy = TileMap.tileXofPixel(this.cy + 3);
			}
		}
		if (this.cvx > 0)
		{
			this.cvx--;
			return;
		}
		if (this.cvx < 0)
		{
			this.cvx++;
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00042548 File Offset: 0x00040748
	public void updateCharAutoJump()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cx += this.cvx * this.cdir;
		this.cy += this.cvyJump;
		this.cvyJump++;
		if (this.cp1 == 0)
		{
			this.cf = 7;
		}
		else
		{
			this.cf = 23;
		}
		if (this.cvyJump == -3)
		{
			this.cf = 8;
		}
		else if (this.cvyJump == -2)
		{
			this.cf = 9;
		}
		else if (this.cvyJump == -1)
		{
			this.cf = 10;
		}
		else if (this.cvyJump == 0)
		{
			this.cf = 11;
		}
		if (this.cvyJump == 0)
		{
			this.statusMe = 6;
			this.cp3 = 0;
			((MovePoint)this.vMovePoints.firstElement()).status = 4;
			this.isJump = true;
			this.cp1 = 0;
			this.cvy = 1;
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00042660 File Offset: 0x00040860
	public int getVx(int size, int dx, int dy)
	{
		if (dy > 0 && !TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			if (dx - dy <= 10)
			{
				return 5;
			}
			if (dx - dy <= 30)
			{
				return 6;
			}
			if (dx - dy <= 50)
			{
				return 7;
			}
			if (dx - dy <= 70)
			{
				return 8;
			}
		}
		if (dx <= 30)
		{
			return 4;
		}
		if (dx <= 160)
		{
			return 5;
		}
		if (dx <= 270)
		{
			return 6;
		}
		if (dx <= 320)
		{
			return 7;
		}
		return 8;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00005EC6 File Offset: 0x000040C6
	public void hide()
	{
		this.isHide = true;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 15, 1));
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00005EEE File Offset: 0x000040EE
	public void show()
	{
		this.isHide = false;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 10, 1));
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00005F16 File Offset: 0x00004116
	public int getVy(int size, int dx, int dy)
	{
		if (dy <= 10)
		{
			return 5;
		}
		if (dy <= 20)
		{
			return 6;
		}
		if (dy <= 30)
		{
			return 7;
		}
		if (dy <= 40)
		{
			return 8;
		}
		if (dy <= 50)
		{
			return 9;
		}
		return 10;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x000426D0 File Offset: 0x000408D0
	public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
	{
		int num = xEnd - xFirst;
		int num2 = yEnd - yFirst;
		if (num == 0 && num2 == 0)
		{
			return 1;
		}
		if (num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2))
		{
			return 2;
		}
		if (num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2)))
		{
			return 4;
		}
		this.cvy = -10;
		this.cp1 = 0;
		this.cdir = ((num <= 0) ? -1 : 1);
		if (num <= 5)
		{
			this.cvx = 0;
		}
		else if (num <= 10)
		{
			this.cvx = 3;
		}
		else
		{
			this.cvx = 5;
		}
		return 9;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0004275C File Offset: 0x0004095C
	public void setAutoJump()
	{
		int num = ((MovePoint)this.vMovePoints.firstElement()).xEnd - this.cx;
		this.cvyJump = -10;
		this.cp1 = 0;
		this.cdir = ((num <= 0) ? -1 : 1);
		if (num <= 6)
		{
			this.cvx = 0;
			return;
		}
		if (num <= 20)
		{
			this.cvx = 3;
			return;
		}
		this.cvx = 5;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x000427C4 File Offset: 0x000409C4
	public void updateCharStand()
	{
		this.isSoundJump = false;
		this.isAttack = false;
		this.isAttFly = false;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1++;
		if (this.cp1 > 30)
		{
			this.cp1 = 0;
		}
		if (this.cp1 % 15 < 5)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		this.updateCharInBridge();
		if (!this.me)
		{
			this.cp3++;
			if (this.cp3 > 50)
			{
				this.cp3 = 0;
				this.currentMovePoint = null;
			}
		}
		this.updateSuperEff();
		if (this.me && GameScr.vCharInMap.size() != 0 && TileMap.mapID == 50)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(0);
			if (!@char.changePos)
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx - 45, this.cy);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx - 45 - @char.cx) <= 10)
				{
					@char.changePos = true;
				}
			}
			else
			{
				if (@char.statusMe != 2)
				{
					@char.moveTo(this.cx + 45, this.cy);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				if (Res.abs(this.cx + 45 - @char.cx) <= 10)
				{
					@char.changePos = false;
				}
			}
			if (GameCanvas.gameTick % 100 == 0)
			{
				@char.addInfo("Cắc cùm cum");
			}
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00042954 File Offset: 0x00040B54
	public void updateSuperEff()
	{
		if (this.isCopy || this.clevel >= 14)
		{
			return;
		}
		if (this.isFusion)
		{
			return;
		}
		if (this.isSetPos)
		{
			return;
		}
		if (this.isPet || this.isMiniPet)
		{
			return;
		}
		if (this.isMonkey == 1)
		{
			return;
		}
		this.ty++;
		if (this.clevel < 14 && this.ty > 50 && this.clevel >= 9)
		{
			if (this.cgender == 0)
			{
				if (GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
			}
			if (this.cgender == 1)
			{
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 7 == 0)
				{
					ServerEffect.addServerEffect(131, this, 1);
				}
			}
			if (this.cgender == 2)
			{
				if (GameCanvas.gameTick % 7 == 0)
				{
					ServerEffect.addServerEffect(131, this, 1);
				}
				if (this.clevel >= 13 && GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
			}
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00042A88 File Offset: 0x00040C88
	public float getSoundVolumn()
	{
		if (this.me)
		{
			return 0.1f;
		}
		int num = Res.abs(global::Char.myChar.cx - this.cx);
		if (num >= 0 && num <= 50)
		{
			return 0.1f;
		}
		return 0.05f;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00042AD0 File Offset: 0x00040CD0
	public void updateCharRun()
	{
		int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
		if (this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)
		{
			if (this.isMonkey == 0)
			{
				SoundMn.gI().charRun(this.getSoundVolumn());
			}
			else
			{
				SoundMn.gI().monkeyRun(this.getSoundVolumn());
			}
		}
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		int num2 = 0;
		if (!this.me && this.currentMovePoint != null)
		{
			num2 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
		}
		this.cp1++;
		if (this.cp1 >= 10)
		{
			this.cp1 = 0;
			this.cBonusSpeed = 0;
		}
		this.cf = (this.cp1 >> 1) + 2;
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy - 1) & 64) == 64)
		{
			this.cx += this.cvx * num >> 1;
		}
		else
		{
			this.cx += this.cvx * num;
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
			{
				if (this.me)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				}
				else
				{
					this.stop();
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
		{
			if (this.me)
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			}
			else
			{
				this.stop();
			}
		}
		if (this.me)
		{
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else
			{
				if (this.cx - this.cxSend != 0 && this.me)
				{
					Service.gI().charMove();
				}
				this.statusMe = 1;
				this.cBonusSpeed = 0;
			}
		}
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
		{
			if (this.me)
			{
				if (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0)
				{
					Service.gI().charMove();
				}
				this.cf = 7;
				this.statusMe = 4;
				this.delayFall = 0;
				this.cvx = 3 * this.cdir;
				this.cp2 = 0;
			}
			else
			{
				this.stop();
			}
		}
		if (!this.me && this.currentMovePoint != null && global::Char.abs(this.cx - this.currentMovePoint.xEnd) > num2)
		{
			this.stop();
		}
		GameCanvas.gI().startDust(this.cdir, this.cx - (this.cdir << 3), this.cy);
		this.updateCharInBridge();
		this.addDustEff(2);
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00042E24 File Offset: 0x00041024
	private void stop()
	{
		this.statusMe = 6;
		this.cp3 = 0;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1 = (this.cp2 = 0);
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00005F3E File Offset: 0x0000413E
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00042E60 File Offset: 0x00041060
	public void updateCharJump()
	{
		this.setMountIsStart();
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.addDustEff(3);
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = 0;
			this.cvy = -1;
		}
		this.cvy++;
		if (this.cvy > 0)
		{
			this.cvy = 0;
		}
		if (!this.me && this.currentMovePoint != null)
		{
			int num = this.currentMovePoint.xEnd - this.cx;
			if (num > 0)
			{
				if (this.cvx > num)
				{
					this.cvx = num;
				}
				if (this.cvx < 0)
				{
					this.cvx = num;
				}
			}
			else if (num < 0)
			{
				if (this.cvx < num)
				{
					this.cvx = num;
				}
				if (this.cvx > 0)
				{
					this.cvx = num;
				}
			}
			else
			{
				this.cvx = num;
			}
		}
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
		{
			this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
			this.cvx = 0;
		}
		if (this.cvy == 0)
		{
			if (!this.isAttFly)
			{
				if (this.me)
				{
					this.setCharFallFromJump();
				}
				else
				{
					this.stop();
				}
			}
			else
			{
				this.setCharFallFromJump();
			}
		}
		if (this.me && !global::Char.ischangingMap && this.isInWaypoint())
		{
			Service.gI().charMove();
			if (TileMap.isTrainingMap())
			{
				global::Char.ischangingMap = true;
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().getMapOffline();
			}
			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
			return;
		}
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			this.statusMe = 4;
			this.cp1 = 0;
			this.cp2 = 0;
			this.cvy = 1;
			this.delayFall = 0;
			if (this.cy < 0)
			{
				this.cy = 0;
			}
			this.cy = TileMap.tileYofPixel(this.cy + 25);
			GameCanvas.clearKeyHold();
		}
		if (this.cp3 < 0)
		{
			this.cp3++;
		}
		this.cf = 7;
		if (!this.me && this.currentMovePoint != null && this.cy < this.currentMovePoint.yEnd)
		{
			this.stop();
		}
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00005F48 File Offset: 0x00004148
	public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
	{
		return xmob <= xw1 && xmob >= x1 && ymob <= y1 && ymob >= yh1;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000431AC File Offset: 0x000413AC
	public void setCharFallFromJump()
	{
		this.cyStartFall = this.cy;
		this.cp1 = 0;
		this.cp2 = 0;
		this.statusMe = 10;
		this.cvx = this.cdir << 2;
		this.cvy = 0;
		this.cy = TileMap.tileYofPixel(this.cy) + 12;
		if (this.me && (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00043270 File Offset: 0x00041470
	public void updateCharFall()
	{
		if (this.holder)
		{
			return;
		}
		this.ty = 0;
		if (this.cy + 4 >= TileMap.pxh)
		{
			this.statusMe = 1;
			if (this.me)
			{
				SoundMn.gI().charFall();
			}
			this.cvx = (this.cvy = 0);
			this.cp3 = 0;
			return;
		}
		if (this.cy % 24 == 0 && (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
		{
			this.delayFall = 0;
			if (this.me)
			{
				if (this.cy - this.cySend > 0)
				{
					Service.gI().charMove();
				}
				else if (this.cx - this.cxSend != 0 || this.cy - this.cySend < 0)
				{
					Service.gI().charMove();
				}
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 1;
				this.cp3 = 0;
				return;
			}
			this.stop();
			this.cf = 0;
			GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
			GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
			this.addDustEff(1);
		}
		if (this.delayFall > 0)
		{
			this.delayFall--;
			if (this.delayFall % 10 > 5)
			{
				this.cy++;
				return;
			}
			this.cy--;
			return;
		}
		else
		{
			if (this.cvy < -4)
			{
				this.cf = 7;
			}
			else
			{
				this.cf = 12;
			}
			this.cx += this.cvx;
			if (!this.me && this.currentMovePoint != null)
			{
				int num = this.currentMovePoint.xEnd - this.cx;
				if (num > 0)
				{
					if (this.cvx > num)
					{
						this.cvx = num;
					}
					if (this.cvx < 0)
					{
						this.cvx = num;
					}
				}
				else if (num < 0)
				{
					if (this.cvx < num)
					{
						this.cvx = num;
					}
					if (this.cvx > 0)
					{
						this.cvx = num;
					}
				}
				else
				{
					this.cvx = num;
				}
			}
			this.cvy++;
			if (this.cvy > 8)
			{
				this.cvy = 8;
			}
			if (this.skillPaintRandomPaint == null)
			{
				this.cy += this.cvy;
			}
			if (this.cdir == 1)
			{
				if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
				{
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					this.cvx = 0;
				}
			}
			else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
				this.cvx = 0;
			}
			if (this.cvy > 3 && (this.cyStartFall == 0 || this.cyStartFall <= TileMap.tileYofPixel(this.cy + 3)) && (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2)
			{
				if (this.me)
				{
					this.cyStartFall = 0;
					this.cvx = (this.cvy = 0);
					this.cp1 = (this.cp2 = 0);
					this.cy = TileMap.tileXofPixel(this.cy + 3);
					this.statusMe = 1;
					if (this.me)
					{
						SoundMn.gI().charFall();
					}
					this.cp3 = 0;
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
					if (this.cy - this.cySend > 0)
					{
						if (this.me)
						{
							Service.gI().charMove();
							return;
						}
					}
					else if ((this.cx - this.cxSend != 0 || this.cy - this.cySend < 0) && this.me)
					{
						Service.gI().charMove();
						return;
					}
				}
				else
				{
					this.stop();
					this.cy = TileMap.tileXofPixel(this.cy + 3);
					this.cf = 0;
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
				}
				return;
			}
			this.cf = 12;
			if (this.me)
			{
				bool flag = this.isAttack;
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) == 2)
			{
				this.cf = 0;
			}
			if (this.currentMovePoint != null && this.cy > this.currentMovePoint.yEnd)
			{
				this.stop();
			}
			return;
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x000437AC File Offset: 0x000419AC
	public void updateCharFly()
	{
		int num = (this.isMonkey != 1 || this.me) ? 1 : 2;
		this.setMountIsStart();
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			if (this.cy - this.ch < 0)
			{
				this.cy = this.ch;
			}
			this.cf = 7;
			this.statusMe = 4;
			this.cvx = 0;
			this.cp2 = 0;
			this.currentMovePoint = null;
			return;
		}
		int num2 = this.cy;
		this.cp1++;
		if (this.cp1 >= 9)
		{
			this.cp1 = 0;
			if (!this.me)
			{
				this.cvx = (this.cvy = 0);
			}
			this.cBonusSpeed = 0;
		}
		this.cf = 8;
		if (Res.abs(this.cvx) <= 4 && this.me)
		{
			if (this.currentMovePoint != null)
			{
				int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
				int num4 = global::Char.abs(this.cy - this.currentMovePoint.yEnd);
				if (num3 > num4 * 10)
				{
					this.cf = 8;
				}
				else if (num3 > num4 && num3 > 48 && num4 > 32)
				{
					this.cf = 8;
				}
				else
				{
					this.cf = 7;
				}
			}
			else
			{
				if (this.cvy < 0)
				{
					this.cvy = 0;
				}
				if (this.cvy > 16)
				{
					this.cvy = 16;
				}
				this.cf = 7;
			}
		}
		if (!this.me)
		{
			if (global::Char.abs(this.cvx) < 2)
			{
				this.cvx = (this.cdir << 1) * num;
			}
			if (this.cvy != 0)
			{
				this.cf = 7;
			}
			if (global::Char.abs(this.cvx) <= 2)
			{
				this.cp2++;
				if (this.cp2 > 32)
				{
					this.statusMe = 4;
					this.cvx = 0;
					this.cvy = 0;
				}
			}
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - 1, 4))
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				if (this.cvy == 0)
				{
					this.currentMovePoint = null;
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - 1, 8))
		{
			this.cvx = 0;
			this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			if (this.cvy == 0)
			{
				this.currentMovePoint = null;
			}
		}
		this.cx += this.cvx * num;
		this.cy += this.cvy * num;
		if (!this.isMount && num2 - this.cy == 0)
		{
			this.ty++;
			this.wt++;
			this.fy += (this.wy ? -1 : 1);
			if (this.wt == 10)
			{
				this.wt = 0;
				this.wy = !this.wy;
			}
			if (this.ty > 20)
			{
				this.delayFall = 10;
				if (GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(111, this.cx + ((this.cdir != 1) ? 27 : -17), this.cy + this.fy + 13, 1, (this.cdir == 1) ? 0 : 2);
				}
			}
		}
		if (this.me)
		{
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else if (this.cvy == 0)
			{
				this.statusMe = 4;
				this.checkDelayFallIfTooHigh();
				Service.gI().charMove();
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(this.cx, this.cy + 40) & 2) == 2)
			{
				if (this.cvy == 0)
				{
					this.delayFall = 0;
				}
				this.cyStartFall = 0;
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 4;
				this.addDustEff(3);
			}
			if (global::Char.abs(this.cx - this.cxSend) > 96 || global::Char.abs(this.cy - this.cySend) > 24)
			{
				Service.gI().charMove();
			}
		}
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00043C68 File Offset: 0x00041E68
	public void setMount(int cid, int ctrans, int cgender)
	{
		this.idcharMount = cid;
		this.transMount = ctrans;
		this.genderMount = cgender;
		this.speedMount = 30;
		if (this.transMount < 0)
		{
			this.transMount = 0;
			this.xMount = GameScr.cmx + GameCanvas.w + 50;
			this.dxMount = -19;
		}
		else if (this.transMount == 1)
		{
			this.transMount = 2;
			this.xMount = GameScr.cmx - 100;
			this.dxMount = -33;
		}
		this.dyMount = -17;
		this.yMount = this.cy;
		this.frameMount = 0;
		this.isMount = false;
		this.isEndMount = false;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00043D10 File Offset: 0x00041F10
	public void updateMount()
	{
		this.frameMount++;
		if (this.frameMount > this.FrameMount.Length - 1)
		{
			this.frameMount = 0;
		}
		if (this.isStartMount && !this.isMount)
		{
			this.yMount = this.cy;
			if (this.transMount == 0)
			{
				if (this.xMount - this.cx >= this.speedMount)
				{
					this.xMount -= this.speedMount;
					return;
				}
				this.xMount = this.cx;
				this.isMount = true;
				this.isEndMount = false;
				return;
			}
			else if (this.transMount == 2)
			{
				if (this.cx - this.xMount >= this.speedMount)
				{
					this.xMount += this.speedMount;
					return;
				}
				this.xMount = this.cx;
				this.isMount = true;
				this.isEndMount = false;
				return;
			}
		}
		else
		{
			if (this.isMount)
			{
				if (this.statusMe == 14 || this.ySd - this.cy < 24)
				{
					this.setMountIsEnd();
				}
				if (this.cp1 % 15 < 5)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
				this.transMount = this.cdir;
				this.updateSuperEff();
				if (this.transMount < 0)
				{
					this.transMount = 0;
					this.dxMount = -19;
				}
				else if (this.transMount == 1)
				{
					this.transMount = 2;
					this.dxMount = -31;
					if (this.isEventMount)
					{
						this.dxMount = -38;
					}
				}
				if (this.skillInfoPaint() != null)
				{
					this.dyMount = -15;
				}
				else
				{
					this.dyMount = -17;
				}
				this.yMount = this.cy;
				this.xMount = this.cx;
				return;
			}
			if (this.isEndMount)
			{
				if (this.transMount == 0)
				{
					if (this.xMount > GameScr.cmx - 100)
					{
						this.xMount -= 20;
						return;
					}
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
					return;
				}
				else if (this.transMount == 2)
				{
					if (this.xMount < GameScr.cmx + GameCanvas.w + 50)
					{
						this.xMount += 20;
						return;
					}
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
					return;
				}
			}
			else if (!this.isStartMount || !this.isMount || !this.isEndMount)
			{
				this.xMount = GameScr.cmx - 100;
				this.yMount = GameScr.cmy - 100;
			}
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00043F94 File Offset: 0x00042194
	public void getMountData()
	{
		if (Mob.arrMobTemplate[50].data == null)
		{
			Mob.arrMobTemplate[50].data = new EffectData();
			string text = "/Mob/" + 50;
			if (MyStream.readFile(text) != null)
			{
				Mob.arrMobTemplate[50].data.readData(text + "/data");
				Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(50);
			}
			Mob.lastMob.addElement(50 + string.Empty);
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00005F63 File Offset: 0x00004163
	public void checkFrameTick(int[] array)
	{
		this.t++;
		if (this.t > array.Length - 1)
		{
			this.t = 0;
		}
		this.fM = array[this.t];
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00044048 File Offset: 0x00042248
	public void paintMount1(mGraphics g)
	{
		if (this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w)
		{
			if (this.me)
			{
				if (this.isEndMount || this.isStartMount || this.isMount)
				{
					if (this.isSpeacialMount)
					{
						return;
					}
					if (this.isEventMount)
					{
						g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
				}
			}
			else if (!this.me)
			{
				if (this.isSpeacialMount)
				{
					return;
				}
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				if (this.isMount)
				{
					if (this.genderMount == 2)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					}
				}
			}
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00044404 File Offset: 0x00042604
	public void paintMount2(mGraphics g)
	{
		if (this.xMount > GameScr.cmx && this.xMount < GameScr.cmx + GameCanvas.w)
		{
			if (this.me)
			{
				if (this.isEndMount || this.isStartMount || this.isMount)
				{
					if (this.isSpeacialMount)
					{
						this.checkFrameTick(this.move);
						if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
						{
							Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
							return;
						}
						this.getMountData();
						return;
					}
					else
					{
						if (this.isEventMount)
						{
							g.drawRegion(global::Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						if (this.genderMount == 0)
						{
							if (!this.isMountVip)
							{
								g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
								return;
							}
							g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						else if (this.genderMount == 1)
						{
							if (!this.isMountVip)
							{
								g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
								return;
							}
							g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
					}
				}
			}
			else if (!this.me)
			{
				if (this.isSpeacialMount)
				{
					this.checkFrameTick(this.move);
					if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
					{
						Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
						return;
					}
					this.getMountData();
					return;
				}
				else if (this.isEventMount)
				{
					this.checkFrameTick(this.move);
					if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
					{
						Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : -8), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
						return;
					}
					this.getMountData();
					return;
				}
				else if (this.isMount)
				{
					if (this.genderMount == 0)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					else if (this.genderMount == 1)
					{
						if (!this.isMountVip)
						{
							g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
							return;
						}
						g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					}
				}
			}
		}
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x000448C8 File Offset: 0x00042AC8
	public void setMountIsStart()
	{
		if (this.me)
		{
			this.isHaveMount = this.checkHaveMount();
			if (TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 51 || TileMap.mapID == 103)
			{
				this.isHaveMount = false;
			}
		}
		if (this.isHaveMount)
		{
			if (this.ySd - this.cy <= 20)
			{
				this.xChar = this.cx;
			}
			if (this.xdis < 100)
			{
				this.xdis = Res.abs(this.xChar - this.cx);
			}
			if (this.xdis >= 70 && this.ySd - this.cy > 30 && !this.isStartMount && !this.isEndMount)
			{
				this.setMount(this.charID, this.cdir, this.cgender);
				this.isStartMount = true;
			}
		}
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00005F95 File Offset: 0x00004195
	public void setMountIsEnd()
	{
		if (this.ySd - this.cy < 24 && !this.isEndMount)
		{
			this.isStartMount = false;
			this.isMount = false;
			this.isEndMount = true;
			this.xdis = 0;
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000449A8 File Offset: 0x00042BA8
	public bool checkHaveMount()
	{
		bool result = false;
		for (int i = 0; i < this.arrItemBag.Length; i++)
		{
			if (this.arrItemBag[i] != null)
			{
				if (this.arrItemBag[i].template.type == 24)
				{
					result = true;
					if (this.arrItemBag[i].template.id != 396)
					{
						this.isMountVip = true;
						this.isEventMount = false;
						this.isSpeacialMount = false;
						break;
					}
				}
				if (this.arrItemBag[i].template.type == 23)
				{
					result = true;
					this.isMountVip = false;
					this.isSpeacialMount = false;
					break;
				}
			}
		}
		for (int j = 0; j < this.arrItemBag.Length; j++)
		{
			if (this.arrItemBag[j] != null && this.arrItemBag[j].template.type == 24)
			{
				result = true;
				if (this.arrItemBag[j].template.id == 396)
				{
					this.isEventMount = true;
					this.isMountVip = false;
					this.isSpeacialMount = false;
					break;
				}
				if (this.arrItemBag[j].template.id == 532)
				{
					this.isEventMount = false;
					this.isMountVip = false;
					this.isSpeacialMount = true;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00044AE4 File Offset: 0x00042CE4
	private void checkDelayFallIfTooHigh()
	{
		bool flag = true;
		for (int i = 0; i < 150; i += 24)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + i) & 2) == 2 || this.cy + i > TileMap.tmh * (int)TileMap.size - 24)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.delayFall = 40;
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00005FCC File Offset: 0x000041CC
	public void setDefaultPart()
	{
		this.setDefaultWeapon();
		this.setDefaultBody();
		this.setDefaultLeg();
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00005FE0 File Offset: 0x000041E0
	public void setDefaultWeapon()
	{
		if (this.cgender == 0)
		{
			this.wp = 0;
		}
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00005FF1 File Offset: 0x000041F1
	public void setDefaultBody()
	{
		if (this.cgender == 0)
		{
			this.body = 57;
			return;
		}
		if (this.cgender == 1)
		{
			this.body = 59;
			return;
		}
		if (this.cgender == 2)
		{
			this.body = 57;
		}
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00006027 File Offset: 0x00004227
	public void setDefaultLeg()
	{
		if (this.cgender == 0)
		{
			this.leg = 58;
			return;
		}
		if (this.cgender == 1)
		{
			this.leg = 60;
			return;
		}
		if (this.cgender == 2)
		{
			this.leg = 58;
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0000605D File Offset: 0x0000425D
	public bool isSelectingSkillUseAlone()
	{
		return this.myskill != null && this.myskill.template.isUseAlone();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00006079 File Offset: 0x00004279
	public bool isSelectingSkillBuffToPlayer()
	{
		return this.myskill != null && this.myskill.template.isBuffToPlayer();
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00006095 File Offset: 0x00004295
	public bool isUseChargeSkill()
	{
		return !this.isUseSkillAfterCharge && this.myskill != null && (this.myskill.template.id == 10 || this.myskill.template.id == 11);
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00044B44 File Offset: 0x00042D44
	public void setSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.hasSendAttack = false;
		if (this.stone)
		{
			return;
		}
		if (this.me && this.myskill.template.id == 9 && this.cHP <= this.cHPFull / 10)
		{
			return;
		}
		if (this.me)
		{
			if (this.mobFocus == null && this.charFocus == null)
			{
				this.stopUseChargeSkill();
			}
			if (this.mobFocus != null && (this.mobFocus.status == 1 || this.mobFocus.status == 0))
			{
				this.stopUseChargeSkill();
			}
			if (this.charFocus != null && (this.charFocus.statusMe == 14 || this.charFocus.statusMe == 5))
			{
				this.stopUseChargeSkill();
			}
			if (this.myskill.template.id == 23)
			{
				if (this.charFocus != null && this.charFocus.holdEffID != 0)
				{
					return;
				}
				if (this.mobFocus != null && this.mobFocus.holdEffID != 0)
				{
					return;
				}
				if (this.holdEffID != 0)
				{
					return;
				}
			}
			if (this.sleepEff || this.blindEff)
			{
				return;
			}
		}
		Res.outz("skill id= " + skillPaint.id);
		if (this.me && this.dart != null)
		{
			return;
		}
		if (TileMap.isOfflineMap())
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me)
		{
			if (this.isSelectingSkillBuffToPlayer() && this.charFocus == null)
			{
				return;
			}
			if (num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
			{
				this.myskill.paintCanNotUseSkill = true;
				return;
			}
			this.myskill.lastTimeUseThisSkill = num;
			if (this.myskill.template.manaUseType == 2)
			{
				this.cMP = 1;
			}
			else if (this.myskill.template.manaUseType != 1)
			{
				this.cMP -= this.myskill.manaUse;
			}
			else
			{
				this.cMP -= this.myskill.manaUse * this.cMPFull / 100;
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			if (this.cMP < 0)
			{
				this.cMP = 0;
			}
		}
		if (this.me)
		{
			if (this.myskill.template.id == 7)
			{
				SoundMn.gI().hoisinh();
			}
			if (this.myskill.template.id == 6)
			{
				Service.gI().skill_not_focus(0);
				GameScr.gI().isUseFreez = true;
				SoundMn.gI().thaiduonghasan();
			}
			if (this.myskill.template.id == 8)
			{
				if (!this.isCharge)
				{
					SoundMn.gI().taitaoPause();
					Service.gI().skill_not_focus(1);
					this.isCharge = true;
					this.last = (this.cur = mSystem.currentTimeMillis());
				}
				else
				{
					Service.gI().skill_not_focus(3);
					this.isCharge = false;
					SoundMn.gI().taitaoPause();
				}
			}
			if (this.myskill.template.id == 13)
			{
				if (this.isMonkey != 0)
				{
					GameScr.gI().auto = 0;
					return;
				}
				if (this.isCreateDark)
				{
					return;
				}
				SoundMn.gI().gong();
				Service.gI().skill_not_focus(6);
				this.chargeCount = 0;
				this.isWaitMonkey = true;
				return;
			}
			else
			{
				if (this.myskill.template.id == 14)
				{
					SoundMn.gI().gong();
					Service.gI().skill_not_focus(7);
					this.useChargeSkill(true);
				}
				if (this.myskill.template.id == 21)
				{
					Service.gI().skill_not_focus(10);
					return;
				}
				if (this.myskill.template.id == 12)
				{
					Service.gI().skill_not_focus(8);
				}
				if (this.myskill.template.id == 19)
				{
					Service.gI().skill_not_focus(9);
					return;
				}
			}
		}
		if (this.isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41)
		{
			skillPaint = GameScr.sks[106];
		}
		if (skillPaint.id >= 128 && skillPaint.id <= 134)
		{
			skillPaint = GameScr.sks[skillPaint.id - 65];
			if (this.charFocus != null)
			{
				this.cx = this.charFocus.cx;
				this.cy = this.charFocus.cy;
				this.currentMovePoint = null;
			}
			if (this.mobFocus != null)
			{
				this.cx = this.mobFocus.x;
				this.cy = this.mobFocus.y;
				this.currentMovePoint = null;
			}
			ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
			this.telePortSkill = true;
		}
		if (skillPaint.id >= 107 && skillPaint.id <= 113)
		{
			skillPaint = GameScr.sks[skillPaint.id - 44];
			EffecMn.addEff(new Effect(23, this.cx, this.cy + this.ch / 2, 3, 2, 1));
		}
		this.setAutoSkillPaint(skillPaint, sType);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0004505C File Offset: 0x0004325C
	public void useSkillNotFocus()
	{
		GameScr.gI().auto = 0;
		global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2)) ? 1 : 0);
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000450B4 File Offset: 0x000432B4
	public void sendUseChargeSkill()
	{
		if (this.me && (this.isFreez || this.isUsePlane))
		{
			GameScr.gI().auto = 0;
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me && num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
		{
			this.myskill.paintCanNotUseSkill = true;
			return;
		}
		if (this.myskill.template.id == 10)
		{
			this.useChargeSkill(false);
		}
		if (this.myskill.template.id == 11)
		{
			this.useChargeSkill(true);
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00045154 File Offset: 0x00043354
	public void stopUseChargeSkill()
	{
		this.isFlyAndCharge = false;
		this.isStandAndCharge = false;
		this.isUseSkillAfterCharge = false;
		this.isCreateDark = false;
		if (this.me && this.statusMe != 14 && this.statusMe != 5)
		{
			this.isLockMove = false;
		}
		GameScr.gI().auto = 0;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000451AC File Offset: 0x000433AC
	public void useChargeSkill(bool isGround)
	{
		if (this.isCreateDark)
		{
			return;
		}
		GameScr.gI().auto = 0;
		if (isGround)
		{
			if (!this.isStandAndCharge)
			{
				this.chargeCount = 0;
				this.seconds = 50000;
				this.posDisY = 0;
				this.last = mSystem.currentTimeMillis();
				if (this.me)
				{
					this.isLockMove = true;
					if (this.cgender == 1)
					{
						Service.gI().skill_not_focus(4);
					}
				}
				if (this.cgender == 1)
				{
					SoundMn.gI().gongName();
				}
				this.isStandAndCharge = true;
				return;
			}
		}
		else if (!this.isFlyAndCharge)
		{
			if (this.me)
			{
				GameScr.gI().auto = 0;
				this.isLockMove = true;
				Service.gI().skill_not_focus(4);
			}
			this.isUseSkillAfterCharge = false;
			this.chargeCount = 0;
			this.isFlyAndCharge = true;
			this.posDisY = 0;
			this.seconds = 50000;
			this.isFlying = TileMap.tileTypeAt(this.cx, this.cy, 2);
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000452AC File Offset: 0x000434AC
	public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.skillPaint = skillPaint;
		Res.outz("set auto skill " + ((skillPaint == null) ? "null" : "!null"));
		if (skillPaint.id >= 0 && skillPaint.id <= 6)
		{
			int num = Res.random(0, skillPaint.id + 4) - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 6)
			{
				num = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num];
		}
		else if (skillPaint.id >= 14 && skillPaint.id <= 20)
		{
			int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 6)
			{
				num2 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num2 + 14];
		}
		else if (skillPaint.id >= 28 && skillPaint.id <= 34)
		{
			int num3 = Res.random(0, ((this.isMonkey != 1) ? skillPaint.id : 105) - ((this.isMonkey != 1) ? 28 : 105) + 4) - 1;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 6)
			{
				num3 = 6;
			}
			if (this.isMonkey == 1)
			{
				num3 = 0;
			}
			this.skillPaintRandomPaint = GameScr.sks[num3 + ((this.isMonkey != 1) ? 28 : 105)];
		}
		else if (skillPaint.id >= 63 && skillPaint.id <= 69)
		{
			int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 6)
			{
				num4 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num4 + 63];
		}
		else if (skillPaint.id >= 107 && skillPaint.id <= 109)
		{
			int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 6)
			{
				num5 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num5 + 107];
		}
		else
		{
			this.skillPaintRandomPaint = skillPaint;
		}
		this.sType = sType;
		this.indexSkill = 0;
		this.i0 = (this.i1 = (this.i2 = (this.dx0 = (this.dx1 = (this.dx2 = (this.dy0 = (this.dy1 = (this.dy2 = 0))))))));
		this.eff0 = null;
		this.eff1 = null;
		this.eff2 = null;
		this.cvy = 0;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x000060D3 File Offset: 0x000042D3
	public SkillInfoPaint[] skillInfoPaint()
	{
		if (this.skillPaint == null)
		{
			return null;
		}
		if (this.skillPaintRandomPaint == null)
		{
			return null;
		}
		if (this.sType == 0)
		{
			return this.skillPaintRandomPaint.skillStand;
		}
		return this.skillPaintRandomPaint.skillfly;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00045508 File Offset: 0x00043708
	public void setAttack()
	{
		if (this.me)
		{
			SkillPaint skillPaint = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint = this.dart.skillPaint;
			}
			if (skillPaint != null)
			{
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				if (this.charFocus != null)
				{
					myVector2.addElement(this.charFocus);
				}
				else if (this.mobFocus != null)
				{
					myVector.addElement(this.mobFocus);
				}
				this.effPaints = new EffectPaint[myVector.size() + myVector2.size()];
				for (int i = 0; i < myVector.size(); i++)
				{
					this.effPaints[i] = new EffectPaint();
					this.effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					if (!this.isSelectingSkillUseAlone())
					{
						this.effPaints[i].eMob = (Mob)myVector.elementAt(i);
					}
				}
				for (int j = 0; j < myVector2.size(); j++)
				{
					this.effPaints[j + myVector.size()] = new EffectPaint();
					this.effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					this.effPaints[j + myVector.size()].eChar = (global::Char)myVector2.elementAt(j);
				}
				int type = 0;
				if (this.mobFocus != null)
				{
					type = 1;
				}
				else if (this.charFocus != null)
				{
					type = 2;
				}
				if (myVector.size() == 0 && myVector2.size() == 0)
				{
					this.stopUseChargeSkill();
				}
				if (this.me && !this.isSelectingSkillUseAlone() && !this.hasSendAttack)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, type);
					this.hasSendAttack = true;
					return;
				}
			}
		}
		else
		{
			SkillPaint skillPaint2 = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint2 = this.dart.skillPaint;
			}
			if (skillPaint2 != null)
			{
				if (this.attMobs != null)
				{
					this.effPaints = new EffectPaint[this.attMobs.Length];
					for (int k = 0; k < this.attMobs.Length; k++)
					{
						this.effPaints[k] = new EffectPaint();
						this.effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[k].eMob = this.attMobs[k];
					}
					this.attMobs = null;
					return;
				}
				if (this.attChars != null)
				{
					this.effPaints = new EffectPaint[this.attChars.Length];
					for (int l = 0; l < this.attChars.Length; l++)
					{
						this.effPaints[l] = new EffectPaint();
						this.effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						this.effPaints[l].eChar = this.attChars[l];
					}
					this.attChars = null;
				}
			}
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00006108 File Offset: 0x00004308
	public bool isOutX()
	{
		return this.cx < GameScr.cmx || this.cx > GameScr.cmx + GameScr.gW;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0000612C File Offset: 0x0000432C
	public bool isPaint()
	{
		return this.cy >= GameScr.cmy && this.cy <= GameScr.cmy + GameScr.gH + 30 && !this.isOutX() && !this.isSetPos && !this.isFusion;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0000616C File Offset: 0x0000436C
	public void createShadow(int x, int y, int life)
	{
		this.shadowX = x;
		this.shadowY = y;
		this.shadowLife = life;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00006183 File Offset: 0x00004383
	public void setMabuHold(bool m)
	{
		this.isMabuHold = m;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x000457E0 File Offset: 0x000439E0
	public virtual void paint(mGraphics g)
	{
		if (this.isHide)
		{
			return;
		}
		if (this.isMabuHold)
		{
			if (this.cmtoChar)
			{
				GameScr.cmtoX = this.cx - GameScr.gW2;
				GameScr.cmtoY = this.cy - GameScr.gH23;
				if (!GameCanvas.isTouchControl)
				{
					GameScr.cmtoX += GameScr.gW6 * this.cdir;
				}
			}
			return;
		}
		if (!this.isPaint())
		{
			return;
		}
		if (!this.me && GameScr.notPaint)
		{
			return;
		}
		if (this.petFollow != null)
		{
			this.petFollow.paint(g);
		}
		this.paintMount1(g);
		if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
		{
			return;
		}
		if (this.isTeleport)
		{
			return;
		}
		if (this.holder && GameCanvas.gameTick % 2 == 0)
		{
			g.setColor(16185600);
			if (this.charHold != null)
			{
				g.drawLine(this.cx, this.cy - this.ch / 2, this.charHold.cx, this.charHold.cy - this.charHold.ch / 2);
			}
			if (this.mobHold != null)
			{
				g.drawLine(this.cx, this.cy - this.ch / 2, this.mobHold.x, this.mobHold.y - this.mobHold.h / 2);
			}
		}
		if (this.shadowLife > 0)
		{
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 25, true);
			}
			else if (this.shadowLife > 5)
			{
				this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 7, true);
			}
		}
		if (!this.isPaint() && this.skillPaint != null && (this.skillPaint.id < 70 || this.skillPaint.id > 76) && (this.skillPaint.id < 77 || this.skillPaint.id > 83))
		{
			if (this.skillPaint != null)
			{
				this.indexSkill = this.skillInfoPaint().Length;
				this.skillPaint = null;
			}
			this.effPaints = null;
			this.eff = null;
			this.effTask = null;
			this.indexEff = -1;
			this.indexEffTask = -1;
			return;
		}
		if (this.statusMe == 15 || (this.moveFast != null && this.moveFast[0] > 0))
		{
			return;
		}
		this.paintCharName_HP_MP_Overhead(g);
		if (this.skillPaint == null || this.skillInfoPaint() == null || this.indexSkill >= this.skillInfoPaint().Length)
		{
			this.paintCharWithoutSkill(g);
		}
		if (this.arr != null)
		{
			this.arr.paint(g);
		}
		if (this.dart != null)
		{
			this.dart.paint(g);
		}
		this.paintEffect(g);
		Mob mob = this.mobMe;
		this.paintMount2(g);
		if (this.statusMe == 1 || this.statusMe == 6)
		{
			if (this.isCopy)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			if (this.clevel == 14)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgBlue[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			if (this.clevel >= 15)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00045BE8 File Offset: 0x00043DE8
	private void paintEffect(mGraphics g)
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						int y = this.effPaints[i].eMob.y;
						if (this.effPaints[i].eMob is BigBoss)
						{
							y = this.effPaints[i].eMob.y - 60;
						}
						if (this.effPaints[i].eMob is BigBoss2)
						{
							y = this.effPaints[i].eMob.y - 50;
						}
						if (this.effPaints[i].eMob is BachTuoc)
						{
							y = this.effPaints[i].eMob.y - 40;
						}
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else if (this.effPaints[i].eChar != null)
					{
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null)
		{
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.indexEff].idImg, this.cx + this.eff.arrEfInfo[this.indexEff].dx, this.cy + this.eff.arrEfInfo[this.indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		if (this.indexEffTask >= 0 && this.effTask != null)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00003584 File Offset: 0x00001784
	private void paintArrowAttack(mGraphics g)
	{
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00045E3C File Offset: 0x0004403C
	public void paintHp(mGraphics g, int x, int y)
	{
		int num = this.cHP * 100 / this.cHPFull / 10 - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num > 9)
		{
			num = 9;
		}
		g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y, 3);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00045E88 File Offset: 0x00044088
	public int getClassColor()
	{
		int result = 9145227;
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			result = 16711680;
		}
		else if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			result = 33023;
		}
		else if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			result = 7443811;
		}
		return result;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00045F08 File Offset: 0x00044108
	public void paintNameInSameParty(mGraphics g)
	{
		if (this.cTypePk == 3 || this.cTypePk == 5)
		{
			return;
		}
		if (this.isPaint())
		{
			if (global::Char.myCharz().charFocus == null || !global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
				return;
			}
			if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00045FE8 File Offset: 0x000441E8
	private void paintCharName_HP_MP_Overhead(mGraphics g)
	{
		int num = this.ch + 5;
		if (this.isInvisiblez && !this.me)
		{
			return;
		}
		if (!this.me && TileMap.mapID == 113 && this.cy >= 360)
		{
			return;
		}
		if (!this.me)
		{
			bool flag = global::Char.myChar.clan != null && this.clanID == global::Char.myChar.clan.ID;
			bool flag2 = this.cTypePk == 3 || this.cTypePk == 5;
			bool flag3 = this.cTypePk == 4;
			if (this.cName.StartsWith("$"))
			{
				this.cName = this.cName.Substring(1);
				this.isPet = true;
			}
			if (this.cName.StartsWith("#"))
			{
				this.cName = this.cName.Substring(1);
				this.isMiniPet = true;
			}
			if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				num += 5;
				this.paintHp(g, this.cx, this.cy - num + 3);
			}
			mFont mFont = mFont.tahoma_7_whiteSmall;
			if (this.isNhapThe)
			{
				num += 10;
			}
			if (this.isPet || this.isMiniPet)
			{
				mFont = mFont.tahoma_7_blue1Small;
			}
			else if (flag2)
			{
				mFont = mFont.nameFontRed;
			}
			else if (flag3)
			{
				mFont = mFont.nameFontYellow;
			}
			else if (flag)
			{
				mFont = mFont.nameFontGreen;
			}
			if ((this.paintName || flag2 || flag3) && !flag)
			{
				if (mSystem.clientType == 1)
				{
					mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else
				{
					mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER);
				}
				num += mFont.tahoma_7.getHeight();
			}
			if (flag)
			{
				if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
				{
					mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
					return;
				}
				if (this.charFocus == null)
				{
					mFont.drawString(g, this.cName, this.cx - 10, this.cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
					this.paintHp(g, this.cx - 16, this.cy - num + 10);
				}
			}
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00046264 File Offset: 0x00044464
	public void paintShadow(mGraphics g)
	{
		if (this.isMabuHold)
		{
			return;
		}
		if (this.head == 377)
		{
			return;
		}
		if (this.leg == 471)
		{
			return;
		}
		if (this.isTeleport)
		{
			return;
		}
		if (this.isFlyUp)
		{
			return;
		}
		int size = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128)
		{
			if (TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4))
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else if (TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8))
			{
				g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
			}
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00046418 File Offset: 0x00044618
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.cx;
		if (TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			this.ySd = this.cy;
			return;
		}
		this.ySd = this.cy;
		while (i < 30)
		{
			i++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x000464B0 File Offset: 0x000446B0
	private void paintCharWithoutSkill(mGraphics g)
	{
		try
		{
			if (this.isInvisiblez)
			{
				if (this.me)
				{
					if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
					{
						SmallImage.drawSmallImage(g, 1196, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, 1195, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
			}
			else
			{
				this.paintCharBody(g, this.cx, this.cy + this.fy, this.cdir, this.cf, true);
			}
			if (this.isLockAttack)
			{
				SmallImage.drawSmallImage(g, 290, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi paint char without skill: " + ex.ToString());
		}
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x000465B4 File Offset: 0x000447B4
	public void paintBag(mGraphics g, short[] id, int x, int y, int dir, bool isPaintChar)
	{
		int num = 0;
		int num2 = 0;
		int vcenter_HCENTER = StaticObj.VCENTER_HCENTER;
		int transform = (dir != 1) ? 2 : 0;
		if (this.statusMe == 6)
		{
			num = 8;
			num2 = 17;
		}
		if (this.statusMe == 1)
		{
			if (this.cp1 % 15 < 5)
			{
				num = 8;
				num2 = 17;
			}
			else
			{
				num = 8;
				num2 = 18;
			}
		}
		if (this.statusMe == 2)
		{
			if (this.cf <= 3)
			{
				num = 7;
				num2 = 17;
			}
			else
			{
				num = 7;
				num2 = 18;
			}
		}
		if (this.statusMe == 3 || this.statusMe == 9)
		{
			num = 5;
			num2 = 20;
		}
		if (this.statusMe == 4)
		{
			if (this.cf == 8)
			{
				num = 5;
				num2 = 16;
			}
			else
			{
				num = 5;
				num2 = 20;
			}
		}
		if (this.statusMe == 10)
		{
			Res.outz("cf= " + this.cf);
			if (this.cf == 8)
			{
				num = 0;
				num2 = 23;
			}
			else
			{
				num = 5;
				num2 = 22;
			}
		}
		if (this.isInjure > 0)
		{
			num = 5;
			num2 = 18;
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length)
		{
			num = -1;
			num2 = 17;
		}
		if (!isPaintChar)
		{
			if (id.Length == 2)
			{
				SmallImage.drawSmallImage(g, (int)id[1], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
				return;
			}
			if (id.Length == 3)
			{
				if (id[2] < 0)
				{
					SmallImage.drawSmallImage(g, (int)id[1], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
					return;
				}
				if (GameCanvas.gameTick % 10 > 5)
				{
					SmallImage.drawSmallImage(g, (int)id[1], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
					return;
				}
				SmallImage.drawSmallImage(g, (int)id[2], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
				return;
			}
		}
		else
		{
			if (id.Length == 1)
			{
				SmallImage.drawSmallImage(g, (int)id[0], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
				return;
			}
			if (GameCanvas.gameTick % 10 > 5)
			{
				SmallImage.drawSmallImage(g, (int)id[0], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
				return;
			}
			SmallImage.drawSmallImage(g, (int)id[1], x + ((dir != 1) ? num : (-num)), y - num2, transform, vcenter_HCENTER);
		}
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x000467C0 File Offset: 0x000449C0
	public bool isCharBodyImageID(int id)
	{
		Part part = GameScr.parts[this.head];
		Part part2 = GameScr.parts[this.leg];
		Part part3 = GameScr.parts[this.body];
		for (int i = 0; i < global::Char.CharInfo.Length; i++)
		{
			if (id == (int)part.pi[global::Char.CharInfo[i][0][0]].id)
			{
				return true;
			}
			if (id == (int)part2.pi[global::Char.CharInfo[i][1][0]].id)
			{
				return true;
			}
			if (id == (int)part3.pi[global::Char.CharInfo[i][2][0]].id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0004685C File Offset: 0x00044A5C
	public void paintHead(mGraphics g, int cx, int cy, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000468A8 File Offset: 0x00044AA8
	public void paintHeadWithXY(mGraphics g, int x, int y, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, x + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00046918 File Offset: 0x00044B18
	public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
	{
		Part part = GameScr.parts[this.head];
		Part part2 = GameScr.parts[this.leg];
		Part part3 = GameScr.parts[this.body];
		if (this.bag >= 0 && this.statusMe != 14 && this.isMonkey == 0)
		{
			if (!ClanImage.idImages.containsKey(this.bag + string.Empty))
			{
				ClanImage.idImages.put(this.bag + string.Empty, new ClanImage());
				Service.gI().requestBagImage((sbyte)this.bag);
			}
			else
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(this.bag + string.Empty);
				if (clanImage.idImage != null && isPaintBag)
				{
					this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
				}
			}
		}
		if (cdir == 1)
		{
			if (this.statusMe == 14)
			{
				if (GameCanvas.gameTick % 4 > 0)
				{
					g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				int num = 0;
				if (this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466)
				{
					num = 15;
				}
				SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num, 0, StaticObj.TOP_CENTER);
				SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, 0, 0);
				if (this.head == 542 && GameCanvas.gameTick % 10 > 5)
				{
					Part part4 = GameScr.parts[543];
					SmallImage.drawSmallImage(g, (int)part4.pi[global::Char.CharInfo[cf][0][0]].id, cx + global::Char.CharInfo[cf][0][1] + (int)part4.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part4.pi[global::Char.CharInfo[cf][0][0]].dy, 0, 0);
				}
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, 0, 0);
				if (this.head == 542 && GameCanvas.gameTick % 10 > 5)
				{
					Part part5 = GameScr.parts[543];
					SmallImage.drawSmallImage(g, (int)part5.pi[global::Char.CharInfo[cf][0][0]].id, cx + global::Char.CharInfo[cf][0][1] + (int)part5.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part5.pi[global::Char.CharInfo[cf][0][0]].dy, 0, 0);
				}
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][1][0]].id, cx + global::Char.CharInfo[cf][1][1] + (int)part2.pi[global::Char.CharInfo[cf][1][0]].dx, cy - global::Char.CharInfo[cf][1][2] + (int)part2.pi[global::Char.CharInfo[cf][1][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[cf][2][0]].id, cx + global::Char.CharInfo[cf][2][1] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dx, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy, 0, 0);
			}
			this.ch = ((this.isMonkey != 1 && !this.isFusion) ? (global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
			if (this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && GameCanvas.gameTick % 30 == 0)
			{
				g.drawImage((this.cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx - ((this.cgender != 1) ? 2 : 2), cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, 0);
			}
		}
		else
		{
			if (this.statusMe == 14)
			{
				if (GameCanvas.gameTick % 4 > 0)
				{
					g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				int num2 = 0;
				if (this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466)
				{
					num2 = 15;
				}
				SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num2, 2, StaticObj.TOP_CENTER);
				SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx - global::Char.CharInfo[cf][0][1] - (int)part.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, 2, 24);
				if (this.head == 542 && GameCanvas.gameTick % 10 > 5)
				{
					Part part6 = GameScr.parts[543];
					SmallImage.drawSmallImage(g, (int)part6.pi[global::Char.CharInfo[cf][0][0]].id, cx - global::Char.CharInfo[cf][0][1] - (int)part6.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part6.pi[global::Char.CharInfo[cf][0][0]].dy, 2, 24);
				}
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx - global::Char.CharInfo[cf][0][1] - (int)part.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, 2, 24);
				if (this.head == 542 && GameCanvas.gameTick % 10 > 5)
				{
					Part part7 = GameScr.parts[543];
					SmallImage.drawSmallImage(g, (int)part7.pi[global::Char.CharInfo[cf][0][0]].id, cx - global::Char.CharInfo[cf][0][1] - (int)part7.pi[global::Char.CharInfo[cf][0][0]].dx, cy - global::Char.CharInfo[cf][0][2] + (int)part7.pi[global::Char.CharInfo[cf][0][0]].dy, 2, 24);
				}
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][1][0]].id, cx - global::Char.CharInfo[cf][1][1] - (int)part2.pi[global::Char.CharInfo[cf][1][0]].dx, cy - global::Char.CharInfo[cf][1][2] + (int)part2.pi[global::Char.CharInfo[cf][1][0]].dy, 2, 24);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[cf][2][0]].id, cx - global::Char.CharInfo[cf][2][1] - (int)part3.pi[global::Char.CharInfo[cf][2][0]].dx, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy, 2, 24);
			}
			this.ch = ((this.isMonkey != 1 && !this.isFusion) ? (global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
			if (this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && ((GameCanvas.gameTick + this.charID) % 30 == 0 || this.isFreez))
			{
				g.drawImage((this.cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx + ((this.cgender != 1) ? 2 : 2), cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, StaticObj.TOP_RIGHT);
			}
		}
		if (this.eProtect != null)
		{
			this.eProtect.paint(g);
		}
		this.paintPKFlag(g);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00047410 File Offset: 0x00045610
	public void paintCharWithSkill(mGraphics g)
	{
		this.ty = 0;
		SkillInfoPaint[] array = this.skillInfoPaint();
		this.cf = array[this.indexSkill].status;
		this.paintCharWithoutSkill(g);
		if (this.cdir == 1)
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx + this.dx0 + this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = (this.dx0 = (this.dy0 = 0));
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx + this.dx1 + this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = (this.dx1 = (this.dy1 = 0));
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx + this.dx2 + this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = (this.dx2 = (this.dy2 = 0));
				}
			}
		}
		else
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx - this.dx0 - this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = 0;
					this.dx0 = 0;
					this.dy0 = 0;
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx - this.dx1 - this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = 0;
					this.dx1 = 0;
					this.dy1 = 0;
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx - this.dx2 - this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = 0;
					this.dx2 = 0;
					this.dy2 = 0;
				}
			}
		}
		this.indexSkill++;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00047A28 File Offset: 0x00045C28
	public static int getIndexChar(int ID)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			if (((global::Char)GameScr.vCharInMap.elementAt(i)).charID == ID)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00047A68 File Offset: 0x00045C68
	public void moveTo(int toX, int toY)
	{
		if (Res.abs(toX - this.cx) > 100 || Res.abs(toY - this.cy) > 300)
		{
			this.createShadow(this.cx, this.cy, 10);
			this.cx = toX;
			this.cy = toY;
			this.vMovePoints.removeAllElements();
			this.statusMe = 6;
			this.cp3 = 0;
			this.currentMovePoint = null;
			this.cf = 25;
			return;
		}
		int dir = 0;
		int act = 0;
		int num = toX - this.cx;
		int num2 = toY - this.cy;
		if (num == 0 && num2 == 0)
		{
			act = 1;
			this.cp3 = 0;
		}
		else if (num2 == 0)
		{
			act = 2;
			if (num > 0)
			{
				dir = 1;
			}
			if (num < 0)
			{
				dir = -1;
			}
		}
		else if (num2 != 0)
		{
			if (num2 < 0)
			{
				act = 3;
			}
			if (num2 > 0)
			{
				act = 4;
			}
			if (num < 0)
			{
				dir = -1;
			}
			if (num > 0)
			{
				dir = 1;
			}
		}
		this.vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
		if (this.statusMe != 6)
		{
			this.statusBeforeNothing = this.statusMe;
		}
		this.statusMe = 6;
		this.cp3 = 0;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00047B74 File Offset: 0x00045D74
	public static void getcharInjure(int cID, int dx, int dy, int HP)
	{
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(cID);
		if (@char.vMovePoints.size() == 0)
		{
			return;
		}
		MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
		int xEnd = movePoint.xEnd + dx;
		int yEnd = movePoint.yEnd + dy;
		global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(cID);
		char2.cHP -= HP;
		if (char2.cHP < 0)
		{
			char2.cHP = 0;
		}
		char2.cHPShow = ((global::Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
		char2.statusMe = 6;
		char2.cp3 = 0;
		char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00047C34 File Offset: 0x00045E34
	public bool isMagicTree()
	{
		if (GameScr.gI().magicTree != null)
		{
			int x = GameScr.gI().magicTree.x;
			int y = GameScr.gI().magicTree.y;
			return this.cx > x - 30 && this.cx < x + 30 && this.cy > y - 30 && this.cy < y + 30;
		}
		return false;
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00047CA4 File Offset: 0x00045EA4
	public void searchItem()
	{
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		if (this.itemFocus == null)
		{
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
				int num2 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
				int num3 = (num <= num2) ? num2 : num;
				if (num <= 48 && num2 <= 48 && (this.itemFocus == null || num3 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap.template.type == 9)
						{
							this.itemFocus = itemMap;
							array[3] = num3;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00047D90 File Offset: 0x00045F90
	public void searchFocus()
	{
		if (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null)
		{
			this.timeFocusToMob = 200;
			return;
		}
		if (this.timeFocusToMob > 0)
		{
			this.timeFocusToMob--;
			return;
		}
		if (global::Char.isManualFocus && this.charFocus != null && (this.charFocus.statusMe == 15 || this.charFocus.isInvisiblez))
		{
			this.charFocus = null;
		}
		if (GameCanvas.gameTick % 2 == 0)
		{
			return;
		}
		if (this.isMeCanAttackOtherPlayer(this.charFocus))
		{
			return;
		}
		int num = 0;
		if (this.nClass.classId == 0 || this.nClass.classId == 1 || this.nClass.classId == 3 || this.nClass.classId == 5)
		{
			num = 40;
		}
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		int num2 = GameScr.cmx - 10;
		int num3 = GameScr.cmx + GameCanvas.w + 10;
		int num4 = GameScr.cmy;
		int num5 = GameScr.cmy + GameCanvas.h - GameScr.cmdBarH + 10;
		if (global::Char.isManualFocus)
		{
			if ((this.mobFocus != null && this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5) || (this.npcFocus != null && num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5) || (this.charFocus != null && num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5) || (this.itemFocus != null && num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5))
			{
				return;
			}
			global::Char.isManualFocus = false;
		}
		num2 = global::Char.myCharz().cx - 80;
		num3 = global::Char.myCharz().cx + 80;
		num4 = global::Char.myCharz().cy - 30;
		num5 = global::Char.myCharz().cy + 30;
		if (this.npcFocus != null && this.npcFocus.template.npcTemplateId == 6)
		{
			num2 = global::Char.myCharz().cx - 20;
			num3 = global::Char.myCharz().cx + 20;
			num4 = global::Char.myCharz().cy - 10;
			num5 = global::Char.myCharz().cy + 10;
		}
		if (this.npcFocus == null)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.statusMe != 15)
				{
					int num6 = global::Math.abs(global::Char.myCharz().cx - npc.cx);
					int num7 = global::Math.abs(global::Char.myCharz().cy - npc.cy);
					int num8 = (num6 <= num7) ? num7 : num6;
					num2 = global::Char.myCharz().cx - 80;
					num3 = global::Char.myCharz().cx + 80;
					num4 = global::Char.myCharz().cy - 30;
					num5 = global::Char.myCharz().cy + 30;
					if (npc.template.npcTemplateId == 6)
					{
						num2 = global::Char.myCharz().cx - 20;
						num3 = global::Char.myCharz().cx + 20;
						num4 = global::Char.myCharz().cy - 10;
						num5 = global::Char.myCharz().cy + 10;
					}
					if (num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5 && (this.npcFocus == null || num8 < array[1]))
					{
						this.npcFocus = npc;
						array[1] = num8;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5)
			{
				this.clearFocus(1);
				return;
			}
			this.deFocusNPC();
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc2 = (Npc)GameScr.vNpc.elementAt(j);
				if (npc2.statusMe != 15)
				{
					int num9 = global::Math.abs(global::Char.myCharz().cx - npc2.cx);
					int num10 = global::Math.abs(global::Char.myCharz().cy - npc2.cy);
					int num11 = (num9 <= num10) ? num10 : num9;
					num2 = global::Char.myCharz().cx - 80;
					num3 = global::Char.myCharz().cx + 80;
					num4 = global::Char.myCharz().cy - 30;
					num5 = global::Char.myCharz().cy + 30;
					if (npc2.template.npcTemplateId == 6)
					{
						num2 = global::Char.myCharz().cx - 20;
						num3 = global::Char.myCharz().cx + 20;
						num4 = global::Char.myCharz().cy - 10;
						num5 = global::Char.myCharz().cy + 10;
					}
					if (num2 <= npc2.cx && npc2.cx <= num3 && num4 <= npc2.cy && npc2.cy <= num5 && (this.npcFocus == null || num11 < array[1]))
					{
						this.npcFocus = npc2;
						array[1] = num11;
					}
				}
			}
		}
		if (this.itemFocus == null)
		{
			for (int k = 0; k < GameScr.vItemMap.size(); k++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
				int num12 = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
				int num13 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
				int num14 = (num12 <= num13) ? num13 : num12;
				if (num12 <= 48 && num13 <= 48 && (this.itemFocus == null || num14 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap.template.type == 9)
						{
							this.itemFocus = itemMap;
							array[3] = num14;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num14;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5)
			{
				this.clearFocus(3);
				return;
			}
			this.itemFocus = null;
			for (int l = 0; l < GameScr.vItemMap.size(); l++)
			{
				ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
				int num15 = global::Math.abs(global::Char.myCharz().cx - itemMap2.x);
				int num16 = global::Math.abs(global::Char.myCharz().cy - itemMap2.y);
				int num17 = (num15 <= num16) ? num16 : num15;
				if (num2 <= itemMap2.x && itemMap2.x <= num3 && num4 <= itemMap2.y && itemMap2.y <= num5 && (this.itemFocus == null || num17 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap2.template.type == 9)
						{
							this.itemFocus = itemMap2;
							array[3] = num17;
						}
					}
					else
					{
						this.itemFocus = itemMap2;
						array[3] = num17;
					}
				}
			}
		}
		num2 = global::Char.myCharz().cx - global::Char.myCharz().getdxSkill() - 10;
		num3 = global::Char.myCharz().cx + global::Char.myCharz().getdxSkill() + 10;
		num4 = global::Char.myCharz().cy - global::Char.myCharz().getdySkill() - num - 20;
		num5 = global::Char.myCharz().cy + global::Char.myCharz().getdySkill() + 20;
		if (num5 > global::Char.myCharz().cy + 30)
		{
			num5 = global::Char.myCharz().cy + 30;
		}
		if (this.mobFocus == null)
		{
			for (int m = 0; m < GameScr.vMob.size(); m++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(m);
				int num18 = global::Math.abs(global::Char.myCharz().cx - mob.x);
				int num19 = global::Math.abs(global::Char.myCharz().cy - mob.y);
				int num20 = (num18 <= num19) ? num19 : num18;
				if (num2 <= mob.x && mob.x <= num3 && num4 <= mob.y && mob.y <= num5 && (this.mobFocus == null || num20 < array[0]))
				{
					this.mobFocus = mob;
					array[0] = num20;
				}
			}
		}
		else
		{
			if (this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5)
			{
				this.clearFocus(0);
				return;
			}
			this.mobFocus = null;
			for (int n = 0; n < GameScr.vMob.size(); n++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(n);
				int num21 = global::Math.abs(global::Char.myCharz().cx - mob2.x);
				int num22 = global::Math.abs(global::Char.myCharz().cy - mob2.y);
				int num23 = (num21 <= num22) ? num22 : num21;
				if (num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5 && (this.mobFocus == null || num23 < array[0]))
				{
					this.mobFocus = mob2;
					array[0] = num23;
				}
			}
		}
		if (this.charFocus == null)
		{
			for (int num24 = 0; num24 < GameScr.vCharInMap.size(); num24++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num24);
				if (@char.statusMe != 15 && !@char.isInvisiblez && this.wdx == 0 && this.wdy == 0)
				{
					int num25 = global::Math.abs(global::Char.myCharz().cx - @char.cx);
					int num26 = global::Math.abs(global::Char.myCharz().cy - @char.cy);
					int num27 = (num25 <= num26) ? num26 : num25;
					if (num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && (this.charFocus == null || num27 < array[2]))
					{
						this.charFocus = @char;
						array[2] = num27;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5 && this.charFocus.statusMe != 15 && !this.charFocus.isInvisiblez)
			{
				this.clearFocus(2);
				return;
			}
			this.charFocus = null;
			for (int num28 = 0; num28 < GameScr.vCharInMap.size(); num28++)
			{
				global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(num28);
				if (char2.statusMe != 15 && !char2.isInvisiblez && this.wdx == 0 && this.wdy == 0)
				{
					int num29 = global::Math.abs(global::Char.myCharz().cx - char2.cx);
					int num30 = global::Math.abs(global::Char.myCharz().cy - char2.cy);
					int num31 = (num29 <= num30) ? num30 : num29;
					if (num2 <= char2.cx && char2.cx <= num3 && num4 <= char2.cy && char2.cy <= num5 && (this.charFocus == null || num31 < array[2]))
					{
						this.charFocus = char2;
						array[2] = num31;
					}
				}
			}
		}
		int num32 = -1;
		for (int num33 = 0; num33 < array.Length; num33++)
		{
			if (num32 == -1)
			{
				if (array[num33] != -1)
				{
					num32 = num33;
				}
			}
			else if (array[num33] < array[num32] && array[num33] != -1)
			{
				num32 = num33;
			}
		}
		this.clearFocus(num32);
		if (this.me && this.isAttacPlayerStatus())
		{
			if (this.mobFocus != null && !this.mobFocus.isMobMe)
			{
				this.mobFocus = null;
			}
			this.npcFocus = null;
			this.itemFocus = null;
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00048AB0 File Offset: 0x00046CB0
	public void clearFocus(int index)
	{
		if (index == 0)
		{
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			return;
		}
		if (index == 1)
		{
			this.mobFocus = null;
			this.charFocus = null;
			this.itemFocus = null;
			return;
		}
		if (index == 2)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.itemFocus = null;
			return;
		}
		if (index == 3)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00048B20 File Offset: 0x00046D20
	public static bool isCharInScreen(global::Char c)
	{
		int cmx = GameScr.cmx;
		int num = GameScr.cmx + GameCanvas.w;
		int num2 = GameScr.cmy + 10;
		int num3 = GameScr.cmy + GameScr.gH;
		return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0000618C File Offset: 0x0000438C
	public bool isAttacPlayerStatus()
	{
		return this.cTypePk == 4 || this.cTypePk == 3;
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x000061A2 File Offset: 0x000043A2
	public void setHoldChar(global::Char r)
	{
		if (this.cx < r.cx)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.charHold = r;
		this.holder = true;
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x000061D0 File Offset: 0x000043D0
	public void setHoldMob(Mob r)
	{
		if (this.cx < r.x)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.mobHold = r;
		this.holder = true;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00048B90 File Offset: 0x00046D90
	public void findNextFocusByKey()
	{
		Res.outz("focus size= " + this.focus.size());
		if ((global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null) && this.focus.size() == 0)
		{
			return;
		}
		this.focus.removeAllElements();
		int num = 0;
		int num2 = GameScr.cmx + 10;
		int num3 = GameScr.cmx + GameCanvas.w - 10;
		int num4 = GameScr.cmy + 10;
		int num5 = GameScr.cmy + GameScr.gH;
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.statusMe != 15 && !@char.isInvisiblez && num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && global::Char.myCharz().cy > 264)))
			{
				this.focus.addElement(@char);
				if (this.charFocus != null && @char.Equals(this.charFocus))
				{
					num = this.focus.size();
				}
			}
		}
		if (this.me && this.isAttacPlayerStatus())
		{
			Res.outz("co the tan cong nguoi");
			for (int j = 0; j < GameScr.vMob.size(); j++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(j);
				if (!GameScr.gI().isMeCanAttackMob(mob))
				{
					Res.outz("khong the tan cong quai");
					this.mobFocus = null;
				}
				else
				{
					Res.outz("co the tan ong quai");
					this.focus.addElement(mob);
					if (this.mobFocus != null)
					{
						num = this.focus.size();
					}
				}
			}
			this.npcFocus = null;
			this.itemFocus = null;
			if (this.focus.size() > 0)
			{
				if (num >= this.focus.size())
				{
					num = 0;
				}
				this.focusManualTo(this.focus.elementAt(num));
				return;
			}
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			global::Char.isManualFocus = false;
			return;
		}
		else
		{
			for (int k = 0; k < GameScr.vItemMap.size(); k++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
				if (num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5)
				{
					this.focus.addElement(itemMap);
					if (this.itemFocus != null && itemMap.Equals(this.itemFocus))
					{
						num = this.focus.size();
					}
				}
			}
			for (int l = 0; l < GameScr.vMob.size(); l++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
				if (mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5)
				{
					this.focus.addElement(mob2);
					if (this.mobFocus != null && mob2.Equals(this.mobFocus))
					{
						num = this.focus.size();
					}
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(m);
				if (npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5)
				{
					this.focus.addElement(npc);
					if (this.npcFocus != null && npc.Equals(this.npcFocus))
					{
						num = this.focus.size();
					}
				}
			}
			if (this.focus.size() > 0)
			{
				if (num >= this.focus.size())
				{
					num = 0;
				}
				this.focusManualTo(this.focus.elementAt(num));
				return;
			}
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			global::Char.isManualFocus = false;
			return;
		}
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x000061FE File Offset: 0x000043FE
	public void deFocusNPC()
	{
		if (this.me && this.npcFocus != null)
		{
			if (!GameCanvas.menu.showMenu)
			{
				global::Char.chatPopup = null;
			}
			this.npcFocus = null;
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00049014 File Offset: 0x00047214
	public void updateCharInBridge()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (TileMap.tileTypeAt(this.cx, this.cy + 1, 1024))
		{
			TileMap.setTileTypeAtPixel(this.cx, this.cy + 1, 512);
			TileMap.setTileTypeAtPixel(this.cx, this.cy - 2, 512);
		}
		if (TileMap.tileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512))
		{
			TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
			TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy - 2, 512);
		}
		if (TileMap.tileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512))
		{
			TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
			TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy - 2, 512);
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0004912C File Offset: 0x0004732C
	public static void sort(int[] data)
	{
		int num = 5;
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				if (data[i] < data[j])
				{
					int num2 = data[j];
					data[j] = data[i];
					data[i] = num2;
				}
			}
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00006229 File Offset: 0x00004429
	public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
	{
		return x <= cmWx && x >= cmX && y <= cmyH && y >= cmy;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00049170 File Offset: 0x00047370
	public void kickOption(Item item, int maxKick)
	{
		int num = 0;
		if (item != null && item.options != null)
		{
			for (int i = 0; i < item.options.size(); i++)
			{
				ItemOption itemOption = (ItemOption)item.options.elementAt(i);
				itemOption.active = 0;
				if (itemOption.optionTemplate.type == 2)
				{
					if (num < maxKick)
					{
						itemOption.active = 1;
						num++;
					}
				}
				else if (itemOption.optionTemplate.type == 3 && item.upgrade >= 4)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 4 && item.upgrade >= 8)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 5 && item.upgrade >= 12)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 6 && item.upgrade >= 14)
				{
					itemOption.active = 1;
				}
				else if (itemOption.optionTemplate.type == 7 && item.upgrade >= 16)
				{
					itemOption.active = 1;
				}
			}
		}
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00049290 File Offset: 0x00047490
	public void doInjure(int HPShow, int MPShow, bool isCrit, bool isMob)
	{
		this.isCrit = isCrit;
		this.isMob = isMob;
		Res.outz(string.Concat(new object[]
		{
			"CHP= ",
			this.cHP,
			" dame -= ",
			HPShow,
			" HP FULL= ",
			this.cHPFull
		}));
		this.cHP -= HPShow;
		this.cMP -= MPShow;
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		if (this.cHP < 0)
		{
			this.cHP = 0;
		}
		if (this.cMP < 0)
		{
			this.cMP = 0;
		}
		if (isMob || (!isMob && this.cTypePk != 4 && this.damMP != -100))
		{
			if (HPShow <= 0)
			{
				if (this.me)
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS_ME);
				}
				else
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS);
				}
			}
			else
			{
				GameScr.startFlyText("-" + HPShow, this.cx, this.cy - this.ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
			}
		}
		if (HPShow > 0)
		{
			this.isInjure = 6;
		}
		ServerEffect.addServerEffect(80, this, 1);
		if (this.isDie)
		{
			this.isDie = false;
			global::Char.isLockKey = false;
			this.startDie((short)this.xSd, (short)this.ySd);
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00049458 File Offset: 0x00047658
	public void doInjure()
	{
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		this.isInjure = 6;
		ServerEffect.addServerEffect(8, this, 1);
		this.isInjureHp = true;
		this.twHp = 0;
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x000494B0 File Offset: 0x000476B0
	public void startDie(short toX, short toY)
	{
		this.isMonkey = 0;
		this.isWaitMonkey = false;
		if (this.me && this.isDie)
		{
			return;
		}
		if (this.me)
		{
			this.isLockMove = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).killCharId = -9999;
			}
			if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
			{
				GameCanvas.panel.cp = null;
			}
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		this.statusMe = 5;
		this.cp2 = (int)toX;
		this.cp3 = (int)toY;
		this.cp1 = 0;
		this.cHP = 0;
		this.testCharId = -9999;
		this.killCharId = -9999;
		if (this.me && this.myskill != null && this.myskill.template.id != 14)
		{
			this.stopUseChargeSkill();
		}
		this.cTypePk = 0;
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00006243 File Offset: 0x00004443
	public void waitToDie(short toX, short toY)
	{
		this.wdx = toX;
		this.wdy = toY;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x000495C0 File Offset: 0x000477C0
	public void liveFromDead()
	{
		this.cHP = this.cHPFull;
		this.cMP = this.cMPFull;
		this.statusMe = 1;
		this.cp1 = (this.cp2 = (this.cp3 = 0));
		ServerEffect.addServerEffect(109, this, 2);
		GameScr.gI().center = null;
		GameScr.isHaveSelectSkill = true;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00049620 File Offset: 0x00047820
	public bool doUsePotion()
	{
		if (this.arrItemBag == null)
		{
			return false;
		}
		for (int i = 0; i < this.arrItemBag.Length; i++)
		{
			if (this.arrItemBag[i] != null && this.arrItemBag[i].template.type == 6)
			{
				Service.gI().useItem(0, 1, -1, this.arrItemBag[i].template.id);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0004968C File Offset: 0x0004788C
	public bool isLang()
	{
		return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000496F4 File Offset: 0x000478F4
	public bool isMeCanAttackOtherPlayer(global::Char cAtt)
	{
		return cAtt != null && global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type != 2 && (global::Char.myCharz().myskill.template.type != 4 || cAtt.statusMe == 14 || cAtt.statusMe == 5) && ((cAtt.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || (global::Char.myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && cAtt.cTypePk == 1)) || (global::Char.myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (global::Char.myCharz().testCharId >= 0 && global::Char.myCharz().testCharId == cAtt.charID) || (global::Char.myCharz().killCharId >= 0 && global::Char.myCharz().killCharId == cAtt.charID && !this.isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == global::Char.myCharz().charID && !this.isLang()) || (global::Char.myCharz().cFlag == 8 && cAtt.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && cAtt.cFlag == 8) || (global::Char.myCharz().cFlag != cAtt.cFlag && global::Char.myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14 && cAtt.statusMe != 5;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00049898 File Offset: 0x00047A98
	public void clearTask()
	{
		global::Char.myCharz().taskMaint = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.type == 8)
			{
				global::Char.myCharz().arrItemBag[i] = null;
			}
		}
		Npc.clearEffTask();
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00006253 File Offset: 0x00004453
	public int getX()
	{
		return this.cx;
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0000625B File Offset: 0x0000445B
	public int getY()
	{
		return this.cy;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00006263 File Offset: 0x00004463
	public int getH()
	{
		return 32;
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00006267 File Offset: 0x00004467
	public int getW()
	{
		return 24;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00049900 File Offset: 0x00047B00
	public void focusManualTo(object objectz)
	{
		if (objectz is Mob)
		{
			this.mobFocus = (Mob)objectz;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else if (objectz is Npc)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().npcFocus = (Npc)objectz;
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is global::Char)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = (global::Char)objectz;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is ItemMap)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = (ItemMap)objectz;
		}
		global::Char.isManualFocus = true;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00003584 File Offset: 0x00001784
	public void stopMoving()
	{
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00003584 File Offset: 0x00001784
	public void cancelAttack()
	{
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00003868 File Offset: 0x00001A68
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0000626B File Offset: 0x0000446B
	public bool focusToAttack()
	{
		return this.mobFocus != null || (this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus));
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x000499F8 File Offset: 0x00047BF8
	public void addDustEff(int type)
	{
		if (!GameCanvas.lowGraphic)
		{
			if (type == 1)
			{
				if (this.clevel >= 9)
				{
					EffecMn.addEff(new Effect(19, this.cx - 5, this.cy + 20, 2, 1, -1));
					return;
				}
			}
			else if (type == 2)
			{
				if (this.me && this.isMonkey == 1)
				{
					return;
				}
				if (this.isNhapThe && GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(22, this.cx - 5, this.cy + 35, 2, 1, -1));
					return;
				}
			}
			else if (type == 3 && this.clevel >= 9 && this.ySd - this.cy <= 5)
			{
				EffecMn.addEff(new Effect(19, this.cx - 5, this.ySd + 20, 2, 1, -1));
			}
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00049AC8 File Offset: 0x00047CC8
	public bool isGetFlagImage(sbyte getFlag)
	{
		bool result = true;
		for (int i = 0; i < GameScr.vFlag.size(); i++)
		{
			PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
			if (pkflag != null)
			{
				if (pkflag.cflag == getFlag)
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00049B10 File Offset: 0x00047D10
	private void paintPKFlag(mGraphics g)
	{
		if (this.cdir == 1)
		{
			if (this.cFlag != 0 && this.cFlag != -1)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx - 10, this.cy - this.ch - ((!this.me) ? 0 : 20) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 2, 0);
				return;
			}
		}
		else if (this.cFlag != 0 && this.cFlag != -1)
		{
			SmallImage.drawSmallImage(g, this.flagImage, this.cx, this.cy - this.ch - ((!this.me) ? 0 : 20) + ((GameCanvas.gameTick % 20 <= 10) ? 0 : (GameCanvas.gameTick % 4 / 2)), 0, 0);
		}
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0000628D File Offset: 0x0000448D
	public void removeHoleEff()
	{
		if (this.holder)
		{
			this.holder = false;
			this.charHold = null;
			this.mobHold = null;
			return;
		}
		this.holdEffID = 0;
		this.charHold = null;
		this.mobHold = null;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x000062C2 File Offset: 0x000044C2
	public void removeProtectEff()
	{
		this.protectEff = false;
		this.eProtect = null;
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x000062D2 File Offset: 0x000044D2
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00049BE4 File Offset: 0x00047DE4
	public void removeEffect()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
		if (this.holder)
		{
			this.holder = false;
		}
		if (this.protectEff)
		{
			this.protectEff = false;
		}
		this.eProtect = null;
		this.charHold = null;
		this.mobHold = null;
		this.blindEff = false;
		this.sleepEff = false;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00049C44 File Offset: 0x00047E44
	public void setPos(short xPos, short yPos, sbyte typePos)
	{
		this.isSetPos = true;
		this.xPos = xPos;
		this.yPos = yPos;
		this.typePos = typePos;
		this.tpos = 0;
		if (this.me)
		{
			if (GameCanvas.panel != null)
			{
				GameCanvas.panel.hide();
			}
			if (GameCanvas.panel2 != null)
			{
				GameCanvas.panel2.hide();
			}
			if (TileMap.mapID == 113)
			{
				this.cTypePk = 0;
				this.charFocus = null;
			}
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x000062DB File Offset: 0x000044DB
	public void removeHuytSao()
	{
		this.huytSao = false;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x000062E4 File Offset: 0x000044E4
	public void fusionComplete()
	{
		this.isFusion = false;
		global::Char.isLockKey = false;
		this.tFusion = 0;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00049CB8 File Offset: 0x00047EB8
	public void setFusion(sbyte fusion)
	{
		this.tFusion = 0;
		if (fusion == 4 || fusion == 5)
		{
			if (this.me)
			{
				Service.gI().funsion(fusion);
			}
			EffecMn.addEff(new Effect(34, this.cx, this.cy + 12, 2, 1, -1));
		}
		if (fusion == 6)
		{
			EffecMn.addEff(new Effect(38, this.cx, this.cy + 12, 2, 1, -1));
		}
		if (this.me)
		{
			GameCanvas.panel.hideNow();
			global::Char.isLockKey = true;
		}
		this.isFusion = true;
		if (fusion == 1)
		{
			this.isNhapThe = false;
			return;
		}
		this.isNhapThe = true;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x000062FA File Offset: 0x000044FA
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x04000966 RID: 2406
	public const sbyte A_STAND = 1;

	// Token: 0x04000967 RID: 2407
	public const sbyte A_RUN = 2;

	// Token: 0x04000968 RID: 2408
	public const sbyte A_JUMP = 3;

	// Token: 0x04000969 RID: 2409
	public const sbyte A_FALL = 4;

	// Token: 0x0400096A RID: 2410
	public const sbyte A_DEADFLY = 5;

	// Token: 0x0400096B RID: 2411
	public const sbyte A_NOTHING = 6;

	// Token: 0x0400096C RID: 2412
	public const sbyte A_ATTK = 7;

	// Token: 0x0400096D RID: 2413
	public const sbyte A_INJURE = 8;

	// Token: 0x0400096E RID: 2414
	public const sbyte A_AUTOJUMP = 9;

	// Token: 0x0400096F RID: 2415
	public const sbyte A_FLY = 10;

	// Token: 0x04000970 RID: 2416
	public const sbyte SKILL_STAND = 12;

	// Token: 0x04000971 RID: 2417
	public const sbyte SKILL_FALL = 13;

	// Token: 0x04000972 RID: 2418
	public const sbyte A_DEAD = 14;

	// Token: 0x04000973 RID: 2419
	public const sbyte A_HIDE = 15;

	// Token: 0x04000974 RID: 2420
	public const sbyte A_RESETPOINT = 16;

	// Token: 0x04000975 RID: 2421
	public const sbyte PK_NORMAL = 0;

	// Token: 0x04000976 RID: 2422
	public const sbyte PK_PHE = 1;

	// Token: 0x04000977 RID: 2423
	public const sbyte PK_BANG = 2;

	// Token: 0x04000978 RID: 2424
	public const sbyte PK_THIDAU = 3;

	// Token: 0x04000979 RID: 2425
	public const sbyte PK_LUYENTAP = 4;

	// Token: 0x0400097A RID: 2426
	public const sbyte PK_TUDO = 5;

	// Token: 0x0400097B RID: 2427
	public long lastUpdateTime;

	// Token: 0x0400097C RID: 2428
	public bool meLive;

	// Token: 0x0400097D RID: 2429
	public bool isMask;

	// Token: 0x0400097E RID: 2430
	public bool isTeleport;

	// Token: 0x0400097F RID: 2431
	public bool isUsePlane;

	// Token: 0x04000980 RID: 2432
	public int shadowX;

	// Token: 0x04000981 RID: 2433
	public int shadowY;

	// Token: 0x04000982 RID: 2434
	public int shadowLife;

	// Token: 0x04000983 RID: 2435
	public bool isNhapThe;

	// Token: 0x04000984 RID: 2436
	public PetFollow petFollow;

	// Token: 0x04000985 RID: 2437
	public int rank;

	// Token: 0x04000986 RID: 2438
	public static ChatPopup chatPopup;

	// Token: 0x04000987 RID: 2439
	public long cPower;

	// Token: 0x04000988 RID: 2440
	public Info chatInfo;

	// Token: 0x04000989 RID: 2441
	public sbyte petStatus;

	// Token: 0x0400098A RID: 2442
	public int cx = 24;

	// Token: 0x0400098B RID: 2443
	public int cy = 24;

	// Token: 0x0400098C RID: 2444
	public int cvx;

	// Token: 0x0400098D RID: 2445
	public int cvy;

	// Token: 0x0400098E RID: 2446
	public int cp1;

	// Token: 0x0400098F RID: 2447
	public int cp2;

	// Token: 0x04000990 RID: 2448
	public int cp3;

	// Token: 0x04000991 RID: 2449
	public int statusMe = 5;

	// Token: 0x04000992 RID: 2450
	public int cdir = 1;

	// Token: 0x04000993 RID: 2451
	public int charID;

	// Token: 0x04000994 RID: 2452
	public int cgender;

	// Token: 0x04000995 RID: 2453
	public int ctaskId;

	// Token: 0x04000996 RID: 2454
	public int menuSelect;

	// Token: 0x04000997 RID: 2455
	public int cBonusSpeed;

	// Token: 0x04000998 RID: 2456
	public int cspeed = 4;

	// Token: 0x04000999 RID: 2457
	public int ccurrentAttack;

	// Token: 0x0400099A RID: 2458
	public int cDamFull;

	// Token: 0x0400099B RID: 2459
	public int cDefull;

	// Token: 0x0400099C RID: 2460
	public int cCriticalFull;

	// Token: 0x0400099D RID: 2461
	public int clevel;

	// Token: 0x0400099E RID: 2462
	public int cMP;

	// Token: 0x0400099F RID: 2463
	public int cHP;

	// Token: 0x040009A0 RID: 2464
	public int cHPNew;

	// Token: 0x040009A1 RID: 2465
	public int cMaxEXP;

	// Token: 0x040009A2 RID: 2466
	public int cHPShow;

	// Token: 0x040009A3 RID: 2467
	public int xReload;

	// Token: 0x040009A4 RID: 2468
	public int yReload;

	// Token: 0x040009A5 RID: 2469
	public int cyStartFall;

	// Token: 0x040009A6 RID: 2470
	public int saveStatus;

	// Token: 0x040009A7 RID: 2471
	public int eff5BuffHp;

	// Token: 0x040009A8 RID: 2472
	public int eff5BuffMp;

	// Token: 0x040009A9 RID: 2473
	public int cHPFull;

	// Token: 0x040009AA RID: 2474
	public int cMPFull;

	// Token: 0x040009AB RID: 2475
	public int cdameDown;

	// Token: 0x040009AC RID: 2476
	public int cStr;

	// Token: 0x040009AD RID: 2477
	public long cLevelPercent;

	// Token: 0x040009AE RID: 2478
	public long cTiemNang;

	// Token: 0x040009AF RID: 2479
	public long cNangdong;

	// Token: 0x040009B0 RID: 2480
	public int damHP;

	// Token: 0x040009B1 RID: 2481
	public int damMP;

	// Token: 0x040009B2 RID: 2482
	public bool isMob;

	// Token: 0x040009B3 RID: 2483
	public bool isCrit;

	// Token: 0x040009B4 RID: 2484
	public bool isDie;

	// Token: 0x040009B5 RID: 2485
	public int pointUydanh;

	// Token: 0x040009B6 RID: 2486
	public int pointNon;

	// Token: 0x040009B7 RID: 2487
	public int pointVukhi;

	// Token: 0x040009B8 RID: 2488
	public int pointAo;

	// Token: 0x040009B9 RID: 2489
	public int pointLien;

	// Token: 0x040009BA RID: 2490
	public int pointGangtay;

	// Token: 0x040009BB RID: 2491
	public int pointNhan;

	// Token: 0x040009BC RID: 2492
	public int pointQuan;

	// Token: 0x040009BD RID: 2493
	public int pointNgocboi;

	// Token: 0x040009BE RID: 2494
	public int pointGiay;

	// Token: 0x040009BF RID: 2495
	public int pointPhu;

	// Token: 0x040009C0 RID: 2496
	public int countFinishDay;

	// Token: 0x040009C1 RID: 2497
	public int countLoopBoos;

	// Token: 0x040009C2 RID: 2498
	public int limitTiemnangso;

	// Token: 0x040009C3 RID: 2499
	public int limitKynangso;

	// Token: 0x040009C4 RID: 2500
	public short[] potential = new short[4];

	// Token: 0x040009C5 RID: 2501
	public string cName = string.Empty;

	// Token: 0x040009C6 RID: 2502
	public int clanID;

	// Token: 0x040009C7 RID: 2503
	public sbyte ctypeClan;

	// Token: 0x040009C8 RID: 2504
	public Clan clan;

	// Token: 0x040009C9 RID: 2505
	public sbyte role;

	// Token: 0x040009CA RID: 2506
	public int cw = 22;

	// Token: 0x040009CB RID: 2507
	public int ch = 32;

	// Token: 0x040009CC RID: 2508
	public int chw = 11;

	// Token: 0x040009CD RID: 2509
	public int chh = 16;

	// Token: 0x040009CE RID: 2510
	public Command cmdMenu;

	// Token: 0x040009CF RID: 2511
	public bool canFly = true;

	// Token: 0x040009D0 RID: 2512
	public bool cmtoChar;

	// Token: 0x040009D1 RID: 2513
	public bool me;

	// Token: 0x040009D2 RID: 2514
	public bool cFinishedAttack;

	// Token: 0x040009D3 RID: 2515
	public bool cchistlast;

	// Token: 0x040009D4 RID: 2516
	public bool isAttack;

	// Token: 0x040009D5 RID: 2517
	public bool isAttFly;

	// Token: 0x040009D6 RID: 2518
	public int cwpt;

	// Token: 0x040009D7 RID: 2519
	public int cwplv;

	// Token: 0x040009D8 RID: 2520
	public int cf;

	// Token: 0x040009D9 RID: 2521
	public int tick;

	// Token: 0x040009DA RID: 2522
	public static bool fallAttack;

	// Token: 0x040009DB RID: 2523
	public bool isJump;

	// Token: 0x040009DC RID: 2524
	public bool autoFall;

	// Token: 0x040009DD RID: 2525
	public bool attack = true;

	// Token: 0x040009DE RID: 2526
	public int xu;

	// Token: 0x040009DF RID: 2527
	public int xuInBox;

	// Token: 0x040009E0 RID: 2528
	public int yen;

	// Token: 0x040009E1 RID: 2529
	public int gold_lock;

	// Token: 0x040009E2 RID: 2530
	public int luong;

	// Token: 0x040009E3 RID: 2531
	public NClass nClass;

	// Token: 0x040009E4 RID: 2532
	public Command endMovePointCommand;

	// Token: 0x040009E5 RID: 2533
	public MyVector vSkill = new MyVector();

	// Token: 0x040009E6 RID: 2534
	public MyVector vSkillFight = new MyVector();

	// Token: 0x040009E7 RID: 2535
	public MyVector vEff = new MyVector();

	// Token: 0x040009E8 RID: 2536
	public Skill myskill;

	// Token: 0x040009E9 RID: 2537
	public Task taskMaint;

	// Token: 0x040009EA RID: 2538
	public bool paintName = true;

	// Token: 0x040009EB RID: 2539
	public Archivement[] arrArchive;

	// Token: 0x040009EC RID: 2540
	public Item[] arrItemBag;

	// Token: 0x040009ED RID: 2541
	public Item[] arrItemBox;

	// Token: 0x040009EE RID: 2542
	public Item[] arrItemBody;

	// Token: 0x040009EF RID: 2543
	public Skill[] arrPetSkill;

	// Token: 0x040009F0 RID: 2544
	public Item[][] arrItemShop;

	// Token: 0x040009F1 RID: 2545
	public string[][] infoSpeacialSkill;

	// Token: 0x040009F2 RID: 2546
	public short[][] imgSpeacialSkill;

	// Token: 0x040009F3 RID: 2547
	public short cResFire;

	// Token: 0x040009F4 RID: 2548
	public short cResIce;

	// Token: 0x040009F5 RID: 2549
	public short cResWind;

	// Token: 0x040009F6 RID: 2550
	public short cMiss;

	// Token: 0x040009F7 RID: 2551
	public short cExactly;

	// Token: 0x040009F8 RID: 2552
	public short cFatal;

	// Token: 0x040009F9 RID: 2553
	public sbyte cPk;

	// Token: 0x040009FA RID: 2554
	public sbyte cTypePk;

	// Token: 0x040009FB RID: 2555
	public short cReactDame;

	// Token: 0x040009FC RID: 2556
	public short sysUp;

	// Token: 0x040009FD RID: 2557
	public short sysDown;

	// Token: 0x040009FE RID: 2558
	public int avatar;

	// Token: 0x040009FF RID: 2559
	public int skillTemplateId;

	// Token: 0x04000A00 RID: 2560
	public Mob mobFocus;

	// Token: 0x04000A01 RID: 2561
	public Mob mobMe;

	// Token: 0x04000A02 RID: 2562
	public int tMobMeBorn;

	// Token: 0x04000A03 RID: 2563
	public Npc npcFocus;

	// Token: 0x04000A04 RID: 2564
	public global::Char charFocus;

	// Token: 0x04000A05 RID: 2565
	public ItemMap itemFocus;

	// Token: 0x04000A06 RID: 2566
	public MyVector focus = new MyVector();

	// Token: 0x04000A07 RID: 2567
	public Mob[] attMobs;

	// Token: 0x04000A08 RID: 2568
	public global::Char[] attChars;

	// Token: 0x04000A09 RID: 2569
	public short[] moveFast;

	// Token: 0x04000A0A RID: 2570
	public int testCharId = -9999;

	// Token: 0x04000A0B RID: 2571
	public int killCharId = -9999;

	// Token: 0x04000A0C RID: 2572
	public sbyte resultTest;

	// Token: 0x04000A0D RID: 2573
	public int countKill;

	// Token: 0x04000A0E RID: 2574
	public int countKillMax;

	// Token: 0x04000A0F RID: 2575
	public bool isInvisiblez;

	// Token: 0x04000A10 RID: 2576
	public bool isShadown = true;

	// Token: 0x04000A11 RID: 2577
	public MyVector taskOrders = new MyVector();

	// Token: 0x04000A12 RID: 2578
	public int cStamina;

	// Token: 0x04000A13 RID: 2579
	public static short[] idHead;

	// Token: 0x04000A14 RID: 2580
	public static short[] idAvatar;

	// Token: 0x04000A15 RID: 2581
	public int exp;

	// Token: 0x04000A16 RID: 2582
	public string[] strLevel;

	// Token: 0x04000A17 RID: 2583
	public string currStrLevel;

	// Token: 0x04000A18 RID: 2584
	public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

	// Token: 0x04000A19 RID: 2585
	public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

	// Token: 0x04000A1A RID: 2586
	public bool isFreez;

	// Token: 0x04000A1B RID: 2587
	public bool isCharge;

	// Token: 0x04000A1C RID: 2588
	public int seconds;

	// Token: 0x04000A1D RID: 2589
	public int freezSeconds;

	// Token: 0x04000A1E RID: 2590
	public long last;

	// Token: 0x04000A1F RID: 2591
	public long cur;

	// Token: 0x04000A20 RID: 2592
	public long lastFreez;

	// Token: 0x04000A21 RID: 2593
	public long currFreez;

	// Token: 0x04000A22 RID: 2594
	public bool isFlyUp;

	// Token: 0x04000A23 RID: 2595
	public static MyVector vItemTime = new MyVector();

	// Token: 0x04000A24 RID: 2596
	public bool isHaveMount;

	// Token: 0x04000A25 RID: 2597
	public bool isMountVip;

	// Token: 0x04000A26 RID: 2598
	public bool isEventMount;

	// Token: 0x04000A27 RID: 2599
	public bool isSpeacialMount;

	// Token: 0x04000A28 RID: 2600
	public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

	// Token: 0x04000A29 RID: 2601
	public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

	// Token: 0x04000A2A RID: 2602
	public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

	// Token: 0x04000A2B RID: 2603
	public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

	// Token: 0x04000A2C RID: 2604
	public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

	// Token: 0x04000A2D RID: 2605
	public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

	// Token: 0x04000A2E RID: 2606
	public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

	// Token: 0x04000A2F RID: 2607
	public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

	// Token: 0x04000A30 RID: 2608
	public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

	// Token: 0x04000A31 RID: 2609
	public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

	// Token: 0x04000A32 RID: 2610
	public sbyte[] FrameMount = new sbyte[]
	{
		0,
		0,
		1,
		1,
		2,
		2,
		1,
		1
	};

	// Token: 0x04000A33 RID: 2611
	public int frameMount;

	// Token: 0x04000A34 RID: 2612
	public int transMount;

	// Token: 0x04000A35 RID: 2613
	public int genderMount;

	// Token: 0x04000A36 RID: 2614
	public int idcharMount;

	// Token: 0x04000A37 RID: 2615
	public int xMount;

	// Token: 0x04000A38 RID: 2616
	public int yMount;

	// Token: 0x04000A39 RID: 2617
	public int dxMount;

	// Token: 0x04000A3A RID: 2618
	public int dyMount;

	// Token: 0x04000A3B RID: 2619
	public int xChar;

	// Token: 0x04000A3C RID: 2620
	public int xdis;

	// Token: 0x04000A3D RID: 2621
	public int speedMount;

	// Token: 0x04000A3E RID: 2622
	public bool isStartMount;

	// Token: 0x04000A3F RID: 2623
	public bool isMount;

	// Token: 0x04000A40 RID: 2624
	public bool isEndMount;

	// Token: 0x04000A41 RID: 2625
	public sbyte cFlag;

	// Token: 0x04000A42 RID: 2626
	public int flagImage;

	// Token: 0x04000A43 RID: 2627
	public static int[][][] CharInfo = new int[][][]
	{
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				16
			},
			new int[]
			{
				1,
				-9,
				45
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				35
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				17
			},
			new int[]
			{
				1,
				-9,
				46
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				2,
				-10,
				11
			},
			new int[]
			{
				2,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				32
			},
			new int[]
			{
				3,
				-12,
				10
			},
			new int[]
			{
				3,
				-11,
				15
			},
			new int[]
			{
				1,
				-13,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				4,
				-8,
				11
			},
			new int[]
			{
				4,
				-7,
				17
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				5,
				-12,
				11
			},
			new int[]
			{
				5,
				-9,
				17
			},
			new int[]
			{
				1,
				-13,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				6,
				-10,
				10
			},
			new int[]
			{
				6,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				7,
				-11,
				25
			},
			new int[]
			{
				1,
				-8,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-7,
				35
			},
			new int[]
			{
				0,
				-18,
				22
			},
			new int[]
			{
				7,
				-10,
				25
			},
			new int[]
			{
				1,
				-7,
				48
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				35
			},
			new int[]
			{
				10,
				-3,
				25
			},
			new int[]
			{
				12,
				-10,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				37
			},
			new int[]
			{
				11,
				-3,
				25
			},
			new int[]
			{
				12,
				-11,
				27
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				12,
				-8,
				21
			},
			new int[]
			{
				9,
				-7,
				31
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-12,
				35
			},
			new int[]
			{
				8,
				-5,
				14
			},
			new int[]
			{
				8,
				-15,
				29
			},
			new int[]
			{
				1,
				-9,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-10,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				7,
				-13,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				8,
				-15,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				9,
				-8,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				8,
				-16,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-8,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				0,
				-5,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				0,
				-6,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				2,
				-9,
				36
			},
			new int[]
			{
				13,
				-5,
				17
			},
			new int[]
			{
				16,
				-11,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				9,
				-8,
				29
			},
			new int[3]
		}
	};

	// Token: 0x04000A44 RID: 2628
	public static int[] CHAR_WEAPONX = new int[]
	{
		-2,
		-6,
		22,
		21,
		19,
		22,
		10,
		-2,
		-2,
		5,
		19
	};

	// Token: 0x04000A45 RID: 2629
	public static int[] CHAR_WEAPONY = new int[]
	{
		9,
		22,
		25,
		17,
		26,
		37,
		36,
		49,
		50,
		52,
		36
	};

	// Token: 0x04000A46 RID: 2630
	private static global::Char myChar;

	// Token: 0x04000A47 RID: 2631
	private static global::Char myPet;

	// Token: 0x04000A48 RID: 2632
	public static int[] listAttack;

	// Token: 0x04000A49 RID: 2633
	public static int[][] listIonC;

	// Token: 0x04000A4A RID: 2634
	public int cvyJump;

	// Token: 0x04000A4B RID: 2635
	private int indexUseSkill = -1;

	// Token: 0x04000A4C RID: 2636
	public int cxSend;

	// Token: 0x04000A4D RID: 2637
	public int cySend;

	// Token: 0x04000A4E RID: 2638
	public int cdirSend = 1;

	// Token: 0x04000A4F RID: 2639
	public int cxFocus;

	// Token: 0x04000A50 RID: 2640
	public int cyFocus;

	// Token: 0x04000A51 RID: 2641
	public int cactFirst = 5;

	// Token: 0x04000A52 RID: 2642
	public MyVector vMovePoints = new MyVector();

	// Token: 0x04000A53 RID: 2643
	public static string[][] inforClass = new string[][]
	{
		new string[]
		{
			"1",
			"1",
			"chiêu 1",
			"0"
		},
		new string[]
		{
			"2",
			"2",
			"chiêu 2",
			"5"
		}
	};

	// Token: 0x04000A54 RID: 2644
	public static int[][] inforSkill = new int[][]
	{
		new int[]
		{
			1,
			0,
			1,
			1000,
			40,
			1,
			0,
			20,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			1,
			10,
			1000,
			100,
			1,
			0,
			40,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			2,
			11,
			800,
			100,
			1,
			0,
			45,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			3,
			12,
			600,
			100,
			1,
			0,
			50,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			4,
			13,
			500,
			100,
			1,
			0,
			55,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			1,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			2,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			3,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			4,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			5,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		}
	};

	// Token: 0x04000A55 RID: 2645
	public static bool flag;

	// Token: 0x04000A56 RID: 2646
	public static bool ischangingMap;

	// Token: 0x04000A57 RID: 2647
	public static bool isLockKey;

	// Token: 0x04000A58 RID: 2648
	public static bool isLoadingMap;

	// Token: 0x04000A59 RID: 2649
	public bool isLockMove;

	// Token: 0x04000A5A RID: 2650
	public bool isLockAttack;

	// Token: 0x04000A5B RID: 2651
	public string strInfo;

	// Token: 0x04000A5C RID: 2652
	public short powerPoint;

	// Token: 0x04000A5D RID: 2653
	public short maxPowerPoint;

	// Token: 0x04000A5E RID: 2654
	public short secondPower;

	// Token: 0x04000A5F RID: 2655
	public long lastS;

	// Token: 0x04000A60 RID: 2656
	public long currS;

	// Token: 0x04000A61 RID: 2657
	public bool havePet = true;

	// Token: 0x04000A62 RID: 2658
	public MovePoint currentMovePoint;

	// Token: 0x04000A63 RID: 2659
	public int bom;

	// Token: 0x04000A64 RID: 2660
	public int delayFall;

	// Token: 0x04000A65 RID: 2661
	private bool isSoundJump;

	// Token: 0x04000A66 RID: 2662
	public int lastFrame;

	// Token: 0x04000A67 RID: 2663
	private Effect eProtect;

	// Token: 0x04000A68 RID: 2664
	private int twHp;

	// Token: 0x04000A69 RID: 2665
	public bool isInjureHp;

	// Token: 0x04000A6A RID: 2666
	public bool changePos;

	// Token: 0x04000A6B RID: 2667
	private bool isHide;

	// Token: 0x04000A6C RID: 2668
	private bool wy;

	// Token: 0x04000A6D RID: 2669
	public int wt;

	// Token: 0x04000A6E RID: 2670
	public int fy;

	// Token: 0x04000A6F RID: 2671
	public int ty;

	// Token: 0x04000A70 RID: 2672
	private int t;

	// Token: 0x04000A71 RID: 2673
	private int fM;

	// Token: 0x04000A72 RID: 2674
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

	// Token: 0x04000A73 RID: 2675
	public int head;

	// Token: 0x04000A74 RID: 2676
	public int leg;

	// Token: 0x04000A75 RID: 2677
	public int body;

	// Token: 0x04000A76 RID: 2678
	public int wp;

	// Token: 0x04000A77 RID: 2679
	public int bag;

	// Token: 0x04000A78 RID: 2680
	public int indexEff = -1;

	// Token: 0x04000A79 RID: 2681
	public int indexEffTask = -1;

	// Token: 0x04000A7A RID: 2682
	public EffectCharPaint eff;

	// Token: 0x04000A7B RID: 2683
	public EffectCharPaint effTask;

	// Token: 0x04000A7C RID: 2684
	public int indexSkill;

	// Token: 0x04000A7D RID: 2685
	public int i0;

	// Token: 0x04000A7E RID: 2686
	public int i1;

	// Token: 0x04000A7F RID: 2687
	public int i2;

	// Token: 0x04000A80 RID: 2688
	public int dx0;

	// Token: 0x04000A81 RID: 2689
	public int dx1;

	// Token: 0x04000A82 RID: 2690
	public int dx2;

	// Token: 0x04000A83 RID: 2691
	public int dy0;

	// Token: 0x04000A84 RID: 2692
	public int dy1;

	// Token: 0x04000A85 RID: 2693
	public int dy2;

	// Token: 0x04000A86 RID: 2694
	public EffectCharPaint eff0;

	// Token: 0x04000A87 RID: 2695
	public EffectCharPaint eff1;

	// Token: 0x04000A88 RID: 2696
	public EffectCharPaint eff2;

	// Token: 0x04000A89 RID: 2697
	public Arrow arr;

	// Token: 0x04000A8A RID: 2698
	public PlayerDart dart;

	// Token: 0x04000A8B RID: 2699
	public bool isCreateDark;

	// Token: 0x04000A8C RID: 2700
	public SkillPaint skillPaint;

	// Token: 0x04000A8D RID: 2701
	public SkillPaint skillPaintRandomPaint;

	// Token: 0x04000A8E RID: 2702
	public EffectPaint[] effPaints;

	// Token: 0x04000A8F RID: 2703
	public int sType;

	// Token: 0x04000A90 RID: 2704
	public sbyte isInjure;

	// Token: 0x04000A91 RID: 2705
	public bool isUseSkillAfterCharge;

	// Token: 0x04000A92 RID: 2706
	public bool isFlyAndCharge;

	// Token: 0x04000A93 RID: 2707
	public bool isStandAndCharge;

	// Token: 0x04000A94 RID: 2708
	private bool isFlying;

	// Token: 0x04000A95 RID: 2709
	public int posDisY;

	// Token: 0x04000A96 RID: 2710
	private int chargeCount;

	// Token: 0x04000A97 RID: 2711
	private bool hasSendAttack;

	// Token: 0x04000A98 RID: 2712
	public bool isMabuHold;

	// Token: 0x04000A99 RID: 2713
	private int tBlue;

	// Token: 0x04000A9A RID: 2714
	private bool isPet;

	// Token: 0x04000A9B RID: 2715
	private bool isMiniPet;

	// Token: 0x04000A9C RID: 2716
	public int xSd;

	// Token: 0x04000A9D RID: 2717
	public int ySd;

	// Token: 0x04000A9E RID: 2718
	private bool isOutMap;

	// Token: 0x04000A9F RID: 2719
	private int statusBeforeNothing;

	// Token: 0x04000AA0 RID: 2720
	private int timeFocusToMob;

	// Token: 0x04000AA1 RID: 2721
	public static bool isManualFocus = false;

	// Token: 0x04000AA2 RID: 2722
	private global::Char charHold;

	// Token: 0x04000AA3 RID: 2723
	private Mob mobHold;

	// Token: 0x04000AA4 RID: 2724
	private int nInjure;

	// Token: 0x04000AA5 RID: 2725
	public short wdx;

	// Token: 0x04000AA6 RID: 2726
	public short wdy;

	// Token: 0x04000AA7 RID: 2727
	public bool isDirtyPostion;

	// Token: 0x04000AA8 RID: 2728
	public Skill lastNormalSkill;

	// Token: 0x04000AA9 RID: 2729
	public bool currentFireByShortcut;

	// Token: 0x04000AAA RID: 2730
	public int cDamGoc;

	// Token: 0x04000AAB RID: 2731
	public int cHPGoc;

	// Token: 0x04000AAC RID: 2732
	public int cMPGoc;

	// Token: 0x04000AAD RID: 2733
	public int cDefGoc;

	// Token: 0x04000AAE RID: 2734
	public int cCriticalGoc;

	// Token: 0x04000AAF RID: 2735
	public sbyte hpFrom1000TiemNang;

	// Token: 0x04000AB0 RID: 2736
	public sbyte mpFrom1000TiemNang;

	// Token: 0x04000AB1 RID: 2737
	public sbyte damFrom1000TiemNang;

	// Token: 0x04000AB2 RID: 2738
	public sbyte defFrom1000TiemNang = 1;

	// Token: 0x04000AB3 RID: 2739
	public sbyte criticalFrom1000Tiemnang = 1;

	// Token: 0x04000AB4 RID: 2740
	public short cMaxStamina;

	// Token: 0x04000AB5 RID: 2741
	public short expForOneAdd;

	// Token: 0x04000AB6 RID: 2742
	public sbyte isMonkey;

	// Token: 0x04000AB7 RID: 2743
	public bool isCopy;

	// Token: 0x04000AB8 RID: 2744
	public bool isWaitMonkey;

	// Token: 0x04000AB9 RID: 2745
	private bool isFeetEff;

	// Token: 0x04000ABA RID: 2746
	public bool meDead;

	// Token: 0x04000ABB RID: 2747
	public int holdEffID;

	// Token: 0x04000ABC RID: 2748
	public bool holder;

	// Token: 0x04000ABD RID: 2749
	public bool protectEff;

	// Token: 0x04000ABE RID: 2750
	private bool isSetPos;

	// Token: 0x04000ABF RID: 2751
	private int tpos;

	// Token: 0x04000AC0 RID: 2752
	private short xPos;

	// Token: 0x04000AC1 RID: 2753
	private short yPos;

	// Token: 0x04000AC2 RID: 2754
	private sbyte typePos;

	// Token: 0x04000AC3 RID: 2755
	private bool isMyFusion;

	// Token: 0x04000AC4 RID: 2756
	public bool isFusion;

	// Token: 0x04000AC5 RID: 2757
	public int tFusion;

	// Token: 0x04000AC6 RID: 2758
	public bool huytSao;

	// Token: 0x04000AC7 RID: 2759
	public bool blindEff;

	// Token: 0x04000AC8 RID: 2760
	public bool telePortSkill;

	// Token: 0x04000AC9 RID: 2761
	public bool sleepEff;

	// Token: 0x04000ACA RID: 2762
	public bool stone;

	// Token: 0x04000ACB RID: 2763
	public int perCentMp = 100;

	// Token: 0x04000ACC RID: 2764
	public int dHP;
}
