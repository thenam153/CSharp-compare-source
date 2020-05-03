using System;

// Token: 0x02000061 RID: 97
public class MagicTree : Npc, IActionListener
{
	// Token: 0x0600034B RID: 843 RVA: 0x00019470 File Offset: 0x00017670
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000194C0 File Offset: 0x000176C0
	public override void paint(mGraphics g)
	{
		if (this.id == 0)
		{
			return;
		}
		SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
		if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
			if (this.name != null)
			{
				mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
		else if (this.name != null)
		{
			mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
		}
		try
		{
			for (int i = 0; i < this.currPeas; i++)
			{
				g.drawImage(MagicTree.pea, this.cx + this.peaPostionX[i] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[i] - SmallImage.smallImg[this.id][4], 0);
			}
		}
		catch (Exception ex)
		{
		}
		if (this.indexEffTask >= 0 && this.effTask != null && (int)this.cTypePk == 0)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx + SmallImage.smallImg[this.id][3] / 2 + 5, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00019734 File Offset: 0x00017934
	public override void update()
	{
		this.p.isPaint = MagicTree.isPaint;
		this.cur = mSystem.currentTimeMillis();
		if (this.cur - this.last >= 1000L)
		{
			this.seconds--;
			this.last = this.cur;
			if (this.seconds < 0)
			{
				this.seconds = 0;
			}
		}
		if (!this.isUpdate)
		{
			if (this.currPeas < this.maxPeas && this.seconds == 0)
			{
				this.waitToUpdate = true;
			}
		}
		else if (this.seconds == 0)
		{
			this.isUpdate = false;
			this.waitToUpdate = true;
		}
		if (this.waitToUpdate)
		{
			this.delay++;
			if (this.delay == 20)
			{
				this.delay = 0;
				this.waitToUpdate = false;
				Service.gI().getMagicTree(2);
			}
		}
		this.num = ((this.peaPostionX == null) ? 0 : (this.peaPostionX.Length * this.currPeas / this.maxPeas));
		if (this.isUpdateTree)
		{
			this.isUpdateTree = false;
			if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas + "/" + this.maxPeas),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else if (this.currPeas == this.maxPeas && !this.isUpdate)
			{
				this.p.updateXYWH(new string[]
				{
					mResources.can_harvest,
					this.currPeas + "/" + this.maxPeas
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
		}
		if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate))
		{
			this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
		}
		if (this.isPeasEffect)
		{
			this.p.isPaint = false;
			ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
			this.currPeas--;
			if (GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().HP_MPup();
			}
			if (this.currPeas == this.remainPeas)
			{
				this.p.isPaint = true;
				this.isUpdateTree = true;
				this.isPeasEffect = false;
			}
		}
		base.update();
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00004F8E File Offset: 0x0000318E
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x04000580 RID: 1408
	public static Image imgMagicTree;

	// Token: 0x04000581 RID: 1409
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x04000582 RID: 1410
	public int id;

	// Token: 0x04000583 RID: 1411
	public int level;

	// Token: 0x04000584 RID: 1412
	public int x;

	// Token: 0x04000585 RID: 1413
	public int y;

	// Token: 0x04000586 RID: 1414
	public int currPeas;

	// Token: 0x04000587 RID: 1415
	public int remainPeas;

	// Token: 0x04000588 RID: 1416
	public int maxPeas;

	// Token: 0x04000589 RID: 1417
	public new string strInfo;

	// Token: 0x0400058A RID: 1418
	public string name;

	// Token: 0x0400058B RID: 1419
	public int timeToRecieve;

	// Token: 0x0400058C RID: 1420
	public bool isUpdate;

	// Token: 0x0400058D RID: 1421
	public int[] peaPostionX;

	// Token: 0x0400058E RID: 1422
	public int[] peaPostionY;

	// Token: 0x0400058F RID: 1423
	private int num;

	// Token: 0x04000590 RID: 1424
	public PopUp p;

	// Token: 0x04000591 RID: 1425
	public bool isUpdateTree;

	// Token: 0x04000592 RID: 1426
	public new static bool isPaint = true;

	// Token: 0x04000593 RID: 1427
	public bool isPeasEffect;

	// Token: 0x04000594 RID: 1428
	public new int seconds;

	// Token: 0x04000595 RID: 1429
	public new long last;

	// Token: 0x04000596 RID: 1430
	public new long cur;

	// Token: 0x04000597 RID: 1431
	private int wPopUp;

	// Token: 0x04000598 RID: 1432
	private bool waitToUpdate;

	// Token: 0x04000599 RID: 1433
	private int delay;
}
