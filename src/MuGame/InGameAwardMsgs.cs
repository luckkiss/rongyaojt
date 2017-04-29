using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameAwardMsgs : MsgProcduresBase
	{
		public InGameAwardMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameAwardMsgs create(IClientBase m)
		{
			return new InGameAwardMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(100u, new NetManager.RPCProcCreator(on_award_res.create));
		}

		public void GetAward(Variant data)
		{
			base.sendRPC(100u, data);
		}

		public void GetMlineMisPrize(int misid)
		{
			Variant variant = new Variant();
			variant["tp"] = 23;
			variant["misid"] = misid;
			base.sendRPC(100u, variant);
		}

		public void sendPvipMsg(Variant data)
		{
			base.sendRPC(95u, data);
		}
	}
}
