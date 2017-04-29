using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameVIPMsgs : MsgProcduresBase
	{
		public InGameVIPMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameVIPMsgs create(IClientBase m)
		{
			return new InGameVIPMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(46u, new NetManager.RPCProcCreator(get_vip_res.create));
		}

		public void get_vip(uint awdtype = 0u, uint lvl = 0u)
		{
			bool flag = awdtype == 0u;
			if (flag)
			{
				Variant msg = new Variant();
				base.sendRPC(46u, msg);
			}
			else
			{
				Variant variant = new Variant();
				variant["awdtp"] = awdtype;
				variant["lvl"] = lvl;
				base.sendRPC(46u, variant);
			}
		}

		public void send_get_pvip(int awdtp = 0)
		{
			bool flag = awdtp != 0;
			if (flag)
			{
				Variant variant = new Variant();
				variant["pvip_awdtp"] = awdtp;
				base.sendRPC(46u, variant);
			}
		}

		public void GetPvipPrizeForGrow(uint lmawd_id)
		{
			Variant variant = new Variant();
			variant["pvip_awdtp"] = 1;
			variant["lmawd_id"] = lmawd_id;
			base.sendRPC(46u, variant);
		}

		public void SendGetVip(int awdtp = 0)
		{
			bool flag = awdtp != 0;
			if (flag)
			{
				Variant variant = new Variant();
				variant["awdtp"] = awdtp;
				base.sendRPC(46u, variant);
			}
			else
			{
				Variant msg = new Variant();
				base.sendRPC(46u, msg);
			}
		}
	}
}
