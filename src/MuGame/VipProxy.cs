using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class VipProxy : BaseProxy<VipProxy>
	{
		public static uint EVENT_ON_VIP_CHANGE = 0u;

		public VipProxy()
		{
			this.addProxyListener(228u, new Action<Variant>(this.onVip));
			this.addProxyListener(229u, new Action<Variant>(this.onBuyTime));
		}

		public void sendBuyTime(int id)
		{
			Variant variant = new Variant();
			variant["int_act"] = id;
			variant["cnt"] = 1;
			this.sendRPC(229u, variant);
		}

		public void sendLoadVip()
		{
			this.sendRPC(228u, null);
		}

		private void onBuyTime(Variant data)
		{
			int num = data["res"];
			bool flag = num != 1;
			if (flag)
			{
				Globle.err_output(num);
			}
		}

		private void onVip(Variant data)
		{
			ModelBase<PlayerModel>.getInstance().vipChange(data);
			base.dispatchEvent(GameEvent.Create(VipProxy.EVENT_ON_VIP_CHANGE, this, data, false));
		}
	}
}
