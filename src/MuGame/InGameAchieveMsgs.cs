using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameAchieveMsgs : MsgProcduresBase
	{
		public InGameAchieveMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameAchieveMsgs create(IClientBase m)
		{
			return new InGameAchieveMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(106u, new NetManager.RPCProcCreator(on_achieve_res.create));
		}

		public void GetAchieve(Variant data)
		{
			base.sendRPC(106u, data);
		}
	}
}
