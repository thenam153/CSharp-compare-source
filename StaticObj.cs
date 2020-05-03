using System;

// Token: 0x020000B2 RID: 178
public class StaticObj
{
	// Token: 0x04000FB6 RID: 4022
	public const string SAVE_SKILL = "skill";

	// Token: 0x04000FB7 RID: 4023
	public const string SAVE_VERSIONUPDATE = "versionUpdate";

	// Token: 0x04000FB8 RID: 4024
	public const string SAVE_KEYKILL = "keyskill";

	// Token: 0x04000FB9 RID: 4025
	public const string SAVE_ITEM = "item";

	// Token: 0x04000FBA RID: 4026
	public const int NORMAL = 0;

	// Token: 0x04000FBB RID: 4027
	public const int UP_FALL = 1;

	// Token: 0x04000FBC RID: 4028
	public const int UP_RUN = 2;

	// Token: 0x04000FBD RID: 4029
	public const int FALL_RIGHT = 3;

	// Token: 0x04000FBE RID: 4030
	public const int FALL_LEFT = 4;

	// Token: 0x04000FBF RID: 4031
	public const int MOD_ATTACK_ME = 100;

	// Token: 0x04000FC0 RID: 4032
	public const int TYPE_PLAYER = 3;

	// Token: 0x04000FC1 RID: 4033
	public const int TYPE_NON = 0;

	// Token: 0x04000FC2 RID: 4034
	public const int TYPE_VUKHI = 1;

	// Token: 0x04000FC3 RID: 4035
	public const int TYPE_AO = 2;

	// Token: 0x04000FC4 RID: 4036
	public const int TYPE_LIEN = 3;

	// Token: 0x04000FC5 RID: 4037
	public const int TYPE_TAY = 4;

	// Token: 0x04000FC6 RID: 4038
	public const int TYPE_NHAN = 5;

	// Token: 0x04000FC7 RID: 4039
	public const int TYPE_QUAN = 6;

	// Token: 0x04000FC8 RID: 4040
	public const int TYPE_BOI = 7;

	// Token: 0x04000FC9 RID: 4041
	public const int TYPE_GIAY = 8;

	// Token: 0x04000FCA RID: 4042
	public const int TYPE_PHU = 9;

	// Token: 0x04000FCB RID: 4043
	public const int TYPE_OTHER = 11;

	// Token: 0x04000FCC RID: 4044
	public const int TYPE_CRYSTAL = 15;

	// Token: 0x04000FCD RID: 4045
	public const int FOCUS_MOD = 1;

	// Token: 0x04000FCE RID: 4046
	public const int FOCUS_ITEM = 2;

	// Token: 0x04000FCF RID: 4047
	public const int FOCUS_PLAYER = 3;

	// Token: 0x04000FD0 RID: 4048
	public const int FOCUS_ZONE = 4;

	// Token: 0x04000FD1 RID: 4049
	public const int FOCUS_NPC = 5;

	// Token: 0x04000FD2 RID: 4050
	public static int TOP_CENTER = mGraphics.TOP | mGraphics.HCENTER;

	// Token: 0x04000FD3 RID: 4051
	public static int TOP_LEFT = mGraphics.TOP | mGraphics.LEFT;

	// Token: 0x04000FD4 RID: 4052
	public static int TOP_RIGHT = mGraphics.TOP | mGraphics.RIGHT;

	// Token: 0x04000FD5 RID: 4053
	public static int BOTTOM_HCENTER = mGraphics.BOTTOM | mGraphics.HCENTER;

	// Token: 0x04000FD6 RID: 4054
	public static int BOTTOM_LEFT = mGraphics.BOTTOM | mGraphics.LEFT;

	// Token: 0x04000FD7 RID: 4055
	public static int BOTTOM_RIGHT = mGraphics.BOTTOM | mGraphics.RIGHT;

	// Token: 0x04000FD8 RID: 4056
	public static int VCENTER_HCENTER = mGraphics.VCENTER | mGraphics.HCENTER;

	// Token: 0x04000FD9 RID: 4057
	public static int VCENTER_LEFT = mGraphics.VCENTER | mGraphics.LEFT;

	// Token: 0x04000FDA RID: 4058
	public static int[][] TYPEBG = new int[][]
	{
		new int[4],
		new int[]
		{
			1,
			1,
			1,
			1
		},
		new int[4],
		new int[]
		{
			2,
			2,
			2,
			2
		},
		new int[]
		{
			3,
			3,
			3,
			3
		},
		new int[]
		{
			4,
			-1,
			-1,
			4
		},
		new int[]
		{
			5,
			5,
			5,
			-1
		},
		new int[]
		{
			6,
			6,
			6,
			5
		},
		new int[]
		{
			7,
			7,
			-1,
			-1
		},
		new int[]
		{
			8,
			8,
			8,
			7
		},
		new int[]
		{
			9,
			-1,
			-1,
			8
		},
		new int[]
		{
			10,
			-1,
			-1,
			9
		},
		new int[]
		{
			11,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x04000FDB RID: 4059
	public static int[] SKYCOLOR = new int[]
	{
		1618168,
		1938102,
		43488,
		16316528,
		1628316,
		3270903,
		3576979,
		6999725,
		14594155,
		8562616,
		16026508,
		1052688,
		13952747,
		15268088,
		1628316,
		2631752
	};
}
