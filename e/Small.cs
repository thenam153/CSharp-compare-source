using System;

namespace Assets.src.e
{
	// Token: 0x0200007B RID: 123
	public class Small
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00005598 File Offset: 0x00003798
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001DEFC File Offset: 0x0001C0FC
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001DF60 File Offset: 0x0001C160
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001DF84 File Offset: 0x0001C184
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (mGraphics.getImageWidth(this.img) == 1)
			{
				return;
			}
			g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001DFEC File Offset: 0x0001C1EC
		public void update()
		{
			this.timeUpdate++;
			if (this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id))
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x040006A2 RID: 1698
		public Image img;

		// Token: 0x040006A3 RID: 1699
		public int id;

		// Token: 0x040006A4 RID: 1700
		public int timePaint;

		// Token: 0x040006A5 RID: 1701
		public int timeUpdate;
	}
}
