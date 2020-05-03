using System;

namespace Assets.src.g
{
	// Token: 0x020000AB RID: 171
	public class PetFollow
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00006DFA File Offset: 0x00004FFA
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000797BC File Offset: 0x000779BC
		public void paint(mGraphics g)
		{
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 32, 32, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0007981C File Offset: 0x00077A1C
		public void update()
		{
			this.moveCamera();
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			if (this.count >= this.frame.Length)
			{
				this.count = 0;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00006E32 File Offset: 0x00005032
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 1);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00079878 File Offset: 0x00077A78
		public void moveCamera()
		{
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x04000F1E RID: 3870
		public short smallID;

		// Token: 0x04000F1F RID: 3871
		public Info info = new Info();

		// Token: 0x04000F20 RID: 3872
		public int dir;

		// Token: 0x04000F21 RID: 3873
		public int f;

		// Token: 0x04000F22 RID: 3874
		public int tF;

		// Token: 0x04000F23 RID: 3875
		public int cmtoY;

		// Token: 0x04000F24 RID: 3876
		public int cmy;

		// Token: 0x04000F25 RID: 3877
		public int cmdy;

		// Token: 0x04000F26 RID: 3878
		public int cmvy;

		// Token: 0x04000F27 RID: 3879
		public int cmyLim;

		// Token: 0x04000F28 RID: 3880
		public int cmtoX;

		// Token: 0x04000F29 RID: 3881
		public int cmx;

		// Token: 0x04000F2A RID: 3882
		public int cmdx;

		// Token: 0x04000F2B RID: 3883
		public int cmvx;

		// Token: 0x04000F2C RID: 3884
		public int cmxLim;

		// Token: 0x04000F2D RID: 3885
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x04000F2E RID: 3886
		private int count;
	}
}
