using Cross;
using System;

namespace MuGame
{
	public class GameConstant
	{
		public enum PlatformType
		{
			NULL,
			i360,
			baidu
		}

		public static GameConstant.PlatformType PLAFORM_TYPE = GameConstant.PlatformType.NULL;

		public const uint GEZI = 32u;

		public const uint GEZI_HDT = 5u;

		public const float GEZI_TRANS_UNITYPOS = 1.666f;

		public const float PIXEL_TRANS_UNITYPOS = 53.333f;

		public const int DEF_ATTACK_RANGE = 1;

		public const float OPEN_NPC_DISTANCE = 1f;

		public const uint LINK_ACT_GRID_RANGE = 5u;

		public const uint MOVE_FREQUENCY_MIN_TM = 300u;

		public const string LINK_EFFECT_ID = "linkeff";

		public const string SELECT_EFF_ID = "selecteff";

		public const float CAMERA_Z_MOVE = 0.01f;

		public static float CHAR_OFFSET_Y = 0.1f;

		public static float SHADOW_OFFSET_Y = 0f;

		public static float CAMERA_ROTATION_X = 45f;

		public static float CAMERA_ROTATION_Y = 45f;

		public static float CAMERA_ROTATION_Z = 0f;

		public static float CAMERA_FIELD_VIEW = 15f;

		public static float CAMERA_POSTION_OFFSET_FROM_CHAR_X = -19.3f;

		public static float CAMERA_POSTION_OFFSET_FROM_CHAR_Y = 28.15f;

		public static float CAMERA_POSTION_OFFSET_FROM_CHAR_Z = -20.35f;

		public const float JOYSTICK_ORI_ROTATION = 90f;

		public const float ORI_UINT = 45f;

		public const int SCREEN_DEF_WIDTH = 1280;

		public const int SCREEN_DEF_HEIGHT = 720;

		public const uint SP_MAIN_CHAR = 1u;

		public const uint SP_CHAR = 2u;

		public const uint SP_Monster = 3u;

		public const uint CONN_DEF = 10u;

		public const uint CONN_CONNECTED = 20u;

		public const uint CONN_LOGINED = 30u;

		public const uint CONN_VER_GOT = 40u;

		public const uint CONN_JOIN_WORLD = 50u;

		public const int INFO_LOADED = 0;

		public const int INFO_DEF = 1;

		public const int INFO_LOADING = 2;

		public const int INFO_ERR = -1;

		public const int ORI_S = 1;

		public const int ORI_SE = 2;

		public const int ORI_SW = 3;

		public const int ORI_W = 4;

		public const int ORI_N = 5;

		public const int ORI_NW = 6;

		public const int ORI_NE = 7;

		public const int ORI_E = 8;

		public static Vec2 EFF_DEF_ORI = new Vec2(0f, 1f);

		public const string DEFAULT_AVATAR = "1070";

		public const string DEFAULT_SHADOW = "shadow";

		public const string ANI_MOVE_NORMAL = "a5";

		public const string ANI_IDLE_NORMAL = "idle";

		public const string ANI_SINGLE_H_ATK = "a16";

		public const int ANI_LOOP_FLAG_AUTO = 0;

		public const int ANI_LOOP_FLAG_NOT_LOOP = 1;

		public const int ANI_LOOP_FLAG_LOOP = 2;

		public const int ST_MOVE = 10;

		public const int ST_STAND = 20;

		public const int ST_ATK = 40;

		public const int ST_DIE = 50;

		public const int CTP_BASE = 1;

		public const int CTP_MOVE_POS = 2;

		public const int CTP_STAND = 3;

		public const int CTP_STOP = 4;

		public const int CTP_MOVE_ORI = 5;

		public const int CTP_MOVE_MAP = 6;

		public const int CTP_KILL_MON = 7;

		public const int CTP_MISSION = 8;

		public const int CTP_MISSION_LINE = 9;

		public const int CTP_ENTER_LEVEL = 10;

		public const int CTP_ATTACK = 11;

		public const int CTP_TASK = 12;

		public const uint PAUSE_ON_MAP_CHANGE = 1u;

		public const int E_PATH_NOT_FIND = 1;

