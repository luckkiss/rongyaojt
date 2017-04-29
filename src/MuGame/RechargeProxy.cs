using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class RechargeProxy : BaseProxy<RechargeProxy>
	{
		private const uint C2S_GET_RECHARGE = 113u;

		private const uint S2C_GET_RECHARGE_RES = 113u;

		public static uint LIS_RECHARGE_TYPE_RES = 1u;

		public Dictionary<int, int> Rechargetm = new Dictionary<int, int>();

		public RechargeProxy()
		{
			this.addProxyListener(113u, new Action<Variant>(this.getRechargeDate));
		}

		public void sendGetRechargeInfo()
		{
			this.sendRPC(113u, null);
		}

		public void getRechargeDate(Variant data)
		{
			this.Rechargetm[1] = 0;
			this.Rechargetm[2] = 0;
			this.Rechargetm[3] = 0;
			bool flag = data.ContainsKey("monthly");
			if (flag)
			{
				this.Rechargetm[1] = data["monthly"]._int32;
			}
			bool flag2 = data.ContainsKey("quarterly");
			if (flag2)
			{
				this.Rechargetm[2] = data["quarterly"]._int32;
			}
			bool flag3 = data.ContainsKey("yearly");
			if (flag3)
			{
				this.Rechargetm[3] = data["yearly"]._int32;
			}
			base.dispatchEvent(GameEvent.Create(RechargeProxy.LIS_RECHARGE_TYPE_RES, this, null, false));
		}
	}
}
