using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class Controller : IMessageHandler
{
	// Token: 0x06000436 RID: 1078 RVA: 0x00005A15 File Offset: 0x00003C15
	public static Controller gI()
	{
		if (Controller.me == null)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00005A30 File Offset: 0x00003C30
	public static Controller gI2()
	{
		if (Controller.me2 == null)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00005A4B File Offset: 0x00003C4B
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00005A58 File Offset: 0x00003C58
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00005A65 File Offset: 0x00003C65
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00024F78 File Offset: 0x00023178
	public void requestItemPlayer(Message msg)
	{
		try
		{
			int num = (int)msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = (int)msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				for (;;)
				{
					item.options.addElement(new ItemOption((int)msg.reader().readByte(), (int)msg.reader().readShort()));
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00025054 File Offset: 0x00023254
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			Controller2.readMessage(msg);
			sbyte command = msg.command;
			switch (command + 107)
			{
			case 0:
			{
				sbyte b = msg.reader().readByte();
				if ((int)b == 0)
				{
					global::Char.myCharz().havePet = false;
				}
				if ((int)b == 1)
				{
					global::Char.myCharz().havePet = true;
				}
				if ((int)b == 2)
				{
					InfoDlg.hide();
					global::Char.myPetz().head = (int)msg.reader().readShort();
					global::Char.myPetz().setDefaultPart();
					int num = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num);
					global::Char.myPetz().arrItemBody = new Item[num];
					for (int j = 0; j < num; j++)
					{
						short num2 = msg.reader().readShort();
						Res.outz("template id= " + num2);
						if (num2 != -1)
						{
							Res.outz("1");
							global::Char.myPetz().arrItemBody[j] = new Item();
							global::Char.myPetz().arrItemBody[j].template = ItemTemplates.get(num2);
							int num3 = (int)global::Char.myPetz().arrItemBody[j].template.type;
							global::Char.myPetz().arrItemBody[j].quantity = msg.reader().readInt();
							Res.outz("3");
							global::Char.myPetz().arrItemBody[j].info = msg.reader().readUTF();
							global::Char.myPetz().arrItemBody[j].content = msg.reader().readUTF();
							int num4 = (int)msg.reader().readUnsignedByte();
							Res.outz("option size= " + num4);
							if (num4 != 0)
							{
								global::Char.myPetz().arrItemBody[j].itemOption = new ItemOption[num4];
								for (int k = 0; k < global::Char.myPetz().arrItemBody[j].itemOption.Length; k++)
								{
									int num5 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									if (num5 != -1)
									{
										global::Char.myPetz().arrItemBody[j].itemOption[k] = new ItemOption(num5, param);
									}
								}
							}
							if (num3 == 0)
							{
								global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[j].template.part;
							}
							else if (num3 == 1)
							{
								global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[j].template.part;
							}
						}
					}
					global::Char.myPetz().cHP = msg.readInt3Byte();
					global::Char.myPetz().cHPFull = msg.readInt3Byte();
					global::Char.myPetz().cMP = msg.readInt3Byte();
					global::Char.myPetz().cMPFull = msg.readInt3Byte();
					global::Char.myPetz().cDamFull = msg.readInt3Byte();
					global::Char.myPetz().cName = msg.reader().readUTF();
					global::Char.myPetz().currStrLevel = msg.reader().readUTF();
					global::Char.myPetz().cPower = msg.reader().readLong();
					global::Char.myPetz().cTiemNang = msg.reader().readLong();
					global::Char.myPetz().petStatus = msg.reader().readByte();
					global::Char.myPetz().cStamina = (int)msg.reader().readShort();
					global::Char.myPetz().cMaxStamina = msg.reader().readShort();
					global::Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
					global::Char.myPetz().cDefull = (int)msg.reader().readShort();
					global::Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
					Res.outz("SKILLENT = " + global::Char.myPetz().arrPetSkill);
					for (int l = 0; l < global::Char.myPetz().arrPetSkill.Length; l++)
					{
						short num6 = msg.reader().readShort();
						if (num6 != -1)
						{
							global::Char.myPetz().arrPetSkill[l] = Skills.get(num6);
						}
						else
						{
							global::Char.myPetz().arrPetSkill[l] = new Skill();
							global::Char.myPetz().arrPetSkill[l].template = null;
							global::Char.myPetz().arrPetSkill[l].moreInfo = msg.reader().readUTF();
						}
					}
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
						GameCanvas.panel.setTypePetMain();
						GameCanvas.panel.show();
					}
					else
					{
						GameCanvas.panel.tabName[21] = mResources.petMainTab;
						GameCanvas.panel.setTypePetMain();
						GameCanvas.panel.show();
					}
				}
				break;
			}
			default:
				if (command == -112)
				{
					sbyte b2 = msg.reader().readByte();
					if ((int)b2 == 0)
					{
						sbyte mobIndex = msg.reader().readByte();
						GameScr.findMobInMap(mobIndex).clearBody();
					}
					if ((int)b2 == 1)
					{
						sbyte mobIndex2 = msg.reader().readByte();
						GameScr.findMobInMap(mobIndex2).setBody(msg.reader().readShort());
					}
				}
				break;
			case 8:
			{
				InfoDlg.hide();
				sbyte b3 = msg.reader().readByte();
				if ((int)b3 == 0)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num7 = (int)msg.reader().readUnsignedByte();
					for (int m = 0; m < num7; m++)
					{
						global::Char char2 = new global::Char();
						char2.charID = msg.reader().readInt();
						char2.head = (int)msg.reader().readShort();
						char2.body = (int)msg.reader().readShort();
						char2.leg = (int)msg.reader().readShort();
						char2.bag = (int)msg.reader().readShort();
						char2.cName = msg.reader().readUTF();
						InfoItem infoItem = new InfoItem(msg.reader().readUTF());
						bool flag = msg.reader().readBoolean();
						infoItem.charInfo = char2;
						infoItem.isOnline = flag;
						Res.outz("isonline = " + flag);
						GameCanvas.panel.vEnemy.addElement(infoItem);
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
				}
				break;
			}
			case 9:
			{
				sbyte b4 = msg.reader().readByte();
				GameCanvas.menu.showMenu = false;
				if ((int)b4 == 0)
				{
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
				}
				break;
			}
			case 10:
				global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
				break;
			case 11:
			{
				sbyte typeTop = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string topName = msg.reader().readUTF();
				sbyte b5 = msg.reader().readByte();
				for (int n = 0; n < (int)b5; n++)
				{
					int rank = msg.reader().readInt();
					int pId = msg.reader().readInt();
					short headID = msg.reader().readShort();
					short body = msg.reader().readShort();
					short leg = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = rank;
					topInfo.headID = (int)headID;
					topInfo.body = body;
					topInfo.leg = leg;
					topInfo.name = name;
					topInfo.info = info;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = pId;
					GameCanvas.panel.vTop.addElement(topInfo);
				}
				GameCanvas.panel.topName = topName;
				GameCanvas.panel.setTypeTop(typeTop);
				GameCanvas.panel.show();
				break;
			}
			case 12:
			{
				sbyte b6 = msg.reader().readByte();
				Res.outz("type= " + b6);
				if ((int)b6 == 0)
				{
					int num8 = msg.reader().readInt();
					short templateId = msg.reader().readShort();
					int num9 = msg.readInt3Byte();
					SoundMn.gI().explode_1();
					if (num8 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = new Mob(num8, false, false, false, false, false, (int)templateId, 1, num9, 0, num9, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? -40 : 40)), (short)global::Char.myCharz().cy, 4, 0);
						global::Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
						global::Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(global::Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num8);
						if (@char != null)
						{
							@char.mobMe = new Mob(num8, false, false, false, false, false, (int)templateId, 1, num9, 0, num9, (short)@char.cx, (short)@char.cy, 4, 0)
							{
								isMobMe = true
							};
							GameScr.vMob.addElement(@char.mobMe);
						}
						else if (GameScr.findMobInMap(num8) == null)
						{
							Mob mob = new Mob(num8, false, false, false, false, false, (int)templateId, 1, num9, 0, num9, -100, -100, 4, 0);
							mob.isMobMe = true;
							GameScr.vMob.addElement(mob);
						}
					}
				}
				if ((int)b6 == 1)
				{
					int num10 = msg.reader().readInt();
					int mobId = (int)msg.reader().readByte();
					Res.outz("mod attack id= " + num10);
					if (num10 == global::Char.myCharz().charID)
					{
						if (GameScr.findMobInMap(mobId) != null)
						{
							global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num10);
						if (@char != null && GameScr.findMobInMap(mobId) != null)
						{
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
				}
				if ((int)b6 == 2)
				{
					int num11 = msg.reader().readInt();
					int num12 = msg.reader().readInt();
					int num13 = msg.readInt3Byte();
					int cHPNew = msg.readInt3Byte();
					if (num11 == global::Char.myCharz().charID)
					{
						Res.outz("mob dame= " + num13);
						@char = GameScr.findCharInMap(num12);
						if (@char != null)
						{
							@char.cHPNew = cHPNew;
							if (global::Char.myCharz().mobMe.isBusyAttackSomeOne)
							{
								@char.doInjure(num13, 0, false, true);
							}
							else
							{
								global::Char.myCharz().mobMe.dame = num13;
								global::Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						Mob mob2 = GameScr.findMobInMap(num11);
						if (mob2 != null)
						{
							if (num12 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().cHPNew = cHPNew;
								if (mob2.isBusyAttackSomeOne)
								{
									global::Char.myCharz().doInjure(num13, 0, false, true);
								}
								else
								{
									mob2.dame = num13;
									mob2.setAttack(global::Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num12);
								if (@char != null)
								{
									@char.cHPNew = cHPNew;
									if (mob2.isBusyAttackSomeOne)
									{
										@char.doInjure(num13, 0, false, true);
									}
									else
									{
										mob2.dame = num13;
										mob2.setAttack(@char);
									}
								}
							}
						}
					}
				}
				if ((int)b6 == 3)
				{
					int num14 = msg.reader().readInt();
					int mobId2 = msg.reader().readInt();
					int hp = msg.readInt3Byte();
					int num15 = msg.readInt3Byte();
					@char = null;
					if (global::Char.myCharz().charID == num14)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num14);
					}
					if (@char != null)
					{
						Mob mob2 = GameScr.findMobInMap(mobId2);
						if (@char.mobMe != null)
						{
							@char.mobMe.attackOtherMob(mob2);
						}
						if (mob2 != null)
						{
							mob2.hp = hp;
							if (num15 == 0)
							{
								mob2.x = mob2.xFirst;
								mob2.y = mob2.yFirst;
								GameScr.startFlyText(mResources.miss, mob2.x, mob2.y - mob2.h, 0, -2, mFont.MISS);
							}
							else
							{
								GameScr.startFlyText("-" + num15, mob2.x, mob2.y - mob2.h, 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				if ((int)b6 == 4)
				{
				}
				if ((int)b6 == 5)
				{
					int num16 = msg.reader().readInt();
					sbyte b7 = msg.reader().readByte();
					int mobId3 = msg.reader().readInt();
					int num17 = msg.readInt3Byte();
					int hp2 = msg.readInt3Byte();
					@char = null;
					if (num16 == global::Char.myCharz().charID)
					{
						@char = global::Char.myCharz();
					}
					else
					{
						@char = GameScr.findCharInMap(num16);
					}
					if (@char == null)
					{
						return;
					}
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[(int)b7], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)b7], 1);
					}
					Mob mob3 = GameScr.findMobInMap(mobId3);
					if (@char.cx <= mob3.x)
					{
						@char.cdir = 1;
					}
					else
					{
						@char.cdir = -1;
					}
					@char.mobFocus = mob3;
					mob3.hp = hp2;
					GameCanvas.debug("SA83v2", 2);
					if (num17 == 0)
					{
						mob3.x = mob3.xFirst;
						mob3.y = mob3.yFirst;
						GameScr.startFlyText(mResources.miss, mob3.x, mob3.y - mob3.h, 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num17, mob3.x, mob3.y - mob3.h, 0, -2, mFont.ORANGE);
					}
				}
				if ((int)b6 == 6)
				{
					int num18 = msg.reader().readInt();
					if (num18 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe.startDie();
					}
					else
					{
						@char = GameScr.findCharInMap(num18);
						if (@char != null)
						{
							@char.mobMe.startDie();
						}
					}
				}
				if ((int)b6 == 7)
				{
					int num19 = msg.reader().readInt();
					if (num19 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = null;
						for (int num20 = 0; num20 < GameScr.vMob.size(); num20++)
						{
							if (((Mob)GameScr.vMob.elementAt(num20)).mobId == num19)
							{
								GameScr.vMob.removeElementAt(num20);
							}
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num19);
						for (int num21 = 0; num21 < GameScr.vMob.size(); num21++)
						{
							if (((Mob)GameScr.vMob.elementAt(num21)).mobId == num19)
							{
								GameScr.vMob.removeElementAt(num21);
							}
						}
						if (@char != null)
						{
							@char.mobMe = null;
						}
					}
				}
				break;
			}
			case 13:
				while (msg.reader().available() > 0)
				{
					short num22 = msg.reader().readShort();
					int num23 = msg.reader().readInt();
					for (int num24 = 0; num24 < global::Char.myCharz().vSkill.size(); num24++)
					{
						Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(num24);
						if (skill != null && skill.skillId == num22)
						{
							if (num23 < skill.coolDown)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num23);
							}
							Res.outz(string.Concat(new object[]
							{
								"1 chieu id= ",
								skill.template.id,
								" cooldown= ",
								num23,
								"curr cool down= ",
								skill.coolDown
							}));
						}
					}
				}
				break;
			case 14:
			{
				short num25 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[(int)num25];
				for (int num26 = 0; num26 < (int)num25; num26++)
				{
					BgItem.newSmallVersion[num26] = msg.reader().readByte();
				}
				break;
			}
			case 15:
				Main.typeClient = (int)msg.reader().readByte();
				Rms.clearAll();
				Rms.saveRMSString("clienttype", Main.typeClient + string.Empty);
				GameCanvas.startOK("Dữ liệu thay đổi, vui lòng khởi động lại game.", 8885, null);
				break;
			case 16:
			{
				sbyte b8 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[(int)b8];
				GameCanvas.panel.planetNames = new string[(int)b8];
				for (int num27 = 0; num27 < (int)b8; num27++)
				{
					GameCanvas.panel.mapNames[num27] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num27] = msg.reader().readUTF();
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				break;
			}
			case 17:
			{
				sbyte b9 = msg.reader().readByte();
				Res.outz("type = " + b9);
				int num28 = msg.reader().readInt();
				if ((int)b9 != -1)
				{
					short num29 = msg.reader().readShort();
					short num30 = msg.reader().readShort();
					short num31 = msg.reader().readShort();
					sbyte b10 = msg.reader().readByte();
					Res.outz("is Monkey = " + b10);
					if (global::Char.myCharz().charID == num28)
					{
						global::Char.myCharz().isMask = true;
						global::Char.myCharz().isMonkey = b10;
						if ((int)global::Char.myCharz().isMonkey != 0)
						{
							global::Char.myCharz().isWaitMonkey = false;
							global::Char.myCharz().isLockMove = false;
						}
					}
					else if (GameScr.findCharInMap(num28) != null)
					{
						GameScr.findCharInMap(num28).isMask = true;
						GameScr.findCharInMap(num28).isMonkey = b10;
					}
					if (num29 != -1)
					{
						if (num28 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().head = (int)num29;
						}
						else if (GameScr.findCharInMap(num28) != null)
						{
							GameScr.findCharInMap(num28).head = (int)num29;
						}
					}
					if (num30 != -1)
					{
						if (num28 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().body = (int)num30;
						}
						else if (GameScr.findCharInMap(num28) != null)
						{
							GameScr.findCharInMap(num28).body = (int)num30;
						}
					}
					if (num31 != -1)
					{
						if (num28 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().leg = (int)num31;
						}
						else if (GameScr.findCharInMap(num28) != null)
						{
							GameScr.findCharInMap(num28).leg = (int)num31;
						}
					}
				}
				if ((int)b9 == -1)
				{
					if (global::Char.myCharz().charID == num28)
					{
						global::Char.myCharz().isMask = false;
						global::Char.myCharz().isMonkey = 0;
					}
					else if (GameScr.findCharInMap(num28) != null)
					{
						GameScr.findCharInMap(num28).isMask = false;
						GameScr.findCharInMap(num28).isMonkey = 0;
					}
				}
				break;
			}
			case 19:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				break;
			case 20:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				sbyte[] data = new sbyte[]
				{
					GameScr.vcData
				};
				Rms.saveRMS("NRdataVersion", data);
				LoginScr.isUpdateData = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					Res.outz(string.Concat(new object[]
					{
						GameScr.vsData,
						",",
						GameScr.vsMap,
						",",
						GameScr.vsSkill,
						",",
						GameScr.vsItem
					}));
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
					return;
				}
				break;
			}
			case 21:
			{
				sbyte b11 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b11);
				if ((int)b11 == 0)
				{
					int playerID = msg.reader().readInt();
					GameScr.gI().giaodich(playerID);
				}
				if ((int)b11 == 1)
				{
					int num32 = msg.reader().readInt();
					global::Char char3 = GameScr.findCharInMap(num32);
					if (char3 == null)
					{
						return;
					}
					GameCanvas.panel.setTypeGiaoDich(char3);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num32);
				}
				if ((int)b11 == 2)
				{
					sbyte b12 = msg.reader().readByte();
					for (int num33 = 0; num33 < GameCanvas.panel.vMyGD.size(); num33++)
					{
						Item item = (Item)GameCanvas.panel.vMyGD.elementAt(num33);
						if (item.indexUI == (int)b12)
						{
							GameCanvas.panel.vMyGD.removeElement(item);
							break;
						}
					}
				}
				if ((int)b11 == 5)
				{
				}
				if ((int)b11 == 6)
				{
					GameCanvas.panel.isFriendLock = true;
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.isFriendLock = true;
					}
					GameCanvas.panel.vFriendGD.removeAllElements();
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.vFriendGD.removeAllElements();
					}
					int friendMoneyGD = msg.reader().readInt();
					sbyte b13 = msg.reader().readByte();
					Res.outz("item size = " + b13);
					for (int num34 = 0; num34 < (int)b13; num34++)
					{
						Item item2 = new Item();
						item2.template = ItemTemplates.get(msg.reader().readShort());
						item2.quantity = (int)msg.reader().readByte();
						int num35 = (int)msg.reader().readUnsignedByte();
						if (num35 != 0)
						{
							item2.itemOption = new ItemOption[num35];
							for (int num36 = 0; num36 < item2.itemOption.Length; num36++)
							{
								int num37 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								if (num37 != -1)
								{
									item2.itemOption[num36] = new ItemOption(num37, param2);
									item2.compare = GameCanvas.panel.getCompare(item2);
								}
							}
						}
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.vFriendGD.addElement(item2);
						}
						else
						{
							GameCanvas.panel.vFriendGD.addElement(item2);
						}
					}
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = friendMoneyGD;
						if (GameCanvas.panel.currentTabIndex == 2)
						{
							GameCanvas.panel.setTabGiaoDich(false);
						}
					}
				}
				if ((int)b11 == 7)
				{
					InfoDlg.hide();
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.hide();
					}
				}
				break;
			}
			case 22:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b14 = msg.reader().readByte();
				if ((int)b14 == 0)
				{
					int num38 = (int)msg.reader().readUnsignedShort();
					Res.outz("lent =" + num38);
					sbyte[] imageData = new sbyte[num38];
					msg.reader().read(ref imageData, 0, num38);
					GameScr.imgCapcha = Image.createImage(imageData, 0, num38);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				if ((int)b14 == 1)
				{
					MobCapcha.isAttack = true;
				}
				if ((int)b14 == 2)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
				}
				break;
			}
			case 23:
			{
				int index = (int)msg.reader().readUnsignedByte();
				Mob mob4 = null;
				try
				{
					mob4 = (Mob)GameScr.vMob.elementAt(index);
				}
				catch (Exception ex)
				{
				}
				if (mob4 != null)
				{
					mob4.maxHp = msg.reader().readInt();
				}
				break;
			}
			case 24:
			{
				sbyte b15 = msg.reader().readByte();
				if ((int)b15 == 0)
				{
					int num39 = (int)msg.reader().readShort();
					int bgRID = (int)msg.reader().readShort();
					int num40 = (int)msg.reader().readUnsignedByte();
					int num41 = msg.reader().readInt();
					string text = msg.reader().readUTF();
					int num42 = (int)msg.reader().readShort();
					int num43 = (int)msg.reader().readShort();
					sbyte b16 = msg.reader().readByte();
					if ((int)b16 == 1)
					{
						GameScr.gI().isRongNamek = true;
					}
					else
					{
						GameScr.gI().isRongNamek = false;
					}
					GameScr.gI().xR = num42;
					GameScr.gI().yR = num43;
					Res.outz(string.Concat(new object[]
					{
						"xR= ",
						num42,
						" yR= ",
						num43,
						" +++++++++++++++++++++++++++++++++++++++"
					}));
					if (global::Char.myCharz().charID == num41)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else if (TileMap.mapID == num39 && TileMap.zoneID == num40)
					{
						GameScr.gI().activeRongThanEff(false);
					}
					else if (mGraphics.zoomLevel > 1)
					{
						GameScr.gI().doiMauTroi();
					}
					GameScr.gI().mapRID = num39;
					GameScr.gI().bgRID = bgRID;
					GameScr.gI().zoneRID = num40;
				}
				if ((int)b15 == 1)
				{
					Res.outz(string.Concat(new object[]
					{
						"map RID = ",
						GameScr.gI().mapRID,
						" zone RID= ",
						GameScr.gI().zoneRID
					}));
					Res.outz(string.Concat(new object[]
					{
						"map ID = ",
						TileMap.mapID,
						" zone ID= ",
						TileMap.zoneID
					}));
					if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
					{
						GameScr.gI().hideRongThanEff();
					}
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						if (GameScr.gI().isRongNamek)
						{
							GameScr.gI().isRongNamek = false;
						}
					}
				}
				if ((int)b15 == 2)
				{
				}
				break;
			}
			case 25:
			{
				sbyte b17 = msg.reader().readByte();
				TileMap.tileIndex = new int[(int)b17][][];
				TileMap.tileType = new int[(int)b17][];
				for (int num44 = 0; num44 < (int)b17; num44++)
				{
					sbyte b18 = msg.reader().readByte();
					TileMap.tileType[num44] = new int[(int)b18];
					TileMap.tileIndex[num44] = new int[(int)b18][];
					for (int num45 = 0; num45 < (int)b18; num45++)
					{
						TileMap.tileType[num44][num45] = msg.reader().readInt();
						sbyte b19 = msg.reader().readByte();
						TileMap.tileIndex[num44][num45] = new int[(int)b19];
						for (int num46 = 0; num46 < (int)b19; num46++)
						{
							TileMap.tileIndex[num44][num45][num46] = (int)msg.reader().readByte();
						}
					}
				}
				break;
			}
			case 26:
			{
				sbyte b20 = msg.reader().readByte();
				if ((int)b20 == 0)
				{
					string src = msg.reader().readUTF();
					string src2 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				if ((int)b20 == 1)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b21 = msg.reader().readByte();
					for (int num47 = 0; num47 < (int)b21; num47++)
					{
						sbyte b22 = msg.reader().readByte();
						for (int num48 = 0; num48 < global::Char.myCharz().arrItemBag.Length; num48++)
						{
							Item item3 = global::Char.myCharz().arrItemBag[num48];
							if (item3 != null && item3.indexUI == (int)b22)
							{
								item3.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item3);
							}
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabCombine();
					}
				}
				if ((int)b20 > 1)
				{
					int num49 = 21;
					for (int num50 = 0; num50 < GameScr.vNpc.size(); num50++)
					{
						Npc npc = (Npc)GameScr.vNpc.elementAt(num50);
						if (npc.template.npcTemplateId == num49)
						{
							GameCanvas.panel.xS = npc.cx - GameScr.cmx;
							GameCanvas.panel.yS = npc.cy - GameScr.cmy;
							GameCanvas.panel.idNPC = num49;
							break;
						}
					}
				}
				if ((int)b20 == 2)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				if ((int)b20 == 3)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				if ((int)b20 == 4)
				{
					short iconID = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				if ((int)b20 == 5)
				{
					short iconID2 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID2;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				if ((int)b20 == 6)
				{
					short iconID3 = msg.reader().readShort();
					short iconID4 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = iconID3;
					GameCanvas.panel.iconID3 = iconID4;
				}
				break;
			}
			case 27:
			{
				sbyte b23 = msg.reader().readByte();
				InfoDlg.hide();
				if ((int)b23 == 0)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num51 = (int)msg.reader().readUnsignedByte();
					for (int num52 = 0; num52 < num51; num52++)
					{
						global::Char char4 = new global::Char();
						char4.charID = msg.reader().readInt();
						char4.head = (int)msg.reader().readShort();
						char4.body = (int)msg.reader().readShort();
						char4.leg = (int)msg.reader().readShort();
						char4.bag = (int)msg.reader().readUnsignedByte();
						char4.cName = msg.reader().readUTF();
						bool isOnline = msg.reader().readBoolean();
						InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem2.charInfo = char4;
						infoItem2.isOnline = isOnline;
						GameCanvas.panel.vFriend.addElement(infoItem2);
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				if ((int)b23 == 3)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num53 = msg.reader().readInt();
					Res.outz("online offline id=" + num53);
					for (int num54 = 0; num54 < vFriend.size(); num54++)
					{
						InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num54);
						if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num53)
						{
							Res.outz("online= " + infoItem3.isOnline);
							infoItem3.isOnline = msg.reader().readBoolean();
							break;
						}
					}
				}
				if ((int)b23 == 2)
				{
					MyVector vFriend2 = GameCanvas.panel.vFriend;
					int num55 = msg.reader().readInt();
					for (int num56 = 0; num56 < vFriend2.size(); num56++)
					{
						InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num56);
						if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num55)
						{
							vFriend2.removeElement(infoItem4);
							break;
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabFriend();
					}
				}
				break;
			}
			case 28:
			{
				InfoDlg.hide();
				int num57 = msg.reader().readInt();
				global::Char charMenu = GameCanvas.panel.charMenu;
				if (charMenu == null)
				{
					return;
				}
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				break;
			}
			case 30:
			{
				short num58 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[(int)num58];
				SmallImage.maxSmall = num58;
				SmallImage.imgNew = new Small[(int)num58];
				for (int num59 = 0; num59 < (int)num58; num59++)
				{
					SmallImage.newSmallVersion[num59] = msg.reader().readByte();
				}
				break;
			}
			case 31:
			{
				sbyte b24 = msg.reader().readByte();
				if ((int)b24 == 0)
				{
					sbyte b25 = msg.reader().readByte();
					if ((int)b25 <= 0)
					{
						return;
					}
					global::Char.myCharz().arrArchive = new Archivement[(int)b25];
					for (int num60 = 0; num60 < (int)b25; num60++)
					{
						global::Char.myCharz().arrArchive[num60] = new Archivement();
						global::Char.myCharz().arrArchive[num60].info1 = num60 + 1 + ". " + msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num60].info2 = msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num60].money = (int)msg.reader().readShort();
						global::Char.myCharz().arrArchive[num60].isFinish = msg.reader().readBoolean();
						global::Char.myCharz().arrArchive[num60].isRecieve = msg.reader().readBoolean();
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
				}
				else if ((int)b24 == 1)
				{
					int num61 = (int)msg.reader().readUnsignedByte();
					if (global::Char.myCharz().arrArchive[num61] != null)
					{
						global::Char.myCharz().arrArchive[num61].isRecieve = true;
					}
				}
				break;
			}
			case 33:
			{
				if (ServerListScreen.stopDownload)
				{
					return;
				}
				if (!GameCanvas.isGetResourceFromServer())
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null)
					{
						LoginScr.isContinueToLogin = true;
					}
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag2 = true;
				sbyte b26 = msg.reader().readByte();
				Res.outz("action = " + b26);
				if ((int)b26 == 0)
				{
					int num62 = msg.reader().readInt();
					string text2 = Rms.loadRMSString("ResVersion");
					int num63 = (text2 == null || !(text2 != string.Empty)) ? -1 : int.Parse(text2);
					if (num63 == -1 || num63 != num62)
					{
						ServerListScreen.loadScreen = false;
						GameCanvas.serverScreen.show2();
					}
					else
					{
						Res.outz("login ngay");
						SmallImage.loadBigRMS();
						SplashScr.imgLogo = null;
						ServerListScreen.loadScreen = true;
						if (GameCanvas.currentScreen != GameCanvas.loginScr)
						{
							GameCanvas.serverScreen.switchToMe();
						}
					}
				}
				if ((int)b26 == 1)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					short nBig = msg.reader().readShort();
					ServerListScreen.nBig = (int)nBig;
					Service.gI().getResource(2, null);
				}
				if ((int)b26 == 2)
				{
					try
					{
						Controller.isLoadingData = true;
						GameCanvas.endDlg();
						ServerListScreen.demPercent++;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string original = msg.reader().readUTF();
						string[] array2 = Res.split(original, "/", 0);
						string filename = "x" + mGraphics.zoomLevel + array2[array2.Length - 1];
						int num64 = msg.reader().readInt();
						sbyte[] data2 = new sbyte[num64];
						msg.reader().read(ref data2, 0, num64);
						Rms.saveRMS(filename, data2);
					}
					catch (Exception ex2)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
				if ((int)b26 == 3 && flag2)
				{
					Controller.isLoadingData = false;
					int num65 = msg.reader().readInt();
					Res.outz("last version= " + num65);
					Rms.saveRMSString("ResVersion", num65 + string.Empty);
					Service.gI().getResource(3, null);
					GameCanvas.endDlg();
					SplashScr.imgLogo = null;
					SmallImage.loadBigRMS();
					mSystem.gcc();
					ServerListScreen.bigOk = true;
					ServerListScreen.loadScreen = true;
					GameScr.gI().loadGameScr();
					if (GameCanvas.currentScreen != GameCanvas.loginScr)
					{
						GameCanvas.serverScreen.switchToMe();
					}
				}
				break;
			}
			case 37:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int avatar = (int)msg.reader().readShort();
				string chat = msg.reader().readUTF();
				ChatPopup.addBigMessage(chat, 100000, new Npc(-1, 0, 0, 0, 0, 0)
				{
					avatar = avatar
				});
				sbyte b27 = msg.reader().readByte();
				if ((int)b27 == 0)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				if ((int)b27 == 1)
				{
					string p = msg.reader().readUTF();
					string caption = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption, ChatPopup.serverChatPopUp, 1000, p);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
				}
				break;
			}
			case 38:
				global::Char.myCharz().cMaxStamina = msg.reader().readShort();
				break;
			case 39:
				global::Char.myCharz().cStamina = (int)msg.reader().readShort();
				break;
			case 40:
			{
				Res.outz("RECIEVE ICON");
				this.demCount += 1f;
				int num66 = msg.reader().readInt();
				sbyte[] array3 = null;
				try
				{
					array3 = NinjaUtil.readByteArray(msg);
					Res.outz("request hinh icon = " + num66);
					if (num66 == 3896)
					{
						Res.outz("SIZE CHECK= " + array3.Length);
					}
					SmallImage.imgNew[num66].img = this.createImage(array3);
				}
				catch (Exception ex3)
				{
					array3 = null;
					SmallImage.imgNew[num66].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				if (array3 != null && mGraphics.zoomLevel > 1)
				{
					Rms.saveRMS(mGraphics.zoomLevel + "Small" + num66, array3);
				}
				break;
			}
			case 41:
			{
				short id = msg.reader().readShort();
				sbyte[] data3 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById((int)id);
				effDataById.readData(data3);
				sbyte[] array4 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array4, 0, array4.Length);
				break;
			}
			case 42:
			{
				Res.outz("TELEPORT ...................................................");
				InfoDlg.hide();
				int num67 = msg.reader().readInt();
				sbyte b28 = msg.reader().readByte();
				if ((int)b28 != 0)
				{
					if (global::Char.myCharz().charID == num67)
					{
						Controller.isStopReadMessage = true;
						GameScr.lockTick = 500;
						GameScr.gI().center = null;
						if ((int)b28 == 0 || (int)b28 == 1 || (int)b28 == 3)
						{
							Teleport p2 = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, ((int)b28 != 1) ? ((int)b28) : global::Char.myCharz().cgender);
							Teleport.addTeleport(p2);
						}
						if ((int)b28 == 2)
						{
							GameScr.lockTick = 50;
							global::Char.myCharz().hide();
						}
					}
					else
					{
						global::Char char5 = GameScr.findCharInMap(num67);
						if (((int)b28 == 0 || (int)b28 == 1 || (int)b28 == 3) && char5 != null)
						{
							char5.isUsePlane = true;
							Teleport.addTeleport(new Teleport(char5.cx, char5.cy, char5.head, char5.cdir, 0, false, ((int)b28 != 1) ? ((int)b28) : char5.cgender)
							{
								id = num67
							});
						}
						if ((int)b28 == 2)
						{
							char5.hide();
						}
					}
				}
				break;
			}
			case 43:
			{
				int num68 = msg.reader().readInt();
				int bag = (int)msg.reader().readUnsignedByte();
				if (num68 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().bag = bag;
				}
				else if (GameScr.findCharInMap(num68) != null)
				{
					GameScr.findCharInMap(num68).bag = bag;
				}
				break;
			}
			case 44:
			{
				Res.outz("GET BAG");
				int num69 = (int)msg.reader().readUnsignedByte();
				sbyte b29 = msg.reader().readByte();
				ClanImage clanImage = new ClanImage();
				clanImage.ID = num69;
				if ((int)b29 > 0)
				{
					clanImage.idImage = new short[(int)b29];
					for (int num70 = 0; num70 < (int)b29; num70++)
					{
						clanImage.idImage[num70] = msg.reader().readShort();
						Res.outz(string.Concat(new object[]
						{
							"ID=  ",
							num69,
							" frame= ",
							clanImage.idImage[num70]
						}));
					}
					ClanImage.idImages.put(num69 + string.Empty, clanImage);
				}
				break;
			}
			case 45:
			{
				int num71 = (int)msg.reader().readUnsignedByte();
				sbyte b30 = msg.reader().readByte();
				if ((int)b30 > 0)
				{
					ClanImage clanImage2 = ClanImage.getClanImage((sbyte)num71);
					if (clanImage2 != null)
					{
						clanImage2.idImage = new short[(int)b30];
						for (int num72 = 0; num72 < (int)b30; num72++)
						{
							clanImage2.idImage[num72] = msg.reader().readShort();
							if (clanImage2.idImage[num72] > 0)
							{
								SmallImage.vKeys.addElement(clanImage2.idImage[num72] + string.Empty);
							}
						}
					}
				}
				break;
			}
			case 46:
			{
				int num73 = msg.reader().readInt();
				if (num73 != global::Char.myCharz().charID)
				{
					if (GameScr.findCharInMap(num73) != null)
					{
						GameScr.findCharInMap(num73).clanID = msg.reader().readInt();
						if (GameScr.findCharInMap(num73).clanID == -2)
						{
							GameScr.findCharInMap(num73).isCopy = true;
						}
					}
				}
				else if (global::Char.myCharz().clan != null)
				{
					global::Char.myCharz().clan.ID = msg.reader().readInt();
				}
				break;
			}
			case 47:
			{
				GameCanvas.debug("SA7666", 2);
				int num74 = msg.reader().readInt();
				int num75 = -1;
				if (num74 != global::Char.myCharz().charID)
				{
					global::Char char6 = GameScr.findCharInMap(num74);
					if (char6 == null)
					{
						return;
					}
					if (char6.currentMovePoint != null)
					{
						char6.createShadow(char6.cx, char6.cy, 10);
						char6.cx = char6.currentMovePoint.xEnd;
						char6.cy = char6.currentMovePoint.yEnd;
					}
					int num76 = (int)msg.reader().readUnsignedByte();
					Res.outz("player skill ID= " + num76);
					if ((TileMap.tileTypeAtPixel(char6.cx, char6.cy) & 2) == 2)
					{
						char6.setSkillPaint(GameScr.sks[num76], 0);
					}
					else
					{
						char6.setSkillPaint(GameScr.sks[num76], 1);
					}
					sbyte b31 = msg.reader().readByte();
					Res.outz("nAttack = " + b31);
					global::Char[] array5 = new global::Char[(int)b31];
					for (i = 0; i < array5.Length; i++)
					{
						num75 = msg.reader().readInt();
						global::Char char7;
						if (num75 == global::Char.myCharz().charID)
						{
							char7 = global::Char.myCharz();
							if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay)
							{
								Service.gI().requestChangeZone(-1, -1);
								GameScr.isChangeZone = true;
							}
						}
						else
						{
							char7 = GameScr.findCharInMap(num75);
						}
						array5[i] = char7;
						if (i == 0)
						{
							if (char6.cx <= char7.cx)
							{
								char6.cdir = 1;
							}
							else
							{
								char6.cdir = -1;
							}
						}
					}
					if (i > 0)
					{
						char6.attChars = new global::Char[i];
						for (i = 0; i < char6.attChars.Length; i++)
						{
							char6.attChars[i] = array5[i];
						}
						char6.mobFocus = null;
						char6.charFocus = char6.attChars[0];
					}
				}
				else
				{
					sbyte b32 = msg.reader().readByte();
					sbyte b33 = msg.reader().readByte();
					num75 = msg.reader().readInt();
				}
				sbyte b34 = msg.reader().readByte();
				Res.outz("isRead continue = " + b34);
				if ((int)b34 == 1)
				{
					sbyte b35 = msg.reader().readByte();
					Res.outz("type skill = " + b35);
					if (num75 == global::Char.myCharz().charID)
					{
						@char = global::Char.myCharz();
						int num77 = msg.readInt3Byte();
						Res.outz("dame hit = " + num77);
						@char.isDie = msg.reader().readBoolean();
						if (@char.isDie)
						{
							global::Char.isLockKey = true;
						}
						Res.outz("isDie=" + @char.isDie + "---------------------------------------");
						int num78 = 0;
						bool isCrit = msg.reader().readBoolean();
						@char.isCrit = isCrit;
						@char.isMob = false;
						num77 += num78;
						@char.damHP = num77;
						if ((int)b35 == 0)
						{
							@char.doInjure(num77, 0, isCrit, false);
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num75);
						if (@char == null)
						{
							return;
						}
						int num79 = msg.readInt3Byte();
						Res.outz("dame hit= " + num79);
						@char.isDie = msg.reader().readBoolean();
						Res.outz("isDie=" + @char.isDie + "---------------------------------------");
						int num80 = 0;
						bool isCrit2 = msg.reader().readBoolean();
						@char.isCrit = isCrit2;
						@char.isMob = false;
						num79 += num80;
						@char.damHP = num79;
						if ((int)b35 == 0)
						{
							@char.doInjure(num79, 0, isCrit2, false);
						}
					}
				}
				break;
			}
			case 48:
			{
				sbyte typePK = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
				break;
			}
			case 49:
				break;
			case 50:
			{
				string strInvite = msg.reader().readUTF();
				int clanID = msg.reader().readInt();
				int code = msg.reader().readInt();
				GameScr.gI().clanInvite(strInvite, clanID, code);
				break;
			}
			case 54:
			{
				Res.outz("MY CLAN INFO");
				InfoDlg.hide();
				bool flag3 = false;
				int num81 = msg.reader().readInt();
				Res.outz("clanId= " + num81);
				if (num81 == -1)
				{
					global::Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					if (GameCanvas.panel.member != null)
					{
						GameCanvas.panel.member.removeAllElements();
					}
					if (GameCanvas.panel.myMember != null)
					{
						GameCanvas.panel.myMember.removeAllElements();
					}
					if (GameCanvas.currentScreen == GameScr.gI())
					{
						GameCanvas.panel.setTabClans();
					}
					return;
				}
				GameCanvas.panel.tabIcon = null;
				if (global::Char.myCharz().clan == null)
				{
					global::Char.myCharz().clan = new Clan();
				}
				global::Char.myCharz().clan.ID = num81;
				global::Char.myCharz().clan.name = msg.reader().readUTF();
				global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				global::Char.myCharz().clan.leaderName = msg.reader().readUTF();
				global::Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().role = msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				for (int num82 = 0; num82 < global::Char.myCharz().clan.currMember; num82++)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
				}
				int num83 = (int)msg.reader().readUnsignedByte();
				for (int num84 = 0; num84 < num83; num84++)
				{
					this.readClanMsg(msg, -1);
				}
				if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
				{
					GameCanvas.panel.setTabClans();
				}
				if (flag3)
				{
					GameCanvas.panel.setTabClans();
				}
				break;
			}
			case 55:
			{
				sbyte b36 = msg.reader().readByte();
				if ((int)b36 == 0)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					if (GameCanvas.panel.myMember == null)
					{
						GameCanvas.panel.myMember = new MyVector();
					}
					GameCanvas.panel.myMember.addElement(member2);
					GameCanvas.panel.initTabClans();
				}
				if ((int)b36 == 1)
				{
					GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
					GameCanvas.panel.currentListLength--;
					GameCanvas.panel.initTabClans();
				}
				if ((int)b36 == 2)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					for (int num85 = 0; num85 < GameCanvas.panel.myMember.size(); num85++)
					{
						Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num85);
						if (member4.ID == member3.ID)
						{
							if (global::Char.myCharz().charID == member3.ID)
							{
								global::Char.myCharz().role = member3.role;
							}
							Member o = member3;
							GameCanvas.panel.myMember.removeElement(member4);
							GameCanvas.panel.myMember.insertElementAt(o, num85);
							return;
						}
					}
				}
				break;
			}
			case 56:
				InfoDlg.hide();
				this.readClanMsg(msg, 0);
				if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			case 57:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b37 = msg.reader().readByte();
				for (int num86 = 0; num86 < (int)b37; num86++)
				{
					Member member5 = new Member();
					member5.ID = msg.reader().readInt();
					member5.head = msg.reader().readShort();
					member5.leg = msg.reader().readShort();
					member5.body = msg.reader().readShort();
					member5.name = msg.reader().readUTF();
					member5.role = msg.reader().readByte();
					member5.powerPoint = msg.reader().readUTF();
					member5.donate = msg.reader().readInt();
					member5.receive_donate = msg.reader().readInt();
					member5.clanPoint = msg.reader().readInt();
					member5.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member5);
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				break;
			}
			case 60:
			{
				InfoDlg.hide();
				sbyte b38 = msg.reader().readByte();
				Res.outz("clan = " + b38);
				if ((int)b38 == 0)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[(int)b38];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length);
					for (int num87 = 0; num87 < GameCanvas.panel.clans.Length; num87++)
					{
						GameCanvas.panel.clans[num87] = new Clan();
						GameCanvas.panel.clans[num87].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num87].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num87].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num87].imgID = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num87].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num87].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num87].currMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num87].maxMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num87].date = msg.reader().readInt();
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				if (GameCanvas.panel.isSearchClan)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			}
			case 61:
			{
				InfoDlg.hide();
				sbyte b39 = msg.reader().readByte();
				if ((int)b39 == 1 || (int)b39 == 3)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num88 = (int)msg.reader().readUnsignedByte();
					for (int num89 = 0; num89 < num88; num89++)
					{
						ClanImage clanImage3 = new ClanImage();
						clanImage3.ID = (int)msg.reader().readUnsignedByte();
						clanImage3.name = msg.reader().readUTF();
						clanImage3.xu = msg.reader().readInt();
						clanImage3.luong = msg.reader().readInt();
						if (!ClanImage.isExistClanImage(clanImage3.ID))
						{
							ClanImage.addClanImage(clanImage3);
						}
						else
						{
							ClanImage.getClanImage((sbyte)clanImage3.ID).name = clanImage3.name;
							ClanImage.getClanImage((sbyte)clanImage3.ID).xu = clanImage3.xu;
							ClanImage.getClanImage((sbyte)clanImage3.ID).luong = clanImage3.luong;
						}
					}
					if (global::Char.myCharz().clan != null)
					{
						GameCanvas.panel.changeIcon();
					}
				}
				if ((int)b39 == 4)
				{
					global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				}
				break;
			}
			case 62:
			{
				sbyte b40 = msg.reader().readByte();
				int num90 = msg.reader().readInt();
				short num91 = msg.reader().readShort();
				Res.outz(string.Concat(new object[]
				{
					"skill type= ",
					b40,
					"   player use= ",
					num90
				}));
				if ((int)b40 == 0)
				{
					Res.outz("id use= " + num90);
					if (global::Char.myCharz().charID != num90)
					{
						@char = GameScr.findCharInMap(num90);
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						{
							@char.setSkillPaint(GameScr.sks[(int)num91], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)num91], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						global::Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b41 = msg.reader().readByte();
					Res.outz("npc size= " + b41);
					for (int num92 = 0; num92 < (int)b41; num92++)
					{
						sbyte b42 = msg.reader().readByte();
						sbyte b43 = msg.reader().readByte();
						Res.outz("index= " + b42);
						if (num91 >= 42 && num91 <= 48)
						{
							((Mob)GameScr.vMob.elementAt((int)b42)).isFreez = true;
							((Mob)GameScr.vMob.elementAt((int)b42)).seconds = (int)b43;
							((Mob)GameScr.vMob.elementAt((int)b42)).last = (((Mob)GameScr.vMob.elementAt((int)b42)).cur = mSystem.currentTimeMillis());
						}
					}
					sbyte b44 = msg.reader().readByte();
					for (int num93 = 0; num93 < (int)b44; num93++)
					{
						int num94 = msg.reader().readInt();
						sbyte b45 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"player ID= ",
							num94,
							" my ID= ",
							global::Char.myCharz().charID
						}));
						if (num91 >= 42 && num91 <= 48)
						{
							if (num94 == global::Char.myCharz().charID)
							{
								if (!global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge)
								{
									GameScr.gI().isFreez = true;
									global::Char.myCharz().isFreez = true;
									global::Char.myCharz().freezSeconds = (int)b45;
									global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
									global::Char.myCharz().isLockMove = true;
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num94);
								if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
								{
									@char.isFreez = true;
									@char.seconds = (int)b45;
									@char.freezSeconds = (int)b45;
									@char.lastFreez = (GameScr.findCharInMap(num94).currFreez = mSystem.currentTimeMillis());
								}
							}
						}
					}
				}
				if ((int)b40 == 1)
				{
					if (num90 != global::Char.myCharz().charID)
					{
						GameScr.findCharInMap(num90).isCharge = true;
					}
				}
				if ((int)b40 == 3)
				{
					if (num90 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						global::Char.myCharz().saveLoadPreviousSkill();
					}
					else
					{
						GameScr.findCharInMap(num90).isCharge = false;
					}
				}
				if ((int)b40 == 4)
				{
					if (num90 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz(string.Concat(new object[]
						{
							"second= ",
							global::Char.myCharz().seconds,
							" last= ",
							global::Char.myCharz().last
						}));
					}
					else if (GameScr.findCharInMap(num90) != null)
					{
						int cgender = GameScr.findCharInMap(num90).cgender;
						if (cgender == 0)
						{
							GameScr.findCharInMap(num90).useChargeSkill(false);
						}
						else if (cgender == 1)
						{
							GameScr.findCharInMap(num90).useChargeSkill(true);
						}
						GameScr.findCharInMap(num90).skillTemplateId = (int)num91;
						GameScr.findCharInMap(num90).isUseSkillAfterCharge = true;
						GameScr.findCharInMap(num90).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num90).last = mSystem.currentTimeMillis();
					}
				}
				if ((int)b40 == 5)
				{
					if (num90 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
					else if (GameScr.findCharInMap(num90) != null)
					{
						GameScr.findCharInMap(num90).stopUseChargeSkill();
					}
				}
				if ((int)b40 == 6)
				{
					if (num90 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num91], 0);
					}
					else if (GameScr.findCharInMap(num90) != null)
					{
						GameScr.findCharInMap(num90).setAutoSkillPaint(GameScr.sks[(int)num91], 0);
						SoundMn.gI().gong();
					}
				}
				if ((int)b40 == 7)
				{
					if (num90 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)msg.reader().readShort();
						Res.outz("second = " + global::Char.myCharz().seconds);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else if (GameScr.findCharInMap(num90) != null)
					{
						GameScr.findCharInMap(num90).useChargeSkill(true);
						GameScr.findCharInMap(num90).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num90).last = mSystem.currentTimeMillis();
						SoundMn.gI().gong();
					}
				}
				if ((int)b40 == 8)
				{
					if (num90 != global::Char.myCharz().charID)
					{
						if (GameScr.findCharInMap(num90) != null)
						{
							GameScr.findCharInMap(num90).setAutoSkillPaint(GameScr.sks[(int)num91], 0);
						}
					}
				}
				break;
			}
			case 63:
			{
				bool flag4 = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					flag4 = true;
				}
				sbyte b46 = msg.reader().readByte();
				int num95 = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().arrItemShop = new Item[num95][];
				GameCanvas.panel.shopTabName = new string[num95 + (flag4 ? 0 : 1)][];
				for (int num96 = 0; num96 < GameCanvas.panel.shopTabName.Length; num96++)
				{
					GameCanvas.panel.shopTabName[num96] = new string[2];
				}
				if ((int)b46 == 2)
				{
					GameCanvas.panel.maxPageShop = new int[num95];
					GameCanvas.panel.currPageShop = new int[num95];
				}
				if (!flag4)
				{
					GameCanvas.panel.shopTabName[num95] = mResources.inventory;
				}
				for (int num97 = 0; num97 < num95; num97++)
				{
					string[] array6 = Res.split(msg.reader().readUTF(), "\n", 0);
					if ((int)b46 == 2)
					{
						GameCanvas.panel.maxPageShop[num97] = (int)msg.reader().readUnsignedByte();
					}
					if (array6.Length == 2)
					{
						GameCanvas.panel.shopTabName[num97] = array6;
					}
					if (array6.Length == 1)
					{
						GameCanvas.panel.shopTabName[num97][0] = array6[0];
						GameCanvas.panel.shopTabName[num97][1] = string.Empty;
					}
					int num98 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[num97] = new Item[num98];
					for (int num99 = 0; num99 < num98; num99++)
					{
						short num100 = msg.reader().readShort();
						if (num100 != -1)
						{
							global::Char.myCharz().arrItemShop[num97][num99] = new Item();
							global::Char.myCharz().arrItemShop[num97][num99].template = ItemTemplates.get(num100);
							Res.outz(string.Concat(new object[]
							{
								"name ",
								num97,
								" = ",
								global::Char.myCharz().arrItemShop[num97][num99].template.name,
								" id templat= ",
								global::Char.myCharz().arrItemShop[num97][num99].template.id
							}));
							if ((int)b46 == 0)
							{
								global::Char.myCharz().arrItemShop[num97][num99].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num97][num99].buyGold = msg.reader().readInt();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							}
							else if ((int)b46 == 1)
							{
								global::Char.myCharz().arrItemShop[num97][num99].powerRequire = msg.reader().readLong();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
							}
							else if ((int)b46 == 2)
							{
								global::Char.myCharz().arrItemShop[num97][num99].itemId = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num97][num99].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num97][num99].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num97][num99].buyType = msg.reader().readByte();
								global::Char.myCharz().arrItemShop[num97][num99].quantity = (int)msg.reader().readByte();
								global::Char.myCharz().arrItemShop[num97][num99].isMe = msg.reader().readByte();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							}
							int num101 = (int)msg.reader().readUnsignedByte();
							if (num101 != 0)
							{
								global::Char.myCharz().arrItemShop[num97][num99].itemOption = new ItemOption[num101];
								for (int num102 = 0; num102 < global::Char.myCharz().arrItemShop[num97][num99].itemOption.Length; num102++)
								{
									int num103 = (int)msg.reader().readUnsignedByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									if (num103 != -1)
									{
										global::Char.myCharz().arrItemShop[num97][num99].itemOption[num102] = new ItemOption(num103, param3);
										global::Char.myCharz().arrItemShop[num97][num99].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num97][num99]);
									}
								}
							}
							sbyte b47 = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[num97][num99].newItem = ((int)b47 != 0);
						}
					}
				}
				if (flag4)
				{
					if ((int)b46 != 2)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				if ((int)b46 == 2)
				{
					string[][] array7 = GameCanvas.panel.tabName[1];
					if (flag4)
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array7[0],
							array7[1],
							array7[2],
							array7[3]
						};
					}
					else
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array7[0],
							array7[1],
							array7[2],
							array7[3],
							array7[4]
						};
					}
				}
				GameCanvas.panel.setTypeShop((int)b46);
				GameCanvas.panel.show();
				break;
			}
			case 64:
			{
				sbyte itemAction = msg.reader().readByte();
				sbyte where = msg.reader().readByte();
				sbyte index2 = msg.reader().readByte();
				string info2 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(itemAction, info2, where, index2);
				break;
			}
			case 65:
				global::Char.myCharz().cHPGoc = msg.readInt3Byte();
				global::Char.myCharz().cMPGoc = msg.readInt3Byte();
				global::Char.myCharz().cDamGoc = msg.reader().readInt();
				global::Char.myCharz().cHPFull = msg.readInt3Byte();
				global::Char.myCharz().cMPFull = msg.readInt3Byte();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().cspeed = (int)msg.reader().readByte();
				global::Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().cDamFull = msg.reader().readInt();
				global::Char.myCharz().cDefull = msg.reader().readInt();
				global::Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().expForOneAdd = msg.reader().readShort();
				global::Char.myCharz().cDefGoc = (int)msg.reader().readShort();
				global::Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
				InfoDlg.hide();
				break;
			case 66:
			{
				sbyte b48 = msg.reader().readByte();
				global::Char.myCharz().strLevel = new string[(int)b48];
				for (int num104 = 0; num104 < (int)b48; num104++)
				{
					string text3 = msg.reader().readUTF();
					global::Char.myCharz().strLevel[num104] = text3;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command);
				break;
			}
			case 70:
			{
				sbyte b49 = msg.reader().readByte();
				Res.outz("cAction= " + b49);
				if ((int)b49 == 0)
				{
					global::Char.myCharz().head = (int)msg.reader().readShort();
					global::Char.myCharz().setDefaultPart();
					int num105 = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num105);
					global::Char.myCharz().arrItemBody = new Item[num105];
					for (int num106 = 0; num106 < num105; num106++)
					{
						short num107 = msg.reader().readShort();
						if (num107 != -1)
						{
							global::Char.myCharz().arrItemBody[num106] = new Item();
							global::Char.myCharz().arrItemBody[num106].template = ItemTemplates.get(num107);
							int num108 = (int)global::Char.myCharz().arrItemBody[num106].template.type;
							global::Char.myCharz().arrItemBody[num106].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[num106].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[num106].content = msg.reader().readUTF();
							int num109 = (int)msg.reader().readUnsignedByte();
							if (num109 != 0)
							{
								global::Char.myCharz().arrItemBody[num106].itemOption = new ItemOption[num109];
								for (int num110 = 0; num110 < global::Char.myCharz().arrItemBody[num106].itemOption.Length; num110++)
								{
									int num111 = (int)msg.reader().readUnsignedByte();
									int param4 = (int)msg.reader().readUnsignedShort();
									if (num111 != -1)
									{
										global::Char.myCharz().arrItemBody[num106].itemOption[num110] = new ItemOption(num111, param4);
									}
								}
							}
							if (num108 == 0)
							{
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num106].template.part;
							}
							else if (num108 == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num106].template.part;
							}
						}
					}
				}
				break;
			}
			case 71:
			{
				sbyte b50 = msg.reader().readByte();
				Res.outz("cAction= " + b50);
				if ((int)b50 == 0)
				{
					int num112 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBag = new Item[num112];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num112);
					for (int num113 = 0; num113 < num112; num113++)
					{
						short num114 = msg.reader().readShort();
						if (num114 != -1)
						{
							global::Char.myCharz().arrItemBag[num113] = new Item();
							global::Char.myCharz().arrItemBag[num113].template = ItemTemplates.get(num114);
							global::Char.myCharz().arrItemBag[num113].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBag[num113].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num113].content = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num113].indexUI = num113;
							int num115 = (int)msg.reader().readUnsignedByte();
							if (num115 != 0)
							{
								global::Char.myCharz().arrItemBag[num113].itemOption = new ItemOption[num115];
								for (int num116 = 0; num116 < global::Char.myCharz().arrItemBag[num113].itemOption.Length; num116++)
								{
									int num117 = (int)msg.reader().readUnsignedByte();
									int param5 = (int)msg.reader().readUnsignedShort();
									if (num117 != -1)
									{
										global::Char.myCharz().arrItemBag[num113].itemOption[num116] = new ItemOption(num117, param5);
									}
								}
								global::Char.myCharz().arrItemBag[num113].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num113]);
							}
							if ((int)global::Char.myCharz().arrItemBag[num113].template.type == 11)
							{
							}
							if ((int)global::Char.myCharz().arrItemBag[num113].template.type == 6)
							{
								GameScr.hpPotion += global::Char.myCharz().arrItemBag[num113].quantity;
							}
						}
					}
				}
				if ((int)b50 == 2)
				{
					sbyte b51 = msg.reader().readByte();
					sbyte b52 = msg.reader().readByte();
					int quantity = global::Char.myCharz().arrItemBag[(int)b51].quantity;
					global::Char.myCharz().arrItemBag[(int)b51].quantity = (int)b52;
					if (global::Char.myCharz().arrItemBag[(int)b51].quantity < quantity && (int)global::Char.myCharz().arrItemBag[(int)b51].template.type == 6)
					{
						GameScr.hpPotion -= quantity - global::Char.myCharz().arrItemBag[(int)b51].quantity;
					}
					if (global::Char.myCharz().arrItemBag[(int)b51].quantity == 0)
					{
						global::Char.myCharz().arrItemBag[(int)b51] = null;
					}
				}
				break;
			}
			case 72:
			{
				sbyte b53 = msg.reader().readByte();
				Res.outz("cAction= " + b53);
				if ((int)b53 == 0)
				{
					int num118 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBox = new Item[num118];
					GameCanvas.panel.hasUse = 0;
					for (int num119 = 0; num119 < num118; num119++)
					{
						short num120 = msg.reader().readShort();
						if (num120 != -1)
						{
							global::Char.myCharz().arrItemBox[num119] = new Item();
							global::Char.myCharz().arrItemBox[num119].template = ItemTemplates.get(num120);
							global::Char.myCharz().arrItemBox[num119].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBox[num119].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBox[num119].content = msg.reader().readUTF();
							int num121 = (int)msg.reader().readUnsignedByte();
							if (num121 != 0)
							{
								global::Char.myCharz().arrItemBox[num119].itemOption = new ItemOption[num121];
								for (int num122 = 0; num122 < global::Char.myCharz().arrItemBox[num119].itemOption.Length; num122++)
								{
									int num123 = (int)msg.reader().readUnsignedByte();
									int param6 = (int)msg.reader().readUnsignedShort();
									if (num123 != -1)
									{
										global::Char.myCharz().arrItemBox[num119].itemOption[num122] = new ItemOption(num123, param6);
									}
								}
							}
							GameCanvas.panel.hasUse++;
						}
					}
				}
				if ((int)b53 == 1)
				{
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.show();
				}
				if ((int)b53 == 2)
				{
					sbyte b54 = msg.reader().readByte();
					sbyte b55 = msg.reader().readByte();
					global::Char.myCharz().arrItemBox[(int)b54].quantity = (int)b55;
					if (global::Char.myCharz().arrItemBox[(int)b54].quantity == 0)
					{
						global::Char.myCharz().arrItemBox[(int)b54] = null;
					}
				}
				break;
			}
			case 73:
			{
				sbyte b56 = msg.reader().readByte();
				Res.outz("act= " + b56);
				if ((int)b56 == 0 && GameScr.gI().magicTree != null)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = (int)msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = (int)msg.reader().readShort();
					magicTree.y = (int)msg.reader().readShort();
					magicTree.level = (int)msg.reader().readByte();
					magicTree.currPeas = (int)msg.reader().readShort();
					magicTree.maxPeas = (int)msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas);
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b57 = msg.reader().readByte();
					magicTree.peaPostionX = new int[(int)b57];
					magicTree.peaPostionY = new int[(int)b57];
					for (int num124 = 0; num124 < (int)b57; num124++)
					{
						magicTree.peaPostionX[num124] = (int)msg.reader().readByte();
						magicTree.peaPostionY[num124] = (int)msg.reader().readByte();
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				if ((int)b56 == 1)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							string caption2 = msg.reader().readUTF();
							myVector.addElement(new Command(caption2, GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex4)
					{
						Cout.println("Loi MAGIC_TREE " + ex4.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				if ((int)b56 == 2)
				{
					GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
				}
				break;
			}
			case 75:
			{
				if (GameCanvas.lowGraphic && TileMap.mapID != 51 && TileMap.mapID != 103)
				{
					return;
				}
				short num125 = msg.reader().readShort();
				int num126 = msg.reader().readInt();
				sbyte[] array8 = null;
				Image image = null;
				try
				{
					array8 = new sbyte[num126];
					for (int num127 = 0; num127 < num126; num127++)
					{
						array8[num127] = msg.reader().readByte();
					}
					image = Image.createImage(array8, 0, num126);
					BgItem.imgNew.put(num125 + string.Empty, image);
				}
				catch (Exception ex5)
				{
					array8 = null;
					BgItem.imgNew.put(num125 + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				if (array8 != null)
				{
					if (mGraphics.zoomLevel > 1)
					{
						Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num125, array8);
					}
					BgItemMn.blendcurrBg(num125, image);
				}
				break;
			}
			case 76:
			{
				if (GameCanvas.lowGraphic && TileMap.mapID != 51)
				{
					return;
				}
				TileMap.vItemBg.removeAllElements();
				short num128 = msg.reader().readShort();
				Cout.LogError2("nItem= " + num128);
				for (int num129 = 0; num129 < (int)num128; num129++)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num129;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = (int)msg.reader().readShort();
					bgItem.dy = (int)msg.reader().readShort();
					sbyte b58 = msg.reader().readByte();
					bgItem.tileX = new int[(int)b58];
					bgItem.tileY = new int[(int)b58];
					for (int num130 = 0; num130 < (int)b58; num130++)
					{
						bgItem.tileX[num129] = (int)msg.reader().readByte();
						bgItem.tileY[num129] = (int)msg.reader().readByte();
					}
					TileMap.vItemBg.addElement(bgItem);
				}
				break;
			}
			case 77:
				this.messageSubCommand(msg);
				break;
			case 78:
				this.messageNotLogin(msg);
				break;
			case 79:
				this.messageNotMap(msg);
				break;
			case 81:
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				global::Char.isLoadingMap = false;
				if (GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				break;
			case 82:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case 83:
				global::Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				mSystem.gcc();
				TileMap.mapID = (int)msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = (int)msg.reader().readByte();
				TileMap.bgID = (int)msg.reader().readByte();
				Cout.println(string.Concat(new object[]
				{
					"load planet from server: ",
					TileMap.planetID,
					"bgType= ",
					TileMap.bgType,
					"............................."
				}));
				TileMap.typeMap = (int)msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = (int)msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception ex6)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					this.messWait = msg;
					return;
				}
				this.loadInfoMap(msg);
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				break;
			case 85:
				GameCanvas.debug("SA65", 2);
				global::Char.isLockKey = true;
				global::Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().focus.removeAllElements();
				global::Char.myCharz().testCharId = -9999;
				global::Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				break;
			case 86:
			{
				GameCanvas.debug("SA60", 2);
				short num131 = msg.reader().readShort();
				for (int num132 = 0; num132 < GameScr.vItemMap.size(); num132++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num132)).itemMapID == (int)num131)
					{
						GameScr.vItemMap.removeElementAt(num132);
						break;
					}
				}
				break;
			}
			case 87:
			{
				GameCanvas.debug("SA61", 2);
				global::Char.myCharz().itemFocus = null;
				short num131 = msg.reader().readShort();
				for (int num133 = 0; num133 < GameScr.vItemMap.size(); num133++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num133);
					if (itemMap.itemMapID == (int)num131)
					{
						itemMap.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
						string text4 = msg.reader().readUTF();
						i = 0;
						try
						{
							i = (int)msg.reader().readShort();
							if ((int)itemMap.template.type == 9)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().xu += i;
							}
							else if ((int)itemMap.template.type == 10)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luong += i;
							}
						}
						catch (Exception ex7)
						{
						}
						if (text4.Equals(string.Empty))
						{
							if ((int)itemMap.template.type == 9)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
								SoundMn.gI().getItem();
							}
							else if ((int)itemMap.template.type == 10)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
								SoundMn.gI().getItem();
							}
							else
							{
								GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i + " ")) + itemMap.template.name, 0);
								SoundMn.gI().getItem();
							}
							if (i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
							}
						}
						else if (text4.Length == 1)
						{
							Cout.LogError3("strInf.Length =1:  " + text4);
						}
						else
						{
							GameScr.info1.addInfo(text4, 0);
						}
						break;
					}
				}
				break;
			}
			case 88:
			{
				GameCanvas.debug("SA62", 2);
				short num131 = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				int num134 = 0;
				while (num134 < GameScr.vItemMap.size())
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num134);
					if (itemMap2.itemMapID == (int)num131)
					{
						if (@char == null)
						{
							return;
						}
						itemMap2.setPoint(@char.cx, @char.cy - 10);
						if (itemMap2.x < @char.cx)
						{
							@char.cdir = -1;
						}
						else if (itemMap2.x > @char.cx)
						{
							@char.cdir = 1;
						}
						break;
					}
					else
					{
						num134++;
					}
				}
				break;
			}
			case 89:
			{
				GameCanvas.debug("SA63", 2);
				int num135 = (int)msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num135].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				global::Char.myCharz().arrItemBag[num135] = null;
				break;
			}
			case 93:
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				break;
			case 103:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameCanvas.debug("SA76v1", 2);
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
				}
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[(int)msg.reader().readByte()];
				for (int num136 = 0; num136 < @char.attMobs.Length; num136++)
				{
					Mob mob5 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
					@char.attMobs[num136] = mob5;
					if (num136 == 0)
					{
						if (@char.cx <= mob5.x)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
					}
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				global::Char[] array5 = new global::Char[10];
				i = 0;
				try
				{
					for (i = 0; i < array5.Length; i++)
					{
						int num137 = msg.reader().readInt();
						global::Char char8;
						if (num137 == global::Char.myCharz().charID)
						{
							char8 = global::Char.myCharz();
						}
						else
						{
							char8 = GameScr.findCharInMap(num137);
						}
						array5[i] = char8;
						if (i == 0)
						{
							if (@char.cx <= char8.cx)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception ex8)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex8.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				if (i > 0)
				{
					@char.attChars = new global::Char[i];
					for (i = 0; i < @char.attChars.Length; i++)
					{
						@char.attChars[i] = array5[i];
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				break;
			}
			case 108:
			{
				bool flag5 = msg.reader().readBool();
				Res.outz("isRes= " + flag5);
				if (!flag5)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = false;
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
					GameCanvas.endDlg();
					GameCanvas.loginScr.doLogin();
				}
				break;
			}
			case 109:
				global::Char.isLoadingMap = true;
				LoginScr.isLoggingIn = false;
				if (!GameScr.isLoadAllData)
				{
					GameScr.gI().initSelectChar();
				}
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				break;
			case 113:
				GameCanvas.debug("SA70", 2);
				global::Char.myCharz().xu = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				GameCanvas.endDlg();
				break;
			case 114:
			{
				sbyte type = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				string info3 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(type, info3, id2);
				break;
			}
			case 118:
			{
				GameCanvas.debug("SA9", 2);
				int num138 = (int)msg.reader().readByte();
				Mob.arrMobTemplate[num138].data.readData(NinjaUtil.readByteArray(msg));
				for (int num139 = 0; num139 < GameScr.vMob.size(); num139++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(num139);
					if (mob2.templateId == num138)
					{
						mob2.w = Mob.arrMobTemplate[num138].data.width;
						mob2.h = Mob.arrMobTemplate[num138].data.height;
					}
				}
				sbyte[] array9 = NinjaUtil.readByteArray(msg);
				Image img = Image.createImage(array9, 0, array9.Length);
				Mob.arrMobTemplate[num138].data.img = img;
				break;
			}
			case 127:
			{
				GameCanvas.debug("SZ7", 2);
				Mob mob2 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
				int num137 = msg.reader().readInt();
				if (num137 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num137);
				}
				@char.moveFast = new short[3];
				@char.moveFast[0] = 0;
				@char.moveFast[1] = (short)mob2.x;
				@char.moveFast[2] = (short)mob2.y;
				break;
			}
			case -125:
				GameCanvas.debug("SA69", 2);
				global::Char.myCharz().xuInBox = msg.reader().readInt();
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readUnsignedByte()];
				for (int num140 = 0; num140 < global::Char.myCharz().arrItemBox.Length; num140++)
				{
					short num141 = msg.reader().readShort();
					if (num141 != -1)
					{
						global::Char.myCharz().arrItemBox[num140] = new Item();
						global::Char.myCharz().arrItemBox[num140].typeUI = 4;
						global::Char.myCharz().arrItemBox[num140].indexUI = num140;
						global::Char.myCharz().arrItemBox[num140].template = ItemTemplates.get(num141);
						global::Char.myCharz().arrItemBox[num140].isLock = msg.reader().readBool();
						if (global::Char.myCharz().arrItemBox[num140].isTypeBody())
						{
							global::Char.myCharz().arrItemBox[num140].upgrade = (int)msg.reader().readByte();
						}
						global::Char.myCharz().arrItemBox[num140].isExpires = msg.reader().readBool();
						global::Char.myCharz().arrItemBox[num140].quantity = (int)msg.reader().readShort();
					}
				}
				break;
			case -122:
			{
				myVector = new MyVector();
				string text5 = msg.reader().readUTF();
				int num142 = (int)msg.reader().readByte();
				for (int num143 = 0; num143 < num142; num143++)
				{
					string caption3 = msg.reader().readUTF();
					short num144 = msg.reader().readShort();
					myVector.addElement(new Command(caption3, GameCanvas.instance, 88819, num144));
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				break;
			}
			case -120:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				break;
			case -117:
			{
				GameCanvas.debug("SA68", 2);
				int num145 = (int)msg.reader().readShort();
				for (int num146 = 0; num146 < GameScr.vNpc.size(); num146++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num146);
					if (npc2.template.npcTemplateId == num145 && npc2.Equals(global::Char.myCharz().npcFocus))
					{
						string chat2 = msg.reader().readUTF();
						string[] array10 = new string[(int)msg.reader().readByte()];
						for (int num147 = 0; num147 < array10.Length; num147++)
						{
							array10[num147] = msg.reader().readUTF();
						}
						GameScr.gI().createMenu(array10, npc2);
						ChatPopup.addChatPopup(chat2, 100000, npc2);
						return;
					}
				}
				Npc npc3 = new Npc(num145, 0, -100, 100, num145, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				string chat3 = msg.reader().readUTF();
				string[] array11 = new string[(int)msg.reader().readByte()];
				for (int num148 = 0; num148 < array11.Length; num148++)
				{
					array11[num148] = msg.reader().readUTF();
				}
				try
				{
					short avatar2 = msg.reader().readShort();
					npc3.avatar = (int)avatar2;
				}
				catch (Exception ex9)
				{
				}
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array11, npc3);
				ChatPopup.addChatPopup(chat3, 100000, npc3);
				break;
			}
			case -116:
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					for (;;)
					{
						string caption4 = msg.reader().readUTF();
						myVector.addElement(new Command(caption4, GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex10)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex10.ToString());
				}
				if (global::Char.myCharz().npcFocus == null)
				{
					return;
				}
				for (int num149 = 0; num149 < global::Char.myCharz().npcFocus.template.menu.Length; num149++)
				{
					string[] array12 = global::Char.myCharz().npcFocus.template.menu[num149];
					myVector.addElement(new Command(array12[0], GameCanvas.instance, 88820, array12));
				}
				GameCanvas.menu.startAt(myVector, 3);
				break;
			case -111:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num145 = (int)msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num145);
				string text6 = msg.reader().readUTF();
				text6 = Res.changeString(text6);
				for (int num150 = 0; num150 < GameScr.vNpc.size(); num150++)
				{
					Npc npc4 = (Npc)GameScr.vNpc.elementAt(num150);
					Res.outz("npc id= " + npc4.template.npcTemplateId);
					if (npc4.template.npcTemplateId == num145)
					{
						ChatPopup.addChatPopupMultiLine(text6, 100000, npc4);
						GameCanvas.panel.hideNow();
						return;
					}
				}
				Npc npc5 = new Npc(num145, 0, 0, 0, num145, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				if (npc5.template.npcTemplateId == 5)
				{
					npc5.charID = 5;
				}
				try
				{
					npc5.avatar = (int)msg.reader().readShort();
				}
				catch (Exception ex11)
				{
				}
				ChatPopup.addChatPopupMultiLine(text6, 100000, npc5);
				GameCanvas.panel.hideNow();
				break;
			}
			case -110:
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
				{
					InfoDlg.showWait();
				}
				break;
			case -109:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short taskId = msg.reader().readShort();
				sbyte index3 = msg.reader().readByte();
				string text7 = msg.reader().readUTF();
				text7 = Res.changeString(text7);
				string text8 = msg.reader().readUTF();
				text8 = Res.changeString(text8);
				string[] array13 = new string[(int)msg.reader().readByte()];
				string[] array14 = new string[array13.Length];
				GameScr.tasks = new int[array13.Length];
				GameScr.mapTasks = new int[array13.Length];
				short[] array15 = new short[array13.Length];
				short count = -1;
				for (int num151 = 0; num151 < array13.Length; num151++)
				{
					string text9 = msg.reader().readUTF();
					text9 = Res.changeString(text9);
					GameScr.tasks[num151] = (int)msg.reader().readByte();
					GameScr.mapTasks[num151] = (int)msg.reader().readShort();
					string text10 = msg.reader().readUTF();
					text10 = Res.changeString(text10);
					array15[num151] = -1;
					if (!text9.Equals(string.Empty))
					{
						array13[num151] = text9;
						array14[num151] = text10;
					}
				}
				try
				{
					count = msg.reader().readShort();
					for (int num152 = 0; num152 < array13.Length; num152++)
					{
						array15[num152] = msg.reader().readShort();
					}
				}
				catch (Exception ex12)
				{
					Cout.println("Loi TASK_GET " + ex12.ToString());
				}
				global::Char.myCharz().taskMaint = new Task(taskId, index3, text7, text8, array13, array15, count, array14);
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				global::Char.taskAction(false);
				break;
			}
			case -108:
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				global::Char.myCharz().taskMaint.index++;
				global::Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				global::Char.taskAction(true);
				break;
			case -106:
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				global::Char.myCharz().taskMaint.count = msg.reader().readShort();
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				break;
			case -103:
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap);
				global::Char.isLockKey = false;
				global::Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
				break;
			case -102:
				GameCanvas.debug("SA4", 2);
				GameScr.gI().resetButton();
				break;
			case -99:
			{
				sbyte b59 = msg.reader().readByte();
				Panel.vGameInfo.removeAllElements();
				for (int num153 = 0; num153 < (int)b59; num153++)
				{
					GameInfo gameInfo = new GameInfo();
					gameInfo.id = msg.reader().readShort();
					gameInfo.main = msg.reader().readUTF();
					gameInfo.content = msg.reader().readUTF();
					Panel.vGameInfo.addElement(gameInfo);
					bool hasRead = Rms.loadRMSInt(gameInfo.id + string.Empty) != -1;
					gameInfo.hasRead = hasRead;
				}
				break;
			}
			case -95:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				int num154 = (int)msg.reader().readUnsignedByte();
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[num154], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[num154], 1);
				}
				GameCanvas.debug("SA769991v2", 2);
				Mob[] array16 = new Mob[10];
				i = 0;
				try
				{
					GameCanvas.debug("SA769991v3", 2);
					for (i = 0; i < array16.Length; i++)
					{
						GameCanvas.debug("SA769991v4-num" + i, 2);
						Mob mob6 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
						array16[i] = mob6;
						if (i == 0)
						{
							if (@char.cx <= mob6.x)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
						GameCanvas.debug("SA769991v5-num" + i, 2);
					}
				}
				catch (Exception ex13)
				{
					Cout.println("Loi PLAYER_ATTACK_NPC " + ex13.ToString());
				}
				GameCanvas.debug("SA769992", 2);
				if (i > 0)
				{
					@char.attMobs = new Mob[i];
					for (i = 0; i < @char.attMobs.Length; i++)
					{
						@char.attMobs[i] = array16[i];
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
				}
				break;
			}
			case -93:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num137 = msg.reader().readInt();
				if (num137 == global::Char.myCharz().charID)
				{
					bool flag6 = false;
					@char = global::Char.myCharz();
					@char.cHP = msg.readInt3Byte();
					int num155 = msg.readInt3Byte();
					Res.outz("dame hit = " + num155);
					if (num155 != 0)
					{
						@char.doInjure();
					}
					int num156 = 0;
					try
					{
						flag6 = msg.reader().readBoolean();
						sbyte b60 = msg.reader().readByte();
						if ((int)b60 != -1)
						{
							Res.outz("hit eff= " + b60);
							EffecMn.addEff(new Effect((int)b60, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception ex14)
					{
					}
					num155 += num156;
					if ((int)global::Char.myCharz().cTypePk != 4)
					{
						if (num155 == 0)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						}
						else
						{
							GameScr.startFlyText("-" + num155, @char.cx, @char.cy - @char.ch, 0, -3, flag6 ? mFont.FATAL : mFont.RED);
						}
					}
				}
				else
				{
					@char = GameScr.findCharInMap(num137);
					if (@char == null)
					{
						return;
					}
					@char.cHP = msg.readInt3Byte();
					bool flag7 = false;
					int num157 = msg.readInt3Byte();
					if (num157 != 0)
					{
						@char.doInjure();
					}
					int num158 = 0;
					try
					{
						flag7 = msg.reader().readBoolean();
						sbyte b61 = msg.reader().readByte();
						if ((int)b61 != -1)
						{
							Res.outz("hit eff= " + b61);
							EffecMn.addEff(new Effect((int)b61, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception ex15)
					{
					}
					num157 += num158;
					if ((int)@char.cTypePk != 4)
					{
						if (num157 == 0)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
						}
						else
						{
							GameScr.startFlyText("-" + num157, @char.cx, @char.cy - @char.ch, 0, -3, flag7 ? mFont.FATAL : mFont.ORANGE);
						}
					}
				}
				break;
			}
			case -92:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				break;
			}
			case -91:
			{
				GameCanvas.debug("SZ7", 2);
				int num137 = msg.reader().readInt();
				global::Char char9;
				if (num137 == global::Char.myCharz().charID)
				{
					char9 = global::Char.myCharz();
				}
				else
				{
					char9 = GameScr.findCharInMap(num137);
				}
				char9.moveFast = new short[3];
				char9.moveFast[0] = 0;
				short num159 = msg.reader().readShort();
				short num160 = msg.reader().readShort();
				char9.moveFast[1] = num159;
				char9.moveFast[2] = num160;
				try
				{
					num137 = msg.reader().readInt();
					global::Char char10;
					if (num137 == global::Char.myCharz().charID)
					{
						char10 = global::Char.myCharz();
					}
					else
					{
						char10 = GameScr.findCharInMap(num137);
					}
					char10.cx = (int)num159;
					char10.cy = (int)num160;
				}
				catch (Exception ex16)
				{
					Cout.println("Loi MOVE_FAST " + ex16.ToString());
				}
				break;
			}
			case -87:
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.killCharId = global::Char.myCharz().charID;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					global::Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
				}
				break;
			case -86:
				GameCanvas.debug("SZ4", 2);
				global::Char.myCharz().killCharId = msg.reader().readInt();
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
				global::Char.isManualFocus = true;
				break;
			case -85:
				GameCanvas.debug("SZ5", 2);
				@char = global::Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex17)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex17.ToString());
				}
				@char.killCharId = -9999;
				break;
			case -84:
			{
				sbyte b62 = msg.reader().readSByte();
				string text11 = msg.reader().readUTF();
				short num161 = msg.reader().readShort();
				if (ItemTime.isExistMessage((int)b62))
				{
					if (num161 != 0)
					{
						ItemTime.getMessageById((int)b62).initTimeText(b62, text11, (int)num161);
					}
					else
					{
						GameScr.textTime.removeElement(ItemTime.getMessageById((int)b62));
					}
				}
				else
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(b62, text11, (int)num161);
					GameScr.textTime.addElement(itemTime);
				}
				break;
			}
			case -81:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short num131 = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num162 = msg.reader().readInt();
				short r = 0;
				if (num162 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap o2 = new ItemMap(num162, num131, itemTemplateID, x, y, r);
				GameScr.vItemMap.addElement(o2);
				break;
			}
			case -80:
				GameCanvas.debug("SA633355", 2);
				global::Char.myCharz().arrItemBag[(int)msg.reader().readByte()].quantity = (int)msg.reader().readShort();
				break;
			case -71:
				break;
			case -70:
				break;
			case -68:
			{
				GameCanvas.debug("SXX4", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isDisable = msg.reader().readBool();
				break;
			}
			case -67:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isDontMove = msg.reader().readBool();
				break;
			}
			case -66:
			{
				GameCanvas.debug("SXX8", 2);
				int num137 = msg.reader().readInt();
				if (num137 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num137);
				}
				if (@char == null)
				{
					return;
				}
				Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				if (@char.mobMe != null)
				{
					@char.mobMe.attackOtherMob(mobToAttack);
				}
				break;
			}
			case -65:
			{
				int num137 = msg.reader().readInt();
				if (num137 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num137);
					if (@char == null)
					{
						return;
					}
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = (int)msg.reader().readShort();
				@char.cy = (int)msg.reader().readShort();
				@char.liveFromDead();
				break;
			}
			case -64:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isFire = msg.reader().readBool();
				break;
			}
			case -63:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isIce = msg.reader().readBool();
				if (!mob7.isIce)
				{
					ServerEffect.addServerEffect(77, mob7.x, mob7.y - 9, 1);
				}
				break;
			}
			case -62:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isWind = msg.reader().readBool();
				break;
			}
			case -61:
			{
				string info4 = msg.reader().readUTF();
				short num163 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num163), TField.INPUT_TYPE_ANY);
				break;
			}
			case -59:
				GameCanvas.debug("SA577", 2);
				this.requestItemPlayer(msg);
				break;
			case -57:
			{
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					GameCanvas.endDlg();
				}
				string text12 = msg.reader().readUTF();
				string text13 = msg.reader().readUTF();
				text13 = Res.changeString(text13);
				string text14 = string.Empty;
				global::Char char11 = null;
				sbyte b63 = 0;
				if (!text12.Equals(string.Empty))
				{
					char11 = new global::Char();
					char11.charID = msg.reader().readInt();
					char11.head = (int)msg.reader().readShort();
					char11.body = (int)msg.reader().readShort();
					char11.bag = (int)msg.reader().readShort();
					char11.leg = (int)msg.reader().readShort();
					b63 = msg.reader().readByte();
					char11.cName = text12;
				}
				text14 += text13;
				InfoDlg.hide();
				if (text12.Equals(string.Empty))
				{
					GameScr.info1.addInfo(text14, 0);
				}
				else
				{
					GameScr.info2.addInfoWithChar(text14, char11, (int)b63 == 0);
					if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
					{
						GameCanvas.panel.initLogMessage();
					}
				}
				break;
			}
			case -55:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case -37:
			{
				sbyte b64 = msg.reader().readByte();
				Res.outz("spec type= " + b64);
				if ((int)b64 == 0)
				{
					Panel.spearcialImage = msg.reader().readShort();
					Panel.specialInfo = msg.reader().readUTF();
				}
				else if ((int)b64 == 1)
				{
					sbyte b65 = msg.reader().readByte();
					global::Char.myCharz().infoSpeacialSkill = new string[(int)b65][];
					global::Char.myCharz().imgSpeacialSkill = new short[(int)b65][];
					GameCanvas.panel.speacialTabName = new string[(int)b65][];
					for (int num164 = 0; num164 < (int)b65; num164++)
					{
						GameCanvas.panel.speacialTabName[num164] = new string[2];
						string[] array17 = Res.split(msg.reader().readUTF(), "\n", 0);
						if (array17.Length == 2)
						{
							GameCanvas.panel.speacialTabName[num164] = array17;
						}
						if (array17.Length == 1)
						{
							GameCanvas.panel.speacialTabName[num164][0] = array17[0];
							GameCanvas.panel.speacialTabName[num164][1] = string.Empty;
						}
						int num165 = (int)msg.reader().readByte();
						global::Char.myCharz().infoSpeacialSkill[num164] = new string[num165];
						global::Char.myCharz().imgSpeacialSkill[num164] = new short[num165];
						for (int num166 = 0; num166 < num165; num166++)
						{
							global::Char.myCharz().imgSpeacialSkill[num164][num166] = msg.reader().readShort();
							global::Char.myCharz().infoSpeacialSkill[num164][num166] = msg.reader().readUTF();
						}
					}
					GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
					GameCanvas.panel.setTypeSpeacialSkill();
					GameCanvas.panel.show();
				}
				break;
			}
			}
			command = msg.command;
			switch (command + 17)
			{
			case 0:
				GameCanvas.debug("SA88", 2);
				global::Char.myCharz().meDead = true;
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
				try
				{
					global::Char.myCharz().cPower = msg.reader().readLong();
					global::Char.myCharz().applyCharLevelPercent();
				}
				catch (Exception ex18)
				{
					Cout.println("Loi tai ME_DIE " + msg.command);
				}
				global::Char.myCharz().countKill = 0;
				break;
			case 1:
				GameCanvas.debug("SA90", 2);
				if (global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0)
				{
					global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
					global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
					global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
				}
				global::Char.myCharz().liveFromDead();
				global::Char.myCharz().isLockMove = false;
				global::Char.myCharz().meDead = false;
				break;
			default:
				switch (command + 75)
				{
				case 0:
				{
					Mob mob8 = null;
					try
					{
						mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					}
					catch (Exception ex19)
					{
					}
					if (mob8 != null)
					{
						mob8.levelBoss = msg.reader().readByte();
						if ((int)mob8.levelBoss > 0)
						{
							mob8.typeSuperEff = Res.random(0, 3);
						}
					}
					break;
				}
				default:
					switch (command)
					{
					case 95:
					{
						GameCanvas.debug("SA77", 22);
						int num167 = msg.reader().readInt();
						global::Char.myCharz().xu += num167;
						GameScr.startFlyText((num167 <= 0) ? (string.Empty + num167) : ("+" + num167), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						break;
					}
					case 96:
						GameCanvas.debug("SA77a", 22);
						global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
						break;
					case 97:
					{
						sbyte b66 = msg.reader().readByte();
						for (int num168 = 0; num168 < global::Char.myCharz().taskOrders.size(); num168++)
						{
							TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num168);
							if (taskOrder.taskId == (int)b66)
							{
								taskOrder.count = (int)msg.reader().readShort();
								break;
							}
						}
						break;
					}
					default:
						if (command != 18)
						{
							if (command != 19)
							{
								if (command != 44)
								{
									if (command != 45)
									{
										if (command != 66)
										{
											if (command == 74)
											{
												GameCanvas.debug("SA85", 2);
												Mob mob8 = null;
												try
												{
													mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
												}
												catch (Exception ex20)
												{
													Cout.println("Loi tai NPC CHANGE " + msg.command);
												}
												if (mob8 != null && mob8.status != 0 && mob8.status != 0)
												{
													mob8.status = 0;
													ServerEffect.addServerEffect(60, mob8.x, mob8.y, 1);
													ItemMap itemMap3 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob8.x, mob8.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
													GameScr.vItemMap.addElement(itemMap3);
													if (Res.abs(itemMap3.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap3.x - global::Char.myCharz().cx) < 24)
													{
														global::Char.myCharz().charFocus = null;
													}
												}
											}
										}
										else
										{
											Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
										}
									}
									else
									{
										GameCanvas.debug("SA84", 2);
										Mob mob8 = null;
										try
										{
											mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
										}
										catch (Exception ex21)
										{
											Cout.println("Loi tai NPC_MISS  " + ex21.ToString());
										}
										if (mob8 != null)
										{
											mob8.hp = msg.reader().readInt();
											GameScr.startFlyText(mResources.miss, mob8.x, mob8.y - mob8.h, 0, -2, mFont.MISS);
										}
									}
								}
								else
								{
									GameCanvas.debug("SA91", 2);
									int num169 = msg.reader().readInt();
									string text15 = msg.reader().readUTF();
									Res.outz(string.Concat(new object[]
									{
										"user id= ",
										num169,
										" text= ",
										text15
									}));
									if (global::Char.myCharz().charID == num169)
									{
										@char = global::Char.myCharz();
									}
									else
									{
										@char = GameScr.findCharInMap(num169);
									}
									if (@char == null)
									{
										return;
									}
									@char.addInfo(text15);
								}
							}
							else
							{
								global::Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
								global::Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							}
						}
						else
						{
							sbyte b67 = msg.reader().readByte();
							for (int num170 = 0; num170 < (int)b67; num170++)
							{
								int charId = msg.reader().readInt();
								int cx = (int)msg.reader().readShort();
								int cy = (int)msg.reader().readShort();
								int cHPShow = msg.readInt3Byte();
								global::Char char12 = GameScr.findCharInMap(charId);
								if (char12 != null)
								{
									char12.cx = cx;
									char12.cy = cy;
									char12.cHP = (char12.cHPShow = cHPShow);
									char12.lastUpdateTime = mSystem.currentTimeMillis();
								}
							}
						}
						break;
					}
					break;
				case 2:
				{
					sbyte b68 = msg.reader().readByte();
					for (int num171 = 0; num171 < GameScr.vNpc.size(); num171++)
					{
						Npc npc6 = (Npc)GameScr.vNpc.elementAt(num171);
						if (npc6.template.npcTemplateId == (int)b68)
						{
							sbyte b69 = msg.reader().readByte();
							if ((int)b69 == 0)
							{
								npc6.isHide = true;
							}
							else
							{
								npc6.isHide = false;
							}
							break;
						}
					}
					break;
				}
				}
				break;
			case 4:
			{
				GameCanvas.debug("SA82", 2);
				int num172 = (int)msg.reader().readUnsignedByte();
				if (num172 > GameScr.vMob.size() - 1 || num172 < 0)
				{
					return;
				}
				Mob mob8 = (Mob)GameScr.vMob.elementAt(num172);
				mob8.sys = (int)msg.reader().readByte();
				mob8.levelBoss = msg.reader().readByte();
				if ((int)mob8.levelBoss != 0)
				{
					mob8.typeSuperEff = Res.random(0, 3);
				}
				mob8.x = mob8.xFirst;
				mob8.y = mob8.yFirst;
				mob8.status = 5;
				mob8.injureThenDie = false;
				mob8.hp = msg.reader().readInt();
				mob8.maxHp = mob8.hp;
				ServerEffect.addServerEffect(60, mob8.x, mob8.y, 1);
				break;
			}
			case 5:
			{
				Res.outz("SERVER SEND MOB DIE");
				GameCanvas.debug("SA85", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex22)
				{
					Cout.println("LOi tai NPC_DIE cmd " + msg.command);
				}
				if (mob8 != null && mob8.status != 0 && mob8.status != 0)
				{
					mob8.startDie();
					try
					{
						int num173 = msg.readInt3Byte();
						bool flag8 = msg.reader().readBool();
						if (flag8)
						{
							GameScr.startFlyText("-" + num173, mob8.x, mob8.y - mob8.h, 0, -2, mFont.FATAL);
						}
						else
						{
							GameScr.startFlyText("-" + num173, mob8.x, mob8.y - mob8.h, 0, -2, mFont.ORANGE);
						}
						sbyte b70 = msg.reader().readByte();
						for (int num174 = 0; num174 < (int)b70; num174++)
						{
							ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob8.x, mob8.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
							int num175 = msg.reader().readInt();
							itemMap4.playerId = num175;
							Res.outz(string.Concat(new object[]
							{
								"playerid= ",
								num175,
								" my id= ",
								global::Char.myCharz().charID
							}));
							GameScr.vItemMap.addElement(itemMap4);
							if (Res.abs(itemMap4.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - global::Char.myCharz().cx) < 24)
							{
								global::Char.myCharz().charFocus = null;
							}
						}
					}
					catch (Exception ex23)
					{
						Cout.println(string.Concat(new object[]
						{
							"LOi tai NPC_DIE ",
							ex23.ToString(),
							" cmd ",
							msg.command
						}));
					}
				}
				break;
			}
			case 6:
			{
				GameCanvas.debug("SA86", 2);
				Mob mob8 = null;
				try
				{
					int index4 = (int)msg.reader().readUnsignedByte();
					mob8 = (Mob)GameScr.vMob.elementAt(index4);
				}
				catch (Exception ex24)
				{
					Cout.println("Loi tai NPC_ATTACK_ME " + msg.command);
				}
				if (mob8 != null)
				{
					global::Char.myCharz().isDie = false;
					global::Char.isLockKey = false;
					int num176 = msg.readInt3Byte();
					int num177;
					try
					{
						num177 = msg.readInt3Byte();
					}
					catch (Exception ex25)
					{
						num177 = 0;
					}
					if (mob8.isBusyAttackSomeOne)
					{
						global::Char.myCharz().doInjure(num176, num177, false, true);
					}
					else
					{
						mob8.dame = num176;
						mob8.dameMp = num177;
						mob8.setAttack(global::Char.myCharz());
					}
				}
				break;
			}
			case 7:
			{
				GameCanvas.debug("SA87", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex26)
				{
				}
				GameCanvas.debug("SA87x1", 2);
				if (mob8 != null)
				{
					GameCanvas.debug("SA87x2", 2);
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
					{
						return;
					}
					GameCanvas.debug("SA87x3", 2);
					int num178 = msg.readInt3Byte();
					mob8.dame = @char.cHP - num178;
					@char.cHPNew = num178;
					GameCanvas.debug("SA87x4", 2);
					try
					{
						@char.cMP = msg.readInt3Byte();
					}
					catch (Exception ex27)
					{
					}
					GameCanvas.debug("SA87x5", 2);
					if (mob8.isBusyAttackSomeOne)
					{
						@char.doInjure(mob8.dame, 0, false, true);
					}
					else
					{
						mob8.setAttack(@char);
					}
					GameCanvas.debug("SA87x6", 2);
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA83", 2);
				Mob mob8 = null;
				try
				{
					mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception ex28)
				{
				}
				GameCanvas.debug("SA83v1", 2);
				if (mob8 != null)
				{
					mob8.hp = msg.readInt3Byte();
					int num179 = msg.readInt3Byte();
					if (num179 == 1)
					{
						return;
					}
					bool flag9 = false;
					try
					{
						flag9 = msg.reader().readBoolean();
					}
					catch (Exception ex29)
					{
					}
					sbyte b71 = msg.reader().readByte();
					if ((int)b71 != -1)
					{
						EffecMn.addEff(new Effect((int)b71, mob8.x, mob8.getY(), 3, 1, -1));
					}
					GameCanvas.debug("SA83v2", 2);
					if (flag9)
					{
						GameScr.startFlyText("-" + num179, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.FATAL);
					}
					else if (num179 == 0)
					{
						mob8.x = mob8.xFirst;
						mob8.y = mob8.yFirst;
						GameScr.startFlyText(mResources.miss, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num179, mob8.x, mob8.getY() - mob8.getH(), 0, -2, mFont.ORANGE);
					}
				}
				GameCanvas.debug("SA83v3", 2);
				break;
			}
			case 9:
				GameCanvas.debug("SA89", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				@char.cPk = msg.reader().readByte();
				@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
				break;
			case 10:
			{
				GameCanvas.debug("SA80", 2);
				int num180 = msg.reader().readInt();
				Cout.println("RECEVED MOVE OF " + num180);
				for (int num181 = 0; num181 < GameScr.vCharInMap.size(); num181++)
				{
					global::Char char13 = null;
					try
					{
						char13 = (global::Char)GameScr.vCharInMap.elementAt(num181);
					}
					catch (Exception ex30)
					{
						Cout.println("Loi PLAYER_MOVE " + ex30.ToString());
					}
					if (char13 == null)
					{
						break;
					}
					if (char13.charID == num180)
					{
						GameCanvas.debug("SA8x2y" + num181, 2);
						char13.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort());
						char13.lastUpdateTime = mSystem.currentTimeMillis();
						break;
					}
				}
				GameCanvas.debug("SA80x3", 2);
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA81", 2);
				int num180 = msg.reader().readInt();
				for (int num182 = 0; num182 < GameScr.vCharInMap.size(); num182++)
				{
					global::Char char14 = (global::Char)GameScr.vCharInMap.elementAt(num182);
					if (char14 != null && char14.charID == num180)
					{
						if (!char14.isInvisiblez && !char14.isUsePlane)
						{
							ServerEffect.addServerEffect(60, char14.cx, char14.cy, 1);
						}
						if (!char14.isUsePlane)
						{
							GameScr.vCharInMap.removeElementAt(num182);
						}
						return;
					}
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA79", 2);
				int charID = msg.reader().readInt();
				int num183 = msg.reader().readInt();
				global::Char char15;
				if (num183 != -100)
				{
					char15 = new global::Char();
					char15.charID = charID;
					char15.clanID = num183;
				}
				else
				{
					char15 = new Mabu();
					char15.charID = charID;
					char15.clanID = num183;
				}
				if (char15.clanID == -2)
				{
					char15.isCopy = true;
				}
				if (this.readCharInfo(char15, msg))
				{
					sbyte b72 = msg.reader().readByte();
					if (char15.cy <= 10 && (int)b72 != 0 && (int)b72 != 2)
					{
						Res.outz(string.Concat(new object[]
						{
							"nhân vật bay trên trời xuống x= ",
							char15.cx,
							" y= ",
							char15.cy
						}));
						Teleport teleport = new Teleport(char15.cx, char15.cy, char15.head, char15.cdir, 1, false, ((int)b72 != 1) ? ((int)b72) : char15.cgender);
						teleport.id = char15.charID;
						char15.isTeleport = true;
						Teleport.addTeleport(teleport);
					}
					if ((int)b72 == 2)
					{
						char15.show();
					}
					for (int num184 = 0; num184 < GameScr.vMob.size(); num184++)
					{
						Mob mob9 = (Mob)GameScr.vMob.elementAt(num184);
						if (mob9 != null && mob9.isMobMe && mob9.mobId == char15.charID)
						{
							Res.outz("co 1 con quai");
							char15.mobMe = mob9;
							char15.mobMe.x = char15.cx;
							char15.mobMe.y = char15.cy - 40;
							break;
						}
					}
					if (GameScr.findCharInMap(char15.charID) == null)
					{
						GameScr.vCharInMap.addElement(char15);
					}
					char15.isMonkey = msg.reader().readByte();
					short num185 = msg.reader().readShort();
					Res.outz("mount id= " + num185 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					if (num185 != -1)
					{
						char15.isHaveMount = true;
						if (num185 == 346 || num185 == 347 || num185 == 348)
						{
							char15.isMountVip = false;
						}
						else if (num185 == 349 || num185 == 350 || num185 == 351)
						{
							char15.isMountVip = true;
						}
						else if (num185 == 396)
						{
							char15.isEventMount = true;
						}
						else if (num185 == 532)
						{
							char15.isSpeacialMount = true;
						}
					}
					else
					{
						char15.isHaveMount = false;
					}
				}
				sbyte b73 = msg.reader().readByte();
				Res.outz("addplayer:   " + b73);
				char15.cFlag = b73;
				char15.isNhapThe = ((int)msg.reader().readByte() == 1);
				GameScr.gI().getFlagImage(char15.charID, char15.cFlag);
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA78", 2);
				sbyte b74 = msg.reader().readByte();
				int num186 = msg.reader().readInt();
				if ((int)b74 == 0)
				{
					global::Char.myCharz().cPower += (long)num186;
				}
				if ((int)b74 == 1)
				{
					global::Char.myCharz().cTiemNang += (long)num186;
				}
				if ((int)b74 == 2)
				{
					global::Char.myCharz().cPower += (long)num186;
					global::Char.myCharz().cTiemNang += (long)num186;
				}
				global::Char.myCharz().applyCharLevelPercent();
				if ((int)global::Char.myCharz().cTypePk != 3)
				{
					GameScr.startFlyText(((num186 <= 0) ? string.Empty : "+") + num186, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
					if (num186 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002)
					{
						ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
						ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
					}
				}
				break;
			}
			case 15:
			{
				GameCanvas.debug("SA77", 22);
				int num187 = msg.reader().readInt();
				global::Char.myCharz().yen += num187;
				GameScr.startFlyText((num187 <= 0) ? (string.Empty + num187) : ("+" + num187), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			case 16:
			{
				GameCanvas.debug("SA77", 222);
				int num188 = msg.reader().readInt();
				global::Char.myCharz().xu += num188;
				global::Char.myCharz().yen -= num188;
				GameScr.startFlyText("+" + num188, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			}
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception ex31)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0002F3CC File Offset: 0x0002D5CC
	private void createItem(myReader d)
	{
		GameScr.vcItem = d.readByte();
		ItemTemplates.itemTemplates.clear();
		GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
		for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
		{
			GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
			GameScr.gI().iOptionTemplates[i].id = i;
			GameScr.gI().iOptionTemplates[i].name = d.readUTF();
			GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
		}
		int num = (int)d.readShort();
		for (int j = 0; j < num; j++)
		{
			ItemTemplate it = new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBool());
			ItemTemplates.add(it);
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0002F4D0 File Offset: 0x0002D6D0
	private void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[(int)d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int lineWidth = 130;
				if (GameCanvas.w == 128 || GameCanvas.h <= 208)
				{
					lineWidth = 100;
				}
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0002F908 File Offset: 0x0002DB08
	private void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[(int)d.readUnsignedByte()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
		sbyte b = 0;
		while ((int)b < Npc.arrNpcTemplate.Length)
		{
			Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
			Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
			Npc.arrNpcTemplate[(int)b].name = d.readUTF();
			Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
				}
			}
			b += 1;
		}
		Mob.arrMobTemplate = new MobTemplate[(int)d.readByte()];
		sbyte b2 = 0;
		while ((int)b2 < Mob.arrMobTemplate.Length)
		{
			Mob.arrMobTemplate[(int)b2] = new MobTemplate();
			Mob.arrMobTemplate[(int)b2].mobTemplateId = b2;
			Mob.arrMobTemplate[(int)b2].type = d.readByte();
			Mob.arrMobTemplate[(int)b2].name = d.readUTF();
			Mob.arrMobTemplate[(int)b2].hp = d.readInt();
			Mob.arrMobTemplate[(int)b2].rangeMove = d.readByte();
			Mob.arrMobTemplate[(int)b2].speed = d.readByte();
			Mob.arrMobTemplate[(int)b2].dartType = d.readByte();
			b2 += 1;
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0002FB3C File Offset: 0x0002DD3C
	private void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
	private Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception ex)
		{
		}
		return null;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0002FC00 File Offset: 0x0002DE00
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			if (num < 0)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0002FC44 File Offset: 0x0002DE44
	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = (int)b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
			bool flag = false;
			GameScr.isNewClanMessage = false;
			if ((int)b == 0)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
				{
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				}
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else if ((int)b == 1)
			{
				clanMessage.recieve = (int)msg.reader().readByte();
				clanMessage.maxCap = (int)msg.reader().readByte();
				flag = ((int)msg.reader().readByte() == 1);
				if (flag)
				{
					GameScr.isNewClanMessage = true;
				}
				if (clanMessage.playerId != global::Char.myCharz().charID)
				{
					if (clanMessage.recieve < clanMessage.maxCap)
					{
						clanMessage.option = new string[]
						{
							mResources.donate
						};
					}
					else
					{
						clanMessage.option = null;
					}
				}
				if (GameCanvas.panel.cp != null)
				{
					GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
				}
			}
			else if ((int)b == 2 && (int)global::Char.myCharz().role == 0)
			{
				GameScr.isNewClanMessage = true;
				clanMessage.option = new string[]
				{
					mResources.CANCEL,
					mResources.receive
				};
			}
			if (GameCanvas.currentScreen != GameScr.instance)
			{
				GameScr.isNewClanMessage = false;
			}
			else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
			{
				GameScr.isNewClanMessage = false;
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception ex)
		{
			Cout.println("LOI TAI CMD -= " + msg.command);
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0002FEE0 File Offset: 0x0002E0E0
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("is loading map = " + global::Char.isLoadingMap);
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, ((int)teleport3 != 1) ? -1 : global::Char.myCharz().cx, ((int)teleport3 != 0) ? 0 : -1);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		global::Char.myCharz().cvx = 0;
		global::Char.myCharz().statusMe = 4;
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().mobFocus = null;
		global::Char.myCharz().charFocus = null;
		global::Char.myCharz().npcFocus = null;
		global::Char.myCharz().itemFocus = null;
		global::Char.myCharz().skillPaint = null;
		global::Char.myCharz().setMabuHold(false);
		global::Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		if (global::Char.myCharz().cy >= TileMap.pxh - 100)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			EffectChar effectChar = (EffectChar)global::Char.myCharz().vEff.elementAt(i);
			if ((int)effectChar.template.type == 10)
			{
				global::Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = global::Char.myCharz().cHP;
		GameScr.gI().dMP = global::Char.myCharz().cMP;
		global::Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		if (global::Char.myCharz().cy <= 10 && (int)teleport3 != 0 && (int)teleport3 != 2)
		{
			Teleport p = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, ((int)teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender);
			Teleport.addTeleport(p);
			global::Char.myCharz().isTeleport = true;
		}
		if ((int)teleport3 == 2)
		{
			global::Char.myCharz().show();
		}
		if (GameScr.gI().isRongThanXuatHien)
		{
			if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			if (mGraphics.zoomLevel > 1)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID, 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00030284 File Offset: 0x0002E484
	public void loadInfoMap(Message msg)
	{
		try
		{
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			global::Char.myCharz().cx = (global::Char.myCharz().cxSend = (global::Char.myCharz().cxFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().cy = (global::Char.myCharz().cySend = (global::Char.myCharz().cyFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().xSd = global::Char.myCharz().cx;
			global::Char.myCharz().ySd = global::Char.myCharz().cy;
			Res.outz(string.Concat(new object[]
			{
				"head= ",
				global::Char.myCharz().head,
				" body= ",
				global::Char.myCharz().body,
				" left= ",
				global::Char.myCharz().leg,
				" x= ",
				global::Char.myCharz().cx,
				" y= ",
				global::Char.myCharz().cy,
				" chung toc= ",
				global::Char.myCharz().cgender
			}));
			if (global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100)
			{
				global::Char.myCharz().cdir = 1;
			}
			else if (global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw)
			{
				global::Char.myCharz().cdir = -1;
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num);
			if (!GameScr.info1.isDone)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				if ((TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23) || waypoint.minX < 0 || waypoint.minX <= 24)
				{
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			GameCanvas.debug("SA75x5", 2);
			num = (int)msg.reader().readByte();
			Mob.newMob.removeAllElements();
			sbyte b = 0;
			while ((int)b < num)
			{
				Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readByte(), (int)msg.reader().readByte(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				if ((int)Mob.arrMobTemplate[mob.templateId].type != 0)
				{
					if ((int)b % 3 == 0)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += 10 - (int)b % 20;
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				if (mob.templateId == 70)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 71)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 72)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				if (bigBoss != null)
				{
					GameScr.vMob.addElement(bigBoss);
				}
				else if (bachTuoc != null)
				{
					GameScr.vMob.addElement(bachTuoc);
				}
				else if (bigBoss2 != null)
				{
					GameScr.vMob.addElement(bigBoss2);
				}
				else
				{
					GameScr.vMob.addElement(mob);
				}
				b += 1;
			}
			for (int j = 0; j < Mob.lastMob.size(); j++)
			{
				string text = (string)Mob.lastMob.elementAt(j);
				if (!Mob.isExistNewMob(text))
				{
					Mob.arrMobTemplate[int.Parse(text)].data = null;
					Mob.lastMob.removeElementAt(j);
					j--;
				}
			}
			if (global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null)
			{
				global::Char.myCharz().mobMe.getData();
				global::Char.myCharz().mobMe.x = global::Char.myCharz().cx;
				global::Char.myCharz().mobMe.y = global::Char.myCharz().cy - 40;
				GameScr.vMob.addElement(global::Char.myCharz().mobMe);
			}
			num = (int)msg.reader().readByte();
			byte b2 = 0;
			while ((int)b2 < num)
			{
				b2 += 1;
			}
			GameCanvas.debug("SA75x6", 2);
			num = (int)msg.reader().readByte();
			Res.outz("NPC size= " + num);
			for (int k = 0; k < num; k++)
			{
				sbyte b3 = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				if ((int)b4 != 6)
				{
					if ((global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || ((int)b4 != 7 && (int)b4 != 8 && (int)b4 != 9))
					{
						if (global::Char.myCharz().taskMaint.taskId >= 6 || (int)b4 != 16)
						{
							if ((int)b4 == 4)
							{
								GameScr.gI().magicTree = new MagicTree(k, (int)b3, (int)cx, (int)num2, (int)b4, (int)num3);
								Service.gI().magicTree(2);
								GameScr.vNpc.addElement(GameScr.gI().magicTree);
							}
							else
							{
								Npc o = new Npc(k, (int)b3, (int)cx, (int)(num2 + 3), (int)b4, (int)num3);
								GameScr.vNpc.addElement(o);
							}
						}
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = (int)msg.reader().readByte();
			Res.outz("item size = " + num);
			for (int l = 0; l < num; l++)
			{
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num4 = msg.reader().readInt();
				short r = 0;
				if (num4 == -2)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num4, itemMapID, itemTemplateID, x, y, r);
				bool flag = false;
				for (int m = 0; m < GameScr.vItemMap.size(); m++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(m);
					if (itemMap2.itemMapID == itemMap.itemMapID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
			}
			if (!GameCanvas.lowGraphic || (GameCanvas.lowGraphic && (TileMap.mapID == 51 || TileMap.mapID == 103)))
			{
				short num5 = msg.reader().readShort();
				TileMap.vCurrItem.removeAllElements();
				if (mGraphics.zoomLevel == 1)
				{
					BgItem.clearHashTable();
				}
				BgItem.vKeysNew.removeAllElements();
				Res.outz("nItem= " + num5);
				for (int n = 0; n < (int)num5; n++)
				{
					short id = msg.reader().readShort();
					short num6 = msg.reader().readShort();
					short num7 = msg.reader().readShort();
					if (TileMap.getBIById((int)id) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)id);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)id;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)num6 * (int)TileMap.size;
						bgItem.y = (int)num7 * (int)TileMap.size;
						bgItem.layer = bibyId.layer;
						if (TileMap.isExistMoreOne(bgItem.id))
						{
							bgItem.trans = ((n % 2 != 0) ? 2 : 0);
							if (TileMap.mapID == 45)
							{
								bgItem.trans = 0;
							}
						}
						if (!BgItem.imgNew.containsKey(bgItem.idImage + string.Empty))
						{
							if (mGraphics.zoomLevel == 1)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
							}
							else
							{
								bool flag2 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "bgItem" + bgItem.idImage);
								if (array != null)
								{
									if (BgItem.newSmallVersion != null)
									{
										Res.outz(string.Concat(new object[]
										{
											"Small  last= ",
											array.Length % 127,
											"new Version= ",
											BgItem.newSmallVersion[(int)bgItem.idImage]
										}));
										if (array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage])
										{
											flag2 = true;
										}
									}
									if (!flag2)
									{
										Image image = Image.createImage(array, 0, array.Length);
										if (image != null)
										{
											BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
										}
										else
										{
											flag2 = true;
										}
									}
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									if (image == null)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
								}
							}
							BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
				}
				for (int num8 = 0; num8 < BgItem.vKeysLast.size(); num8++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(num8);
					if (!BgItem.isExistKeyNews(text2))
					{
						BgItem.imgNew.remove(text2);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 1))
						{
							BgItem.imgNew.remove(text2 + "blend" + 1);
						}
						if (BgItem.imgNew.containsKey(text2 + "blend" + 3))
						{
							BgItem.imgNew.remove(text2 + "blend" + 3);
						}
						BgItem.vKeysLast.removeElementAt(num8);
						num8--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num9 = msg.reader().readShort();
				for (int num10 = 0; num10 < (int)num9; num10++)
				{
					string key = msg.reader().readUTF();
					string value = msg.reader().readUTF();
					this.keyValueAction(key, value);
				}
				for (int num11 = 0; num11 < Effect.lastEff.size(); num11++)
				{
					string text3 = (string)Effect.lastEff.elementAt(num11);
					if (!Effect.isExistNewEff(text3))
					{
						Effect.removeEffData(int.Parse(text3));
						Effect.lastEff.removeElementAt(num11);
						num11--;
					}
				}
			}
			TileMap.bgType = (int)msg.reader().readByte();
			sbyte teleport = msg.reader().readByte();
			this.loadCurrMap(teleport);
			global::Char.isLoadingMap = false;
			GameCanvas.debug("SA75x8", 2);
			Resources.UnloadUnusedAssets();
			GC.Collect();
			Cout.LogError("----------DA CHAY XONG LOAD INFO MAP");
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI LOADMAP INFO " + ex.ToString());
		}
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0003118C File Offset: 0x0002F38C
	public void keyValueAction(string key, string value)
	{
		if (key.Equals("eff"))
		{
			if (Panel.graphics > 0)
			{
				return;
			}
			string[] array = Res.split(value, ".", 0);
			int id = int.Parse(array[0]);
			int layer = int.Parse(array[1]);
			int x = int.Parse(array[2]);
			int y = int.Parse(array[3]);
			int loop;
			int loopCount;
			if (array.Length <= 4)
			{
				loop = -1;
				loopCount = 1;
			}
			else
			{
				loop = int.Parse(array[4]);
				loopCount = int.Parse(array[5]);
			}
			Effect effect = new Effect(id, x, y, layer, loop, loopCount);
			if (array.Length > 6)
			{
				effect.typeEff = int.Parse(array[6]);
				if (array.Length > 7)
				{
					effect.indexFrom = int.Parse(array[7]);
					effect.indexTo = int.Parse(array[8]);
				}
			}
			EffecMn.addEff(effect);
		}
		else if (key.Equals("beff"))
		{
			if (Panel.graphics > 1)
			{
				return;
			}
			BackgroudEffect.addEffect(int.Parse(value));
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00031294 File Offset: 0x0002F494
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			sbyte b2 = b;
			switch (b2)
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				sbyte b3 = msg.reader().readByte();
				if (GameCanvas.loginScr.isLogin2)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				}
				if ((int)GameScr.vsData != (int)GameScr.vcData)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
				{
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception ex)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				}
				if ((int)GameScr.vsMap != (int)GameScr.vcMap)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NRmap"));
							this.createMap(dataInputStream.r);
						}
						LoginScr.isUpdateMap = false;
					}
					catch (Exception ex2)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				}
				if ((int)GameScr.vsSkill != (int)GameScr.vcSkill)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							DataInputStream dataInputStream2 = new DataInputStream(Rms.loadRMS("NRskill"));
							this.createSkill(dataInputStream2.r);
						}
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception ex3)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				}
				if ((int)GameScr.vsItem != (int)GameScr.vcItem)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
				{
					try
					{
						DataInputStream dataInputStream3 = new DataInputStream(Rms.loadRMS("NRitem"));
						this.createItem(dataInputStream3.r);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception ex4)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
				}
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					if (!GameScr.isLoadAllData)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b4 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b4);
				GameScr.exps = new long[(int)b4];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				break;
			}
			default:
				switch (b2)
				{
				case 35:
					GameCanvas.endDlg();
					GameScr.gI().resetButton();
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					break;
				case 36:
					GameScr.typeActive = msg.reader().readByte();
					Res.outz("load Me Active: " + GameScr.typeActive);
					break;
				}
				break;
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createMap(msg.reader());
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRmap", data);
				sbyte[] data2 = new sbyte[]
				{
					GameScr.vcMap
				};
				Rms.saveRMS("NRmapVersion", data2);
				LoginScr.isUpdateMap = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] data3 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data3);
				Rms.saveRMS("NRskill", data3);
				sbyte[] data4 = new sbyte[]
				{
					GameScr.vcSkill
				};
				Rms.saveRMS("NRskillVersion", data4);
				LoginScr.isUpdateSkill = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 8:
			{
				Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createItem(msg.reader());
				msg.reader().reset();
				sbyte[] data5 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data5);
				Rms.saveRMS("NRitem", data5);
				sbyte[] data6 = new sbyte[]
				{
					GameScr.vcItem
				};
				Rms.saveRMS("NRitemVersion", data6);
				LoginScr.isUpdateItem = false;
				if ((int)GameScr.vsData == (int)GameScr.vcData && (int)GameScr.vsMap == (int)GameScr.vcMap && (int)GameScr.vsSkill == (int)GameScr.vcSkill && (int)GameScr.vsItem == (int)GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					global::Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					mSystem.gcc();
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = (int)msg.reader().readByte();
					TileMap.tmh = (int)msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.outz("   M apsize= " + TileMap.tmw * TileMap.tmh);
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						if (num < 0)
						{
							num += 256;
						}
						TileMap.maps[j] = (int)((ushort)num);
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = this.messWait;
					this.loadInfoMap(msg);
				}
				catch (Exception ex5)
				{
					Cout.LogError("LOI TAI CASE REQUEST_MAPTEMPLATE " + ex5.ToString());
				}
				msg.cleanup();
				this.messWait.cleanup();
				msg = (this.messWait = null);
				break;
			case 12:
				GameCanvas.debug("SA10", 2);
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				break;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk, 0);
				break;
			}
		}
		catch (Exception ex6)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command);
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00031D04 File Offset: 0x0002FF04
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
		}
		catch (Exception ex)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00031D54 File Offset: 0x0002FF54
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			sbyte b2 = b;
			switch (b2)
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				global::Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = global::Char.myCharz();
				global::Char.myCharz().charID = msg.reader().readInt();
				global::Char.myCharz().ctaskId = (int)msg.reader().readByte();
				global::Char.myCharz().cgender = (int)msg.reader().readByte();
				global::Char.myCharz().head = (int)msg.reader().readShort();
				global::Char.myCharz().cName = msg.reader().readUTF();
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().cTypePk = msg.reader().readByte();
				global::Char.myCharz().cPower = msg.reader().readLong();
				global::Char.myCharz().applyCharLevelPercent();
				global::Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
				global::Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				sbyte b3 = msg.reader().readByte();
				sbyte b4 = 0;
				while ((int)b4 < (int)b3)
				{
					Skill skill = Skills.get(msg.reader().readShort());
					this.useSkill(skill);
					b4 += 1;
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				global::Char.myCharz().xu = msg.reader().readInt();
				global::Char.myCharz().yen = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
				try
				{
					global::Char.myCharz().setDefaultPart();
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						short num = msg.reader().readShort();
						if (num != -1)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int num2 = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num3 = (int)msg.reader().readUnsignedByte();
							if (num3 != 0)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num3];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									int num4 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									if (num4 != -1)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num4, param);
									}
								}
							}
							if (num2 == 0)
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body);
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
							else if (num2 == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
								Res.outz("toi day =======================================" + global::Char.myCharz().leg);
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
				global::Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
				GameScr.hpPotion = 0;
				for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
				{
					short num5 = msg.reader().readShort();
					if (num5 != -1)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num5);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b5 = msg.reader().readByte();
						if ((int)b5 != 0)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b5];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								int num6 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								if (num6 != -1)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num6, param2);
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						if ((int)global::Char.myCharz().arrItemBag[k].template.type == 6)
						{
							GameScr.hpPotion += global::Char.myCharz().arrItemBag[k].quantity;
						}
					}
				}
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int m = 0; m < global::Char.myCharz().arrItemBox.Length; m++)
				{
					short num7 = msg.reader().readShort();
					if (num7 != -1)
					{
						global::Char.myCharz().arrItemBox[m] = new Item();
						global::Char.myCharz().arrItemBox[m].template = ItemTemplates.get(num7);
						global::Char.myCharz().arrItemBox[m].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBox[m].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].itemOption = new ItemOption[(int)msg.reader().readByte()];
						for (int n = 0; n < global::Char.myCharz().arrItemBox[m].itemOption.Length; n++)
						{
							int num8 = (int)msg.reader().readUnsignedByte();
							int param3 = (int)msg.reader().readUnsignedShort();
							if (num8 != -1)
							{
								global::Char.myCharz().arrItemBox[m].itemOption[n] = new ItemOption(num8, param3);
								global::Char.myCharz().arrItemBox[m].getCompare();
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				global::Char.myCharz().statusMe = 4;
				int num9 = Rms.loadRMSInt(global::Char.myCharz().cName + "vci");
				if (num9 < 1)
				{
					GameScr.isViewClanInvite = false;
				}
				else
				{
					GameScr.isViewClanInvite = true;
				}
				short num10 = msg.reader().readShort();
				global::Char.idHead = new short[(int)num10];
				global::Char.idAvatar = new short[(int)num10];
				for (int num11 = 0; num11 < (int)num10; num11++)
				{
					global::Char.idHead[num11] = msg.reader().readShort();
					global::Char.idAvatar[num11] = msg.reader().readShort();
				}
				for (int num12 = 0; num12 < GameScr.info1.charId.Length; num12++)
				{
					GameScr.info1.charId[num12] = new int[3];
				}
				GameScr.info1.charId[global::Char.myCharz().cgender][0] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][1] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][2] = (int)msg.reader().readShort();
				global::Char.myCharz().isNhapThe = ((int)msg.reader().readByte() == 1);
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe);
				GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)global::Char.myCharz().cgender);
				Service.gI().androidPack();
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				break;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
				{
					global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
					global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				sbyte b3 = msg.reader().readByte();
				sbyte b6 = 0;
				while ((int)b6 < (int)b3)
				{
					short skillId = msg.reader().readShort();
					Skill skill2 = Skills.get(skillId);
					this.useSkill(skill2);
					b6 += 1;
				}
				GameScr.gI().sortSkill();
				if (GameScr.isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				break;
			}
			default:
				switch (b2)
				{
				case 61:
				{
					string text = msg.reader().readUTF();
					sbyte[] array = new sbyte[msg.reader().readInt()];
					msg.reader().read(ref array);
					if (array.Length == 0)
					{
						array = null;
					}
					if (text.Equals("KSkill"))
					{
						GameScr.gI().onKSkill(array);
					}
					else if (text.Equals("OSkill"))
					{
						GameScr.gI().onOSkill(array);
					}
					else if (text.Equals("CSkill"))
					{
						GameScr.gI().onCSkill(array);
					}
					break;
				}
				case 62:
				{
					Res.outz("ME UPDATE SKILL");
					short skillId2 = msg.reader().readShort();
					Skill skill3 = Skills.get(skillId2);
					for (int num13 = 0; num13 < global::Char.myCharz().vSkill.size(); num13++)
					{
						Skill skill4 = (Skill)global::Char.myCharz().vSkill.elementAt(num13);
						if ((int)skill4.template.id == (int)skill3.template.id)
						{
							global::Char.myCharz().vSkill.setElementAt(skill3, num13);
							break;
						}
					}
					for (int num14 = 0; num14 < global::Char.myCharz().vSkillFight.size(); num14++)
					{
						Skill skill5 = (Skill)global::Char.myCharz().vSkillFight.elementAt(num14);
						if ((int)skill5.template.id == (int)skill3.template.id)
						{
							global::Char.myCharz().vSkillFight.setElementAt(skill3, num14);
							break;
						}
					}
					for (int num15 = 0; num15 < GameScr.onScreenSkill.Length; num15++)
					{
						if (GameScr.onScreenSkill[num15] != null && (int)GameScr.onScreenSkill[num15].template.id == (int)skill3.template.id)
						{
							GameScr.onScreenSkill[num15] = skill3;
							break;
						}
					}
					for (int num16 = 0; num16 < GameScr.keySkill.Length; num16++)
					{
						if (GameScr.keySkill[num16] != null && (int)GameScr.keySkill[num16].template.id == (int)skill3.template.id)
						{
							GameScr.keySkill[num16] = skill3;
							break;
						}
					}
					if ((int)global::Char.myCharz().myskill.template.id == (int)skill3.template.id)
					{
						global::Char.myCharz().myskill = skill3;
					}
					GameScr.info1.addInfo(string.Concat(new object[]
					{
						mResources.hasJustUpgrade1,
						skill3.template.name,
						mResources.hasJustUpgrade2,
						skill3.point
					}), 0);
					break;
				}
				case 63:
				{
					sbyte b7 = msg.reader().readByte();
					if ((int)b7 > 0)
					{
						InfoDlg.showWait();
						MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
						for (int num17 = 0; num17 < (int)b7; num17++)
						{
							string caption = msg.reader().readUTF();
							string caption2 = msg.reader().readUTF();
							short menuSelect = msg.reader().readShort();
							global::Char.myCharz().charFocus.menuSelect = (int)menuSelect;
							vPlayerMenu.addElement(new Command(caption, 11115, global::Char.myCharz().charFocus)
							{
								caption2 = caption2
							});
						}
						InfoDlg.hide();
						GameCanvas.panel.setTabPlayerMenu();
					}
					break;
				}
				}
				break;
			case 4:
				GameCanvas.debug("SA23", 2);
				global::Char.myCharz().xu = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				break;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.readInt3Byte();
				if (global::Char.myCharz().cHP > cHP && (int)global::Char.myCharz().cTypePk != 4)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cHP - cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cHP < cHP)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"-",
						cHP - global::Char.myCharz().cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
				}
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				if (!GameScr.isPaintInfoMe)
				{
				}
				break;
			}
			case 6:
				GameCanvas.debug("SA25", 2);
				if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
				{
					int cMP = global::Char.myCharz().cMP;
					global::Char.myCharz().cMP = msg.readInt3Byte();
					if (global::Char.myCharz().cMP > cMP)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"+",
							global::Char.myCharz().cMP - cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
						SoundMn.gI().HP_MPup();
						if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001)
						{
							MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
						}
					}
					if (global::Char.myCharz().cMP < cMP)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"-",
							cMP - global::Char.myCharz().cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					}
					Res.outz("curr MP= " + global::Char.myCharz().cMP);
					GameScr.gI().dMP = global::Char.myCharz().cMP;
					if (!GameScr.isPaintInfoMe)
					{
					}
				}
				break;
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.clanID = msg.reader().readInt();
					if (@char.clanID == -2)
					{
						@char.isCopy = true;
					}
					this.readCharInfo(@char, msg);
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cspeed = (int)msg.reader().readByte();
				}
				break;
			}
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
				}
				break;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.wp = (int)msg.reader().readShort();
					if (@char.wp == -1)
					{
						@char.setDefaultWeapon();
					}
				}
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.body = (int)msg.reader().readShort();
					if (@char.body == -1)
					{
						@char.setDefaultBody();
					}
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
					@char.leg = (int)msg.reader().readShort();
					if (@char.leg == -1)
					{
						@char.setDefaultLeg();
					}
				}
				break;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = (int)msg.reader().readShort();
					@char.eff5BuffMp = (int)msg.reader().readShort();
				}
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					sbyte b8 = msg.reader().readByte();
					Res.outz("player load hp type= " + b8);
					if ((int)b8 == 1)
					{
						ServerEffect.addServerEffect(11, @char, 5);
						ServerEffect.addServerEffect(104, @char, 4);
					}
					try
					{
						@char.cHPFull = msg.readInt3Byte();
					}
					catch (Exception ex2)
					{
					}
				}
				break;
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.cx = (int)msg.reader().readShort();
					@char.cy = (int)msg.reader().readShort();
					@char.statusMe = 1;
					@char.cp3 = 3;
					ServerEffect.addServerEffect(109, @char, 2);
				}
				break;
			}
			case 19:
				GameCanvas.debug("SA17", 2);
				global::Char.myCharz().boxSort();
				break;
			case 20:
			{
				GameCanvas.debug("SA18", 2);
				int num18 = msg.reader().readInt();
				global::Char.myCharz().xu -= num18;
				global::Char.myCharz().xuInBox += num18;
				break;
			}
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num19 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num19;
				global::Char.myCharz().xu += num19;
				break;
			}
			case 23:
			{
				short num20 = msg.reader().readShort();
				Skill skill6 = Skills.get(num20);
				this.useSkill(skill6);
				if (num20 != 0 && num20 != 14 && num20 != 28)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill6.template.name, 0);
				}
				break;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num21 = msg.reader().readInt();
				Res.outz("CID = " + num21);
				if (TileMap.mapID == 130)
				{
					GameScr.gI().starVS();
				}
				if (num21 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					if (GameScr.gI().isVS() && (int)global::Char.myCharz().cTypePk != 0)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk);
					global::Char.myCharz().npcFocus = null;
					if (!GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char @char = GameScr.findCharInMap(num21);
					if (@char != null)
					{
						Res.outz("type pk= " + @char.cTypePk);
						@char.cTypePk = msg.reader().readByte();
						if (@char.isAttacPlayerStatus())
						{
							global::Char.myCharz().charFocus = @char;
						}
					}
				}
				for (int num22 = 0; num22 < GameScr.vCharInMap.size(); num22++)
				{
					global::Char char2 = GameScr.findCharInMap(num22);
					if (char2 != null && (int)char2.cTypePk != 0 && (int)char2.cTypePk == (int)global::Char.myCharz().cTypePk)
					{
						if (!global::Char.myCharz().mobFocus.isMobMe)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				break;
			}
			}
		}
		catch (Exception ex3)
		{
			Cout.println("Loi tai Sub : " + ex3.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00033830 File Offset: 0x00031A30
	private void useSkill(Skill skill)
	{
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		else if (skill.template.Equals(global::Char.myCharz().myskill.template))
		{
			global::Char.myCharz().myskill = skill;
		}
		global::Char.myCharz().vSkill.addElement(skill);
		if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
		{
			if ((int)skill.template.id == global::Char.myCharz().skillTemplateId)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00033948 File Offset: 0x00031B48
	public bool readCharInfo(global::Char c, Message msg)
	{
		try
		{
			c.clevel = (int)msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz(string.Concat(new object[]
			{
				"ADD TYPE PK= ",
				c.cTypePk,
				" to player ",
				c.charID,
				" @@ ",
				c.cName
			}));
			c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
			c.cgender = (int)msg.reader().readByte();
			c.head = (int)msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.readInt3Byte();
			c.dHP = c.cHP;
			if (c.cHP == 0)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.readInt3Byte();
			if (c.cy >= TileMap.pxh - 100)
			{
				c.isFlyUp = true;
			}
			c.body = (int)msg.reader().readShort();
			c.leg = (int)msg.reader().readShort();
			c.bag = (int)msg.reader().readUnsignedByte();
			Res.outz(string.Concat(new object[]
			{
				" body= ",
				c.body,
				" leg= ",
				c.leg,
				" bag=",
				c.bag,
				"BAG ==",
				c.bag,
				"*********************************"
			}));
			c.isShadown = true;
			sbyte b = msg.reader().readByte();
			if (c.wp == -1)
			{
				c.setDefaultWeapon();
			}
			if (c.body == -1)
			{
				c.setDefaultBody();
			}
			if (c.leg == -1)
			{
				c.setDefaultLeg();
			}
			Res.outz("1");
			c.cx = (int)msg.reader().readShort();
			c.cy = (int)msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = (int)msg.reader().readShort();
			c.eff5BuffMp = (int)msg.reader().readShort();
			int num = (int)msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				if ((int)effectChar.template.type == 12 || (int)effectChar.template.type == 11)
				{
					c.isInvisiblez = true;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	// Token: 0x040008C5 RID: 2245
	protected static Controller me;

	// Token: 0x040008C6 RID: 2246
	protected static Controller me2;

	// Token: 0x040008C7 RID: 2247
	public Message messWait;

	// Token: 0x040008C8 RID: 2248
	public static bool isLoadingData;

	// Token: 0x040008C9 RID: 2249
	public static bool isConnectOK;

	// Token: 0x040008CA RID: 2250
	public static bool isConnectionFail;

	// Token: 0x040008CB RID: 2251
	public static bool isDisconnected;

	// Token: 0x040008CC RID: 2252
	public static bool isMain;

	// Token: 0x040008CD RID: 2253
	private float demCount;

	// Token: 0x040008CE RID: 2254
	private int move;

	// Token: 0x040008CF RID: 2255
	private int total;

	// Token: 0x040008D0 RID: 2256
	public static bool isStopReadMessage;
}
