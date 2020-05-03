using System;

// Token: 0x0200004B RID: 75
public class Cmd
{
	// Token: 0x0400035B RID: 859
	public const sbyte LOGIN = 0;

	// Token: 0x0400035C RID: 860
	public const sbyte REGISTER = 1;

	// Token: 0x0400035D RID: 861
	public const sbyte CLIENT_INFO = 2;

	// Token: 0x0400035E RID: 862
	public const sbyte SEND_SMS = 3;

	// Token: 0x0400035F RID: 863
	public const sbyte REGISTER_IMAGE = 4;

	// Token: 0x04000360 RID: 864
	public const sbyte MESSAGE_TIME = 65;

	// Token: 0x04000361 RID: 865
	public const sbyte LOGOUT = 0;

	// Token: 0x04000362 RID: 866
	public const sbyte SELECT_PLAYER = 1;

	// Token: 0x04000363 RID: 867
	public const sbyte CREATE_PLAYER = 2;

	// Token: 0x04000364 RID: 868
	public const sbyte DELETE_PLAYER = 3;

	// Token: 0x04000365 RID: 869
	public const sbyte UPDATE_VERSION = 4;

	// Token: 0x04000366 RID: 870
	public const sbyte UPDATE_MAP = 6;

	// Token: 0x04000367 RID: 871
	public const sbyte UPDATE_SKILL = 7;

	// Token: 0x04000368 RID: 872
	public const sbyte UPDATE_ITEM = 8;

	// Token: 0x04000369 RID: 873
	public const sbyte REQUEST_SKILL = 9;

	// Token: 0x0400036A RID: 874
	public const sbyte REQUEST_MAPTEMPLATE = 10;

	// Token: 0x0400036B RID: 875
	public const sbyte REQUEST_NPCTEMPLATE = 11;

	// Token: 0x0400036C RID: 876
	public const sbyte REQUEST_NPCPLAYER = 12;

	// Token: 0x0400036D RID: 877
	public const sbyte UPDATE_TYPE_PK = 35;

	// Token: 0x0400036E RID: 878
	public const sbyte PLAYER_ATTACK_PLAYER = -60;

	// Token: 0x0400036F RID: 879
	public const sbyte PLAYER_VS_PLAYER = -59;

	// Token: 0x04000370 RID: 880
	public const sbyte CLAN_PARTY = -58;

	// Token: 0x04000371 RID: 881
	public const sbyte CLAN_INVITE = -57;

	// Token: 0x04000372 RID: 882
	public const sbyte CLAN_REMOTE = -56;

	// Token: 0x04000373 RID: 883
	public const sbyte CLAN_LEAVE = -55;

	// Token: 0x04000374 RID: 884
	public const sbyte CLAN_DONATE = -54;

	// Token: 0x04000375 RID: 885
	public const sbyte CLAN_MESSAGE = -51;

	// Token: 0x04000376 RID: 886
	public const sbyte CLAN_UPDATE = -52;

	// Token: 0x04000377 RID: 887
	public const sbyte CLAN_INFO = -53;

	// Token: 0x04000378 RID: 888
	public const sbyte CLAN_JOIN = -49;

	// Token: 0x04000379 RID: 889
	public const sbyte CLAN_MEMBER = -50;

	// Token: 0x0400037A RID: 890
	public const sbyte CLAN_SEARCH = -47;

	// Token: 0x0400037B RID: 891
	public const sbyte CLAN_CREATE_INFO = -46;

	// Token: 0x0400037C RID: 892
	public const sbyte CLIENT_OK = 13;

	// Token: 0x0400037D RID: 893
	public const sbyte CLIENT_OK_INMAP = 14;

	// Token: 0x0400037E RID: 894
	public const sbyte UPDATE_VERSION_OK = 15;

	// Token: 0x0400037F RID: 895
	public const sbyte INPUT_CARD = 16;

	// Token: 0x04000380 RID: 896
	public const sbyte CLEAR_TASK = 17;

	// Token: 0x04000381 RID: 897
	public const sbyte CHANGE_NAME = 18;

	// Token: 0x04000382 RID: 898
	public const sbyte UPDATE_PK = 20;

