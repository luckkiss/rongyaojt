using System;

namespace MuGame
{
	public class UI_EVENT
	{
		public const uint UI_OPEN = 4001u;

		public const uint UI_CLOSE = 4002u;

		public const uint UI_OPEN_SWITCH = 4003u;

		public const uint UI_DIPOSE_CREAT = 4004u;

		public const uint UI_SELECT_CHAR = 4005u;

		public const uint UI_ON_SELECT_SID = 4015u;

		public const uint UI_ON_TRY_ENTER_GAME = 4020u;

		public const uint UI_ACT_CREATE_CHAR = 4031u;

		public const uint UI_ACT_DELETE_CHAR = 4032u;

		public const uint UI_ACT_SELECT_CHAR = 4033u;

		public const uint UI_ACT_ENTER_GAME = 4034u;

		public const uint UI_ACT_PLAY_MOV = 4040u;

		public const uint UI_ACT_PLAY_STOP = 4041u;

		public const uint UI_ACT_MISSION = 4042u;

		public const uint UI_SUBMIT_MISSION = 4043u;

		public const uint UI_OPNE_MISSION_GUIDE = 4044u;

		public const uint UI_HERO_ATTACK = 4045u;

		public const uint UI_HERO_SKILL = 4046u;

		public const uint UI_HERO_JUMP = 4047u;

		public const uint UI_DELETE_CHARUI = 4048u;

		public const uint UI_MAIN_TEXT = 4049u;

		public const uint UI_SHOP_GET_HOT_ITM = 4500u;

		public const uint UI_SHOP_CHANGESHOP = 4501u;

		public const uint UI_SHOP_SHOW = 4502u;

		public const uint UI_SHOP_BUY = 4503u;

		public const uint UI_SHOP_CHARGE = 4504u;

		public const uint UI_CHAT_TALK = 4505u;

		public const uint UI_OPEN_ROLE = 4600u;

		public const uint UI_OPEN_PACKAGE = 4601u;

		public const uint UI_OPEN_SHOP = 4602u;

		public const uint UI_OPEN_PK = 4603u;

		public const uint UI_OPEN_SMITHY = 4603u;

		public const uint UI_OPEN_CHAT = 4604u;

		public const uint UI_OPEN_SET = 4605u;

		public const uint UI_OPEN_WORLDMAP = 4606u;

		public const uint UI_OPEN_NPCFUN = 4607u;

		public const uint UI_OPEN_NPCMIS = 4608u;

		public const uint UI_OPEN_LEVELHALL = 4609u;

		public const uint UI_OPEN_CLAN = 4610u;

		public const uint UI_OPEN_DONATE = 4611u;

		public const uint UI_OPEN_MARKET = 4610u;

		public const uint UI_OPEN_USEITEM_PROMPT = 4611u;

		public const uint UI_SETCHARACTER_VALUE = 5001u;

		public const uint UI_SETCHARACTER_CHAGEEQP = 5002u;

		public const uint UI_SETCHARACTER_RMVEQP = 5003u;

		public const uint UI_SETCHARACTER_SETW = 5004u;

		public const uint UI_SETCHARACTER_SETFW = 5005u;

		public const uint UI_SETCHARACTER_CLEARW = 5005u;

		public const uint UI_SETCHARACTER_SETWINFO = 5007u;

		public const uint UI_SETCHARACTER_SETWEQUIP = 5008u;

		public const uint UI_SETCHARACTER_CHGEQP = 5009u;

		public const uint UI_SET_EPHF = 5010u;

		public const uint UI_SET_OP_CK = 5010u;

		public const uint UI_SETCK = 5011u;

		public const uint UI_SET_OP_CB = 5012u;

		public const uint UI_SET_RE_CB = 5013u;

		public const uint UI_SET_NO_CB = 5014u;

		public const uint UI_SET_NCB = 5015u;

		public const uint UI_INIT_CF = 5016u;

		public const uint UI_ROM_3DEQP = 5017u;

		public const uint UI_SET_P3DSH = 5018u;

		public const uint UI_REF_SKILLLIST = 5019u;

		public const uint UI_SET_RADIO = 5020u;

		public const uint UI_SET_ONCLKIDX = 5021u;

		public const uint UI_SET_TILENAME = 5022u;

		public const uint UI_ONCHAGE_EQP = 5023u;

		public const uint UI_SET_LVL = 5024u;

		public const uint UI_SET_PROFESS = 5025u;

		public const uint UI_SET_DINFO_CHANGE = 5026u;

		public const uint UI_REFESH_EQP = 5027u;

		public const uint UI_CHAGE_SHOP_SS = 5028u;

		public const uint UI_SET_NAME = 5029u;

		public const uint UI_SET_INFO = 5030u;

