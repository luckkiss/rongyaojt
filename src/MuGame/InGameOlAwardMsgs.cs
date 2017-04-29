using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameOlAwardMsgs : MsgProcduresBase
	{
		public InGameOlAwardMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameOlAwardMsgs create(IClientBase m)
		{
			return new InGameOlAwardMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(36u, new NetManager.RPCProcCreator(on_get_olawd_res.create));
			this.g_mgr.regRPCProcesser(44u, new NetManager.RPCProcCreator(get_ol_tm_res.create));
		}

		public void GetOlAward()
		{
			Variant msg = new Variant();
			base.sendRPC(36u, msg);
		}

		public void GetChaOLTm()
		{
			Variant msg = new Variant();
			base.sendRPC(44u, msg);
		}

		public void GetChaOflTmAwd(uint ofl_tm, uint tp)
		{
			Variant variant = new Variant();
			variant["tp"] = tp;
			variant["ofl_tm"] = ofl_tm;
			base.sendRPC(44u, variant);
		}
	}
}
