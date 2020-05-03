using System;
using Assets.src.g;

namespace Assets.src.f
{
	// Token: 0x0200008E RID: 142
	internal class Controller2
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00033C94 File Offset: 0x00031E94
		public static void readMessage(Message msg)
		{
			try
			{
				Res.outz("cmd=" + msg.command);
				sbyte command = msg.command;
				switch (command + 126)
				{
				case 0:
				{
					sbyte b = msg.reader().readByte();
					Res.outz("type quay= " + b);
					if ((int)b == 1)
					{
						sbyte b2 = msg.reader().readByte();
						string num = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num, finish);
					}
					if ((int)b == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					break;
				}
				case 1:
				{
					ChatTextField.gI().isShow = false;
					string text = msg.reader().readUTF();
					Res.outz("titile= " + text);
					sbyte b3 = msg.reader().readByte();
					ClientInput.gI().setInput((int)b3, text);
					for (int i = 0; i < (int)b3; i++)
					{
						ClientInput.gI().tf[i].name = msg.reader().readUTF();
						sbyte b4 = msg.reader().readByte();
						if ((int)b4 == 0)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if ((int)b4 == 1)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_ANY);
						}
						if ((int)b4 == 2)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					break;
				}
				case 2:
				{
					sbyte b5 = msg.reader().readByte();
					sbyte b6 = msg.reader().readByte();
					if ((int)b6 == 0)
					{
						if ((int)b5 == 2)
						{
							int num2 = msg.reader().readInt();
							if (num2 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num2) != null)
							{
								GameScr.findCharInMap(num2).removeEffect();
							}
						}
						int num3 = (int)msg.reader().readUnsignedByte();
						int num4 = msg.reader().readInt();
						if (num3 == 32)
						{
							if ((int)b5 == 1)
							{
								int num5 = msg.reader().readInt();
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().holdEffID = num3;
									GameScr.findCharInMap(num5).setHoldChar(global::Char.myCharz());
								}
								else if (GameScr.findCharInMap(num4) != null && num5 != global::Char.myCharz().charID)
								{
									GameScr.findCharInMap(num4).holdEffID = num3;
									GameScr.findCharInMap(num5).setHoldChar(GameScr.findCharInMap(num4));
								}
								else if (GameScr.findCharInMap(num4) != null && num5 == global::Char.myCharz().charID)
								{
									GameScr.findCharInMap(num4).holdEffID = num3;
									global::Char.myCharz().setHoldChar(GameScr.findCharInMap(num4));
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeHoleEff();
							}
						}
						if (num3 == 33)
						{
							if ((int)b5 == 1)
							{
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num4) != null)
								{
									GameScr.findCharInMap(num4).protectEff = true;
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeProtectEff();
							}
						}
						if (num3 == 39)
						{
							if ((int)b5 == 1)
							{
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num4) != null)
								{
									GameScr.findCharInMap(num4).huytSao = true;
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeHuytSao();
							}
						}
						if (num3 == 40)
						{
							if ((int)b5 == 1)
							{
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num4) != null)
								{
									GameScr.findCharInMap(num4).blindEff = true;
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeBlindEff();
							}
						}
						if (num3 == 41)
						{
							if ((int)b5 == 1)
							{
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num4) != null)
								{
									GameScr.findCharInMap(num4).sleepEff = true;
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeSleepEff();
							}
						}
						if (num3 == 42)
						{
							if ((int)b5 == 1)
							{
								if (num4 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().stone = true;
								}
							}
							else if (num4 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().stone = false;
							}
						}
					}
					if ((int)b6 == 1)
					{
						int num6 = (int)msg.reader().readUnsignedByte();
						sbyte b7 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"modbHoldID= ",
							b7,
							" skillID= ",
							num6,
							"eff ID= ",
							b5
						}));
						if (num6 == 32)
						{
							if ((int)b5 == 1)
							{
								int num7 = msg.reader().readInt();
								if (num7 == global::Char.myCharz().charID)
								{
									GameScr.findMobInMap(b7).holdEffID = num6;
									global::Char.myCharz().setHoldMob(GameScr.findMobInMap(b7));
								}
								else if (GameScr.findCharInMap(num7) != null)
								{
									GameScr.findMobInMap(b7).holdEffID = num6;
									GameScr.findCharInMap(num7).setHoldMob(GameScr.findMobInMap(b7));
								}
							}
							else
							{
								GameScr.findMobInMap(b7).removeHoldEff();
							}
						}
						if (num6 == 40)
						{
							if ((int)b5 == 1)
							{
								GameScr.findMobInMap(b7).blindEff = true;
							}
							else
							{
								GameScr.findMobInMap(b7).removeBlindEff();
							}
						}
						if (num6 == 41)
						{
							if ((int)b5 == 1)
							{
								GameScr.findMobInMap(b7).sleepEff = true;
							}
							else
							{
								GameScr.findMobInMap(b7).removeSleepEff();
							}
						}
					}
					break;
				}
				case 3:
				{
					int charId = msg.reader().readInt();
					if (GameScr.findCharInMap(charId) != null)
					{
						GameScr.findCharInMap(charId).perCentMp = (int)msg.reader().readByte();
					}
					break;
				}
				case 4:
				{
					short id = msg.reader().readShort();
					Npc npc = GameScr.findNPCInMap(id);
					sbyte b8 = msg.reader().readByte();
					npc.duahau = new int[(int)b8];
					Res.outz("N DUA HAU= " + b8);
					for (int j = 0; j < (int)b8; j++)
					{
						npc.duahau[j] = (int)msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case 5:
				{
					long num8 = mSystem.currentTimeMillis();
					Service.logMap = num8 - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				}
				case 6:
				{
					long num9 = mSystem.currentTimeMillis();
					Service.logController = num9 - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				}
				case 7:
					global::Char.myCharz().rank = msg.reader().readInt();
					break;
				default:
					switch (command)
					{
					case 48:
					{
						sbyte b9 = msg.reader().readByte();
						ServerListScreen.ipSelect = 6;
						GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
						Session_ME.gI().close();
						GameCanvas.endDlg();
						ServerListScreen.waitToLogin = true;
						break;
					}
					default:
						switch (command)
						{
						case 121:
							mSystem.publicID = msg.reader().readUTF();
							mSystem.strAdmob = msg.reader().readUTF();
							Res.outz("SHOW AD public ID= " + mSystem.publicID);
							mSystem.createAdmob();
							break;
						case 122:
						{
							short num10 = msg.reader().readShort();
							Res.outz("second login = " + num10);
							LoginScr.timeLogin = num10;
							LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
							GameCanvas.endDlg();
							break;
						}
						case 123:
						{
							Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
							int num11 = msg.reader().readInt();
							short xPos = msg.reader().readShort();
							short yPos = msg.reader().readShort();
							sbyte b10 = msg.reader().readByte();
							global::Char @char = null;
							if (num11 == global::Char.myCharz().charID)
							{
								@char = global::Char.myCharz();
							}
							else if (GameScr.findCharInMap(num11) != null)
							{
								@char = GameScr.findCharInMap(num11);
							}
							if (@char != null)
							{
								ServerEffect.addServerEffect(((int)b10 != 0) ? 173 : 60, @char, 1);
								@char.setPos(xPos, yPos, b10);
							}
							break;
						}
						case 124:
						{
							short num12 = msg.reader().readShort();
							string text2 = msg.reader().readUTF();
							Res.outz(string.Concat(new object[]
							{
								"noi chuyen = ",
								text2,
								"npc ID= ",
								num12
							}));
							Npc npc2 = GameScr.findNPCInMap(num12);
							if (npc2 != null)
							{
								npc2.addInfo(text2);
							}
							break;
						}
						case 125:
						{
							sbyte fusion = msg.reader().readByte();
							int num13 = msg.reader().readInt();
							if (num13 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().setFusion(fusion);
							}
							else if (GameScr.findCharInMap(num13) != null)
							{
								GameScr.findCharInMap(num13).setFusion(fusion);
							}
							break;
						}
						default:
							switch (command)
							{
							case 100:
							{
								sbyte b11 = msg.reader().readByte();
								sbyte b12 = msg.reader().readByte();
								Item item = null;
								if ((int)b11 == 0)
								{
									item = global::Char.myCharz().arrItemBody[(int)b12];
								}
								if ((int)b11 == 1)
								{
									item = global::Char.myCharz().arrItemBag[(int)b12];
								}
								short num14 = msg.reader().readShort();
								if (num14 != -1)
								{
									item.template = ItemTemplates.get(num14);
									item.quantity = msg.reader().readInt();
									item.info = msg.reader().readUTF();
									item.content = msg.reader().readUTF();
									sbyte b13 = msg.reader().readByte();
									if ((int)b13 != 0)
									{
										item.itemOption = new ItemOption[(int)b13];
										for (int k = 0; k < item.itemOption.Length; k++)
										{
											sbyte b14 = msg.reader().readByte();
											Res.outz("id o= " + b14);
											int param = (int)msg.reader().readUnsignedShort();
											if ((int)b14 != -1)
											{
												item.itemOption[k] = new ItemOption((int)b14, param);
											}
										}
									}
								}
								break;
							}
							case 101:
							{
								Res.outz("big boss--------------------------------------------------");
								BigBoss bigBoss = Mob.getBigBoss();
								if (bigBoss != null)
								{
									sbyte b15 = msg.reader().readByte();
									if ((int)b15 == 0 || (int)b15 == 1 || (int)b15 == 2 || (int)b15 == 4 || (int)b15 == 3)
									{
										if ((int)b15 == 3)
										{
											bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
											bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
											bigBoss.setFly();
										}
										else
										{
											sbyte b16 = msg.reader().readByte();
											Res.outz("CHUONG nChar= " + b16);
											global::Char[] array = new global::Char[(int)b16];
											int[] array2 = new int[(int)b16];
											for (int l = 0; l < (int)b16; l++)
											{
												int num15 = msg.reader().readInt();
												Res.outz("char ID=" + num15);
												array[l] = null;
												if (num15 != global::Char.myCharz().charID)
												{
													array[l] = GameScr.findCharInMap(num15);
												}
												else
												{
													array[l] = global::Char.myCharz();
												}
												array2[l] = msg.reader().readInt();
											}
											bigBoss.setAttack(array, array2, b15);
										}
									}
									if ((int)b15 == 5)
									{
										bigBoss.haftBody = true;
										bigBoss.status = 2;
									}
									if ((int)b15 == 6)
									{
										bigBoss.getDataB2();
										bigBoss.x = (int)msg.reader().readShort();
										bigBoss.y = (int)msg.reader().readShort();
									}
									if ((int)b15 == 7)
									{
										bigBoss.setAttack(null, null, b15);
									}
									if ((int)b15 == 8)
									{
										bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
										bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
										bigBoss.status = 2;
									}
									if ((int)b15 == 9)
									{
										bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
									}
								}
								break;
							}
							case 102:
							{
								sbyte b17 = msg.reader().readByte();
								if ((int)b17 == 0 || (int)b17 == 1 || (int)b17 == 2 || (int)b17 == 6)
								{
									BigBoss2 bigBoss2 = Mob.getBigBoss2();
									if (bigBoss2 == null)
									{
										break;
									}
									if ((int)b17 == 6)
									{
										bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
										break;
									}
									sbyte b18 = msg.reader().readByte();
									global::Char[] array3 = new global::Char[(int)b18];
									int[] array4 = new int[(int)b18];
									for (int m = 0; m < (int)b18; m++)
									{
										int num16 = msg.reader().readInt();
										array3[m] = null;
										if (num16 != global::Char.myCharz().charID)
										{
											array3[m] = GameScr.findCharInMap(num16);
										}
										else
										{
											array3[m] = global::Char.myCharz();
										}
										array4[m] = msg.reader().readInt();
									}
									bigBoss2.setAttack(array3, array4, b17);
								}
								if ((int)b17 == 3 || (int)b17 == 4 || (int)b17 == 5 || (int)b17 == 7)
								{
									BachTuoc bachTuoc = Mob.getBachTuoc();
									if (bachTuoc != null)
									{
										if ((int)b17 == 7)
										{
											bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
										}
										else
										{
											if ((int)b17 == 3 || (int)b17 == 4)
											{
												sbyte b19 = msg.reader().readByte();
												global::Char[] array5 = new global::Char[(int)b19];
												int[] array6 = new int[(int)b19];
												for (int n = 0; n < (int)b19; n++)
												{
													int num17 = msg.reader().readInt();
													array5[n] = null;
													if (num17 != global::Char.myCharz().charID)
													{
														array5[n] = GameScr.findCharInMap(num17);
													}
													else
													{
														array5[n] = global::Char.myCharz();
													}
													array6[n] = msg.reader().readInt();
												}
												bachTuoc.setAttack(array5, array6, b17);
											}
											if ((int)b17 == 5)
											{
												short xMoveTo = msg.reader().readShort();
												bachTuoc.move(xMoveTo);
											}
										}
									}
								}
								break;
							}
							default:
								if (command != 31)
								{
									if (command != 42)
									{
										if (command != 93)
										{
											if (command == 113)
											{
												int loop = (int)msg.reader().readByte();
												int layer = (int)msg.reader().readByte();
												int id2 = (int)msg.reader().readUnsignedByte();
												short x = msg.reader().readShort();
												short y = msg.reader().readShort();
												EffecMn.addEff(new Effect(id2, (int)x, (int)y, layer, loop, -1));
											}
										}
										else
										{
											string text3 = msg.reader().readUTF();
											text3 = Res.changeString(text3);
											GameScr.gI().chatVip(text3);
										}
									}
									else
									{
										GameCanvas.endDlg();
										LoginScr.isContinueToLogin = false;
										global::Char.isLoadingMap = false;
										sbyte haveName = msg.reader().readByte();
										if (GameCanvas.registerScr == null)
										{
											GameCanvas.registerScr = new RegisterScreen(haveName);
										}
										GameCanvas.registerScr.switchToMe();
									}
								}
								else
								{
									int num18 = msg.reader().readInt();
									sbyte b20 = msg.reader().readByte();
									if ((int)b20 == 1)
									{
										if (num18 == global::Char.myCharz().charID)
										{
											global::Char.myCharz().petFollow = new PetFollow();
											global::Char.myCharz().petFollow.smallID = msg.reader().readShort();
										}
										else
										{
											global::Char char2 = GameScr.findCharInMap(num18);
											char2.petFollow = new PetFollow();
											char2.petFollow.smallID = msg.reader().readShort();
										}
									}
									else if (num18 == global::Char.myCharz().charID)
									{
										global::Char.myCharz().petFollow.remove();
										global::Char.myCharz().petFollow = null;
									}
									else
									{
										global::Char char3 = GameScr.findCharInMap(num18);
										char3.petFollow.remove();
										char3.petFollow = null;
									}
								}
								break;
							}
							break;
						}
						break;
					case 51:
					{
						int charId2 = msg.reader().readInt();
						Mabu mabu = (Mabu)GameScr.findCharInMap(charId2);
						sbyte id3 = msg.reader().readByte();
						short x2 = msg.reader().readShort();
						short y2 = msg.reader().readShort();
						sbyte b21 = msg.reader().readByte();
						global::Char[] array7 = new global::Char[(int)b21];
						int[] array8 = new int[(int)b21];
						for (int num19 = 0; num19 < (int)b21; num19++)
						{
							int num20 = msg.reader().readInt();
							Res.outz("char ID=" + num20);
							array7[num19] = null;
							if (num20 != global::Char.myCharz().charID)
							{
								array7[num19] = GameScr.findCharInMap(num20);
							}
							else
							{
								array7[num19] = global::Char.myCharz();
							}
							array8[num19] = msg.reader().readInt();
						}
						mabu.setSkill(id3, x2, y2, array7, array8);
						break;
					}
					case 52:
					{
						sbyte b22 = msg.reader().readByte();
						if ((int)b22 == 1)
						{
							int num21 = msg.reader().readInt();
							if (num21 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().setMabuHold(true);
								global::Char.myCharz().cx = (int)msg.reader().readShort();
								global::Char.myCharz().cy = (int)msg.reader().readShort();
							}
							else
							{
								global::Char char4 = GameScr.findCharInMap(num21);
								if (char4 != null)
								{
									char4.setMabuHold(true);
									char4.cx = (int)msg.reader().readShort();
									char4.cy = (int)msg.reader().readShort();
								}
							}
						}
						if ((int)b22 == 0)
						{
							int num22 = msg.reader().readInt();
							if (num22 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().setMabuHold(false);
							}
							else
							{
								global::Char char5 = GameScr.findCharInMap(num22);
								if (char5 != null)
								{
									char5.setMabuHold(false);
								}
							}
						}
						if ((int)b22 == 2)
						{
							int charId3 = msg.reader().readInt();
							int id4 = msg.reader().readInt();
							Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId3);
							mabu2.eat(id4);
						}
						if ((int)b22 == 3)
						{
							GameScr.mabuPercent = msg.reader().readByte();
						}
						break;
					}
					}
					break;
				case 9:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if ((int)GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if ((int)GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					break;
				case 10:
					GameScr.canAutoPlay = ((int)msg.reader().readByte() == 1);
					break;
				case 11:
					global::Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case 13:
				{
					sbyte[] array9 = new sbyte[5];
					for (int num23 = 0; num23 < 5; num23++)
					{
						array9[num23] = msg.reader().readByte();
						Res.outz("vlue i= " + array9[num23]);
					}
					GameScr.gI().onKSkill(array9);
					GameScr.gI().onOSkill(array9);
					GameScr.gI().onCSkill(array9);
					break;
				}
				case 15:
				{
					short num24 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num25 = 0; num25 < (int)num24; num25++)
					{
						string id5 = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(id5, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case 16:
				{
					sbyte b23 = msg.reader().readByte();
					if ((int)b23 == 1)
					{
						int num26 = msg.reader().readInt();
						sbyte[] array10 = Rms.loadRMS(num26 + string.Empty);
						if (array10 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num26, array10);
						}
					}
					if ((int)b23 == 0)
					{
						int num27 = msg.reader().readInt();
						short num28 = msg.reader().readShort();
						sbyte[] data = new sbyte[(int)num28];
						msg.reader().read(ref data, 0, (int)num28);
						Rms.saveRMS(num27 + string.Empty, data);
					}
					break;
				}
				case 20:
				{
					short num29 = msg.reader().readShort();
					int num30 = (int)msg.reader().readShort();
					if (ItemTime.isExistItem((int)num29))
					{
						ItemTime.getItemById((int)num29).initTime(num30);
					}
					else
					{
						ItemTime o = new ItemTime(num29, num30);
						global::Char.vItemTime.addElement(o);
					}
					break;
				}
				case 21:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case 23:
				{
					sbyte b24 = msg.reader().readByte();
					if ((int)b24 == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b25 = msg.reader().readByte();
						for (int num31 = 0; num31 < (int)b25; num31++)
						{
							Item item2 = new Item();
							short num32 = msg.reader().readShort();
							if (num32 != -1)
							{
								item2.template = ItemTemplates.get(num32);
								sbyte b26 = msg.reader().readByte();
								if ((int)b26 != -1)
								{
									item2.itemOption = new ItemOption[(int)b26];
									for (int num33 = 0; num33 < item2.itemOption.Length; num33++)
									{
										sbyte b27 = msg.reader().readByte();
										int param2 = (int)msg.reader().readUnsignedShort();
										if ((int)b27 != -1)
										{
											item2.itemOption[num33] = new ItemOption((int)b27, param2);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item2);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if ((int)b24 == 1)
					{
						int num34 = msg.reader().readInt();
						sbyte b28 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"---------------actionFlag1:  ",
							num34,
							" : ",
							b28
						}));
						if (num34 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().cFlag = b28;
						}
						else if (GameScr.findCharInMap(num34) != null)
						{
							GameScr.findCharInMap(num34).cFlag = b28;
						}
						GameScr.gI().getFlagImage(num34, b28);
					}
					else if ((int)b24 == 2)
					{
						sbyte b29 = msg.reader().readByte();
						int num35 = (int)msg.reader().readShort();
						PKFlag pkflag = new PKFlag();
						pkflag.cflag = b29;
						pkflag.IDimageFlag = num35;
						GameScr.vFlag.addElement(pkflag);
						for (int num36 = 0; num36 < GameScr.vFlag.size(); num36++)
						{
							PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(num36);
							Res.outz(string.Concat(new object[]
							{
								"i: ",
								num36,
								"  cflag: ",
								pkflag2.cflag,
								"   IDimageFlag: ",
								pkflag2.IDimageFlag
							}));
						}
						for (int num37 = 0; num37 < GameScr.vCharInMap.size(); num37++)
						{
							global::Char char6 = (global::Char)GameScr.vCharInMap.elementAt(num37);
							if (char6 != null && (int)char6.cFlag == (int)b29)
							{
								char6.flagImage = num35;
							}
						}
						if ((int)global::Char.myCharz().cFlag == (int)b29)
						{
							global::Char.myCharz().flagImage = num35;
						}
					}
					break;
				}
				case 24:
				{
					sbyte b30 = msg.reader().readByte();
					if ((int)b30 != 0)
					{
						if ((int)b30 == 1)
						{
							GameCanvas.loginScr.isLogin2 = false;
							Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
							LoginScr.isLoggingIn = true;
						}
					}
					break;
				}
				case 25:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text4 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text4);
					Service.gI().setClientType();
					Service.gI().login(text4, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case 26:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b31 = msg.reader().readByte();
					Res.outz("t Indxe= " + b31);
					GameCanvas.panel.maxPageShop[(int)b31] = (int)msg.reader().readByte();
					GameCanvas.panel.currPageShop[(int)b31] = (int)msg.reader().readByte();
					Res.outz(string.Concat(new object[]
					{
						"max page= ",
						GameCanvas.panel.maxPageShop[(int)b31],
						" curr page= ",
						GameCanvas.panel.currPageShop[(int)b31]
					}));
					int num38 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[(int)b31] = new Item[num38];
					for (int num39 = 0; num39 < num38; num39++)
					{
						short num40 = msg.reader().readShort();
						if (num40 != -1)
						{
							Res.outz("template id= " + num40);
							global::Char.myCharz().arrItemShop[(int)b31][num39] = new Item();
							global::Char.myCharz().arrItemShop[(int)b31][num39].template = ItemTemplates.get(num40);
							global::Char.myCharz().arrItemShop[(int)b31][num39].itemId = (int)msg.reader().readShort();
							global::Char.myCharz().arrItemShop[(int)b31][num39].buyCoin = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b31][num39].buyGold = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b31][num39].buyType = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[(int)b31][num39].quantity = (int)msg.reader().readByte();
							global::Char.myCharz().arrItemShop[(int)b31][num39].isMe = msg.reader().readByte();
							Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							sbyte b32 = msg.reader().readByte();
							if ((int)b32 != -1)
							{
								global::Char.myCharz().arrItemShop[(int)b31][num39].itemOption = new ItemOption[(int)b32];
								for (int num41 = 0; num41 < global::Char.myCharz().arrItemShop[(int)b31][num39].itemOption.Length; num41++)
								{
									sbyte b33 = msg.reader().readByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									if ((int)b33 != -1)
									{
										global::Char.myCharz().arrItemShop[(int)b31][num39].itemOption[num41] = new ItemOption((int)b33, param3);
										global::Char.myCharz().arrItemShop[(int)b31][num39].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[(int)b31][num39]);
									}
								}
							}
						}
					}
					if (flag)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					break;
				}
				case 37:
					GameCanvas.open3Hour = ((int)msg.reader().readByte() == 1);
					break;
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}