		public const uint SET_SHOP_ITEM = 5031u;

		public const uint SET_SHOP_RADIO = 5032u;

		public const uint PREPARE_SHOP = 5033u;

		public const uint UI_LEVELHALLINFO = 5034u;

		public const uint UI_LEVELHALL = 5035u;

		public const uint TILE_TRANSCRIPT = 5036u;

		public const uint TILE_TRANSCRIPTINFO = 5037u;

		public const uint INITTRANSCRIPTTILES = 5038u;

		public const uint LGUI_LEVEL = 5039u;

		public const uint LGGD_LEVEL = 5040u;

		public const uint TILE_TAGRADIOBTN = 5036u;

		public const uint TEST_BUTTON1 = 7000u;

		public const uint TEST_BUTTON2 = 7001u;

		public const uint SELECT_CHAR_INIT = 7002u;

		public const uint SELECT_CHAR_ENTER_GAME = 7003u;

		public const uint CHAR_SELECT = 7004u;

		public const uint SET_TILE_INFO = 7005u;

		public const uint CLEAR_TILE_INFO = 7006u;

		public const uint MODIFY_TILE_INFO = 7007u;

		public const uint ON_CHAR_TILE_INIT = 7008u;

		public const uint ON_TILES_INIT = 7010u;

		public const uint ON_TILES_ADDED = 7011u;

		public const uint ON_LIST_ADD = 7012u;

		public const uint ON_LIST_RMV = 7013u;

		public const uint ON_LIST_MOD = 7014u;

		public const uint ON_LIST_SET_DATA = 7015u;

		public const uint ITEM_SELECT = 7020u;

		public const uint ITEM_GET = 7021u;

		public const uint CREAT_PROMPT = 7022u;

		public const uint ON_REFRESH_INFO = 7030u;

		public const uint ON_BUTTON_CHANGE = 7031u;

		public const uint ON_RADIO_SELECT = 7032u;

		public const uint CHAINFO_INIT = 7033u;

		public const uint CHAINFO_TILE_INIT = 7033u;

		public const uint TILE_SET_SHOW_DATA = 7034u;

		public const uint ON_NPCDIALOG_FIN = 7035u;

		public const uint ON_NPCDIALOG_ACP = 7036u;

		public const uint ON_NPCDIALOG_DOUBLE_FIN = 7037u;

		public const uint ON_NPCDIALOG_CLKTILE = 7038u;

		public const uint ON_NPCDIALOG_NEXT = 7039u;

		public const uint TESTBASEOK = 8000u;

		public const uint DIANJIBASE = 8001u;

		public const uint ON_LOAD_ITEMS = 9001u;

		public const uint ON_BUY_ITEMS = 9002u;

		public const uint ON_SELL_ITEMS = 9003u;

		public const uint ON_USE_ITEMS = 9004u;

		public const uint ON_MONEY_CHANGE = 9005u;

		public const uint ON_LV_CHANGE = 9006u;

		public const uint ON_VIP_CHANGE = 9007u;

		public const uint ON_EXP_CHANGE = 9008u;

		public const uint ON_LOAD_GIFT_SHOP = 9009u;

		public const uint ON_CHANGE_ITEM = 9010u;

		public const uint ON_LOAD_DRESS = 10001u;

		public const uint ON_DRESS_UP = 10002u;

		public const uint ON_FINISH_TASK = 10003u;

		public const uint ON_ACTIVE = 10004u;

		public const uint ON_UPGRADE = 10005u;

		public const uint ON_REFESH_PROPERTY = 10006u;

		public const uint ON_LOAD_WEAPON = 12001u;

		public const uint ON_CAILIAOJB_WEAPON = 12002u;

		public const uint ON_CAILIAOYB_WEAPON = 12003u;

		public const uint ON_WPUPGRADE_WEAPON = 12004u;

		public const uint ON_LOAD_SIGN = 13001u;

		public const uint ON_SIGN = 13002u;

		public const uint ON_FILL_SIGN = 13003u;

		public const uint GO_PUBLISH = 14001u;

		public const uint ON_LOAD_MAIL = 15001u;

		public const uint ON_NEW_MAIL = 15002u;

		public const uint ON_SEND_MAIL = 15003u;

		public const uint ON_DELETE_MAIL = 15004u;

		public const uint ON_TAELS_INIT = 16001u;

		public const uint ON_TAELS_CRIT = 16002u;

		public const uint ON_GET_RANKING = 18001u;

		public const uint ON_GETLOTTERYDRAWINFO = 19001u;

		public const uint ON_STOPLOTTERYAWARD = 1902u;

		public const uint ON_FUBENENTER = 20001u;

		public const uint ON_FUBENEXIT = 20002u;

		public const uint ON_OPENFRIENDPANEL = 17001u;
	}
}