	// Token: 0x04000383 RID: 899
	public const sbyte CREATE_CLAN = 21;

	// Token: 0x04000384 RID: 900
	public const sbyte CONVERT_UPGRADE = 33;

	// Token: 0x04000385 RID: 901
	public const sbyte INVITE_CLANDUN = 34;

	// Token: 0x04000386 RID: 902
	public const sbyte NOT_USEACC = 35;

	// Token: 0x04000387 RID: 903
	public const sbyte ME_LOAD_ACTIVE = 36;

	// Token: 0x04000388 RID: 904
	public const sbyte ME_ACTIVE = 37;

	// Token: 0x04000389 RID: 905
	public const sbyte ME_UPDATE_ACTIVE = 38;

	// Token: 0x0400038A RID: 906
	public const sbyte ME_OPEN_LOCK = 39;

	// Token: 0x0400038B RID: 907
	public const sbyte ITEM_SPLIT = 40;

	// Token: 0x0400038C RID: 908
	public const sbyte ME_CLEAR_LOCK = 41;

	// Token: 0x0400038D RID: 909
	public const sbyte ME_LOAD_ALL = 0;

	// Token: 0x0400038E RID: 910
	public const sbyte ME_LOAD_CLASS = 1;

	// Token: 0x0400038F RID: 911
	public const sbyte ME_LOAD_SKILL = 2;

	// Token: 0x04000390 RID: 912
	public const sbyte ME_LOAD_INFO = 4;

	// Token: 0x04000391 RID: 913
	public const sbyte ME_LOAD_HP = 5;

	// Token: 0x04000392 RID: 914
	public const sbyte ME_LOAD_MP = 6;

	// Token: 0x04000393 RID: 915
	public const sbyte PLAYER_LOAD_ALL = 7;

	// Token: 0x04000394 RID: 916
	public const sbyte PLAYER_SPEED = 8;

	// Token: 0x04000395 RID: 917
	public const sbyte PLAYER_LOAD_LEVEL = 9;

	// Token: 0x04000396 RID: 918
	public const sbyte PLAYER_LOAD_VUKHI = 10;

	// Token: 0x04000397 RID: 919
	public const sbyte PLAYER_LOAD_AO = 11;

	// Token: 0x04000398 RID: 920
	public const sbyte PLAYER_LOAD_QUAN = 12;

	// Token: 0x04000399 RID: 921
	public const sbyte PLAYER_LOAD_BODY = 13;

	// Token: 0x0400039A RID: 922
	public const sbyte PLAYER_LOAD_HP = 14;

	// Token: 0x0400039B RID: 923
	public const sbyte PLAYER_LOAD_LIVE = 15;

	// Token: 0x0400039C RID: 924
	public const sbyte GOTO_PLAYER = 18;

	// Token: 0x0400039D RID: 925
	public const sbyte POTENTIAL_UP = 16;

	// Token: 0x0400039E RID: 926
	public const sbyte SKILL_UP = 17;

	// Token: 0x0400039F RID: 927
	public const sbyte BAG_SORT = 18;

	// Token: 0x040003A0 RID: 928
	public const sbyte BOX_SORT = 19;

	// Token: 0x040003A1 RID: 929
	public const sbyte BOX_COIN_IN = 20;

	// Token: 0x040003A2 RID: 930
	public const sbyte BOX_COIN_OUT = 21;

	// Token: 0x040003A3 RID: 931
	public const sbyte REQUEST_ITEM = 22;

	// Token: 0x040003A4 RID: 932
	public const sbyte ME_ADD_SKILL = 23;

	// Token: 0x040003A5 RID: 933
	public const sbyte ME_UPDATE_SKILL = 62;

	// Token: 0x040003A6 RID: 934
	public const sbyte GET_PLAYER_MENU = 63;

	// Token: 0x040003A7 RID: 935
	public const sbyte PLAYER_MENU_ACTION = 64;

	// Token: 0x040003A8 RID: 936
	public const sbyte SAVE_RMS = 60;

	// Token: 0x040003A9 RID: 937
	public const sbyte LOAD_RMS = 61;

	// Token: 0x040003AA RID: 938
	public const sbyte USE_BOOK_SKILL = 43;

