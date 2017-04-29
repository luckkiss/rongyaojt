using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameChatMsgs : MsgProcduresBase
	{
		public InGameChatMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameChatMsgs create(IClientBase m)
		{
			return new InGameChatMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(160u, new NetManager.RPCProcCreator(on_chat_msg.create));
			this.g_mgr.regRPCProcesser(161u, new NetManager.RPCProcCreator(on_chat_msg_res.create));
		}

		public void chat_msg(uint type, string msg, uint cid = 0u, bool withtid = false, uint backshowtp = 0u)
		{
			Variant variant = new Variant();
			variant["tp"] = type;
			variant["msg"] = msg;
			bool flag = cid > 0u;
			if (flag)
			{
				variant["cid"] = cid;
			}
			if (withtid)
			{
			}
			bool flag2 = backshowtp != 1u;
			if (flag2)
			{
				lgGDChat g_chatCT = (this.g_mgr.g_gameM as muLGClient).g_chatCT;
				g_chatCT.push(variant);
			}
			base.sendRPC(160u, variant);
		}
	}
}
