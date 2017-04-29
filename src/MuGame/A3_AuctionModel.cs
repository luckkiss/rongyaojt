using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_AuctionModel : ModelBase<A3_AuctionModel>
	{
		private Dictionary<uint, a3_BagItemData> items = new Dictionary<uint, a3_BagItemData>();

		private Dictionary<uint, a3_BagItemData> myitems_up = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<uint, a3_BagItemData> myitems_down = new Dictionary<uint, a3_BagItemData>();

		public void AddMyItem(Variant data)
		{
			this.myitems_up.Clear();
			this.myitems_down.Clear();
			bool flag = data.ContainsKey("auc_data");
			if (flag)
			{
				Variant variant = data["auc_data"];
				foreach (Variant current in variant._arr)
				{
					a3_BagItemData a3_BagItemData = this.ReadItem(current);
					this.RemoveItem(a3_BagItemData.id);
					this.myitems_up[a3_BagItemData.id] = a3_BagItemData;
				}
			}
			bool flag2 = data.ContainsKey("get_list");
			if (flag2)
			{
				Variant variant2 = data["get_list"];
				foreach (Variant current2 in variant2._arr)
				{
					a3_BagItemData a3_BagItemData2 = this.ReadItem(current2);
					this.RemoveItem(a3_BagItemData2.id);
					this.myitems_down[a3_BagItemData2.id] = a3_BagItemData2;
				}
			}
		}

		public void UpToDown(Variant data)
		{
			bool flag = data.ContainsKey("get_list");
			if (flag)
			{
				Variant variant = data["get_list"];
				foreach (Variant current in variant._arr)
				{
					uint key = current["id"];
					bool flag2 = this.myitems_up.ContainsKey(key);
					if (flag2)
					{
						this.myitems_up.Remove(key);
					}
					a3_BagItemData a3_BagItemData = this.ReadItem(current);
					this.myitems_down[a3_BagItemData.id] = a3_BagItemData;
				}
			}
		}

		public void AddItem(Variant data)
		{
			a3_BagItemData a3_BagItemData = this.ReadItem(data);
			this.items[a3_BagItemData.id] = a3_BagItemData;
		}

		private a3_BagItemData ReadItem(Variant data)
		{
			uint id = data["id"];
			uint tpid = data["tpid"];
			a3_BagItemData a3_BagItemData = default(a3_BagItemData);
			a3_BagItemData.id = id;
			a3_BagItemData.tpid = tpid;
			bool flag = data.ContainsKey("num");
			if (flag)
			{
				a3_BagItemData.num = data["num"];
			}
			a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid);
			Variant item = data["itm"];
			a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, item);
			a3_BagItemData.auctiondata.cid = data["cid"];
			a3_BagItemData.auctiondata.tm = data["tm"];
			a3_BagItemData.auctiondata.pro_tm = data["puttm_type"];
			a3_BagItemData.auctiondata.cost = data["cost"];
			bool flag2 = data.ContainsKey("get_type");
			if (flag2)
			{
				a3_BagItemData.auctiondata.get_type = data["get_type"];
			}
			bool flag3 = data.ContainsKey("get_tm");
			if (flag3)
			{
				a3_BagItemData.auctiondata.get_tm = data["get_tm"];
			}
			bool flag4 = data.ContainsKey("seller_name");
			if (flag4)
			{
				a3_BagItemData.auctiondata.seller = data["seller_name"];
			}
			return a3_BagItemData;
		}

		public void RemoveItem(uint id)
		{
			bool flag = this.items.ContainsKey(id);
			if (flag)
			{
				this.items.Remove(id);
			}
		}

		public void UpdateItem(Variant data)
		{
			uint key = data["id"];
			bool flag = this.items.ContainsKey(key);
			if (flag)
			{
				this.items.Remove(key);
			}
			this.AddItem(data);
		}

		public void Clear()
		{
			this.items.Clear();
		}

		public Dictionary<uint, a3_BagItemData> GetItems()
		{
			return this.items;
		}

		public Dictionary<uint, a3_BagItemData> GetMyItems_up()
		{
			return this.myitems_up;
		}

		public Dictionary<uint, a3_BagItemData> GetMyItems_down()
		{
			return this.myitems_down;
		}

		public string FromGetTypeToString(int get_type)
		{
			bool flag = get_type == 1;
			string result;
			if (flag)
			{
				result = "物品下架";
			}
			else
			{
				bool flag2 = get_type == 2;
				if (flag2)
				{
					result = "物品购买";
				}
				else
				{
					bool flag3 = get_type == 3;
					if (flag3)
					{
						result = "物品卖出";
					}
					else
					{
						result = "特殊物品";
					}
				}
			}
			return result;
		}
	}
}
