using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class ResetLvLProxy : BaseProxy<ResetLvLProxy>
	{
		public static uint EVENT_RESETLVL = 9101u;

		public static uint EVENT_SHOWRESETLVL = 9102u;

		public ResetLvLProxy()
		{
			this.addProxyListener(91u, new Action<Variant>(this.onGetMerisRes));
		}

		public void sendResetLvL()
		{
			this.sendRPC(91u, null);
		}

		private void onGetMerisRes(Variant data)
		{
			bool flag = data["res"] == 1;
			if (flag)
			{
				debug.Log(data.dump());
				Variant variant = new Variant();
				variant["zhuan"] = data["zhuan"];
				variant["att_pt"] = data["att_pt"];
				bool flag2 = data.ContainsKey("lvl");
				if (flag2)
				{
					variant["lvl"] = data["lvl"];
				}
				ModelBase<PlayerModel>.getInstance().lvUp(variant);
				ModelBase<PlayerModel>.getInstance().up_lvl = data["zhuan"]._uint;
				ModelBase<PlayerModel>.getInstance().pt_att = data["att_pt"]._int;
				ModelBase<PlayerModel>.getInstance().lvl = ((data["lvl"] == null) ? ModelBase<PlayerModel>.getInstance().lvl : data["lvl"]._uint);
				this.resetLvL();
				Variant variant2 = new Variant();
				bool flag3 = data.ContainsKey("lvl");
				if (flag3)
				{
					data["lvl"] = data["lvl"];
				}
				bool flag4 = data.ContainsKey("zhuan");
				if (flag4)
				{
					data["zhuan"] = data["zhuan"];
				}
				InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modInfo", "model/PlayerModel", new object[]
				{
					variant2
				});
				base.dispatchEvent(GameEvent.Create(ResetLvLProxy.EVENT_RESETLVL, this, variant, false));
			}
			else
			{
				Globle.err_output(data["res"]);
			}
		}

		public void resetLvL()
		{
			bool val = this.checkResetLvL(ModelBase<PlayerModel>.getInstance());
			Variant variant = new Variant();
			variant["show"] = val;
			base.dispatchEvent(GameEvent.Create(ResetLvLProxy.EVENT_SHOWRESETLVL, this, variant, false));
		}

		public bool checkResetLvL(PlayerModel pm)
		{
			int profession = pm.profession;
			uint lvl = pm.lvl;
			uint up_lvl = pm.up_lvl;
			uint exp = pm.exp;
			return this.isCanResetLvL(profession, lvl, up_lvl, exp);
		}

		private bool isCanResetLvL(int pp, uint pl, uint pz, uint exp)
		{
			uint needExpByCurrentZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedExpByCurrentZhuan(pp, pz);
			uint needLvLByCurrentZhuan = ModelBase<ResetLvLModel>.getInstance().getNeedLvLByCurrentZhuan(pp, pz);
			bool flag = needLvLByCurrentZhuan > pl;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = pz >= 10u;
				result = !flag2;
			}
			return result;
		}
	}
}
