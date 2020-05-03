using System;

// Token: 0x020000B3 RID: 179
public class TabClanIcon : IActionListener
{
	// Token: 0x060008A6 RID: 2214 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
	public TabClanIcon()
	{
		this.left = new Command(mResources.SELECT, this, 1, null);
		this.right = new Command(mResources.CLOSE, this, 2, null);
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0007E138 File Offset: 0x0007C338
	public void init()
	{
		if (this.isGetName)
		{
			this.w = 170;
			this.h = 118;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
		}
		else
		{
			this.w = 170;
			this.h = 170;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
			if (GameCanvas.h < 240)
			{
				this.y -= 10;
			}
		}
		this.cmx = this.x;
		this.cmtoX = 0;
		if (!this.isRequest)
		{
			this.nItem = ClanImage.vClanImage.size();
		}
		else
		{
			this.nItem = this.vItems.size();
		}
		if (GameCanvas.isTouch)
		{
			this.left.x = this.x;
			this.left.y = this.y + this.h + 5;
			this.right.x = this.x + this.w - 68;
			this.right.y = this.y + this.h + 5;
		}
		TabClanIcon.scrMain = new Scroll();
		TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00007167 File Offset: 0x00005367
	public void show(bool isGetName)
	{
		if (global::Char.myCharz().clan != null)
		{
			this.isUpdate = true;
		}
		this.isShow = true;
		this.isGetName = isGetName;
		this.init();
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00007193 File Offset: 0x00005393
	public void showRequest(int msgID)
	{
		this.isShow = true;
		this.isRequest = true;
		this.msgID = msgID;
		this.init();
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x000071B0 File Offset: 0x000053B0
	public void hide()
	{
		this.cmtoX = this.x + this.w;
		SmallImage.clearHastable();
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00003584 File Offset: 0x00001784
	public void paintPeans(mGraphics g)
	{
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0007E2EC File Offset: 0x0007C4EC
	public void paintIcon(mGraphics g)
	{
		g.translate(-this.cmx, 0);
		PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
		if (this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
			if (clanImage.idImage != null)
			{
				global::Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
			}
		}
		global::Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, global::Char.myCharz().cf, false);
		g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
		if (TabClanIcon.scrMain != null)
		{
			g.translate(0, -TabClanIcon.scrMain.cmy);
		}
		for (int i = 0; i < this.nItem; i++)
		{
			int num = this.x + 10;
			int num2 = this.y + i * this.WIDTH + this.disStart;
			if (num2 + this.WIDTH - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain == null) ? 0 : TabClanIcon.scrMain.cmy) <= this.y + this.disStart + this.h)
			{
				ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
				mFont mFont = mFont.tahoma_7_grey;
				if (i == this.lastSelect)
				{
					mFont = mFont.tahoma_7_blue;
				}
				if (clanImage2.name != null)
				{
					mFont.drawString(g, clanImage2.name, num + 20, num2, 0);
				}
				if (clanImage2.xu > 0)
				{
					mFont.drawString(g, clanImage2.xu + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
				}
				else if (clanImage2.luong > 0)
				{
					mFont.drawString(g, clanImage2.luong + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
				}
				else
				{
					mFont.drawString(g, mResources.free, num + this.w - 20, num2, mFont.RIGHT);
				}
				if (clanImage2.idImage != null)
				{
					SmallImage.drawSmallImage(g, (int)clanImage2.idImage[0], num, num2, 0, 0);
				}
			}
		}
		g.translate(0, -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x000071CA File Offset: 0x000053CA
	public void paint(mGraphics g)
	{
		if (!this.isRequest)
		{
			this.paintIcon(g);
		}
		else
		{
			this.paintPeans(g);
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0007E624 File Offset: 0x0007C824
	public void update()
	{
		if (TabClanIcon.scrMain != null)
		{
			TabClanIcon.scrMain.updatecm();
		}
		if (this.cmx != this.cmtoX)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		if (global::Math.abs(this.cmtoX - this.cmx) < 10)
		{
			this.cmx = this.cmtoX;
		}
		if (this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10)
		{
			this.isShow = false;
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0007E704 File Offset: 0x0007C904
	public void updateKey()
	{
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			this.left.performAction();
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			this.right.performAction();
		}
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			this.center.performAction();
		}
		if (!this.isGetName)
		{
			if (TabClanIcon.scrMain == null)
			{
				return;
			}
			if (GameCanvas.isTouch)
			{
				TabClanIcon.scrMain.updateKey();
				this.select = TabClanIcon.scrMain.selectedItem;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.select--;
				if (this.select < 0)
				{
					this.select = this.nItem - 1;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.select++;
				if (this.select > this.nItem - 1)
				{
					this.select = 0;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (this.select != -1)
			{
				this.lastSelect = this.select;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0007E910 File Offset: 0x0007CB10
	public void perform(int idAction, object p)
	{
		if (idAction == 2)
		{
			this.hide();
		}
		if (idAction == 1)
		{
			if (!this.isGetName)
			{
				if (!this.isRequest)
				{
					if (this.lastSelect >= 0)
					{
						this.hide();
						if (global::Char.myCharz().clan == null)
						{
							Service.gI().getClan(2, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
						}
						else
						{
							Service.gI().getClan(4, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
						}
					}
				}
				else if (this.lastSelect >= 0)
				{
					Item item = (Item)this.vItems.elementAt(this.select);
				}
			}
		}
	}

	// Token: 0x04000FDC RID: 4060
	private int x;

	// Token: 0x04000FDD RID: 4061
	private int y;

	// Token: 0x04000FDE RID: 4062
	private int w;

	// Token: 0x04000FDF RID: 4063
	private int h;

	// Token: 0x04000FE0 RID: 4064
	private Command left;

	// Token: 0x04000FE1 RID: 4065
	private Command right;

	// Token: 0x04000FE2 RID: 4066
	private Command center;

	// Token: 0x04000FE3 RID: 4067
	private int WIDTH = 24;

	// Token: 0x04000FE4 RID: 4068
	public int nItem;

	// Token: 0x04000FE5 RID: 4069
	private int disStart = 50;

	// Token: 0x04000FE6 RID: 4070
	public static Scroll scrMain;

	// Token: 0x04000FE7 RID: 4071
	public int cmtoX;

	// Token: 0x04000FE8 RID: 4072
	public int cmx;

	// Token: 0x04000FE9 RID: 4073
	public int cmvx;

	// Token: 0x04000FEA RID: 4074
	public int cmdx;

	// Token: 0x04000FEB RID: 4075
	public bool isShow;

	// Token: 0x04000FEC RID: 4076
	public bool isGetName;

	// Token: 0x04000FED RID: 4077
	public string text;

	// Token: 0x04000FEE RID: 4078
	private bool isRequest;

	// Token: 0x04000FEF RID: 4079
	private bool isUpdate;

	// Token: 0x04000FF0 RID: 4080
	public MyVector vItems = new MyVector();

	// Token: 0x04000FF1 RID: 4081
	private int msgID;

	// Token: 0x04000FF2 RID: 4082
	private int select;

	// Token: 0x04000FF3 RID: 4083
	private int lastSelect;

	// Token: 0x04000FF4 RID: 4084
	private ScrollResult sr;
}