	// Token: 0x040003AB RID: 939
	public const sbyte LOCK_INVENTORY = -104;

	// Token: 0x040003AC RID: 940
	public const sbyte CHANGE_FLAG = -103;

	// Token: 0x040003AD RID: 941
	public const sbyte LOGINFAIL = -102;

	// Token: 0x040003AE RID: 942
	public const sbyte LOGIN2 = -101;

	// Token: 0x040003AF RID: 943
	public const sbyte KIGUI = -100;

	// Token: 0x040003B0 RID: 944
	public const sbyte ENEMY_LIST = -99;

	// Token: 0x040003B1 RID: 945
	public const sbyte ANDROID_IAP = -98;

	// Token: 0x040003B2 RID: 946
	public const sbyte UPDATE_ACTIVEPOINT = -97;

	// Token: 0x040003B3 RID: 947
	public const sbyte TOP = -96;

	// Token: 0x040003B4 RID: 948
	public const sbyte MOB_ME_UPDATE = -95;

	// Token: 0x040003B5 RID: 949
	public const sbyte UPDATE_COOLDOWN = -94;

	// Token: 0x040003B6 RID: 950
	public const sbyte BGITEM_VERSION = -93;

	// Token: 0x040003B7 RID: 951
	public const sbyte SET_CLIENTTYPE = -92;

	// Token: 0x040003B8 RID: 952
	public const sbyte MAP_TRASPORT = -91;

	// Token: 0x040003B9 RID: 953
	public const sbyte UPDATE_BODY = -90;

	// Token: 0x040003BA RID: 954
	public const sbyte SERVERSCREEN = -88;

	// Token: 0x040003BB RID: 955
	public const sbyte UPDATE_DATA = -87;

	// Token: 0x040003BC RID: 956
	public const sbyte GIAO_DICH = -86;

	// Token: 0x040003BD RID: 957
	public const sbyte MOB_CAPCHA = -85;

	// Token: 0x040003BE RID: 958
	public const sbyte MOB_MAX_HP = -84;

	// Token: 0x040003BF RID: 959
	public const sbyte CALL_DRAGON = -83;

	// Token: 0x040003C0 RID: 960
	public const sbyte TILE_SET = -82;

	// Token: 0x040003C1 RID: 961
	public const sbyte COMBINNE = -81;

	// Token: 0x040003C2 RID: 962
	public const sbyte FRIEND = -80;

	// Token: 0x040003C3 RID: 963
	public const sbyte PLAYER_MENU = -79;

	// Token: 0x040003C4 RID: 964
	public const sbyte CHECK_MOVE = -78;

	// Token: 0x040003C5 RID: 965
	public const sbyte SMALLIMAGE_VERSION = -77;

	// Token: 0x040003C6 RID: 966
	public const sbyte ARCHIVEMENT = -76;

	// Token: 0x040003C7 RID: 967
	public const sbyte NPC_BOSS = -75;

	// Token: 0x040003C8 RID: 968
	public const sbyte GET_IMAGE_SOURCE = -74;

	// Token: 0x040003C9 RID: 969
	public const sbyte NPC_ADD_REMOVE = -73;

	// Token: 0x040003CA RID: 970
	public const sbyte CHAT_PLAYER = -72;

	// Token: 0x040003CB RID: 971
	public const sbyte CHAT_THEGIOI_CLIENT = -71;

	// Token: 0x040003CC RID: 972
	public const sbyte BIG_MESSAGE = -70;

	// Token: 0x040003CD RID: 973
	public const sbyte MAXSTAMINA = -69;

	// Token: 0x040003CE RID: 974
	public const sbyte STAMINA = -68;

	// Token: 0x040003CF RID: 975
	public const sbyte REQUEST_ICON = -67;

	// Token: 0x040003D0 RID: 976
	public const sbyte GET_EFFDATA = -66;

	// Token: 0x040003D1 RID: 977
	public const sbyte TELEPORT = -65;

	// Token: 0x040003D2 RID: 978
	public const sbyte UPDATE_BAG = -64;

	// Token: 0x040003D3 RID: 979
	public const sbyte GET_BAG = -63;

	// Token: 0x040003D4 RID: 980
	public const sbyte CLAN_IMAGE = -62;

