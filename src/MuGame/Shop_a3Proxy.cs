using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class Shop_a3Proxy : BaseProxy<Shop_a3Proxy>
	{
		public static uint LIMITED = 0u;

		public static uint CHANGELIMITED = 1u;

		public static uint DELETELIMITED = 2u;

		public static uint DONATECHANGE = 3u;

		public Shop_a3Proxy()
		{
			this.addProxyListener(62u, new Action<Variant>(this.onLoadShop));
		}

		public void sendinfo(int count, int id = -1, int num = 0, int activityId = -1)
		{
			Variant variant = new Variant();
			switch (count)
			{
			case 0:
				variant["op"] = 0;
				break;
			case 1:
				variant["op"] = 1;
				break;
			case 2:
				variant["op"] = 2;
				variant["id"] = id;
				variant["item_num"] = num;
				break;
			case 3:
				variant["op"] = 3;
				variant["id"] = id;
				variant["item_num"] = num;
				variant["shop_id"] = activityId;
				break;
			case 5:
				variant["op"] = 2;
				variant["id"] = id;
				variant["item_num"] = num;
				break;
			case 6:
				variant["op"] = 5;
				variant["id"] = id;
				variant["item_num"] = num;
				break;
			}
			this.sendRPC(62u, variant);
			debug.Log(variant.dump());
		}

		public void BuyStoreItems(uint tpid, uint num)
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			shopDatas shopDataById = ModelBase<Shop_a3Model>.getInstance().GetShopDataById((int)tpid);
			variant["id"] = shopDataById.id;
			variant["item_num"] = num;
			this.sendRPC(62u, variant);
			flytxt.instance.fly(string.Concat(new object[]
			{
				"自动购买",
				num,
				"个",
				shopDataById.itemName
			}), 0, default(Color), null);
		}

		public void onLoadShop(Variant data)
		{
			int num = data["res"];
			bool flag = num == 0;
			if (flag)
			{
				debug.Log("绑定宝石信息：" + data.dump());
				bool flag2 = data["confs"].Length > 0;
				if (flag2)
				{
					foreach (Variant current in data["confs"]._arr)
					{
						ModelBase<Shop_a3Model>.getInstance().bundinggem(current["id"], current["item_num"]);
					}
				}
			}
			else
			{
				bool flag3 = num == 1;
				if (flag3)
				{
					debug.Log("收到的a3显示抢购刷新信息：" + data.dump());
					base.dispatchEvent(GameEvent.Create(Shop_a3Proxy.LIMITED, this, data, false));
				}
				else
				{
					bool flag4 = num == 2;
					if (flag4)
					{
						debug.Log("收到的3购买信息：" + data.dump());
						ModelBase<Shop_a3Model>.getInstance().bundinggem(data["id"], data["item_num"]);
						bool flag5 = data.ContainsKey("donate");
						if (flag5)
						{
							base.dispatchEvent(GameEvent.Create(Shop_a3Proxy.DONATECHANGE, this, data, false));
						}
						bool flag6 = shop_a3._instance != null;
						if (flag6)
						{
							shop_a3._instance.Refresh(data["id"], data["item_num"]);
						}
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_DYETIP);
					}
					else
					{
						bool flag7 = num == 3;
						if (flag7)
						{
							debug.Log("收到的a3限时特卖购买信息：" + data.dump());
							shop_a3._instance.Refresh_limited(data["id"], data["shop_id"], data["left_num"]);
						}
						else
						{
							bool flag8 = num == 4;
							if (flag8)
							{
								debug.Log("收到的a3新增或变更限时特卖购买活动:" + data.dump());
								base.dispatchEvent(GameEvent.Create(Shop_a3Proxy.CHANGELIMITED, this, data, false));
							}
							else
							{
								bool flag9 = num == 5;
								if (flag9)
								{
									debug.Log("收到的a3限时特卖更改活动信息:" + data.dump());
									base.dispatchEvent(GameEvent.Create(Shop_a3Proxy.DELETELIMITED, this, data, false));
								}
								else
								{
									bool flag10 = num < 0;
									if (flag10)
									{
										Globle.err_output(data["res"]);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
