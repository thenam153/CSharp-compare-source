using System;

// Token: 0x0200008C RID: 140
public class mResources
{
	// Token: 0x06000432 RID: 1074 RVA: 0x000059FF File Offset: 0x00003BFF
	public static void loadLanguague()
	{
		mResources.loadLanguague(1);
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00024EDC File Offset: 0x000230DC
	public static void loadLanguague(sbyte newLanguage)
	{
		mResources.language = newLanguage;
		switch (mResources.language)
		{
		case 0:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1.png");
			T1.load();
			ServerListScreen.linkweb = "http://ngocrongonline.com";
			break;
		case 1:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
			T2.load();
			ServerListScreen.linkweb = "http://world.teamobi.com";
			break;
		case 2:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
			T3.load();
			ServerListScreen.linkweb = "http://dragonball.indonaga.com";
			break;
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00005A07 File Offset: 0x00003C07
	public static string replace(string str, string replacement)
	{
		return NinjaUtil.replace(str, "#", replacement);
	}

	// Token: 0x0400071A RID: 1818
	public const sbyte VIETNAM = 0;

	// Token: 0x0400071B RID: 1819
	public const sbyte ENGLISH = 1;

	// Token: 0x0400071C RID: 1820
	public const sbyte INDONESIA = 2;

	// Token: 0x0400071D RID: 1821
	public static string tang;

	// Token: 0x0400071E RID: 1822
	public static string kquaVongQuay;

	// Token: 0x0400071F RID: 1823
	public static string useGem;

	// Token: 0x04000720 RID: 1824
	public static string autoFunction;

	// Token: 0x04000721 RID: 1825
	public static string choitiep;

	// Token: 0x04000722 RID: 1826
	public static string attack;

	// Token: 0x04000723 RID: 1827
	public static string defend;

	// Token: 0x04000724 RID: 1828
	public static string follow;

	// Token: 0x04000725 RID: 1829
	public static string status;

	// Token: 0x04000726 RID: 1830
	public static string gohome;

	// Token: 0x04000727 RID: 1831
	public static string pet;

	// Token: 0x04000728 RID: 1832
	public static string maychutathoacmatsong;

	// Token: 0x04000729 RID: 1833
	public static string cauhinhthap;

	// Token: 0x0400072A RID: 1834
	public static string cauhinhcao;

	// Token: 0x0400072B RID: 1835
	public static string combineSpell;

	// Token: 0x0400072C RID: 1836
	public static string combineFail;

	// Token: 0x0400072D RID: 1837
	public static string combineSuccess;

	// Token: 0x0400072E RID: 1838
	public static string turnOnAnalog;

	// Token: 0x0400072F RID: 1839
	public static string turnOffAnalog;

	// Token: 0x04000730 RID: 1840
	public static string analog;

	// Token: 0x04000731 RID: 1841
	public static string inventory_Pass;

	// Token: 0x04000732 RID: 1842
	public static string input_Inventory_Pass;

	// Token: 0x04000733 RID: 1843
	public static string input_Inventory_Pass_wrong = string.Empty;

	// Token: 0x04000734 RID: 1844
	public static string REGISTOPROTECT = string.Empty;

	// Token: 0x04000735 RID: 1845
	public static string turnOnSound = string.Empty;

	// Token: 0x04000736 RID: 1846
	public static string turnOffSound = string.Empty;

	// Token: 0x04000737 RID: 1847
	public static string REGISTERING = string.Empty;

	// Token: 0x04000738 RID: 1848
	public static string SENDINGMSG = string.Empty;

	// Token: 0x04000739 RID: 1849
	public static string SENTMSG = string.Empty;

	// Token: 0x0400073A RID: 1850
	public static string NOSENDMSG = string.Empty;

	// Token: 0x0400073B RID: 1851
	public static string sendMsgSuccess = string.Empty;

	// Token: 0x0400073C RID: 1852
	public static string cannotSendMsg = string.Empty;

	// Token: 0x0400073D RID: 1853
	public static string sendGuessMsgSuccess = string.Empty;

	// Token: 0x0400073E RID: 1854
	public static string sendMsgFail = string.Empty;

	// Token: 0x0400073F RID: 1855
	public static string ALERT_PRIVATE_PASS_1 = string.Empty;

	// Token: 0x04000740 RID: 1856
	public static string ALERT_PRIVATE_PASS_2 = string.Empty;

	// Token: 0x04000741 RID: 1857
	public static string INPUT_PRIVATE_PASS = string.Empty;

	// Token: 0x04000742 RID: 1858
	public static string change_account = string.Empty;

	// Token: 0x04000743 RID: 1859
	public static string alreadyHadAccount1 = string.Empty;

	// Token: 0x04000744 RID: 1860
	public static string alreadyHadAccount2 = string.Empty;

	// Token: 0x04000745 RID: 1861
	public static string userBlank = string.Empty;

	// Token: 0x04000746 RID: 1862
	public static string passwordBlank = string.Empty;

	// Token: 0x04000747 RID: 1863
	public static string accTooShort = string.Empty;

	// Token: 0x04000748 RID: 1864
	public static string phoneInvalid = string.Empty;

	// Token: 0x04000749 RID: 1865
	public static string emailInvalid = string.Empty;

	// Token: 0x0400074A RID: 1866
	public static string registerNewAcc = string.Empty;

	// Token: 0x0400074B RID: 1867
	public static string selectServer = string.Empty;

	// Token: 0x0400074C RID: 1868
	public static string selectServer2 = string.Empty;

	// Token: 0x0400074D RID: 1869
	public static string forgetPass = string.Empty;

	// Token: 0x0400074E RID: 1870
	public static string password = string.Empty;

	// Token: 0x0400074F RID: 1871
	public static string[] LOGINLABELS = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000750 RID: 1872
	public static string msg = string.Empty;

	// Token: 0x04000751 RID: 1873
	public static string[] msgg = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000752 RID: 1874
	public static string no_msg = string.Empty;

	// Token: 0x04000753 RID: 1875
	public static string cancelAccountProtection = string.Empty;

	// Token: 0x04000754 RID: 1876
	public static string plsCheckAcc = string.Empty;

	// Token: 0x04000755 RID: 1877
	public static string phone = string.Empty;

	// Token: 0x04000756 RID: 1878
	public static string email = string.Empty;

	// Token: 0x04000757 RID: 1879
	public static string acc = string.Empty;

	// Token: 0x04000758 RID: 1880
	public static string pwd = string.Empty;

	// Token: 0x04000759 RID: 1881
	public static string goToWebForPassword = string.Empty;

	// Token: 0x0400075A RID: 1882
	public static string dragon_ball = string.Empty;

	// Token: 0x0400075B RID: 1883
	public static string character = string.Empty;

	// Token: 0x0400075C RID: 1884
	public static string account = string.Empty;

	// Token: 0x0400075D RID: 1885
	public static string account_server = string.Empty;

	// Token: 0x0400075E RID: 1886
	public static string char_name_blank = string.Empty;

	// Token: 0x0400075F RID: 1887
	public static string char_name_short = string.Empty;

	// Token: 0x04000760 RID: 1888
	public static string char_name_long = string.Empty;

	// Token: 0x04000761 RID: 1889
	public static string changeNameChar = string.Empty;

	// Token: 0x04000762 RID: 1890
	public static string char_name = string.Empty;

	// Token: 0x04000763 RID: 1891
	public static string login = string.Empty;

	// Token: 0x04000764 RID: 1892
	public static string login2 = string.Empty;

	// Token: 0x04000765 RID: 1893
	public static string register = string.Empty;

	// Token: 0x04000766 RID: 1894
	public static string WAIT = string.Empty;

	// Token: 0x04000767 RID: 1895
	public static string PLEASEWAIT = string.Empty;

	// Token: 0x04000768 RID: 1896
	public static string CONNECTING = string.Empty;

	// Token: 0x04000769 RID: 1897
	public static string LOGGING = string.Empty;

	// Token: 0x0400076A RID: 1898
	public static string LOADING = string.Empty;

	// Token: 0x0400076B RID: 1899
	public static string downloading_data = string.Empty;

	// Token: 0x0400076C RID: 1900
	public static string select_server = string.Empty;

	// Token: 0x0400076D RID: 1901
	public static string pls_restart_game_error = string.Empty;

	// Token: 0x0400076E RID: 1902
	public static string lost_connection = string.Empty;

	// Token: 0x0400076F RID: 1903
	public static string check_3G = string.Empty;

	// Token: 0x04000770 RID: 1904
	public static string UPDATE = string.Empty;

	// Token: 0x04000771 RID: 1905
	public static string change_zone = string.Empty;

	// Token: 0x04000772 RID: 1906
	public static string select_zone = string.Empty;

	// Token: 0x04000773 RID: 1907
	public static string website = string.Empty;

	// Token: 0x04000774 RID: 1908
	public static string server = string.Empty;

	// Token: 0x04000775 RID: 1909
	public static string planet = string.Empty;

	// Token: 0x04000776 RID: 1910
	public static string[] MENUME = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000777 RID: 1911
	public static string[] MENUNEWCHAR = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000778 RID: 1912
	public static string[] MENUGENDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000779 RID: 1913
	public static string[] CHAR_ORDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400077A RID: 1914
	public static string[][] mainTab1 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x0400077B RID: 1915
	public static string[][] mainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x0400077C RID: 1916
	public static string[][] petMainTab = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x0400077D RID: 1917
	public static string[][] petMainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x0400077E RID: 1918
	public static string[] key_skill_qwerty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400077F RID: 1919
	public static string[] key_skill = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000780 RID: 1920
	public static string SKILL_FAIL = string.Empty;

	// Token: 0x04000781 RID: 1921
	public static string HP_EMPTY = string.Empty;

	// Token: 0x04000782 RID: 1922
	public static string ZONE_HERE = string.Empty;

	// Token: 0x04000783 RID: 1923
	public static string[] DES_TASK = new string[]
	{
		" ",
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000784 RID: 1924
	public static string[] DIES = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000785 RID: 1925
	public static string[] SYNTHESIS = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000786 RID: 1926
	public static string[] tips = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000787 RID: 1927
	public static string TASK_INPUT_CLASS = string.Empty;

	// Token: 0x04000788 RID: 1928
	public static string SERI_NUM = string.Empty;

	// Token: 0x04000789 RID: 1929
	public static string CARD_CODE = string.Empty;

	// Token: 0x0400078A RID: 1930
	public static string pay_card = string.Empty;

	// Token: 0x0400078B RID: 1931
	public static string pay_card2 = string.Empty;

	// Token: 0x0400078C RID: 1932
	public static string serial_blank = string.Empty;

	// Token: 0x0400078D RID: 1933
	public static string card_code_blank = string.Empty;

	// Token: 0x0400078E RID: 1934
	public static string billion = string.Empty;

	// Token: 0x0400078F RID: 1935
	public static string million = string.Empty;

	// Token: 0x04000790 RID: 1936
	public static string MENU = string.Empty;

	// Token: 0x04000791 RID: 1937
	public static string CLOSE = string.Empty;

	// Token: 0x04000792 RID: 1938
	public static string ON = string.Empty;

	// Token: 0x04000793 RID: 1939
	public static string OFF = string.Empty;

	// Token: 0x04000794 RID: 1940
	public static string ENABLE = string.Empty;

	// Token: 0x04000795 RID: 1941
	public static string DELETE = string.Empty;

	// Token: 0x04000796 RID: 1942
	public static string VIEW = string.Empty;

	// Token: 0x04000797 RID: 1943
	public static string CONTINUE = string.Empty;

	// Token: 0x04000798 RID: 1944
	public static string NEXTSTEP = string.Empty;

	// Token: 0x04000799 RID: 1945
	public static string USE = string.Empty;

	// Token: 0x0400079A RID: 1946
	public static string SORT = string.Empty;

	// Token: 0x0400079B RID: 1947
	public static string YES = string.Empty;

	// Token: 0x0400079C RID: 1948
	public static string NO = string.Empty;

	// Token: 0x0400079D RID: 1949
	public static string EXIT = string.Empty;

	// Token: 0x0400079E RID: 1950
	public static string CHAT = string.Empty;

	// Token: 0x0400079F RID: 1951
	public static string REVENGE = string.Empty;

	// Token: 0x040007A0 RID: 1952
	public static string OK = string.Empty;

	// Token: 0x040007A1 RID: 1953
	public static string retry = string.Empty;

	// Token: 0x040007A2 RID: 1954
	public static string uncheck = string.Empty;

	// Token: 0x040007A3 RID: 1955
	public static string remember = string.Empty;

	// Token: 0x040007A4 RID: 1956
	public static string ACCEPT = string.Empty;

	// Token: 0x040007A5 RID: 1957
	public static string CANCEL = string.Empty;

	// Token: 0x040007A6 RID: 1958
	public static string SELECT = string.Empty;

	// Token: 0x040007A7 RID: 1959
	public static string enter = string.Empty;

	// Token: 0x040007A8 RID: 1960
	public static string open_link = string.Empty;

	// Token: 0x040007A9 RID: 1961
	public static string DOYOUWANTEXIT = string.Empty;

	// Token: 0x040007AA RID: 1962
	public static string NEWCHAR = string.Empty;

	// Token: 0x040007AB RID: 1963
	public static string BACK = string.Empty;

	// Token: 0x040007AC RID: 1964
	public static string LOCKED = string.Empty;

	// Token: 0x040007AD RID: 1965
	public static string KILL = string.Empty;

	// Token: 0x040007AE RID: 1966
	public static string KILLBOSS = string.Empty;

	// Token: 0x040007AF RID: 1967
	public static string NOLOCK = string.Empty;

	// Token: 0x040007B0 RID: 1968
	public static string XU = string.Empty;

	// Token: 0x040007B1 RID: 1969
	public static string LUONG = string.Empty;

	// Token: 0x040007B2 RID: 1970
	public static string PK_NOW = string.Empty;

	// Token: 0x040007B3 RID: 1971
	public static string CUU_SAT = string.Empty;

	// Token: 0x040007B4 RID: 1972
	public static string NOT_ENOUGH_MP = string.Empty;

	// Token: 0x040007B5 RID: 1973
	public static string you_receive = string.Empty;

	// Token: 0x040007B6 RID: 1974
	public static string MONTH = string.Empty;

	// Token: 0x040007B7 RID: 1975
	public static string WEEK = string.Empty;

	// Token: 0x040007B8 RID: 1976
	public static string DAY = string.Empty;

	// Token: 0x040007B9 RID: 1977
	public static string HOUR = string.Empty;

	// Token: 0x040007BA RID: 1978
	public static string SECOND = string.Empty;

	// Token: 0x040007BB RID: 1979
	public static string MINUTE = string.Empty;

	// Token: 0x040007BC RID: 1980
	public static string LEARN_SKILL = string.Empty;

	// Token: 0x040007BD RID: 1981
	public static string rank = string.Empty;

	// Token: 0x040007BE RID: 1982
	public static string active_point = string.Empty;

	// Token: 0x040007BF RID: 1983
	public static string friend = string.Empty;

	// Token: 0x040007C0 RID: 1984
	public static string enemy = string.Empty;

	// Token: 0x040007C1 RID: 1985
	public static string no_friend = string.Empty;

	// Token: 0x040007C2 RID: 1986
	public static string chat_world = string.Empty;

	// Token: 0x040007C3 RID: 1987
	public static string change_flag = string.Empty;

	// Token: 0x040007C4 RID: 1988
	public static string gameInfo = string.Empty;

	// Token: 0x040007C5 RID: 1989
	public static string quayso = string.Empty;

	// Token: 0x040007C6 RID: 1990
	public static string option = string.Empty;

	// Token: 0x040007C7 RID: 1991
	public static string high = string.Empty;

	// Token: 0x040007C8 RID: 1992
	public static string medium = string.Empty;

	// Token: 0x040007C9 RID: 1993
	public static string low = string.Empty;

	// Token: 0x040007CA RID: 1994
	public static string increase_vga = string.Empty;

	// Token: 0x040007CB RID: 1995
	public static string decrease_vga = string.Empty;

	// Token: 0x040007CC RID: 1996
	public static string serverchat_off = string.Empty;

	// Token: 0x040007CD RID: 1997
	public static string serverchat_on = string.Empty;

	// Token: 0x040007CE RID: 1998
	public static string x2Screen = string.Empty;

	// Token: 0x040007CF RID: 1999
	public static string x1Screen = string.Empty;

	// Token: 0x040007D0 RID: 2000
	public static string changeSizeScreen = string.Empty;

	// Token: 0x040007D1 RID: 2001
	public static string chest = string.Empty;

	// Token: 0x040007D2 RID: 2002
	public static string[] chestt = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D3 RID: 2003
	public static string[] inventory = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D4 RID: 2004
	public static string[] combine = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D5 RID: 2005
	public static string[] mapp = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D6 RID: 2006
	public static string[] item_give = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D7 RID: 2007
	public static string[] item_receive = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D8 RID: 2008
	public static string[] zonee = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040007D9 RID: 2009
	public static string zone = string.Empty;

	// Token: 0x040007DA RID: 2010
	public static string map = string.Empty;

	// Token: 0x040007DB RID: 2011
	public static string item_receive2 = string.Empty;

	// Token: 0x040007DC RID: 2012
	public static string item = string.Empty;

	// Token: 0x040007DD RID: 2013
	public static string give_upper = string.Empty;

	// Token: 0x040007DE RID: 2014
	public static string receive_upper = string.Empty;

	// Token: 0x040007DF RID: 2015
	public static string no_map = string.Empty;

	// Token: 0x040007E0 RID: 2016
	public static string go_to_quest = string.Empty;

	// Token: 0x040007E1 RID: 2017
	public static string from_earth = string.Empty;

	// Token: 0x040007E2 RID: 2018
	public static string from_namec = string.Empty;

	// Token: 0x040007E3 RID: 2019
	public static string from_sayda = string.Empty;

	// Token: 0x040007E4 RID: 2020
	public static string expire = string.Empty;

	// Token: 0x040007E5 RID: 2021
	public static string pow_request = string.Empty;

	// Token: 0x040007E6 RID: 2022
	public static string your_pow = string.Empty;

	// Token: 0x040007E7 RID: 2023
	public static string used = string.Empty;

	// Token: 0x040007E8 RID: 2024
	public static string place = string.Empty;

	// Token: 0x040007E9 RID: 2025
	public static string FOREVER = string.Empty;

	// Token: 0x040007EA RID: 2026
	public static string NOUPGRADE = string.Empty;

	// Token: 0x040007EB RID: 2027
	public static string NOTUPGRADE = string.Empty;

	// Token: 0x040007EC RID: 2028
	public static string UPGRADE = string.Empty;

	// Token: 0x040007ED RID: 2029
	public static string UPGRADING = string.Empty;

	// Token: 0x040007EE RID: 2030
	public static string make_shortcut = string.Empty;

	// Token: 0x040007EF RID: 2031
	public static string into_place = string.Empty;

	// Token: 0x040007F0 RID: 2032
	public static string move_to_chest = string.Empty;

	// Token: 0x040007F1 RID: 2033
	public static string move_to_chest2 = string.Empty;

	// Token: 0x040007F2 RID: 2034
	public static string press_chat_querty = string.Empty;

	// Token: 0x040007F3 RID: 2035
	public static string press_chat = string.Empty;

	// Token: 0x040007F4 RID: 2036
	public static string saying = string.Empty;

	// Token: 0x040007F5 RID: 2037
	public static string miss = string.Empty;

	// Token: 0x040007F6 RID: 2038
	public static string donate = string.Empty;

	// Token: 0x040007F7 RID: 2039
	public static string receive = string.Empty;

	// Token: 0x040007F8 RID: 2040
	public static string press_twice = string.Empty;

	// Token: 0x040007F9 RID: 2041
	public static string can_harvest = string.Empty;

	// Token: 0x040007FA RID: 2042
	public static string do_accept_qwerty = string.Empty;

	// Token: 0x040007FB RID: 2043
	public static string do_accept = string.Empty;

	// Token: 0x040007FC RID: 2044
	public static string plsRestartGame = string.Empty;

	// Token: 0x040007FD RID: 2045
	public static string is_online = string.Empty;

	// Token: 0x040007FE RID: 2046
	public static string is_offline = string.Empty;

	// Token: 0x040007FF RID: 2047
	public static string make_friend = string.Empty;

	// Token: 0x04000800 RID: 2048
	public static string chat_player = string.Empty;

	// Token: 0x04000801 RID: 2049
	public static string chat_with = string.Empty;

	// Token: 0x04000802 RID: 2050
	public static string clan_point = string.Empty;

	// Token: 0x04000803 RID: 2051
	public static string give_pea = string.Empty;

	// Token: 0x04000804 RID: 2052
	public static string receive_pea = string.Empty;

	// Token: 0x04000805 RID: 2053
	public static string request_pea = string.Empty;

	// Token: 0x04000806 RID: 2054
	public static string time = string.Empty;

	// Token: 0x04000807 RID: 2055
	public static string received = string.Empty;

	// Token: 0x04000808 RID: 2056
	public static string power = string.Empty;

	// Token: 0x04000809 RID: 2057
	public static string join_date = string.Empty;

	// Token: 0x0400080A RID: 2058
	public static string clan_leader = string.Empty;

	// Token: 0x0400080B RID: 2059
	public static string clan_coleader = string.Empty;

	// Token: 0x0400080C RID: 2060
	public static string power_point = string.Empty;

	// Token: 0x0400080D RID: 2061
	public static string member = string.Empty;

	// Token: 0x0400080E RID: 2062
	public static string[] memberr = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400080F RID: 2063
	public static string[] chatClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000810 RID: 2064
	public static string[] leaveClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000811 RID: 2065
	public static string[] createClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000812 RID: 2066
	public static string[] findClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000813 RID: 2067
	public static string[] khau_hieuu = new string[]
	{
		string.Empty
	};

	// Token: 0x04000814 RID: 2068
	public static string[] bieu_tuongg = new string[]
	{
		string.Empty
	};

	// Token: 0x04000815 RID: 2069
	public static string[] request_pea2 = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000816 RID: 2070
	public static string level = string.Empty;

	// Token: 0x04000817 RID: 2071
	public static string clan_birthday = string.Empty;

	// Token: 0x04000818 RID: 2072
	public static string clan_list = string.Empty;

	// Token: 0x04000819 RID: 2073
	public static string create = string.Empty;

	// Token: 0x0400081A RID: 2074
	public static string find = string.Empty;

	// Token: 0x0400081B RID: 2075
	public static string leave = string.Empty;

	// Token: 0x0400081C RID: 2076
	public static string not_join_clan = string.Empty;

	// Token: 0x0400081D RID: 2077
	public static string[] clanEmpty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400081E RID: 2078
	public static string input_clan_name = string.Empty;

	// Token: 0x0400081F RID: 2079
	public static string clan_name = string.Empty;

	// Token: 0x04000820 RID: 2080
	public static string chat_clan = string.Empty;

	// Token: 0x04000821 RID: 2081
	public static string input_clan_name_to_create = string.Empty;

	// Token: 0x04000822 RID: 2082
	public static string input_clan_slogan = string.Empty;

	// Token: 0x04000823 RID: 2083
	public static string do_u_want_join_clan = string.Empty;

	// Token: 0x04000824 RID: 2084
	public static string select_clan_icon = string.Empty;

	// Token: 0x04000825 RID: 2085
	public static string request_join_clan = string.Empty;

	// Token: 0x04000826 RID: 2086
	public static string view_clan_member = string.Empty;

	// Token: 0x04000827 RID: 2087
	public static string create_clan_co_leader = string.Empty;

	// Token: 0x04000828 RID: 2088
	public static string create_clan_leader = string.Empty;

	// Token: 0x04000829 RID: 2089
	public static string disable_clan_mastership = string.Empty;

	// Token: 0x0400082A RID: 2090
	public static string kick_clan_mem = string.Empty;

	// Token: 0x0400082B RID: 2091
	public static string clan_name_blank = string.Empty;

	// Token: 0x0400082C RID: 2092
	public static string clan_slogan_blank = string.Empty;

	// Token: 0x0400082D RID: 2093
	public static string cannot_find_clan = string.Empty;

	// Token: 0x0400082E RID: 2094
	public static string ago = string.Empty;

	// Token: 0x0400082F RID: 2095
	public static string findingClan = string.Empty;

	// Token: 0x04000830 RID: 2096
	public static string trade = string.Empty;

	// Token: 0x04000831 RID: 2097
	public static string not_lock_trade = string.Empty;

	// Token: 0x04000832 RID: 2098
	public static string not_lock_trade_upper = string.Empty;

	// Token: 0x04000833 RID: 2099
	public static string locked_trade = string.Empty;

	// Token: 0x04000834 RID: 2100
	public static string locked_trade_upper = string.Empty;

	// Token: 0x04000835 RID: 2101
	public static string lock_trade = string.Empty;

	// Token: 0x04000836 RID: 2102
	public static string wait_opp_lock_trade = string.Empty;

	// Token: 0x04000837 RID: 2103
	public static string press_done = string.Empty;

	// Token: 0x04000838 RID: 2104
	public static string THROW = string.Empty;

	// Token: 0x04000839 RID: 2105
	public static string SPLIT = string.Empty;

	// Token: 0x0400083A RID: 2106
	public static string done = string.Empty;

	// Token: 0x0400083B RID: 2107
	public static string opponent = string.Empty;

	// Token: 0x0400083C RID: 2108
	public static string you = string.Empty;

	// Token: 0x0400083D RID: 2109
	public static string mlock = string.Empty;

	// Token: 0x0400083E RID: 2110
	public static string money_trade = string.Empty;

	// Token: 0x0400083F RID: 2111
	public static string GETOUT = string.Empty;

	// Token: 0x04000840 RID: 2112
	public static string MOVEOUT = string.Empty;

	// Token: 0x04000841 RID: 2113
	public static string MOVEFORPET = string.Empty;

	// Token: 0x04000842 RID: 2114
	public static string GETOUTMONEY = string.Empty;

	// Token: 0x04000843 RID: 2115
	public static string GETINMONEY = string.Empty;

	// Token: 0x04000844 RID: 2116
	public static string SENDMONEY = string.Empty;

	// Token: 0x04000845 RID: 2117
	public static string GETIN = string.Empty;

	// Token: 0x04000846 RID: 2118
	public static string SALE = string.Empty;

	// Token: 0x04000847 RID: 2119
	public static string SALES = string.Empty;

	// Token: 0x04000848 RID: 2120
	public static string SALEALL = string.Empty;

	// Token: 0x04000849 RID: 2121
	public static string BUY = string.Empty;

	// Token: 0x0400084A RID: 2122
	public static string BUYS = string.Empty;

	// Token: 0x0400084B RID: 2123
	public static string input_money_to_trade = string.Empty;

	// Token: 0x0400084C RID: 2124
	public static string input_money = string.Empty;

	// Token: 0x0400084D RID: 2125
	public static string input_money_wrong = string.Empty;

	// Token: 0x0400084E RID: 2126
	public static string not_enough_money = string.Empty;

	// Token: 0x0400084F RID: 2127
	public static string input_quantity_to_trade = string.Empty;

	// Token: 0x04000850 RID: 2128
	public static string input_quantity = string.Empty;

	// Token: 0x04000851 RID: 2129
	public static string input_quantity_wrong = string.Empty;

	// Token: 0x04000852 RID: 2130
	public static string already_has_item = string.Empty;

	// Token: 0x04000853 RID: 2131
	public static string unlock_item_to_trade = string.Empty;

	// Token: 0x04000854 RID: 2132
	public static string root = string.Empty;

	// Token: 0x04000855 RID: 2133
	public static string need = string.Empty;

	// Token: 0x04000856 RID: 2134
	public static string need_upper = string.Empty;

	// Token: 0x04000857 RID: 2135
	public static string free = string.Empty;

	// Token: 0x04000858 RID: 2136
	public static string free1 = string.Empty;

	// Token: 0x04000859 RID: 2137
	public static string free2 = string.Empty;

	// Token: 0x0400085A RID: 2138
	public static string select_item = string.Empty;

	// Token: 0x0400085B RID: 2139
	public static string random = string.Empty;

	// Token: 0x0400085C RID: 2140
	public static string say_hello = string.Empty;

	// Token: 0x0400085D RID: 2141
	public static string say_wat_do_u_want_to_buy = string.Empty;

	// Token: 0x0400085E RID: 2142
	public static string say_wat_do_u_want_to_buy2 = string.Empty;

	// Token: 0x0400085F RID: 2143
	public static string do_u_sure_to_trade = string.Empty;

	// Token: 0x04000860 RID: 2144
	public static string learn_with = string.Empty;

	// Token: 0x04000861 RID: 2145
	public static string buy_with = string.Empty;

	// Token: 0x04000862 RID: 2146
	public static string can_not_do_when_die = string.Empty;

	// Token: 0x04000863 RID: 2147
	public static string use_for_combine = string.Empty;

	// Token: 0x04000864 RID: 2148
	public static string use_for_trade = string.Empty;

	// Token: 0x04000865 RID: 2149
	public static string not_enough_luong_world_channel = string.Empty;

	// Token: 0x04000866 RID: 2150
	public static string world_channel_5_luong = string.Empty;

	// Token: 0x04000867 RID: 2151
	public static string want_to_trade = string.Empty;

	// Token: 0x04000868 RID: 2152
	public static string hasJustUpgrade1 = string.Empty;

	// Token: 0x04000869 RID: 2153
	public static string hasJustUpgrade2 = string.Empty;

	// Token: 0x0400086A RID: 2154
	public static string potential_to_learn = string.Empty;

	// Token: 0x0400086B RID: 2155
	public static string potential_point = string.Empty;

	// Token: 0x0400086C RID: 2156
	public static string achievement_point = string.Empty;

	// Token: 0x0400086D RID: 2157
	public static string increase = string.Empty;

	// Token: 0x0400086E RID: 2158
	public static string increase_upper = string.Empty;

	// Token: 0x0400086F RID: 2159
	public static string not_enough_potential_point1 = string.Empty;

	// Token: 0x04000870 RID: 2160
	public static string not_enough_potential_point2 = string.Empty;

	// Token: 0x04000871 RID: 2161
	public static string use_potential_point_for1 = string.Empty;

	// Token: 0x04000872 RID: 2162
	public static string use_potential_point_for2 = string.Empty;

	// Token: 0x04000873 RID: 2163
	public static string for_HP = string.Empty;

	// Token: 0x04000874 RID: 2164
	public static string for_KI = string.Empty;

	// Token: 0x04000875 RID: 2165
	public static string for_hit_point = string.Empty;

	// Token: 0x04000876 RID: 2166
	public static string for_armor = string.Empty;

	// Token: 0x04000877 RID: 2167
	public static string for_crit = string.Empty;

	// Token: 0x04000878 RID: 2168
	public static string can_buy_from_Uron1 = string.Empty;

	// Token: 0x04000879 RID: 2169
	public static string can_buy_from_Uron2 = string.Empty;

	// Token: 0x0400087A RID: 2170
	public static string can_buy_from_Uron3 = string.Empty;

	// Token: 0x0400087B RID: 2171
	public static string HP = string.Empty;

	// Token: 0x0400087C RID: 2172
	public static string KI = string.Empty;

	// Token: 0x0400087D RID: 2173
	public static string hit_point = string.Empty;

	// Token: 0x0400087E RID: 2174
	public static string armor = string.Empty;

	// Token: 0x0400087F RID: 2175
	public static string vitality = string.Empty;

	// Token: 0x04000880 RID: 2176
	public static string critical = string.Empty;

	// Token: 0x04000881 RID: 2177
	public static string cap_do = string.Empty;

	// Token: 0x04000882 RID: 2178
	public static string KI_consume = string.Empty;

	// Token: 0x04000883 RID: 2179
	public static string speed = string.Empty;

	// Token: 0x04000884 RID: 2180
	public static string milisecond = string.Empty;

	// Token: 0x04000885 RID: 2181
	public static string max_level_reach = string.Empty;

	// Token: 0x04000886 RID: 2182
	public static string next_level_require = string.Empty;

	// Token: 0x04000887 RID: 2183
	public static string potential = string.Empty;

	// Token: 0x04000888 RID: 2184
	public static string not_learn = string.Empty;

	// Token: 0x04000889 RID: 2185
	public static string learn_require = string.Empty;

	// Token: 0x0400088A RID: 2186
	public static string learn = string.Empty;

	// Token: 0x0400088B RID: 2187
	public static string to_gain_20hp = string.Empty;

	// Token: 0x0400088C RID: 2188
	public static string to_gain_20mp = string.Empty;

	// Token: 0x0400088D RID: 2189
	public static string to_gain_1pow = string.Empty;

	// Token: 0x0400088E RID: 2190
	public static string[][] hairStyleName = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x0400088F RID: 2191
	public static string hp_ki_full = string.Empty;

	// Token: 0x04000890 RID: 2192
	public static string quest_place = string.Empty;

	// Token: 0x04000891 RID: 2193
	public static string no_mission = string.Empty;

	// Token: 0x04000892 RID: 2194
	public static string reward_mission = string.Empty;

	// Token: 0x04000893 RID: 2195
	public static string achievement_mission = string.Empty;

	// Token: 0x04000894 RID: 2196
	public static string trangbi = string.Empty;

	// Token: 0x04000895 RID: 2197
	public static string wat_do_u_want = string.Empty;

	// Token: 0x04000896 RID: 2198
	public static string off = string.Empty;

	// Token: 0x04000897 RID: 2199
	public static string on = string.Empty;

	// Token: 0x04000898 RID: 2200
	public static string select_map = string.Empty;

	// Token: 0x04000899 RID: 2201
	public static string offPlease = string.Empty;

	// Token: 0x0400089A RID: 2202
	public static string onPlease = string.Empty;

	// Token: 0x0400089B RID: 2203
	public static sbyte language;

	// Token: 0x0400089C RID: 2204
	public static string choigame;

	// Token: 0x0400089D RID: 2205
	public static string no_enemy = string.Empty;

	// Token: 0x0400089E RID: 2206
	public static string kigui;

	// Token: 0x0400089F RID: 2207
	public static string kiguiXu;

	// Token: 0x040008A0 RID: 2208
	public static string kiguiLuong;

	// Token: 0x040008A1 RID: 2209
	public static string kiguiXuchat;

	// Token: 0x040008A2 RID: 2210
	public static string kiguiLuongchat;

	// Token: 0x040008A3 RID: 2211
	public static string huykigui;

	// Token: 0x040008A4 RID: 2212
	public static string nhantien;

	// Token: 0x040008A5 RID: 2213
	public static string dangban;

	// Token: 0x040008A6 RID: 2214
	public static string daban;

	// Token: 0x040008A7 RID: 2215
	public static string num;

	// Token: 0x040008A8 RID: 2216
	public static string upTop;

	// Token: 0x040008A9 RID: 2217
	public static string page;

	// Token: 0x040008AA RID: 2218
	public static string getDown;

	// Token: 0x040008AB RID: 2219
	public static string getUp;

	// Token: 0x040008AC RID: 2220
	public static string notYetSell;

	// Token: 0x040008AD RID: 2221
	public static string charger;

	// Token: 0x040008AE RID: 2222
	public static string finishBomong;

	// Token: 0x040008AF RID: 2223
	public static string note;

	// Token: 0x040008B0 RID: 2224
	public static string regNote;

	// Token: 0x040008B1 RID: 2225
	public static string remain;

	// Token: 0x040008B2 RID: 2226
	public static string faster;

	// Token: 0x040008B3 RID: 2227
	public static string fasterQuestion;

	// Token: 0x040008B4 RID: 2228
	public static string chuacotaikhoan;

	// Token: 0x040008B5 RID: 2229
	public static string taidulieudechoi;

	// Token: 0x040008B6 RID: 2230
	public static string huy;

	// Token: 0x040008B7 RID: 2231
	public static string taidulieu;

	// Token: 0x040008B8 RID: 2232
	public static string xoadulieu;

	// Token: 0x040008B9 RID: 2233
	public static string deletaDataNote;

	// Token: 0x040008BA RID: 2234
	public static string playNew;

	// Token: 0x040008BB RID: 2235
	public static string playAcc;

	// Token: 0x040008BC RID: 2236
	public static string vuilongnhapduthongtin;

	// Token: 0x040008BD RID: 2237
	public static string not_register_yet = string.Empty;

	// Token: 0x040008BE RID: 2238
	public static string nhanngoc;

	// Token: 0x040008BF RID: 2239
	public static string fusion;

	// Token: 0x040008C0 RID: 2240
	public static string sure_fusion;

	// Token: 0x040008C1 RID: 2241
	public static string fusionForever;

	// Token: 0x040008C2 RID: 2242
	public static string xinchucmung;

	// Token: 0x040008C3 RID: 2243
	public static string den;

	// Token: 0x040008C4 RID: 2244
	public static string nhatvatpham;
}
