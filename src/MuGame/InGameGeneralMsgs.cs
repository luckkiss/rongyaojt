using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameGeneralMsgs : MsgProcduresBase
	{
		public InGameGeneralMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameGeneralMsgs create(IClientBase m)
		{
			return new InGameGeneralMsgs(m);
		}

		public override void init()
		{
			BaseProxy<MapProxy>.getInstance();
			this.g_mgr.regRPCProcesser(1u, new NetManager.RPCProcCreator(fcm_notify.create));
			this.g_mgr.regRPCProcesser(3u, new NetManager.RPCProcCreator(pk_v_change.create));
			this.g_mgr.regRPCProcesser(4u, new NetManager.RPCProcCreator(line_change.create));
			this.g_mgr.regRPCProcesser(5u, new NetManager.RPCProcCreator(gain_achive.create));
			this.g_mgr.regRPCProcesser(35u, new NetManager.RPCProcCreator(get_achives_res.create));
			this.g_mgr.regRPCProcesser(49u, new NetManager.RPCProcCreator(line_info_res.create));
			this.g_mgr.regRPCProcesser(50u, new NetManager.RPCProcCreator(onJoinWorldRes.create));
		}

		public void SendSaveQuickbar(Variant msg)
		{
			base.sendRPC(150u, GameTools.createGroup(new Variant[]
			{
				"quickbar",
				msg
			}));
		}

		public void GetAchives()
		{
			base.sendRPC(35u, new Variant());
		}

		public void ActiveAchive(uint achid)
		{
			Variant variant = new Variant();
			variant["archiveid"] = achid;
			base.sendRPC(34u, variant);
		}

		public void GetRnkAchives()
		{
			Variant variant = new Variant();
			variant["rnkach"] = 1u;
			base.sendRPC(35u, variant);
		}

		public void SendPKState(Variant data)
		{
		}
	}
}
