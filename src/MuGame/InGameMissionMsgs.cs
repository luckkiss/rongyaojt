using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameMissionMsgs : MsgProcduresBase
	{
		public InGameMissionMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameMissionMsgs create(IClientBase m)
		{
			return new InGameMissionMsgs(m);
		}

		public override void init()
		{
			this.g_mgr.regRPCProcesser(43u, new NetManager.RPCProcCreator(onFetchGmisAwdRes.create));
			this.g_mgr.regRPCProcesser(108u, new NetManager.RPCProcCreator(onRmisRes.create));
			this.g_mgr.regRPCProcesser(110u, new NetManager.RPCProcCreator(onAcceptMisRes.create));
			this.g_mgr.regRPCProcesser(111u, new NetManager.RPCProcCreator(onComitMisRes.create));
			this.g_mgr.regRPCProcesser(112u, new NetManager.RPCProcCreator(onGetFinedMisStateRes.create));
			this.g_mgr.regRPCProcesser(113u, new NetManager.RPCProcCreator(onMisDataModify.create));
			this.g_mgr.regRPCProcesser(114u, new NetManager.RPCProcCreator(onGetMisLineStateRes.create));
			this.g_mgr.regRPCProcesser(115u, new NetManager.RPCProcCreator(onAbordMisRes.create));
			this.g_mgr.regRPCProcesser(116u, new NetManager.RPCProcCreator(onLvlMisChanged.create));
		}

		public void GetGmisInfo()
		{
			Variant msg = new Variant();
			base.sendRPC(42u, msg);
		}

		public void GetGmisAwd(Variant data)
		{
			Variant variant = new Variant();
			variant["gmisid"] = 0;
			variant["vip"] = false;
			foreach (string current in data.Keys)
			{
				bool flag = !variant.ContainsKey(current);
				if (!flag)
				{
					variant[current] = data[current];
				}
			}
			base.sendRPC(43u, variant);
		}

		public void GetLvlmisPrize(int lmisid)
		{
			Variant variant = new Variant();
			variant["lmisid"] = lmisid;
			base.sendRPC(116u, variant);
		}

		public void GetRmisInfo(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			variant["id"] = id;
			base.sendRPC(108u, variant);
		}

		public void RefreshRmisInfo(int id, int rqualid, int toqual = 0)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["id"] = id;
			variant["rqualid"] = rqualid;
			bool flag = toqual > 0;
			if (flag)
			{
				variant["bcnt"] = 0;
				variant["toqual"] = toqual;
			}
			base.sendRPC(108u, variant);
		}

		public void RefreshRmisBqual(int id, int rqualid, int bqual)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["id"] = id;
			variant["rqualid"] = rqualid;
			variant["rqualid"] = bqual;
			base.sendRPC(108u, variant);
		}

		public void GetAppawd()
		{
			Variant variant = new Variant();
			variant["tp"] = 8;
			base.sendRPC(108u, variant);
		}

		public void OnekeyFinRmis(int rmisid, bool isdouble)
		{
			Variant variant = new Variant();
			variant["tp"] = 9;
			variant["id"] = rmisid;
			variant["double"] = isdouble;
			base.sendRPC(108u, variant);
		}

		public void GetDayAwd(int rmisid)
		{
			Variant variant = new Variant();
			variant["tp"] = 10;
			variant["id"] = rmisid;
			base.sendRPC(108u, variant);
		}

		public void GetRmisShareInfo()
		{
			Variant variant = new Variant();
			variant["tp"] = 11;
			base.sendRPC(108u, variant);
		}

		public void AcceptMis(int misid)
		{
			Variant variant = new Variant();
			variant["misid"] = misid;
			base.sendRPC(110u, variant);
		}

		public void CommitMis(int misid, bool double_awd = false)
		{
			Variant variant = new Variant();
			variant["misid"] = misid;
			variant["double_awd"] = double_awd;
			base.sendRPC(111u, variant);
		}

		public void GetFinedMisState(Variant misids)
		{
			Variant variant = new Variant();
			variant["misids"] = misids;
			base.sendRPC(112u, variant);
		}

		public void AutoComitMis(int misid, bool double_awd = false)
		{
			Variant variant = new Variant();
			variant["misid"] = misid;
			variant["double_awd"] = double_awd;
			base.sendRPC(113u, variant);
		}

		public void GetMisLineState(Variant lineids)
		{
			Variant variant = new Variant();
			variant["lineids"] = lineids;
			base.sendRPC(114u, variant);
		}

		public void AbordMis(int misid)
		{
			Variant variant = new Variant();
			variant["misid"] = misid;
			base.sendRPC(115u, variant);
		}
	}
}
