using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class Session_ME : ISession
{
	// Token: 0x0600013C RID: 316 RVA: 0x000041EA File Offset: 0x000023EA
	public Session_ME()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00004232 File Offset: 0x00002432
	public void clearSendingMessage()
	{
		Session_ME.sender.sendingMessage.Clear();
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00004243 File Offset: 0x00002443
	public static Session_ME gI()
	{
		if (Session_ME.instance == null)
		{
			Session_ME.instance = new Session_ME();
		}
		return Session_ME.instance;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000425E File Offset: 0x0000245E
	public bool isConnected()
	{
		return Session_ME.connected;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00004265 File Offset: 0x00002465
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME.messageHandler = msgHandler;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000C140 File Offset: 0x0000A340
	public void connect(string host, int port)
	{
		if (Session_ME.connected || Session_ME.connecting)
		{
			return;
		}
		if (Session_ME.isMainSession)
		{
			ServerListScreen.testConnect = -1;
		}
		this.host = host;
		this.port = port;
		Session_ME.getKeyComplete = false;
		Session_ME.sc = null;
		Debug.Log("connecting...!");
		Debug.Log("host: " + host);
		Debug.Log("port: " + port);
		Session_ME.initThread = new Thread(new ThreadStart(this.NetworkInit));
		Session_ME.initThread.Start();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000C1DC File Offset: 0x0000A3DC
	private void NetworkInit()
	{
		Session_ME.isCancel = false;
		Session_ME.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME.messageHandler.onConnectOK(Session_ME.isMainSession);
		}
		catch (Exception)
		{
			if (Session_ME.messageHandler != null)
			{
				this.close();
				Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
			}
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000C264 File Offset: 0x0000A464
	public void doConnect(string host, int port)
	{
		Session_ME.sc = new TcpClient();
		Session_ME.sc.Connect(host, port);
		Session_ME.dataStream = Session_ME.sc.GetStream();
		Session_ME.dis = new BinaryReader(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.dos = new BinaryWriter(Session_ME.dataStream, new UTF8Encoding());
		new Thread(new ThreadStart(Session_ME.sender.run)).Start();
		Session_ME.MessageCollector @object = new Session_ME.MessageCollector();
		Cout.LogError("new -----");
		Session_ME.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME.collectorThread.Start();
		Session_ME.timeConnected = Session_ME.currentTimeMillis();
		Session_ME.connecting = false;
		Session_ME.doSendMessage(new Message(-27));
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000426D File Offset: 0x0000246D
	public void sendMessage(Message message)
	{
		Session_ME.count++;
		Res.outz("SEND MSG: " + message.command);
		Session_ME.sender.AddMessage(message);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000C324 File Offset: 0x0000A524
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			if (Session_ME.getKeyComplete)
			{
				sbyte value = Session_ME.writeKey(m.command);
				Session_ME.dos.Write(value);
			}
			else
			{
				Session_ME.dos.Write(m.command);
			}
			if (data != null)
			{
				int num = data.Length;
				if (Session_ME.getKeyComplete)
				{
					int num2 = (int)Session_ME.writeKey((sbyte)(num >> 8));
					Session_ME.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME.writeKey((sbyte)(num & 255));
					Session_ME.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME.dos.Write((ushort)num);
				}
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME.writeKey(data[i]);
						Session_ME.dos.Write(value2);
					}
				}
				Session_ME.sendByteCount += 5 + data.Length;
			}
			else
			{
				if (Session_ME.getKeyComplete)
				{
					int num4 = 0;
					int num5 = (int)Session_ME.writeKey((sbyte)(num4 >> 8));
					Session_ME.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME.writeKey((sbyte)(num4 & 255));
					Session_ME.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME.dos.Write(0);
				}
				Session_ME.sendByteCount += 5;
			}
			Session_ME.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curR;
		Session_ME.curR = b2 + 1;
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curR >= Session_ME.key.Length)
		{
			Session_ME.curR = (sbyte)((int)Session_ME.curR % (int)((sbyte)Session_ME.key.Length));
		}
		return result;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000C514 File Offset: 0x0000A714
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curW;
		Session_ME.curW = b2 + 1;
		sbyte result = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curW >= Session_ME.key.Length)
		{
			Session_ME.curW = (sbyte)((int)Session_ME.curW % (int)((sbyte)Session_ME.key.Length));
		}
		return result;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000042A0 File Offset: 0x000024A0
	public static void onRecieveMsg(Message msg)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Session_ME.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000C574 File Offset: 0x0000A774
	public static void update()
	{
		while (Session_ME.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME.recieveMsg.elementAt(0);
			if (Controller.isStopReadMessage)
			{
				return;
			}
			if (message == null)
			{
				Session_ME.recieveMsg.removeElementAt(0);
				return;
			}
			Session_ME.messageHandler.onMessage(message);
			Session_ME.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000042D6 File Offset: 0x000024D6
	public void close()
	{
		Session_ME.cleanNetwork();
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000C5DC File Offset: 0x0000A7DC
	private static void cleanNetwork()
	{
		Session_ME.key = null;
		Session_ME.curR = 0;
		Session_ME.curW = 0;
		try
		{
			Session_ME.connected = false;
			Session_ME.connecting = false;
			if (Session_ME.sc != null)
			{
				Session_ME.sc.Close();
				Session_ME.sc = null;
			}
			if (Session_ME.dataStream != null)
			{
				Session_ME.dataStream.Close();
				Session_ME.dataStream = null;
			}
			if (Session_ME.dos != null)
			{
				Session_ME.dos.Close();
				Session_ME.dos = null;
			}
			if (Session_ME.dis != null)
			{
				Session_ME.dis.Close();
				Session_ME.dis = null;
			}
			Session_ME.sendThread = null;
			Session_ME.collectorThread = null;
			if (Session_ME.isMainSession)
			{
				ServerListScreen.testConnect = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000042DD File Offset: 0x000024DD
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00003740 File Offset: 0x00001940
	public static byte convertSbyteToByte(sbyte var)
	{
		if ((int)var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007ABC File Offset: 0x00005CBC
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

	// Token: 0x04000117 RID: 279
	protected static Session_ME instance = new Session_ME();

	// Token: 0x04000118 RID: 280
	private static NetworkStream dataStream;

	// Token: 0x04000119 RID: 281
	private static BinaryReader dis;

	// Token: 0x0400011A RID: 282
	private static BinaryWriter dos;

	// Token: 0x0400011B RID: 283
	public static IMessageHandler messageHandler;

	// Token: 0x0400011C RID: 284
	public static bool isMainSession = true;

	// Token: 0x0400011D RID: 285
	private static TcpClient sc;

	// Token: 0x0400011E RID: 286
	public static bool connected;

	// Token: 0x0400011F RID: 287
	public static bool connecting;

	// Token: 0x04000120 RID: 288
	private static Session_ME.Sender sender = new Session_ME.Sender();

	// Token: 0x04000121 RID: 289
	public static Thread initThread;

	// Token: 0x04000122 RID: 290
	public static Thread collectorThread;

	// Token: 0x04000123 RID: 291
	public static Thread sendThread;

	// Token: 0x04000124 RID: 292
	public static int sendByteCount;

	// Token: 0x04000125 RID: 293
	public static int recvByteCount;

	// Token: 0x04000126 RID: 294
	private static bool getKeyComplete;

	// Token: 0x04000127 RID: 295
	public static sbyte[] key = null;

	// Token: 0x04000128 RID: 296
	private static sbyte curR;

	// Token: 0x04000129 RID: 297
	private static sbyte curW;

	// Token: 0x0400012A RID: 298
	private static int timeConnected;

	// Token: 0x0400012B RID: 299
	private long lastTimeConn;

	// Token: 0x0400012C RID: 300
	public static string strRecvByteCount = string.Empty;

	// Token: 0x0400012D RID: 301
	public static bool isCancel;

	// Token: 0x0400012E RID: 302
	private string host;

	// Token: 0x0400012F RID: 303
	private int port;

	// Token: 0x04000130 RID: 304
	public static int count;

	// Token: 0x04000131 RID: 305
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x02000022 RID: 34
	public class Sender
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000042E4 File Offset: 0x000024E4
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000042F7 File Offset: 0x000024F7
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public void run()
		{
			while (Session_ME.connected)
			{
				try
				{
					if (Session_ME.getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message m = this.sendingMessage[0];
							Session_ME.doSendMessage(m);
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

		// Token: 0x04000132 RID: 306
		public List<Message> sendingMessage;
	}

	// Token: 0x02000023 RID: 35
	private class MessageCollector
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000C750 File Offset: 0x0000A950
		public void run()
		{
			try
			{
				while (Session_ME.connected)
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
							Session_ME.onRecieveMsg(message);
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
			if (Session_ME.connected)
			{
				if (Session_ME.messageHandler != null)
				{
					if (Session_ME.currentTimeMillis() - Session_ME.timeConnected > 500)
					{
						Session_ME.messageHandler.onDisconnected(Session_ME.isMainSession);
					}
					else
					{
						Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
					}
				}
				if (Session_ME.sc != null)
				{
					Session_ME.cleanNetwork();
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME.key;
					int num = j + 1;
					key[num] = (sbyte)((int)key[num] ^ (int)Session_ME.key[j]);
				}
				Session_ME.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = ((int)message.reader().readByte() != 0);
				if (Session_ME.isMainSession && GameMidlet.isConnect2)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C974 File Offset: 0x0000AB74
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num4);
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME.recvByteCount += 5 + num4;
			int num5 = Session_ME.recvByteCount + Session_ME.sendByteCount;
			Session_ME.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			if (Session_ME.getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME.dis.ReadSByte();
				if (Session_ME.getKeyComplete)
				{
					b = Session_ME.readKey(b);
				}
				if ((int)b == -32 || (int)b == -66 || (int)b == 11 || (int)b == -67 || (int)b == -74 || (int)b == -87)
				{
					return this.readMessage2(b);
				}
				int num;
				if (Session_ME.getKeyComplete)
				{
					sbyte b2 = Session_ME.dis.ReadSByte();
					sbyte b3 = Session_ME.dis.ReadSByte();
					num = (((int)Session_ME.readKey(b2) & 255) << 8 | ((int)Session_ME.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME.dis.ReadSByte();
					sbyte b5 = Session_ME.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME.recvByteCount += 5 + num;
				int num2 = Session_ME.recvByteCount + Session_ME.sendByteCount;
				Session_ME.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME.readKey(array[i]);
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
