using System;

// Token: 0x02000012 RID: 18
public class TouchScreenKeyboard
{
	// Token: 0x06000080 RID: 128 RVA: 0x00003AD1 File Offset: 0x00001CD1
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType t, bool b1, bool b2, bool type, bool b3, string caption)
	{
		return null;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003584 File Offset: 0x00001784
	public static void Clear()
	{
	}

	// Token: 0x04000023 RID: 35
	public static bool hideInput;

	// Token: 0x04000024 RID: 36
	public static bool visible;

	// Token: 0x04000025 RID: 37
	public bool done;

	// Token: 0x04000026 RID: 38
	public bool active;

	// Token: 0x04000027 RID: 39
	public string text;
}
