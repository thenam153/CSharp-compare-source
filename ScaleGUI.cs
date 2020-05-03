using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class ScaleGUI
{
	// Token: 0x060000A2 RID: 162 RVA: 0x00009054 File Offset: 0x00007254
	public static void initScaleGUI()
	{
		Cout.println(string.Concat(new object[]
		{
			"Init Scale GUI: Screen.w=",
			Screen.width,
			" Screen.h=",
			Screen.height
		}));
		ScaleGUI.WIDTH = (float)Screen.width;
		ScaleGUI.HEIGHT = (float)Screen.height;
		ScaleGUI.scaleScreen = false;
		if (Screen.width > 1200)
		{
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000090C8 File Offset: 0x000072C8
	public static void BeginGUI()
	{
		if (!ScaleGUI.scaleScreen)
		{
			return;
		}
		ScaleGUI.stack.Add(GUI.matrix);
		Matrix4x4 rhs = default(Matrix4x4);
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float num3 = num / num2;
		Vector3 zero = Vector3.zero;
		float d;
		if (num3 < ScaleGUI.WIDTH / ScaleGUI.HEIGHT)
		{
			d = (float)Screen.width / ScaleGUI.WIDTH;
		}
		else
		{
			d = (float)Screen.height / ScaleGUI.HEIGHT;
		}
		rhs.SetTRS(zero, Quaternion.identity, Vector3.one * d);
		GUI.matrix *= rhs;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00003B74 File Offset: 0x00001D74
	public static void EndGUI()
	{
		if (!ScaleGUI.scaleScreen)
		{
			return;
		}
		GUI.matrix = ScaleGUI.stack[ScaleGUI.stack.Count - 1];
		ScaleGUI.stack.RemoveAt(ScaleGUI.stack.Count - 1);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003BB2 File Offset: 0x00001DB2
	public static float scaleX(float x)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return x;
		}
		x = x * ScaleGUI.WIDTH / (float)Screen.width;
		return x;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003BD1 File Offset: 0x00001DD1
	public static float scaleY(float y)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return y;
		}
		y = y * ScaleGUI.HEIGHT / (float)Screen.height;
		return y;
	}

	// Token: 0x04000055 RID: 85
	public static bool scaleScreen;

	// Token: 0x04000056 RID: 86
	public static float WIDTH;

	// Token: 0x04000057 RID: 87
	public static float HEIGHT;

	// Token: 0x04000058 RID: 88
	private static List<Matrix4x4> stack = new List<Matrix4x4>();
}
