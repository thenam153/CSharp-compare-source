using System;
using System.Collections;
using Assets.src.e;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class mGraphics
{
	// Token: 0x06000193 RID: 403 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
	private void cache(string key, Texture value)
	{
		if (mGraphics.cachedTextures.Count > 400)
		{
			mGraphics.cachedTextures.Clear();
		}
		if (value.width * value.height < GameCanvas.w * GameCanvas.h)
		{
			mGraphics.cachedTextures.Add(key, value);
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000EF38 File Offset: 0x0000D138
	public void translate(int tx, int ty)
	{
		tx *= mGraphics.zoomLevel;
		ty *= mGraphics.zoomLevel;
		this.translateX += tx;
		this.translateY += ty;
		this.isTranslate = true;
		if (this.translateX == 0 && this.translateY == 0)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000EF98 File Offset: 0x0000D198
	public void translate(float x, float y)
	{
		this.translateXf += x;
		this.translateYf += y;
		this.isTranslate = true;
		if (this.translateXf == 0f && this.translateYf == 0f)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000044CC File Offset: 0x000026CC
	public int getTranslateX()
	{
		return this.translateX / mGraphics.zoomLevel;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000044DA File Offset: 0x000026DA
	public int getTranslateY()
	{
		return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
	public void setClip(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		this.clipTX = this.translateX;
		this.clipTY = this.translateY;
		this.clipX = x;
		this.clipY = y;
		this.clipW = w;
		this.clipH = h;
		this.isClip = true;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000044EE File Offset: 0x000026EE
	public int getClipX()
	{
		return GameScr.cmx;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000044F5 File Offset: 0x000026F5
	public int getClipY()
	{
		return GameScr.cmy;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x000044FC File Offset: 0x000026FC
	public int getClipWidth()
	{
		return GameScr.gW;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00004503 File Offset: 0x00002703
	public int getClipHeight()
	{
		return GameScr.gH;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000F060 File Offset: 0x0000D260
	public void fillRect(int x, int y, int w, int h, int color, int alpha)
	{
		float alpha2 = 0.5f;
		this.setColor(color, alpha2);
		this.fillRect(x, y, w, h);
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000F088 File Offset: 0x0000D288
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		x1 *= mGraphics.zoomLevel;
		y1 *= mGraphics.zoomLevel;
		x2 *= mGraphics.zoomLevel;
		y2 *= mGraphics.zoomLevel;
		if (y1 == y2)
		{
			if (x1 > x2)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			this.fillRect(x1, y1, x2 - x1, 1);
			return;
		}
		if (x1 == x2)
		{
			if (y1 > y2)
			{
				int num2 = y2;
				y2 = y1;
				y1 = num2;
			}
			this.fillRect(x1, y1, 1, y2 - y1);
			return;
		}
		if (this.isTranslate)
		{
			x1 += this.translateX;
			y1 += this.translateY;
			x2 += this.translateX;
			y2 += this.translateY;
		}
		string key = string.Concat(new object[]
		{
			"dl",
			this.r,
			this.g,
			this.b
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
		if (texture2D == null)
		{
			texture2D = new Texture2D(1, 1);
			Color color = new Color(this.r, this.g, this.b);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			this.cache(key, texture2D);
		}
		Vector2 pivotPoint = new Vector2((float)x1, (float)y1);
		Vector2 vector = new Vector2((float)x2, (float)y2);
		Vector2 vector2 = vector - pivotPoint;
		float num3 = 57.29578f * Mathf.Atan(vector2.y / vector2.x);
		if (vector2.x < 0f)
		{
			num3 += 180f;
		}
		int num4 = (int)Mathf.Ceil(0f);
		GUIUtility.RotateAroundPivot(num3, pivotPoint);
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (this.isClip)
		{
			num5 = this.clipX;
			num6 = this.clipY;
			num7 = this.clipW;
			num8 = this.clipH;
			if (this.isTranslate)
			{
				num5 += this.clipTX;
				num6 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
		}
		Graphics.DrawTexture(new Rect(pivotPoint.x - (float)num5, pivotPoint.y - (float)num4 - (float)num6, vector2.magnitude, 1f), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
		GUIUtility.RotateAroundPivot(-num3, pivotPoint);
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000E014 File Offset: 0x0000C214
	public Color setColorMiniMap(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color result = new Color(num6, num5, num4);
		return result;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000F300 File Offset: 0x0000D500
	public float[] getRGB(Color cl)
	{
		float num = 256f * cl.r;
		float num2 = 256f * cl.g;
		float num3 = 256f * cl.b;
		return new float[]
		{
			num,
			num2,
			num3
		};
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000F34C File Offset: 0x0000D54C
	public void drawRect(int x, int y, int w, int h)
	{
		int num = 1;
		this.fillRect(x, y, w, num);
		this.fillRect(x, y, num, h);
		this.fillRect(x + w, y, num, h + 1);
		this.fillRect(x, y + h, w + 1, num);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000F390 File Offset: 0x0000D590
	public void fillRect(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		if (w < 0 || h < 0)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 1;
		int num2 = 1;
		string key = string.Concat(new object[]
		{
			"fr",
			num,
			num2,
			this.r,
			this.g,
			this.b,
			this.a
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
		if (texture2D == null)
		{
			texture2D = new Texture2D(num, num2);
			Color color = new Color(this.r, this.g, this.b, this.a);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			this.cache(key, texture2D);
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (this.isClip)
		{
			num3 = this.clipX;
			num4 = this.clipY;
			num5 = this.clipW;
			num6 = this.clipH;
			if (this.isTranslate)
			{
				num3 += this.clipTX;
				num4 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
		}
		GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000F554 File Offset: 0x0000D754
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000450A File Offset: 0x0000270A
	public void setColor(Color color)
	{
		this.b = color.b;
		this.g = color.g;
		this.r = color.r;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
	public void setBgColor(int rgb)
	{
		if (rgb != this.currentBGColor)
		{
			this.currentBGColor = rgb;
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			Main.main.GetComponent<Camera>().backgroundColor = new Color(this.r, this.g, this.b);
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000F644 File Offset: 0x0000D844
	public void drawString(string s, int x, int y, GUIStyle style)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000F71C File Offset: 0x0000D91C
	public void setColor(int rgb, float alpha)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = alpha;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000F778 File Offset: 0x0000D978
	public void drawString(string s, int x, int y, GUIStyle style, int w)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2 - 4), (float)w, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000F850 File Offset: 0x0000DA50
	private void UpdatePos(int anchor)
	{
		Vector2 vector = new Vector2(0f, 0f);
		switch (anchor)
		{
		case 3:
			vector = new Vector2(this.size.x / 2f, this.size.y / 2f);
			break;
		default:
			switch (anchor)
			{
			case 17:
				vector = new Vector2((float)(Screen.width / 2), 0f);
				break;
			default:
				switch (anchor)
				{
				case 33:
					vector = new Vector2((float)(Screen.width / 2), (float)Screen.height);
					break;
				default:
					if (anchor != 10)
					{
						if (anchor != 24)
						{
							if (anchor == 40)
							{
								vector = new Vector2((float)Screen.width, (float)Screen.height);
							}
						}
						else
						{
							vector = new Vector2((float)Screen.width, 0f);
						}
					}
					else
					{
						vector = new Vector2((float)Screen.width, (float)(Screen.height / 2));
					}
					break;
				case 36:
					vector = new Vector2(0f, (float)Screen.height);
					break;
				}
				break;
			case 20:
				vector = new Vector2(0f, 0f);
				break;
			}
			break;
		case 6:
			vector = new Vector2(0f, (float)(Screen.height / 2));
			break;
		}
		this.pos = vector + this.relativePosition;
		this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
		this.pivot = new Vector2(this.rect.xMin + this.rect.width * 0.5f, this.rect.yMin + this.rect.height * 0.5f);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000FA80 File Offset: 0x0000DC80
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		x0 *= mGraphics.zoomLevel;
		y0 *= mGraphics.zoomLevel;
		w0 *= mGraphics.zoomLevel;
		h0 *= mGraphics.zoomLevel;
		this._drawRegion(arg0, (float)x0, (float)y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= mGraphics.zoomLevel;
		y0 *= mGraphics.zoomLevel;
		w0 *= mGraphics.zoomLevel;
		h0 *= mGraphics.zoomLevel;
		this.__drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000FB40 File Offset: 0x0000DD40
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
	{
		this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000FB64 File Offset: 0x0000DD64
	public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += (float)this.translateX;
			y += (float)this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		int num9 = 1;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += num5;
		y += num6;
		int num10 = 0;
		int num11 = 0;
		if (this.isClip)
		{
			num10 = this.clipX;
			int num12 = this.clipY;
			num11 = this.clipW;
			int num13 = this.clipH;
			if (this.isTranslate)
			{
				num10 += this.clipTX;
				num12 += this.clipTY;
			}
			Rect r = new Rect(x, y, (float)w, (float)h);
			Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		float num14 = 0f;
		float num15 = 0f;
		if (transform == 2)
		{
			num14 += num;
			num7 = -1f;
			if (this.isClip)
			{
				if ((float)num10 > x)
				{
					num8 = -num3;
				}
				else if ((float)(num10 + num11) < x + (float)w)
				{
					num8 = -((float)(num10 + num11) - x - (float)w);
				}
			}
		}
		else if (transform == 1)
		{
			num9 = -1;
			num15 += num2;
		}
		else if (transform == 3)
		{
			num9 = -1;
			num15 += num2;
			num7 = -1f;
			num14 += num;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			this.matrixBackup = GUI.matrix;
			this.size = new Vector2((float)w, (float)h);
			this.relativePosition = new Vector2(x, y);
			this.UpdatePos(3);
			if (transform == 6)
			{
				this.UpdatePos(3);
			}
			else if (transform == 5)
			{
				this.size = new Vector2((float)w, (float)h);
				this.UpdatePos(3);
			}
			if (transform == 5)
			{
				GUIUtility.RotateAroundPivot(90f, this.pivot);
			}
			else if (transform == 6)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
			}
			else if (transform == 4)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if ((float)num10 > x)
					{
						num8 = -num3;
					}
					else if ((float)(num10 + num11) < x + (float)w)
					{
						num8 = -((float)(num10 + num11) - x - (float)w);
					}
				}
			}
			else if (transform == 7)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num9 = -1;
				num15 += num2;
			}
		}
		Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect(((float)x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - ((float)y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = this.matrixBackup;
		}
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		int num9 = 1;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += (int)num5;
		y += (int)num6;
		int num10 = 0;
		int num11 = 0;
		if (this.isClip)
		{
			num10 = this.clipX;
			int num12 = this.clipY;
			num11 = this.clipW;
			int num13 = this.clipH;
			if (this.isTranslate)
			{
				num10 += this.clipTX;
				num12 += this.clipTY;
			}
			Rect r = new Rect((float)x, (float)y, (float)w, (float)h);
			Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
			Rect rect = this.intersectRect(r, r2);
			if (rect.width <= 0f || rect.height <= 0f)
			{
				return;
			}
			num = rect.width;
			num2 = rect.height;
			num3 = rect.x - r.x;
			num4 = rect.y - r.y;
		}
		float num14 = 0f;
		float num15 = 0f;
		if (transform == 2)
		{
			num14 += num;
			num7 = -1f;
			if (this.isClip)
			{
				if (num10 > x)
				{
					num8 = -num3;
				}
				else if (num10 + num11 < x + w)
				{
					num8 = (float)(-(float)(num10 + num11 - x - w));
				}
			}
		}
		else if (transform == 1)
		{
			num9 = -1;
			num15 += num2;
		}
		else if (transform == 3)
		{
			num9 = -1;
			num15 += num2;
			num7 = -1f;
			num14 += num;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			this.matrixBackup = GUI.matrix;
			this.size = new Vector2((float)w, (float)h);
			this.relativePosition = new Vector2((float)x, (float)y);
			this.UpdatePos(3);
			if (transform == 6)
			{
				this.UpdatePos(3);
			}
			else if (transform == 5)
			{
				this.size = new Vector2((float)w, (float)h);
				this.UpdatePos(3);
			}
			if (transform == 5)
			{
				GUIUtility.RotateAroundPivot(90f, this.pivot);
			}
			else if (transform == 6)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
			}
			else if (transform == 4)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if (num10 > x)
					{
						num8 = -num3;
					}
					else if (num10 + num11 < x + w)
					{
						num8 = (float)(-(float)(num10 + num11 - x - w));
					}
				}
			}
			else if (transform == 7)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num9 = -1;
				num15 += num2;
			}
		}
		Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = this.matrixBackup;
		}
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00010424 File Offset: 0x0000E624
	public void drawRegionGui(Image image, float x0, float y0, int w, int h, int transform, float x, float y, int anchor)
	{
		GUI.color = this.setColorMiniMap(807956);
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= (float)mGraphics.zoomLevel;
		y0 *= (float)mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00010480 File Offset: 0x0000E680
	public void drawRegion2(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		GUI.color = image.colorBlend;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		string key = string.Concat(new object[]
		{
			"dg",
			x0,
			y0,
			w,
			h,
			transform,
			image.GetHashCode()
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
		if (texture2D == null)
		{
			Image image2 = Image.createImage(image, (int)x0, (int)y0, w, h, transform);
			texture2D = image2.texture;
			this.cache(key, texture2D);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)w;
		float num6 = (float)h;
		float num7 = 0f;
		float num8 = 0f;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num7 -= num5 / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num8 -= num6 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num7 -= num5;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num8 -= num6;
		}
		x += (int)num7;
		y += (int)num8;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.DrawTexture(new Rect((float)(x - num), (float)(y - num2), (float)w, (float)h), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000106A0 File Offset: 0x0000E8A0
	public void drawImagaByDrawTexture(Image image, float x, float y)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		GUI.DrawTexture(new Rect(x + (float)this.translateX, y + (float)this.translateY, (float)image.getRealImageWidth(), (float)image.getRealImageHeight()), image.texture);
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000106F4 File Offset: 0x0000E8F4
	public void drawImage(Image image, int x, int y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00010724 File Offset: 0x0000E924
	public void drawImageFog(Image image, int x, int y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0001075C File Offset: 0x0000E95C
	public void drawImage(Image image, int x, int y)
	{
		if (image == null)
		{
			return;
		}
		this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00010794 File Offset: 0x0000E994
	public void drawImage(Image image, float x, float y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00004533 File Offset: 0x00002733
	public void drawRoundRect(int x, int y, int w, int h, int arcWidth, int arcHeight)
	{
		this.drawRect(x, y, w, h);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00004540 File Offset: 0x00002740
	public void fillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
	{
		this.fillRect(x, y, width, height);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000454D File Offset: 0x0000274D
	public void reset()
	{
		this.isClip = false;
		this.isTranslate = false;
		this.translateX = 0;
		this.translateY = 0;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000107C4 File Offset: 0x0000E9C4
	public Rect intersectRect(Rect r1, Rect r2)
	{
		float num = r1.x;
		float num2 = r1.y;
		float x = r2.x;
		float y = r2.y;
		float num3 = num;
		num3 += r1.width;
		float num4 = num2;
		num4 += r1.height;
		float num5 = x;
		num5 += r2.width;
		float num6 = y;
		num6 += r2.height;
		if (num < x)
		{
			num = x;
		}
		if (num2 < y)
		{
			num2 = y;
		}
		if (num3 > num5)
		{
			num3 = num5;
		}
		if (num4 > num6)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		if (num3 < -30000f)
		{
			num3 = -30000f;
		}
		if (num4 < -30000f)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0001089C File Offset: 0x0000EA9C
	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		GUI.color = Color.red;
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)(x + this.translateX), (float)(y + this.translateY), (float)((tranform != 0) ? (-(float)w) : w), (float)h), image.texture);
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000456B File Offset: 0x0000276B
	public void drawImageSimple(Image image, int x, int y)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), image.texture);
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000375D File Offset: 0x0000195D
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00003765 File Offset: 0x00001965
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x000045A7 File Offset: 0x000027A7
	public static bool isNotTranColor(Color color)
	{
		return !(color == Color.clear) && !(color == mGraphics.transParentColor);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00010918 File Offset: 0x0000EB18
	public static Image blend(Image img0, float level, int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color color = new Color(num6, num5, num4);
		Color[] pixels = img0.texture.GetPixels();
		float num7 = color.r;
		float num8 = color.g;
		float num9 = color.b;
		for (int i = 0; i < pixels.Length; i++)
		{
			Color color2 = pixels[i];
			if (mGraphics.isNotTranColor(color2))
			{
				float num10 = (num7 - color2.r) * level + color2.r;
				float num11 = (num8 - color2.g) * level + color2.g;
				float num12 = (num9 - color2.b) * level + color2.b;
				if (num10 > 255f)
				{
					num10 = 255f;
				}
				if (num10 < 0f)
				{
					num10 = 0f;
				}
				if (num11 > 255f)
				{
					num11 = 255f;
				}
				if (num11 < 0f)
				{
					num11 = 0f;
				}
				if (num12 < 0f)
				{
					num12 = 0f;
				}
				if (num12 > 255f)
				{
					num12 = 255f;
				}
				pixels[i].r = num10;
				pixels[i].g = num11;
				pixels[i].b = num12;
			}
		}
		Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
		image.texture.SetPixels(pixels);
		Image.setTextureQuality(image.texture);
		image.texture.Apply();
		Cout.LogError2("BLEND ----------------------------------------------------");
		return image;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00007B70 File Offset: 0x00005D70
	public static Color setColorObj(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color result = new Color(num6, num5, num4);
		return result;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x000045CC File Offset: 0x000027CC
	public void fillTrans(Image imgTrans, int x, int y, int w, int h)
	{
		this.setColor(0, 0.5f);
		this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00010AF4 File Offset: 0x0000ECF4
	public static int blendColor(float level, int color, int colorBlend)
	{
		Color color2 = mGraphics.setColorObj(colorBlend);
		float num = color2.r * 255f;
		float num2 = color2.g * 255f;
		float num3 = color2.b * 255f;
		Color color3 = mGraphics.setColorObj(color);
		float num4 = (num + color3.r) * level + color3.r;
		float num5 = (num2 + color3.g) * level + color3.g;
		float num6 = (num3 + color3.b) * level + color3.b;
		if (num4 > 255f)
		{
			num4 = 255f;
		}
		if (num4 < 0f)
		{
			num4 = 0f;
		}
		if (num5 > 255f)
		{
			num5 = 255f;
		}
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		if (num6 < 0f)
		{
			num6 = 0f;
		}
		if (num6 > 255f)
		{
			num6 = 255f;
		}
		return (int)num6 & 255 + ((int)num5 << 8) & 255 + ((int)num4 << 16) & 255;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00010C14 File Offset: 0x0000EE14
	public static int getIntByColor(Color cl)
	{
		float num = cl.r * 255f;
		float num2 = cl.b * 255f;
		float num3 = cl.g * 255f;
		return ((int)num & 255) << 16 | ((int)num3 & 255) << 8 | ((int)num2 & 255);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000037C5 File Offset: 0x000019C5
	public static int getRealImageWidth(Image img)
	{
		return img.w;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x000037CD File Offset: 0x000019CD
	public static int getRealImageHeight(Image img)
	{
		return img.h;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000045FE File Offset: 0x000027FE
	public void fillArg(int i, int j, int k, int l, int m, int n)
	{
		this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00010C70 File Offset: 0x0000EE70
	public void CreateLineMaterial()
	{
		if (!this.lineMaterial)
		{
			this.lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {  Blend SrcAlpha OneMinusSrcAlpha  ZWrite Off Cull Off Fog { Mode Off }  BindChannels { Bind \"vertex\", vertex Bind \"color\", color }} } }");
			this.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00010CBC File Offset: 0x0000EEBC
	public void drawlineGL(MyVector totalLine)
	{
		this.lineMaterial.SetPass(0);
		GL.PushMatrix();
		GL.Begin(1);
		for (int i = 0; i < totalLine.size(); i++)
		{
			mLine mLine = (mLine)totalLine.elementAt(i);
			GL.Color(new Color(mLine.r, mLine.g, mLine.b, mLine.a));
			int num = mLine.x1 * mGraphics.zoomLevel;
			int num2 = mLine.y1 * mGraphics.zoomLevel;
			int num3 = mLine.x2 * mGraphics.zoomLevel;
			int num4 = mLine.y2 * mGraphics.zoomLevel;
			if (this.isTranslate)
			{
				num += this.translateX;
				num2 += this.translateY;
				num3 += this.translateX;
				num4 += this.translateY;
			}
			for (int j = 0; j < mGraphics.zoomLevel; j++)
			{
				GL.Vertex(new Vector2((float)(num + j), (float)(num2 + j)));
				GL.Vertex(new Vector2((float)(num3 + j), (float)(num4 + j)));
				if (j > 0)
				{
					GL.Vertex(new Vector2((float)(num + j), (float)num2));
					GL.Vertex(new Vector2((float)(num3 + j), (float)num4));
					GL.Vertex(new Vector2((float)num, (float)(num2 + j)));
					GL.Vertex(new Vector2((float)num3, (float)(num4 + j)));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		totalLine.removeAllElements();
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00010E54 File Offset: 0x0000F054
	public void drawLine(mGraphics g, int x, int y, int xTo, int yTo, int nLine, int color)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < nLine; i++)
		{
			myVector.addElement(new mLine(x, y, xTo + i, yTo + i, color));
		}
		g.drawlineGL(myVector);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00003669 File Offset: 0x00001869
	internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000198 RID: 408
	public const int BASELINE = 64;

	// Token: 0x04000199 RID: 409
	public const int SOLID = 0;

	// Token: 0x0400019A RID: 410
	public const int DOTTED = 1;

	// Token: 0x0400019B RID: 411
	public const int TRANS_MIRROR = 2;

	// Token: 0x0400019C RID: 412
	public const int TRANS_MIRROR_ROT180 = 1;

	// Token: 0x0400019D RID: 413
	public const int TRANS_MIRROR_ROT270 = 4;

	// Token: 0x0400019E RID: 414
	public const int TRANS_MIRROR_ROT90 = 7;

	// Token: 0x0400019F RID: 415
	public const int TRANS_NONE = 0;

	// Token: 0x040001A0 RID: 416
	public const int TRANS_ROT180 = 3;

	// Token: 0x040001A1 RID: 417
	public const int TRANS_ROT270 = 6;

	// Token: 0x040001A2 RID: 418
	public const int TRANS_ROT90 = 5;

	// Token: 0x040001A3 RID: 419
	public static int HCENTER = 1;

	// Token: 0x040001A4 RID: 420
	public static int VCENTER = 2;

	// Token: 0x040001A5 RID: 421
	public static int LEFT = 4;

	// Token: 0x040001A6 RID: 422
	public static int RIGHT = 8;

	// Token: 0x040001A7 RID: 423
	public static int TOP = 16;

	// Token: 0x040001A8 RID: 424
	public static int BOTTOM = 32;

	// Token: 0x040001A9 RID: 425
	private float r;

	// Token: 0x040001AA RID: 426
	private float g;

	// Token: 0x040001AB RID: 427
	private float b;

	// Token: 0x040001AC RID: 428
	private float a;

	// Token: 0x040001AD RID: 429
	public int clipX;

	// Token: 0x040001AE RID: 430
	public int clipY;

	// Token: 0x040001AF RID: 431
	public int clipW;

	// Token: 0x040001B0 RID: 432
	public int clipH;

	// Token: 0x040001B1 RID: 433
	private bool isClip;

	// Token: 0x040001B2 RID: 434
	private bool isTranslate = true;

	// Token: 0x040001B3 RID: 435
	private int translateX;

	// Token: 0x040001B4 RID: 436
	private int translateY;

	// Token: 0x040001B5 RID: 437
	private float translateXf;

	// Token: 0x040001B6 RID: 438
	private float translateYf;

	// Token: 0x040001B7 RID: 439
	public static int zoomLevel = 1;

	// Token: 0x040001B8 RID: 440
	public static Hashtable cachedTextures = new Hashtable();

	// Token: 0x040001B9 RID: 441
	public static int addYWhenOpenKeyBoard;

	// Token: 0x040001BA RID: 442
	private int clipTX;

	// Token: 0x040001BB RID: 443
	private int clipTY;

	// Token: 0x040001BC RID: 444
	private int currentBGColor;

	// Token: 0x040001BD RID: 445
	private Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x040001BE RID: 446
	private Rect rect;

	// Token: 0x040001BF RID: 447
	private Matrix4x4 matrixBackup;

	// Token: 0x040001C0 RID: 448
	private Vector2 pivot;

	// Token: 0x040001C1 RID: 449
	public Vector2 size = new Vector2(128f, 128f);

	// Token: 0x040001C2 RID: 450
	public Vector2 relativePosition = new Vector2(0f, 0f);

	// Token: 0x040001C3 RID: 451
	public Color clTrans;

	// Token: 0x040001C4 RID: 452
	public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x040001C5 RID: 453
	private Material lineMaterial;
}
