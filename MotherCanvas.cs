using System;

// Token: 0x020000BA RID: 186
public class MotherCanvas
{
	// Token: 0x06000941 RID: 2369 RVA: 0x00007793 File Offset: 0x00005993
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00084218 File Offset: 0x00082418
	public void checkZoomLevel(int w, int h)
	{
		if (Main.isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h > 384000)
			{
				mGraphics.zoomLevel = 3;
			}
		}
		else if (!Main.isPC)
		{
			if (Main.isIpod)
			{
				mGraphics.zoomLevel = 2;
			}
			else if (w * h >= 2073600)
			{
				mGraphics.zoomLevel = 4;
			}
			else if (w * h >= 691200)
			{
				mGraphics.zoomLevel = 3;
			}
			else if (w * h > 153600)
			{
				mGraphics.zoomLevel = 3;
			}
		}
		else
		{
			mGraphics.zoomLevel = 2;
			if (w * h < 480000)
			{
				mGraphics.zoomLevel = 1;
			}
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0000740B File Offset: 0x0000560B
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00007413 File Offset: 0x00005613
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x000077BC File Offset: 0x000059BC
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000077C5 File Offset: 0x000059C5
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x000077D3 File Offset: 0x000059D3
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000077E1 File Offset: 0x000059E1
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000077EF File Offset: 0x000059EF
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00007810 File Offset: 0x00005A10
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00007831 File Offset: 0x00005A31
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000842E8 File Offset: 0x000824E8
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0008430C File Offset: 0x0008250C
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x04001145 RID: 4421
	public static MotherCanvas instance;

	// Token: 0x04001146 RID: 4422
	public GameCanvas tCanvas;

	// Token: 0x04001147 RID: 4423
	public int zoomLevel = 1;

	// Token: 0x04001148 RID: 4424
	public Image imgCache;

	// Token: 0x04001149 RID: 4425
	private int[] imgRGBCache;

	// Token: 0x0400114A RID: 4426
	private int newWidth;

	// Token: 0x0400114B RID: 4427
	private int newHeight;

	// Token: 0x0400114C RID: 4428
	private int[] output;

	// Token: 0x0400114D RID: 4429
	private int OUTPUTSIZE = 20;
}
