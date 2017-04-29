using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class BagProxy : BaseProxy<BagProxy>
	{
		public static uint EVENT_USE_ADD_HEROEXP = 0u;

		public static uint EVENT_USE_GETGIFT = 1u;

		public static uint EVENT_LOAD_BAG = 2u;

		public static uint EVENT_ITME_SELL = 3u;

		public static uint EVENT_ITEM_BUY = 4u;

		public static uint EVENT_ITEM_CHANGE = 5u;

		public static uint EVENT_OPEN_BAGLOCK = 6u;

		public static uint EVENT_OPEN_HOUSELOCK = 7u;

		public static uint EVENT_ROOM_TURN = 8u;

		public static uint EVENT_USE_DYE = 9u;

		private bool isRanse = false;

		public BagProxy()
		{
			this.addProxyListener(65u, new Action<Variant>(this.onLoadItems));
			this.addProxyListener(63u, new Action<Variant>(this.onSellItems));
			this.addProxyListener(68u, new Action<Variant>(this.onUseItems));
			this.addProxyListener(75u, new Action<Variant>(this.onItemChange));
			this.addProxyListener(48u, new Action<Variant>(this.onItemCd));
		}

		private void onItemChange(Variant data)
		{
			debug.Log(data.dump() + ":::::::::::::::");
			bool flag = data.ContainsKey("money") || data.ContainsKey("yb") || data.ContainsKey("bndyb");
			if (flag)
			{
				bool flag2 = data.ContainsKey("money");
				if (flag2)
				{
					bool flag3 = data["money"] > ModelBase<PlayerModel>.getInstance().money;
					if (flag3)
					{
						flytxt.instance.fly("金币+ " + (data["money"] - ModelBase<PlayerModel>.getInstance().money), 0, default(Color), null);
						bool flag4 = a3_insideui_fb.instance != null && data.Count == 1;
						if (flag4)
						{
							a3_insideui_fb.instance.SetInfMoney((int)(data["money"] - ModelBase<PlayerModel>.getInstance().money));
						}
					}
					else
					{
						debug.Log("消耗金币" + (ModelBase<PlayerModel>.getInstance().money - data["money"]));
						skill_a3.upgold = (int)(ModelBase<PlayerModel>.getInstance().money - data["money"]);
					}
					ModelBase<PlayerModel>.getInstance().money = data["money"];
				}
				bool flag5 = data.ContainsKey("yb");
				if (flag5)
				{
					uint gold = ModelBase<PlayerModel>.getInstance().gold;
					ModelBase<PlayerModel>.getInstance().gold = data["yb"];
					bool flag6 = gold < data["yb"] && HttpAppMgr.instance != null && HttpAppMgr.instance.giftCard != null;
					if (flag6)
					{
						HttpAppMgr.instance.giftCard.getFirstRechangeCard();
						HttpAppMgr.instance.giftCard.getRechangeCard();
					}
				}
				bool flag7 = data.ContainsKey("bndyb");
				if (flag7)
				{
					ModelBase<PlayerModel>.getInstance().gift = data["bndyb"];
				}
				UIClient.instance.dispatchEvent(GameEvent.Create(9005u, this, data, false));
			}
			bool flag8 = true;
			bool flag9 = data.ContainsKey("new_itm");
			if (flag9)
			{
				flag8 = data["new_itm"];
			}
			bool flag10 = data.ContainsKey("add");
			if (flag10)
			{
				int num = 0;
				Variant variant = data["add"];
				foreach (Variant current in variant._arr)
				{
					a3_BagItemData a3_BagItemData = default(a3_BagItemData);
					a3_BagItemData.id = current["id"];
					a3_BagItemData.tpid = current["tpid"];
					a3_BagItemData.num = current["cnt"];
					a3_BagItemData.bnd = current["bnd"];
					bool flag11 = ModelBase<a3_BagModel>.getInstance().Items.ContainsKey(current["id"]);
					if (flag11)
					{
						num = ModelBase<a3_BagModel>.getInstance().Items[current["id"]].num;
					}
					bool flag12 = current.ContainsKey("mark");
					if (flag12)
					{
						a3_BagItemData.ismark = current["mark"];
					}
					a3_BagItemData.isEquip = false;
					bool flag13 = flag8;
					if (flag13)
					{
						a3_BagItemData.isNew = true;
					}
					else
					{
						a3_BagItemData.isNew = false;
					}
					bool flag14 = current.ContainsKey("stone_att");
					if (flag14)
					{
						a3_BagItemData.isrunestone = true;
						foreach (Variant current2 in current["stone_att"]._arr)
						{
							a3_BagItemData.runestonedata.runeston_att = new Dictionary<int, int>();
							int key = current2["att_type"];
							int value = current2["att_value"];
							a3_BagItemData.runestonedata.runeston_att[key] = value;
						}
					}
					else
					{
						a3_BagItemData.isrunestone = false;
					}
					bool flag15 = current.ContainsKey("intensify_lv");
					if (flag15)
					{
						a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, current);
					}
					a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummonData(a3_BagItemData, current);
					ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
					bool flag16 = off_line_exp.instance != null;
					if (flag16)
					{
						bool offline = off_line_exp.instance.offline;
						if (offline)
						{
							off_line_exp.instance.offline_item.Add(a3_BagItemData);
							bool flag17 = !off_line_exp.instance.fenjie.isOn;
							if (flag17)
							{
								flytxt.instance.fly("获得装备：" + ModelBase<a3_BagModel>.getInstance().getItemXml(current["tpid"]).getString("item_name"), 0, default(Color), null);
							}
							else
							{
								flytxt.instance.fly(string.Concat(new object[]
								{
									"获得道具：",
									ModelBase<a3_BagModel>.getInstance().getItemXml(current["tpid"]).getString("item_name"),
									"x",
									current["cnt"] - num
								}), 0, default(Color), null);
							}
						}
					}
				}
				bool flag18 = a3_role.instan != null;
				if (flag18)
				{
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_USE_DYE, this, data, false));
				}
			}
			bool flag19 = data.ContainsKey("modcnts");
			if (flag19)
			{
				int num2 = 0;
				Variant variant2 = data["modcnts"];
				foreach (Variant current3 in variant2._arr)
				{
					a3_BagItemData a3_BagItemData2 = default(a3_BagItemData);
					a3_BagItemData2.id = current3["id"];
					a3_BagItemData2.tpid = current3["tpid"];
					a3_BagItemData2.num = current3["cnt"];
					a3_BagItemData2.isEquip = false;
					a3_BagItemData2.isNew = false;
					bool flag20 = ModelBase<a3_BagModel>.getInstance().Items.ContainsKey(current3["id"]);
					if (flag20)
					{
						num2 = ModelBase<a3_BagModel>.getInstance().Items[current3["id"]].num;
					}
					bool flag21 = current3.ContainsKey("intensify_lv");
					if (flag21)
					{
						a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData2, current3);
					}
					bool flag22 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(a3_BagItemData2.id);
					if (flag22)
					{
						int num3 = ModelBase<a3_BagModel>.getInstance().getItems(false)[a3_BagItemData2.id].num;
					}
					ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData2);
					bool flag23 = current3["tpid"] == 1540;
					if (flag23)
					{
						skill_a3.upmojing = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
					}
					bool flag24 = off_line_exp.instance != null;
					if (flag24)
					{
						bool offline2 = off_line_exp.instance.offline;
						if (offline2)
						{
							off_line_exp.instance.offline_item.Add(a3_BagItemData2);
							flytxt.instance.fly(string.Concat(new object[]
							{
								"获得道具：",
								ModelBase<a3_BagModel>.getInstance().getItemXml(current3["tpid"]).getString("item_name"),
								"x",
								current3["cnt"] - num2
							}), 0, default(Color), null);
						}
					}
				}
			}
			bool flag25 = data.ContainsKey("rmvids");
			if (flag25)
			{
				Variant variant3 = data["rmvids"];
				using (List<Variant>.Enumerator enumerator4 = variant3._arr.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						uint id = enumerator4.Current;
						ModelBase<a3_BagModel>.getInstance().removeItem(id);
					}
				}
			}
			bool flag26 = data.ContainsKey("rmvids") || data.ContainsKey("add") || data.ContainsKey("modcnts");
			if (flag26)
			{
				base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_ITEM_CHANGE, this, data, false));
				bool flag27 = this.isRanse;
				if (flag27)
				{
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_USE_DYE, this, data, false));
					this.isRanse = false;
				}
			}
			bool flag28 = data.ContainsKey("nobpt");
			if (flag28)
			{
				ModelBase<PlayerModel>.getInstance().nobpt = data["nobpt"];
			}
			bool flag29 = data.ContainsKey("energy");
			if (flag29)
			{
				MapModel.getInstance().energy = data["energy"];
			}
		}

		public void onItemCd(Variant data)
		{
			bool flag = data.ContainsKey("itemcds");
			if (flag)
			{
				foreach (Variant current in data["itemcd"]._arr)
				{
					int num = current["cdtp"];
					float num2 = current["cdtm"];
				}
			}
			bool flag2 = data.ContainsKey("cd_type");
			if (flag2)
			{
				int num3 = data["cd_type"];
				float time = data["cd"];
				bool flag3 = num3 == 4;
				if (flag3)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/use_hp", false, null);
				}
				ModelBase<a3_BagModel>.getInstance().addItemCd(num3, time);
			}
		}

		public void sendLoadItems(int val)
		{
			Variant variant = new Variant();
			variant["option"] = val;
			this.sendRPC(65u, variant);
		}

		public void sendMark(uint id)
		{
			Variant variant = new Variant();
			variant["option"] = 6;
			variant["id"] = id;
			this.sendRPC(65u, variant);
		}

		public void sendOpenLock(int type, int num, bool use_yb)
		{
			Variant variant = new Variant();
			variant["option"] = type;
			variant["unlock_num"] = num;
			variant["use_yb"] = use_yb;
			this.sendRPC(65u, variant);
		}

		public void sendRoomItems(bool pack_to_cangku, uint id, int num)
		{
			Variant variant = new Variant();
			variant["option"] = 4;
			variant["pack_to_cangku"] = pack_to_cangku;
			variant["item_id"] = id;
			variant["item_num"] = num;
			this.sendRPC(65u, variant);
		}

		public void sendBuyItems(int tp, int id = 0, int num = 0)
		{
			Variant variant = new Variant();
			bool flag = tp == 2;
			if (flag)
			{
				variant["tp"] = 2;
				variant["id"] = id;
				variant["cnt"] = num;
			}
			bool flag2 = tp == 1;
			if (flag2)
			{
				variant["tp"] = 1;
			}
			this.sendRPC(62u, variant);
		}

		public void sendSellItems(uint id, int num)
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["id"] = id;
			variant["num"] = num;
			this.sendRPC(63u, variant);
		}

		public void sendSellItems(List<Variant> id)
		{
			Variant variant = new Variant();
			variant["sell_items"] = new Variant();
			variant["op"] = 2;
			for (int i = 0; i < id.Count; i++)
			{
				variant["sell_items"].pushBack(id[i]);
			}
			this.sendRPC(63u, variant);
		}

		public void sendUseItems(uint id, int num)
		{
			Variant variant = new Variant();
			variant["id"] = id;
			variant["num"] = num;
			this.sendRPC(68u, variant);
		}

		public void sendUseHeroExp(string itemid, int heroid)
		{
			Variant variant = new Variant();
			variant["tpid"] = itemid;
			variant["heroexp_hero_id"] = heroid;
			this.sendRPC(68u, variant);
		}

		public void onSellItems(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (!flag)
			{
				bool flag2 = num == 1;
				if (flag2)
				{
					uint num2 = data["id"];
					uint num3 = data["earn"];
					MediaClient.instance.PlaySoundUrl("audio/common/sold_coin", false, null);
				}
				else
				{
					bool flag3 = num == 2;
					if (flag3)
					{
						uint num4 = data["earn"];
						MediaClient.instance.PlaySoundUrl("audio/common/sold_coin", false, null);
						bool flag4 = a3_bag.indtans != null;
						if (flag4)
						{
							a3_bag.indtans.refresh_Sell();
						}
						bool flag5 = InterfaceMgr.getInstance().checkWinOpened(InterfaceMgr.A3_SMITHY) && A3_Smithy.Instance != null;
						if (flag5)
						{
							A3_Smithy.Instance.refresh_Sell();
						}
					}
				}
				a3_BagModel expr_E2 = ModelBase<a3_BagModel>.getInstance();
				if (expr_E2 != null)
				{
					expr_E2.SellItem();
				}
				base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_ITME_SELL, this, data, false));
			}
		}

		public void onBuyItems(Variant data)
		{
		}

		public void onLoadItems(Variant data)
		{
			Debug.Log("包裹On_C#" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				switch (num)
				{
				case 0:
				{
					ModelBase<a3_BagModel>.getInstance().item_num.Clear();
					ModelBase<a3_BagModel>.getInstance().getItems(false).Clear();
					ModelBase<a3_BagModel>.getInstance().curi = data["curi"];
					Variant variant = data["items"];
					foreach (Variant current in variant._arr)
					{
						a3_BagItemData a3_BagItemData = default(a3_BagItemData);
						a3_BagItemData.id = current["id"];
						a3_BagItemData.tpid = current["tpid"];
						a3_BagItemData.num = current["cnt"];
						a3_BagItemData.bnd = current["bnd"];
						a3_BagItemData.ismark = current["mark"];
						a3_BagItemData.isEquip = false;
						a3_BagItemData.isNew = false;
						bool flag2 = current.ContainsKey("intensify_lv");
						if (flag2)
						{
							a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, current);
						}
						bool flag3 = current.ContainsKey("stone_att");
						if (flag3)
						{
							a3_BagItemData.isrunestone = true;
							foreach (Variant current2 in current["stone_att"]._arr)
							{
								a3_BagItemData.runestonedata.runeston_att = new Dictionary<int, int>();
								int key = current2["att_type"];
								int value = current2["att_value"];
								a3_BagItemData.runestonedata.runeston_att[key] = value;
							}
						}
						else
						{
							a3_BagItemData.isrunestone = false;
						}
						a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().GetSummonData(a3_BagItemData, current);
						ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
						bool flag4 = ModelBase<a3_BagModel>.getInstance().item_num.ContainsKey(a3_BagItemData.tpid);
						if (flag4)
						{
							a3_BagItemData.num = ModelBase<a3_BagModel>.getInstance().item_num[a3_BagItemData.tpid].num + a3_BagItemData.num;
							ModelBase<a3_BagModel>.getInstance().item_num.Remove(a3_BagItemData.tpid);
							ModelBase<a3_BagModel>.getInstance().item_num.Add(a3_BagItemData.tpid, a3_BagItemData);
						}
						else
						{
							ModelBase<a3_BagModel>.getInstance().item_num.Add(a3_BagItemData.tpid, a3_BagItemData);
						}
						bool flag5 = a3_legion_info.mInstance != null;
						if (flag5)
						{
							a3_legion_info.mInstance.buff_up();
						}
					}
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_LOAD_BAG, this, null, false));
					break;
				}
				case 1:
				{
					ModelBase<a3_BagModel>.getInstance().getHouseItems().Clear();
					ModelBase<a3_BagModel>.getInstance().house_curi = data["curi"];
					Variant variant2 = data["items"];
					foreach (Variant current3 in variant2._arr)
					{
						a3_BagItemData a3_BagItemData2 = default(a3_BagItemData);
						a3_BagItemData2.id = current3["id"];
						a3_BagItemData2.tpid = current3["tpid"];
						a3_BagItemData2.num = current3["cnt"];
						a3_BagItemData2.bnd = current3["bnd"];
						a3_BagItemData2.ismark = current3["mark"];
						a3_BagItemData2.isEquip = false;
						bool flag6 = current3.ContainsKey("intensify_lv");
						if (flag6)
						{
							a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData2, current3);
						}
						ModelBase<a3_BagModel>.getInstance().addHouseItem(a3_BagItemData2);
					}
					InterfaceMgr.getInstance().openUiFirstTime();
					break;
				}
				case 2:
				{
					ModelBase<a3_BagModel>.getInstance().curi = data["unlock_num"];
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_OPEN_BAGLOCK, this, null, false));
					bool flag7 = a3_expbar.instance != null;
					if (flag7)
					{
						a3_expbar.instance.bag_Count();
					}
					break;
				}
				case 3:
					ModelBase<a3_BagModel>.getInstance().house_curi = data["unlock_num"];
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_OPEN_HOUSELOCK, this, null, false));
					break;
				case 4:
				{
					bool flag8 = data.ContainsKey("add");
					if (flag8)
					{
						Variant variant3 = data["add"];
						foreach (Variant current4 in variant3._arr)
						{
							a3_BagItemData a3_BagItemData3 = default(a3_BagItemData);
							a3_BagItemData3.id = current4["id"];
							a3_BagItemData3.tpid = current4["tpid"];
							a3_BagItemData3.num = current4["cnt"];
							a3_BagItemData3.bnd = current4["bnd"];
							a3_BagItemData3.isEquip = false;
							bool flag9 = current4.ContainsKey("intensify_lv");
							if (flag9)
							{
								a3_BagItemData3 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData3, current4);
							}
							ModelBase<a3_BagModel>.getInstance().addHouseItem(a3_BagItemData3);
						}
					}
					bool flag10 = data.ContainsKey("modcnts");
					if (flag10)
					{
						Variant variant4 = data["modcnts"];
						foreach (Variant current5 in variant4._arr)
						{
							a3_BagItemData a3_BagItemData4 = default(a3_BagItemData);
							a3_BagItemData4.id = current5["id"];
							a3_BagItemData4.tpid = current5["tpid"];
							a3_BagItemData4.num = current5["cnt"];
							a3_BagItemData4.isEquip = false;
							bool flag11 = current5.ContainsKey("intensify_lv");
							if (flag11)
							{
								a3_BagItemData4 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData4, current5);
							}
							ModelBase<a3_BagModel>.getInstance().addHouseItem(a3_BagItemData4);
						}
					}
					bool flag12 = data.ContainsKey("rmvids");
					if (flag12)
					{
						Variant variant5 = data["rmvids"];
						using (List<Variant>.Enumerator enumerator6 = variant5._arr.GetEnumerator())
						{
							while (enumerator6.MoveNext())
							{
								uint id = enumerator6.Current;
								ModelBase<a3_BagModel>.getInstance().removeHouseItem(id);
							}
						}
					}
					base.dispatchEvent(GameEvent.Create(BagProxy.EVENT_ROOM_TURN, this, data, false));
					break;
				}
				case 6:
				{
					uint num2 = data["id"];
					a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num2);
					equipByAll.ismark = data["mark"];
					bool flag13 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num2);
					if (flag13)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll);
					}
					bool flag14 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num2);
					if (flag14)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll);
					}
					ModelBase<a3_BagModel>.getInstance().isFirstMark = false;
					bool flag15 = a3_equiptip.instans;
					if (flag15)
					{
						a3_equiptip.instans.IsfirstMark();
					}
					bool flag16 = a3_bag.indtans;
					if (flag16)
					{
						a3_bag.indtans.refreshMark(num2);
					}
					break;
				}
				}
			}
		}

		public void onUseItems(Variant data)
		{
			int num = data["res"];
			Debug.Log("使用" + data.dump());
			bool flag = num <= 0;
			if (flag)
			{
				bool flag2 = num == -1101;
				if (flag2)
				{
					flytxt.instance.fly("没有足够的背包空间了", 0, default(Color), null);
				}
				bool flag3 = num == -903;
				if (flag3)
				{
					flytxt.instance.fly("现在还不能使用", 0, default(Color), null);
				}
				bool flag4 = num == -539;
				if (flag4)
				{
					flytxt.instance.fly("您已经有该宠物了", 0, default(Color), null);
				}
				bool flag5 = num == -969;
				if (flag5)
				{
					flytxt.instance.fly("道具使用次数不足", 0, default(Color), null);
				}
				bool flag6 = num == -899;
				if (flag6)
				{
					flytxt.instance.fly("等级不足", 0, default(Color), null);
				}
				bool flag7 = num == -2302;
				if (flag7)
				{
					flytxt.instance.fly("使用失败，经验值已达上限", 0, default(Color), null);
				}
				bool flag8 = num == -9002;
				if (flag8)
				{
					flytxt.instance.fly("使用失败，生命值已满", 0, default(Color), null);
				}
				bool flag9 = num == -220;
				if (flag9)
				{
					flytxt.instance.fly("使用失败，没有可以重置的属性点", 0, default(Color), null);
				}
			}
			else
			{
				bool flag10 = num == 1;
				if (flag10)
				{
					bool flag11 = data["tpid"] == 1528;
					if (flag11)
					{
						flytxt.instance.fly("使用成功，已增加1小时的双倍经验时间", 0, default(Color), null);
					}
					bool flag12 = data["tpid"] >= 1511 && data["tpid"] <= 1515;
					if (flag12)
					{
						int num2 = data["tpid"];
						string str = "";
						switch (num2)
						{
						case 1511:
							str = "力量属性点";
							break;
						case 1512:
							str = "敏捷属性点";
							break;
						case 1513:
							str = "体力属性点";
							break;
						case 1514:
							str = "魔力属性点";
							break;
						case 1515:
							str = "智慧属性点";
							break;
						}
						flytxt.instance.fly("使用成功，已经重置" + data["cnt"] + "点" + str, 0, default(Color), null);
					}
				}
				bool flag13 = num == 19;
				if (flag13)
				{
					uint key = data["id"];
					a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[key];
					a3_BagItemData.equipdata.color = data["colour"];
					ModelBase<a3_EquipModel>.getInstance().addEquip(a3_BagItemData);
					ModelBase<a3_EquipModel>.getInstance().equipColor_on(data["colour"]);
					bool flag14 = a3_BagItemData.equipdata.color > 0u;
					if (flag14)
					{
						string str2 = ModelBase<a3_BagModel>.getInstance().getItemXml((int)a3_BagItemData.equipdata.color).getString("item_name").Substring(0, 2);
						flytxt.instance.fly("使用成功，装备已染成漂亮的" + str2 + "!", 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("使用成功，装备已呈现出原有的光泽了！", 0, default(Color), null);
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_DYETIP);
					this.isRanse = true;
				}
				bool flag15 = num == 2;
				if (flag15)
				{
				}
				bool flag16 = num == 13;
				if (flag16)
				{
					bool flag17 = data.ContainsKey("money");
					if (flag17)
					{
					}
					bool flag18 = data.ContainsKey("itms");
					if (flag18)
					{
						Variant variant = data["itms"];
						foreach (Variant current in variant._arr)
						{
							flytxt.instance.fly("获得道具：" + ModelBase<a3_BagModel>.getInstance().getItemXml(current["tpid"]).getString("item_name") + "x" + current["cnt"], 0, default(Color), null);
						}
					}
					bool flag19 = data.ContainsKey("eqps");
					if (flag19)
					{
						Variant variant2 = data["eqps"];
						foreach (Variant current2 in variant2._arr)
						{
							flytxt.instance.fly("获得装备：" + ModelBase<a3_BagModel>.getInstance().getItemXml(current2["tpid"]).getString("item_name"), 0, default(Color), null);
						}
					}
				}
			}
		}
	}
}
