using Assets.src.g;
using System;

namespace Assets.src.f
{
	internal class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				Res.outz("cmd=" + msg.command);
				switch (msg.command)
				{
				case 113:
				{
					int loop = msg.reader().readByte();
					int layer = msg.reader().readByte();
					int id2 = msg.reader().readUnsignedByte();
					short x = msg.reader().readShort();
					short y = msg.reader().readShort();
					short loopCount = msg.reader().readShort();
					EffecMn.addEff(new Effect(id2, x, y, layer, loop, loopCount));
					break;
				}
				case 48:
				{
					sbyte b28 = msg.reader().readByte();
					ServerListScreen.ipSelect = b28;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					break;
				}
				case 31:
				{
					int num32 = msg.reader().readInt();
					sbyte b32 = msg.reader().readByte();
					if (b32 == 1)
					{
						short smallID = msg.reader().readShort();
						sbyte b33 = -1;
						int[] array9 = null;
						short wimg = 0;
						short himg = 0;
						try
						{
							b33 = msg.reader().readByte();
							if (b33 > 0)
							{
								sbyte b34 = msg.reader().readByte();
								array9 = new int[b34];
								for (int num33 = 0; num33 < b34; num33++)
								{
									array9[num33] = msg.reader().readByte();
								}
								wimg = msg.reader().readShort();
								himg = msg.reader().readShort();
							}
						}
						catch (Exception)
						{
						}
						if (num32 == Char.myCharz().charID)
						{
							Char.myCharz().petFollow = new PetFollow();
							Char.myCharz().petFollow.smallID = smallID;
							if (b33 > 0)
							{
								Char.myCharz().petFollow.SetImg(b33, array9, wimg, himg);
							}
						}
						else
						{
							Char char4 = GameScr.findCharInMap(num32);
							char4.petFollow = new PetFollow();
							char4.petFollow.smallID = smallID;
							if (b33 > 0)
							{
								char4.petFollow.SetImg(b33, array9, wimg, himg);
							}
						}
					}
					else if (num32 == Char.myCharz().charID)
					{
						Char.myCharz().petFollow.remove();
						Char.myCharz().petFollow = null;
					}
					else
					{
						Char char5 = GameScr.findCharInMap(num32);
						char5.petFollow.remove();
						char5.petFollow = null;
					}
					break;
				}
				case -89:
					GameCanvas.open3Hour = ((msg.reader().readByte() == 1) ? true : false);
					break;
				case 42:
				{
					GameCanvas.endDlg();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					sbyte haveName = msg.reader().readByte();
					if (GameCanvas.registerScr == null)
					{
						GameCanvas.registerScr = new RegisterScreen(haveName);
					}
					GameCanvas.registerScr.switchToMe();
					break;
				}
				case 52:
				{
					sbyte b10 = msg.reader().readByte();
					if (b10 == 1)
					{
						int num11 = msg.reader().readInt();
						if (num11 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(m: true);
							Char.myCharz().cx = msg.reader().readShort();
							Char.myCharz().cy = msg.reader().readShort();
						}
						else
						{
							Char @char = GameScr.findCharInMap(num11);
							if (@char != null)
							{
								@char.setMabuHold(m: true);
								@char.cx = msg.reader().readShort();
								@char.cy = msg.reader().readShort();
							}
						}
					}
					if (b10 == 0)
					{
						int num12 = msg.reader().readInt();
						if (num12 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(m: false);
						}
						else
						{
							GameScr.findCharInMap(num12)?.setMabuHold(m: false);
						}
					}
					if (b10 == 2)
					{
						int charId = msg.reader().readInt();
						int id = msg.reader().readInt();
						Mabu mabu = (Mabu)GameScr.findCharInMap(charId);
						mabu.eat(id);
					}
					if (b10 == 3)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					break;
				}
				case 51:
				{
					int charId2 = msg.reader().readInt();
					Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId2);
					sbyte id4 = msg.reader().readByte();
					short x2 = msg.reader().readShort();
					short y2 = msg.reader().readShort();
					sbyte b31 = msg.reader().readByte();
					Char[] array7 = new Char[b31];
					int[] array8 = new int[b31];
					for (int num30 = 0; num30 < b31; num30++)
					{
						int num31 = msg.reader().readInt();
						Res.outz("char ID=" + num31);
						array7[num30] = null;
						if (num31 != Char.myCharz().charID)
						{
							array7[num30] = GameScr.findCharInMap(num31);
						}
						else
						{
							array7[num30] = Char.myCharz();
						}
						array8[num30] = msg.reader().readInt();
					}
					mabu2.setSkill(id4, x2, y2, array7, array8);
					break;
				}
				case -126:
				{
					sbyte b22 = msg.reader().readByte();
					Res.outz("type quay= " + b22);
					if (b22 == 1)
					{
						sbyte b23 = msg.reader().readByte();
						string num23 = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num23, finish);
					}
					if (b22 == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					break;
				}
				case -122:
				{
					short id3 = msg.reader().readShort();
					Npc npc = GameScr.findNPCInMap(id3);
					sbyte b30 = msg.reader().readByte();
					npc.duahau = new int[b30];
					Res.outz("N DUA HAU= " + b30);
					for (int num29 = 0; num29 < b30; num29++)
					{
						npc.duahau[num29] = msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case 102:
				{
					sbyte b19 = msg.reader().readByte();
					if (b19 == 0 || b19 == 1 || b19 == 2 || b19 == 6)
					{
						BigBoss2 bigBoss2 = Mob.getBigBoss2();
						if (bigBoss2 == null)
						{
							break;
						}
						if (b19 == 6)
						{
							bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							break;
						}
						sbyte b20 = msg.reader().readByte();
						Char[] array3 = new Char[b20];
						int[] array4 = new int[b20];
						for (int num19 = 0; num19 < b20; num19++)
						{
							int num20 = msg.reader().readInt();
							array3[num19] = null;
							if (num20 != Char.myCharz().charID)
							{
								array3[num19] = GameScr.findCharInMap(num20);
							}
							else
							{
								array3[num19] = Char.myCharz();
							}
							array4[num19] = msg.reader().readInt();
						}
						bigBoss2.setAttack(array3, array4, b19);
					}
					if (b19 == 3 || b19 == 4 || b19 == 5 || b19 == 7)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						if (bachTuoc != null)
						{
							if (b19 == 7)
							{
								bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							}
							else
							{
								if (b19 == 3 || b19 == 4)
								{
									sbyte b21 = msg.reader().readByte();
									Char[] array5 = new Char[b21];
									int[] array6 = new int[b21];
									for (int num21 = 0; num21 < b21; num21++)
									{
										int num22 = msg.reader().readInt();
										array5[num21] = null;
										if (num22 != Char.myCharz().charID)
										{
											array5[num21] = GameScr.findCharInMap(num22);
										}
										else
										{
											array5[num21] = Char.myCharz();
										}
										array6[num21] = msg.reader().readInt();
									}
									bachTuoc.setAttack(array5, array6, b19);
								}
								if (b19 == 5)
								{
									short xMoveTo = msg.reader().readShort();
									bachTuoc.move(xMoveTo);
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
						sbyte b17 = msg.reader().readByte();
						if (b17 == 0 || b17 == 1 || b17 == 2 || b17 == 4 || b17 == 3)
						{
							if (b17 == 3)
							{
								bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
								bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
								bigBoss.setFly();
							}
							else
							{
								sbyte b18 = msg.reader().readByte();
								Res.outz("CHUONG nChar= " + b18);
								Char[] array = new Char[b18];
								int[] array2 = new int[b18];
								for (int num17 = 0; num17 < b18; num17++)
								{
									int num18 = msg.reader().readInt();
									Res.outz("char ID=" + num18);
									array[num17] = null;
									if (num18 != Char.myCharz().charID)
									{
										array[num17] = GameScr.findCharInMap(num18);
									}
									else
									{
										array[num17] = Char.myCharz();
									}
									array2[num17] = msg.reader().readInt();
								}
								bigBoss.setAttack(array, array2, b17);
							}
						}
						if (b17 == 5)
						{
							bigBoss.haftBody = true;
							bigBoss.status = 2;
						}
						if (b17 == 6)
						{
							bigBoss.getDataB2();
							bigBoss.x = msg.reader().readShort();
							bigBoss.y = msg.reader().readShort();
						}
						if (b17 == 7)
						{
							bigBoss.setAttack(null, null, b17);
						}
						if (b17 == 8)
						{
							bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
							bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
							bigBoss.status = 2;
						}
						if (b17 == 9)
						{
							bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
						}
					}
					break;
				}
				case -120:
				{
					long num28 = mSystem.currentTimeMillis();
					Service.logController = num28 - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				}
				case -121:
				{
					long num10 = mSystem.currentTimeMillis();
					Service.logMap = num10 - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				}
				case 100:
				{
					sbyte b24 = msg.reader().readByte();
					sbyte b25 = msg.reader().readByte();
					Item item2 = null;
					if (b24 == 0)
					{
						item2 = Char.myCharz().arrItemBody[b25];
					}
					if (b24 == 1)
					{
						item2 = Char.myCharz().arrItemBag[b25];
					}
					short num25 = msg.reader().readShort();
					if (num25 != -1)
					{
						item2.template = ItemTemplates.get(num25);
						item2.quantity = msg.reader().readInt();
						item2.info = msg.reader().readUTF();
						item2.content = msg.reader().readUTF();
						sbyte b26 = msg.reader().readByte();
						if (b26 != 0)
						{
							item2.itemOption = new ItemOption[b26];
							for (int num26 = 0; num26 < item2.itemOption.Length; num26++)
							{
								sbyte b27 = msg.reader().readByte();
								Res.outz("id o= " + b27);
								int param3 = msg.reader().readUnsignedShort();
								if (b27 != -1)
								{
									item2.itemOption[num26] = new ItemOption(b27, param3);
								}
							}
						}
					}
					break;
				}
				case -123:
				{
					int charId3 = msg.reader().readInt();
					if (GameScr.findCharInMap(charId3) != null)
					{
						GameScr.findCharInMap(charId3).perCentMp = msg.reader().readByte();
					}
					break;
				}
				case -119:
					Char.myCharz().rank = msg.reader().readInt();
					break;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if (GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					break;
				case -116:
					GameScr.canAutoPlay = ((msg.reader().readByte() == 1) ? true : false);
					break;
				case -115:
					Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case -113:
				{
					sbyte[] array10 = new sbyte[5];
					for (int num38 = 0; num38 < 5; num38++)
					{
						array10[num38] = msg.reader().readByte();
						Res.outz("vlue i= " + array10[num38]);
					}
					GameScr.gI().onKSkill(array10);
					GameScr.gI().onOSkill(array10);
					GameScr.gI().onCSkill(array10);
					break;
				}
				case -111:
				{
					short num34 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num35 = 0; num35 < num34; num35++)
					{
						string iD = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(iD, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num42 = msg.reader().readInt();
					if (num42 == Char.myCharz().charID)
					{
						Char.myCharz().setFusion(fusion);
					}
					else if (GameScr.findCharInMap(num42) != null)
					{
						GameScr.findCharInMap(num42).setFusion(fusion);
					}
					break;
				}
				case 124:
				{
					short num24 = msg.reader().readShort();
					string text3 = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text3 + "npc ID= " + num24);
					GameScr.findNPCInMap(num24)?.addInfo(text3);
					break;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num27 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b29 = msg.reader().readByte();
					Char char3 = null;
					if (num27 == Char.myCharz().charID)
					{
						char3 = Char.myCharz();
					}
					else if (GameScr.findCharInMap(num27) != null)
					{
						char3 = GameScr.findCharInMap(num27);
					}
					if (char3 != null)
					{
						ServerEffect.addServerEffect((b29 != 0) ? 173 : 60, char3, 1);
						char3.setPos(xPos, yPos, b29);
					}
					break;
				}
				case 122:
				{
					short num3 = msg.reader().readShort();
					Res.outz("second login = " + num3);
					LoginScr.timeLogin = num3;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					break;
				}
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					break;
				case -124:
				{
					sbyte b6 = msg.reader().readByte();
					sbyte b7 = msg.reader().readByte();
					if (b7 == 0)
					{
						if (b6 == 2)
						{
							int num4 = msg.reader().readInt();
							if (num4 == Char.myCharz().charID)
							{
								Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeEffect();
							}
						}
						int num5 = msg.reader().readUnsignedByte();
						int num6 = msg.reader().readInt();
						if (num5 == 32)
						{
							if (b6 == 1)
							{
								int num7 = msg.reader().readInt();
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(Char.myCharz());
								}
								else if (GameScr.findCharInMap(num6) != null && num7 != Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(GameScr.findCharInMap(num6));
								}
								else if (GameScr.findCharInMap(num6) != null && num7 == Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									Char.myCharz().setHoldChar(GameScr.findCharInMap(num6));
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHoleEff();
							}
						}
						if (num5 == 33)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).protectEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeProtectEff();
							}
						}
						if (num5 == 39)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).huytSao = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHuytSao();
							}
						}
						if (num5 == 40)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).blindEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeBlindEff();
							}
						}
						if (num5 == 41)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).sleepEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeSleepEff();
							}
						}
						if (num5 == 42)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().stone = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().stone = false;
							}
						}
					}
					if (b7 == 1)
					{
						int num8 = msg.reader().readUnsignedByte();
						sbyte b8 = msg.reader().readByte();
						Res.outz("modbHoldID= " + b8 + " skillID= " + num8 + "eff ID= " + b6);
						if (num8 == 32)
						{
							if (b6 == 1)
							{
								int num9 = msg.reader().readInt();
								if (num9 == Char.myCharz().charID)
								{
									GameScr.findMobInMap(b8).holdEffID = num8;
									Char.myCharz().setHoldMob(GameScr.findMobInMap(b8));
								}
								else if (GameScr.findCharInMap(num9) != null)
								{
									GameScr.findMobInMap(b8).holdEffID = num8;
									GameScr.findCharInMap(num9).setHoldMob(GameScr.findMobInMap(b8));
								}
							}
							else
							{
								GameScr.findMobInMap(b8).removeHoldEff();
							}
						}
						if (num8 == 40)
						{
							if (b6 == 1)
							{
								GameScr.findMobInMap(b8).blindEff = true;
							}
							else
							{
								GameScr.findMobInMap(b8).removeBlindEff();
							}
						}
						if (num8 == 41)
						{
							if (b6 == 1)
							{
								GameScr.findMobInMap(b8).sleepEff = true;
							}
							else
							{
								GameScr.findMobInMap(b8).removeSleepEff();
							}
						}
					}
					break;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text = msg.reader().readUTF();
					Res.outz("titile= " + text);
					sbyte b4 = msg.reader().readByte();
					ClientInput.gI().setInput(b4, text);
					for (int k = 0; k < b4; k++)
					{
						ClientInput.gI().tf[k].name = msg.reader().readUTF();
						sbyte b5 = msg.reader().readByte();
						if (b5 == 0)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if (b5 == 1)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_ANY);
						}
						if (b5 == 2)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					break;
				}
				case -110:
				{
					sbyte b35 = msg.reader().readByte();
					if (b35 == 1)
					{
						int num39 = msg.reader().readInt();
						sbyte[] array11 = Rms.loadRMS(num39 + string.Empty);
						if (array11 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num39, array11);
						}
					}
					if (b35 == 0)
					{
						int num40 = msg.reader().readInt();
						short num41 = msg.reader().readShort();
						sbyte[] data = new sbyte[num41];
						msg.reader().read(ref data, 0, num41);
						Rms.saveRMS(num40 + string.Empty, data);
					}
					break;
				}
				case 93:
				{
					string str = msg.reader().readUTF();
					str = Res.changeString(str);
					GameScr.gI().chatVip(str);
					break;
				}
				case -106:
				{
					short num36 = msg.reader().readShort();
					int num37 = msg.reader().readShort();
					if (ItemTime.isExistItem(num36))
					{
						ItemTime.getItemById(num36).initTime(num37);
					}
					else
					{
						ItemTime o = new ItemTime(num36, num37);
						Char.vItemTime.addElement(o);
					}
					break;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case -103:
				{
					sbyte b11 = msg.reader().readByte();
					if (b11 == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b12 = msg.reader().readByte();
						for (int l = 0; l < b12; l++)
						{
							Item item = new Item();
							short num13 = msg.reader().readShort();
							if (num13 != -1)
							{
								item.template = ItemTemplates.get(num13);
								sbyte b13 = msg.reader().readByte();
								if (b13 != -1)
								{
									item.itemOption = new ItemOption[b13];
									for (int m = 0; m < item.itemOption.Length; m++)
									{
										sbyte b14 = msg.reader().readByte();
										int param2 = msg.reader().readUnsignedShort();
										if (b14 != -1)
										{
											item.itemOption[m] = new ItemOption(b14, param2);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if (b11 == 1)
					{
						int num14 = msg.reader().readInt();
						sbyte b15 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num14 + " : " + b15);
						if (num14 == Char.myCharz().charID)
						{
							Char.myCharz().cFlag = b15;
						}
						else if (GameScr.findCharInMap(num14) != null)
						{
							GameScr.findCharInMap(num14).cFlag = b15;
						}
						GameScr.gI().getFlagImage(num14, b15);
					}
					else if (b11 == 2)
					{
						sbyte b16 = msg.reader().readByte();
						int num15 = msg.reader().readShort();
						PKFlag pKFlag = new PKFlag();
						pKFlag.cflag = b16;
						pKFlag.IDimageFlag = num15;
						GameScr.vFlag.addElement(pKFlag);
						for (int n = 0; n < GameScr.vFlag.size(); n++)
						{
							PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(n);
							Res.outz("i: " + n + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
						}
						for (int num16 = 0; num16 < GameScr.vCharInMap.size(); num16++)
						{
							Char char2 = (Char)GameScr.vCharInMap.elementAt(num16);
							if (char2 != null && char2.cFlag == b16)
							{
								char2.flagImage = num15;
							}
						}
						if (Char.myCharz().cFlag == b16)
						{
							Char.myCharz().flagImage = num15;
						}
					}
					break;
				}
				case -102:
				{
					sbyte b9 = msg.reader().readByte();
					if (b9 != 0 && b9 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					break;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text2 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text2);
					Service.gI().setClientType();
					Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b = msg.reader().readByte();
					Res.outz("t Indxe= " + b);
					GameCanvas.panel.maxPageShop[b] = msg.reader().readByte();
					GameCanvas.panel.currPageShop[b] = msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[b] + " curr page= " + GameCanvas.panel.currPageShop[b]);
					int num = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[b] = new Item[num];
					for (int i = 0; i < num; i++)
					{
						short num2 = msg.reader().readShort();
						if (num2 != -1)
						{
							Res.outz("template id= " + num2);
							Char.myCharz().arrItemShop[b][i] = new Item();
							Char.myCharz().arrItemShop[b][i].template = ItemTemplates.get(num2);
							Char.myCharz().arrItemShop[b][i].itemId = msg.reader().readShort();
							Char.myCharz().arrItemShop[b][i].buyCoin = msg.reader().readInt();
							Char.myCharz().arrItemShop[b][i].buyGold = msg.reader().readInt();
							Char.myCharz().arrItemShop[b][i].buyType = msg.reader().readByte();
							Char.myCharz().arrItemShop[b][i].quantity = msg.reader().readByte();
							Char.myCharz().arrItemShop[b][i].isMe = msg.reader().readByte();
							Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							sbyte b2 = msg.reader().readByte();
							if (b2 != -1)
							{
								Char.myCharz().arrItemShop[b][i].itemOption = new ItemOption[b2];
								for (int j = 0; j < Char.myCharz().arrItemShop[b][i].itemOption.Length; j++)
								{
									sbyte b3 = msg.reader().readByte();
									int param = msg.reader().readUnsignedShort();
									if (b3 != -1)
									{
										Char.myCharz().arrItemShop[b][i].itemOption[j] = new ItemOption(b3, param);
										Char.myCharz().arrItemShop[b][i].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b][i]);
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
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
