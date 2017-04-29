using Cross;
using GameFramework;
using System;
using System.Runtime.CompilerServices;

namespace MuGame
{
	public class InGameClansMsgs : MsgProcduresBase
	{
		private class on_create_clan_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 210u;
				}
			}

			public static InGameClansMsgs.on_create_clan_res create()
			{
				return new InGameClansMsgs.on_create_clan_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_join_clan_req : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 211u;
				}
			}

			public static InGameClansMsgs.on_join_clan_req create()
			{
				return new InGameClansMsgs.on_join_clan_req();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.add_clan_req(this.msgData);
			}
		}

		private class on_pl_join_clan : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 212u;
				}
			}

			public static InGameClansMsgs.on_pl_join_clan create()
			{
				return new InGameClansMsgs.on_pl_join_clan();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_join_clan : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 213u;
				}
			}

			public static InGameClansMsgs.on_join_clan create()
			{
				return new InGameClansMsgs.on_join_clan();
			}

			protected override void _onProcess()
			{
				Variant variant = new Variant();
				variant["clanid"] = this.msgData["id"];
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_selfPlayer.updateNetData(variant);
				((this.session as ClientSession).g_mgr as muNetCleint).joinWorldInfoInst.mainPlayerInfo["clanid"] = this.msgData["id"];
				((this.session as ClientSession).g_mgr as muNetCleint).igClanMsgs.get_clan_selfinfo();
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_missionCT.clan_info_change();
				Variant mainPlayerInfo = ((this.session as ClientSession).g_mgr as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
			}
		}

		private class on_pl_leave_clan : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 214u;
				}
			}

			public static InGameClansMsgs.on_pl_leave_clan create()
			{
				return new InGameClansMsgs.on_pl_leave_clan();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_clan_donate_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 215u;
				}
			}

			public static InGameClansMsgs.on_clan_donate_res create()
			{
				return new InGameClansMsgs.on_clan_donate_res();
			}

			protected override void _onProcess()
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(215u, this, this.msgData, false));
				bool flag = this.msgData.ContainsKey("gld");
				if (flag)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.add_cur_clanagld(this.msgData["gld"]);
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_gold(this.msgData["gld"]);
				}
				bool flag2 = this.msgData.ContainsKey("yb");
				if (flag2)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.add_cur_clanayb(this.msgData["yb"]);
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_yb(this.msgData["yb"], true);
				}
				bool flag3 = this.msgData.ContainsKey("itms_donate");
				if (flag3)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.set_items_donate(this.msgData["itms_donate"]);
				}
				bool flag4 = this.msgData.ContainsKey("clang_add");
				if (flag4)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.mode_clang(this.msgData["clang_add"]);
				}
				bool flag5 = this.msgData.ContainsKey("clana_add");
				if (flag5)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.mode_clana(this.msgData["clana_add"]);
				}
				bool flag6 = this.msgData.ContainsKey("day_record");
				if (flag6)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.SetClanDayRecord(this.msgData["day_record"]);
				}
				bool flag7 = this.msgData.ContainsKey("clan_pt");
				if (flag7)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.mode_clanpt(this.msgData["clan_pt"]);
				}
			}
		}

		private class on_clan_info_change : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 216u;
				}
			}

			public static InGameClansMsgs.on_clan_info_change create()
			{
				return new InGameClansMsgs.on_clan_info_change();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_change_ply_clanc : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 217u;
				}
			}

			public static InGameClansMsgs.on_change_ply_clanc create()
			{
				return new InGameClansMsgs.on_change_ply_clanc();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.change_ply_clanc(this.msgData);
			}
		}

		private class on_clan_dismiss : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 218u;
				}
			}

			public static InGameClansMsgs.on_clan_dismiss create()
			{
				return new InGameClansMsgs.on_clan_dismiss();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_get_clan_info_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 219u;
				}
			}

			public static InGameClansMsgs.on_get_clan_info_res create()
			{
				return new InGameClansMsgs.on_get_clan_info_res();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.get_clan_info_res(this.msgData);
			}
		}

		private class on_get_clans_list_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 220u;
				}
			}

			public static InGameClansMsgs.on_get_clans_list_res create()
			{
				return new InGameClansMsgs.on_get_clans_list_res();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.on_clan_list(this.msgData);
			}
		}

		private class on_query_clan_id_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 221u;
				}
			}

			public static InGameClansMsgs.on_query_clan_id_res create()
			{
				return new InGameClansMsgs.on_query_clan_id_res();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.on_query_clan_id_res(this.msgData);
			}
		}

		private class on_get_clan_selfinfo_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 222u;
				}
			}

			public static InGameClansMsgs.on_get_clan_selfinfo_res create()
			{
				return new InGameClansMsgs.on_get_clan_selfinfo_res();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.set_clan_self_info(this.msgData);
			}
		}

		private class on_get_clanpls_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 223u;
				}
			}

			public static InGameClansMsgs.on_get_clanpls_res create()
			{
				return new InGameClansMsgs.on_get_clanpls_res();
			}

			protected override void _onProcess()
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.on_clan_plys_list(this.msgData);
			}
		}

		private class on_query_clinfo_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 224u;
				}
			}

			public static InGameClansMsgs.on_query_clinfo_res create()
			{
				return new InGameClansMsgs.on_query_clinfo_res();
			}

			protected override void _onProcess()
			{
				bool flag = this.msgData["res"] == 1;
				if (flag)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT.on_query_clinfo_res(this.msgData);
				}
				else
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_levelsCT.OnClanError(this.msgData["id"]);
				}
			}
		}

		private class on_join_clan_req_res : RPCMsgProcesser
		{
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				public static readonly InGameClansMsgs.on_join_clan_req_res.<>c <>9 = new InGameClansMsgs.on_join_clan_req_res.<>c();

				public static Action<uint, Variant> <>9__4_0;

				internal void <_join_clan_req_res_msg>b__4_0(uint _clanid, Variant data)
				{
				}
			}

			public override uint msgID
			{
				get
				{
					return 225u;
				}
			}

			public static InGameClansMsgs.on_join_clan_req_res create()
			{
				return new InGameClansMsgs.on_join_clan_req_res();
			}

			protected override void _onProcess()
			{
			}

			private void _join_clan_req_res_msg(muLGClient lgClient, uint clanid)
			{
				lgGDClans arg_40_0 = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_clansCT;
				Action<uint, Variant> arg_40_2;
				if ((arg_40_2 = InGameClansMsgs.on_join_clan_req_res.<>c.<>9__4_0) == null)
				{
					arg_40_2 = (InGameClansMsgs.on_join_clan_req_res.<>c.<>9__4_0 = new Action<uint, Variant>(InGameClansMsgs.on_join_clan_req_res.<>c.<>9.<_join_clan_req_res_msg>b__4_0));
				}
				arg_40_0.get_claninfo_by_clanid(clanid, arg_40_2);
			}
		}

		private class on_clan_msg : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 226u;
				}
			}

			public static InGameClansMsgs.on_clan_msg create()
			{
				return new InGameClansMsgs.on_clan_msg();
			}

			protected override void _onProcess()
			{
			}
		}

		public InGameClansMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameClansMsgs create(IClientBase m)
		{
			return new InGameClansMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(210u, new NetManager.RPCProcCreator(InGameClansMsgs.on_create_clan_res.create));
			this.g_mgr.regRPCProcesser(212u, new NetManager.RPCProcCreator(InGameClansMsgs.on_pl_join_clan.create));
			this.g_mgr.regRPCProcesser(213u, new NetManager.RPCProcCreator(InGameClansMsgs.on_join_clan.create));
			this.g_mgr.regRPCProcesser(214u, new NetManager.RPCProcCreator(InGameClansMsgs.on_pl_leave_clan.create));
			this.g_mgr.regRPCProcesser(215u, new NetManager.RPCProcCreator(InGameClansMsgs.on_clan_donate_res.create));
			this.g_mgr.regRPCProcesser(216u, new NetManager.RPCProcCreator(InGameClansMsgs.on_clan_info_change.create));
			this.g_mgr.regRPCProcesser(217u, new NetManager.RPCProcCreator(InGameClansMsgs.on_change_ply_clanc.create));
			this.g_mgr.regRPCProcesser(218u, new NetManager.RPCProcCreator(InGameClansMsgs.on_clan_dismiss.create));
			this.g_mgr.regRPCProcesser(219u, new NetManager.RPCProcCreator(InGameClansMsgs.on_get_clan_info_res.create));
			this.g_mgr.regRPCProcesser(220u, new NetManager.RPCProcCreator(InGameClansMsgs.on_get_clans_list_res.create));
			this.g_mgr.regRPCProcesser(221u, new NetManager.RPCProcCreator(InGameClansMsgs.on_query_clan_id_res.create));
			this.g_mgr.regRPCProcesser(222u, new NetManager.RPCProcCreator(InGameClansMsgs.on_get_clan_selfinfo_res.create));
			this.g_mgr.regRPCProcesser(223u, new NetManager.RPCProcCreator(InGameClansMsgs.on_get_clanpls_res.create));
			this.g_mgr.regRPCProcesser(224u, new NetManager.RPCProcCreator(InGameClansMsgs.on_query_clinfo_res.create));
			this.g_mgr.regRPCProcesser(225u, new NetManager.RPCProcCreator(InGameClansMsgs.on_join_clan_req_res.create));
			this.g_mgr.regRPCProcesser(226u, new NetManager.RPCProcCreator(InGameClansMsgs.on_clan_msg.create));
		}

		public void create_clan(uint itmtpid, string name, string info)
		{
			Variant variant = new Variant();
			variant["itmtpid"] = itmtpid;
			variant["name"] = name;
			variant["info"] = info;
			base.sendRPC(210u, variant);
		}

		public void join_clan_req(uint clid)
		{
			Variant variant = new Variant();
			variant["clid"] = clid;
			base.sendRPC(211u, variant);
		}

		public void aprov_join_clan(uint cid, bool approved)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["approved"] = approved;
			base.sendRPC(212u, variant);
		}

		public void leave_clan()
		{
			Variant msg = new Variant();
			base.sendRPC(213u, msg);
		}

		public void clan_donate(int gld = -1, int yb = -1, Variant itms = null)
		{
			bool flag = gld < 0 && yb < 0 && (itms == null || itms.Length <= 0);
			if (!flag)
			{
				Variant variant = new Variant();
				bool flag2 = gld > 0;
				if (flag2)
				{
					variant["gld"] = gld;
				}
				bool flag3 = yb > 0;
				if (flag3)
				{
					variant["yb"] = yb;
				}
				bool flag4 = itms && itms.Length > 0;
				if (flag4)
				{
					variant["itms"] = itms;
				}
				base.sendRPC(214u, variant);
			}
		}

		public void up_clan_level()
		{
			Variant variant = new Variant();
			variant["tartp"] = 1;
			base.sendRPC(215u, variant);
		}

		public void up_clan_tech_itm(uint techid, uint itmtpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["itmtpid"] = itmtpid;
			Variant variant2 = new Variant();
			variant2["tp"] = 2;
			variant2["techproc"] = variant;
			base.sendRPC(215u, variant2);
		}

		public void up_clan_tech_yb(uint techid, uint yb)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["techproc"] = yb;
			Variant variant2 = new Variant();
			variant2["tp"] = 2;
			variant2["techproc"] = variant;
			base.sendRPC(215u, variant2);
		}

		public void up_clan_tech_gld(uint techid, uint gld)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["techproc"] = gld;
			Variant variant2 = new Variant();
			variant2["tp"] = 2;
			variant2["techproc"] = variant;
			base.sendRPC(215u, variant2);
		}

		public void use_clan_tech(uint techid)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["id"] = techid;
			base.sendRPC(215u, variant);
		}

		public void fetch_clan_awd()
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			base.sendRPC(215u, variant);
		}

		public void change_ply_clanc(uint cid, uint clanc)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["clanc"] = clanc;
			base.sendRPC(216u, variant);
		}

		public void kick_clan_ply(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(217u, variant);
		}

		public void change_clan_leader(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(218u, variant);
		}

		public void change_clan_info(uint infotp, string info)
		{
			Variant variant = new Variant();
			variant["infotp"] = infotp;
			variant["info"] = info;
			base.sendRPC(219u, variant);
		}

		public void ChangeClanDirectJoin(int flag)
		{
			Variant variant = new Variant();
			variant["infotp"] = 3;
			variant["direct_join"] = flag;
			base.sendRPC(219u, variant);
		}

		public void dismis_clan()
		{
			Variant msg = new Variant();
			base.sendRPC(220u, msg);
		}

		public void get_clan_base_info()
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			base.sendRPC(221u, variant);
		}

		public void get_clan_donate_logs()
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			base.sendRPC(221u, variant);
		}

		public void get_clan_logs(float tm)
		{
			bool flag = tm == 0f;
			if (flag)
			{
				Variant variant = new Variant();
				variant["tp"] = 3;
				base.sendRPC(221u, variant);
			}
			else
			{
				Variant variant2 = new Variant();
				variant2["tp"] = 3;
				variant2["tm"] = tm;
				base.sendRPC(221u, variant2);
			}
		}

		public void get_clan_reqs()
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			base.sendRPC(221u, variant);
		}

		public void get_clan_tech_info()
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			base.sendRPC(221u, variant);
		}

		public void get_clan_gmis_info()
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			base.sendRPC(221u, variant);
		}

		public void get_sub_clan_info()
		{
			Variant variant = new Variant();
			variant["tp"] = 7;
			base.sendRPC(221u, variant);
		}

		public void get_clan_itm_trig_info()
		{
			Variant variant = new Variant();
			variant["tp"] = 8;
			base.sendRPC(221u, variant);
		}

		public void get_clans_list(uint begin_idx, uint end_idx)
		{
			Variant variant = new Variant();
			variant["begin_idx"] = begin_idx;
			variant["end_idx"] = end_idx;
			base.sendRPC(222u, variant);
		}

		public void query_clan_id(string name)
		{
			Variant variant = new Variant();
			variant["name"] = name;
			base.sendRPC(223u, variant);
		}

		public void get_clan_selfinfo()
		{
			Variant msg = new Variant();
			base.sendRPC(224u, msg);
		}

		public void get_clanpls(uint begin_idx, uint end_idx)
		{
			Variant variant = new Variant();
			variant["begin_idx"] = begin_idx;
			variant["end_idx"] = end_idx;
			base.sendRPC(225u, variant);
		}

		public void query_clinfo(uint clid, uint errchk)
		{
			Variant variant = new Variant();
			variant["clid"] = clid;
			variant["errchk"] = errchk;
			base.sendRPC(226u, variant);
		}

		public void clan_call(uint calltp, string msg)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["calltp"] = calltp;
			variant["msg"] = msg;
			base.sendRPC(227u, variant);
		}

		public void clan_declare(uint tclid)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["tclid"] = tclid;
			base.sendRPC(227u, variant);
		}

		public void clan_get_declare_list()
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			base.sendRPC(227u, variant);
		}

		public void fire_clan_leader(uint pcid, uint item_tpid)
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			variant["cid"] = pcid;
			variant["itmtpid"] = item_tpid;
			base.sendRPC(227u, variant);
		}

		public void clan_discharge_sub_clan()
		{
		}
	}
}
