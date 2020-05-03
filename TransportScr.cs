using System;

// Token: 0x020000B6 RID: 182
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x060008D6 RID: 2262 RVA: 0x0007FCCC File Offset: 0x0007DECC
	public TransportScr()
	{
		this.posX = new int[this.n];
		this.posY = new int[this.n];
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] = Res.random(0, GameCanvas.w);
			this.posY[i] = i * (GameCanvas.h / this.n);
		}
		this.posX2 = new int[this.n];
		this.posY2 = new int[this.n];
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] = Res.random(0, GameCanvas.w);
			this.posY2[j] = j * (GameCanvas.h / this.n);
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0000733D File Offset: 0x0000553D
	public static TransportScr gI()
	{
		if (TransportScr.instance == null)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0007FDB4 File Offset: 0x0007DFB4
	public override void switchToMe()
	{
		if (TransportScr.ship == null)
		{
			TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
		}
		if (TransportScr.taungam == null)
		{
			TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
		}
		this.isSpeed = false;
		this.transNow = false;
		if (global::Char.myCharz().luong > 0 && (int)this.type == 0)
		{
			this.center = new Command(mResources.faster, this, 1, null);
		}
		else
		{
			this.center = null;
		}
		this.currSpeed = 0;
		base.switchToMe();
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0007FE4C File Offset: 0x0007E04C
	public override void paint(mGraphics g)
	{
		g.setColor(((int)this.type != 0) ? 3056895 : 0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		for (int i = 0; i < this.n; i++)
		{
			g.setColor(((int)this.type != 0) ? 11140863 : 14802654);
			g.fillRect(this.posX[i], this.posY[i], 10, 2);
		}
		if ((int)this.type == 0)
		{
			g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		if ((int)this.type == 1)
		{
			g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		for (int j = 0; j < this.n; j++)
		{
			g.setColor(((int)this.type != 0) ? 7536127 : 14935011);
			g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
		}
		base.paint(g);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0007FF9C File Offset: 0x0007E19C
	public override void update()
	{
		if ((int)this.type == 0)
		{
			if (!this.isSpeed)
			{
				this.currSpeed = GameCanvas.w / 2 * (int)this.time / (int)this.maxTime;
			}
		}
		else
		{
			this.currSpeed += 2;
		}
		Controller.isStopReadMessage = false;
		this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
		if ((int)this.type == 1)
		{
			this.cmx = 0;
		}
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] -= this.speed / 2;
			if (this.posX[i] < -20)
			{
				this.posX[i] = GameCanvas.w;
			}
		}
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] -= this.speed;
			if (this.posX2[j] < -20)
			{
				this.posX2[j] = GameCanvas.w;
			}
		}
		if (GameCanvas.gameTick % 3 == 0)
		{
			this.speed += ((!this.isSpeed) ? 1 : 2);
		}
		if (this.speed > ((!this.isSpeed) ? 25 : 80))
		{
			this.speed = ((!this.isSpeed) ? 25 : 80);
		}
		this.curr = mSystem.currentTimeMillis();
		if (this.curr - this.last >= 1000L)
		{
			this.time += 1;
			this.last = this.curr;
		}
		if (this.isSpeed)
		{
			this.currSpeed += 3;
		}
		if (this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow)
		{
			this.transNow = true;
			Service.gI().transportNow();
		}
		base.update();
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00007358 File Offset: 0x00005558
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x000801B8 File Offset: 0x0007E3B8
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
		}
		if (idAction == 2 && global::Char.myCharz().luong > 0)
		{
			this.isSpeed = true;
			GameCanvas.endDlg();
			this.center = null;
		}
		if (idAction == 3)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04001065 RID: 4197
	public static TransportScr instance;

	// Token: 0x04001066 RID: 4198
	public static Image ship;

	// Token: 0x04001067 RID: 4199
	public static Image taungam;

	// Token: 0x04001068 RID: 4200
	public sbyte type;

	// Token: 0x04001069 RID: 4201
	public int speed = 5;

	// Token: 0x0400106A RID: 4202
	public int[] posX;

	// Token: 0x0400106B RID: 4203
	public int[] posY;

	// Token: 0x0400106C RID: 4204
	public int[] posX2;

	// Token: 0x0400106D RID: 4205
	public int[] posY2;

	// Token: 0x0400106E RID: 4206
	private int cmx;

	// Token: 0x0400106F RID: 4207
	private int n = 20;

	// Token: 0x04001070 RID: 4208
	public short time;

	// Token: 0x04001071 RID: 4209
	public short maxTime;

	// Token: 0x04001072 RID: 4210
	public long last;

	// Token: 0x04001073 RID: 4211
	public long curr;

	// Token: 0x04001074 RID: 4212
	private bool isSpeed;

	// Token: 0x04001075 RID: 4213
	private bool transNow;

	// Token: 0x04001076 RID: 4214
	private int currSpeed;
}
