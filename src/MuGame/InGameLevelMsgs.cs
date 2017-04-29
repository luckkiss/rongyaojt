using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameLevelMsgs : MsgProcduresBase
	{
		public static InGameLevelMsgs instance;

		public InGameLevelMsgs(IClientBase m) : base(m)
		{
			InGameLevelMsgs.instance = this;
		}

		public static InGameLevelMsgs create(IClientBase m)
		{
			return new InGameLevelMsgs(m);
		}

		public override void init()
		{
		}

		public void get_clanter_info(Variant data)
		{
			base.sendRPC(232u, data);
		}

		public void get_lvl_info(Variant data)
		{
			base.sendRPC(233u, data);
		}

		public void get_carrchief_info(Variant data)
		{
			base.sendRPC(234u, data);
		}

		public void get_arena_info(Variant data)
		{
			base.sendRPC(235u, data);
		}

		public void get_lvl_pvpinfo_board(Variant data)
		{
			base.sendRPC(239u, data);
		}

		public void check_in_lvl(Variant data)
		{
			base.sendRPC(240u, data);
		}

		public void create_lvl(Variant data)
		{
			base.sendRPC(241u, data);
		}

		public void enter_lvl(Variant data)
		{
			debug.Log("SCENE_LEVEL ---- 收到服务器创建完副本的消息，开始准备资源播放剧情，地图ID为" + data["mapid"]);
			Variant variant = SvrLevelConfig.instacne.get_level_data(data["ltpid"]);
			bool flag = variant != null;
			if (flag)
			{
				joinWorldInfo joinWorldInfo = this.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
				joinWorldInfo.m_data["mpid"] = variant["map"][0]["id"];
				MapModel.getInstance().curLevelId = data["ltpid"]._uint;
				InterfaceMgr.doCommandByLua("MapModel:getInstance().getcurLevelId", "model/MapModel", new object[]
				{
					data["ltpid"]._uint
				});
				MapModel.getInstance().curDiff = data["diff_lvl"]._uint;
				GRMap.LEVEL_PLOT_ID = data["ltpid"]._int;
				debug.Log("!!sendRPC(PKG_NAME.C2S_ENTER_LVL_RES, data)2!!");
				base.sendRPC(242u, data);
				LGLoadResource._instance.m_nLoaded_MapID = -1;
			}
			else
			{
				MapModel.getInstance().curLevelId = 0u;
				InterfaceMgr.doCommandByLua("MapModel:getInstance().getcurLevelId", "model/MapModel", new object[]
				{
					0
				});
				debug.Log("!!sendRPC(PKG_NAME.C2S_ENTER_LVL_RES, data)2!!");
				base.sendRPC(242u, data);
				LGLoadResource._instance.m_nLoaded_MapID = -1;
			}
		}

		public void get_associate_lvls(Variant data)
		{
			base.sendRPC(243u, data);
		}

		public void get_lvl_cnt_info(Variant data)
		{
			base.sendRPC(244u, data);
		}

		public void get_lvl_prize(Variant data)
		{
			base.sendRPC(245u, data);
		}

		public void leave_lvl()
		{
			base.sendRPC(246u, new Variant());
		}

		public void close_lvl(Variant data)
		{
			base.sendRPC(247u, data);
		}

		public void GetLvlmisInfo()
		{
			base.sendRPC(117u, new Variant());
		}
	}
}
