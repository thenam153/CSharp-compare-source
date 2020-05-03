using System;

namespace Assets.src.g
{
	// Token: 0x02000096 RID: 150
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0004A714 File Offset: 0x00048914
		private void init(string t)
		{
			this.w = GameCanvas.w - 20;
			if (this.w > 320)
			{
				this.w = 320;
			}
			Res.outz("title= " + t);
			this.strPaint = mFont.tahoma_7b_dark.splitFontArray(t, this.w - 20);
			this.x = (GameCanvas.w - this.w) / 2;
			this.tf = new TField[this.nTf];
			this.h = this.tf.Length * 35 + (this.strPaint.Length - 1) * 20 + 40;
			this.y = GameCanvas.h - this.h - 40 - (this.strPaint.Length - 1) * 20;
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i] = new TField();
				this.tf[i].name = string.Empty;
				this.tf[i].x = this.x + 10;
				this.tf[i].y = this.y + 35 + (this.strPaint.Length - 1) * 20 + i * 35;
				this.tf[i].width = this.w - 20;
				this.tf[i].height = mScreen.ITEM_HEIGHT + 2;
				if (GameCanvas.isTouch)
				{
					this.tf[0].isFocus = false;
				}
				else
				{
					this.tf[0].isFocus = true;
				}
				if (!GameCanvas.isTouch)
				{
					this.right = this.tf[0].cmdClear;
				}
			}
			this.left = new Command(mResources.CLOSE, this, 1, null);
			this.center = new Command(mResources.OK, this, 2, null);
			if (GameCanvas.isTouch)
			{
				this.center.x = GameCanvas.w / 2 + 18;
				this.left.x = GameCanvas.w / 2 - 85;
				this.center.y = (this.left.y = this.y + this.h + 5);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00006345 File Offset: 0x00004545
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00006360 File Offset: 0x00004560
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000636F File Offset: 0x0000456F
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0004A94C File Offset: 0x00048B4C
		public override void paint(mGraphics g)
		{
			GameScr.gI().paint(g);
			PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
			for (int i = 0; i < this.strPaint.Length; i++)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
			}
			for (int j = 0; j < this.tf.Length; j++)
			{
				this.tf[j].paint(g);
			}
			base.paint(g);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0004A9F8 File Offset: 0x00048BF8
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0004AA38 File Offset: 0x00048C38
		public override void keyPress(int keyCode)
		{
			for (int i = 0; i < this.tf.Length; i++)
			{
				if (this.tf[i].isFocus)
				{
					this.tf[i].keyPressed(keyCode);
					break;
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0004AA8C File Offset: 0x00048C8C
		public override void updateKey()
		{
			if (GameCanvas.keyPressed[2])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = this.tf.Length - 1;
				}
			}
			else if (GameCanvas.keyPressed[8])
			{
				this.focus++;
				if (this.focus > this.tf.Length - 1)
				{
					this.focus = 0;
				}
			}
			if (GameCanvas.keyPressed[2] || GameCanvas.keyPressed[8])
			{
				GameCanvas.clearKeyPressed();
				for (int i = 0; i < this.tf.Length; i++)
				{
					if (this.focus == i)
					{
						this.tf[i].isFocus = true;
						if (!GameCanvas.isTouch)
						{
							this.right = this.tf[i].cmdClear;
						}
					}
					else
					{
						this.tf[i].isFocus = false;
					}
					if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(this.tf[i].x, this.tf[i].y, this.tf[i].width, this.tf[i].height))
					{
						this.focus = i;
						break;
					}
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00006385 File Offset: 0x00004585
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0004ABEC File Offset: 0x00048DEC
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
			if (idAction == 2)
			{
				for (int i = 0; i < this.tf.Length; i++)
				{
					if (this.tf[i].getText() == null || this.tf[i].getText().Equals(string.Empty))
					{
						GameCanvas.startOKDlg(mResources.vuilongnhapduthongtin);
						return;
					}
				}
				Service.gI().sendClientInput(this.tf);
				GameScr.instance.switchToMe();
			}
		}

		// Token: 0x04000AE1 RID: 2785
		public static ClientInput instance;

		// Token: 0x04000AE2 RID: 2786
		public TField[] tf;

		// Token: 0x04000AE3 RID: 2787
		private int x;

		// Token: 0x04000AE4 RID: 2788
		private int y;

		// Token: 0x04000AE5 RID: 2789
		private int w;

		// Token: 0x04000AE6 RID: 2790
		private int h;

		// Token: 0x04000AE7 RID: 2791
		private string[] strPaint;

		// Token: 0x04000AE8 RID: 2792
		private int focus;

		// Token: 0x04000AE9 RID: 2793
		private int nTf;
	}
}
