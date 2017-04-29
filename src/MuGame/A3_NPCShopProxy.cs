using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_NPCShopProxy : BaseProxy<A3_NPCShopProxy>
	{
		public static uint EVENT_NPCSHOP_BUY = 0u;

		public static uint EVENT_NPCSHOP_REFRESH = 1u;

		public static uint EVENT_NPCSHOP_TIME = 2u;

		public A3_NPCShopProxy()
		{
			this.addProxyListener(234u, new Action<Variant>(this.onNPCShop));
		}

		public void sendShowFloat(uint npcid)
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["index"] = npcid;
			this.sendRPC(234u, variant);
			debug.Log(variant.dump());
		}

		public void sendBuy(uint npcid, uint shop_idx, uint shop_type, uint shop_num)
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			variant["index"] = npcid;
			variant["shop_idx"] = shop_idx;
			variant["shop_type"] = shop_type;
			variant["shop_num"] = shop_num;
			this.sendRPC(234u, variant);
			debug.Log(variant.dump());
		}

		public void sendShowAll()
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			this.sendRPC(234u, variant);
		}

		public void onNPCShop(Variant data)
		{
			debug.Log("NPCSHOP============" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
				bool flag2 = num == -5100;
				if (flag2)
				{
					this.sendShowFloat((uint)ModelBase<A3_NPCShopModel>.getInstance().listNPCShop[0].getInt("shop_id"));
				}
			}
			else
			{
				switch (num)
				{
				case 1:
					this.onFloat(data);
					break;
				case 2:
					this.onBuy(data);
					break;
				case 3:
					this.onRefresh(data);
					break;
				}
			}
		}

		private void onFloat(Variant data)
		{
			ModelBase<A3_NPCShopModel>.getInstance().alltimes = data["next_tm"];
			ModelBase<A3_NPCShopModel>.getInstance().price.Clear();
			bool flag = data.ContainsKey("float_list");
			if (flag)
			{
				Variant variant = data["float_list"];
				ModelBase<A3_NPCShopModel>.getInstance().float_list.Clear();
				ModelBase<A3_NPCShopModel>.getInstance().float_list_last.Clear();
				ModelBase<A3_NPCShopModel>.getInstance().float_list_num.Clear();
				ModelBase<A3_NPCShopModel>.getInstance().limit_num.Clear();
				foreach (Variant current in variant._arr)
				{
					bool flag2 = !ModelBase<A3_NPCShopModel>.getInstance().float_list.ContainsKey(current["item_id"]);
					if (flag2)
					{
						NpcShopData npcShopData = new NpcShopData();
						npcShopData.shop_id = current["shop_idx"]._uint;
						bool flag3 = !ModelBase<A3_NPCShopModel>.getInstance().float_list.ContainsKey(current["item_id"]);
						if (flag3)
						{
							ModelBase<A3_NPCShopModel>.getInstance().float_list.Add(current["item_id"], current["cost"]);
							npcShopData.nowprice = current["cost"]._int;
						}
						bool flag4 = !ModelBase<A3_NPCShopModel>.getInstance().float_list_last.ContainsKey(current["item_id"]);
						if (flag4)
						{
							ModelBase<A3_NPCShopModel>.getInstance().float_list_last.Add(current["item_id"], current["last_cost"]);
							npcShopData.lastprice = current["last_cost"]._int;
						}
						bool flag5 = !ModelBase<A3_NPCShopModel>.getInstance().float_list_num.ContainsKey(current["item_id"]);
						if (flag5)
						{
							ModelBase<A3_NPCShopModel>.getInstance().float_list_num.Add(current["item_id"], current["self_limit"]);
						}
						ModelBase<A3_NPCShopModel>.getInstance().price.Add((int)npcShopData.shop_id, npcShopData);
						ModelBase<A3_NPCShopModel>.getInstance().limit_num.Add(current["item_id"], current["limit_num"]);
					}
				}
				base.dispatchEvent(GameEvent.Create(A3_NPCShopProxy.EVENT_NPCSHOP_TIME, this, data, false));
			}
		}

		private void onBuy(Variant data)
		{
			base.dispatchEvent(GameEvent.Create(A3_NPCShopProxy.EVENT_NPCSHOP_BUY, this, data, false));
		}

		private void onRefresh(Variant data)
		{
			bool flag = data["float_list"] != null;
			if (flag)
			{
				Variant variant = data["float_list"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = !ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(current["item_id"]);
					if (flag2)
					{
						ModelBase<A3_NPCShopModel>.getInstance().all_float.Add(current["item_id"], current["cost"]);
					}
					else
					{
						ModelBase<A3_NPCShopModel>.getInstance().all_float[current["item_id"]] = current["cost"];
					}
				}
			}
			base.dispatchEvent(GameEvent.Create(A3_NPCShopProxy.EVENT_NPCSHOP_REFRESH, this, null, false));
		}
	}
}
