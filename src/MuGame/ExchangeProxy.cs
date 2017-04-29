using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class ExchangeProxy : BaseProxy<ExchangeProxy>
	{
		public static uint EVENT_EXCHANGE_SUC = 0u;

		public static uint EVENT_EXCHANGE_SYNC_COUNT = 1u;

		public ExchangeProxy()
		{
			this.addProxyListener(105u, new Action<Variant>(this.OnExchange));
		}

		public void GetExchangeInfo()
		{
			Variant variant = new Variant();
			variant["op"] = 0;
			this.sendRPC(105u, variant);
		}

		public void Exchange()
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(105u, variant);
		}

		public void OnExchange(Variant data)
		{
			int num = data["res"];
			int num2 = num;
			if (num2 != 0)
			{
				if (num2 != 1)
				{
					flytxt.instance.fly("兑换失败", 0, default(Color), null);
				}
				else
				{
					this.OnOneceExchange(data);
				}
			}
			else
			{
				this.OnSyncCount(data);
				IconAddLightMgr.getInstance().showOrHideFire("Light_exchange", data);
			}
		}

		private void OnOneceExchange(Variant data)
		{
			ExchangeModel instance = ModelBase<ExchangeModel>.getInstance();
			instance.Count = data["yinpiao_count"];
			base.dispatchEvent(GameEvent.Create(ExchangeProxy.EVENT_EXCHANGE_SUC, this, null, false));
		}

		private void OnSyncCount(Variant data)
		{
			ExchangeModel instance = ModelBase<ExchangeModel>.getInstance();
			instance.Count = data["yinpiao_count"];
			base.dispatchEvent(GameEvent.Create(ExchangeProxy.EVENT_EXCHANGE_SYNC_COUNT, this, null, false));
		}
	}
}
