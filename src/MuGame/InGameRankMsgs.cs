using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameRankMsgs : MsgProcduresBase
	{
		public InGameRankMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameRankMsgs create(IClientBase m)
		{
			return new InGameRankMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(254u, new NetManager.RPCProcCreator(GetRankInfo.create));
		}

		public void get_rank_info(Variant data)
		{
			Variant variant = new Variant();
			variant["rnk_tp"] = data["tp"];
			variant["begin_idx"] = data["begin_idx"];
			bool flag = data.ContainsKey("end_idx");
			if (flag)
			{
				variant["end_idx"] = data["end_idx"];
			}
			bool flag2 = data.ContainsKey("rnk_cnt");
			if (flag2)
			{
				variant["rnk_cnt"] = data["rnk_cnt"];
			}
			bool flag3 = data.ContainsKey("arenaid");
			if (flag3)
			{
				variant["arenaid"] = data["arenaid"];
			}
			bool flag4 = data.ContainsKey("sub_tp") && data["sub_tp"];
			if (flag4)
			{
				variant["sub_tp"] = data["sub_tp"];
			}
			base.sendRPC(254u, variant);
		}

		public void get_rank_info_with_carr(int tp, int begin_idx, int end_idx, int rnk_carr)
		{
			Variant variant = new Variant();
			variant["rnk_tp"] = tp;
			variant["begin_idx"] = begin_idx;
			variant["end_idx"] = end_idx;
			variant["rnk_carr"] = rnk_carr;
			base.sendRPC(254u, variant);
		}
	}
}