	// Token: 0x040003D5 RID: 981
	public const sbyte UPDATE_CLANID = -61;

	// Token: 0x040003D6 RID: 982
	public const sbyte SKILL_NOT_FOCUS = -45;

	// Token: 0x040003D7 RID: 983
	public const sbyte SHOP = -44;

	// Token: 0x040003D8 RID: 984
	public const sbyte USE_ITEM = -43;

	// Token: 0x040003D9 RID: 985
	public const sbyte ME_LOAD_POINT = -42;

	// Token: 0x040003DA RID: 986
	public const sbyte UPDATE_CAPTION = -41;

	// Token: 0x040003DB RID: 987
	public const sbyte GET_ITEM = -40;

	// Token: 0x040003DC RID: 988
	public const sbyte FINISH_LOADMAP = -39;

	// Token: 0x040003DD RID: 989
	public const sbyte FINISH_UPDATE = -38;

	// Token: 0x040003DE RID: 990
	public const sbyte BODY = -37;

	// Token: 0x040003DF RID: 991
	public const sbyte BAG = -36;

	// Token: 0x040003E0 RID: 992
	public const sbyte BOX = -35;

	// Token: 0x040003E1 RID: 993
	public const sbyte MAGIC_TREE = -34;

	// Token: 0x040003E2 RID: 994
	public const sbyte MAP_OFFLINE = -33;

	// Token: 0x040003E3 RID: 995
	public const sbyte BACKGROUND_TEMPLATE = -32;

	// Token: 0x040003E4 RID: 996
	public const sbyte ITEM_BACKGROUND = -31;

	// Token: 0x040003E5 RID: 997
	public const sbyte SUB_COMMAND = -30;

	// Token: 0x040003E6 RID: 998
	public const sbyte NOT_LOGIN = -29;

	// Token: 0x040003E7 RID: 999
	public const sbyte NOT_MAP = -28;

	// Token: 0x040003E8 RID: 1000
	public const sbyte GET_SESSION_ID = -27;

	// Token: 0x040003E9 RID: 1001
	public const sbyte DIALOG_MESSAGE = -26;

	// Token: 0x040003EA RID: 1002
	public const sbyte SERVER_MESSAGE = -25;

	// Token: 0x040003EB RID: 1003
	public const sbyte MAP_INFO = -24;

	// Token: 0x040003EC RID: 1004
	public const sbyte MAP_CHANGE = -23;

	// Token: 0x040003ED RID: 1005
	public const sbyte MAP_CLEAR = -22;

	// Token: 0x040003EE RID: 1006
	public const sbyte ITEMMAP_REMOVE = -21;

	// Token: 0x040003EF RID: 1007
	public const sbyte ITEMMAP_MYPICK = -20;

	// Token: 0x040003F0 RID: 1008
	public const sbyte ITEMMAP_PLAYERPICK = -19;

	// Token: 0x040003F1 RID: 1009
	public const sbyte ME_THROW = -18;

	// Token: 0x040003F2 RID: 1010
	public const sbyte ME_DIE = -17;

	// Token: 0x040003F3 RID: 1011
	public const sbyte ME_LIVE = -16;

	// Token: 0x040003F4 RID: 1012
	public const sbyte ME_BACK = -15;

	// Token: 0x040003F5 RID: 1013
	public const sbyte PLAYER_THROW = -14;

	// Token: 0x040003F6 RID: 1014
	public const sbyte NPC_LIVE = -13;

	// Token: 0x040003F7 RID: 1015
	public const sbyte NPC_DIE = -12;

	// Token: 0x040003F8 RID: 1016
	public const sbyte NPC_ATTACK_ME = -11;

	// Token: 0x040003F9 RID: 1017
	public const sbyte NPC_ATTACK_PLAYER = -10;

	// Token: 0x040003FA RID: 1018
	public const sbyte MOB_HP = -9;

	// Token: 0x040003FB RID: 1019
	public const sbyte PLAYER_DIE = -8;

	// Token: 0x040003FC RID: 1020
	public const sbyte PLAYER_MOVE = -7;

	// Token: 0x040003FD RID: 1021
	public const sbyte PLAYER_REMOVE = -6;

