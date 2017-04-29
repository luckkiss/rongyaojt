using GameFramework;
using System;

namespace MuGame
{
	public class muCLientConfig : ClientConfig
	{
		public static muCLientConfig instance;

		public ClientAIConf localAI
		{
			get
			{
				return base.getObject("ai") as ClientAIConf;
			}
		}

		public ClientGeneralConf localGeneral
		{
			get
			{
				return base.getObject("general") as ClientGeneralConf;
			}
		}

		public ClientGrdConfig localGrd
		{
			get
			{
				return base.getObject("grd") as ClientGrdConfig;
			}
		}

		public ClientGuideConf localGuild
		{
			get
			{
				return base.getObject("guide") as ClientGuideConf;
			}
		}

		public ClientImgResConf localImgRes
		{
			get
			{
				return base.getObject("imgres") as ClientImgResConf;
			}
		}

		public ClientItemsConf localItems
		{
			get
			{
				return base.getObject("items") as ClientItemsConf;
			}
		}

		public ClientMarketConf localMarket
		{
			get
			{
				return base.getObject("market") as ClientMarketConf;
			}
		}

		public ClientOutGameConf localOutGame
		{
			get
			{
				return base.getObject("outgame") as ClientOutGameConf;
			}
		}

		public ClientShieldConf localShield
		{
			get
			{
				return base.getObject("shield") as ClientShieldConf;
			}
		}

		public ClientSkillConf localSkill
		{
			get
			{
				return base.getObject("skill") as ClientSkillConf;
			}
		}

		public ClientSystemConf localSystem
		{
			get
			{
				return base.getObject("system") as ClientSystemConf;
			}
		}

		public ClientSystemOpenConf localSystemOpen
		{
			get
			{
				return base.getObject("systemopen") as ClientSystemOpenConf;
			}
		}

		public ClientTriggerConf localTrigger
		{
			get
			{
				return base.getObject("trigger") as ClientTriggerConf;
			}
		}

		public ClientVipConf localVip
		{
			get
			{
				return base.getObject("vip") as ClientVipConf;
			}
		}

		public ClientFWGameConf localFWGame
		{
			get
			{
				return base.getObject("fwgame") as ClientFWGameConf;
			}
		}

		public ClientActivityViewConf localActivityView
		{
			get
			{
				return base.getObject("activityview") as ClientActivityViewConf;
			}
		}

		public SvrMapConfig svrMapsConf
		{
			get
			{
				return base.getObject("SvrMap") as SvrMapConfig;
			}
		}

		public SvrMissionConfig svrMisConf
		{
			get
			{
				return base.getObject("SvrMission") as SvrMissionConfig;
			}
		}

		public SvrNPCConfig svrNpcConf
		{
			get
			{
				return base.getObject("SvrNPC") as SvrNPCConfig;
			}
		}

		public SvrGeneralConfig svrGeneralConf
		{
			get
			{
				return base.getObject("SvrGeneral") as SvrGeneralConfig;
			}
		}

		public SvrMonsterConfig svrMonsterConf
		{
			get
			{
				return base.getObject("SvrMonsters") as SvrMonsterConfig;
			}
		}

		public SvrItemConfig svrItemConf
		{
			get
			{
				return base.getObject("SvrItem") as SvrItemConfig;
			}
		}

		public SvrSkillConfig svrSkillConf
		{
			get
			{
				return base.getObject("SvrSkill") as SvrSkillConfig;
			}
		}

		public SvrMarketConfig svrMarketConf
		{
			get
			{
				return base.getObject("SvrMarket") as SvrMarketConfig;
			}
		}

		public SvrMeriConfig svrMeriConf
		{
			get
			{
				return base.getObject("SvrMeri") as SvrMeriConfig;
			}
		}

		public SvrLevelConfig svrLevelConf
		{
			get
			{
				return base.getObject("SvrLevel") as SvrLevelConfig;
			}
		}

		public SvrCarrLvlConfig svrCarrLvlConf
		{
			get
			{
				return base.getObject("SvrCarrLvl") as SvrCarrLvlConfig;
			}
		}

		public muCLientConfig(gameMain m) : base(m)
		{
		}

		protected override void onInit()
		{
			muCLientConfig.instance = this;
			base.regCreator("general", new Func<IClientBase, IObjectPlugin>(ClientGeneralConf.create));
			base.regCreator("grd", new Func<IClientBase, IObjectPlugin>(ClientGrdConfig.create));
			base.regCreator("outgame", new Func<IClientBase, IObjectPlugin>(ClientOutGameConf.create));
			base.regCreator("monster", new Func<IClientBase, IObjectPlugin>(MonsterConfig.create));
			base.regCreator("skill", new Func<IClientBase, IObjectPlugin>(SkillConf.create));
			base.regCreator("SvrMap", new Func<IClientBase, IObjectPlugin>(SvrMapConfig.create));
			base.regCreator("SvrNPC", new Func<IClientBase, IObjectPlugin>(SvrNPCConfig.create));
			base.regCreator("SvrLevel", new Func<IClientBase, IObjectPlugin>(SvrLevelConfig.create));
			base.createInst("grd", true);
		}
	}
}
