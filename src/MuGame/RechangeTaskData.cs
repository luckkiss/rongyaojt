using GameFramework;
using System;

namespace MuGame
{
	public class RechangeTaskData
	{
		public int moneyPayed = 0;

		private bool sended = false;

		public void init(JSONNode d)
		{
			bool flag = d["tcyb_day"] != null;
			if (flag)
			{
				this.moneyPayed = d["tcyb_day"].AsInt / 10;
				debug.Log("当日充值：" + this.moneyPayed);
			}
			bool flag2 = this.sended;
			if (!flag2)
			{
				bool flag3 = d["1000"] == null;
				if (!flag3)
				{
					d = d["1000"];
					bool flag4 = d["cards"] != null;
					if (flag4)
					{
						debug.Log(" cards ：" + d["cards"]);
						JSONArray asArray = d["cards"].AsArray;
						foreach (JSONNode jSONNode in asArray)
						{
							BaseProxy<GiftCardProxy>.getInstance().sendFetchCard(jSONNode.ToString());
							this.sended = true;
						}
					}
				}
			}
		}
	}
}