	// Token: 0x040003FE RID: 1022
	public const sbyte PLAYER_ADD = -5;

	// Token: 0x040003FF RID: 1023
	public const sbyte PLAYER_ATTACK_N_P = -4;

	// Token: 0x04000400 RID: 1024
	public const sbyte PLAYER_UP_EXP = -3;

	// Token: 0x04000401 RID: 1025
	public const sbyte ME_UP_COIN_LOCK = -2;

	// Token: 0x04000402 RID: 1026
	public const sbyte ME_CHANGE_COIN = -1;

	// Token: 0x04000403 RID: 1027
	public const sbyte ITEM_BUY = 6;

	// Token: 0x04000404 RID: 1028
	public const sbyte ITEM_SALE = 7;

	// Token: 0x04000405 RID: 1029
	public const sbyte UPPEARL_LOCK = 13;

	// Token: 0x04000406 RID: 1030
	public const sbyte UPGRADE = 14;

	// Token: 0x04000407 RID: 1031
	public const sbyte PLEASE_INPUT_PARTY = 16;

	// Token: 0x04000408 RID: 1032
	public const sbyte ACCEPT_PLEASE_PARTY = 17;

	// Token: 0x04000409 RID: 1033
	public const sbyte REQUEST_PLAYERS = 18;

	// Token: 0x0400040A RID: 1034
	public const sbyte UPDATE_ACHIEVEMENT = 19;

	// Token: 0x0400040B RID: 1035
	public const sbyte MOVE_FAST_NPC = 20;

	// Token: 0x0400040C RID: 1036
	public const sbyte ZONE_CHANGE = 21;

	// Token: 0x0400040D RID: 1037
	public const sbyte MENU = 22;

	// Token: 0x0400040E RID: 1038
	public const sbyte OPEN_UI = 23;

	// Token: 0x0400040F RID: 1039
	public const sbyte OPEN_UI_BOX = 24;

	// Token: 0x04000410 RID: 1040
	public const sbyte OPEN_UI_PT = 25;

	// Token: 0x04000411 RID: 1041
	public const sbyte OPEN_UI_SHOP = 26;

	// Token: 0x04000412 RID: 1042
	public const sbyte OPEN_MENU_ID = 27;

	// Token: 0x04000413 RID: 1043
	public const sbyte OPEN_UI_COLLECT = 28;

	// Token: 0x04000414 RID: 1044
	public const sbyte OPEN_UI_ZONE = 29;

	// Token: 0x04000415 RID: 1045
	public const sbyte OPEN_UI_TRADE = 30;

	// Token: 0x04000416 RID: 1046
	public const sbyte OPEN_UI_SAY = 38;

	// Token: 0x04000417 RID: 1047
	public const sbyte OPEN_UI_CONFIRM = 32;

	// Token: 0x04000418 RID: 1048
	public const sbyte OPEN_UI_MENU = 33;

	// Token: 0x04000419 RID: 1049
	public const sbyte SKILL_SELECT = 34;

	// Token: 0x0400041A RID: 1050
	public const sbyte REQUEST_ITEM_INFO = 35;

	// Token: 0x0400041B RID: 1051
	public const sbyte TRADE_INVITE = 36;

	// Token: 0x0400041C RID: 1052
	public const sbyte TRADE_INVITE_ACCEPT = 37;

	// Token: 0x0400041D RID: 1053
	public const sbyte TRADE_LOCK_ITEM = 38;

	// Token: 0x0400041E RID: 1054
	public const sbyte TRADE_ACCEPT = 39;

	// Token: 0x0400041F RID: 1055
	public const sbyte TASK_GET = 40;

	// Token: 0x04000420 RID: 1056
	public const sbyte TASK_NEXT = 41;

	// Token: 0x04000421 RID: 1057
	public const sbyte GAME_INFO = 50;

	// Token: 0x04000422 RID: 1058
	public const sbyte TASK_UPDATE = 43;

	// Token: 0x04000423 RID: 1059
	public const sbyte CHAT_MAP = 44;

	// Token: 0x04000424 RID: 1060
	public const sbyte NPC_MISS = 45;

	// Token: 0x04000425 RID: 1061
	public const sbyte RESET_POINT = 46;

