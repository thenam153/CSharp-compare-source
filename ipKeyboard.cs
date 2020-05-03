using System;

// Token: 0x0200001D RID: 29
public class ipKeyboard
{
	// Token: 0x06000101 RID: 257 RVA: 0x0000B62C File Offset: 0x0000982C
	public static void openKeyBoard(string caption, int type, string text, Command action)
	{
		ipKeyboard.act = action;
		TouchScreenKeyboardType t = (type != 0 && type != 2) ? TouchScreenKeyboardType.NumberPad : TouchScreenKeyboardType.ASCIICapable;
		TouchScreenKeyboard.hideInput = false;
		ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000B670 File Offset: 0x00009870
	public static void update()
	{
		try
		{
			if (ipKeyboard.tk != null)
			{
				if (ipKeyboard.tk.done)
				{
					if (ipKeyboard.act != null)
					{
						ipKeyboard.act.perform(ipKeyboard.tk.text);
					}
					ipKeyboard.tk.text = string.Empty;
					ipKeyboard.tk = null;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x040000E1 RID: 225
	private static TouchScreenKeyboard tk;

	// Token: 0x040000E2 RID: 226
	public static int TEXT;

	// Token: 0x040000E3 RID: 227
	public static int NUMBERIC = 1;

	// Token: 0x040000E4 RID: 228
	public static int PASS = 2;

	// Token: 0x040000E5 RID: 229
	private static Command act;
}
