using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameChangeLineMsgs : MsgProcduresBase
	{
		public InGameChangeLineMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameChangeLineMsgs create(IClientBase m)
		{
			return new InGameChangeLineMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(49u, new NetManager.RPCProcCreator(on_line_info_res.create));
		}

		public void select_line(uint l)
		{
			Variant variant = new Variant();
			variant["idx"] = l;
			base.sendTpkg(7u, variant);
		}

		public void requestLineData()
		{
			Variant msg = new Variant();
			base.sendRPC(49u, msg);
		}
	}
}
