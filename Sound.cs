using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class Sound
{
	// Token: 0x060000A9 RID: 169 RVA: 0x00003584 File Offset: 0x00001784
	public static void setActivity(SoundMn.AssetManager ac)
	{
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000093A4 File Offset: 0x000075A4
	public static void stop()
	{
		for (int i = 0; i < Sound.player.Length; i++)
		{
			if (Sound.player[i] != null)
			{
				Sound.player[i].GetComponent<AudioSource>().Pause();
			}
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003868 File Offset: 0x00001A68
	public static bool isPlaying()
	{
		return false;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000093EC File Offset: 0x000075EC
	public static void init()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Audio Player";
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<AudioListener>();
		Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000942C File Offset: 0x0000762C
	public static void init(int[] musicID, int[] sID)
	{
		if (Sound.player != null || Sound.music != null)
		{
			return;
		}
		Sound.init();
		Sound.l1 = musicID.Length;
		Sound.player = new GameObject[musicID.Length + sID.Length];
		Sound.music = new AudioClip[musicID.Length + sID.Length];
		for (int i = 0; i < Sound.player.Length; i++)
		{
			string text = (i >= Sound.l1) ? ("/sound/" + (i - Sound.l1)) : ("/music/" + i);
			Sound.getAssetSoundFile(text, i);
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003BF0 File Offset: 0x00001DF0
	public static void playSound(int id, float volume)
	{
		Sound.play(id + Sound.l1, volume);
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003BFF File Offset: 0x00001DFF
	public static void playSound1(int id, float volume)
	{
		Sound.play(id, volume);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000094D4 File Offset: 0x000076D4
	public static void getAssetSoundFile(string fileName, int pos)
	{
		Sound.stop(pos);
		string filename = string.Empty;
		filename = Main.res + fileName;
		Sound.load(filename, pos);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00009500 File Offset: 0x00007700
	public static void stopAllz()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		for (int j = 0; j < Sound.l1; j++)
		{
			Sound.sTopSoundBG(j);
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00009548 File Offset: 0x00007748
	public static void stopAllBg()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		Sound.sTopSoundBG(0);
		Sound.sTopSoundRun();
		Sound.stopSoundNatural(0);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00003584 File Offset: 0x00001784
	public static void update()
	{
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00003C08 File Offset: 0x00001E08
	public static void stopMusic(int x)
	{
		if (GameCanvas.isPlaySound)
		{
			Sound.stop(x);
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00003C1A File Offset: 0x00001E1A
	public static void play(int id, float volume)
	{
		if (Sound.isNotPlay)
		{
			return;
		}
		if (GameCanvas.isPlaySound)
		{
			Sound.start(volume, id);
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00009584 File Offset: 0x00007784
	public static void playSoundRun(int id, float volume)
	{
		if (GameCanvas.isPlaySound)
		{
			if (Sound.SoundRun == null)
			{
				return;
			}
			Sound.SoundRun.GetComponent<AudioSource>().loop = true;
			Sound.SoundRun.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundRun.GetComponent<AudioSource>().volume = volume;
			Sound.SoundRun.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00003C38 File Offset: 0x00001E38
	public static void sTopSoundRun()
	{
		Sound.SoundRun.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000095F4 File Offset: 0x000077F4
	public static bool isPlayingSound()
	{
		return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00009624 File Offset: 0x00007824
	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		if (GameCanvas.isPlaySound)
		{
			if (Sound.SoundBGLoop == null)
			{
				return;
			}
			Sound.SoundWater.GetComponent<AudioSource>().loop = isLoop;
			Sound.SoundWater.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundWater.GetComponent<AudioSource>().volume = volume;
			Sound.SoundWater.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00003C49 File Offset: 0x00001E49
	public static void stopSoundNatural(int id)
	{
		Sound.SoundWater.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00009694 File Offset: 0x00007894
	public static bool isPlayingSoundatural(int id)
	{
		return !(Sound.SoundWater == null) && Sound.SoundWater.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00003C5A File Offset: 0x00001E5A
	public static void playMus(int type, float vl, bool loop)
	{
		if (Sound.isNotPlay)
		{
			return;
		}
		vl -= 0.3f;
		if (vl <= 0f)
		{
			vl = 0.01f;
		}
		Sound.playSoundBGLoop(type, vl);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000096C4 File Offset: 0x000078C4
	public static void playSoundBGLoop(int id, float volume)
	{
		if (GameCanvas.isPlaySound)
		{
			if (id == SoundMn.AIR_SHIP)
			{
				Sound.playSound1(id, volume + 0.2f);
				return;
			}
			if (Sound.SoundBGLoop == null)
			{
				return;
			}
			if (Sound.isPlayingSoundBG(id))
			{
				return;
			}
			Sound.SoundBGLoop.GetComponent<AudioSource>().loop = true;
			Sound.SoundBGLoop.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundBGLoop.GetComponent<AudioSource>().volume = volume;
			Sound.SoundBGLoop.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00003C89 File Offset: 0x00001E89
	public static void sTopSoundBG(int id)
	{
		Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00009758 File Offset: 0x00007958
	public static bool isPlayingSoundBG(int id)
	{
		return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00003C9A File Offset: 0x00001E9A
	public static void load(string filename, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__load(filename, pos);
		}
		else
		{
			Sound._load(filename, pos);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00009788 File Offset: 0x00007988
	private static void _load(string filename, int pos)
	{
		if (Sound.status != 0)
		{
			Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Sound.filenametemp);
			return;
		}
		Sound.filenametemp = filename;
		Sound.postem = pos;
		Sound.status = 2;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
		}
		else
		{
			Cout.Log(string.Concat(new object[]
			{
				"Load Audio ",
				filename,
				" done in ",
				i * 5,
				"ms"
			}));
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00003CC8 File Offset: 0x00001EC8
	private static void __load(string filename, int pos)
	{
		Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		Sound.player[pos] = GameObject.Find("Main Camera");
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00003D07 File Offset: 0x00001F07
	public static void start(float volume, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__start(volume, pos);
		}
		else
		{
			Sound._start(volume, pos);
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00009848 File Offset: 0x00007A48
	public static void _start(float volume, int pos)
	{
		if (Sound.status != 0)
		{
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
			return;
		}
		Sound.volumetem = volume;
		Sound.postem = pos;
		Sound.status = 3;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR START AUDIO");
		}
		else
		{
			Debug.Log("Start Audio done in " + i * 5 + "ms");
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00003D35 File Offset: 0x00001F35
	public static void __start(float volume, int pos)
	{
		if (Sound.player[pos] == null)
		{
			return;
		}
		Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00003D63 File Offset: 0x00001F63
	public static void stop(int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__stop(pos);
		}
		else
		{
			Sound._stop(pos);
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000098D8 File Offset: 0x00007AD8
	public static void _stop(int pos)
	{
		if (Sound.status != 0)
		{
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
			return;
		}
		Sound.postem = pos;
		Sound.status = 4;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR STOP AUDIO");
		}
		else
		{
			Debug.Log("Stop Audio done in " + i * 5 + "ms");
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00003D8F File Offset: 0x00001F8F
	public static void __stop(int pos)
	{
		if (Sound.player[pos] != null)
		{
			Sound.player[pos].GetComponent<AudioSource>().Stop();
		}
	}

	// Token: 0x04000059 RID: 89
	private const int INTERVAL = 5;

	// Token: 0x0400005A RID: 90
	private const int MAXTIME = 100;

	// Token: 0x0400005B RID: 91
	public static int status;

	// Token: 0x0400005C RID: 92
	public static int postem;

	// Token: 0x0400005D RID: 93
	public static int timestart;

	// Token: 0x0400005E RID: 94
	private static string filenametemp;

	// Token: 0x0400005F RID: 95
	private static float volumetem;

	// Token: 0x04000060 RID: 96
	public static bool isSound = true;

	// Token: 0x04000061 RID: 97
	public static bool isNotPlay;

	// Token: 0x04000062 RID: 98
	public static bool stopAll;

	// Token: 0x04000063 RID: 99
	public static AudioSource SoundWater;

	// Token: 0x04000064 RID: 100
	public static AudioSource SoundRun;

	// Token: 0x04000065 RID: 101
	public static AudioSource SoundBGLoop;

	// Token: 0x04000066 RID: 102
	public static AudioClip[] music;

	// Token: 0x04000067 RID: 103
	public static GameObject[] player;

	// Token: 0x04000068 RID: 104
	public static string[] fileName = new string[]
	{
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9",
		"10",
		"11",
		"12",
		"13",
		"14",
		"15",
		"16",
		"17",
		"18",
		"19",
		"29",
		"21",
		"22",
		"23",
		"24",
		"25",
		"26",
		"27",
		"28",
		"29",
		"30",
		"31",
		"32",
		"33"
	};

	// Token: 0x04000069 RID: 105
	public static sbyte MLogin = 0;

	// Token: 0x0400006A RID: 106
	public static sbyte MBClick = 1;

	// Token: 0x0400006B RID: 107
	public static sbyte MTone = 2;

	// Token: 0x0400006C RID: 108
	public static sbyte MSanzu = 3;

	// Token: 0x0400006D RID: 109
	public static sbyte MChakumi = 4;

	// Token: 0x0400006E RID: 110
	public static sbyte MChai = 5;

	// Token: 0x0400006F RID: 111
	public static sbyte MOshin = 6;

	// Token: 0x04000070 RID: 112
	public static sbyte MEchigo = 7;

	// Token: 0x04000071 RID: 113
	public static sbyte MKojin = 8;

	// Token: 0x04000072 RID: 114
	public static sbyte MHaruna = 9;

	// Token: 0x04000073 RID: 115
	public static sbyte MHirosaki = 10;

	// Token: 0x04000074 RID: 116
	public static sbyte MOokaza = 11;

	// Token: 0x04000075 RID: 117
	public static sbyte MGiotuyet = 12;

	// Token: 0x04000076 RID: 118
	public static sbyte MHangdong = 13;

	// Token: 0x04000077 RID: 119
	public static sbyte MDeKeu = 14;

	// Token: 0x04000078 RID: 120
	public static sbyte MChimKeu = 15;

	// Token: 0x04000079 RID: 121
	public static sbyte MBuocChan = 16;

	// Token: 0x0400007A RID: 122
	public static sbyte MNuocChay = 17;

	// Token: 0x0400007B RID: 123
	public static sbyte MBomMau = 18;

	// Token: 0x0400007C RID: 124
	public static sbyte MKiemGo = 19;

	// Token: 0x0400007D RID: 125
	public static sbyte MKiem = 20;

	// Token: 0x0400007E RID: 126
	public static sbyte MTieu = 21;

	// Token: 0x0400007F RID: 127
	public static sbyte MKunai = 22;

	// Token: 0x04000080 RID: 128
	public static sbyte MCung = 23;

	// Token: 0x04000081 RID: 129
	public static sbyte MDao = 24;

	// Token: 0x04000082 RID: 130
	public static sbyte MQuat = 25;

	// Token: 0x04000083 RID: 131
	public static sbyte MCung2 = 26;

	// Token: 0x04000084 RID: 132
	public static sbyte MTieu2 = 27;

	// Token: 0x04000085 RID: 133
	public static sbyte MTieu3 = 28;

	// Token: 0x04000086 RID: 134
	public static sbyte MKiem2 = 29;

	// Token: 0x04000087 RID: 135
	public static sbyte MKiem3 = 30;

	// Token: 0x04000088 RID: 136
	public static sbyte MDao2 = 31;

	// Token: 0x04000089 RID: 137
	public static sbyte MDao3 = 32;

	// Token: 0x0400008A RID: 138
	public static sbyte MCung3 = 33;

	// Token: 0x0400008B RID: 139
	public static int l1;
}
