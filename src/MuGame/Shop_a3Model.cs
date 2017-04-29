using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class Shop_a3Model : ModelBase<Shop_a3Model>
	{
		public Dictionary<int, shopDatas> itemsdic = new Dictionary<int, shopDatas>();

		public int selectnum = 0;

		public Shop_a3Model()
		{
			this.Readxml();
		}

		private void Readxml()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("golden_shop.golden_shop", "");
			foreach (SXML current in sXMLList)
			{
				shopDatas shopDatas = new shopDatas();
				shopDatas.id = current.getInt("id");
				shopDatas.type = current.getInt("type");
				shopDatas.itemid = current.getInt("itemid");
				shopDatas.money_type = current.getInt("money_type");
				shopDatas.value = current.getInt("value");
				shopDatas.itemName = current.getString("itemname");
				bool flag = current.getInt("limit") != -1;
				if (flag)
				{
					shopDatas.limiteD = current.getInt("limit");
					shopDatas.limiteNum = current.getInt("limit");
				}
				bool flag2 = current.getInt("limit_w") != -1;
				if (flag2)
				{
					shopDatas.limiteD = current.getInt("limit_w");
					shopDatas.limiteNum = current.getInt("limit_w");
				}
				this.itemsdic[shopDatas.id] = shopDatas;
			}
		}

		public void bundinggem(int id, int num)
		{
			foreach (int current in this.itemsdic.Keys)
			{
				bool flag = id == current;
				if (flag)
				{
					this.itemsdic[id].limiteD -= num;
					Debug.Log(id + "num:" + this.itemsdic[id].limiteD);
				}
			}
		}

		public shopDatas GetShopDataById(int itemid)
		{
			Dictionary<int, shopDatas>.Enumerator enumerator = this.itemsdic.GetEnumerator();
			shopDatas result;
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, shopDatas> current = enumerator.Current;
				bool flag = current.Value.itemid == itemid;
				if (flag)
				{
					current = enumerator.Current;
					result = current.Value;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
