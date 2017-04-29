using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_signProxy : BaseProxy<A3_signProxy>
	{
		public static uint SIGNINFO = 1001u;

		public static uint AllREPARISIGN = 1002u;

		public static uint SIGNorREPAIR = 1003u;

		public static uint ACCUMULATE = 1004u;

		public static uint SIGNINFO_YUEKA = 1005u;

		public A3_signProxy()
		{
			this.addProxyListener(207u, new Action<Variant>(this.onLoadSign));
		}

		public void sendproxy(int tp, int day)
		{
			Variant variant = new Variant();
			variant["tp"] = tp;
			switch (tp)
			{
			case 3:
				variant["day"] = day;
				break;
			}
			this.sendRPC(207u, variant);
		}

		private void onLoadSign(Variant data)
		{
			debug.Log("签到的信息：" + data.dump());
			bool flag = data.ContainsKey("yueka");
			if (flag)
			{
				base.dispatchEvent(GameEvent.Create(A3_signProxy.SIGNINFO_YUEKA, this, data, false));
			}
			bool flag2 = data.ContainsKey("yueka_tm");
			if (flag2)
			{
				base.dispatchEvent(GameEvent.Create(A3_signProxy.SIGNINFO, this, data, false));
				IconAddLightMgr.getInstance().showOrHideFire("refreshSign", data);
			}
			bool flag3 = data.ContainsKey("daysign");
			if (flag3)
			{
				debug.Log("签到或单个补签：" + data.dump());
				base.dispatchEvent(GameEvent.Create(A3_signProxy.SIGNorREPAIR, this, data, false));
				IconAddLightMgr.getInstance().showOrHideFire("singorrepair", data);
			}
			bool flag4 = data.ContainsKey("fillsign_all");
			if (flag4)
			{
				debug.Log("一键补签：" + data.dump());
				base.dispatchEvent(GameEvent.Create(A3_signProxy.AllREPARISIGN, this, data, false));
			}
			bool flag5 = data.ContainsKey("total_signup");
			if (flag5)
			{
				debug.Log("累积奖励：" + data.dump());
				base.dispatchEvent(GameEvent.Create(A3_signProxy.ACCUMULATE, this, data, false));
			}
		}
	}
}
