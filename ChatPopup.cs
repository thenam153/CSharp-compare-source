using System;

// Token: 0x02000033 RID: 51
public class ChatPopup : Effect2, IActionListener
{
	// Token: 0x06000231 RID: 561 RVA: 0x00004866 File Offset: 0x00002A66
	public static void addNextPopUpMultiLine(string strNext, Npc next)
	{
		ChatPopup.nextMultiChatPopUp = strNext;
		ChatPopup.nextChar = next;
		if (ChatPopup.currChatPopup == null)
		{
			ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
			ChatPopup.nextMultiChatPopUp = null;
			ChatPopup.nextChar = null;
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00003584 File Offset: 0x00001784
	public static void addBigMessage(string chat, int howLong, Npc c)
	{
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00003584 File Offset: 0x00001784
	public static void addChatPopupMultiLine(string chat, int howLong, Npc c)
	{
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00003AD1 File Offset: 0x00001CD1
	public static ChatPopup addChatPopupWithIcon(string chat, int howLong, Npc c, int idIcon)
	{
		return null;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00003AD1 File Offset: 0x00001CD1
	public static ChatPopup addChatPopup(string chat, int howLong, Npc c)
	{
		return null;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00003584 File Offset: 0x00001784
	public static void addChatPopup(string chat, int howLong, int x, int y)
	{
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00003584 File Offset: 0x00001784
	public override void update()
	{
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00003584 File Offset: 0x00001784
	public override void paint(mGraphics g)
	{
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000121C0 File Offset: 0x000103C0
	public void updateKey()
	{
		if (ChatPopup.scr != null)
		{
			if (GameCanvas.isTouch)
			{
				ChatPopup.scr.updateKey();
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				ChatPopup.scr.cmtoY -= 12;
				if (ChatPopup.scr.cmtoY < 0)
				{
					ChatPopup.scr.cmtoY = 0;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				ChatPopup.scr.cmtoY += 12;
				if (ChatPopup.scr.cmtoY > ChatPopup.scr.cmyLim)
				{
					ChatPopup.scr.cmtoY = ChatPopup.scr.cmyLim;
				}
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			if (this.cmdNextLine != null)
			{
				this.cmdNextLine.performAction();
			}
			else if (this.cmdMsg1 != null)
			{
				this.cmdMsg1.performAction();
			}
			else if (this.cmdMsg2 != null)
			{
				this.cmdMsg2.performAction();
			}
		}
		if (ChatPopup.scr != null && ChatPopup.scr.pointerIsDowning)
		{
			return;
		}
		if (this.cmdMsg1 != null && (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.cmdMsg1)))
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			this.cmdMsg1.performAction();
			mScreen.keyTouch = -1;
		}
		if (this.cmdMsg2 != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.cmdMsg2)))
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			this.cmdMsg2.performAction();
			mScreen.keyTouch = -1;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x000123CC File Offset: 0x000105CC
	public void paintCmd(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		if (this.cmdNextLine != null)
		{
			GameCanvas.paintz.paintCmdBar(g, null, this.cmdNextLine, null);
		}
		if (this.cmdMsg1 != null)
		{
			GameCanvas.paintz.paintCmdBar(g, this.cmdMsg1, null, this.cmdMsg2);
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00012448 File Offset: 0x00010648
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception)
			{
			}
			if (!Main.isPC)
			{
				GameMidlet.instance.notifyDestroyed();
			}
			else
			{
				idAction = 1001;
			}
			GameCanvas.endDlg();
		}
		if (idAction == 1001)
		{
			ChatPopup.scr = null;
			global::Char.chatPopup = null;
			ChatPopup.serverChatPopUp = null;
			GameScr.info1.isUpdate = true;
			global::Char.isLockKey = false;
			if (ChatPopup.isHavePetNpc)
			{
				GameScr.info1.info.time = 0;
				GameScr.info1.info.info.speed = 10;
			}
		}
		if (idAction == 8000)
		{
			if (ChatPopup.performDelay > 0)
			{
				return;
			}
			int num = ChatPopup.currChatPopup.currentLine;
			num++;
			if (num >= ChatPopup.currChatPopup.lines.Length)
			{
				global::Char.chatPopup = null;
				ChatPopup.currChatPopup = null;
				GameScr.info1.isUpdate = true;
				global::Char.isLockKey = false;
				if (ChatPopup.nextMultiChatPopUp != null)
				{
					ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
					ChatPopup.nextMultiChatPopUp = null;
					ChatPopup.nextChar = null;
					return;
				}
				if (ChatPopup.isHavePetNpc)
				{
					GameScr.info1.info.time = 0;
					for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
					{
						if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed == 10000000)
						{
							((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
						}
					}
				}
				return;
			}
			else
			{
				ChatPopup chatPopup = ChatPopup.addChatPopup(ChatPopup.currChatPopup.lines[num], ChatPopup.currChatPopup.delay, ChatPopup.currChatPopup.c);
				chatPopup.currentLine = num;
				chatPopup.lines = ChatPopup.currChatPopup.lines;
				chatPopup.cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
				ChatPopup.currChatPopup = chatPopup;
			}
		}
	}

	// Token: 0x04000247 RID: 583
	public int sayWidth = 100;

	// Token: 0x04000248 RID: 584
	public int delay;

	// Token: 0x04000249 RID: 585
	public int sayRun;

	// Token: 0x0400024A RID: 586
	public string[] says;

	// Token: 0x0400024B RID: 587
	public int cx;

	// Token: 0x0400024C RID: 588
	public int cy;

	// Token: 0x0400024D RID: 589
	public int ch;

	// Token: 0x0400024E RID: 590
	public int cmx;

	// Token: 0x0400024F RID: 591
	public int cmy;

	// Token: 0x04000250 RID: 592
	public Npc c;

	// Token: 0x04000251 RID: 593
	private bool outSide;

	// Token: 0x04000252 RID: 594
	public static long curr;

	// Token: 0x04000253 RID: 595
	public static long last;

	// Token: 0x04000254 RID: 596
	private int currentLine;

	// Token: 0x04000255 RID: 597
	private string[] lines;

	// Token: 0x04000256 RID: 598
	public Command cmdNextLine;

	// Token: 0x04000257 RID: 599
	public Command cmdMsg1;

	// Token: 0x04000258 RID: 600
	public Command cmdMsg2;

	// Token: 0x04000259 RID: 601
	public static ChatPopup currChatPopup;

	// Token: 0x0400025A RID: 602
	public static ChatPopup serverChatPopUp;

	// Token: 0x0400025B RID: 603
	public static string nextMultiChatPopUp;

	// Token: 0x0400025C RID: 604
	public static Npc nextChar;

	// Token: 0x0400025D RID: 605
	public bool isShopDetail;

	// Token: 0x0400025E RID: 606
	public sbyte starSlot;

	// Token: 0x0400025F RID: 607
	public sbyte maxStarSlot;

	// Token: 0x04000260 RID: 608
	public static Scroll scr;

	// Token: 0x04000261 RID: 609
	public static bool isHavePetNpc;

	// Token: 0x04000262 RID: 610
	public int mH;

	// Token: 0x04000263 RID: 611
	public static int performDelay;

	// Token: 0x04000264 RID: 612
	public int dx;

	// Token: 0x04000265 RID: 613
	public int dy;

	// Token: 0x04000266 RID: 614
	public int second;

	// Token: 0x04000267 RID: 615
	public int strY;

	// Token: 0x04000268 RID: 616
	private int iconID;
}
