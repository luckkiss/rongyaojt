using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_VipProxy : BaseProxy<A3_VipProxy>
	{
		public static uint EVENT_ON_VIP_CHANGE = 0u;

		public A3_VipProxy()
		{
			this.addProxyListener(228u, new Action<Variant>(this.OnVip));
		}

		public void GetVip()
		{
			this.sendRPC(228u, null);
		}

		public void GetVipGift(int val)
		{
			Variant variant = new Variant();
			variant["gift_id"] = val;
			this.sendRPC(228u, variant);
		}

		private void OnVip(Variant data)
		{
			A3_VipModel instance = ModelBase<A3_VipModel>.getInstance();
			int num = data["res"];
			bool flag = num == 1;
			if (flag)
			{
				debug.Log(data.dump());
				instance.Level = data["viplvl"];
				instance.Exp = data["vipexp"];
				instance.isGetVipGift.Clear();
				using (List<Variant>.Enumerator enumerator = data["vip_gifts"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						uint item = enumerator.Current;
						instance.isGetVipGift.Add(item);
					}
				}
				ModelBase<A3_VipModel>.getInstance().viplvl_refresh();
			}
			bool flag2 = a3_vip.instan;
			if (flag2)
			{
				a3_vip.instan.OnGiftBtnRefresh();
			}
			bool flag3 = num == 2;
			if (flag3)
			{
			}
			bool flag4 = data.ContainsKey("vip_level");
			if (flag4)
			{
			}
		}
	}
}
