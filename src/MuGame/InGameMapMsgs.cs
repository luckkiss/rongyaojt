using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameMapMsgs : MsgProcduresBase
	{
		public InGameMapMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameMapMsgs create(IClientBase m)
		{
			return new InGameMapMsgs(m);
		}

		public override void init()
		{
		}

		public void goto_map(Variant data)
		{
			base.sendRPC(12u, data);
		}

		public void npcTrans(uint npcid, uint trid)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["trid"] = trid;
			base.sendRPC(19u, variant);
		}

		public void get_wrdboss_respawntm(bool islvl = false)
		{
			Variant variant = new Variant();
			variant["islvl"] = islvl;
			base.sendRPC(20u, variant);
		}

		public void respawn(bool useGolden = false)
		{
			Variant variant = new Variant();
			variant["useGolden"] = useGolden;
			base.sendRPC(21u, variant);
		}

		public void get_sprite_hp_info(uint iid)
		{
			Variant variant = new Variant();
			variant["iid"] = iid;
			base.sendRPC(53u, variant);
		}

		public void change_map(uint mapid)
		{
			Variant variant = new Variant();
			variant["gto"] = mapid;
			base.sendRPC(57u, variant);
		}

		public void end_change_map()
		{
			base.sendRPC(58u, new Variant());
		}
	}
}
