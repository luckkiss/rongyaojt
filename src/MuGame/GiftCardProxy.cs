using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class GiftCardProxy : BaseProxy<GiftCardProxy>
	{
		public GiftCardProxy()
		{
			this.addProxyListener(23u, new Action<Variant>(this.onItemCardInfo));
			this.addProxyListener(252u, new Action<Variant>(this.onErrorRes));
		}

		public void sendFetchCard(string id)
		{
			Variant variant = new Variant();
			variant["cardid"] = id;
			debug.Log("发送协议激活码:" + id);
			this.sendRPC(22u, variant);
		}

		public void sendLoadItemCardInfo(List<int> list = null)
		{
			Variant v = new Variant();
			bool flag = list != null;
			if (flag)
			{
				List<Variant> list2 = new List<Variant>();
				foreach (int current in list)
				{
					list2.Add(new Variant
					{
						_uint = (uint)current
					});
				}
			}
			this.sendRPC(23u, v);
		}

		private void onErrorRes(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				bool flag2 = flytxt.instance != null;
				if (flag2)
				{
					flytxt.instance.fly(err_string.get_Err_String(num), 0, default(Color), null);
				}
			}
		}

		private void onItemCardInfo(Variant d)
		{
			bool flag = d.ContainsKey("tp");
			if (flag)
			{
				HttpAppMgr.instance.onGetRewardItems(d["tp"]);
			}
			else
			{
				bool flag2 = d.ContainsKey("tpchange");
				if (flag2)
				{
					int random = ConfigUtil.getRandom(0, 20);
					DelayDoManager.singleton.AddDelayDo(delegate
					{
						this.sendLoadItemCardInfo(null);
					}, random, 0u);
					debug.Log("兑换码后台发生变！！！间隔发送请求礼品卡：" + random);
				}
				else
				{
					bool flag3 = d.ContainsKey("card_fetch");
					if (flag3)
					{
						Variant variant = d["card_fetch"];
						int num = variant["tp"];
						string text = variant["cardid"];
						Variant variant2 = variant["reward"];
						bool flag4 = variant2.ContainsKey("money");
						if (flag4)
						{
							flytxt.instance.fly("金币+ " + variant2["money"], 0, default(Color), null);
						}
						bool flag5 = variant2.ContainsKey("yb");
						if (flag5)
						{
							flytxt.instance.fly("钻石+ " + variant2["yb"], 0, default(Color), null);
						}
						bool flag6 = variant2.ContainsKey("bndyb");
						if (flag6)
						{
							flytxt.instance.fly("绑定钻石+ " + variant2["bndyb"], 0, default(Color), null);
						}
						bool flag7 = variant2.ContainsKey("itm");
						if (flag7)
						{
							List<Variant> arr = variant2["itm"]._arr;
							for (int i = 0; i < arr.Count; i++)
							{
								a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(arr[i]["tpid"]);
								string colorStrByQuality = Globle.getColorStrByQuality(itemDataById.item_name, itemDataById.quality);
								flytxt.instance.fly("获得" + colorStrByQuality + "+" + arr[i]["cnt"], 0, default(Color), null);
							}
						}
					}
					bool flag8 = d.ContainsKey("cards");
					if (flag8)
					{
						List<Variant> arr2 = d["cards"]._arr;
						for (int j = 0; j < arr2.Count; j++)
						{
							int num2 = arr2[j]["tp"];
							string text2 = arr2[j]["cardid"];
							bool flag9 = num2 == 4;
							if (flag9)
							{
							}
						}
					}
					bool flag10 = d.ContainsKey("tpawds");
					if (flag10)
					{
						List<Variant> arr3 = d["tpawds"]._arr;
						for (int k = 0; k < arr3.Count; k++)
						{
							GiftCardType giftCardType = new GiftCardType();
							giftCardType.init(arr3[k]);
							HttpAppMgr.instance.giftCard.addGiftType(giftCardType);
						}
					}
					HttpAppMgr.instance.giftCard.getAllCode();
				}
			}
		}
	}
}
