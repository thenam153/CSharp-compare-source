using System;

// Token: 0x0200009B RID: 155
public interface IChatable
{
	// Token: 0x060006D6 RID: 1750
	void onChatFromMe(string text, string to);

	// Token: 0x060006D7 RID: 1751
	void onCancelChat();
}
