using System;

// Token: 0x0200007E RID: 126
public class SoundMn
{
	// Token: 0x060003E7 RID: 999 RVA: 0x00005610 File Offset: 0x00003810
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00005618 File Offset: 0x00003818
	public static SoundMn gI()
	{
		if (SoundMn.gIz == null)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0001E7AC File Offset: 0x0001C9AC
	public void loadSound(int mapID)
	{
		Sound.init(new int[]
		{
			SoundMn.AIR_SHIP,
			SoundMn.RAIN,
			SoundMn.TAITAONANGLUONG
		}, new int[]
		{
			SoundMn.GET_ITEM,
			SoundMn.MOVE,
			SoundMn.LOW_PUNCH,
			SoundMn.LOW_KICK,
			SoundMn.FLY,
			SoundMn.JUMP,
			SoundMn.PANEL_OPEN,
			SoundMn.BUTTON_CLOSE,
			SoundMn.BUTTON_CLICK,
			SoundMn.MEDIUM_PUNCH,
			SoundMn.MEDIUM_KICK,
			SoundMn.PANEL_OPEN,
			SoundMn.EAT_PEAN,
			SoundMn.OPEN_DIALOG,
			SoundMn.NORMAL_KAME,
			SoundMn.NAMEK_KAME,
			SoundMn.XAYDA_KAME,
			SoundMn.EXPLODE_1,
			SoundMn.EXPLODE_2,
			SoundMn.TRAIDAT_KAME,
			SoundMn.HP_UP,
			SoundMn.THAIDUONGHASAN,
			SoundMn.HOISINH,
			SoundMn.GONG,
			SoundMn.KHICHAY,
			SoundMn.BIG_EXPLODE,
			SoundMn.NAMEK_LAZER,
			SoundMn.NAMEK_CHARGE
		});
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
	public void getSoundOption()
	{
		if (GameCanvas.loginScr.isLogin2 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2)
		{
			Panel.strTool = new string[]
			{
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account,
				mResources.REGISTOPROTECT
			};
			if (global::Char.myCharz().havePet)
			{
				Panel.strTool = new string[]
				{
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account,
					mResources.REGISTOPROTECT
				};
			}
		}
		else
		{
			Panel.strTool = new string[]
			{
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account
			};
			if (global::Char.myCharz().havePet)
			{
				Panel.strTool = new string[]
				{
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001EA8C File Offset: 0x0001CC8C
	public void getStrOption()
	{
		if (Main.isPC)
		{
			Panel.strCauhinh = new string[]
			{
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
			};
		}
		else
		{
			Panel.strCauhinh = new string[]
			{
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(GameScr.isAnalog != 0) ? mResources.turnOffAnalog : mResources.turnOnAnalog
			};
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00005633 File Offset: 0x00003833
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001EB30 File Offset: 0x0001CD30
	public void charPunch(bool isKick, float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2;
		}
		if (volumn <= 0f)
		{
			volumn = 0.01f;
		}
		int num = Res.random(0, 3);
		if (isKick)
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_KICK : SoundMn.LOW_KICK, 0.1f);
		}
		else
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_PUNCH : SoundMn.LOW_PUNCH, 0.1f);
		}
		this.poolCount++;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00005644 File Offset: 0x00003844
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00005663 File Offset: 0x00003863
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00005675 File Offset: 0x00003875
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00005694 File Offset: 0x00003894
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000056B3 File Offset: 0x000038B3
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0001EBCC File Offset: 0x0001CDCC
	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		if (GameCanvas.isPlaySound)
		{
			Panel.strCauhinh[0] = mResources.turnOffSound;
			SoundMn.gI().loadSound(TileMap.mapID);
			Rms.saveRMSInt("isPlaySound", 1);
		}
		else
		{
			Panel.strCauhinh[0] = mResources.turnOnSound;
			SoundMn.gI().closeSound();
			Rms.saveRMSInt("isPlaySound", 0);
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00003584 File Offset: 0x00001784
	public void update()
	{
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000056D2 File Offset: 0x000038D2
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000056E0 File Offset: 0x000038E0
	public void openSound()
	{
		if (Sound.music == null)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000056F9 File Offset: 0x000038F9
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00005718 File Offset: 0x00003918
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00005718 File Offset: 0x00003918
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00005737 File Offset: 0x00003937
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00005756 File Offset: 0x00003956
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00005775 File Offset: 0x00003975
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00005794 File Offset: 0x00003994
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0001EC3C File Offset: 0x0001CE3C
	public void mobKame(int type)
	{
		int id = SoundMn.XAYDA_KAME;
		if (type == 13)
		{
			id = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(id, 0.1f);
		this.poolCount++;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0001EC78 File Offset: 0x0001CE78
	public void charRun(float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2;
			if (volumn <= 0f)
			{
				volumn = 0.01f;
			}
		}
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.MOVE, volumn);
			this.poolCount++;
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000057B3 File Offset: 0x000039B3
	public void monkeyRun(float volumn)
	{
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x000057DE File Offset: 0x000039DE
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x000057FD File Offset: 0x000039FD
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0000581C File Offset: 0x00003A1C
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0000583B File Offset: 0x00003A3B
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0000585A File Offset: 0x00003A5A
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00003584 File Offset: 0x00001784
	public void stopMove()
	{
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00005879 File Offset: 0x00003A79
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00003584 File Offset: 0x00001784
	public void stopFly()
	{
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0000583B File Offset: 0x00003A3B
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00005898 File Offset: 0x00003A98
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x000058B7 File Offset: 0x00003AB7
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x000058D6 File Offset: 0x00003AD6
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000058E7 File Offset: 0x00003AE7
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00005906 File Offset: 0x00003B06
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00003584 File Offset: 0x00001784
	public void taitaoPause()
	{
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001ECD8 File Offset: 0x0001CED8
	public bool isPlayRain()
	{
		bool result;
		try
		{
			result = Sound.isPlayingSound();
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00003868 File Offset: 0x00001A68
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00005918 File Offset: 0x00003B18
	public void airShip()
	{
		SoundMn.cout++;
		if (SoundMn.cout % 2 == 0)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00003584 File Offset: 0x00001784
	public void pauseAirShip()
	{
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00003584 File Offset: 0x00001784
	public void resumeAirShip()
	{
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00005942 File Offset: 0x00003B42
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00005949 File Offset: 0x00003B49
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x040006B1 RID: 1713
	public static SoundMn gIz;

	// Token: 0x040006B2 RID: 1714
	public static bool isSound = true;

	// Token: 0x040006B3 RID: 1715
	public static int volume;

	// Token: 0x040006B4 RID: 1716
	private static int MAX_VOLUME = 10;

	// Token: 0x040006B5 RID: 1717
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x040006B6 RID: 1718
	public static SoundMn.SoundPool[] sound;

	// Token: 0x040006B7 RID: 1719
	public static int[] soundID;

	// Token: 0x040006B8 RID: 1720
	public static int AIR_SHIP;

	// Token: 0x040006B9 RID: 1721
	public static int RAIN = 1;

	// Token: 0x040006BA RID: 1722
	public static int TAITAONANGLUONG = 2;

	// Token: 0x040006BB RID: 1723
	public static int GET_ITEM;

	// Token: 0x040006BC RID: 1724
	public static int MOVE = 1;

	// Token: 0x040006BD RID: 1725
	public static int LOW_PUNCH = 2;

	// Token: 0x040006BE RID: 1726
	public static int LOW_KICK = 3;

	// Token: 0x040006BF RID: 1727
	public static int FLY = 4;

	// Token: 0x040006C0 RID: 1728
	public static int JUMP = 5;

	// Token: 0x040006C1 RID: 1729
	public static int PANEL_OPEN = 6;

	// Token: 0x040006C2 RID: 1730
	public static int BUTTON_CLOSE = 7;

	// Token: 0x040006C3 RID: 1731
	public static int BUTTON_CLICK = 8;

	// Token: 0x040006C4 RID: 1732
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x040006C5 RID: 1733
	public static int MEDIUM_KICK = 10;

	// Token: 0x040006C6 RID: 1734
	public static int PANEL_CLICK = 11;

	// Token: 0x040006C7 RID: 1735
	public static int EAT_PEAN = 12;

	// Token: 0x040006C8 RID: 1736
	public static int OPEN_DIALOG = 13;

	// Token: 0x040006C9 RID: 1737
	public static int NORMAL_KAME = 14;

	// Token: 0x040006CA RID: 1738
	public static int NAMEK_KAME = 15;

	// Token: 0x040006CB RID: 1739
	public static int XAYDA_KAME = 16;

	// Token: 0x040006CC RID: 1740
	public static int EXPLODE_1 = 17;

	// Token: 0x040006CD RID: 1741
	public static int EXPLODE_2 = 18;

	// Token: 0x040006CE RID: 1742
	public static int TRAIDAT_KAME = 19;

	// Token: 0x040006CF RID: 1743
	public static int HP_UP = 20;

	// Token: 0x040006D0 RID: 1744
	public static int THAIDUONGHASAN = 21;

	// Token: 0x040006D1 RID: 1745
	public static int HOISINH = 22;

	// Token: 0x040006D2 RID: 1746
	public static int GONG = 23;

	// Token: 0x040006D3 RID: 1747
	public static int KHICHAY = 24;

	// Token: 0x040006D4 RID: 1748
	public static int BIG_EXPLODE = 25;

	// Token: 0x040006D5 RID: 1749
	public static int NAMEK_LAZER = 26;

	// Token: 0x040006D6 RID: 1750
	public static int NAMEK_CHARGE = 27;

	// Token: 0x040006D7 RID: 1751
	public bool freePool;

	// Token: 0x040006D8 RID: 1752
	public int poolCount;

	// Token: 0x040006D9 RID: 1753
	public static int cout = 1;

	// Token: 0x0200007F RID: 127
	public class MediaPlayer
	{
	}

	// Token: 0x02000080 RID: 128
	public class SoundPool
	{
	}

	// Token: 0x02000081 RID: 129
	public class AssetManager
	{
	}
}
