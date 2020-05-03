using System;

// Token: 0x02000098 RID: 152
public abstract class Dialog
{
	// Token: 0x0600061D RID: 1565 RVA: 0x0004C4AC File Offset: 0x0004A6AC
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0004C508 File Offset: 0x0004A708
	public virtual void keyPress(int keyCode)
	{
		switch (keyCode + 7)
		{
		case 0:
			goto IL_CC;
		case 1:
			goto IL_B9;
		case 2:
			goto IL_DF;
		default:
			if (keyCode == -39)
			{
				goto IL_86;
			}
			if (keyCode != -38)
			{
				if (keyCode == -22)
				{
					goto IL_CC;
				}
				if (keyCode == -21)
				{
					goto IL_B9;
				}
				if (keyCode != 10)
				{
					return;
				}
				goto IL_DF;
			}
			break;
		case 5:
			goto IL_86;
		case 6:
			break;
		}
		GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
		return;
		IL_86:
		GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
		return;
		IL_B9:
		GameCanvas.keyHold[12] = true;
		GameCanvas.keyPressed[12] = true;
		return;
		IL_CC:
		GameCanvas.keyHold[13] = true;
		GameCanvas.keyPressed[13] = true;
		return;
		IL_DF:
		GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0004C628 File Offset: 0x0004A828
	public virtual void update()
	{
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.left != null)
			{
				this.left.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			mScreen.keyTouch = -1;
			if (this.right != null)
			{
				this.right.performAction();
			}
			mScreen.keyTouch = -1;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00003584 File Offset: 0x00001784
	public virtual void show()
	{
	}

	// Token: 0x04000AFE RID: 2814
	public Command left;

	// Token: 0x04000AFF RID: 2815
	public Command center;

	// Token: 0x04000B00 RID: 2816
	public Command right;

	// Token: 0x04000B01 RID: 2817
	private int lenCaption;
}