		public const int E_NEED_TP = 2;

		public const int E_NEED_PAR = 3;

		public const int POS_NORMAL = 0;

		public const int POS_HIGH = 1;

		public const int POS_HIGHER = 2;

		public const int MAX_BOARD_CHARS = 130;

		public const int CARDID_LENGTH_15 = 15;

		public const int CARDID_LENGTH_18 = 18;

		public const int CARDNAME_LENGTH_5 = 5;

		public const int EQP_POS_HAT = 1;

		public const int EQP_POS_NECK = 2;

		public const int EQP_POS_COAT = 3;

		public const int EQP_POS_PANT = 4;

		public const int EQP_POS_SHOE = 5;

		public const int EQP_POS_WEAPON = 6;

		public const int EQP_POS_HAND = 7;

		public const int EQP_POS_FASHION = 8;

		public const int EQP_POS_RING = 9;

		public const int EQP_POS_RIDE = 10;

		public const int EQP_POS_WING = 11;

		public const int EQP_POS_GUARD = 12;

		public const int EQP_POS_PET = 13;

		public const int EQP_POS_MEDAL = 14;

		public const int EQP_WEAPON_BOW = 1;

		public const int EQP_WEAPON_CROSSBOW = 2;

		public const int EQP_WEAPON_SHIELD = 3;

		public const int EQP_WEAPON_ARROWS = 4;

		public const int EQP_POSUNIQ_BOW = 3;

		public const int EQP_POSUNIQ_CROSIER = 2;

		public const int EQP_POSUNIQ_SHIELD = 1;

		public const int EQP_POSUNIQ_ARROWS = 4;

		public const uint EVT_MouseClick = 0u;

		public const uint EVT_DoubleClick = 1u;

		public const uint EVT_MouseOver = 2u;

		public const uint EVT_MouseOut = 3u;

		public const uint EVT_DragDrop = 4u;

		public const uint EVT_TextLink = 5u;

		public const uint EVT_SelectChange = 6u;

		public const uint EVT_PosChange = 7u;

		public const uint ACT_Close = 100u;

		public const uint ACT_Open = 101u;

		public const string WORLD_BG_1 = "world_bg_1";

		public const string WORLD_BG_2 = "world_bg_2";

		public const string SCRIPT_BG_1 = "script_bg_1";

		public const string SCRIPT_BG_2 = "script_bg_2";

		public const string MOUSE_CLICK_1 = "mouse_click_1";

		public const string UI_OPEAN_1 = "ui_opean_1";

		public const string UI_CLOSE_1 = "ui_close_1";

		public const string TASK_RECEIVE_1 = "task_receive_1";

		public const string TASK_FINISH_1 = "task_finish_1";

		public const string TO_BAG_1 = "to_bag_1";

		public const string MAKE_BAG_1 = "make_bag_1";

		public const string REPAIR_EQ_1 = "repair_eq_1";

		public const string PAY_GOLD_1 = "pay_gold_1";

		public const string CHARACTER_UP_1 = "character_up_1";

		public const string ARENA_WAITING = "arena_waiting";

		public const string FORGING_SUCCESS_1 = "forging_success_1";

		public const string FORGING_FAIL_1 = "forging_fail_1";

		public const string FORGING_ADD_1 = "forging_add_1";

		public const string FEINED_LOGIN_1 = "frined_login_1";

		public const string PRIVATE_TALK_1 = "private_talk_1";

		public const string MAIL_1 = "mail_1";

		public const string SALE_1 = "sale_1";

		public const string CONNECT_MUSIC = "connect_music";

		public const string SKILL_1 = "skill_1";

		public const string SKILL_2 = "skill_2";

		public const string TAKE_OFF = "take_off";

		public const string PUT_ON = "put_on";

		public const int CAST_SKILL_TYPE_NOTAR = 0;

		public const int CAST_SKILL_TYPE_TARGET = 1;

		public const int CAST_SKILL_TYPE_POINT = 2;

		public const string SUPERTXT_PROFIX = "<txt ";

		public const string SUPERTXT_END = "/>";

		public const string SUPERTXT_BR = "<br/>";

