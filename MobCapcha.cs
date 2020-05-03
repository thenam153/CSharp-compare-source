using System;

// Token: 0x02000063 RID: 99
public class MobCapcha
{
	// Token: 0x06000354 RID: 852 RVA: 0x00004FA2 File Offset: 0x000031A2
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00019B1C File Offset: 0x00017D1C
	public static void paint(mGraphics g, int x, int y)
	{
		if (!MobCapcha.isAttack)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					MobCapcha.cmtoX = x - 20 - GameScr.cmx;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					MobCapcha.cmtoX = x + 20 - GameScr.cmx;
				}
			}
			MobCapcha.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
		}
		else
		{
			MobCapcha.delay++;
			if (MobCapcha.delay == 5)
			{
				MobCapcha.isAttack = false;
				MobCapcha.delay = 0;
			}
			MobCapcha.cmtoX = x - GameScr.cmx;
			MobCapcha.cmtoY = y - GameScr.cmy;
		}
		if (MobCapcha.cmx > x - GameScr.cmx)
		{
			MobCapcha.dir = -1;
		}
		else
		{
			MobCapcha.dir = 1;
		}
		g.drawImage(GameScr.imgCapcha, MobCapcha.cmx, MobCapcha.cmy - 40, 3);
		PopUp.paintPopUp(g, MobCapcha.cmx - 25, MobCapcha.cmy - 70, 50, 20, 16777215, false);
		mFont.tahoma_7b_dark.drawString(g, GameScr.gI().keyInput, MobCapcha.cmx, MobCapcha.cmy - 65, 2);
		if (MobCapcha.isCreateMob)
		{
			MobCapcha.isCreateMob = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
		}
		if (MobCapcha.explode)
		{
			MobCapcha.explode = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
			GameScr.gI().mobCapcha = null;
			MobCapcha.cmtoX = -GameScr.cmx;
			MobCapcha.cmtoY = -GameScr.cmy;
		}
		g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 3);
		MobCapcha.moveCamera();
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00019D34 File Offset: 0x00017F34
	public static void moveCamera()
	{
		if (MobCapcha.cmy != MobCapcha.cmtoY)
		{
			MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
			MobCapcha.cmdy += MobCapcha.cmvy;
			MobCapcha.cmy += MobCapcha.cmdy >> 4;
			MobCapcha.cmdy &= 15;
		}
		if (MobCapcha.cmx != MobCapcha.cmtoX)
		{
			MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
			MobCapcha.cmdx += MobCapcha.cmvx;
			MobCapcha.cmx += MobCapcha.cmdx >> 4;
			MobCapcha.cmdx &= 15;
		}
		MobCapcha.tF++;
		if (MobCapcha.tF == 5)
		{
			MobCapcha.tF = 0;
			MobCapcha.f++;
			if (MobCapcha.f > 2)
			{
				MobCapcha.f = 0;
			}
		}
	}

	// Token: 0x040005A2 RID: 1442
	public static Image imgMob;

	// Token: 0x040005A3 RID: 1443
	public static int cmtoY;

	// Token: 0x040005A4 RID: 1444
	public static int cmy;

	// Token: 0x040005A5 RID: 1445
	public static int cmdy;

	// Token: 0x040005A6 RID: 1446
	public static int cmvy;

	// Token: 0x040005A7 RID: 1447
	public static int cmyLim;

	// Token: 0x040005A8 RID: 1448
	public static int cmtoX;

	// Token: 0x040005A9 RID: 1449
	public static int cmx;

	// Token: 0x040005AA RID: 1450
	public static int cmdx;

	// Token: 0x040005AB RID: 1451
	public static int cmvx;

	// Token: 0x040005AC RID: 1452
	public static int cmxLim;

	// Token: 0x040005AD RID: 1453
	public static bool explode;

	// Token: 0x040005AE RID: 1454
	public static int delay;

	// Token: 0x040005AF RID: 1455
	public static bool isCreateMob;

	// Token: 0x040005B0 RID: 1456
	public static int tF;

	// Token: 0x040005B1 RID: 1457
	public static int f;

	// Token: 0x040005B2 RID: 1458
	public static int dir;

	// Token: 0x040005B3 RID: 1459
	public static bool isAttack;
}
