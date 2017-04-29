using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class SignProxy : BaseProxy<SignProxy>
	{
		public static uint SIGN_ALLINFO = 1u;

		public static uint SIGN_SIGN = 2u;

		public static uint SIGN_FILL = 3u;

		public bool isOneOpen = false;

		public bool isFirstGet = false;

		public bool isOneOpen_ShouChong = false;

		public Variant _variant = null;

		public void sendMonth()
		{
		}

		public void sendDaysign()
		{
			Variant variant = new Variant();
			variant["daysign"] = 1;
		}

		public void sendFillsign()
		{
			Variant variant = new Variant();
			variant["fillsign"] = 1;
		}

		public void onLoadSign(Variant data)
		{
			bool flag = data.ContainsKey("daysign");
			if (flag)
			{
				debug.Log("要签到：" + data.dump());
				base.dispatchEvent(GameEvent.Create(SignProxy.SIGN_SIGN, this, data, false));
			}
			bool flag2 = data.ContainsKey("fillsign");
			if (flag2)
			{
				debug.Log("要补签：" + data.dump());
				base.dispatchEvent(GameEvent.Create(SignProxy.SIGN_FILL, this, data, false));
			}
			bool flag3 = data.ContainsKey("month_data");
			if (flag3)
			{
				debug.Log("签到信息：" + data.dump());
				bool flag4 = !this.isOneOpen_ShouChong && !ModelBase<PlayerModel>.getInstance().isFirstRechange && ModelBase<PlayerModel>.getInstance().lvl > 1u;
				if (flag4)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.FIRST_RECHANGE, null, false);
				}
				else
				{
					bool flag5 = !this.isOneOpen && data["today"] == 0;
					if (!flag5)
					{
						BaseProxy<SignProxy>.getInstance().isOneOpen = true;
					}
				}
				this.isOneOpen_ShouChong = true;
				bool flag6 = data["today"] == 0;
				if (flag6)
				{
					this.isFirstGet = false;
				}
				else
				{
					this.isFirstGet = true;
				}
				base.dispatchEvent(GameEvent.Create(SignProxy.SIGN_ALLINFO, this, data, false));
			}
		}
	}
}
