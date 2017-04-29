using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class muLGClient : gameManager
	{
		public static muLGClient instance;

		public lgGDSkill g_skillCT
		{
			get
			{
				return base.getObject("LGGD_SKILL") as lgGDSkill;
			}
		}

		public LGGDFeeds g_FeedCT
		{
			get
			{
				return base.getObject("LGGD_FEED") as LGGDFeeds;
			}
		}

		public MgrPlayerInfo g_MgrPlayerInfoCT
		{
			get
			{
				return base.getObject("MgrPlayerInfo") as MgrPlayerInfo;
			}
		}

		public LGGDRmission g_rmissCT
		{
			get
			{
				return base.getObject("LG_RMISSION") as LGGDRmission;
			}
		}

		public lgGDWorldBoss g_worldboss
		{
			get
			{
				return base.getObject("LGGD_WORLDOOS") as lgGDWorldBoss;
			}
		}

		public LGGDMission g_missionCT
		{
			get
			{
				return base.getObject("LG_MISSION") as LGGDMission;
			}
		}

		public lgGDTrade g_tradeCT
		{
			get
			{
				return base.getObject("LGGD_TRADE") as lgGDTrade;
			}
		}

		public lgGDItems g_itemsCT
		{
			get
			{
				return base.getObject("LG_ITEMS") as lgGDItems;
			}
		}

		public lgGDBuddy g_buddyCT
		{
			get
			{
				return base.getObject("LGGD_BUDDY") as lgGDBuddy;
			}
		}

		public lgGDCard g_cardCT
		{
			get
			{
				return base.getObject("LGGD_CARD") as lgGDCard;
			}
		}

		public lgGDChat lgGD_Chat
		{
			get
			{
				return base.getObject("LGGD_CHAT") as lgGDChat;
			}
		}

		public lgGDChat g_chatCT
		{
			get
			{
				return base.getObject("LGGD_CHAT") as lgGDChat;
			}
		}

		public lgGDClans g_clansCT
		{
			get
			{
				return base.getObject("LGGD_CLANS") as lgGDClans;
			}
		}

		public lgGDGeneral g_generalCT
		{
			get
			{
				return base.getObject("LGGD_GENERAL") as lgGDGeneral;
			}
		}

		public LGJoinWorld g_joinWorldCT
		{
			get
			{
				return base.getObject("LG_JOIN_WORLD") as LGJoinWorld;
			}
		}

		public LGOutGame g_outGameCT
		{
			get
			{
				return base.getObject("LG_OUT_GAME") as LGOutGame;
			}
		}

		public LGLoadResource g_loadResourceCT
		{
			get
			{
				return base.getObject("LG_LOAD_RESOURCE") as LGLoadResource;
			}
		}

		public LGMap g_mapCT
		{
			get
			{
				return base.getObject("LG_MAP") as LGMap;
			}
		}

		public lgSelfPlayer g_selfPlayer
		{
			get
			{
				return base.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public LGCamera g_cameraCT
		{
			get
			{
				return base.getObject("LG_CAMERA") as LGCamera;
			}
		}

		public LGNpcs g_npcsCT
		{
			get
			{
				return base.getObject("LG_NPCS") as LGNpcs;
			}
		}

		public LGMonsters g_monstersCT
		{
			get
			{
				return base.getObject("LG_MONSTERS") as LGMonsters;
			}
		}

		public LGGDActivity g_activityCT
		{
			get
			{
				return base.getObject("LG_ACTIVITY") as LGGDActivity;
			}
		}

		public LGGDAcupoint g_acupointCT
		{
			get
			{
				return base.getObject("LG_ACUPOINT") as LGGDAcupoint;
			}
		}

		public LGGDArena g_arenaCT
		{
			get
			{
				return base.getObject("LG_ARENA") as LGGDArena;
			}
		}

		public LGGDAward lgGD_Award
		{
			get
			{
				return base.getObject("LG_AWARD") as LGGDAward;
			}
		}

		public LGGDLevels g_levelsCT
		{
			get
			{
				return base.getObject("LG_LEVEL") as LGGDLevels;
			}
		}

		public lgGDVIP g_vipCT
		{
			get
			{
				return base.getObject("lgGDVIP") as lgGDVIP;
			}
		}

		public lgGDWorldline g_worldlineCT
		{
			get
			{
				return base.getObject("LG_WORLDLINE") as lgGDWorldline;
			}
		}

		public lgGDAchieve g_achieveCT
		{
			get
			{
				return base.getObject("lgGDAchieve") as lgGDAchieve;
			}
		}

		public lgGDAchives g_achivesCT
		{
			get
			{
				return base.getObject("lgGDAchives") as lgGDAchives;
			}
		}

		public LGGDPVP g_pvpCT
		{
			get
			{
				return base.getObject("lgGDPvp") as LGGDPVP;
			}
		}

		public lgGDPlyFun g_plyfunCT
		{
			get
			{
				return base.getObject("LGGD_PLYFUN") as lgGDPlyFun;
			}
		}

		public lgGDOlAward g_olawardCT
		{
			get
			{
				return base.getObject("LG_OLAWARD") as lgGDOlAward;
			}
		}

		public lgGDRandshop g_randshopCT
		{
			get
			{
				return base.getObject("LG_RANDSHOP") as lgGDRandshop;
			}
		}

		public LGGDMails g_mailsCT
		{
			get
			{
				return base.getObject("LGGD_MAILS") as LGGDMails;
			}
		}

		public LGOthers g_othersCT
		{
			get
			{
				return base.getObject("LG_OTHER_PLAYERS") as LGOthers;
			}
		}

		public MUlgGDVendor g_vendorCT
		{
			get
			{
				return base.getObject("LGGD_VENDOR") as MUlgGDVendor;
			}
		}

		public lgGDMarket g_marketCT
		{
			get
			{
				return base.getObject("LGGD_MARKET") as lgGDMarket;
			}
		}

		public lgGDLottery g_lotteryCT
		{
			get
			{
				return base.getObject("LGGD_LOTTERY") as lgGDLottery;
			}
		}

		public lgGDRank g_RankCT
		{
			get
			{
				return base.getObject("LGGD_RANK") as lgGDRank;
			}
		}

		public lgGDDmis g_DmisCT
		{
			get
			{
				return base.getObject("LGGD_DMIS") as lgGDDmis;
			}
		}

		public muLGClient(gameMain m) : base(m)
		{
			Screen.sleepTimeout = -1;
		}

		protected override void onInit()
		{
			muLGClient.instance = this;
			base.regCreator("MgrPlayerInfo", new Func<IClientBase, IObjectPlugin>(MgrPlayerInfo.create));
			base.regCreator("LG_OUT_GAME", new Func<IClientBase, IObjectPlugin>(LGOutGame.create));
			base.regCreator("LG_MAIN_PLAY", new Func<IClientBase, IObjectPlugin>(lgSelfPlayer.create));
			base.regCreator("LG_CAMERA", new Func<IClientBase, IObjectPlugin>(LGCamera.create));
			base.regCreator("LG_MAP", new Func<IClientBase, IObjectPlugin>(LGMap.create));
			base.regCreator("LG_NPCS", new Func<IClientBase, IObjectPlugin>(LGNpcs.create));
			base.regCreator("LG_ITEMS", new Func<IClientBase, IObjectPlugin>(lgGDItems.create));
			base.regCreator("LG_JOIN_WORLD", new Func<IClientBase, IObjectPlugin>(LGJoinWorld.create));
			base.regCreator("LG_LOAD_RESOURCE", new Func<IClientBase, IObjectPlugin>(LGLoadResource.create));
			base.regCreator("LG_MONSTERS", new Func<IClientBase, IObjectPlugin>(LGMonsters.create));
			base.regCreator("LG_HEROS", new Func<IClientBase, IObjectPlugin>(LGHeros.create));
			base.regCreator("LG_OTHER_PLAYERS", new Func<IClientBase, IObjectPlugin>(LGOthers.create));
			base.regCreator("MSG_CHANGELINE", new Func<IClientBase, IObjectPlugin>(LGChangeLine.create));
			base.regCreator("LG_LEVEL", new Func<IClientBase, IObjectPlugin>(LGGDLevels.create));
			new DelayDoManager(this);
			base.createAllSingleInst();
			foreach (IObjectPlugin current in this.m_objectPlugins.Values)
			{
				current.init();
			}
			base.g_processM.addRender(this.g_selfPlayer, true);
		}
	}
}
