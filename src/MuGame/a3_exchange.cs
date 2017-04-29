using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_exchange : Window
	{
		private int diamand = 50;

		public static a3_exchange Instance;

		public override void init()
		{
			a3_exchange.Instance = this;
			base.getEventTrigerByPath("btclose").onClick = new EventTriggerListener.VoidDelegate(this.onClose);
			base.getEventTrigerByPath("exchangeBtn").onClick = new EventTriggerListener.VoidDelegate(this.onExchange);
		}

		public override void onShowed()
		{
			BaseProxy<ExchangeProxy>.getInstance().addEventListener(ExchangeProxy.EVENT_EXCHANGE_SUC, new Action<GameEvent>(this.onExchangeSuccess));
			BaseProxy<ExchangeProxy>.getInstance().addEventListener(ExchangeProxy.EVENT_EXCHANGE_SYNC_COUNT, new Action<GameEvent>(this.onSyncCount));
			this.refreshCount();
		}

		public override void onClosed()
		{
			BaseProxy<ExchangeProxy>.getInstance().removeEventListener(ExchangeProxy.EVENT_EXCHANGE_SUC, new Action<GameEvent>(this.onExchangeSuccess));
			BaseProxy<ExchangeProxy>.getInstance().removeEventListener(ExchangeProxy.EVENT_EXCHANGE_SYNC_COUNT, new Action<GameEvent>(this.onSyncCount));
		}

		private void onExchangeSuccess(GameEvent e)
		{
			this.refreshCount();
		}

		private void onSyncCount(GameEvent e)
		{
			this.refreshCount();
		}

		private void refreshCount()
		{
			bool flag = ModelBase<A3_VipModel>.getInstance().Level > 0;
			int num;
			if (flag)
			{
				num = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(3);
			}
			else
			{
				num = 10;
			}
			ExchangeModel instance = ModelBase<ExchangeModel>.getInstance();
			base.getComponentByPath<Text>("diamand/Text").text = ((long)this.diamand * (long)((ulong)(instance.Count + 1u))).ToString();
			bool flag2 = (long)num - (long)((ulong)instance.Count) >= 0L;
			if (flag2)
			{
				base.getComponentByPath<Text>("exchangeBtn/Text/leftCnt").text = string.Concat(new object[]
				{
					"(",
					(long)num - (long)((ulong)instance.Count),
					"/",
					num,
					")"
				});
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EXCHANGE);
		}

		private void onExchange(GameObject go)
		{
			ExchangeModel instance = ModelBase<ExchangeModel>.getInstance();
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().gold < (ulong)((long)this.diamand * (long)((ulong)(instance.Count + 1u)));
			if (flag)
			{
				flytxt.instance.fly("钻石不足", 0, default(Color), null);
			}
			else
			{
				bool flag2 = ModelBase<A3_VipModel>.getInstance().Level > 0;
				int num;
				if (flag2)
				{
					num = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(3);
				}
				else
				{
					num = 10;
				}
				bool flag3 = (long)num - (long)((ulong)instance.Count) <= 0L;
				if (flag3)
				{
					flytxt.instance.fly("兑换次数已用尽", 0, default(Color), null);
				}
				else
				{
					ExchangeProxy instance2 = BaseProxy<ExchangeProxy>.getInstance();
					instance2.Exchange();
				}
			}
		}
	}
}
