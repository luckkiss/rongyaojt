using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameSkillMsgs : MsgProcduresBase
	{
		public InGameSkillMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameSkillMsgs create(IClientBase m)
		{
			return new InGameSkillMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(85u, new NetManager.RPCProcCreator(onGetSkillInfoRes.create));
			this.g_mgr.regRPCProcesser(87u, new NetManager.RPCProcCreator(onSkillUPRes.create));
			this.g_mgr.regRPCProcesser(90u, new NetManager.RPCProcCreator(onSkexpUpRes.create));
		}
	}
}
