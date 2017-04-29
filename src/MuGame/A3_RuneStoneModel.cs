using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_RuneStoneModel : ModelBase<A3_RuneStoneModel>
	{
		public int nowstamina = 50;

		public int nowlv = 1;

		public int nowexp = 0;

		public Dictionary<uint, a3_BagItemData> dressup_runestones = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<uint, a3_BagItemData> getHaveRunestones()
		{
			return ModelBase<a3_BagModel>.getInstance().getRunestonrs();
		}

		public a3_BagItemData getA3_BagItemDataById(uint id)
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(id);
			a3_BagItemData result;
			if (flag)
			{
				result = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
			}
			else
			{
				result = default(a3_BagItemData);
			}
			return result;
		}

		public void initDressupInfos(List<Variant> arr)
		{
			foreach (Variant current in arr)
			{
				this.dressup_runestones[this.DressupInfos(current).id] = this.DressupInfos(current);
			}
		}

		public a3_BagItemData DressupInfos(Variant data)
		{
			a3_BagItemData a3_BagItemData = default(a3_BagItemData);
			a3_BagItemData.id = data["fushi"]["id"];
			a3_BagItemData.tpid = data["fushi"]["tpid"];
			a3_BagItemData.isrunestone = true;
			bool flag = data["fushi"].ContainsKey("mark");
			if (flag)
			{
				a3_BagItemData.ismark = data["fushi"]["mark"];
			}
			bool flag2 = data["fushi"].ContainsKey("stone_att");
			if (flag2)
			{
				foreach (Variant current in data["fushi"]["stone_att"]._arr)
				{
					a3_BagItemData.runestonedata.runeston_att = new Dictionary<int, int>();
					int key = current["att_type"];
					int value = current["att_value"];
					a3_BagItemData.runestonedata.runeston_att[key] = value;
				}
			}
			return a3_BagItemData;
		}

		public int getNeedMoney(int stone_id)
		{
			return XMLMgr.instance.GetSXML("item.rune_stone", "id==" + stone_id).getInt("money");
		}

		public int nowStamina(int v)
		{
			this.nowstamina = v;
			return Mathf.Clamp(this.nowstamina, 0, 50);
		}

		public int nowLv(int lv)
		{
			this.nowlv = lv;
			return Mathf.Clamp(this.nowlv, 1, 10);
		}

		public int nowExp(int exp)
		{
			this.nowexp = exp;
			return this.nowexp;
		}
	}
}
