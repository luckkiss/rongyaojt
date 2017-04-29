using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class LGJoinWorld : lgGDBase
	{
		private bool _mapChangeFlag = false;

		private bool _enterFlag = false;

		private bool _firstFlag = false;

		private bool _first_join = true;

		protected int _platid = 0;

		private LGIUIOGLoading ogLoading
		{
			get
			{
				return null;
			}
		}

		private MUlgGDVendor _lgGD_vendor
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_vendorCT;
			}
		}

		private lgGDBuddy _lgGD_Buddy
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_buddyCT;
			}
		}

		private lgGDAchieve _lgGD_Achieve
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_achieveCT;
			}
		}

		private lgGDWorldline _lgGD_Worldline
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_worldlineCT;
			}
		}

		private LGGDAcupoint _lgGD_Acupoint
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_acupointCT;
			}
		}

		private LGGDRmission _lgGD_RMis
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_rmissCT;
			}
		}

		private LGGDLevels _lgGD_levels
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_levelsCT;
			}
		}

		private lgGDVIP _lgGD_VIP
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_vipCT;
			}
		}

		private lgGDOlAward _lgGD_OlAward
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_olawardCT;
			}
		}

		private LGGDActivity _lgGD_Activity
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_activityCT;
			}
		}

		private lgGDItems _lgGD_items
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_itemsCT;
			}
		}

		private lgGDCard lgGD_Card
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_cardCT;
			}
		}

		private lgGDPlyFun _lgGD_PlyFun
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_plyfunCT;
			}
		}

		private MgrPlayerInfo plyinfoMgr
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_MgrPlayerInfoCT;
			}
		}

		private lgGDClans lgGD_clans
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_clansCT;
			}
		}

		private LGGDAward lgGD_Award
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).lgGD_Award;
			}
		}

		private lgGDDmis lgGD_Dmis
		{
			get
			{
				return (this.g_mgr.g_gameM as muLGClient).g_DmisCT;
			}
		}

		public LGJoinWorld(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGJoinWorld(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListenerCL("DATA_JOIN_WORLD", 50u, new Action<GameEvent>(this.onJoinWorld));
			this.g_mgr.g_gameM.addEventListenerCL("LG_LOAD_RESOURCE", 3024u, new Action<GameEvent>(this.onMapChange));
			this.g_mgr.g_sceneM.addEventListener(2165u, new Action<GameEvent>(this.onMapLoadReady));
		}

		private void onMapLoadReady(GameEvent e)
		{
		}

		private void onMapChange(GameEvent e)
		{
			this._mapChangeFlag = true;
			this.tryBroadCastEnterGame();
		}

		private void onJoinWorld(GameEvent e)
		{
			this._enterFlag = true;
			this.tryBroadCastEnterGame();
		}

		private void tryBroadCastEnterGame()
		{
			bool flag = !this._mapChangeFlag || !this._enterFlag;
			if (!flag)
			{
				this._mapChangeFlag = false;
				bool flag2 = !this._firstFlag;
				if (flag2)
				{
					this._firstFlag = true;
					this._joinWorld((this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.m_data);
					bool flag3 = login.instance == null;
					if (flag3)
					{
						(this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI).show_all();
					}
					base.dispatchEvent(GameEvent.Create(3034u, this, null, false));
				}
				else
				{
					base.dispatchEvent(GameEvent.Create(3035u, this, null, false));
				}
				debug.Log("!!tryBroadCastEnterGame!! " + debug.count);
				LGUIMainUIImpl_NEED_REMOVE.TRY_SHOW_MAP_OBJ = true;
			}
		}

		private Variant take_va(string name)
		{
			Variant variant = new Variant();
			variant["name"] = name;
			return variant;
		}

		protected void _joinWorld(Variant data)
		{
			bool flag = this.IsPlatID_P51();
			if (flag)
			{
				string text = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("reportLvlUrl_" + 12);
				bool flag2 = text != null && text != "";
				if (flag2)
				{
				}
			}
			else
			{
				bool flag3 = this.IsPlatID_PPTV();
				if (flag3)
				{
					string text = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("reportMsgUrl_" + 14);
					bool flag4 = text != null && text != "";
					if (flag4)
					{
					}
				}
			}
		}

		private void getPakgeItems()
		{
			this._lgGD_items.pkg_get_items();
		}

		protected bool IsPlatID_P51()
		{
			return 12 == this._platid;
		}

		protected bool IsPlatID_PPTV()
		{
			return 14 == this._platid;
		}

		public void onMapChanged(Variant data)
		{
			bool first_join = this._first_join;
			if (first_join)
			{
				this._first_join = false;
				this._showGame();
			}
			LGIUIMap lGIUIMap = this.g_mgr.g_uiM.getLGUI("mapui") as LGIUIMap;
			lGIUIMap.changeMap(data);
			LGIUIMainUI lGIUIMainUI = this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			lGIUIMainUI.changeMap(data);
			LGIUIMission lGIUIMission = this.g_mgr.g_uiM.getLGUI("mission") as LGIUIMission;
			lGIUIMission.changeMap(data);
		}

		protected void _showGame()
		{
			this.lgGD_Card.get_itmcards(null);
			this.lgGD_clans.GetClanReqs(false);
			this.lgGD_Award.SendGetAwardInfo();
			(this.g_mgr.g_uiM.getLGUI("system") as LGIUISystem).InitSystemSet();
			bool flag = login.instance == null;
			if (flag)
			{
				(this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI).show_all();
			}
		}
	}
}
