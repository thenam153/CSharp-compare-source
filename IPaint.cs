using System;

// Token: 0x02000056 RID: 86
public abstract class IPaint
{
	// Token: 0x060002DA RID: 730
	public abstract void paintDefaultBg(mGraphics g);

	// Token: 0x060002DB RID: 731
	public abstract void paintfillDefaultBg(mGraphics g);

	// Token: 0x060002DC RID: 732
	public abstract void repaintCircleBg();

	// Token: 0x060002DD RID: 733
	public abstract void paintSolidBg(mGraphics g);

	// Token: 0x060002DE RID: 734
	public abstract void paintDefaultPopup(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060002DF RID: 735
	public abstract void paintWhitePopup(mGraphics g, int y, int x, int width, int height);

	// Token: 0x060002E0 RID: 736
	public abstract void paintDefaultPopupH(mGraphics g, int h);

	// Token: 0x060002E1 RID: 737
	public abstract void paintCmdBar(mGraphics g, Command left, Command center, Command right);

	// Token: 0x060002E2 RID: 738
	public abstract void paintSelect(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060002E3 RID: 739
	public abstract void paintLogo(mGraphics g, int x, int y);

	// Token: 0x060002E4 RID: 740
	public abstract void paintHotline(mGraphics g, string num);

	// Token: 0x060002E5 RID: 741
	public abstract void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text);

	// Token: 0x060002E6 RID: 742
	public abstract void paintTabSoft(mGraphics g);

	// Token: 0x060002E7 RID: 743
	public abstract void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x060002E8 RID: 744
	public abstract void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check);

	// Token: 0x060002E9 RID: 745
	public abstract void paintDefaultScrLisst(mGraphics g, string title, string subTitle, string check);

	// Token: 0x060002EA RID: 746
	public abstract void paintCheck(mGraphics g, int x, int y, int index);

	// Token: 0x060002EB RID: 747
	public abstract void paintImgMsg(mGraphics g, int x, int y, int index);

	// Token: 0x060002EC RID: 748
	public abstract void paintTitleBoard(mGraphics g, int roomID);

	// Token: 0x060002ED RID: 749
	public abstract void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus);

	// Token: 0x060002EE RID: 750
	public abstract void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str);

	// Token: 0x060002EF RID: 751
	public abstract void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool issSe, int i, int wStr);

	// Token: 0x060002F0 RID: 752
	public abstract void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x060002F1 RID: 753
	public abstract void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x060002F2 RID: 754
	public abstract void paintScroll(mGraphics g, int x, int y, int h);

	// Token: 0x060002F3 RID: 755
	public abstract int[] getColorMsg();

	// Token: 0x060002F4 RID: 756
	public abstract void paintLogo(mGraphics g);

	// Token: 0x060002F5 RID: 757
	public abstract void paintTextLogin(mGraphics g, bool issRes);

	// Token: 0x060002F6 RID: 758
	public abstract void paintSellectBoard(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060002F7 RID: 759
	public abstract int issRegissterUsingWAP();

	// Token: 0x060002F8 RID: 760
	public abstract string getCard();

	// Token: 0x060002F9 RID: 761
	public abstract void paintSellectedShop(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060002FA RID: 762
	public abstract string getUrlUpdateGame();

	// Token: 0x060002FB RID: 763 RVA: 0x00004BDD File Offset: 0x00002DDD
	public string getFAQLink()
	{
		return "http://wap.teamobi.com/faqs.php?provider=";
	}

	// Token: 0x060002FC RID: 764
	public abstract void doSelect(int focus);
}
