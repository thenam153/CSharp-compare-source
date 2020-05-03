using System;

// Token: 0x02000043 RID: 67
public interface IMessageHandler
{
	// Token: 0x06000291 RID: 657
	void onMessage(Message message);

	// Token: 0x06000292 RID: 658
	void onConnectionFail(bool isMain);

	// Token: 0x06000293 RID: 659
	void onDisconnected(bool isMain);

	// Token: 0x06000294 RID: 660
	void onConnectOK(bool isMain);
}
