using GameFramework;
using System;

namespace MuGame
{
	public class muNetCleint : NetClient
	{
		public new static muNetCleint instance;

		public outGameMsgs outGameMsgsInst
		{
			get
			{
				return base.getObject("MSG_OUT_GAME") as outGameMsgs;
			}
		}

		public InGameDmisMsgs igDmisMsgs
		{
			get
			{
				return base.getObject("MSG_DMIS") as InGameDmisMsgs;
			}
		}

		public InGameAchieveMsgs igAchieveMsgs
		{
			get
			{
				return base.getObject("MSG_ACHIEVE") as InGameAchieveMsgs;
			}
		}

		public InGameAcupointMsgs igAcupointMsgs
		{
			get
			{
				return base.getObject("MSG_ACUPOINT") as InGameAcupointMsgs;
			}
		}

		public InGameMissionMsgs igMissionMsgs
		{
			get
			{
				return base.getObject("MSG_MISSION") as InGameMissionMsgs;
			}
		}

		public InGameVIPMsgs igVipMsgs
		{
			get
			{
				return base.getObject("MSG_VIP") as InGameVIPMsgs;
			}
		}

		public InGamePalyerInfoMsgs igPlayerInfoMsgs
		{
			get
			{
				return base.getObject("MSG_PALYER_INFO") as InGamePalyerInfoMsgs;
			}
		}

		public InGameGeneralMsgs igGenMsg
		{
			get
			{
				return base.getObject("MSG_GENERAL") as InGameGeneralMsgs;
			}
		}

		public InGamePlyFunMsgs igPlyFunMsgs
		{
			get
			{
				return base.getObject("MSG_PLY_FUN") as InGamePlyFunMsgs;
			}
		}

		public InGameRankMsgs igRankMsgs
		{
			get
			{
				return base.getObject("MSG_RANK_INFO") as InGameRankMsgs;
			}
		}

		public InGameRandshop igRandShopMsgs
		{
			get
			{
				return base.getObject("MSG_RANDSHOP") as InGameRandshop;
			}
		}

		public InGameTradeMsgs igTradeMsgs
		{
			get
			{
				return base.getObject("MSG_TRADE") as InGameTradeMsgs;
			}
		}

		public InGameSkillMsgs igSkillMsgs
		{
			get
			{
				return base.getObject("MSG_SKILL") as InGameSkillMsgs;
			}
		}

		public InGameItemMsgs igItemMsg
		{
			get
			{
				return base.getObject("MSG_ITEM") as InGameItemMsgs;
			}
		}

		public InGameChangeLineMsgs igChangeLineMsgs
		{
			get
			{
				return base.getObject("MSG_CHANGELINE") as InGameChangeLineMsgs;
			}
		}

		public InGameChatMsgs igChatMsgs
		{
			get
			{
				return base.getObject("MSG_CHAT") as InGameChatMsgs;
			}
		}

		public InGameActivityMsgs igActivityMsgs
		{
			get
			{
				return base.getObject("MSG_ACTIVITY") as InGameActivityMsgs;
			}
		}

		public InGameWelfare igWelfareMsgs
		{
			get
			{
				return base.getObject("MSG_WELFARE") as InGameWelfare;
			}
		}

		public InGameOlAwardMsgs igOlAwardMsgs
		{
			get
			{
				return base.getObject("MSG_OLAWARD") as InGameOlAwardMsgs;
			}
		}

		public InGameMailMsgs igMailMsgs
		{
			get
			{
				return base.getObject("MSG_MAIL") as InGameMailMsgs;
			}
		}

		public InGameClansMsgs igClanMsgs
		{
			get
			{
				return base.getObject("MSG_CLAN") as InGameClansMsgs;
			}
		}

		public InGameBuddyMsgs igBuddyMsgs
		{
			get
			{
				return base.getObject("MSG_BUDDY") as InGameBuddyMsgs;
			}
		}

		public InGameTeamMsgs igTeamMsgs
		{
			get
			{
				return base.getObject("MSG_TEAM") as InGameTeamMsgs;
			}
		}

		public InGameAwardMsgs igAwardMsgs
		{
			get
			{
				return base.getObject("MSG_AWARD") as InGameAwardMsgs;
			}
		}

		public InGameMapMsgs igMapMsgs
		{
			get
			{
				return base.getObject("MSG_MAP") as InGameMapMsgs;
			}
		}

		public InGameLevelMsgs igLevelMsgs
		{
			get
			{
				return base.getObject("MSG_LEVEL") as InGameLevelMsgs;
			}
		}

		public InGameLotteryMsgs igLotteryMsgs
		{
			get
			{
				return base.getObject("MSG_LOTTERY") as InGameLotteryMsgs;
			}
		}

		public connInfo connInfoInst
		{
			get
			{
				return base.getObject("DATA_CONN") as connInfo;
			}
		}

		public charsInfo charsInfoInst
		{
			get
			{
				return base.getObject("DATA_CHARS") as charsInfo;
			}
		}

		public joinWorldInfo joinWorldInfoInst
		{
			get
			{
				return base.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
			}
		}

		public itemsInfo itemsInfoInst
		{
			get
			{
				return base.getObject("DATA_ITEMS") as itemsInfo;
			}
		}

