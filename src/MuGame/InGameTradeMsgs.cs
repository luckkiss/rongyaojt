using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameTradeMsgs : MsgProcduresBase
	{
		public InGameTradeMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameTradeMsgs create(IClientBase m)
		{
			return new InGameTradeMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(140u, new NetManager.RPCProcCreator(TradeReqRes.create));
			this.g_mgr.regRPCProcesser(141u, new NetManager.RPCProcCreator(TradeReq.create));
			this.g_mgr.regRPCProcesser(142u, new NetManager.RPCProcCreator(TradwReqConfirmRes.create));
			this.g_mgr.regRPCProcesser(143u, new NetManager.RPCProcCreator(CancelTradeRes.create));
			this.g_mgr.regRPCProcesser(144u, new NetManager.RPCProcCreator(TradeAddItm.create));
			this.g_mgr.regRPCProcesser(145u, new NetManager.RPCProcCreator(TradeLockState.create));
			this.g_mgr.regRPCProcesser(146u, new NetManager.RPCProcCreator(TradeErrMsg.create));
			this.g_mgr.regRPCProcesser(147u, new NetManager.RPCProcCreator(TradeDone.create));
		}

		public void trade_req(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(140u, variant);
		}

		public void trade_req_confirm(uint cid, bool confirm)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			variant["confirm"] = confirm;
			base.sendRPC(142u, variant);
		}

		public void cancel_trade()
		{
			Variant msg = new Variant();
			base.sendRPC(143u, msg);
		}

		public void trade_add_itm(uint gold = 0u, uint yb = 0u, Variant addids = null)
		{
			Variant variant = new Variant();
			bool flag = gold > 0u;
			if (flag)
			{
				variant["gold"] = gold;
			}
			bool flag2 = yb > 0u;
			if (flag2)
			{
				variant["yb"] = yb;
			}
			bool flag3 = addids;
			if (flag3)
			{
				variant["addids"] = addids;
			}
			base.sendRPC(144u, variant);
		}

		public void trade_lock()
		{
			Variant msg = new Variant();
			base.sendRPC(145u, msg);
		}

		public void trade_done()
		{
			Variant msg = new Variant();
			base.sendRPC(147u, msg);
		}
	}
}
