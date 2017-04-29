using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameBuddyMsgs : MsgProcduresBase
	{
		public InGameBuddyMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameBuddyMsgs create(IClientBase m)
		{
			return new InGameBuddyMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(170u, new NetManager.RPCProcCreator(on_add_buddy_res.create));
			this.g_mgr.regRPCProcesser(171u, new NetManager.RPCProcCreator(on_rmv_buddy_res.create));
			this.g_mgr.regRPCProcesser(172u, new NetManager.RPCProcCreator(on_get_buddy_res.create));
			this.g_mgr.regRPCProcesser(173u, new NetManager.RPCProcCreator(on_become_buddy.create));
			this.g_mgr.regRPCProcesser(174u, new NetManager.RPCProcCreator(on_get_buddy_ol_res.create));
		}

		public void add_buddy(int cid, uint type)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["type"] = type;
			base.sendRPC(170u, variant);
		}

		public void rmv_buddy(int cid, uint type)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["type"] = type;
			base.sendRPC(171u, variant);
		}

		public void get_buddy(uint type)
		{
			Variant variant = new Variant();
			variant["type"] = type;
			base.sendRPC(172u, variant);
		}

		public void get_buddy_ol(uint type)
		{
			Variant variant = new Variant();
			variant["type"] = type;
			base.sendRPC(174u, variant);
		}
	}
}
