using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameLotteryMsgs : MsgProcduresBase
	{
		public InGameLotteryMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameLotteryMsgs create(IClientBase m)
		{
			return new InGameLotteryMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(162u, new NetManager.RPCProcCreator(on_add_self_lottery_log.create));
			this.g_mgr.regRPCProcesser(163u, new NetManager.RPCProcCreator(on_other_lottery_log.create));
		}

		public void lottery(uint lolvl, int cnt, int usetp)
		{
			Variant variant = new Variant();
			variant["lolvl"] = lolvl;
			variant["cnt"] = cnt;
			variant["usetp"] = usetp;
			base.sendRPC(162u, variant);
		}

		public void get_lottery_log(uint type, int curid)
		{
			Variant variant = new Variant();
			variant["tp"] = type;
			variant["curid"] = curid;
			base.sendRPC(163u, variant);
		}
	}
}
