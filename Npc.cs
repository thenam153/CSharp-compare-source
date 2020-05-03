using System;

// Token: 0x02000067 RID: 103
public class Npc : global::Char
{
	// Token: 0x0600035B RID: 859 RVA: 0x00019E1C File Offset: 0x0001801C
	public Npc(int npcId, int status, int cx, int cy, int templateId, int avatar)
	{
		this.isShadown = true;
		this.npcId = npcId;
		this.avatar = avatar;
		this.cx = cx;
		this.cy = cy;
		this.xSd = cx;
		this.ySd = cy;
		this.statusMe = status;
		if (npcId != -1)
		{
			this.template = Npc.arrNpcTemplate[templateId];
		}
		if (templateId == 23 || templateId == 42)
		{
			this.ch = 45;
		}
		if (templateId == 51)
		{
			this.isShadown = false;
			this.duaHauIndex = status;
		}
		if (this.template != null)
		{
			if (this.template.name == null)
			{
				this.template.name = string.Empty;
			}
			this.template.name = Res.changeString(this.template.name);
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00019F00 File Offset: 0x00018100
	public void setStatus(sbyte s, int sc)
	{
		this.duaHauIndex = (int)s;
		this.last = (this.cur = mSystem.currentTimeMillis());
		this.seconds = sc;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00019F30 File Offset: 0x00018130
	public static void clearEffTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			npc.effTask = null;
			npc.indexEffTask = -1;
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00019F78 File Offset: 0x00018178
	public override void update()
	{
		if (this.template.npcTemplateId == 51)
		{
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last >= 1000L)
			{
				this.seconds--;
				this.last = this.cur;
				if (this.seconds < 0)
				{
					this.seconds = 0;
				}
			}
		}
		if (this.isShadown)
		{
			base.updateShadown();
		}
		if (this.effTask == null)
		{
			sbyte[] array = new sbyte[]
			{
				-1,
				9,
				9,
				10,
				10,
				11,
				11
			};
			if (global::Char.myCharz().ctaskId >= 9 && global::Char.myCharz().ctaskId <= 10 && global::Char.myCharz().nClass.classId > 0 && (int)array[global::Char.myCharz().nClass.classId] == this.template.npcTemplateId)
			{
				if (global::Char.myCharz().taskMaint == null)
				{
					this.effTask = GameScr.efs[57];
					this.indexEffTask = 0;
				}
				else if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length)
				{
					this.effTask = GameScr.efs[62];
					this.indexEffTask = 0;
				}
			}
			else
			{
				sbyte taskNpcId = GameScr.getTaskNpcId();
				if (global::Char.myCharz().taskMaint == null && (int)taskNpcId == this.template.npcTemplateId)
				{
					this.indexEffTask = 0;
				}
				else if (global::Char.myCharz().taskMaint != null && (int)taskNpcId == this.template.npcTemplateId)
				{
					if (global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length)
					{
						this.effTask = GameScr.efs[98];
					}
					else
					{
						this.effTask = GameScr.efs[98];
					}
					this.indexEffTask = 0;
				}
			}
		}
		base.update();
		if (TileMap.mapID == 51)
		{
			if (this.cx > global::Char.myCharz().cx)
			{
				this.cdir = -1;
			}
			else
			{
				this.cdir = 1;
			}
			if (this.template.npcTemplateId % 2 == 0)
			{
				if (this.cf == 1)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
			}
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001A1F8 File Offset: 0x000183F8
	public void paintHead(mGraphics g, int xStart, int yStart)
	{
		Part part = GameScr.parts[this.template.headId];
		if (this.cdir == 1)
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 0, 0);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 2, 24);
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001A2A0 File Offset: 0x000184A0
	public override void paint(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (!base.isPaint())
		{
			return;
		}
		if (this.statusMe == 15)
		{
			return;
		}
		if ((int)this.cTypePk != 0)
		{
			base.paint(g);
			return;
		}
		if (this.template == null)
		{
			return;
		}
		if (this.template.npcTemplateId != 4 && this.template.npcTemplateId != 51 && this.template.npcTemplateId != 50)
		{
			g.drawImage(TileMap.bong, this.cx, this.cy, 3);
		}
		if (this.template.npcTemplateId == 3)
		{
			SmallImage.drawSmallImage(g, 265, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
			{
				if (ChatPopup.currChatPopup == null)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch + 4, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			this.dyEff = 60;
		}
		else if (this.template.npcTemplateId != 4)
		{
			if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
			{
				if (this.duahau != null)
				{
					if (this.template.npcTemplateId == 50 && Npc.mabuEff)
					{
						Npc.tMabuEff++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							Effect me = new Effect(19, this.cx + Res.random(-50, 50), this.cy, 2, 1, -1);
							EffecMn.addEff(me);
						}
						if (GameCanvas.gameTick % 15 == 0)
						{
							Effect me2 = new Effect(18, this.cx + Res.random(-5, 5), this.cy + Res.random(-90, 0), 2, 1, -1);
							EffecMn.addEff(me2);
						}
						if (Npc.tMabuEff == 100)
						{
							GameScr.gI().activeSuperPower(this.cx, this.cy);
						}
						if (Npc.tMabuEff == 110)
						{
							Npc.mabuEff = false;
							this.template.npcTemplateId = 4;
						}
					}
					int num = 0;
					if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
					{
						num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
					}
					SmallImage.drawSmallImage(g, this.duahau[this.duaHauIndex], this.cx + Res.random(-1, 1), this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (ChatPopup.currChatPopup == null)
						{
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9 + 16 - num, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 16 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
					}
					else
					{
						mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
					}
				}
			}
			else if (this.template.npcTemplateId == 6)
			{
				SmallImage.drawSmallImage(g, 545, this.cx, this.cy + 5, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
				{
					if (ChatPopup.currChatPopup == null)
					{
						g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
				mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, this.cx, this.cy - this.ch + 19 - mFont.tahoma_7.getHeight(), mFont.CENTER);
			}
			else
			{
				int headId = this.template.headId;
				int legId = this.template.legId;
				int bodyId = this.template.bodyId;
				Part part = GameScr.parts[headId];
				Part part2 = GameScr.parts[legId];
				Part part3 = GameScr.parts[bodyId];
				if (this.cdir == 1)
				{
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 0, 0);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 0, 0);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx - global::Char.CharInfo[this.cf][0][1] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 2, 24);
					SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx - global::Char.CharInfo[this.cf][1][1] - (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 2, 24);
					SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx - global::Char.CharInfo[this.cf][2][1] - (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 2, 24);
				}
				if (TileMap.mapID != 51)
				{
					int num2 = 15;
					if (this.template.npcTemplateId == 47)
					{
						num2 = 47;
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (ChatPopup.currChatPopup == null)
						{
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - (num2 - 8), mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
					else if (this.template.npcTemplateId == 47)
					{
					}
				}
				this.dyEff = 65;
			}
		}
		if (this.indexEffTask >= 0 && this.effTask != null && (int)this.cTypePk == 0)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy - this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}
		if (this.chatInfo != null)
		{
			this.chatInfo.paint(g, this.cx, this.cy - this.ch, this.cdir);
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001ACDC File Offset: 0x00018EDC
	public new void paintName(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		if (this.isHide)
		{
			return;
		}
		if (!base.isPaint())
		{
			return;
		}
		if (this.statusMe == 15)
		{
			return;
		}
		if (this.template == null)
		{
			return;
		}
		if (this.template.npcTemplateId == 3)
		{
			if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 3 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
			}
			this.dyEff = 60;
		}
		else if (this.template.npcTemplateId != 4)
		{
			if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
			{
				if (this.duahau != null)
				{
					int num = 0;
					if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
					{
						num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num, mFont.CENTER, mFont.tahoma_7_grey);
					}
					else
					{
						mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - num + 16, mFont.CENTER, mFont.tahoma_7_grey);
					}
				}
			}
			else if (this.template.npcTemplateId == 6)
			{
				if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 16, mFont.CENTER, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else
			{
				if (TileMap.mapID != 51)
				{
					int num2 = 15;
					if (this.template.npcTemplateId == 47)
					{
						num2 = 47;
					}
					if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
					{
						if (TileMap.mapID != 113)
						{
							mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num2, mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
					else
					{
						num2 = 8;
						if (this.template.npcTemplateId == 47)
						{
							num2 = 40;
						}
						if (TileMap.mapID != 113)
						{
							mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - num2 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
				}
				this.dyEff = 65;
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0000501C File Offset: 0x0000321C
	public new void hide()
	{
		this.statusMe = 15;
		global::Char.chatPopup = null;
	}

	// Token: 0x040005C5 RID: 1477
	public const sbyte BINH_KHI = 0;

	// Token: 0x040005C6 RID: 1478
	public const sbyte PHONG_CU = 1;

	// Token: 0x040005C7 RID: 1479
	public const sbyte TRANG_SUC = 2;

	// Token: 0x040005C8 RID: 1480
	public const sbyte DUOC_PHAM = 3;

	// Token: 0x040005C9 RID: 1481
	public const sbyte TAP_HOA = 4;

	// Token: 0x040005CA RID: 1482
	public const sbyte THU_KHO = 5;

	// Token: 0x040005CB RID: 1483
	public const sbyte DA_LUYEN = 6;

	// Token: 0x040005CC RID: 1484
	public NpcTemplate template;

	// Token: 0x040005CD RID: 1485
	public int npcId;

	// Token: 0x040005CE RID: 1486
	public bool isFocus = true;

	// Token: 0x040005CF RID: 1487
	public static NpcTemplate[] arrNpcTemplate;

	// Token: 0x040005D0 RID: 1488
	public int sys;

	// Token: 0x040005D1 RID: 1489
	public bool isHide;

	// Token: 0x040005D2 RID: 1490
	private int duaHauIndex;

	// Token: 0x040005D3 RID: 1491
	private int dyEff;

	// Token: 0x040005D4 RID: 1492
	public static bool mabuEff;

	// Token: 0x040005D5 RID: 1493
	public static int tMabuEff;

	// Token: 0x040005D6 RID: 1494
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x040005D7 RID: 1495
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x040005D8 RID: 1496
	public static int shock_scr;

	// Token: 0x040005D9 RID: 1497
	public int[] duahau;

	// Token: 0x040005DA RID: 1498
	public new int seconds;

	// Token: 0x040005DB RID: 1499
	public new long last;

	// Token: 0x040005DC RID: 1500
	public new long cur;

	// Token: 0x040005DD RID: 1501
	public int idItem;
}
