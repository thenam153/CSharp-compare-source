using System;

// Token: 0x020000B9 RID: 185
public class GamePad
{
	// Token: 0x0600093A RID: 2362 RVA: 0x0008395C File Offset: 0x00081B5C
	public GamePad()
	{
		this.R = 28;
		if (GameCanvas.w < 300)
		{
			this.isSmallGamePad = true;
			this.isMediumGamePad = false;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w >= 300 && GameCanvas.w <= 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = true;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w > 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = false;
			this.isLargeGamePad = true;
		}
		if (!this.isLargeGamePad)
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h - 80;
		}
		else
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw / 4 * 3 - 20;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h;
		}
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00083A60 File Offset: 0x00081C60
	public void update()
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		if (GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease)
		{
			this.xTemp = GameCanvas.pxFirst;
			this.yTemp = GameCanvas.pyFirst;
			if (this.xTemp >= this.xZone && this.xTemp <= this.wZone && this.yTemp >= this.yZone && this.yTemp <= this.hZone)
			{
				if (!this.isGamePad)
				{
					this.xC = (this.xM = this.xTemp);
					this.yC = (this.yM = this.yTemp);
				}
				this.isGamePad = true;
				this.deltaX = GameCanvas.px - this.xC;
				this.deltaY = GameCanvas.py - this.yC;
				this.delta = global::Math.pow(this.deltaX, 2) + global::Math.pow(this.deltaY, 2);
				this.d = Res.sqrt(this.delta);
				if (global::Math.abs(this.deltaX) > 4 || global::Math.abs(this.deltaY) > 4)
				{
					this.angle = Res.angle(this.deltaX, this.deltaY);
					if (!GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R))
					{
						if (this.d != 0)
						{
							this.yM = this.deltaY * this.R / this.d;
							this.xM = this.deltaX * this.R / this.d;
							this.xM += this.xC;
							this.yM += this.yC;
							if (!Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM))
							{
								this.xM = this.xMLast;
								this.yM = this.yMLast;
							}
							else
							{
								this.xMLast = this.xM;
								this.yMLast = this.yM;
							}
						}
						else
						{
							this.xM = this.xMLast;
							this.yM = this.yMLast;
						}
					}
					else
					{
						this.xM = GameCanvas.px;
						this.yM = GameCanvas.py;
					}
					this.resetHold();
					if (this.checkPointerMove(2))
					{
						if ((this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20))
						{
							GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
						}
						else if (this.angle > 40 && this.angle < 70)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
						}
						else if (this.angle >= 70 && this.angle <= 110)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
						}
						else if (this.angle > 110 && this.angle < 120)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
						}
						else if (this.angle >= 120 && this.angle <= 200)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
						}
						else if (this.angle > 200 && this.angle < 250)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
							GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
						}
						else if (this.angle >= 250 && this.angle <= 290)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
						}
						else if (this.angle > 290 && this.angle < 340)
						{
							GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
							GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
							GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
						}
					}
					else
					{
						this.resetHold();
					}
				}
			}
		}
		else
		{
			this.xM = (this.xC = 45);
			if (!this.isLargeGamePad)
			{
				this.yM = (this.yC = GameCanvas.h - 90);
			}
			else
			{
				this.yM = (this.yC = GameCanvas.h - 45);
			}
			this.isGamePad = false;
			this.resetHold();
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00084090 File Offset: 0x00082290
	private bool checkPointerMove(int distance)
	{
		if (GameScr.isAnalog == 0)
		{
			return false;
		}
		if (global::Char.myCharz().statusMe == 3)
		{
			return true;
		}
		try
		{
			for (int i = 2; i > 0; i--)
			{
				int i2 = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
				int i3 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
				if (Res.abs(i2) > distance && Res.abs(i3) > distance)
				{
					return false;
				}
			}
		}
		catch (Exception ex)
		{
		}
		return true;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00007778 File Offset: 0x00005978
	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00084144 File Offset: 0x00082344
	public void paint(mGraphics g)
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
		g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0000777F File Offset: 0x0000597F
	public bool disableCheckDrag()
	{
		return GameScr.isAnalog != 0 && this.isGamePad;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000841A0 File Offset: 0x000823A0
	public bool disableClickMove()
	{
		return GameScr.isAnalog != 0 && ((GameCanvas.px >= this.xZone && GameCanvas.px <= this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.hZone) || GameCanvas.px >= GameCanvas.w - 50);
	}

	// Token: 0x0400112E RID: 4398
	private int xC;

	// Token: 0x0400112F RID: 4399
	private int yC;

	// Token: 0x04001130 RID: 4400
	private int xM;

	// Token: 0x04001131 RID: 4401
	private int yM;

	// Token: 0x04001132 RID: 4402
	private int xMLast;

	// Token: 0x04001133 RID: 4403
	private int yMLast;

	// Token: 0x04001134 RID: 4404
	private int R;

	// Token: 0x04001135 RID: 4405
	private int r;

	// Token: 0x04001136 RID: 4406
	private int d;

	// Token: 0x04001137 RID: 4407
	private int xTemp;

	// Token: 0x04001138 RID: 4408
	private int yTemp;

	// Token: 0x04001139 RID: 4409
	private int deltaX;

	// Token: 0x0400113A RID: 4410
	private int deltaY;

	// Token: 0x0400113B RID: 4411
	private int delta;

	// Token: 0x0400113C RID: 4412
	private int angle;

	// Token: 0x0400113D RID: 4413
	public int xZone;

	// Token: 0x0400113E RID: 4414
	public int yZone;

	// Token: 0x0400113F RID: 4415
	public int wZone;

	// Token: 0x04001140 RID: 4416
	public int hZone;

	// Token: 0x04001141 RID: 4417
	private bool isGamePad;

	// Token: 0x04001142 RID: 4418
	public bool isSmallGamePad;

	// Token: 0x04001143 RID: 4419
	public bool isMediumGamePad;

	// Token: 0x04001144 RID: 4420
	public bool isLargeGamePad;
}
