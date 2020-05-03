using System;

// Token: 0x02000059 RID: 89
public class ItemMap : IMapObject
{
	// Token: 0x0600031C RID: 796 RVA: 0x00018224 File Offset: 0x00016424
	public ItemMap(short itemMapID, short itemTemplateID, int x, int y, int xEnd, int yEnd)
	{
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		this.x = xEnd;
		this.y = y;
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - x >> 2;
		this.vy = 5;
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			this.playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
	}

	// Token: 0x0600031D RID: 797 RVA: 0x000182C0 File Offset: 0x000164C0
	public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
	{
		Res.outz(string.Concat(new object[]
		{
			"item map item= ",
			itemMapID,
			" template= ",
			itemTemplateID,
			" x= ",
			x,
			" y= ",
			y
		}));
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
		this.xEnd = x;
		this.x = x;
		this.yEnd = y;
		this.y = y;
		this.status = 1;
		this.playerId = playerId;
		if (this.isAuraItem())
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00004E29 File Offset: 0x00003029
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000183C0 File Offset: 0x000165C0
	public void update()
	{
		if ((int)this.status == 2 && this.x == this.xEnd && this.y == this.yEnd)
		{
			GameScr.vItemMap.removeElement(this);
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this))
			{
				global::Char.myCharz().itemFocus = null;
			}
			return;
		}
		if ((int)this.status > 0)
		{
			if (this.vx == 0)
			{
				this.x = this.xEnd;
			}
			if (this.vy == 0)
			{
				this.y = this.yEnd;
			}
			if (this.x != this.xEnd)
			{
				this.x += this.vx;
				if ((this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd))
				{
					this.x = this.xEnd;
				}
			}
			if (this.y != this.yEnd)
			{
				this.y += this.vy;
				if ((this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd))
				{
					this.y = this.yEnd;
				}
			}
		}
		else
		{
			this.status = (sbyte)((int)this.status - 4);
			if ((int)this.status < -12)
			{
				this.y -= 12;
				this.status = 1;
			}
		}
		if (this.isAuraItem())
		{
			this.updateAuraItemEff();
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00018590 File Offset: 0x00016790
	public void paint(mGraphics g)
	{
		if (this.isAuraItem())
		{
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			if ((int)this.status <= 0)
			{
				if (this.countAura < 10)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else if (this.countAura < 10)
			{
				g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		else if (!this.isAuraItem())
		{
			if (GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if ((int)this.status <= 0)
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && (int)this.status != 2)
			{
				g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
			}
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x000187B0 File Offset: 0x000169B0
	private bool isAuraItem()
	{
		return (int)this.template.type == 22;
	}

	// Token: 0x06000323 RID: 803 RVA: 0x000187DC File Offset: 0x000169DC
	private void setAuraItem()
	{
		this.xO = this.x;
		this.yO = this.y;
		this.iDot = 120;
		this.angle = 0;
		if (!GameCanvas.lowGraphic)
		{
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00018878 File Offset: 0x00016A78
	private void updateAuraItemEff()
	{
		this.count++;
		this.countAura++;
		if (this.countAura >= 40)
		{
			this.countAura = 0;
		}
		if (this.count >= this.iDot)
		{
			this.count = 0;
		}
		if (this.count % 10 == 0 && !GameCanvas.lowGraphic)
		{
			ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00018900 File Offset: 0x00016B00
	public void paintAuraItemEff(mGraphics g)
	{
		if (!GameCanvas.lowGraphic && this.isAuraItem())
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				if (this.count == i)
				{
					if (this.countAura <= 20)
					{
						g.drawImage(ItemMap.imageAuraItem3, this.xDot[i], this.yDot[i] + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.xDot[i], this.yDot[i] + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x000189B0 File Offset: 0x00016BB0
	private void setDotPosition()
	{
		if (!GameCanvas.lowGraphic)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				if (this.angle < 90)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 90 && this.angle < 180)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 180 && this.angle < 270)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				else
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				this.angle += this.iAngle;
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00004E60 File Offset: 0x00003060
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00004E68 File Offset: 0x00003068
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00004E70 File Offset: 0x00003070
	public int getH()
	{
		return 20;
	}

	// Token: 0x0600032A RID: 810 RVA: 0x00004E70 File Offset: 0x00003070
	public int getW()
	{
		return 20;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00003584 File Offset: 0x00001784
	public void stopMoving()
	{
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00003868 File Offset: 0x00001A68
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x0400052C RID: 1324
	public int x;

	// Token: 0x0400052D RID: 1325
	public int y;

	// Token: 0x0400052E RID: 1326
	public int xEnd;

	// Token: 0x0400052F RID: 1327
	public int yEnd;

	// Token: 0x04000530 RID: 1328
	public int f;

	// Token: 0x04000531 RID: 1329
	public int vx;

	// Token: 0x04000532 RID: 1330
	public int vy;

	// Token: 0x04000533 RID: 1331
	public int playerId;

	// Token: 0x04000534 RID: 1332
	public int itemMapID;

	// Token: 0x04000535 RID: 1333
	public int IdCharMove;

	// Token: 0x04000536 RID: 1334
	public ItemTemplate template;

	// Token: 0x04000537 RID: 1335
	public sbyte status;

	// Token: 0x04000538 RID: 1336
	public bool isHintFocus;

	// Token: 0x04000539 RID: 1337
	public int rO;

	// Token: 0x0400053A RID: 1338
	public int xO;

	// Token: 0x0400053B RID: 1339
	public int yO;

	// Token: 0x0400053C RID: 1340
	public int angle;

	// Token: 0x0400053D RID: 1341
	public int iAngle;

	// Token: 0x0400053E RID: 1342
	public int iDot;

	// Token: 0x0400053F RID: 1343
	public int[] xArg;

	// Token: 0x04000540 RID: 1344
	public int[] yArg;

	// Token: 0x04000541 RID: 1345
	public int[] xDot;

	// Token: 0x04000542 RID: 1346
	public int[] yDot;

	// Token: 0x04000543 RID: 1347
	public int count;

	// Token: 0x04000544 RID: 1348
	public int countAura;

	// Token: 0x04000545 RID: 1349
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x04000546 RID: 1350
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x04000547 RID: 1351
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x04000548 RID: 1352
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
