using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class a3_RuneStoneProxy : BaseProxy<a3_RuneStoneProxy>
	{
		public static uint WEARORUNLOAD = 1u;

		public static uint COMPOSE = 2u;

		public static uint DECOMPOSE = 3u;

		public static uint DECOMPOSES = 4u;

		public static uint INFOS = 5u;

		public static uint ADDRUNESTONEST = 6u;

		public a3_RuneStoneProxy()
		{
			this.addProxyListener(182u, new Action<Variant>(this.onLoadRunestone));
		}

		public void sendporxy(int res, int id = 0, int num = 0, List<uint> lst_runestones = null)
		{
			Variant variant = new Variant();
			variant["op"] = res;
			switch (res)
			{
			case 1:
				variant["id"] = id;
				break;
			case 2:
				variant["stone_tpid"] = id;
				variant["num"] = num;
				break;
			case 3:
				variant["id"] = id;
				break;
			case 4:
				variant["ids"] = new Variant();
				for (int i = 0; i < lst_runestones.Count; i++)
				{
					variant["ids"].pushBack(lst_runestones[i]);
					debug.Log(string.Concat(lst_runestones[i]));
				}
				break;
			case 6:
				variant["id"] = id;
				break;
			case 7:
				variant["id"] = id;
				break;
			}
			this.sendRPC(182u, variant);
		}

		public void onLoadRunestone(Variant data)
		{
			debug.Log("a3符石信息：" + data.dump());
			switch (data["res"])
			{
			case 1:
			{
				bool flag = data.ContainsKey("fushi");
				if (flag)
				{
					bool flag2 = a3_runestone._instance != null;
					if (flag2)
					{
						a3_BagItemData a3_BagItemData = ModelBase<A3_RuneStoneModel>.getInstance().DressupInfos(data);
						a3_runestone._instance.DressUp(a3_BagItemData, a3_BagItemData.id);
					}
				}
				else
				{
					bool flag3 = a3_runestone._instance != null;
					if (flag3)
					{
						a3_runestone._instance.DressDown(data["part_id"]);
					}
				}
				break;
			}
			case 2:
			{
				bool flag4 = data.ContainsKey("fushi_stamina");
				if (flag4)
				{
					ModelBase<A3_RuneStoneModel>.getInstance().nowStamina(data["fushi_stamina"]);
				}
				bool flag5 = data.ContainsKey("fushi_level");
				if (flag5)
				{
					bool flag6 = a3_runestone._instance != null && data["fushi_level"] != ModelBase<A3_RuneStoneModel>.getInstance().nowlv;
					if (flag6)
					{
						a3_runestone._instance.RefreScrollLv(data["fushi_level"]);
					}
					ModelBase<A3_RuneStoneModel>.getInstance().nowLv(data["fushi_level"]);
				}
				bool flag7 = data.ContainsKey("fushi_exp");
				if (flag7)
				{
					ModelBase<A3_RuneStoneModel>.getInstance().nowExp(data["fushi_exp"]);
				}
				base.dispatchEvent(GameEvent.Create(a3_RuneStoneProxy.INFOS, this, data, false));
				break;
			}
			case 3:
			{
				bool flag8 = a3_runestonetip._instance != null;
				if (flag8)
				{
					bool flag9 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(data["id"]);
					if (flag9)
					{
						a3_BagItemData data2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[data["id"]];
						data2.ismark = data["mark"];
						ModelBase<a3_BagModel>.getInstance().addItem(data2);
					}
					bool flag10 = a3_bag.isshow;
					if (flag10)
					{
						a3_bag.isshow.refreshMarkRuneStones(data["id"]);
					}
					bool flag11 = a3_runestone._instance != null;
					if (flag11)
					{
						a3_runestone._instance.RefreshMark(data["id"], data["mark"]);
					}
				}
				break;
			}
			default:
				Globle.err_output(data["res"]);
				break;
			}
		}
	}
}
