using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class InterfaceMgr : GameEventDispatcher
	{
		public delegate object[] objDelegate(string commandid, params object[] args);

		public delegate object[] objDelegate1(string commandid, string path, params object[] args);

		public bool worldmap;

		public static Action<string, object> handleOpenByLua;

		public static InterfaceMgr.objDelegate doCommandByLua_discard;

		public static InterfaceMgr.objDelegate1 doCommandByLua;

		public static string A3_RUNESTONE = "a3_runestone";

		public static string A3_RUNESTONETIP = "a3_runestonetip";

		public static string RETURN_BT = "returnbt";

		public static string A3_BLESSING = "a3_blessing";

		public static string A3_CHAPTERHINT = "a3_chapter_hint";

		public static string A3_WANTLVUP = "a3_wantlvup";

		public static string A3_DOING = "a3_doing";

		public static string A3_SUMMON = "a3_summon";

		public static string A3_AUCTION = "a3_auction";

		public static string A3_TARGETINFO = "a3_targetinfo";

		public static string A3_INSIDEUI_FB = "a3_insideui_fb";

		public static string A3_FB_FINISH = "a3_fb_finish";

		public static string A3_SPEEDTEAM = "a3_SpeedTeam";

		public static string A3_BUFF = "a3_buff";

		public static string A3_ACTIVE = "a3_active";

		public static string A3_MINIMAP = "a3_minimap";

		public static string A3_LITEMINIMAP = "a3_liteMinimap";

		public static string A3_HEROHEAD = "a3_herohead";

		public static string A3_EXPBAR = "a3_expbar";

		public static string A3_FUNCTIONBAR = "a3_functionbar";

		public static string A3_BAG = "a3_bag";

		public static string A3_ACTIVE_GODLIGHT = "a3_active_godlight";

		public static string A3_WAREHOUSE = "a3_warehouse";

		public static string A3_EQUIPSELL = "a3_equipsell";

		public static string A3_EQUIPTIP = "a3_equiptip";

		public static string A3_EQUIPUP = "a3_equipup";

		public static string A3_ITEMTIP = "a3_itemtip";

		public static string A3_DYETIP = "a3_dyetip";

		public static string A3TIPS_SUMMON = "a3tips_summon";

		public static string A3_LVUP = "a3_lvup";

		public static string A3_LOWBLOOD = "a3_lowblood";

		public static string A3_TRRIGERDIALOG = "a3_trrigerDialog";

		public static string A3_MAPNAME = "a3_mapname";

		public static string A3_FUNCOPEN = "a3_funcopen";

		public static string A3_SILLOPEN = "a3_skillopen";

		public static string A3_RUNEOPEN = "a3_runeopen";

		public static string A3_ROLE = "a3_role";

		public static string A3_EQUIP = "a3_equip";

		public static string A3_PET_DESC = "a3_pet_desc";

		public static string A3_EXCHANGE = "a3_exchange";

		public static string SHOP_A3 = "shop_a3";

		public static string SKILL_A3 = "skill_a3";

		public static string A3_RANK = "a3_rank";

		public static string A3_CREATECHA = "a3_createcha";

		public static string A3_SELECTCHA = "a3_selectcha";

		public static string A3_SIGN = "a3_sign";

		public static string A3_GIFTCARD = "a3_giftCard";

		public static string A3_AUTOPLAY = "a3_autoplay";

		public static string A3_AUTOPLAY2 = "a3_autoplay2";

		public static string A3_AUTOPLAY_EQP = "a3_autoplay_eqp";

		public static string A3_AUTOPLAY_SKILL = "a3_autoplay_skill";

		public static string A3_AUTOPLAY_PICK = "a3_autoplay_pick";

		public static string A3_RESETLVL = "a3_resetlvl";

		public static string A3_GETGOLDWAY = "a3_getGoldWay";

		public static string A3_RESETLVLSUCCESS = "a3_resetLvLSuccess";

		public static string A3_STORE = "a3_store";

		public static string A3_RELIVE = "a3_relive";

		public static string A3_MAIL = "a3_mail";

		public static string A3_LOTTERY = "a3_lottery";

		public static string A3_ENTERLOTTERY = "a3_enterlottery";

		public static string TARGET_HEAD = "tragethead";

		public static string A3_ITEMLACK = "a3_itemLack";

		public static string A3_VIP = "a3_vip";

		public static string A3_RECHARGE = "a3_Recharge";

		public static string A3_YILING = "a3_yiling";

		public static string A3_WASHREDNAME = "a3_washredname";

		public static string WORLD_MAP = "worldmap";

		public static string WORLD_MAP_SUB = "worldmapsubwin";

		public static string A3_TASK = "a3_task";

		public static string A3_SYSTEM_SETTING = "a3_systemSetting";

		public static string A3_CURRENTTEAMINFO = "a3_currentTeamInfo";

		public static string A3_HUDUN = "a3_hudun";

		public static string NEWBIE_LINE = "teachline";

		public static string A3_YGYIWU = "a3_ygyiwu";

		public static string CD = "cd";

		public static string LOGIN = "login";

		public static string SERVE_CHOOSE = "choose_server";

		public static string A3_CHATROOM = "a3_chatroom";

		public static string A3_FINDBESTO = "A3_FindBesto";

		public static string A3_PKMODEL = "a3_pkmodel";

		public static string A3_ACHIEVEMENT = "a3_achievement";

		public static string A3_SHEJIAO = "a3_shejiao";

		public static string A3_BEREQUESTFRIEND = "a3_beRequestFriend";

		public static string NPC_TALK = "npctalk";

		public static string A3_AWARDCENTER = "a3_awardCenter";

		public static string A3_COUNTERPART = "a3_counterpart";

		public static string A3_NPC_SHOP = "a3_npc_shop";

		public static string A3_FIRESTRECHARGEAWARD = "a3_firstRechargeAward";

		public static string A3_TEAMAPPLYPANEL = "a3_teamApplyPanel";

		public static string A3_TEAMINVITEDPANEL = "a3_teamInvitedPanel";

		public static string A3_TEAMPANEL = "a3_teamPanel";

		public static string A3_TEAMMEMBERLIST = "a3_teamMemberList";

		public static string A3_LEGION_DART = "a3_legion_dart";

		public static string A3_SLAY_DRAGON = "A3_SlayDragon";

		public static string NPC_TASK_TALK = "npctasktalk";

		public static string A3_INTERACTOTHERUI = "a3_interactOtherUI";

		public static string A3_EVERYDAYLOGIN = "a3_everydayLogin";

		public static string A3_ACTIVEDEGREE = "a3_activeDegree";

		public static string A3_EQPINHERIT = "a3_eqpInherit";

		public static string A3_WIBG_SKIN = "a3_wing_skin";

		public static string A3_PET_SKIN = "a3_pet_skin";

		public static string A3_NEW_PET = "a3_new_pet";

		public static string A3_PET_RENEW = "a3_pet_renew";

		public static string CONFIRM_TEXT = "confirmtext";

		public static string A3_SMITHY = "A3_Smithy";

		public static string A3_MINITIP = "a3_miniTip";

		public static string TRANSMIT_PANEL = "TransmitPanel";

		public static string BEGIN_LOADING = "beginloading";

		public static string MAP_LOADING = "maploading";

		public static string DISCONECT = "disconect";

		public static string LOADING_CLOUD = "loading_cloud";

		public static string FLYTXT = "flytxt";

		public static string FIGHTINGTXT = "fightingup";

		public static string PLAYER_INFO = "playerinfo";

		public static string WAIT_LOADING = "wait_loading";

		public static string SDK_LOADING = "sdkloading";

		public static string COMBO_TEXT = "combo_txt";

		public static string MONSTER_DICT = "monster_direction";

		public static string PK_NOTIFY = "pk_notify";

		public static string SKILL_TEST = "skillbartest";

		public static string CD_TIME = "cdtime";

		public static string A3_QHMASTER = "a3_QHmaster";

		public static string A3_RANKING = "a3_ranking";

		public static string A3_SUMMONINFO = "a3_summoninfo";

		public static string A3_WINGINFO = "a3_Winginfo";

		public static string FB_3D = "fb_3d";

		public static string HERO_3D = "hero3d";

		public static string FUNCTION_BAR = "functionbar";

		public static string JOYSTICK = "joystick";

		public static string SKILL_BAR = "skillbar";

		public static string A3_BESTRONGER = "A3_BeStronger";

		public static string CREATE_CHAR = "creatchar";

		public static string SELECT_CHAR = "selchar";

		public static string EXP_BAR = "expbar";

		public static string DEBUG = "debug";

		public static string ACTIVE = "active";

		public static string FB_WIN = "fb_win";

		public static string FB_LOSE = "fb_lose";

		public static string UPLEVEL = "uplv";

		public static string GETTING = "getthing";

		public static string BAG = "bag";

		public static string SHOP = "shop";

		public static string EQUIP = "equip";

		public static string HEROHEAD = "herohead";

		public static string MINIMAP = "minimap";

		public static string DRESS = "dress";

		public static string DRESSUPGRADE = "dressupgrade";

		public static string WEAPONUPGRADE = "weapon_upgrade";

		public static string WEAPON = "weapon";

		public static string WEAPONLUCKYDRAW = "weapon_luckydraw";

		public static string TASK = "task";

		public static string HERO = "hero";

		public static string STRONG = "strong";

		public static string STORY_DIALOG = "storydialog";

		public static string PLOT_LINKUI = "plot_linkui";

		public static string FB = "fb_map_main";

		public static string FB_MAIN = "fb_main";

		public static string FB_INFO = "fb_info";

		public static string FB_ENERGY = "fb_energy_new";

		public static string ACHIEVE = "achieve";

		public static string SKILL = "skill";

		public static string MOUNT = "mount";

		public static string PAIHANG = "paihang";

		public static string FAMILY = "family";

		public static string FAMILY_CHECKS = "family_checks";

		public static string FAMILY_CREATE = "family_create";

		public static string FAMILY_DONATION = "family_contribution";

		public static string FAMILY_ICONTAGS = "family_icontags";

		public static string FAMILY_JOBAPPOINT = "family_jobappoint";

		public static string FAMILY_SKILL = "family_skill";

		public static string FAMILY_SETTING = "family_setting";

		public static string FAMILY_NOTICE = "family_notice";

		public static string FAMILY_TREASURE = "family_treasure";

		public static string ATTR = "heroattr";

		public static string VIP = "vip";

		public static string VIP_UP = "vip_ani";

		public static string SIGN = "sign";

		public static string RECHARGE = "recharge";

		public static string FRIEND = "friend";

		public static string FRIEND_SEARCH = "friend_search";

		public static string WIPE_OUT = "fb_wipeout";

		public static string E_MAIL = "my_mail";

		public static string STICAL = "stical";

		public static string BUY_SIGN = "buy_sign";

		public static string ARENA = "arena";

		public static string ARENA_BUY = "arena_buy";

		public static string ARENA_REPORT = "arena_report";

		public static string ARENA_REWARDS = "arena_rewards";

		public static string ARENA_SHOP = "arena_shop";

		public static string ARENA_ACCOUNT = "arena_account";

		public static string FIRST_RECHANGE = "first_rechange";

		public static string TAELS = "taels";

		public static string SETTINGS = "settings";

		public static string RELIVE = "relive";

		public static string FACTION = "faction";

		public static string MAILPAPER = "mail_paper";

		public static string BROADCASTING = "broadcasting";

		public static string RANKING = "ranking";

		public static string LOTTERY = "lottery";

		public static string LOTTERY_DRAW = "lottery_draw";

		public static string HERO_NOTICE = "hero_notice";

		public static string OFFLINEEXP = "off_line_exp";

		public static string GETGIFT = "getgift";

		public static string NEEDGET = "needget";

		public static string A3_LHLIANJIE = "a3_LHlianjie";

		public static string A3_ELITEMON = "A3_EliteMonster";

		public static string A3_QUICKOP = "A3_QuickOp";

		public static string A3_PKMAPUI = "a3_pkMapUI";

		public static string A3_ATTCHANGE = "a3_attChange";

		public static string A3_GETJEWELRYWAY = "a3_getJewelryWay";

		public static string A3_BAOTUUI = "a3_baotuUI";

		public static string A3_TASKOPT = "A3_TaskOpt";

		public static string FLOAT_ARENA = "floatui_arena_hp";

		public static string CAMPAIGN = "campaign";

		public static string MJJD = "mjjd";

		public static string MJJD_WIN = "mhjd_win";

		public static string MJJD_FLOAT = "float_mjjd";

		public static string GETNEW = "getnew";

		public static string DIALOG = "dialog";

		public RectTransform cemaraRectTransform;

		private GameObject mainLoadingBg;

		private Vector3 vecHide = new Vector3(999999f, 0f, 0f);

		private processStruct process;

		private int tick = 0;

		private GameObject m_obj_GameScreen;

		private RawImage m_ri_GameScreen;

		public static Transform ui_Camera_tf;

		public static Camera ui_Camera_cam;

		public static GameObject goMain;

		private GameObject goPlayerNameLayer;

		private Transform bgLayer;

		public Transform floatUI;

		public Transform winLayer;

		private GameObject bgLayerObj;

		private Transform loadingLayer;

		private Transform stroyLayer;

		private Transform fightTextLayer;

		private Transform newbieLayer;

		private Transform dropItemLayer;

		private Dictionary<string, winData> winPool = new Dictionary<string, winData>();

		private List<string> dOpeningWin = new List<string>();

		public static int STATE_NORMAL = 0;

		public static int STATE_STORY = 1;

		public static int STATE_FUNCTIONBAR = 2;

		public static int STATE_FB_WIN = 3;

		public static int STATE_FB_BATTLE = 4;

		public static int STATE_3DUI = 5;

		public static int STATE_HIDE_ALL = 6;

		public static int STATE_SHOW_ONLYWIN = 7;

		public static int STATE_DIS_CONECT = 8;

		public static int STATE_ZHUANSHENG_ANI = 9;

		private int curState = InterfaceMgr.STATE_NORMAL;

		private static InterfaceMgr _instance;

		public InterfaceMgr()
		{
			this.init();
		}

		public static void openByLua(string name, object pram = null)
		{
			bool flag = InterfaceMgr.handleOpenByLua == null;
			if (!flag)
			{
				InterfaceMgr.handleOpenByLua(name, pram);
			}
		}

		public void doAction(string name, params object[] args)
		{
			debug.Log("aaaaaaaaa==" + name);
		}

		private void init()
		{
			this.creatWinData(InterfaceMgr.A3_RUNESTONETIP);
			this.creatWinData(InterfaceMgr.A3_RUNESTONE);
			this.creatWinData(InterfaceMgr.A3_BAOTUUI);
			this.creatWinData(InterfaceMgr.A3_EQPINHERIT);
			this.creatWinData(InterfaceMgr.A3_GETJEWELRYWAY);
			this.creatWinData(InterfaceMgr.A3_WINGINFO);
			this.creatWinData(InterfaceMgr.A3_SUMMONINFO);
			this.creatWinData(InterfaceMgr.A3_ATTCHANGE);
			this.creatWinData(InterfaceMgr.A3_PKMAPUI);
			this.creatWinData(InterfaceMgr.A3_ITEMLACK);
			this.creatWinData(InterfaceMgr.A3_RANKING);
			this.creatWinData(InterfaceMgr.A3_LHLIANJIE);
			this.creatWinData(InterfaceMgr.A3_QHMASTER);
			this.creatWinData(InterfaceMgr.A3_FINDBESTO);
			this.creatWinData(InterfaceMgr.RETURN_BT);
			this.creatWinData(InterfaceMgr.A3_PET_SKIN);
			this.creatWinData(InterfaceMgr.A3_NEW_PET);
			this.creatWinData(InterfaceMgr.A3_PET_RENEW);
			this.creatWinData(InterfaceMgr.A3_WIBG_SKIN);
			this.creatWinData(InterfaceMgr.A3_YGYIWU);
			this.creatWinData(InterfaceMgr.A3_RECHARGE);
			this.creatWinData(InterfaceMgr.A3_HUDUN);
			this.creatWinData(InterfaceMgr.NEWBIE_LINE);
			this.creatWinData(InterfaceMgr.NPC_TASK_TALK);
			this.creatWinData(InterfaceMgr.A3_DOING);
			this.creatWinData(InterfaceMgr.A3_AUCTION);
			this.creatWinData(InterfaceMgr.A3_CHAPTERHINT);
			this.creatWinData(InterfaceMgr.A3_BLESSING);
			this.creatWinData(InterfaceMgr.A3_WANTLVUP);
			this.creatWinData(InterfaceMgr.NPC_TALK);
			this.creatWinData(InterfaceMgr.A3_TARGETINFO);
			this.creatWinData(InterfaceMgr.A3TIPS_SUMMON);
			this.creatWinData(InterfaceMgr.A3_INSIDEUI_FB);
			this.creatWinData(InterfaceMgr.A3_FB_FINISH);
			this.creatWinData(InterfaceMgr.A3_SPEEDTEAM);
			this.creatWinData(InterfaceMgr.A3_ACTIVEDEGREE);
			this.creatWinData(InterfaceMgr.A3_DYETIP);
			this.creatWinData(InterfaceMgr.A3_ACTIVE);
			this.creatWinData(InterfaceMgr.A3_SUMMON);
			this.creatWinData(InterfaceMgr.A3_MINIMAP);
			this.creatWinData(InterfaceMgr.A3_HEROHEAD);
			this.creatWinData(InterfaceMgr.A3_BUFF);
			this.creatWinData(InterfaceMgr.A3_EXPBAR);
			this.creatWinData(InterfaceMgr.A3_FUNCTIONBAR);
			this.creatWinData(InterfaceMgr.A3_BAG);
			this.creatWinData(InterfaceMgr.A3_ACTIVE_GODLIGHT);
			this.creatWinData(InterfaceMgr.A3_WAREHOUSE);
			this.creatWinData(InterfaceMgr.A3_MAPNAME);
			this.creatWinData(InterfaceMgr.A3_FUNCOPEN);
			this.creatWinData(InterfaceMgr.A3_EQUIPSELL);
			this.creatWinData(InterfaceMgr.A3_EQUIPTIP);
			this.creatWinData(InterfaceMgr.A3_EQUIPUP);
			this.creatWinData(InterfaceMgr.A3_LVUP);
			this.creatWinData(InterfaceMgr.A3_LOWBLOOD);
			this.creatWinData(InterfaceMgr.A3_TRRIGERDIALOG);
			this.creatWinData(InterfaceMgr.A3_ITEMTIP);
			this.creatWinData(InterfaceMgr.A3_ROLE);
			this.creatWinData(InterfaceMgr.A3_EQUIP);
			this.creatWinData(InterfaceMgr.A3_PET_DESC);
			this.creatWinData(InterfaceMgr.A3_SILLOPEN);
			this.creatWinData(InterfaceMgr.A3_RUNEOPEN);
			this.creatWinData(InterfaceMgr.SHOP_A3);
			this.creatWinData(InterfaceMgr.SKILL_A3);
			this.creatWinData(InterfaceMgr.A3_RANK);
			this.creatWinData(InterfaceMgr.A3_CREATECHA);
			this.creatWinData(InterfaceMgr.A3_SELECTCHA);
			this.creatWinData(InterfaceMgr.A3_SIGN);
			this.creatWinData(InterfaceMgr.A3_EXCHANGE);
			this.creatWinData(InterfaceMgr.A3_AUTOPLAY);
			this.creatWinData(InterfaceMgr.A3_AUTOPLAY_SKILL);
			this.creatWinData(InterfaceMgr.A3_STORE);
			this.creatWinData(InterfaceMgr.A3_RELIVE);
			this.creatWinData(InterfaceMgr.WORLD_MAP);
			this.creatWinData(InterfaceMgr.WORLD_MAP_SUB);
			this.creatWinData(InterfaceMgr.TARGET_HEAD);
			this.creatWinData(InterfaceMgr.A3_AUTOPLAY2);
			this.creatWinData(InterfaceMgr.A3_AUTOPLAY_EQP);
			this.creatWinData(InterfaceMgr.A3_AUTOPLAY_PICK);
			this.creatWinData(InterfaceMgr.A3_VIP);
			this.creatWinData(InterfaceMgr.A3_YILING);
			this.creatWinData(InterfaceMgr.A3_WASHREDNAME);
			this.creatWinData(InterfaceMgr.A3_TASK);
			this.creatWinData(InterfaceMgr.SKILL_TEST);
			this.creatWinData(InterfaceMgr.A3_MAIL);
			this.creatWinData(InterfaceMgr.LOGIN);
			this.creatWinData(InterfaceMgr.SERVE_CHOOSE);
			this.creatWinData(InterfaceMgr.MAILPAPER);
			this.creatWinData(InterfaceMgr.MINIMAP);
			this.creatWinData(InterfaceMgr.HEROHEAD);
			this.creatWinData(InterfaceMgr.SELECT_CHAR);
			this.creatWinData(InterfaceMgr.CREATE_CHAR);
			this.creatWinData(InterfaceMgr.HERO_3D);
			this.creatWinData(InterfaceMgr.HERO_NOTICE);
			this.creatWinData(InterfaceMgr.DISCONECT);
			this.creatWinData(InterfaceMgr.BEGIN_LOADING);
			this.creatWinData(InterfaceMgr.MAP_LOADING);
			this.creatWinData(InterfaceMgr.FUNCTION_BAR);
			this.creatWinData(InterfaceMgr.FLYTXT);
			this.creatWinData(InterfaceMgr.FIGHTINGTXT);
			this.creatWinData(InterfaceMgr.EXP_BAR);
			this.creatWinData(InterfaceMgr.A3_GIFTCARD);
			this.creatWinData(InterfaceMgr.PLAYER_INFO);
			this.creatWinData(InterfaceMgr.WAIT_LOADING);
			this.creatWinData(InterfaceMgr.JOYSTICK);
			this.creatWinData(InterfaceMgr.BAG);
			this.creatWinData(InterfaceMgr.SHOP);
			this.creatWinData(InterfaceMgr.SKILL_BAR);
			this.creatWinData(InterfaceMgr.A3_BESTRONGER);
			this.creatWinData(InterfaceMgr.EQUIP);
			this.creatWinData(InterfaceMgr.DEBUG);
			this.creatWinData(InterfaceMgr.WEAPON);
			this.creatWinData(InterfaceMgr.DRESS);
			this.creatWinData(InterfaceMgr.DRESSUPGRADE);
			this.creatWinData(InterfaceMgr.WEAPONUPGRADE);
			this.creatWinData(InterfaceMgr.WEAPONLUCKYDRAW);
			this.creatWinData(InterfaceMgr.TASK);
			this.creatWinData(InterfaceMgr.HERO);
			this.creatWinData(InterfaceMgr.STRONG);
			this.creatWinData(InterfaceMgr.A3_SYSTEM_SETTING);
			this.creatWinData(InterfaceMgr.STORY_DIALOG);
			this.creatWinData(InterfaceMgr.PLOT_LINKUI);
			this.creatWinData(InterfaceMgr.FB_MAIN);
			this.creatWinData(InterfaceMgr.FB_INFO);
			this.creatWinData(InterfaceMgr.FB_WIN);
			this.creatWinData(InterfaceMgr.FB_LOSE);
			this.creatWinData(InterfaceMgr.ACHIEVE);
			this.creatWinData(InterfaceMgr.SKILL);
			this.creatWinData(InterfaceMgr.MOUNT);
			this.creatWinData(InterfaceMgr.PAIHANG);
			this.creatWinData(InterfaceMgr.FAMILY);
			this.creatWinData(InterfaceMgr.FAMILY_CHECKS);
			this.creatWinData(InterfaceMgr.FAMILY_CREATE);
			this.creatWinData(InterfaceMgr.FAMILY_DONATION);
			this.creatWinData(InterfaceMgr.FAMILY_ICONTAGS);
			this.creatWinData(InterfaceMgr.FAMILY_JOBAPPOINT);
			this.creatWinData(InterfaceMgr.FAMILY_SKILL);
			this.creatWinData(InterfaceMgr.FAMILY_SETTING);
			this.creatWinData(InterfaceMgr.FAMILY_NOTICE);
			this.creatWinData(InterfaceMgr.FAMILY_TREASURE);
			this.creatWinData(InterfaceMgr.ATTR);
			this.creatWinData(InterfaceMgr.VIP);
			this.creatWinData(InterfaceMgr.VIP_UP);
			this.creatWinData(InterfaceMgr.GETTING);
			this.creatWinData(InterfaceMgr.SIGN);
			this.creatWinData(InterfaceMgr.RECHARGE);
			this.creatWinData(InterfaceMgr.WIPE_OUT);
			this.creatWinData(InterfaceMgr.FRIEND);
			this.creatWinData(InterfaceMgr.FRIEND_SEARCH);
			this.creatWinData(InterfaceMgr.UPLEVEL);
			this.creatWinData(InterfaceMgr.E_MAIL);
			this.creatWinData(InterfaceMgr.LOADING_CLOUD);
			this.creatWinData(InterfaceMgr.ARENA);
			this.creatWinData(InterfaceMgr.ARENA_BUY);
			this.creatWinData(InterfaceMgr.ARENA_REPORT);
			this.creatWinData(InterfaceMgr.ARENA_REWARDS);
			this.creatWinData(InterfaceMgr.ARENA_SHOP);
			this.creatWinData(InterfaceMgr.ARENA_ACCOUNT);
			this.creatWinData(InterfaceMgr.COMBO_TEXT);
			this.creatWinData(InterfaceMgr.BUY_SIGN);
			this.creatWinData(InterfaceMgr.FIRST_RECHANGE);
			this.creatWinData(InterfaceMgr.TAELS);
			this.creatWinData(InterfaceMgr.SETTINGS);
			this.creatWinData(InterfaceMgr.RELIVE);
			this.creatWinData(InterfaceMgr.STICAL);
			this.creatWinData(InterfaceMgr.FACTION);
			this.creatWinData(InterfaceMgr.BROADCASTING);
			this.creatWinData(InterfaceMgr.FB_ENERGY);
			this.creatWinData(InterfaceMgr.LOTTERY);
			this.creatWinData(InterfaceMgr.LOTTERY_DRAW);
			this.creatWinData(InterfaceMgr.RANKING);
			this.creatWinData(InterfaceMgr.FB);
			this.creatWinData(InterfaceMgr.PK_NOTIFY);
			this.creatWinData(InterfaceMgr.FLOAT_ARENA);
			this.creatWinData(InterfaceMgr.FB_3D);
			this.creatWinData(InterfaceMgr.OFFLINEEXP);
			this.creatWinData(InterfaceMgr.MONSTER_DICT);
			this.creatWinData(InterfaceMgr.CD_TIME);
			this.creatWinData(InterfaceMgr.ACTIVE);
			this.creatWinData(InterfaceMgr.GETGIFT);
			this.creatWinData(InterfaceMgr.NEEDGET);
			this.creatWinData(InterfaceMgr.SDK_LOADING);
			this.creatWinData(InterfaceMgr.CAMPAIGN);
			this.creatWinData(InterfaceMgr.MJJD);
			this.creatWinData(InterfaceMgr.MJJD_WIN);
			this.creatWinData(InterfaceMgr.MJJD_FLOAT);
			this.creatWinData(InterfaceMgr.GETNEW);
			this.creatWinData(InterfaceMgr.DIALOG);
			this.creatWinData(InterfaceMgr.A3_RESETLVL);
			this.creatWinData(InterfaceMgr.A3_GETGOLDWAY);
			this.creatWinData(InterfaceMgr.A3_RESETLVLSUCCESS);
			this.creatWinData(InterfaceMgr.A3_LOTTERY);
			this.creatWinData(InterfaceMgr.A3_ENTERLOTTERY);
			this.creatWinData(InterfaceMgr.CD);
			this.creatWinData(InterfaceMgr.A3_CHATROOM);
			this.creatWinData(InterfaceMgr.A3_PKMODEL);
			this.creatWinData(InterfaceMgr.CONFIRM_TEXT);
			this.creatWinData(InterfaceMgr.A3_SHEJIAO);
			this.creatWinData(InterfaceMgr.A3_BEREQUESTFRIEND);
			this.creatWinData(InterfaceMgr.A3_ACHIEVEMENT);
			this.creatWinData(InterfaceMgr.A3_INTERACTOTHERUI);
			this.creatWinData(InterfaceMgr.A3_AWARDCENTER);
			this.creatWinData(InterfaceMgr.A3_COUNTERPART);
			this.creatWinData(InterfaceMgr.A3_NPC_SHOP);
			this.creatWinData(InterfaceMgr.A3_FIRESTRECHARGEAWARD);
			this.creatWinData(InterfaceMgr.A3_TEAMAPPLYPANEL);
			this.creatWinData(InterfaceMgr.A3_TEAMINVITEDPANEL);
			this.creatWinData(InterfaceMgr.A3_TEAMPANEL);
			this.creatWinData(InterfaceMgr.A3_TEAMMEMBERLIST);
			this.creatWinData(InterfaceMgr.A3_LEGION_DART);
			this.creatWinData(InterfaceMgr.A3_SLAY_DRAGON);
			this.creatWinData(InterfaceMgr.A3_CURRENTTEAMINFO);
			this.creatWinData(InterfaceMgr.A3_EVERYDAYLOGIN);
			this.creatWinData(InterfaceMgr.A3_LITEMINIMAP);
			this.creatWinData(InterfaceMgr.A3_SMITHY);
			this.creatWinData(InterfaceMgr.A3_ELITEMON);
			this.creatWinData(InterfaceMgr.A3_QUICKOP);
			this.creatWinData(InterfaceMgr.A3_MINITIP);
			this.creatWinData(InterfaceMgr.TRANSMIT_PANEL);
			this.creatWinData(InterfaceMgr.A3_TASKOPT);
			this.m_obj_GameScreen = GameObject.Find("game_screen");
			bool flag = this.m_obj_GameScreen;
			if (flag)
			{
				this.m_ri_GameScreen = this.m_obj_GameScreen.GetComponent<RawImage>();
				this.m_obj_GameScreen.SetActive(false);
			}
			Transform transform = GameObject.Find("bgLayer").transform.FindChild("bgLayer1");
			this.bgLayer = GameObject.Find("bgLayer").transform;
			this.bgLayerObj = transform.gameObject;
			GameObject gameObject = GameObject.Find("winLayer");
			this.winLayer = gameObject.transform;
			GameObject gameObject2 = GameObject.Find("floatUI");
			this.floatUI = gameObject2.transform;
			this.stroyLayer = GameObject.Find("storyLayer").transform;
			this.fightTextLayer = GameObject.Find("fightText").transform;
			this.loadingLayer = GameObject.Find("loadingLayer").transform;
			this.goPlayerNameLayer = GameObject.Find("playername");
			this.newbieLayer = GameObject.Find("newbieLayer").transform;
			this.dropItemLayer = GameObject.Find("dropItemLayer").transform;
			InterfaceMgr.setUntouchable(this.goPlayerNameLayer);
			transform.localScale = new Vector3(3f, 3f, 1f);
			InterfaceMgr.goMain = GameObject.Find("canvas_main");
			this.mainLoadingBg = InterfaceMgr.goMain.transform.FindChild("loadingbg").gameObject;
			this.mainLoadingBg.SetActive(false);
			this.open(InterfaceMgr.DEBUG, null, false);
			bool flag2 = CrossApp.singleton != null;
			if (flag2)
			{
				this.process = new processStruct(new Action<float>(this.onUpdate), "InterfaceMgr", false, false);
				(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			}
			InterfaceMgr.ui_Camera_tf = GameObject.Find("ui_main_camera").transform;
			InterfaceMgr.ui_Camera_cam = GameObject.Find("ui_main_camera").GetComponent<Camera>();
			Transform transform2 = GameObject.Find("Canvas_overlay").transform;
			Vector3 position = transform2.position;
			position.z = -100f;
			InterfaceMgr.ui_Camera_tf.position = position;
			InterfaceMgr.ui_Camera_cam.orthographicSize = transform2.localScale.x * 320f;
		}

		public void linkGameScreen(RenderTexture rt)
		{
			bool flag = this.m_ri_GameScreen != null;
			if (flag)
			{
				this.m_ri_GameScreen.texture = rt;
			}
		}

		public void showGameScreen()
		{
			bool flag = this.m_obj_GameScreen != null;
			if (flag)
			{
				this.m_obj_GameScreen.SetActive(true);
			}
		}

		public void hideGameScreen()
		{
			bool flag = this.m_obj_GameScreen != null;
			if (flag)
			{
				this.m_obj_GameScreen.SetActive(false);
			}
		}

		public void showLoadingBg(bool b)
		{
			this.mainLoadingBg.SetActive(b);
		}

		private void hideLayer(Transform lay)
		{
			lay.localScale = Vector3.zero;
		}

		private void showLayer(Transform lay)
		{
			lay.localScale = Vector3.one;
		}

		private void onUpdate(float s)
		{
			this.tick++;
			bool flag = this.tick < 30;
			if (!flag)
			{
				this.tick = 0;
				bool flag2 = InterfaceMgr.ui_Camera_cam != null && !InterfaceMgr.ui_Camera_cam.gameObject.active;
				if (flag2)
				{
					InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
				}
			}
		}

		private winData creatWinData(string name)
		{
			winData winData = default(winData);
			winData.winName = name;
			this.winPool.Add(name, winData);
			return winData;
		}

		public static void setUntouchable(GameObject go)
		{
			CanvasGroup canvasGroup = go.GetComponent<CanvasGroup>();
			bool flag = canvasGroup == null;
			if (flag)
			{
				canvasGroup = go.AddComponent<CanvasGroup>();
			}
			canvasGroup.blocksRaycasts = false;
		}

		public winData open(string name, ArrayList data = null, bool isFunctionBar = false)
		{
			bool flag = name == InterfaceMgr.A3_RECHARGE;
			winData result;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("comm_un_recharge", null), 0, default(Color), null);
				result = default(winData);
			}
			else
			{
				winData winData = this.winPool[name];
				bool flag2 = winData.winName == null;
				if (flag2)
				{
					result = default(winData);
				}
				else
				{
					bool flag3 = lgSelfPlayer.instance != null && lgSelfPlayer.instance.getAni() == "run";
					if (flag3)
					{
						bool flag4 = joystick.instance != null;
						if (flag4)
						{
							joystick.instance.OnDragOut(null);
						}
						else
						{
							lgSelfPlayer.instance.onJoystickEnd(false);
						}
					}
					bool flag5 = winData.winItem == null;
					Baselayer baselayer;
					if (flag5)
					{
						Debug.Log("prefab ==== ............ " + winData.winName);
						GameObject gameObject = Resources.Load("prefab/" + winData.winName) as GameObject;
						bool flag6 = gameObject == null;
						if (flag6)
						{
							result = default(winData);
							return result;
						}
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						string text = winData.winTempName;
						bool flag7 = text == null;
						if (flag7)
						{
							text = winData.winName;
						}
						Type type = Type.GetType("MuGame." + text);
						bool flag8 = type == null;
						if (flag8)
						{
							result = default(winData);
							return result;
						}
						gameObject2.AddComponent(type);
						baselayer = (Baselayer)gameObject2.GetComponent(winData.winName);
						baselayer.uiName = name;
						baselayer.uiData = data;
						winData.winComponent = baselayer;
						bool flag9 = baselayer.type == (float)Baselayer.LAYER_TYPE_FLOATUI;
						if (flag9)
						{
							gameObject2.transform.SetParent(this.floatUI, false);
						}
						else
						{
							bool flag10 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW;
							if (flag10)
							{
								gameObject2.transform.SetParent(this.winLayer, false);
								bool flag11 = name != InterfaceMgr.WAIT_LOADING;
								if (flag11)
								{
								}
								baselayer.addBg();
								if (isFunctionBar)
								{
									baselayer.isFunctionBar = true;
								}
								else
								{
									baselayer.isFunctionBar = false;
								}
							}
							else
							{
								bool flag12 = baselayer.type == (float)Baselayer.LAYER_TYPE_LOADING;
								if (flag12)
								{
									gameObject2.transform.SetParent(this.loadingLayer, false);
								}
								else
								{
									bool flag13 = baselayer.type == (float)Baselayer.LAYER_TYPE_STORY;
									if (flag13)
									{
										gameObject2.transform.SetParent(this.stroyLayer, false);
									}
									else
									{
										bool flag14 = baselayer.type == (float)Baselayer.LAYER_TYPE_FIGHT_TEXT;
										if (flag14)
										{
											gameObject2.transform.SetParent(this.fightTextLayer, false);
										}
									}
								}
							}
						}
						winData.winItem = gameObject2;
						this.winPool.Remove(name);
						this.winPool.Add(name, winData);
						gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
						gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
					}
					else
					{
						bool active = winData.winItem.active;
						if (active)
						{
							result = winData;
							return result;
						}
						baselayer = (Baselayer)winData.winItem.GetComponent(winData.winName);
						baselayer.uiData = data;
						baselayer.__open(true);
					}
					bool flag15 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW;
					if (flag15)
					{
						bool flag16 = name != InterfaceMgr.WAIT_LOADING && name != InterfaceMgr.A3_EVERYDAYLOGIN;
						if (flag16)
						{
							MediaClient.instance.PlaySoundUrl("audio/common/open_interface", false, null);
						}
						this.dOpeningWin.Add(name);
						if (isFunctionBar)
						{
							baselayer.isFunctionBar = true;
						}
						else
						{
							baselayer.isFunctionBar = false;
						}
					}
					bool flag17 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW_3D;
					if (flag17)
					{
						MediaClient.instance.PlaySoundUrl("media/ui/open", false, null);
					}
					bool flag18 = baselayer is IBgLayerUI;
					if (flag18)
					{
						IBgLayerUI bgLayerUI = (IBgLayerUI)baselayer;
					}
					result = winData;
				}
			}
			return result;
		}

		public bool checkWinOpened(string winid)
		{
			return this.winPool.ContainsKey(winid) && this.winPool[winid].winItem != null && this.winPool[winid].winItem.active;
		}

		public Baselayer getUiComponentByName(string name)
		{
			return this.winPool[name].winComponent;
		}

		public void close(string name)
		{
			bool flag = name == null || name.Equals("");
			if (!flag)
			{
				winData winData = this.winPool[name];
				bool flag2 = winData.winItem == null;
				if (!flag2)
				{
					Baselayer baselayer = (Baselayer)winData.winItem.GetComponent(winData.winName);
					baselayer.__close();
					bool flag3 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW;
					if (flag3)
					{
						bool flag4 = name != InterfaceMgr.WAIT_LOADING;
						if (flag4)
						{
						}
					}
					bool flag5 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW_3D;
					if (flag5)
					{
						MediaClient.instance.PlaySoundUrl("media/ui/close", false, null);
					}
					bool flag6 = baselayer.type == (float)Baselayer.LAYER_TYPE_WINDOW;
					if (flag6)
					{
						this.dOpeningWin.Remove(name);
					}
				}
			}
		}

		public void closeAllWin(string except = "")
		{
			for (int i = 0; i < this.dOpeningWin.Count; i++)
			{
				string text = this.dOpeningWin[i];
				bool flag = except != text && text != InterfaceMgr.LOGIN;
				if (flag)
				{
					this.close(text);
					i--;
				}
			}
		}

		public void closeFui_NB()
		{
			this.close(InterfaceMgr.A3_CHATROOM);
			this.close(InterfaceMgr.A3_PKMAPUI);
		}

		public void openUiFirstTime()
		{
			List<string> list = new List<string>();
			list.Add(InterfaceMgr.A3_BAG);
			list.Add(InterfaceMgr.A3_SHEJIAO);
			list.Add(InterfaceMgr.A3_EQUIP);
			List<string> first_ui_lua = new List<string>();
			bool flag = maploading.instance != null;
			if (flag)
			{
				maploading.instance.loadingUi(list, first_ui_lua, false);
			}
		}

		public void closeUiFirstTime()
		{
			this.close(InterfaceMgr.A3_BAG);
			this.close(InterfaceMgr.A3_SHEJIAO);
			this.close(InterfaceMgr.A3_EQUIP);
		}

		public void initFirstUi()
		{
			List<string> list = new List<string>();
			list.Add(InterfaceMgr.A3_LITEMINIMAP);
			list.Add(InterfaceMgr.JOYSTICK);
			list.Add(InterfaceMgr.SKILL_BAR);
			list.Add(InterfaceMgr.A3_BESTRONGER);
			list.Add(InterfaceMgr.FLYTXT);
			list.Add(InterfaceMgr.A3_ATTCHANGE);
			list.Add(InterfaceMgr.PK_NOTIFY);
			list.Add(InterfaceMgr.A3_HEROHEAD);
			list.Add(InterfaceMgr.BROADCASTING);
			list.Add(InterfaceMgr.A3_EXPBAR);
			list.Add(InterfaceMgr.A3_EQUIPUP);
			list.Add(InterfaceMgr.A3_LOWBLOOD);
			list.Add(InterfaceMgr.A3_TRRIGERDIALOG);
			list.Add(InterfaceMgr.A3_LVUP);
			list.Add(InterfaceMgr.A3_MAPNAME);
			list.Add(InterfaceMgr.A3_FUNCOPEN);
			list.Add(InterfaceMgr.A3_SILLOPEN);
			list.Add(InterfaceMgr.A3_RUNEOPEN);
			list.Add(InterfaceMgr.TARGET_HEAD);
			list.Add(InterfaceMgr.A3_QUICKOP);
			list.Add(InterfaceMgr.A3_TASKOPT);
			list.Add(InterfaceMgr.A3_CHATROOM);
			list.Add(InterfaceMgr.A3_ENTERLOTTERY);
			bool showBaotu_ui = ModelBase<PlayerModel>.getInstance().showBaotu_ui;
			if (showBaotu_ui)
			{
				list.Add(InterfaceMgr.A3_BAOTUUI);
				ModelBase<PlayerModel>.getInstance().showBaotu_ui = false;
			}
			List<string> list2 = new List<string>();
			list2.Add("a3_litemap");
			list2.Add("a3_litemap_btns");
			list2.Add("flytxt");
			list2.Add("fightingup");
			list2.Add("herohead2");
			list2.Add("herohead");
			list2.Add("expbar");
			bool flag = maploading.instance != null;
			if (flag)
			{
				maploading.instance.loadingUi(list, list2, true);
			}
		}

		public void closeAllWins(string[] except = null)
		{
			List<string> list = null;
			bool flag = except != null;
			if (flag)
			{
				list = new List<string>(except);
			}
			for (int i = 0; i < this.dOpeningWin.Count; i++)
			{
				string text = this.dOpeningWin[i];
				bool flag2 = list != null && !list.Contains(text) && text != InterfaceMgr.LOGIN;
				if (flag2)
				{
					this.close(text);
					i--;
				}
			}
		}

		public void changeState(int state)
		{
			bool flag = this.curState == state;
			if (!flag)
			{
				this.curState = state;
				bool flag2 = state == InterfaceMgr.STATE_NORMAL;
				if (flag2)
				{
					this.showLayer(this.stroyLayer);
					this.showLayer(this.floatUI);
					this.showLayer(this.winLayer);
					this.showLayer(this.bgLayer);
					this.showLayer(this.goPlayerNameLayer.transform);
					this.showLayer(this.fightTextLayer);
					this.showLayer(this.newbieLayer);
					this.showLayer(this.loadingLayer);
					this.showLayer(this.dropItemLayer);
					InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
					bool flag3 = a3_liteMinimap.instance;
					if (flag3)
					{
						a3_liteMinimap.instance.miniMapActive = true;
					}
					bool flag4 = broadcasting.instance;
					if (flag4)
					{
						broadcasting.instance.on_off(true);
					}
					bool flag5 = npctalk.instance != null;
					if (flag5)
					{
						npctalk.instance.MinOrMax(true);
					}
				}
				else
				{
					bool flag6 = state == InterfaceMgr.STATE_STORY;
					if (flag6)
					{
						this.hideLayer(this.floatUI);
						this.closeAllWins(new string[]
						{
							InterfaceMgr.DIALOG,
							InterfaceMgr.CD,
							InterfaceMgr.A3_CHAPTERHINT
						});
						this.hideLayer(this.bgLayer);
						this.hideLayer(this.goPlayerNameLayer.transform);
						bool flag7 = a3_liteMinimap.instance;
						if (flag7)
						{
							a3_liteMinimap.instance.miniMapActive = false;
						}
						InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
						this.showLayer(this.stroyLayer);
					}
					else
					{
						bool flag8 = state == InterfaceMgr.STATE_FUNCTIONBAR;
						if (flag8)
						{
							this.hideLayer(this.floatUI);
							this.hideLayer(this.bgLayer);
							this.hideLayer(this.dropItemLayer);
							this.hideLayer(this.goPlayerNameLayer.transform);
							this.hideLayer(this.fightTextLayer);
							InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
						}
						else
						{
							bool flag9 = state == InterfaceMgr.STATE_FB_WIN;
							if (flag9)
							{
								this.hideLayer(this.goPlayerNameLayer.transform);
								this.hideLayer(this.floatUI);
								InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
							}
							else
							{
								bool flag10 = state == InterfaceMgr.STATE_DIS_CONECT;
								if (flag10)
								{
									this.hideLayer(this.stroyLayer);
									this.showLayer(this.winLayer);
									InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
								}
								else
								{
									bool flag11 = state == InterfaceMgr.STATE_FB_BATTLE;
									if (flag11)
									{
										this.showLayer(this.floatUI);
										this.showLayer(this.winLayer);
										this.showLayer(this.bgLayer);
										this.showLayer(this.goPlayerNameLayer.transform);
										bool flag12 = broadcasting.instance;
										if (flag12)
										{
											broadcasting.instance.on_off(false);
										}
										InterfaceMgr.ui_Camera_cam.gameObject.SetActive(true);
									}
									else
									{
										bool flag13 = state == InterfaceMgr.STATE_HIDE_ALL;
										if (flag13)
										{
											bool flag14 = a3_liteMinimap.instance;
											if (flag14)
											{
												a3_liteMinimap.instance.miniMapActive = false;
											}
											this.hideLayer(this.floatUI);
											this.hideLayer(this.winLayer);
											this.hideLayer(this.bgLayer);
											this.hideLayer(this.goPlayerNameLayer.transform);
											this.hideLayer(this.newbieLayer);
										}
										else
										{
											bool flag15 = state == InterfaceMgr.STATE_SHOW_ONLYWIN;
											if (flag15)
											{
												this.hideLayer(this.loadingLayer);
												this.hideLayer(this.floatUI);
												this.hideLayer(this.bgLayer);
												this.hideLayer(this.goPlayerNameLayer.transform);
											}
											else
											{
												bool flag16 = state == InterfaceMgr.STATE_ZHUANSHENG_ANI;
												if (flag16)
												{
													this.hideLayer(this.floatUI);
													this.hideLayer(this.winLayer);
													this.hideLayer(this.bgLayer);
													this.hideLayer(this.goPlayerNameLayer.transform);
													this.hideLayer(this.newbieLayer);
													this.hideLayer(this.stroyLayer);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		public void itemToWin(bool Toclose, string winName)
		{
			bool flag = a3_itemLack.intans && a3_itemLack.intans.closewindow != null;
			if (flag)
			{
				if (Toclose)
				{
					bool flag2 = a3_itemLack.intans.closewindow != winName;
					if (flag2)
					{
						InterfaceMgr.getInstance().open(a3_itemLack.intans.closewindow, null, false);
					}
					a3_itemLack.intans.closewindow = null;
				}
				else
				{
					bool flag3 = a3_itemLack.intans.closewindow != winName;
					if (flag3)
					{
						a3_itemLack.intans.closewindow = null;
					}
				}
			}
		}

		public void delclose(string name)
		{
			winData winData = this.winPool[name];
			bool flag = winData.winItem == null;
			if (!flag)
			{
				Baselayer winComponent = winData.winComponent;
				winComponent.dispose();
			}
		}

		public void destory(string name)
		{
			winData winData = this.winPool[name];
			bool flag = winData.winItem == null;
			if (!flag)
			{
				Baselayer winComponent = winData.winComponent;
				winComponent.dispose();
				this.winPool.Remove(name);
			}
		}

		public static InterfaceMgr getInstance()
		{
			bool flag = InterfaceMgr._instance == null;
			if (flag)
			{
				InterfaceMgr._instance = new InterfaceMgr();
			}
			return InterfaceMgr._instance;
		}

		private void Awake()
		{
			InterfaceMgr._instance = this;
		}

		public static EventTriggerListener Get(GameObject go)
		{
			EventTriggerListener eventTriggerListener = go.GetComponent<EventTriggerListener>();
			bool flag = eventTriggerListener == null;
			if (flag)
			{
				eventTriggerListener = go.AddComponent<EventTriggerListener>();
			}
			return eventTriggerListener;
		}
	}
}