		public const uint ICMF_NO_NOTIFY = 1u;

		public const uint ICMF_COL_ITEM = 2u;

		public const uint ICMF_DECOMP_EQP = 3u;

		public const uint ICMF_DELAY_EXPIRE = 4u;

		public const uint ICMF_LVL_PRIZE = 5u;

		public const uint ICMF_OPEN_PKG = 6u;

		public const uint ICMF_FGUESS_RETURN = 7u;

		public const uint ICMF_LVL_TM_COST = 8u;

		public const uint ICMF_PICK_ITEM = 9u;

		public const uint ICMF_RMV_EQP = 100u;

		public const uint ICMF_NEW_ITEM = 101u;

		public const int PLAT_LY = 1;

		public const int PLAT_QQ = 2;

		public const int PLAT_YN = 3;

		public const int PLAT_YY = 4;

		public const int PLAT_TG = 5;

		public const int PLAT_360 = 6;

		public const int PLAT_BM = 7;

		public const int PLAT_P4399 = 10;

		public const int PLAT_NM = 11;

		public const int Plat_TW = 9;

		public const int PLATID_P51 = 12;

		public const int PLATID_PPTV = 14;

		public const string MULTILVL_CHALLENGE = "multilvl_challenge";

		public const string LUCKYDRAW_RADLUCKY = "luckydraw_radlucky";

		public const string LUCKYDRAW_RADCHARGE = "luckydraw_radcharge";

		public const string LUCKYDRAW_RADCONSUME = "luckydraw_radconsume";

		public const int TRANSCRIPTTILESUM = 5;

		public const int LAST_TM = 10000;

		public const int LAST_TM_V = 1000;

		public const int INTERVAL = 300000;

		public const int B1 = 1;

		public const int B2 = 2;

		public const int B3 = 3;

		public const int B4 = 4;

		public const int B5 = 5;

		public const int B6 = 6;

		public const int B7 = 7;

		public const int B8 = 8;

		public const int PAGE_UP_X = 1;

		public const int PAGE_DOWN_X = 2;

		public const uint HUMDR_ACTIVITY = 0u;

		public const uint OTHER_ACTIVITY = 1u;

		public const int TYPE_FRIEND = 1;

		public const int TYPE_ENEMY = 2;

		public const int TYPE_BLACK = 3;

		public const int MISSION_LINE_MAIN = 1;

		public const uint DOMISSION_STATE_NONE = 0u;

		public const uint DOMISSION_STATE_DOACCEPT = 1u;

		public const uint DOMISSION_STATE_ACCEPTING = 2u;

		public const uint DOMISSION_STATE_ACCEPTED = 3u;

		public const uint DOMISSION_STATE_DOING = 4u;

		public const uint DOMISSION_STATE_WAITFIN = 5u;

		public const uint DOMISSION_STATE_COMMITING = 6u;

		public const uint DOMISSION_STATE_COMMITED = 7u;

		public const uint DOMISSION_STATE_COMPLETE = 8u;

		public const int ERR_CONF = -1;

		public const int ERR_MIS_CONF = -2;

		public const int ERR_NPC_CONF = -3;

		public const int ERR_MON_CONF = -4;

		public const int ERR_MAP_CONF = -5;

		public const int ERR_INIT = -100;

		public const int ERR_MIS_DATA = -1000;

		public const int ERR_ENTER_LEVEL = -1001;

		public const int ERR_IN_LEVEL = -1002;

		public const int ERR_MAP_PATH = -1101;

		public const int ERR_PATH = -1102;

		public const int FOUR = 4;

		public const int FIVE = 5;

		public const int LOADING_MAP_GRD = 10;

		public const int LOADING_MAP = 20;

		public const int TAKEPOS_NONE = 0;

		public const int TAKEPOS_SINGLE = 65536;

		public const int TAKEPOS_DOUBLE = 131072;

		public const int TAKEPOS_WING = 262144;

		public const int TAKEPOS_MOUNT = 524288;

		public const int TAKEPOS_SUBTP_11 = 720896;

		public const int TAKEPOS_SUBTP_12 = 786432;

		public const int TAKEPOS_SUBTP_13 = 851968;
	}
}
