using System;

namespace MuGame
{
	public class GAME_EVENT
	{
		public const uint LOAD_DEF_RES = 3001u;

		public const uint CONN_SET = 3011u;

		public const uint CONN_ED = 3012u;

		public const uint CONN_ERR = 3013u;

		public const uint CONN_VER = 3014u;

		public const uint CONN_VERX = 3020u;

		public const uint CONN_CLOSE = 3015u;

		public const uint CONN_FAILE = 3016u;

		public const uint GAME_LOADING = 3050u;

		public const uint GAME_INIT_START = 3060u;

		public const uint ON_LOGIN = 3021u;

		public const uint ON_LOAD_MIN = 3022u;

		public const uint ON_LOAD_BEFORE_GAME = 3023u;

		public const uint ON_LOAD_MAP = 3024u;

		public const uint ON_SCENE_INIT_FIN = 3025u;

		public const uint ON_SCENE_MAP_CREATE = 3026u;

		public const uint ON_ENTER_GAME = 3034u;

		public const uint ON_MAP_CHANGE = 3035u;

		public const uint DELETE_RETURE = 2004u;

		public const uint S2C_CREAT_CHAR = 2005u;

		public const uint GET_SKILL_LGUIMAIN = 2010u;

		public const uint LEARN_SKILL_LGUIMAIN = 2011u;

		public const uint GET_SKILL_LGUICHAR = 2012u;

		public const uint LEARN_SKILL_LGUISYSTEM = 2013u;

		public const uint SETUPSKILL_LGSKILL = 2014u;

		public const uint ON_CREATE_CHAR = 3031u;

		public const uint ON_DELETE_CHAR = 3032u;

		public const uint ON_SELECT_CHAR = 3033u;

		public const uint ON_MAP_CLK = 3035u;

		public const uint ITEM_ADD = 3036u;

		public const uint ITEM_MOD = 3037u;

		public const uint ITEM_RMV = 3038u;

		public const uint ITEM_INFO_INIT = 3039u;

		public const uint ITEM_USE = 3040u;

		public const uint ITEM_PICK = 3041u;

		public const uint ITEM_GIVE_UP = 3042u;

		public const uint ITEM_BUY = 3043u;

		public const uint ITEM_SELL = 3044u;

		public const uint MIS_ACCEPT = 3045u;

		public const uint MIS_COMMIT = 3046u;

		public const uint MIS_FINISH = 3047u;

		public const uint MIS_ABORD = 3048u;

		public const uint MIS_TRANS = 3049u;

		public const uint MIS_AUTO = 3050u;

		public const uint MIS_DATA_CHANGE = 3051u;

		public const uint UI_SHOP_CHOOSE = 3061u;

		public const uint UI_SHOP_PREPARE = 3062u;

		public const uint UI_SHOP_ACHIEVE = 3063u;

		public const uint UI_CHAT_SEND = 3066u;

		public const uint UI_CHAT_RES_SPEAKER = 3067u;

		public const uint UI_CHAT_RES_ME = 3068u;

		public const uint UI_CHAT_GET_OTHER = 3069u;

		public const uint LG_MEDIA_PLAY = 3301u;

		public const uint LGGD_ACCEPT_MIS_RES = 3302u;

		public const uint LGGD_COMMIT_MIS_RES = 3303u;

		public const uint LGGD_SET_MISSION = 3304u;

		public const uint LGGD_CAN_COMMIT = 3305u;

		public const uint TYPE_PKG_OFFSET = 2000u;

		public const uint SVR_CONF_OK = 2100u;

		public const uint CONF_MAPS = 2101u;

		public const uint CONF_MAP = 2102u;

		public const uint CONF_MISSION = 2103u;

		public const uint CONF_MONSTER = 2104u;

		public const uint MAP_CHANGE = 2160u;

		public const uint MAP_INIT = 2161u;

		public const uint MAP_SIZE_CHANGE = 2162u;

		public const uint MAP_LINK_ADD = 2163u;

		public const uint MAP_ADD_SHOW_EFF = 2164u;

		public const uint MAP_LOAD_READY = 2165u;

