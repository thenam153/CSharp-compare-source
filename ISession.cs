using System;

// Token: 0x02000044 RID: 68
public interface ISession
{
	// Token: 0x06000295 RID: 661
	bool isConnected();

	// Token: 0x06000296 RID: 662
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x06000297 RID: 663
	void connect(string host, int port);

	// Token: 0x06000298 RID: 664
	void sendMessage(Message message);

	// Token: 0x06000299 RID: 665
	void close();
}
