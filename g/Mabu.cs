using System;

namespace Assets.src.g
{
	// Token: 0x020000A5 RID: 165
	internal class Mabu : global::Char
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x00006900 File Offset: 0x00004B00
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0005EF2C File Offset: 0x0005D12C
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == global::Char.myCharz().charID)
			{
				this.focus = global::Char.myCharz();
			}
			else
			{
				this.focus = GameScr.findCharInMap(id);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0005EF90 File Offset: 0x0005D190
		public new void checkFrameTick(int[] array)
		{
			if ((int)this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					Effect me = new Effect(19, this.cx, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(me);
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if ((int)this.skillID == 1 && this.tick == array.Length - 1)
			{
				this.skillID = 3;
				this.cy -= 15;
				return;
			}
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0005F05C File Offset: 0x0005D25C
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				102,
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102 + "/img.png");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0005F100 File Offset: 0x0005D300
		public void setSkill(sbyte id, short x, short y, global::Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo <= this.cx) ? -1 : 1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0005F15C File Offset: 0x0005D35C
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				103,
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0005F20C File Offset: 0x0005D40C
		public override void update()
		{
			if (this.focus != null)
			{
				if (this.effEat.t >= 30)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x <= this.focus.cx) ? 0 : 1);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if ((int)this.skillID != -1)
			{
				if ((int)this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					if ((this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo))
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure(this.damageAttack[i], 0, false, false);
						}
					}
				}
				if ((int)this.skillID == 3)
				{
					this.xTo = this.charAttack[this.pIndex].cx;
					this.yTo = this.charAttack[this.pIndex].cy;
					this.cx += (this.xTo - this.cx) / 3;
					this.cy += (this.yTo - this.cy) / 3;
					if (GameCanvas.gameTick % 5 == 0)
					{
						Effect me = new Effect(19, this.cx, this.cy, 2, 1, -1);
						EffecMn.addEff(me);
					}
					if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
					{
						this.cx = this.xTo;
						this.cy = this.yTo;
						this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0, false, false);
						this.pIndex++;
						if (this.pIndex == this.charAttack.Length)
						{
							this.skillID = -1;
							this.pIndex = 0;
						}
					}
				}
				return;
			}
			base.update();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
		public override void paint(mGraphics g)
		{
			if ((int)this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if ((int)this.skillID == 0 || (int)this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
			}
			else
			{
				base.paint(g);
			}
		}

		// Token: 0x04000D64 RID: 3428
		public static EffectData data1;

		// Token: 0x04000D65 RID: 3429
		public static EffectData data2;

		// Token: 0x04000D66 RID: 3430
		private new int tick;

		// Token: 0x04000D67 RID: 3431
		private int lastDir;

		// Token: 0x04000D68 RID: 3432
		private bool addFoot;

		// Token: 0x04000D69 RID: 3433
		private Effect effEat;

		// Token: 0x04000D6A RID: 3434
		private new global::Char focus;

		// Token: 0x04000D6B RID: 3435
		public int xTo;

		// Token: 0x04000D6C RID: 3436
		public int yTo;

		// Token: 0x04000D6D RID: 3437
		public bool haftBody;

		// Token: 0x04000D6E RID: 3438
		public bool change;

		// Token: 0x04000D6F RID: 3439
		private global::Char[] charAttack;

		// Token: 0x04000D70 RID: 3440
		private int[] damageAttack;

		// Token: 0x04000D71 RID: 3441
		private int dx;

		// Token: 0x04000D72 RID: 3442
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x04000D73 RID: 3443
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x04000D74 RID: 3444
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x04000D75 RID: 3445
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x04000D76 RID: 3446
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x04000D77 RID: 3447
		public sbyte skillID = -1;

		// Token: 0x04000D78 RID: 3448
		private int frame;

		// Token: 0x04000D79 RID: 3449
		private int pIndex;
	}
}