	// Token: 0x04000426 RID: 1062
	public const sbyte ALERT_MESSAGE = 47;

	// Token: 0x04000427 RID: 1063
	public const sbyte AUTO_SERVER = 48;

	// Token: 0x04000428 RID: 1064
	public const sbyte ALERT_SEND_SMS = 49;

	// Token: 0x04000429 RID: 1065
	public const sbyte TRADE_INVITE_CANCEL = 50;

	// Token: 0x0400042A RID: 1066
	public const sbyte BOSS_SKILL = 51;

	// Token: 0x0400042B RID: 1067
	public const sbyte MABU_HOLD = 52;

	// Token: 0x0400042C RID: 1068
	public const sbyte FRIEND_INVITE = 53;

	// Token: 0x0400042D RID: 1069
	public const sbyte PLAYER_ATTACK_NPC = 54;

	// Token: 0x0400042E RID: 1070
	public const sbyte HAVE_ATTACK_PLAYER = 56;

	// Token: 0x0400042F RID: 1071
	public const sbyte OPEN_UI_NEWMENU = 57;

	// Token: 0x04000430 RID: 1072
	public const sbyte MOVE_FAST = 58;

	// Token: 0x04000431 RID: 1073
	public const sbyte TEST_INVITE = 59;

	// Token: 0x04000432 RID: 1074
	public const sbyte ADD_CUU_SAT = 62;

	// Token: 0x04000433 RID: 1075
	public const sbyte ME_CUU_SAT = 63;

	// Token: 0x04000434 RID: 1076
	public const sbyte CLEAR_CUU_SAT = 64;

	// Token: 0x04000435 RID: 1077
	public const sbyte PLAYER_UP_EXPDOWN = 65;

	// Token: 0x04000436 RID: 1078
	public const sbyte ME_DIE_EXP_DOWN = 66;

	// Token: 0x04000437 RID: 1079
	public const sbyte PLAYER_ATTACK_P_N = 67;

	// Token: 0x04000438 RID: 1080
	public const sbyte ITEMMAP_ADD = 68;

	// Token: 0x04000439 RID: 1081
	public const sbyte ITEM_BAG_REFRESH = 69;

	// Token: 0x0400043A RID: 1082
	public const sbyte USE_SKILL_MY_BUFF = 70;

	// Token: 0x0400043B RID: 1083
	public const sbyte NPC_CHANGE = 74;

	// Token: 0x0400043C RID: 1084
	public const sbyte PARTY_INVITE = 75;

	// Token: 0x0400043D RID: 1085
	public const sbyte PARTY_ACCEPT = 76;

	// Token: 0x0400043E RID: 1086
	public const sbyte PARTY_CANCEL = 77;

	// Token: 0x0400043F RID: 1087
	public const sbyte PLAYER_IN_PARTY = 78;

	// Token: 0x04000440 RID: 1088
	public const sbyte PARTY_OUT = 79;

	// Token: 0x04000441 RID: 1089
	public const sbyte FRIEND_ADD = 80;

	// Token: 0x04000442 RID: 1090
	public const sbyte NPC_IS_DISABLE = 81;

	// Token: 0x04000443 RID: 1091
	public const sbyte NPC_IS_MOVE = 82;

	// Token: 0x04000444 RID: 1092
	public const sbyte SUMON_ATTACK = 83;

	// Token: 0x04000445 RID: 1093
	public const sbyte RETURN_POINT_MAP = 84;

	// Token: 0x04000446 RID: 1094
	public const sbyte NPC_IS_FIRE = 85;

	// Token: 0x04000447 RID: 1095
	public const sbyte NPC_IS_ICE = 86;

	// Token: 0x04000448 RID: 1096
	public const sbyte NPC_IS_WIND = 87;

	// Token: 0x04000449 RID: 1097
	public const sbyte OPEN_TEXT_BOX_ID = 88;

	// Token: 0x0400044A RID: 1098
	public const sbyte REQUEST_ITEM_PLAYER = 90;

	// Token: 0x0400044B RID: 1099
	public const sbyte CHAT_PRIVATE = 91;

	// Token: 0x0400044C RID: 1100
	public const sbyte CHAT_THEGIOI_SERVER = 92;

