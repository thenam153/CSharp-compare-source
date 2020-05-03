using System;

// Token: 0x0200002F RID: 47
public class ClanMessage : IActionListener
{
	// Token: 0x0600020F RID: 527 RVA: 0x000116EC File Offset: 0x0000F8EC
	public static void addMessage(ClanMessage cm, int index, bool upToTop)
	{
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
			if (clanMessage.id == cm.id)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
				if (!upToTop)
				{
					ClanMessage.vMessage.insertElementAt(cm, i);
				}
				else
				{
					ClanMessage.vMessage.insertElementAt(cm, 0);
				}
				return;
			}
			if (clanMessage.maxCap != 0 && clanMessage.recieve == clanMessage.maxCap)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
			}
		}
		if (index == -1)
		{
			ClanMessage.vMessage.addElement(cm);
		}
		else
		{
			ClanMessage.vMessage.insertElementAt(cm, 0);
		}
		if (ClanMessage.vMessage.size() > 20)
		{
			ClanMessage.vMessage.removeElementAt(ClanMessage.vMessage.size() - 1);
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000117D8 File Offset: 0x0000F9D8
	public void paint(mGraphics g, int x, int y)
	{
		mFont mFont = mFont.tahoma_7b_dark;
		if ((int)this.role == 0)
		{
			mFont = mFont.tahoma_7b_red;
		}
		else if ((int)this.role == 1)
		{
			mFont = mFont.tahoma_7b_green;
		}
		else if ((int)this.role == 2)
		{
			mFont = mFont.tahoma_7b_green2;
		}
		if (this.type == 0)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			if ((int)this.color == 0)
			{
				mFont.tahoma_7_grey.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			else
			{
				mFont.tahoma_7_red.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			mFont.tahoma_7_grey.drawString(g, NinjaUtil.getTimeAgo(this.timeAgo) + " " + mResources.ago, x + GameCanvas.panel.wScroll - 3, y + 1, mFont.RIGHT);
		}
		if (this.type == 1)
		{
			mFont.drawString(g, string.Concat(new object[]
			{
				this.playerName,
				" (",
				this.recieve,
				"/",
				this.maxCap,
				")"
			}), x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
			{
				mResources.request_pea,
				" ",
				NinjaUtil.getTimeAgo(this.timeAgo),
				" ",
				mResources.ago
			}), x + 3, y + 11, 0);
		}
		if (this.type == 2)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, mResources.request_join_clan, x + 3, y + 11, 0);
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00003584 File Offset: 0x00001784
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000047D0 File Offset: 0x000029D0
	public void update()
	{
		if (this.time != 0L)
		{
			this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
		}
	}

	// Token: 0x040001E9 RID: 489
	public int id;

	// Token: 0x040001EA RID: 490
	public int type;

	// Token: 0x040001EB RID: 491
	public int playerId;

	// Token: 0x040001EC RID: 492
	public string playerName;

	// Token: 0x040001ED RID: 493
	public long time;

	// Token: 0x040001EE RID: 494
	public int headId;

	// Token: 0x040001EF RID: 495
	public string[] chat;

	// Token: 0x040001F0 RID: 496
	public sbyte color;

	// Token: 0x040001F1 RID: 497
	public sbyte role;

	// Token: 0x040001F2 RID: 498
	private int timeAgo;

	// Token: 0x040001F3 RID: 499
	public int recieve;

	// Token: 0x040001F4 RID: 500
	public int maxCap;

	// Token: 0x040001F5 RID: 501
	public string[] option;

	// Token: 0x040001F6 RID: 502
	public static MyVector vMessage = new MyVector();
}
