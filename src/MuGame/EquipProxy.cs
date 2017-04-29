using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class EquipProxy : BaseProxy<EquipProxy>
	{
		public static uint EVENT_EQUIP_PUTON = 1u;

		public static uint EVENT_EQUIP_PUTDOWN = 2u;

		public static uint EVENT_EQUIP_STRENGTH = 3u;

		public static uint EVENT_EQUIP_ADVANCE = 4u;

		public static uint EVENT_EQUIP_ADDATTR = 7u;

		public static uint EVENT_EQUIP_INHERIT = 6u;

		public static uint EVENT_EQUIP_GEM_UP = 8u;

		public static uint EVENT_CHANGE_ATT = 9u;

		public static uint EVENT_DO_CHANGE_ATT = 10u;

		public static uint EVENT_BAOSHI = 13u;

		public static uint EVENT_SMITHY = 14u;

		public static uint ONEQUIP = 11u;

		public static uint EVENT_BAOSHI_HC = 12u;

		public EquipProxy()
		{
			this.addProxyListener(66u, new Action<Variant>(this.onEquip));
		}

		public void sendChangeEquip(uint id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 1;
			variant["id"] = id;
			this.sendRPC(66u, variant);
		}

		public void sendAdvance(uint id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 2;
			variant["id"] = id;
			this.sendRPC(66u, variant);
		}

		public void sendsell(List<uint> id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 5;
			variant["ids"] = new Variant();
			for (int i = 0; i < id.Count; i++)
			{
				variant["ids"].pushBack(id[i]);
			}
			this.sendRPC(66u, variant);
		}

		public void sendsell_one(uint id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 4;
			variant["id"] = id;
			this.sendRPC(66u, variant);
			debug.Log("HHH" + variant.dump());
		}

		public void send_Changebaoshi(uint id, uint type)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 13;
			variant["eqp_id"] = id;
			variant["gem_id"] = type;
			debug.Log(string.Concat(new object[]
			{
				"eqpid",
				id,
				"gemid",
				type
			}));
			this.sendRPC(66u, variant);
		}

		public void send_outBaoshi(uint id, uint key)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 12;
			variant["id"] = id;
			variant["idx"] = key;
			this.sendRPC(66u, variant);
		}

		public void send_hcBaoshi(uint id, uint num)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 11;
			variant["tar_id"] = id;
			variant["num"] = num;
			this.sendRPC(66u, variant);
		}

		public void sendAddAttr(uint id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 7;
			variant["id"] = id;
			this.sendRPC(66u, variant);
		}

		public void sendInherit(uint frm_id, uint to_id, int type, bool cost_yb)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 6;
			variant["frm_id"] = frm_id;
			variant["to_id"] = to_id;
			variant["type"] = type;
			variant["cost_yb"] = cost_yb;
			this.sendRPC(66u, variant);
		}

		public void sendGemUp(uint id, int type)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 8;
			variant["id"] = id;
			variant["type"] = type;
			this.sendRPC(66u, variant);
		}

		public void sendStrengthEquip(uint id)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 3;
			variant["id"] = id;
			this.sendRPC(66u, variant);
		}

		public void sendChangeAtt(uint id, int type)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 9;
			variant["id"] = id;
			variant["type"] = type;
			this.sendRPC(66u, variant);
		}

		public void sendDoChangeAtt(uint id, bool replace)
		{
			Variant variant = new Variant();
			variant["eqp_cmd"] = 10;
			variant["id"] = id;
			variant["replace"] = replace;
			this.sendRPC(66u, variant);
		}

		public void onEquip(Variant data)
		{
			debug.Log("装" + data.dump());
			int num = data["res"];
			bool flag = num <= 0;
			if (flag)
			{
				bool flag2 = num == -4007;
				if (flag2)
				{
					flytxt.instance.fly("道具不足,无法进行强化", 0, default(Color), null);
				}
				else
				{
					debug.Log("错误码：" + num);
					Globle.err_output(num);
				}
			}
			else
			{
				switch (num)
				{
				case 1:
				{
					bool flag3 = data.ContainsKey("eqpinfo");
					if (flag3)
					{
						ModelBase<a3_EquipModel>.getInstance().initEquipOne(data);
						base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_PUTON, this, data, false));
					}
					else
					{
						int num2 = data["part_id"];
						ModelBase<a3_EquipModel>.getInstance().unEquipOneByPart(data["part_id"]);
						base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_PUTDOWN, this, data, false));
						base.dispatchEvent(GameEvent.Create(EquipProxy.ONEQUIP, this, data, false));
					}
					bool flag4 = a3_bag.indtans;
					if (flag4)
					{
						a3_bag.indtans.refreshQHdashi();
						a3_bag.indtans.refreshLHlianjie();
					}
					break;
				}
				case 2:
				{
					bool flag5 = data.ContainsKey("id");
					if (flag5)
					{
						uint num3 = data["id"];
						a3_BagItemData equipByAll = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num3);
						equipByAll.equipdata.stage = data["stage"];
						equipByAll.equipdata.intensify_lv = data["intensify_lv"];
						bool flag6 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num3);
						if (flag6)
						{
							ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll);
						}
						bool flag7 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num3);
						if (flag7)
						{
							ModelBase<a3_BagModel>.getInstance().addItem(equipByAll);
						}
						bool flag8 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(equipByAll.id);
						if (flag8)
						{
							ModelBase<a3_EquipModel>.getInstance().equipModel_on(equipByAll);
						}
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_ADVANCE, this, data, false));
					break;
				}
				case 3:
				{
					uint num4 = data["id"];
					a3_BagItemData equipByAll2 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num4);
					equipByAll2.equipdata.intensify_lv = data["intensify_lv"];
					bool flag9 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num4);
					if (flag9)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll2);
					}
					bool flag10 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num4);
					if (flag10)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll2);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_STRENGTH, this, data, false));
					break;
				}
				case 4:
				{
					bool flag11 = a3_bag.indtans != null;
					if (flag11)
					{
						a3_bag.indtans.refresh();
					}
					break;
				}
				case 5:
				{
					bool flag12 = a3_equipsell._instance != null;
					if (flag12)
					{
						a3_equipsell._instance.refresh();
					}
					bool flag13 = a3_bag.indtans != null;
					if (flag13)
					{
						a3_bag.indtans.refresh();
					}
					break;
				}
				case 6:
				{
					Variant variant = data["frm_eqpinfo"];
					uint num5 = variant["id"];
					a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num5);
					a3_BagItemData.id = variant["id"];
					a3_BagItemData.tpid = variant["tpid"];
					a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, variant);
					bool flag14 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num5);
					if (flag14)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(a3_BagItemData);
					}
					bool flag15 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num5);
					if (flag15)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
					}
					Variant variant2 = data["to_eqpinfo"];
					uint num6 = variant2["id"];
					a3_BagItemData a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num6);
					a3_BagItemData2.id = variant2["id"];
					a3_BagItemData2.tpid = variant2["tpid"];
					a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData2, variant2);
					bool flag16 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num6);
					if (flag16)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(a3_BagItemData2);
					}
					bool flag17 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num6);
					if (flag17)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData2);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_INHERIT, this, data, false));
					break;
				}
				case 7:
				{
					uint num7 = data["id"];
					a3_BagItemData equipByAll3 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num7);
					equipByAll3.equipdata.add_exp = data["add_exp"];
					bool flag18 = equipByAll3.equipdata.add_level != data["add_level"];
					if (flag18)
					{
						data["do_add_level_up"] = true;
					}
					else
					{
						data["do_add_level_up"] = false;
					}
					equipByAll3.equipdata.add_level = data["add_level"];
					bool flag19 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num7);
					if (flag19)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll3);
					}
					bool flag20 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num7);
					if (flag20)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll3);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_ADDATTR, this, data, false));
					break;
				}
				case 8:
				{
					uint num8 = data["id"];
					a3_BagItemData equipByAll4 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num8);
					bool flag21 = data.ContainsKey("gem_att");
					if (flag21)
					{
						int val = 0;
						int val2 = 0;
						Dictionary<int, int> dictionary = new Dictionary<int, int>();
						foreach (int current in equipByAll4.equipdata.gem_att.Keys)
						{
							dictionary[current] = equipByAll4.equipdata.gem_att[current];
						}
						equipByAll4.equipdata.gem_att = new Dictionary<int, int>();
						Variant variant3 = data["gem_att"];
						foreach (Variant current2 in variant3._arr)
						{
							int num9 = current2["att_type"];
							int num10 = current2["att_value"];
							bool flag22 = num10 != dictionary[num9];
							if (flag22)
							{
								val = num10 - dictionary[num9];
								val2 = num9;
							}
							equipByAll4.equipdata.gem_att[num9] = num10;
						}
						data["att_type"] = val2;
						data["att_value"] = val;
					}
					bool flag23 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num8);
					if (flag23)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll4);
					}
					bool flag24 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num8);
					if (flag24)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll4);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_EQUIP_GEM_UP, this, data, false));
					break;
				}
				case 9:
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_CHANGE_ATT, this, data, false));
					break;
				case 10:
				{
					uint num11 = data["id"];
					a3_BagItemData equipByAll5 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num11);
					bool flag25 = data.ContainsKey("subjoin_att");
					if (flag25)
					{
						equipByAll5.equipdata.subjoin_att = new Dictionary<int, int>();
						Variant variant4 = data["subjoin_att"];
						foreach (Variant current3 in variant4._arr)
						{
							int key = current3["att_type"];
							int value = current3["att_value"];
							equipByAll5.equipdata.subjoin_att[key] = value;
						}
					}
					bool flag26 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num11);
					if (flag26)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll5);
					}
					bool flag27 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num11);
					if (flag27)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll5);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_DO_CHANGE_ATT, this, data, false));
					break;
				}
				case 12:
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_BAOSHI_HC, this, data, false));
					break;
				case 13:
				{
					uint num12 = data["id"];
					a3_BagItemData equipByAll6 = ModelBase<a3_EquipModel>.getInstance().getEquipByAll(num12);
					bool flag28 = data.ContainsKey("gem_att2");
					if (flag28)
					{
						equipByAll6.equipdata.baoshi = new Dictionary<int, int>();
						Variant variant5 = data["gem_att2"];
						int num13 = 0;
						foreach (Variant current4 in variant5._arr)
						{
							int value2 = current4["tpid"];
							equipByAll6.equipdata.baoshi[num13] = value2;
							num13++;
						}
					}
					bool flag29 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num12);
					if (flag29)
					{
						ModelBase<a3_EquipModel>.getInstance().addEquip(equipByAll6);
					}
					bool flag30 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num12);
					if (flag30)
					{
						ModelBase<a3_BagModel>.getInstance().addItem(equipByAll6);
					}
					base.dispatchEvent(GameEvent.Create(EquipProxy.EVENT_BAOSHI, this, data, false));
					break;
				}
				}
			}
		}
	}
}
