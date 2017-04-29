using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LotteryProxy : BaseProxy<LotteryProxy>
	{
		public static uint LOADLOTTERY = 1u;

		public static uint LOTTERYOK_FREEDRAW = 2u;

		public static uint LOTTERYOK_ICEDRAWONCE = 3u;

		public static uint LOTTERYOK_ICEDRAWTENTH = 4u;

		public static uint LOTTERYTIP_FREEDRAW = 5u;

		public static uint LOTTERYOK_ICED_NEWBIE = 6u;

		public LotteryProxy()
		{
			this.addProxyListener(98u, new Action<Variant>(this.onloadlottery));
		}

		public void sendlottery(int id)
		{
			Variant variant = new Variant();
			variant["lottery_cmd"] = id;
			debug.Log("发送" + variant.dump());
			this.sendRPC(98u, variant);
		}

		public void onloadlottery(Variant data)
		{
			int num = data["res"];
			debug.Log("C#：：" + data.dump());
			bool flag = num == 1;
			if (flag)
			{
				IconAddLightMgr.getInstance().showOrHideFire("ShowFreeDrawAvaible", data);
				base.dispatchEvent(GameEvent.Create(LotteryProxy.LOADLOTTERY, this, data, false));
			}
			else
			{
				bool flag2 = num == 2;
				if (flag2)
				{
					base.dispatchEvent(GameEvent.Create(LotteryProxy.LOTTERYOK_FREEDRAW, this, data, false));
				}
				else
				{
					bool flag3 = num == 3;
					if (flag3)
					{
						base.dispatchEvent(GameEvent.Create(LotteryProxy.LOTTERYOK_ICEDRAWONCE, this, data, false));
					}
					else
					{
						bool flag4 = num == 4;
						if (flag4)
						{
							base.dispatchEvent(GameEvent.Create(LotteryProxy.LOTTERYOK_ICEDRAWTENTH, this, data, false));
						}
						else
						{
							bool flag5 = num == 5;
							if (flag5)
							{
								base.dispatchEvent(GameEvent.Create(LotteryProxy.LOTTERYOK_ICED_NEWBIE, this, data, false));
							}
							else
							{
								Globle.err_output(num);
							}
						}
					}
				}
			}
		}
	}
}
