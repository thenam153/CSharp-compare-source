using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class MyAudioClip
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003826 File Offset: 0x00001A26
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003846 File Offset: 0x00001A46
	public void Play()
	{
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = mSystem.currentTimeMillis();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003868 File Offset: 0x00001A68
	public bool isPlaying()
	{
		return false;
	}

	// Token: 0x0400001A RID: 26
	public string name;

	// Token: 0x0400001B RID: 27
	public AudioClip clip;

	// Token: 0x0400001C RID: 28
	public long timeStart;
}
