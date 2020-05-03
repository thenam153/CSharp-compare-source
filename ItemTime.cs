using System;

// Token: 0x0200005F RID: 95
public class ItemTime
{
	// Token: 0x0600033C RID: 828 RVA: 0x0000357C File Offset: 0x0000177C
	public ItemTime()
	{
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00018FD4 File Offset: 0x000171D4
	public ItemTime(short idIcon, int s)
	{
		this.idIcon = idIcon;
		this.minute = s / 60;
		this.second = s % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00019018 File Offset: 0x00017218
	public void initTimeText(sbyte id, string text, int time)
	{
		if (time == -1)
		{
			this.dontClear = true;
		}
		else
		{
			this.dontClear = false;
		}
		this.isText = true;
		this.minute = time / 60;
		this.second = time % 60;
		this.idIcon = (short)id;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.text = text;
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00019080 File Offset: 0x00017280
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isText = isText;
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000190BC File Offset: 0x000172BC
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00019104 File Offset: 0x00017304
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001914C File Offset: 0x0001734C
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00019194 File Offset: 0x00017394
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x000191DC File Offset: 0x000173DC
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00019214 File Offset: 0x00017414
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string st = string.Empty;
		st = this.minute + "'";
		if (this.minute == 0)
		{
			st = this.second + "s";
		}
		mFont.tahoma_7b_white.drawString(g, st, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00019288 File Offset: 0x00017488
	public void paintText(mGraphics g, int x, int y)
	{
		string str = string.Empty;
		str = this.minute + "'";
		if (this.minute < 1)
		{
			str = this.second + "s";
		}
		if (this.minute < 0)
		{
			str = string.Empty;
		}
		if (this.dontClear)
		{
			str = string.Empty;
		}
		mFont.tahoma_7b_white.drawString(g, this.text + " " + str, x, y, mFont.LEFT, mFont.tahoma_7b_dark);
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00019320 File Offset: 0x00017520
	public void update()
	{
		this.curr = mSystem.currentTimeMillis();
		if (this.curr - this.last >= 1000L)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			if (this.second <= 0)
			{
				this.second = 60;
				this.minute--;
			}
		}
		if (this.minute < 0 && !this.isText)
		{
			global::Char.vItemTime.removeElement(this);
		}
		if (this.minute < 0 && this.isText && !this.dontClear)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x04000563 RID: 1379
	public short idIcon;

	// Token: 0x04000564 RID: 1380
	public int second;

	// Token: 0x04000565 RID: 1381
	public int minute;

	// Token: 0x04000566 RID: 1382
	private long curr;

	// Token: 0x04000567 RID: 1383
	private long last;

	// Token: 0x04000568 RID: 1384
	private bool isText;

	// Token: 0x04000569 RID: 1385
	private bool dontClear;

	// Token: 0x0400056A RID: 1386
	private string text;
}