	// Token: 0x0400044D RID: 1101
	public const sbyte CHAT_VIP = 93;

	// Token: 0x0400044E RID: 1102
	public const sbyte SERVER_ALERT = 94;

	// Token: 0x0400044F RID: 1103
	public const sbyte ME_UP_COIN_BAG = 95;

	// Token: 0x04000450 RID: 1104
	public const sbyte GET_TASK_ORDER = 96;

	// Token: 0x04000451 RID: 1105
	public const sbyte GET_TASK_UPDATE = 97;

	// Token: 0x04000452 RID: 1106
	public const sbyte CLEAR_TASK_ORDER = 98;

	// Token: 0x04000453 RID: 1107
	public const sbyte ADD_ITEM_MAP = 99;

	// Token: 0x04000454 RID: 1108
	public const sbyte TRANSPORT = -105;

	// Token: 0x04000455 RID: 1109
	public const sbyte ITEM_TIME = -106;

	// Token: 0x04000456 RID: 1110
	public const sbyte PET_INFO = -107;

	// Token: 0x04000457 RID: 1111
	public const sbyte PET_STATUS = -108;

	// Token: 0x04000458 RID: 1112
	public const sbyte SERVER_DATA = -110;

	// Token: 0x04000459 RID: 1113
	public const sbyte CLIENT_INPUT = -125;

	// Token: 0x0400045A RID: 1114
	public const sbyte HOLD = -124;

	// Token: 0x0400045B RID: 1115
	public const sbyte SHOW_ADS = 121;

	// Token: 0x0400045C RID: 1116
	public const sbyte LOGIN_DE = 122;

	// Token: 0x0400045D RID: 1117
	public const sbyte SET_POS = 123;

	// Token: 0x0400045E RID: 1118
	public const sbyte NPC_CHAT = 124;

	// Token: 0x0400045F RID: 1119
	public const sbyte FUSION = 125;

	// Token: 0x04000460 RID: 1120
	public const sbyte ANDROID_PACK = 126;

	// Token: 0x04000461 RID: 1121
	public const sbyte GET_IMAGE_SOURCE2 = -111;

	// Token: 0x04000462 RID: 1122
	public const sbyte CHAGE_MOD_BODY = -112;

	// Token: 0x04000463 RID: 1123
	public const sbyte CHANGE_ONSKILL = -113;

	// Token: 0x04000464 RID: 1124
	public const sbyte REQUEST_PEAN = -114;

	// Token: 0x04000465 RID: 1125
	public const sbyte POWER_INFO = -115;

	// Token: 0x04000466 RID: 1126
	public const sbyte AUTOPLAY = -116;

	// Token: 0x04000467 RID: 1127
	public const sbyte MABU = -117;

	// Token: 0x04000468 RID: 1128
	public const sbyte THACHDAU = -118;

	// Token: 0x04000469 RID: 1129
	public const sbyte THELUC = -119;

	// Token: 0x0400046A RID: 1130
	public const sbyte UPDATECHAR_MP = -123;

	// Token: 0x0400046B RID: 1131
	public const sbyte REFRESH_ITEM = 100;

	// Token: 0x0400046C RID: 1132
	public const sbyte CHECK_CONTROLLER = -120;

	// Token: 0x0400046D RID: 1133
	public const sbyte CHECK_MAP = -121;

	// Token: 0x0400046E RID: 1134
	public const sbyte BIG_BOSS = 101;

	// Token: 0x0400046F RID: 1135
	public const sbyte BIG_BOSS_2 = 102;

	// Token: 0x04000470 RID: 1136
	public const sbyte DUAHAU = -122;

	// Token: 0x04000471 RID: 1137
	public const sbyte QUAYSO = -126;

	// Token: 0x04000472 RID: 1138
	public const sbyte USER_INFO = 42;

	// Token: 0x04000473 RID: 1139
	public const sbyte OPEN3HOUR = -89;

	// Token: 0x04000474 RID: 1140
	public const sbyte STATUS_PET = 31;

	// Token: 0x04000475 RID: 1141
	public const sbyte SPEACIAL_SKILL = 112;

	// Token: 0x04000476 RID: 1142
	public const sbyte SERVER_EFFECT = 113;
}
