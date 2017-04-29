using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class TaelsProxy : BaseProxy<TaelsProxy>
	{
		public static uint SHOWED_TAELS = 1u;

		public static uint TAELS = 2u;

		public TaelsProxy()
		{
			this.addProxyListener(105u, new Action<Variant>(this.onExchange));
		}

		public void sendExchange()
		{
			Variant variant = new Variant();
			variant["yinpiao_use"] = 0;
			this.sendRPC(105u, variant);
		}

		public void sendInfo()
		{
			Variant variant = new Variant();
			variant["yinpiao_info"] = 0;
			this.sendRPC(105u, variant);
		}

		public void onExchange(Variant data)
		{
			Debug.Log("银票：" + data.dump());
			bool flag = data["yinpiao_count"] != null;
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(TaelsProxy.SHOWED_TAELS, this, data, false));
			}
			bool flag2 = data["yinpiao_success"] != null;
			if (flag2)
			{
				bool flag3 = data["res"] != null && data["res"] < 0;
				if (flag3)
				{
					Globle.err_output(data["res"]);
				}
				else
				{
					base.dispatchEvent(GameEvent.Create(TaelsProxy.TAELS, this, data, false));
				}
			}
		}
	}
}
