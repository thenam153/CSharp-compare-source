using System;

// Token: 0x02000032 RID: 50
public class BackgroudEffect
{
	// Token: 0x06000216 RID: 534 RVA: 0x0000357C File Offset: 0x0000177C
	public BackgroudEffect(int typeS)
	{
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000484E File Offset: 0x00002A4E
	public static void clearImage()
	{
		TileMap.yWater = 0;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00003868 File Offset: 0x00001A68
	public static bool isHaveRain()
	{
		return false;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00003584 File Offset: 0x00001784
	public static void initCloud()
	{
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00003584 File Offset: 0x00001784
	public static void updateCloud2()
	{
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00003584 File Offset: 0x00001784
	public static void updateFog()
	{
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintCloud2(mGraphics g)
	{
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintFog(mGraphics g)
	{
	}

	// Token: 0x0600021F RID: 543 RVA: 0x000119F8 File Offset: 0x0000FBF8
	private void reloadShip()
	{
		int cmx = GameScr.cmx;
		int cmy = GameScr.cmy;
		this.way = Res.random(1, 3);
		this.isFly = false;
		this.speed = Res.random(3, 5);
		if (this.way == 1)
		{
			this.xShip = -50;
			this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
			this.trans = 0;
			return;
		}
		if (this.way == 2)
		{
			this.xShip = TileMap.pxw + 50;
			this.yShip = Res.random(cmy, GameCanvas.h - 100 + cmy);
			this.trans = 2;
			return;
		}
		if (this.way == 3)
		{
			this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
			this.yShip = -50;
			this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
			return;
		}
		if (this.way == 4)
		{
			this.xShip = Res.random(50 + cmx, GameCanvas.w - 50 + cmx);
			this.yShip = TileMap.pxh + 50;
			this.trans = ((Res.random(0, 2) != 0) ? 2 : 0);
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00003584 File Offset: 0x00001784
	public void paintWater(mGraphics g)
	{
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00003584 File Offset: 0x00001784
	public void paintFar(mGraphics g)
	{
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00011B18 File Offset: 0x0000FD18
	public void update()
	{
		switch (this.typeEff)
		{
		case 0:
		case 12:
			for (int i = 0; i < this.sum; i++)
			{
				if (i % 3 != 0 && this.typeEff != 12 && TileMap.tileTypeAt(this.x[i], this.y[i] - GameCanvas.transY, 2))
				{
					this.activeEff[i] = true;
				}
				if (i % 3 == 0 && this.y[i] > GameCanvas.h + GameScr.cmy)
				{
					this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
					this.y[i] = Res.random(-100, 0) + GameScr.cmy;
				}
				if (!this.activeEff[i])
				{
					this.y[i] += this.vy[i];
					this.x[i] += this.vx[i];
				}
				if (this.activeEff[i])
				{
					this.t[i]++;
					if (this.t[i] > 2)
					{
						this.frame[i]++;
						this.t[i] = 0;
						if (this.frame[i] > 1)
						{
							this.frame[i] = 0;
							this.activeEff[i] = false;
							this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
							this.y[i] = Res.random(-100, 0) + GameScr.cmy;
						}
					}
				}
			}
			return;
		case 1:
		case 2:
		case 5:
		case 6:
		case 7:
		case 11:
			for (int j = 0; j < this.sum; j++)
			{
				if (j % 3 != 0 && TileMap.tileTypeAt(this.x[j], this.y[j] + ((TileMap.tileID != 15) ? 0 : 10), 2))
				{
					this.activeEff[j] = true;
				}
				if (j % 3 == 0 && this.y[j] > TileMap.pxh)
				{
					this.x[j] = Res.random(-10, TileMap.pxw + 50);
					this.y[j] = Res.random(-50, 0);
				}
				if (!this.activeEff[j])
				{
					for (int k = 0; k < Teleport.vTeleport.size(); k++)
					{
						Teleport teleport = (Teleport)Teleport.vTeleport.elementAt(k);
						if (teleport != null && teleport.paintFire && this.x[j] < teleport.x + 80 && this.x[j] > teleport.x - 80 && this.y[j] < teleport.y + 80 && this.y[j] > teleport.y - 80)
						{
							this.x[j] += ((this.x[j] >= teleport.x) ? 10 : -10);
						}
					}
					this.y[j] += this.vy[j];
					this.x[j] += this.vx[j];
					this.t[j]++;
					int num = (this.typeEff != 11) ? 4 : 3;
					if (this.t[j] > ((this.typeEff == 2) ? 4 : 2))
					{
						this.frame[j]++;
						this.t[j] = 0;
						if (this.frame[j] > num - 1)
						{
							this.frame[j] = 0;
						}
					}
				}
				else
				{
					this.t[j]++;
					if (this.t[j] == 100)
					{
						this.t[j] = 0;
						this.x[j] = Res.random(-10, TileMap.pxw + 50);
						this.y[j] = Res.random(-50, 0);
						this.activeEff[j] = false;
					}
				}
			}
			return;
		case 3:
			break;
		case 4:
			for (int l = 0; l < this.sum; l++)
			{
				this.t[l]++;
				if (this.t[l] > 10)
				{
					this.tick[l]++;
					this.t[l] = 0;
					if (this.tick[l] > 5)
					{
						this.tick[l] = 0;
					}
					this.frame[l] = this.dem[this.tick[l]];
				}
			}
			return;
		case 8:
			this.tFire++;
			if (this.tFire == 3)
			{
				this.tFire = 0;
				this.frameFire++;
				if (this.frameFire > 1)
				{
					this.frameFire = 0;
				}
			}
			if (GameCanvas.gameTick % this.tStart == 0)
			{
				this.isFly = true;
			}
			if (this.isFly)
			{
				if (this.way == 1)
				{
					this.xShip += this.speed;
					if (this.xShip > TileMap.pxw + 50)
					{
						this.reloadShip();
						return;
					}
				}
				else if (this.way == 2)
				{
					this.xShip -= this.speed;
					if (this.xShip < -50)
					{
						this.reloadShip();
						return;
					}
				}
				else if (this.way == 3)
				{
					this.yShip += this.speed;
					if (this.yShip > TileMap.pxh + 50)
					{
						this.reloadShip();
						return;
					}
				}
				else if (this.way == 4)
				{
					this.yShip -= this.speed;
					if (this.yShip < -50)
					{
						this.reloadShip();
						return;
					}
				}
			}
			break;
		case 9:
			for (int m = 0; m < this.num; m++)
			{
				this.x[m] -= this.vx[m];
				if (this.x[m] < -this.vx[m])
				{
					BackgroudEffect.wP[m] = Res.abs(Res.random(1, 3));
					this.vx[m] = BackgroudEffect.wP[m];
					this.x[m] = GameCanvas.w + this.vx[m];
				}
			}
			return;
		case 10:
			for (int n = 0; n < this.num; n++)
			{
				this.x[n] -= this.vx[n];
				if (this.x[n] < -this.vx[n] + GameScr.cmx)
				{
					this.x[n] = GameCanvas.w + this.vx[n] + GameScr.cmx;
				}
			}
			return;
		case 13:
			BackgroudEffect.updateCloud2();
			return;
		case 14:
			BackgroudEffect.updateFog();
			break;
		default:
			return;
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00003584 File Offset: 0x00001784
	public void paintFront(mGraphics g)
	{
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00003584 File Offset: 0x00001784
	public void paintLacay1(mGraphics g, Image img)
	{
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00003584 File Offset: 0x00001784
	public void paintLacay2(mGraphics g, Image img)
	{
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00003584 File Offset: 0x00001784
	public void paintBehindTile(mGraphics g)
	{
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00003584 File Offset: 0x00001784
	public void paintBack(mGraphics g)
	{
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00003584 File Offset: 0x00001784
	public static void addEffect(int id)
	{
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00003584 File Offset: 0x00001784
	public static void addWater(int color, int yWater)
	{
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintWaterAll(mGraphics g)
	{
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintBehindTileAll(mGraphics g)
	{
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintFrontAll(mGraphics g)
	{
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintFarAll(mGraphics g)
	{
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00003584 File Offset: 0x00001784
	public static void paintBackAll(mGraphics g)
	{
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00003584 File Offset: 0x00001784
	public static void updateEff()
	{
	}

	// Token: 0x04000205 RID: 517
	public const int TYPE_MUA = 0;

	// Token: 0x04000206 RID: 518
	public const int TYPE_LATRAIDAT_1 = 1;

	// Token: 0x04000207 RID: 519
	public const int TYPE_LATRAIDAT_2 = 2;

	// Token: 0x04000208 RID: 520
	public const int TYPE_SAMSET = 3;

	// Token: 0x04000209 RID: 521
	public const int TYPE_SAO = 4;

	// Token: 0x0400020A RID: 522
	public const int TYPE_LANAMEK_1 = 5;

	// Token: 0x0400020B RID: 523
	public const int TYPE_LASAYAI_1 = 6;

	// Token: 0x0400020C RID: 524
	public const int TYPE_LANAMEK_2 = 7;

	// Token: 0x0400020D RID: 525
	public const int TYPE_SHIP_TRAIDAT = 8;

	// Token: 0x0400020E RID: 526
	public const int TYPE_HANHTINH = 9;

	// Token: 0x0400020F RID: 527
	public const int TYPE_WATER = 10;

	// Token: 0x04000210 RID: 528
	public const int TYPE_SNOW = 11;

	// Token: 0x04000211 RID: 529
	public const int TYPE_MUA_FRONT = 12;

	// Token: 0x04000212 RID: 530
	public const int TYPE_CLOUD = 13;

	// Token: 0x04000213 RID: 531
	public const int TYPE_FOG = 14;

	// Token: 0x04000214 RID: 532
	public static MyVector vBgEffect = new MyVector();

	// Token: 0x04000215 RID: 533
	private int[] x;

	// Token: 0x04000216 RID: 534
	private int[] y;

	// Token: 0x04000217 RID: 535
	private int[] vx;

	// Token: 0x04000218 RID: 536
	private int[] vy;

	// Token: 0x04000219 RID: 537
	public static int[] wP;

	// Token: 0x0400021A RID: 538
	private int num;

	// Token: 0x0400021B RID: 539
	private int xShip;

	// Token: 0x0400021C RID: 540
	private int yShip;

	// Token: 0x0400021D RID: 541
	private int way;

	// Token: 0x0400021E RID: 542
	private int trans;

	// Token: 0x0400021F RID: 543
	private int frameFire;

	// Token: 0x04000220 RID: 544
	private int tFire;

	// Token: 0x04000221 RID: 545
	private int tStart;

	// Token: 0x04000222 RID: 546
	private int speed;

	// Token: 0x04000223 RID: 547
	private bool isFly;

	// Token: 0x04000224 RID: 548
	public static Image imgSnow;

	// Token: 0x04000225 RID: 549
	public static Image imgHatMua;

	// Token: 0x04000226 RID: 550
	public static Image imgMua1;

	// Token: 0x04000227 RID: 551
	public static Image imgMua2;

	// Token: 0x04000228 RID: 552
	public static Image imgSao;

	// Token: 0x04000229 RID: 553
	private static Image imgLacay;

	// Token: 0x0400022A RID: 554
	private static Image imgShip;

	// Token: 0x0400022B RID: 555
	private static Image imgFire1;

	// Token: 0x0400022C RID: 556
	private static Image imgFire2;

	// Token: 0x0400022D RID: 557
	private int[] type;

	// Token: 0x0400022E RID: 558
	private int sum;

	// Token: 0x0400022F RID: 559
	public int typeEff;

	// Token: 0x04000230 RID: 560
	public int xx;

	// Token: 0x04000231 RID: 561
	public int waterY;

	// Token: 0x04000232 RID: 562
	private bool[] isRainEffect;

	// Token: 0x04000233 RID: 563
	private int[] frame;

	// Token: 0x04000234 RID: 564
	private int[] t;

	// Token: 0x04000235 RID: 565
	private bool[] activeEff;

	// Token: 0x04000236 RID: 566
	private int yWater;

	// Token: 0x04000237 RID: 567
	private int colorWater;

	// Token: 0x04000238 RID: 568
	public static Image water1 = GameCanvas.loadImage("/mainImage/myTexture2dwater1.png");

	// Token: 0x04000239 RID: 569
	public static Image water2 = GameCanvas.loadImage("/mainImage/myTexture2dwater2.png");

	// Token: 0x0400023A RID: 570
	public static Image imgChamTron1;

	// Token: 0x0400023B RID: 571
	public static Image imgChamTron2;

	// Token: 0x0400023C RID: 572
	public static bool isFog;

	// Token: 0x0400023D RID: 573
	public static bool isPaintFar;

	// Token: 0x0400023E RID: 574
	public static int nCloud;

	// Token: 0x0400023F RID: 575
	public static Image imgCloud1;

	// Token: 0x04000240 RID: 576
	public static Image imgFog;

	// Token: 0x04000241 RID: 577
	public static int cloudw;

	// Token: 0x04000242 RID: 578
	public static int xfog;

	// Token: 0x04000243 RID: 579
	public static int yfog;

	// Token: 0x04000244 RID: 580
	public static int fogw;

	// Token: 0x04000245 RID: 581
	private int[] dem;

	// Token: 0x04000246 RID: 582
	private int[] tick;
}
