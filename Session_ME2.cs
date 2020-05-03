using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class Session_ME2 : ISession
{
	// Token: 0x06000158 RID: 344 RVA: 0x000041EA File Offset: 0x000023EA
	public Session_ME2()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000433B File Offset: 0x0000253B
	public void clearSendingMessage()
	{
		Session_ME2.sender.sendingMessage.Clear();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000434C File Offset: 0x0000254C
	public static Session_ME2 gI()
	{
		if (Session_ME2.instance == null)
		{
			Session_ME2.instance = new Session_ME2();
		}
		return Session_ME2.instance;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00004367 File Offset: 0x00002567
	public bool isConnected()
	{
		return Session_ME2.connected;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000436E File Offset: 0x0000256E
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME2.messageHandler = msgHandler;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000CC94 File Offset: 0x0000AE94
	public void connect(string host, int port)
	{
		if (Session_ME2.connected || Session_ME2.connecting)
		{
			return;
		}
		this.host = host;
		this.port = port;
		Session_ME2.getKeyComplete = false;
		Session_ME2.sc = null;
		Debug.Log("connecting...!");
		Debug.Log("host: " + host);
		Debug.Log("port: " + port);
		Session_ME2.initThread = new Thread(new ThreadStart(this.NetworkInit));
		Session_ME2.initThread.Start();
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000CD20 File Offset: 0x0000AF20
	private void NetworkInit()
	{
		Session_ME2.isCancel = false;
		Session_ME2.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME2.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME2.messageHandler.onConnectOK(Session_ME2.isMainSession);
		}
		catch (Exception)
		{
			if (Session_ME2.messageHandler != null)
			{
				this.close();
				Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
			}
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
	public void doConnect(string host, int port)
	{
		Session_ME2.sc = new TcpClient();
		Session_ME2.sc.Connect(host, port);
		Session_ME2.dataStream = Session_ME2.sc.GetStream();
		Session_ME2.dis = new BinaryReader(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.dos = new BinaryWriter(Session_ME2.dataStream, new UTF8Encoding());
		new Thread(new ThreadStart(Session_ME2.sender.run)).Start();
		Session_ME2.MessageCollector @object = new Session_ME2.MessageCollector();
		Cout.LogError("new -----");
		Session_ME2.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME2.collectorThread.Start();
		Session_ME2.timeConnected = Session_ME2.currentTimeMillis();
		Session_ME2.connecting = false;
		Session_ME2.doSendMessage(new Message(-27));
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00004376 File Offset: 0x00002576
	public void sendMessage(Message message)
	{
		Res.outz("SEND MSG: " + message.command);
		Session_ME2.sender.AddMessage(message);
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000CE68 File Offset: 0x0000B068
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			if (Session_ME2.getKeyComplete)
			{
				sbyte value = Session_ME2.writeKey(m.command);
				Session_ME2.dos.Write(value);
			}
			else
			{
				Session_ME2.dos.Write(m.command);
			}
			if (data != null)
			{
				int num = data.Length;
				if (Session_ME2.getKeyComplete)
				{
					int num2 = (int)Session_ME2.writeKey((sbyte)(num >> 8));
					Session_ME2.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME2.writeKey((sbyte)(num & 255));
					Session_ME2.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME2.dos.Write((ushort)num);
				}
				if (Session_ME2.getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME2.writeKey(data[i]);
						Session_ME2.dos.Write(value2);
					}
				}
				Session_ME2.sendByteCount += 5 + data.Length;
			}
			else
			{
				if (Session_ME2.getKeyComplete)
				{
					int num4 = 0;
					int num5 = (int)Session_ME2.writeKey((sbyte)(num4 >> 8));
					Session_ME2.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME2.writeKey((sbyte)(num4 & 255));
					Session_ME2.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME2.dos.Write(0);
				}
				Session_ME2.sendByteCount += 5;
			}
			Session_ME2.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curR;
		Session_ME2.curR = b2 + 1;
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME2.curR >= Session_ME2.key.Length)
		{
			Session_ME2.curR = (sbyte)((int)Session_ME2.curR % (int)((sbyte)Session_ME2.key.Length));
		}
		return result;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000D058 File Offset: 0x0000B258
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curW;
		Session_ME2.curW = b2 + 1;
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME2.curW >= Session_ME2.key.Length)
		{
			Session_ME2.curW = (sbyte)((int)Session_ME2.curW % (int)((sbyte)Session_ME2.key.Length));
		}
		return result;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000439D File Offset: 0x0000259D
	public static void onRecieveMsg(Message msg)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Session_ME2.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME2.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
	public static void update()
	{
		while (Session_ME2.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME2.recieveMsg.elementAt(0);
			if (Controller.isStopReadMessage)
			{
				return;
			}
			if (message == null)
			{
				Session_ME2.recieveMsg.removeElementAt(0);
				return;
			}
			Session_ME2.messageHandler.onMessage(message);
			Session_ME2.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000043D3 File Offset: 0x000025D3
	public void close()
	{
		Session_ME2.cleanNetwork();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000D120 File Offset: 0x0000B320
	private static void cleanNetwork()
	{
		Session_ME2.key = null;
		Session_ME2.curR = 0;
		Session_ME2.curW = 0;
		try
		{
			Session_ME2.connected = false;
			Session_ME2.connecting = false;
			if (Session_ME2.sc != null)
			{
				Session_ME2.sc.Close();
				Session_ME2.sc = null;
			}
			if (Session_ME2.dataStream != null)
			{
				Session_ME2.dataStream.Close();
				Session_ME2.dataStream = null;
			}
			if (Session_ME2.dos != null)
			{
				Session_ME2.dos.Close();
				Session_ME2.dos = null;
			}
			if (Session_ME2.dis != null)
			{
				Session_ME2.dis.Close();
				Session_ME2.dis = null;
			}
			Session_ME2.sendThread = null;
			Session_ME2.collectorThread = null;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000042DD File Offset: 0x000024DD
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00003740 File Offset: 0x00001940
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00007ABC File Offset: 0x00005CBC
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x04000133 RID: 307
	protected static Session_ME2 instance = new Session_ME2();

	// Token: 0x04000134 RID: 308
	private static NetworkStream dataStream;

	// Token: 0x04000135 RID: 309
	private static BinaryReader dis;

	// Token: 0x04000136 RID: 310
	private static BinaryWriter dos;

	// Token: 0x04000137 RID: 311
	public static IMessageHandler messageHandler;

	// Token: 0x04000138 RID: 312
	public static bool isMainSession = true;

	// Token: 0x04000139 RID: 313
	private static TcpClient sc;

	// Token: 0x0400013A RID: 314
	public static bool connected;

	// Token: 0x0400013B RID: 315
	public static bool connecting;

	// Token: 0x0400013C RID: 316
	private static Session_ME2.Sender sender = new Session_ME2.Sender();

	// Token: 0x0400013D RID: 317
	public static Thread initThread;

	// Token: 0x0400013E RID: 318
	public static Thread collectorThread;

	// Token: 0x0400013F RID: 319
	public static Thread sendThread;

	// Token: 0x04000140 RID: 320
	public static int sendByteCount;

	// Token: 0x04000141 RID: 321
	public static int recvByteCount;

	// Token: 0x04000142 RID: 322
	private static bool getKeyComplete;

	// Token: 0x04000143 RID: 323
	public static sbyte[] key = null;

	// Token: 0x04000144 RID: 324
	private static sbyte curR;

	// Token: 0x04000145 RID: 325
	private static sbyte curW;

	// Token: 0x04000146 RID: 326
	private static int timeConnected;

	// Token: 0x04000147 RID: 327
	private long lastTimeConn;

	// Token: 0x04000148 RID: 328
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04000149 RID: 329
	public static bool isCancel;

	// Token: 0x0400014A RID: 330
	private string host;

	// Token: 0x0400014B RID: 331
	private int port;

	// Token: 0x0400014C RID: 332
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x02000025 RID: 37
	public class Sender
	{
		// Token: 0x0600016C RID: 364 RVA: 0x000043DA File Offset: 0x000025DA
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000043ED File Offset: 0x000025ED
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public void run()
		{
			while (Session_ME2.connected)
			{
				try
				{
					if (Session_ME2.getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message m = this.sendingMessage[0];
							Session_ME2.doSendMessage(m);
							this.sendingMessage.RemoveAt(0);
						}
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception ex)
					{
						Cout.LogError(ex.ToString());
					}
				}
				catch (Exception)
				{
					Res.outz("error send message! ");
				}
			}
		}

		// Token: 0x0400014D RID: 333
		public List<Message> sendingMessage;
	}

	// Token: 0x02000026 RID: 38
	private class MessageCollector
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000D284 File Offset: 0x0000B484
		public void run()
		{
			try
			{
				while (Session_ME2.connected)
				{
					Message message = this.readMessage();
					if (message == null)
					{
						break;
					}
					try
					{
						if ((int)message.command == -27)
						{
							this.getKey(message);
						}
						else
						{
							Session_ME2.onRecieveMsg(message);
						}
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 1");
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 2");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error read message!");
				Debug.Log(ex.Message.ToString());
			}
			if (Session_ME2.connected)
			{
				if (Session_ME2.messageHandler != null)
				{
					if (Session_ME2.currentTimeMillis() - Session_ME2.timeConnected > 500)
					{
						Session_ME2.messageHandler.onDisconnected(Session_ME2.isMainSession);
					}
					else
					{
						Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
					}
				}
				if (Session_ME2.sc != null)
				{
					Session_ME2.cleanNetwork();
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME2.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME2.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME2.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME2.key;
					int num = j + 1;
					key[num] = (sbyte)((int)key[num] ^ (int)Session_ME2.key[j]);
				}
				Session_ME2.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = ((int)message.reader().readByte() != 0);
				if (Session_ME2.isMainSession && GameMidlet.isConnect2)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num4);
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME2.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME2.recvByteCount += 5 + num4;
			int num5 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
			Session_ME2.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			if (Session_ME2.getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME2.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME2.dis.ReadSByte();
				if (Session_ME2.getKeyComplete)
				{
					b = Session_ME2.readKey(b);
				}
				if ((int)b == -32 || (int)b == -66 || (int)b == 11 || (int)b == -67 || (int)b == -74 || (int)b == -87)
				{
					return this.readMessage2(b);
				}
				int num;
				if (Session_ME2.getKeyComplete)
				{
					sbyte b2 = Session_ME2.dis.ReadSByte();
					sbyte b3 = Session_ME2.dis.ReadSByte();
					num = (((int)Session_ME2.readKey(b2) & 255) << 8 | ((int)Session_ME2.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME2.dis.ReadSByte();
					sbyte b5 = Session_ME2.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME2.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME2.recvByteCount += 5 + num;
				int num2 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
				Session_ME2.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				if (Session_ME2.getKeyComplete)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME2.readKey(array[i]);
					}
				}
				return new Message(b, array);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.StackTrace.ToString());
			}
			return null;
		}
	}
}