		public shopInfo shopInfoInst
		{
			get
			{
				return base.getObject("DATA_SHOP") as shopInfo;
			}
		}

		public chatInfo chatInfoInst
		{
			get
			{
				return base.getObject("DATA_CHAT") as chatInfo;
			}
		}

		public smithyInfo smithyInfoInst
		{
			get
			{
				return base.getObject("DATA_SMITHY") as smithyInfo;
			}
		}

		public muNetCleint(gameMain m) : base(m)
		{
		}

		protected override void onInit()
		{
			muNetCleint.instance = this;
			base.regCreator("DATA_CONN", new Func<IClientBase, IObjectPlugin>(connInfo.create));
			base.regCreator("DATA_JOIN_WORLD", new Func<IClientBase, IObjectPlugin>(joinWorldInfo.create));
			base.regCreator("DATA_CHARS", new Func<IClientBase, IObjectPlugin>(charsInfo.create));
			base.regCreator("DATA_ITEMS", new Func<IClientBase, IObjectPlugin>(itemsInfo.create));
			base.regCreator("DATA_CHAT", new Func<IClientBase, IObjectPlugin>(chatInfo.create));
			base.regCreator("DATA_SMITHY", new Func<IClientBase, IObjectPlugin>(smithyInfo.create));
			base.regCreator("DATA_SHOP", new Func<IClientBase, IObjectPlugin>(shopInfo.create));
			base.regCreator("MSG_OUT_GAME", new Func<IClientBase, IObjectPlugin>(outGameMsgs.create));
			base.regCreator("MSG_GENERAL", new Func<IClientBase, IObjectPlugin>(InGameGeneralMsgs.create));
			base.regCreator("MSG_MISSION", new Func<IClientBase, IObjectPlugin>(InGameMissionMsgs.create));
			base.regCreator("MSG_VIP", new Func<IClientBase, IObjectPlugin>(InGameVIPMsgs.create));
			base.regCreator("MSG_PALYER_INFO", new Func<IClientBase, IObjectPlugin>(InGamePalyerInfoMsgs.create));
			base.regCreator("MSG_PLY_FUN", new Func<IClientBase, IObjectPlugin>(InGamePlyFunMsgs.create));
			base.regCreator("MSG_RANK_INFO", new Func<IClientBase, IObjectPlugin>(InGameRankMsgs.create));
			base.regCreator("MSG_RANDSHOP", new Func<IClientBase, IObjectPlugin>(InGameRandshop.create));
			base.regCreator("MSG_TRADE", new Func<IClientBase, IObjectPlugin>(InGameTradeMsgs.create));
			base.regCreator("MSG_SKILL", new Func<IClientBase, IObjectPlugin>(InGameSkillMsgs.create));
			base.regCreator("MSG_ITEM", new Func<IClientBase, IObjectPlugin>(InGameItemMsgs.create));
			base.regCreator("MSG_MAP", new Func<IClientBase, IObjectPlugin>(InGameMapMsgs.create));
			base.regCreator("MSG_CHANGELINE", new Func<IClientBase, IObjectPlugin>(InGameChangeLineMsgs.create));
			base.regCreator("MSG_CHAT", new Func<IClientBase, IObjectPlugin>(InGameChatMsgs.create));
			base.regCreator("MSG_ACTIVITY", new Func<IClientBase, IObjectPlugin>(InGameActivityMsgs.create));
			base.regCreator("MSG_WELFARE", new Func<IClientBase, IObjectPlugin>(InGameWelfare.create));
			base.regCreator("MSG_OLAWARD", new Func<IClientBase, IObjectPlugin>(InGameOlAwardMsgs.create));
			base.regCreator("MSG_MAIL", new Func<IClientBase, IObjectPlugin>(InGameMailMsgs.create));
			base.regCreator("MSG_CLAN", new Func<IClientBase, IObjectPlugin>(InGameClansMsgs.create));
			base.regCreator("MSG_LEVEL", new Func<IClientBase, IObjectPlugin>(InGameLevelMsgs.create));
			base.regCreator("MSG_BUDDY", new Func<IClientBase, IObjectPlugin>(InGameBuddyMsgs.create));
			base.regCreator("MSG_TEAM", new Func<IClientBase, IObjectPlugin>(InGameTeamMsgs.create));
			base.regCreator("MSG_LOTTERY", new Func<IClientBase, IObjectPlugin>(InGameLotteryMsgs.create));
			base.regCreator("MSG_AWARD", new Func<IClientBase, IObjectPlugin>(InGameAwardMsgs.create));
			base.regCreator("MSG_ACUPOINT", new Func<IClientBase, IObjectPlugin>(InGameAcupointMsgs.create));
			base.regCreator("MSG_ACHIEVE", new Func<IClientBase, IObjectPlugin>(InGameAchieveMsgs.create));
			base.regCreator("MSG_DMIS", new Func<IClientBase, IObjectPlugin>(InGameDmisMsgs.create));
			base.createAllSingleInst();
			foreach (IObjectPlugin current in this.m_objectPlugins.Values)
			{
				current.init();
			}
			BaseProxy<PlayerInfoProxy>.getInstance();
		}
	}
}
