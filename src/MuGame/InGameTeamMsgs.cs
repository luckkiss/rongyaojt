using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameTeamMsgs : MsgProcduresBase
	{
		private class on_team_setting : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 119u;
				}
			}

			public static InGameTeamMsgs.on_team_setting create()
			{
				return new InGameTeamMsgs.on_team_setting();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_create_team_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 120u;
				}
			}

			public static InGameTeamMsgs.on_create_team_res create()
			{
				return new InGameTeamMsgs.on_create_team_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_join_team_req : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 121u;
				}
			}

			public static InGameTeamMsgs.on_join_team_req create()
			{
				return new InGameTeamMsgs.on_join_team_req();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_cancel_join_team_req : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 122u;
				}
			}

			public static InGameTeamMsgs.on_cancel_join_team_req create()
			{
				return new InGameTeamMsgs.on_cancel_join_team_req();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_refuse_join_team_req : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 123u;
				}
			}

			public static InGameTeamMsgs.on_refuse_join_team_req create()
			{
				return new InGameTeamMsgs.on_refuse_join_team_req();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_leave_team : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 124u;
				}
			}

			public static InGameTeamMsgs.on_leave_team create()
			{
				return new InGameTeamMsgs.on_leave_team();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_invite_join_team : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 125u;
				}
			}

			public static InGameTeamMsgs.on_invite_join_team create()
			{
				return new InGameTeamMsgs.on_invite_join_team();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_cancel_invite_join_team : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 126u;
				}
			}

			public static InGameTeamMsgs.on_cancel_invite_join_team create()
			{
				return new InGameTeamMsgs.on_cancel_invite_join_team();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_refuse_invite_join_team : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 127u;
				}
			}

			public static InGameTeamMsgs.on_refuse_invite_join_team create()
			{
				return new InGameTeamMsgs.on_refuse_invite_join_team();
			}

			protected override void _onProcess()
			{
				LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				string text = LanguagePack.getLanguageText("UIMdlgTeam", "refuseTeamAsk");
				text = DebugTrace.Printf(text, new string[]
				{
					this.msgData["name"]
				});
				lGIUIMainUI.systemmsg(text, 4u);
			}
		}

		private class on_publish_team_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 128u;
				}
			}

			public static InGameTeamMsgs.on_publish_team_res create()
			{
				return new InGameTeamMsgs.on_publish_team_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_drop_team_dpitm : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 129u;
				}
			}

			public static InGameTeamMsgs.on_drop_team_dpitm create()
			{
				return new InGameTeamMsgs.on_drop_team_dpitm();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_change_team_leader : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 130u;
				}
			}

			public static InGameTeamMsgs.on_change_team_leader create()
			{
				return new InGameTeamMsgs.on_change_team_leader();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_get_pubed_team_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 131u;
				}
			}

			public static InGameTeamMsgs.on_get_pubed_team_res create()
			{
				return new InGameTeamMsgs.on_get_pubed_team_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_team_roll_dpitm : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 132u;
				}
			}

			public static InGameTeamMsgs.on_get_pubed_team_res create()
			{
				return new InGameTeamMsgs.on_get_pubed_team_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_join_team_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 133u;
				}
			}

			public static InGameTeamMsgs.on_join_team_res create()
			{
				return new InGameTeamMsgs.on_join_team_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_player_join_team : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 134u;
				}
			}

			public static InGameTeamMsgs.on_player_join_team create()
			{
				return new InGameTeamMsgs.on_player_join_team();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_room_operate_res : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 135u;
				}
			}

			public static InGameTeamMsgs.on_room_operate_res create()
			{
				return new InGameTeamMsgs.on_room_operate_res();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_team_change : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 136u;
				}
			}

			public static InGameTeamMsgs.on_team_change create()
			{
				return new InGameTeamMsgs.on_team_change();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_member_online_change : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 137u;
				}
			}

			public static InGameTeamMsgs.on_member_online_change create()
			{
				return new InGameTeamMsgs.on_member_online_change();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_member_att_change : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 138u;
				}
			}

			public static InGameTeamMsgs.on_member_att_change create()
			{
				return new InGameTeamMsgs.on_member_att_change();
			}

			protected override void _onProcess()
			{
			}
		}

		private class on_team_pick_dpitm : RPCMsgProcesser
		{
			public override uint msgID
			{
				get
				{
					return 139u;
				}
			}

			public static InGameTeamMsgs.on_team_pick_dpitm create()
			{
				return new InGameTeamMsgs.on_team_pick_dpitm();
			}

			protected override void _onProcess()
			{
			}
		}

		public InGameTeamMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameTeamMsgs create(IClientBase m)
		{
			return new InGameTeamMsgs(m);
		}

		public override void init()
		{
		}

		public void room_operate(Variant data)
		{
			base.sendRPC(135u, data);
		}

		public void set_dir_join(bool b)
		{
			Variant variant = new Variant();
			variant["dir_join"] = b;
			base.sendRPC(119u, variant);
		}

		public void set_memb_inv(bool b)
		{
			Variant variant = new Variant();
			variant["memb_inv"] = b;
			base.sendRPC(119u, variant);
		}

		public void create_team()
		{
			Variant msg = new Variant();
			base.sendRPC(120u, msg);
		}

		public void join_team_req(uint team_id)
		{
			Variant variant = new Variant();
			variant["team_id"] = team_id;
			base.sendRPC(121u, variant);
		}

		public void cancel_join_team_req(uint team_id)
		{
			Variant variant = new Variant();
			variant["team_id"] = team_id;
			base.sendRPC(122u, variant);
		}

		public void cofirm_join_team_req(uint cid, bool approved)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["approved"] = approved;
			base.sendRPC(123u, variant);
		}

		public void leave_team()
		{
			Variant msg = new Variant();
			base.sendRPC(124u, msg);
		}

		public void invite_join_team(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(125u, variant);
		}

		public void cancel_invite_join_team(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(126u, variant);
		}

		public void cofirm_invite_join_team(uint cid, bool cofirmed)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["cofirmed"] = cofirmed;
			base.sendRPC(127u, variant);
		}

		public void publish_team(int pub, string teaminfo = null)
		{
			Variant variant = new Variant();
			variant["pub"] = pub;
			variant["teaminfo"] = teaminfo;
			base.sendRPC(128u, variant);
		}

		public void kick_team_member(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(129u, variant);
		}

		public void change_team_leader(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(130u, variant);
		}

		public void get_pubed_team(int sidx, int eidx)
		{
			Variant variant = new Variant();
			variant["idx_begin"] = sidx;
			variant["idx_end"] = eidx;
			base.sendRPC(131u, variant);
		}

		public void dismiss_team()
		{
			Variant msg = new Variant();
			base.sendRPC(132u, msg);
		}
	}
}
