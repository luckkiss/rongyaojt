using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameActivityMsgs : MsgProcduresBase
	{
		public InGameActivityMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameActivityMsgs create(IClientBase m)
		{
			return new InGameActivityMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(45u, new NetManager.RPCProcCreator(get_rect_res.create));
		}

		public void get_firstracttm()
		{
			Variant variant = new Variant();
			variant["tp"] = 0;
			variant["ractid"] = 0;
			base.sendRPC(45u, variant);
		}

		public void get_self_idx(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_rank_info(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_rank_list(uint ractid, uint begin_idx, uint end_idx)
		{
			Variant variant = new Variant();
			variant["tp"] = 3;
			variant["ractid"] = ractid;
			variant["begin_idx"] = begin_idx;
			variant["end_idx"] = end_idx;
			base.sendRPC(45u, variant);
		}

		public void get_dalay_award(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_total_award(uint ractid, int awdid = 0)
		{
			Variant variant = new Variant();
			variant["tp"] = 5;
			variant["ractid"] = ractid;
			variant["awdid"] = awdid;
			base.sendRPC(45u, variant);
		}

		public void flush_normal_act(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_normal_act_awds(uint ractid, uint tid, uint awdid = 0u)
		{
			Variant variant = new Variant();
			variant["tp"] = 7;
			variant["ractid"] = ractid;
			variant["tid"] = tid;
			variant["awdid"] = awdid;
			base.sendRPC(45u, variant);
		}

		public void flush_clan_act(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_clan_act_awds(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 9;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void GetChargeRankInfo(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 10;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}

		public void get_rank_act_awds(uint ractid, uint awdid)
		{
			Variant variant = new Variant();
			variant["tp"] = 11;
			variant["ractid"] = ractid;
			variant["awdid"] = awdid;
			base.sendRPC(45u, variant);
		}

		public void getFestivalAwd(uint ractid, uint tid)
		{
			Variant variant = new Variant();
			variant["tp"] = 13;
			variant["ractid"] = ractid;
			variant["tid"] = tid;
			base.sendRPC(45u, variant);
		}

		public void GetFestivaldData(uint ractid)
		{
			Variant variant = new Variant();
			variant["tp"] = 12;
			variant["ractid"] = ractid;
			base.sendRPC(45u, variant);
		}
	}
}
