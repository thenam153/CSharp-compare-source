using System;

// Token: 0x020000A0 RID: 160
public class InfoMe
{
	// Token: 0x060006EE RID: 1774 RVA: 0x0005C654 File Offset: 0x0005A854
	public InfoMe()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x000067BE File Offset: 0x000049BE
	public static InfoMe gI()
	{
		if (InfoMe.me == null)
		{
			InfoMe.me = new InfoMe();
		}
		return InfoMe.me;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0005C6A8 File Offset: 0x0005A8A8
	public void loadCharId()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00003584 File Offset: 0x00001784
	public void paint(mGraphics g)
	{
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x000067D6 File Offset: 0x000049D6
	public void hide()
	{
		this.info.hide();
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0005C6D8 File Offset: 0x0005A8D8
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
		this.tF++;
		if (this.tF == 5)
		{
			this.tF = 0;
			if (this.f == 0)
			{
				this.f = 1;
				return;
			}
			this.f = 0;
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x000067E3 File Offset: 0x000049E3
	public void doClick(int t)
	{
		this.timeDelay = t;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
	public void update()
	{
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			return;
		}
		if (!this.isUpdate)
		{
			return;
		}
		this.moveCamera();
		if (this.info == null)
		{
			return;
		}
		if (this.info != null && this.info.info == null)
		{
			return;
		}
		if (!this.isDone)
		{
			if (this.timeDelay > 0)
			{
				this.timeDelay--;
				if (this.timeDelay == 0)
				{
					GameCanvas.panel.setTypeMessage();
					GameCanvas.panel.show();
				}
			}
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					this.cmtoX = global::Char.myCharz().cx - 20 - GameScr.cmx;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					this.cmtoX = global::Char.myCharz().cx + 20 - GameScr.cmx;
				}
				if (this.cmtoX <= 24)
				{
					this.cmtoX += this.info.sayWidth / 2;
				}
				if (this.cmtoX >= GameCanvas.w - 24)
				{
					this.cmtoX -= this.info.sayWidth / 2;
				}
				this.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
				if (this.info.says != null && this.cmtoY < (this.info.says.Length + 1) * 12 + 10)
				{
					this.cmtoY = (this.info.says.Length + 1) * 12 + 10;
				}
				if (this.info.info.charInfo != null)
				{
					if (GameCanvas.w - 50 > 155 + this.info.W)
					{
						this.cmtoX = GameCanvas.w - 60 - this.info.W / 2;
						this.cmtoY = this.info.H + 10;
					}
					else
					{
						this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
						this.cmtoY = 45 + this.info.H;
						if (GameCanvas.w > GameCanvas.h || GameCanvas.w < 220)
						{
							this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
							this.cmtoY = this.info.H + 10;
						}
					}
				}
			}
			if (this.cmx > global::Char.myCharz().cx - GameScr.cmx)
			{
				this.dir = -1;
			}
			else
			{
				this.dir = 1;
			}
		}
		if (this.info.info != null)
		{
			if (this.info.infoWaitToShow.size() > 1)
			{
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeElementAt(0);
						InfoItem infoItem = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem;
						this.info.getInfo();
						return;
					}
				}
				else
				{
					this.info.info.curr = mSystem.currentTimeMillis();
					if (this.info.info.curr - this.info.info.last >= 100L)
					{
						this.info.info.last = mSystem.currentTimeMillis();
						this.info.info.timeCount--;
					}
					if (this.info.info.timeCount == 0)
					{
						this.info.infoWaitToShow.removeElementAt(0);
						if (this.info.infoWaitToShow.size() == 0)
						{
							return;
						}
						InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
						this.info.info = infoItem2;
						this.info.getInfo();
						return;
					}
				}
			}
			else if (this.info.infoWaitToShow.size() == 1)
			{
				if (this.info.info.timeCount == 0)
				{
					this.info.time++;
					if (this.info.time >= this.info.info.speed)
					{
						this.isDone = true;
					}
					if (this.info.time == this.info.info.speed)
					{
						this.cmtoY = -40;
						this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
					}
					if (this.info.time >= this.info.info.speed + 20)
					{
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						return;
					}
				}
				else
				{
					this.info.info.curr = mSystem.currentTimeMillis();
					if (this.info.info.curr - this.info.info.last >= 100L)
					{
						this.info.info.last = mSystem.currentTimeMillis();
						this.info.info.timeCount--;
					}
					if (this.info.info.timeCount == 0)
					{
						this.isDone = true;
						this.cmtoY = -40;
						this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
						this.info.time = 0;
						this.info.infoWaitToShow.removeAllElements();
						this.info.says = null;
						this.cmdChat = null;
					}
				}
			}
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00003584 File Offset: 0x00001784
	public void addInfoWithChar(string s, global::Char c, bool isChatServer)
	{
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00003584 File Offset: 0x00001784
	public void addInfo(string s, int Type)
	{
	}

	// Token: 0x04000CF5 RID: 3317
	public static InfoMe me;

	// Token: 0x04000CF6 RID: 3318
	public int[][] charId = new int[3][];

	// Token: 0x04000CF7 RID: 3319
	public Info info = new Info();

	// Token: 0x04000CF8 RID: 3320
	public int dir;

	// Token: 0x04000CF9 RID: 3321
	public int f;

	// Token: 0x04000CFA RID: 3322
	public int tF;

	// Token: 0x04000CFB RID: 3323
	public int cmtoY;

	// Token: 0x04000CFC RID: 3324
	public int cmy;

	// Token: 0x04000CFD RID: 3325
	public int cmdy;

	// Token: 0x04000CFE RID: 3326
	public int cmvy;

	// Token: 0x04000CFF RID: 3327
	public int cmyLim;

	// Token: 0x04000D00 RID: 3328
	public int cmtoX;

	// Token: 0x04000D01 RID: 3329
	public int cmx;

	// Token: 0x04000D02 RID: 3330
	public int cmdx;

	// Token: 0x04000D03 RID: 3331
	public int cmvx;

	// Token: 0x04000D04 RID: 3332
	public int cmxLim;

	// Token: 0x04000D05 RID: 3333
	public bool isDone;

	// Token: 0x04000D06 RID: 3334
	public bool isUpdate = true;

	// Token: 0x04000D07 RID: 3335
	public int timeDelay;

	// Token: 0x04000D08 RID: 3336
	public int playerID;

	// Token: 0x04000D09 RID: 3337
	public int timeCount;

	// Token: 0x04000D0A RID: 3338
	public Command cmdChat;

	// Token: 0x04000D0B RID: 3339
	public bool isShow;
}
