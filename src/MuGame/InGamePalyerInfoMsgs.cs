using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGamePalyerInfoMsgs : MsgProcduresBase
	{
		public InGamePalyerInfoMsgs(IClientBase m) : base(m)
		{
		}

		public static InGamePalyerInfoMsgs create(IClientBase m)
		{
			return new InGamePalyerInfoMsgs(m);
		}

		public override void init()
		{
		}

		public void AttChange()
		{
			Variant msg = new Variant();
			base.sendRPC(26u, msg);
		}

		public void SelfAttchange()
		{
			Variant msg = new Variant();
			base.sendRPC(32u, msg);
		}

		public void DetailInfoChange()
		{
			Variant msg = new Variant();
			base.sendRPC(40u, msg);
		}

		public void SkexpChange()
		{
			Variant msg = new Variant();
			base.sendRPC(41u, msg);
		}

		public void PlayerShowInfo(Variant cids)
		{
			Variant variant = new Variant();
			variant["cidary"] = cids;
			base.sendRPC(51u, variant);
		}

		public void PlayerDetailInfo(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendRPC(52u, variant);
		}

		public void lvl_up()
		{
			Variant msg = new Variant();
			base.sendRPC(60u, msg);
		}

		public void mod_exp()
		{
			Variant msg = new Variant();
			base.sendRPC(61u, msg);
		}

		public void on_get_user_cid_res(string name, bool ol, uint func)
		{
			Variant variant = new Variant();
			variant["ol"] = ol;
			variant["name"] = name;
			variant["func"] = func;
			base.sendRPC(251u, variant);
		}

		public void on_query_ply_info_res(Variant data)
		{
			base.sendRPC(253u, data);
		}
	}
}
