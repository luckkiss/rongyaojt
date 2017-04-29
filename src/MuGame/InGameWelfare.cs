using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameWelfare : MsgProcduresBase
	{
		public InGameWelfare(IClientBase m) : base(m)
		{
		}

		public static InGameWelfare create(IClientBase m)
		{
			return new InGameWelfare(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(36u, new NetManager.RPCProcCreator(get_olawd_res.create));
		}

		public void requestGrowLvlAwd(int lvl)
		{
			Variant variant = new Variant();
			variant[""] = lvl;
			base.sendRPC(24u, variant);
		}
	}
}
