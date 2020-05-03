using System;

// Token: 0x02000054 RID: 84
public class Hint
{
	// Token: 0x060002CE RID: 718 RVA: 0x00004B9F File Offset: 0x00002D9F
	public static bool isOnTask(int tastId, int index)
	{
		return global::Char.myCharz().taskMaint != null && (int)global::Char.myCharz().taskMaint.taskId == tastId && global::Char.myCharz().taskMaint.index == index;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00015F80 File Offset: 0x00014180
	public static bool isPaintz()
	{
		return (!Hint.isOnTask(0, 3) || GameCanvas.panel.currentTabIndex != 0 || (GameCanvas.panel.cmy >= 0 && GameCanvas.panel.cmy <= 30)) && (!Hint.isOnTask(2, 0) || !GameCanvas.panel.isShow || GameCanvas.panel.currentTabIndex == 0);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00015FF8 File Offset: 0x000141F8
	public static void clickNpc()
	{
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		if (GameScr.getNpcTask() != null)
		{
			Hint.x = GameScr.getNpcTask().cx;
			Hint.y = GameScr.getNpcTask().cy;
			Hint.trans = 0;
			Hint.isCamera = true;
			Hint.type = ((!GameCanvas.isTouch) ? 0 : 1);
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00016064 File Offset: 0x00014264
	public static void nextMap(int index)
	{
		if (GameCanvas.panel.isShow)
		{
			return;
		}
		if (PopUp.vPopups.size() - 1 < index)
		{
			return;
		}
		PopUp popUp = (PopUp)PopUp.vPopups.elementAt(index);
		Hint.x = popUp.cx + popUp.sayWidth / 2;
		Hint.y = popUp.cy + 30;
		if (popUp.isHide || !popUp.isPaint)
		{
			Hint.isPaint = false;
		}
		else
		{
			Hint.isPaint = true;
		}
		Hint.type = 0;
		Hint.isCamera = true;
		Hint.trans = 0;
		if (!GameCanvas.isTouch)
		{
			Hint.isPaint = false;
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00016110 File Offset: 0x00014310
	public static void clickMob()
	{
		Hint.type = 1;
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		bool flag = false;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.isHintFocus)
			{
				flag = true;
				break;
			}
		}
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
			if (mob2.isHintFocus)
			{
				Hint.x = mob2.x;
				Hint.y = mob2.y + 5;
				Hint.isCamera = true;
				if (mob2.status == 0)
				{
					mob2.isHintFocus = false;
				}
				break;
			}
			if (!flag)
			{
				if (mob2.status != 0)
				{
					mob2.isHintFocus = true;
					break;
				}
				mob2.isHintFocus = false;
			}
		}
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00016218 File Offset: 0x00014418
	public static bool isHaveItem()
	{
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			if (itemMap.playerId == global::Char.myCharz().charID && itemMap.template.id == 73)
			{
				Hint.type = 1;
				Hint.x = itemMap.x;
				Hint.y = itemMap.y + 5;
				Hint.isCamera = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x000162B4 File Offset: 0x000144B4
	public static void paintArrowPointToHint(mGraphics g)
	{
		try
		{
			if (Hint.isPaintArrow)
			{
				if (Hint.x <= GameScr.cmx || Hint.x >= GameScr.cmx + GameScr.gW || Hint.y <= GameScr.cmy || Hint.y >= GameScr.cmy + GameScr.gH)
				{
					if (GameCanvas.gameTick % 10 >= 5)
					{
						if (ChatPopup.currChatPopup == null)
						{
							if (ChatPopup.serverChatPopUp == null)
							{
								if (!GameCanvas.panel.isShow)
								{
									if (Hint.isCamera)
									{
										int num = Hint.x - global::Char.myCharz().cx;
										int num2 = Hint.y - global::Char.myCharz().cy;
										int num3 = 0;
										int num4 = 0;
										int arg = 0;
										if (num > 0 && num2 >= 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = GameScr.gW - 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 0;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else if (num >= 0 && num2 < 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = GameScr.gW - 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 0;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = 10;
												arg = 6;
											}
										}
										if (num < 0 && num2 >= 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 3;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else if (num <= 0 && num2 < 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 3;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = 10;
												arg = 6;
											}
										}
										GameScr.resetTranslate(g);
										g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, num3, num4, StaticObj.VCENTER_HCENTER);
									}
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00016564 File Offset: 0x00014764
	public static void paint(mGraphics g)
	{
		if (ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (global::Char.myCharz().isUsePlane || global::Char.myCharz().isTeleport)
		{
			return;
		}
		Hint.paintArrowPointToHint(g);
		if (GameCanvas.menu.tDelay != 0)
		{
			return;
		}
		if (!Hint.isPaint)
		{
			return;
		}
		if (ChatPopup.scr != null)
		{
			return;
		}
		if (global::Char.ischangingMap)
		{
			return;
		}
		if (GameCanvas.currentScreen != GameScr.gI())
		{
			return;
		}
		if (GameCanvas.panel.isShow && GameCanvas.panel.cmx != 0)
		{
			return;
		}
		if (Hint.isCamera)
		{
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}
		if (Hint.trans == 0)
		{
			g.drawImage(Panel.imgBantay, Hint.x - 15, Hint.y, 0);
		}
		if (Hint.trans == 1)
		{
			g.drawRegion(Panel.imgBantay, 0, 0, 14, 16, 2, Hint.x + 15, Hint.y, StaticObj.TOP_RIGHT);
		}
		if (Hint.paintFlare)
		{
			g.drawImage(ItemMap.imageFlare, Hint.x, Hint.y, 3);
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00016690 File Offset: 0x00014890
	public static void hint()
	{
		if (global::Char.myCharz().taskMaint != null && GameCanvas.currentScreen == GameScr.instance)
		{
			int taskId = (int)global::Char.myCharz().taskMaint.taskId;
			int index = global::Char.myCharz().taskMaint.index;
			Hint.isCamera = false;
			Hint.trans = 0;
			Hint.type = 0;
			Hint.isPaint = true;
			Hint.isPaintArrow = true;
			if (GameCanvas.menu.showMenu && taskId > 0)
			{
				Hint.isPaint = false;
			}
			switch (taskId)
			{
			case 0:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0 && TileMap.vGo.size() != 0)
					{
						Hint.x = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minX - 100);
						Hint.y = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minY + 40);
						Hint.isCamera = true;
					}
					if (index == 1)
					{
						Hint.nextMap(0);
					}
					if (index == 2)
					{
						Hint.clickNpc();
					}
					if (index == 3)
					{
						if (!GameCanvas.panel.isShow)
						{
							Hint.clickNpc();
						}
						else if (GameCanvas.panel.currentTabIndex == 0)
						{
							if (GameCanvas.panel.cp == null)
							{
								Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
								Hint.y = GameCanvas.panel.yScroll + 20;
							}
							else if (GameCanvas.menu.tDelay != 0)
							{
								Hint.x = GameCanvas.panel.xScroll + 25;
								Hint.y = GameCanvas.panel.yScroll + 60;
							}
						}
						else if (GameCanvas.panel.currentTabIndex == 1)
						{
							Hint.x = GameCanvas.panel.startTabPos + 10;
							Hint.y = 65;
						}
					}
					if (index == 4)
					{
						if (GameCanvas.panel.isShow)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else if (GameCanvas.menu.showMenu)
						{
							Hint.x = GameCanvas.w / 2;
							Hint.y = GameCanvas.h - 20;
						}
						else
						{
							Hint.clickNpc();
						}
					}
					if (index == 5)
					{
						Hint.clickNpc();
					}
				}
				break;
			case 1:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0)
					{
						if (TileMap.isOfflineMap())
						{
							Hint.nextMap(0);
						}
						else
						{
							Hint.clickMob();
						}
					}
					if (index == 1)
					{
						if (!TileMap.isOfflineMap())
						{
							Hint.nextMap(1);
						}
						else
						{
							Hint.clickNpc();
						}
					}
				}
				break;
			case 2:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0)
					{
						if (!TileMap.isOfflineMap())
						{
							Hint.isViewMap = true;
						}
						if (!GameCanvas.panel.isShow)
						{
							if (!Hint.isViewMap)
							{
								Hint.x = GameScr.gI().cmdMenu.x;
								Hint.y = GameScr.gI().cmdMenu.y + 13;
								Hint.trans = 1;
							}
							else
							{
								if (GameScr.getTaskMapId() == TileMap.mapID)
								{
									if (!Hint.isHaveItem())
									{
										Hint.clickMob();
									}
								}
								else
								{
									Hint.nextMap(0);
								}
								if (Hint.isViewMap)
								{
									Hint.isCloseMap = true;
								}
							}
						}
						else if (!Hint.isViewMap)
						{
							if (GameCanvas.panel.currentTabIndex == 0)
							{
								int num = (GameCanvas.h <= 300) ? 10 : 15;
								Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
								Hint.y = GameCanvas.panel.yScroll + GameCanvas.panel.hScroll - num;
							}
							else
							{
								Hint.x = GameCanvas.panel.startTabPos + 10;
								Hint.y = 65;
							}
						}
						else if (!Hint.isCloseMap)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else
						{
							Hint.isPaint = false;
						}
						if (global::Char.myCharz().cMP <= 0)
						{
							Hint.x = GameScr.xHP + 5;
							Hint.y = GameScr.yHP + 13;
							Hint.isCamera = false;
						}
					}
					if (index == 1)
					{
						Hint.isPaint = false;
						Hint.isPaintArrow = false;
					}
				}
				break;
			default:
				if (global::Char.myCharz().taskMaint.taskId == 9 && global::Char.myCharz().taskMaint.index == 2)
				{
					for (int i = 0; i < PopUp.vPopups.size(); i++)
					{
						PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
						if (popUp.cy <= 24)
						{
							Hint.x = popUp.cx + popUp.sayWidth / 2;
							Hint.y = popUp.cy + 30;
							Hint.isCamera = true;
							Hint.isPaint = false;
							Hint.isPaintArrow = true;
							return;
						}
					}
				}
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				break;
			}
		}
		else
		{
			Hint.isPaint = false;
			Hint.isPaintArrow = false;
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00016C64 File Offset: 0x00014E64
	public static void update()
	{
		Hint.hint();
		int num = (Hint.trans != 0) ? -2 : 2;
		if (!Hint.activeClick)
		{
			Hint.paintFlare = false;
			Hint.t++;
			if (Hint.t == 50)
			{
				Hint.t = 0;
				Hint.activeClick = true;
			}
		}
		else
		{
			Hint.t++;
			if (Hint.type == 0)
			{
				if (Hint.t == 2)
				{
					Hint.x += 2 * num;
					Hint.y -= 4;
					Hint.paintFlare = true;
				}
				if (Hint.t == 4)
				{
					Hint.x -= 2 * num;
					Hint.y += 4;
					Hint.activeClick = false;
					Hint.paintFlare = false;
					Hint.t = 0;
				}
				if (Hint.t > 4)
				{
					Hint.activeClick = false;
				}
			}
			if (Hint.type == 1)
			{
				if (Hint.t == 2)
				{
					if (GameCanvas.isTouch)
					{
						GameScr.startFlyText(mResources.press_twice, Hint.x, Hint.y + 10, 0, 20, mFont.MISS_ME);
					}
					Hint.paintFlare = true;
					Hint.x += 2 * num;
					Hint.y -= 4;
				}
				if (Hint.t == 4)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				if (Hint.t == 6)
				{
					Hint.paintFlare = true;
					Hint.x += num;
					Hint.y -= 2;
				}
				if (Hint.t == 8)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				if (Hint.t == 10)
				{
					Hint.x -= num;
					Hint.y += 2;
					Hint.activeClick = false;
					Hint.t = 0;
				}
			}
		}
	}

	// Token: 0x040004B1 RID: 1201
	public static int x;

	// Token: 0x040004B2 RID: 1202
	public static int y;

	// Token: 0x040004B3 RID: 1203
	public static int type;

	// Token: 0x040004B4 RID: 1204
	public static int t;

	// Token: 0x040004B5 RID: 1205
	public static int xF;

	// Token: 0x040004B6 RID: 1206
	public static int yF;

	// Token: 0x040004B7 RID: 1207
	public static bool isShow;

	// Token: 0x040004B8 RID: 1208
	public static bool activeClick;

	// Token: 0x040004B9 RID: 1209
	public static bool isViewMap;

	// Token: 0x040004BA RID: 1210
	public static bool isCloseMap;

	// Token: 0x040004BB RID: 1211
	public static bool isPaint;

	// Token: 0x040004BC RID: 1212
	public static bool isCamera;

	// Token: 0x040004BD RID: 1213
	public static int trans;

	// Token: 0x040004BE RID: 1214
	public static bool paintFlare;

	// Token: 0x040004BF RID: 1215
	public static bool isPaintArrow;

	// Token: 0x040004C0 RID: 1216
	private int s = 2;
}