		public const uint MAP_ADD_FLY_EFF = 2166u;

		public const uint CAMERA_INIT = 2170u;

		public const uint CAMERA_BIND_CHAR = 2171u;

		public const uint SCENE_CREATE_CAMERA = 2180u;

		public const uint SCENE_CREATE_MAP = 2181u;

		public const uint SCENE_CREATE_NPC = 2182u;

		public const uint SCENE_CREATE_MAIN_CHAR = 2183u;

		public const uint SCENE_CREATE_MONSTER = 2184u;

		public const uint SCENE_CREATE_OTHER_CHAR = 2185u;

		public const uint SCENE_CREATE_HERO = 2186u;

		public const uint SPRITE_SET_DATA = 2100u;

		public const uint SPRITE_SET_VISIBLE = 2200u;

		public const uint SPRITE_ORI = 2201u;

		public const uint SPRITE_MOVE = 2202u;

		public const uint SPRITE_DISPOSE = 2205u;

		public const uint SPRITE_SET_Z = 2204u;

		public const uint SPRITE_SET_XY = 2206u;

		public const uint SPRITE_SIZE_CHANGE = 2207u;

		public const uint SPRITE_MAP_CREATE = 2208u;

		public const uint SPRITE_CAMERA_LOOK_AT = 2209u;

		public const uint SPRITE_ADD_SHOW_EFF = 2210u;

		public const uint SPRITE_ONHURT_SHOW = 2220u;

		public const uint SPRITE_MAINPLAYER_MOVE = 2221u;

		public const uint SPRITE_GR_CAMERA_MOVE = 2222u;

		public const uint SPRITE_OBJ_MASK = 2226u;

		public const uint SPRITE_ON_CLICK = 2280u;

		public const uint SPRITE_DIE = 2285u;

		public const uint SPRITE_OP_REACHED = 2300u;

		public const uint SPRITE_OP_REACHED_INTER = 2301u;

		public const uint S2C_GET_DBMKT_ITM = 5004u;

		public const uint MODE_HEXP = 5005u;

		public const uint MODE_CLANG = 5006u;

		public const uint SELF_INFO_CHANGE = 5007u;

		public const uint MODE_BATPT = 5008u;

		public const uint MODE_NOBPT = 5009u;

		public const uint MODE_CARRLVLCHANGE = 5010u;

		public const uint PRIZELVL = 5011u;

		public const uint MODE_SOULPT = 5012u;

		public const uint MODE_SHOPPT = 5013u;

		public const uint MODE_LOTEXPT = 5014u;

		public const uint MODE_TCYB_LOTT_COST = 5015u;

		public const uint MODE_TCYB_LOTT = 5016u;

		public const uint REFRESHLOTCNT = 5017u;

		public const uint REFRESHLVLSHARE = 5018u;

		public const uint ON_LVL_UP = 5019u;

		public const uint REFRESH_GROW_PACK = 5020u;

		public const uint PLAYER_INFO_CHANGED = 5021u;

		public const uint R_PLAYER_INFO_CHANGED = 5022u;

		public const uint MODIFY_TEAMMATE_DATA = 5023u;

		public const uint MODE_ATTPT = 5024u;

		public const uint ONADDPOINT = 5025u;

		public const uint PASSWORD_SAFEDATARES = 5026u;

		public const uint ONUPGRADE_NOBRES = 5027u;

		public const uint SET_MERGE_INFO = 5028u;

		public const uint ROLL_PT_BACK = 5029u;

		public const uint ON_RESET_LVL = 5030u;

		public const uint UPGRADE_RIDE_RES = 5031u;

		public const uint RIDE_CHANGE = 5032u;

		public const uint UPDATE_PLAYER_NAME = 5033u;

		public const uint ON_INVEST_BACK = 5034u;

		public const uint GET_OI_AWDS = 5035u;

		public const uint PKGS_ITEM_BACK = 5036u;

		public const uint RIDE_QUAL_ATT_ACTIVE_RES = 5037u;

		public const uint SELECT_RIDE_SKILL_RES = 5038u;
	}
}
