using System;

// Token: 0x02000088 RID: 136
public class Teleport
{
	// Token: 0x06000423 RID: 1059 RVA: 0x00022F24 File Offset: 0x00021124
	public Teleport(int x, int y, int headId, int dir, int type, bool isMe, int planet)
	{
		this.x = x;
		this.y = 5;
		this.y2 = y;
		this.headId = headId;
		this.type = type;
		this.isMe = isMe;
		this.dir = dir;
		this.planet = planet;
		this.tPrepare = 0;
		int i = 0;
		while (i < 100)
		{
			i++;
			this.y2 += 12;
			if (TileMap.tileTypeAt(x, this.y2, 2))
			{
				if (this.y2 % 24 != 0)
				{
					this.y2 -= this.y2 % 24;
					break;
				}
				break;
			}
		}
		this.isDown = true;
		if (this.planet > 2)
		{
			this.y2 += 4;
		}
		if (x > GameScr.cmx && x < GameScr.cmx + GameCanvas.w && this.y2 > 100 && !SoundMn.gI().isPlayAirShip() && !SoundMn.gI().isPlayRain())
		{
			this.createShip = true;
			SoundMn.gI().airShip();
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x000059D2 File Offset: 0x00003BD2
	public static void addTeleport(Teleport p)
	{
		Teleport.vTeleport.addElement(p);
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00003584 File Offset: 0x00001784
	public void paintHole(mGraphics g)
	{
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00003584 File Offset: 0x00001784
	public void paint(mGraphics g)
	{
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00023038 File Offset: 0x00021238
	public void update()
	{
		if (this.isMe)
		{
			if (this.isMe && this.type == 1 && global::Char.myCharz().isTeleport)
			{
				global::Char.myCharz().cx = this.x;
				global::Char.myCharz().cy = this.y2;
				global::Char.myCharz().statusMe = 4;
				GameScr.cmtoX = this.x - GameScr.gW2;
				GameScr.cmtoY = this.y - GameScr.gH23;
				GameScr.info1.isUpdate = false;
				global::Char.myCharz().isTeleport = false;
				Teleport.vTeleport.removeElement(this);
			}
			if (this.isMe && this.type == 0)
			{
				global::Char.myCharz().isTeleport = true;
			}
			if (this.isMe)
			{
				if (this.type == 0)
				{
					GameScr.cmtoX = this.x - GameScr.gW2;
					GameScr.cmtoY = this.y - GameScr.gH23;
				}
				if (this.type == 1)
				{
					GameScr.info1.isUpdate = true;
				}
			}
			if (this.isMe && this.type == 0)
			{
				Controller.isStopReadMessage = false;
				global::Char.ischangingMap = true;
				Teleport.vTeleport.removeElement(this);
			}
			return;
		}
		this.tFire++;
		if (this.tFire > 3)
		{
			this.tFire = 0;
		}
		if (this.isDown)
		{
			this.paintFire = true;
			this.painHead = (this.type != 0);
			if (this.planet < 3)
			{
				int num = this.y2 - this.y >> 3;
				if (num < 1)
				{
					num = 1;
					this.paintFire = false;
				}
				this.y += num;
			}
			else
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.vy++;
				}
				if (this.y2 - this.y < this.vy)
				{
					this.y = this.y2;
					this.paintFire = false;
				}
				else
				{
					this.y += this.vy;
				}
			}
			if (this.isMe && this.type == 1 && global::Char.myCharz().isTeleport)
			{
				global::Char.myCharz().cx = this.x;
				global::Char.myCharz().cy = this.y - 30;
				global::Char.myCharz().statusMe = 4;
				GameScr.cmtoX = this.x - GameScr.gW2;
				GameScr.cmtoY = this.y - GameScr.gH23;
				GameScr.info1.isUpdate = false;
			}
			if (GameScr.findCharInMap(this.id) != null && !this.isMe && this.type == 1 && GameScr.findCharInMap(this.id).isTeleport)
			{
				GameScr.findCharInMap(this.id).cx = this.x;
				GameScr.findCharInMap(this.id).cy = this.y - 30;
				GameScr.findCharInMap(this.id).statusMe = 4;
			}
			if (Res.abs(this.y - this.y2) < 50 && TileMap.tileTypeAt(this.x, this.y, 2))
			{
				this.tHole = true;
				if (this.planet < 3)
				{
					if (this.y % 24 != 0)
					{
						this.y -= this.y % 24;
					}
					this.tPrepare++;
					if (this.tPrepare > 10)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					if (this.type == 1)
					{
						if (this.isMe)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else if (GameScr.findCharInMap(this.id) != null)
						{
							GameScr.findCharInMap(this.id).isTeleport = false;
						}
						this.painHead = false;
						return;
					}
				}
				else
				{
					this.y = this.y2;
					if (!this.isShock)
					{
						GameScr.shock_scr = 10;
						this.isShock = true;
					}
					this.tPrepare++;
					if (this.tPrepare > 30)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					if (this.type == 1)
					{
						if (this.isMe)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else if (GameScr.findCharInMap(this.id) != null)
						{
							GameScr.findCharInMap(this.id).isTeleport = false;
						}
						this.painHead = false;
						return;
					}
				}
			}
		}
		else if (this.isUp)
		{
			this.tPrepare++;
			if (this.tPrepare > 30)
			{
				int num2 = this.y2 + 24 - this.y >> 3;
				if (num2 > 30)
				{
					num2 = 30;
				}
				this.y -= num2;
				this.paintFire = true;
			}
			else
			{
				if (this.tPrepare == 14 && this.createShip)
				{
					SoundMn.gI().resumeAirShip();
				}
				if (this.tPrepare > 0 && this.type == 0)
				{
					if (this.isMe)
					{
						global::Char.myCharz().isTeleport = false;
						if (global::Char.myCharz().statusMe != 14)
						{
							global::Char.myCharz().statusMe = 3;
						}
						global::Char.myCharz().cvy = -3;
					}
					else if (GameScr.findCharInMap(this.id) != null)
					{
						GameScr.findCharInMap(this.id).isTeleport = false;
						if (GameScr.findCharInMap(this.id).statusMe != 14)
						{
							GameScr.findCharInMap(this.id).statusMe = 3;
						}
						GameScr.findCharInMap(this.id).cvy = -3;
					}
					this.painHead = false;
				}
				if (this.tPrepare > 12 && this.type == 0)
				{
					if (this.isMe)
					{
						global::Char.myCharz().isTeleport = true;
					}
					else if (GameScr.findCharInMap(this.id) != null)
					{
						GameScr.findCharInMap(this.id).cx = this.x;
						GameScr.findCharInMap(this.id).cy = this.y;
						GameScr.findCharInMap(this.id).isTeleport = true;
					}
					this.painHead = true;
				}
			}
			if (this.isMe)
			{
				if (this.type == 0)
				{
					GameScr.cmtoX = this.x - GameScr.gW2;
					GameScr.cmtoY = this.y - GameScr.gH23;
				}
				if (this.type == 1)
				{
					GameScr.info1.isUpdate = true;
				}
			}
			if (this.y <= -80)
			{
				if (this.isMe && this.type == 0)
				{
					Controller.isStopReadMessage = false;
					global::Char.ischangingMap = true;
				}
				if (!this.isMe && GameScr.findCharInMap(this.id) != null && this.type == 0)
				{
					GameScr.vCharInMap.removeElement(GameScr.findCharInMap(this.id));
				}
				if (this.planet < 3)
				{
					Teleport.vTeleport.removeElement(this);
					return;
				}
				this.y = -80;
				this.tDelayHole++;
				if (this.tDelayHole > 80)
				{
					this.tDelayHole = 0;
					Teleport.vTeleport.removeElement(this);
				}
			}
		}
	}

	// Token: 0x040006EF RID: 1775
	public static MyVector vTeleport = new MyVector();

	// Token: 0x040006F0 RID: 1776
	public int x;

	// Token: 0x040006F1 RID: 1777
	public int y;

	// Token: 0x040006F2 RID: 1778
	public int headId;

	// Token: 0x040006F3 RID: 1779
	public int type;

	// Token: 0x040006F4 RID: 1780
	public bool isMe;

	// Token: 0x040006F5 RID: 1781
	public int y2;

	// Token: 0x040006F6 RID: 1782
	public int id;

	// Token: 0x040006F7 RID: 1783
	public int dir;

	// Token: 0x040006F8 RID: 1784
	public int planet;

	// Token: 0x040006F9 RID: 1785
	public static Image[] maybay = new Image[5];

	// Token: 0x040006FA RID: 1786
	public static Image hole;

	// Token: 0x040006FB RID: 1787
	public bool isUp;

	// Token: 0x040006FC RID: 1788
	public bool isDown;

	// Token: 0x040006FD RID: 1789
	private bool createShip;

	// Token: 0x040006FE RID: 1790
	public bool paintFire;

	// Token: 0x040006FF RID: 1791
	private bool painHead;

	// Token: 0x04000700 RID: 1792
	private int tPrepare;

	// Token: 0x04000701 RID: 1793
	private int vy = 1;

	// Token: 0x04000702 RID: 1794
	private int tFire;

	// Token: 0x04000703 RID: 1795
	private int tDelayHole;

	// Token: 0x04000704 RID: 1796
	private bool tHole;

	// Token: 0x04000705 RID: 1797
	private bool isShock;
}
