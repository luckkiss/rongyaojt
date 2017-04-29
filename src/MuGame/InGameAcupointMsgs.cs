using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameAcupointMsgs : MsgProcduresBase
	{
		public InGameAcupointMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameAcupointMsgs create(IClientBase m)
		{
			return new InGameAcupointMsgs(m);
		}

		public override void init()
		{
		}

		public void OpenMeri(int meriid, int aid)
		{
			Variant variant = new Variant();
			variant["meriid"] = meriid;
			variant["aid"] = aid;
			base.sendRPC(93u, variant);
		}

		public void OpenAcup(int meriid)
		{
			Variant variant = new Variant();
			variant["meriid"] = meriid;
			base.sendRPC(93u, variant);
		}

		public void requets_open_meri()
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			base.sendRPC(91u, variant);
		}

		public void get_mes_meri(int meriid)
		{
			Variant variant = new Variant();
			variant["bacupup"] = true;
			variant["meriid"] = meriid;
			base.sendRPC(79u, variant);
		}

		public void expend_cdcnt()
		{
			Variant msg = new Variant();
			base.sendRPC(84u, msg);
		}

		public void quick_fin_cd(float tm)
		{
			Variant variant = new Variant();
			variant["tm"] = tm;
			base.sendRPC(83u, variant);
		}

		public void start_acupup(uint meriid, uint aid)
		{
			Variant variant = new Variant();
			variant["meriid"] = meriid;
			variant["aid"] = aid;
			base.sendRPC(82u, variant);
		}
	}
}
