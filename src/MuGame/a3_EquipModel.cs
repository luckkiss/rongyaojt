using Cross;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class a3_EquipModel : ModelBase<a3_EquipModel>
	{
		[Flags]
		public enum EquipStrengthOption
		{
			None = 0,
			Intensify = 1,
			Stage = 2,
			Add = 4,
			Gem = 8
		}

		private Dictionary<uint, a3_BagItemData> Equips;

		private Dictionary<int, a3_BagItemData> Equips_byType;

		public static Dictionary<uint, Color> EQUIP_COLOR = new Dictionary<uint, Color>();

		public Dictionary<int, a3_BagItemData> active_eqp = new Dictionary<int, a3_BagItemData>();

		public Dictionary<int, int> eqp_type_act = new Dictionary<int, int>();

		public Dictionary<int, int> eqp_type_act_fanxiang = new Dictionary<int, int>();

		public Dictionary<int, int> eqp_att_act = new Dictionary<int, int>();

		public bool Attchange_wite = false;

		public a3_EquipModel()
		{
			this.Equips = new Dictionary<uint, a3_BagItemData>();
			this.Equips_byType = new Dictionary<int, a3_BagItemData>();
			SXML sXML = XMLMgr.instance.GetSXML("item.equip_color", "");
			List<SXML> nodeList = sXML.GetNodeList("color", null);
			bool flag = nodeList != null;
			if (flag)
			{
				foreach (SXML current in nodeList)
				{
					string[] array = current.getString("color").Split(new char[]
					{
						','
					});
					Color value = new Color(float.Parse(array[0]) / 255f, float.Parse(array[1]) / 255f, float.Parse(array[2]) / 255f);
					a3_EquipModel.EQUIP_COLOR[current.getUint("id")] = value;
				}
			}
			SXML sXML2 = XMLMgr.instance.GetSXML("activate_fun.activate_rule", "");
			List<SXML> nodeList2 = sXML2.GetNodeList("eqp_t", "");
			bool flag2 = nodeList2 != null;
			if (flag2)
			{
				foreach (SXML current2 in nodeList2)
				{
					this.eqp_type_act[current2.getInt("equip_type")] = current2.getInt("need_type");
					this.eqp_type_act_fanxiang[current2.getInt("need_type")] = current2.getInt("equip_type");
				}
			}
			SXML sXML3 = XMLMgr.instance.GetSXML("activate_fun.attribute_rule", "");
			List<SXML> nodeList3 = sXML3.GetNodeList("attribute", "");
			bool flag3 = nodeList3 != null;
			if (flag3)
			{
				foreach (SXML current3 in nodeList3)
				{
					this.eqp_att_act[current3.getInt("attribute_type")] = current3.getInt("need_attribute");
				}
			}
		}

		public Dictionary<uint, a3_BagItemData> getEquips()
		{
			return this.Equips;
		}

		public Dictionary<int, a3_BagItemData> getEquipsByType()
		{
			return this.Equips_byType;
		}

		public void initEquipList(List<Variant> arr)
		{
			foreach (Variant current in arr)
			{
				this.initEquipOne(current);
			}
		}

		public void initEquipOne(Variant data)
		{
			a3_BagItemData a3_BagItemData = default(a3_BagItemData);
			a3_BagItemData.id = data["eqpinfo"]["id"];
			a3_BagItemData.tpid = data["eqpinfo"]["tpid"];
			a3_BagItemData.num = data["eqpinfo"]["cnt"];
			a3_BagItemData.bnd = data["eqpinfo"]["bnd"];
			a3_BagItemData.isEquip = true;
			bool flag = data["eqpinfo"].ContainsKey("mark");
			if (flag)
			{
				a3_BagItemData.ismark = data["eqpinfo"]["mark"];
			}
			a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, data["eqpinfo"]);
			a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(a3_BagItemData.tpid);
			this.playAudio(a3_BagItemData);
			this.addEquip(a3_BagItemData);
		}

		public void unEquipOneByPart(int part)
		{
			a3_BagItemData a3_BagItemData = this.Equips_byType[part];
			this.Equips.Remove(a3_BagItemData.id);
			this.Equips_byType.Remove(part);
			this.playAudio(a3_BagItemData);
			bool flag = this.active_eqp.ContainsKey(a3_BagItemData.confdata.equip_type);
			if (flag)
			{
				this.active_eqp.Remove(a3_BagItemData.confdata.equip_type);
			}
			bool flag2 = this.Equips_byType.ContainsKey(this.eqp_type_act_fanxiang[a3_BagItemData.confdata.equip_type]);
			if (flag2)
			{
				bool flag3 = this.active_eqp.ContainsKey(this.Equips_byType[this.eqp_type_act_fanxiang[a3_BagItemData.confdata.equip_type]].confdata.equip_type);
				if (flag3)
				{
					this.active_eqp.Remove(this.Equips_byType[this.eqp_type_act_fanxiang[a3_BagItemData.confdata.equip_type]].confdata.equip_type);
				}
			}
			this.equipModel_down(a3_BagItemData);
		}

		private void playAudio(a3_BagItemData itemData)
		{
			switch (itemData.confdata.job_limit)
			{
			case 2:
			{
				bool flag = itemData.confdata.equip_type == 6;
				if (flag)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/equip_aex", false, null);
				}
				break;
			}
			case 3:
			{
				bool flag2 = itemData.confdata.equip_type == 6;
				if (flag2)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/equip_staff", false, null);
				}
				break;
			}
			case 5:
			{
				bool flag3 = itemData.confdata.equip_type == 6;
				if (flag3)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/equip_dagger", false, null);
				}
				break;
			}
			}
			bool flag4 = itemData.confdata.equip_type == 3;
			if (flag4)
			{
				MediaClient.instance.PlaySoundUrl("audio/common/equip_armour", false, null);
			}
		}

		public a3_BagItemData equipData_read(a3_BagItemData itemData, Variant item)
		{
			itemData.isEquip = true;
			bool flag = item.ContainsKey("colour");
			if (flag)
			{
				itemData.equipdata.color = item["colour"];
			}
			bool flag2 = item.ContainsKey("intensify_lv");
			if (flag2)
			{
				itemData.equipdata.intensify_lv = item["intensify_lv"];
			}
			bool flag3 = item.ContainsKey("add_level");
			if (flag3)
			{
				itemData.equipdata.add_level = item["add_level"];
			}
			bool flag4 = item.ContainsKey("add_exp");
			if (flag4)
			{
				itemData.equipdata.add_exp = item["add_exp"];
			}
			bool flag5 = item.ContainsKey("stage");
			if (flag5)
			{
				itemData.equipdata.stage = item["stage"];
			}
			bool flag6 = item.ContainsKey("blessing_lv");
			if (flag6)
			{
				itemData.equipdata.blessing_lv = item["blessing_lv"];
			}
			bool flag7 = item.ContainsKey("combpt");
			if (flag7)
			{
				itemData.equipdata.combpt = item["combpt"];
			}
			bool flag8 = item.ContainsKey("equip_level");
			if (flag8)
			{
				itemData.equipdata.eqp_level = item["equip_level"];
			}
			bool flag9 = item.ContainsKey("equip_type");
			if (flag9)
			{
				itemData.equipdata.eqp_type = item["equip_type"];
			}
			bool flag10 = item.ContainsKey("subjoin_att");
			if (flag10)
			{
				itemData.equipdata.subjoin_att = new Dictionary<int, int>();
				Variant variant = item["subjoin_att"];
				foreach (Variant current in variant._arr)
				{
					int key = current["att_type"];
					int value = current["att_value"];
					itemData.equipdata.subjoin_att[key] = value;
				}
			}
			bool flag11 = item.ContainsKey("gem_att");
			if (flag11)
			{
				itemData.equipdata.gem_att = new Dictionary<int, int>();
				Variant variant2 = item["gem_att"];
				foreach (Variant current2 in variant2._arr)
				{
					int key2 = current2["att_type"];
					int value2 = current2["att_value"];
					itemData.equipdata.gem_att[key2] = value2;
				}
			}
			bool flag12 = item.ContainsKey("gem_att2");
			if (flag12)
			{
				itemData.equipdata.baoshi = new Dictionary<int, int>();
				Variant variant3 = item["gem_att2"];
				int num = 0;
				foreach (Variant current3 in variant3._arr)
				{
					int value3 = current3["tpid"];
					itemData.equipdata.baoshi[num] = value3;
					num++;
				}
			}
			bool flag13 = item.ContainsKey("prefix_name");
			if (flag13)
			{
				SXML sXML = XMLMgr.instance.GetSXML("item", "");
				SXML node = sXML.GetNode("item", "id==" + itemData.tpid);
				SXML sXML2 = XMLMgr.instance.GetSXML("activate_fun.eqp", "eqp_type==" + node.getInt("equip_type"));
				SXML node2 = sXML2.GetNode("prefix_fun", "num==" + item["prefix_name"]);
				itemData.equipdata.attribute = node2.getInt("id");
				itemData.equipdata.att_type = node2.getInt("funtype");
				itemData.equipdata.att_value = item["att_value"];
			}
			return itemData;
		}

		public bool checkisSelfEquip(a3_ItemData data)
		{
			bool result = false;
			bool flag = ModelBase<PlayerModel>.getInstance().profession == data.job_limit || data.job_limit == 1;
			if (flag)
			{
				result = true;
			}
			return result;
		}

		public bool checkCanEquip(a3_BagItemData data)
		{
			return this.checkCanEquip(data.confdata, data.equipdata.stage, data.equipdata.blessing_lv);
		}

		public bool checkCanEquip(a3_ItemData data, int stage, int blessing_lv)
		{
			bool flag = false;
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + stage);
			bool flag2 = sXML == null;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				sXML = sXML.GetNode("stage_info", "itemid==" + data.tpid);
				string[] array = sXML.getString("equip_limit1").Split(new char[]
				{
					','
				});
				string[] array2 = sXML.getString("equip_limit2").Split(new char[]
				{
					','
				});
				int num = int.Parse(array[1]) * (100 - 3 * blessing_lv) / 100;
				int num2 = int.Parse(array2[1]) * (100 - 3 * blessing_lv) / 100;
				bool flag3 = num <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array[0])] && num2 <= ModelBase<PlayerModel>.getInstance().attr_list[uint.Parse(array2[0])] && (long)stage <= (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
				if (flag3)
				{
					flag = true;
				}
				result = flag;
			}
			return result;
		}

		public bool CanInherit(uint eqp1, uint eqp2)
		{
			a3_BagItemData equipByAll = this.getEquipByAll(eqp1);
			a3_BagItemData equipByAll2 = this.getEquipByAll(eqp2);
			bool flag = equipByAll.confdata.equip_type != equipByAll2.confdata.equip_type;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = equipByAll.equipdata.stage * 15 + equipByAll.equipdata.intensify_lv >= equipByAll2.equipdata.stage * 15 + equipByAll2.equipdata.intensify_lv;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = equipByAll.equipdata.add_level > equipByAll2.equipdata.add_level;
					result = !flag3;
				}
			}
			return result;
		}

		public void addEquip(a3_BagItemData data)
		{
			bool flag = this.Equips_byType.ContainsKey(data.confdata.equip_type);
			if (flag)
			{
				bool flag2 = this.CanInherit(data.id, this.Equips_byType[data.confdata.equip_type].id);
				if (flag2)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(this.Equips_byType[data.confdata.equip_type].id);
					arrayList.Add(data.id);
					this.Attchange_wite = true;
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQPINHERIT, arrayList, false);
				}
				bool flag3 = this.Equips_byType[data.confdata.equip_type].id != data.id;
				if (flag3)
				{
					ModelBase<a3_BagModel>.getInstance().gheqpData(this.Equips_byType[data.confdata.equip_type], data);
					bool flag4 = a3_bag.isshow;
					if (flag4)
					{
						a3_bag.indtans.ghuaneqp(this.Equips_byType[data.confdata.equip_type], data);
					}
				}
				this.Equips.Remove(this.Equips_byType[data.confdata.equip_type].id);
				bool flag5 = this.active_eqp.ContainsKey(data.confdata.equip_type);
				if (flag5)
				{
					this.active_eqp.Remove(data.confdata.equip_type);
				}
				bool flag6 = this.Equips_byType.ContainsKey(this.eqp_type_act_fanxiang[data.confdata.equip_type]);
				if (flag6)
				{
					bool flag7 = this.active_eqp.ContainsKey(this.Equips_byType[this.eqp_type_act_fanxiang[data.confdata.equip_type]].confdata.equip_type);
					if (flag7)
					{
						this.active_eqp.Remove(this.Equips_byType[this.eqp_type_act_fanxiang[data.confdata.equip_type]].confdata.equip_type);
					}
				}
			}
			this.Equips[data.id] = data;
			this.Equips_byType[data.confdata.equip_type] = data;
			A3_BeStronger expr_257 = A3_BeStronger.Instance;
			if (expr_257 != null)
			{
				expr_257.CheckUpItem();
			}
			bool flag8 = this.isActive_eqp(data);
			if (flag8)
			{
				this.active_eqp[data.confdata.equip_type] = data;
			}
			bool flag9 = this.Equips_byType.ContainsKey(this.eqp_type_act_fanxiang[data.confdata.equip_type]);
			if (flag9)
			{
				bool flag10 = this.isActive_eqp(this.Equips_byType[this.eqp_type_act_fanxiang[data.confdata.equip_type]]);
				if (flag10)
				{
					this.active_eqp[this.eqp_type_act_fanxiang[data.confdata.equip_type]] = this.Equips_byType[this.eqp_type_act_fanxiang[data.confdata.equip_type]];
				}
			}
			this.equipModel_on(data);
		}

		public bool isActive_eqp(a3_BagItemData data)
		{
			bool flag = data.equipdata.attribute == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int key = this.eqp_type_act[data.confdata.equip_type];
				bool flag2 = !this.Equips_byType.ContainsKey(key);
				if (flag2)
				{
					result = false;
				}
				else
				{
					int num = this.eqp_att_act[data.equipdata.attribute];
					bool flag3 = this.Equips_byType[key].equipdata.attribute == num;
					result = flag3;
				}
			}
			return result;
		}

		public void equipModel_on(a3_BagItemData data)
		{
			bool flag = data.confdata.equip_type == 3 && SelfRole._inst != null;
			if (flag)
			{
				int tpid = (int)data.tpid;
				int stage = data.equipdata.stage;
				SelfRole._inst.m_roleDta.m_BodyID = tpid;
				SelfRole._inst.m_roleDta.m_BodyFXID = stage;
				SelfRole._inst.set_body(tpid, stage);
				SelfRole._inst.rebind_ani();
				uint color = data.equipdata.color;
				SelfRole._inst.m_roleDta.m_EquipColorID = color;
				SelfRole._inst.set_equip_color(color);
			}
			bool flag2 = data.confdata.equip_type == 6 && SelfRole._inst != null;
			if (flag2)
			{
				int tpid2 = (int)data.tpid;
				int stage2 = data.equipdata.stage;
				switch (ModelBase<PlayerModel>.getInstance().profession)
				{
				case 2:
					SelfRole._inst.m_roleDta.m_Weapon_RID = tpid2;
					SelfRole._inst.m_roleDta.m_Weapon_RFXID = stage2;
					SelfRole._inst.set_weaponr(tpid2, stage2);
					break;
				case 3:
					SelfRole._inst.m_roleDta.m_Weapon_LID = tpid2;
					SelfRole._inst.m_roleDta.m_Weapon_LFXID = stage2;
					SelfRole._inst.set_weaponl(tpid2, stage2);
					break;
				case 5:
					SelfRole._inst.m_roleDta.m_Weapon_LID = tpid2;
					SelfRole._inst.m_roleDta.m_Weapon_LFXID = stage2;
					SelfRole._inst.m_roleDta.m_Weapon_RID = tpid2;
					SelfRole._inst.m_roleDta.m_Weapon_RFXID = stage2;
					SelfRole._inst.set_weaponl(tpid2, stage2);
					SelfRole._inst.set_weaponr(tpid2, stage2);
					break;
				}
			}
			bool flag3 = SelfRole._inst != null;
			if (flag3)
			{
				SelfRole._inst.clear_eff();
			}
			bool flag4 = this.active_eqp.Count >= 10 && SelfRole._inst != null;
			if (flag4)
			{
				SelfRole._inst.set_equip_eff(this.GetEqpIdbyType(3), false);
			}
		}

		public int GetEqpIdbyType(int type)
		{
			bool flag = !this.Equips_byType.ContainsKey(type);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = (int)this.Equips_byType[type].tpid;
			}
			return result;
		}

		public int Getlvl_up(a3_ItemData data, int stage)
		{
			int result = 0;
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==" + stage);
			List<SXML> nodeList = sXML.GetNodeList("stage_info", null);
			bool flag = nodeList != null;
			if (flag)
			{
				foreach (SXML current in nodeList)
				{
					bool flag2 = (ulong)data.tpid == (ulong)((long)current.getInt("itemid"));
					if (flag2)
					{
						result = current.getInt("zhuan");
					}
				}
			}
			return result;
		}

		public void equipModel_down(a3_BagItemData data)
		{
			bool flag = data.confdata.equip_type == 3 && SelfRole._inst != null;
			if (flag)
			{
				SelfRole._inst.m_roleDta.m_BodyID = 0;
				SelfRole._inst.m_roleDta.m_BodyFXID = 0;
				SelfRole._inst.set_body(0, 0);
				SelfRole._inst.rebind_ani();
				SelfRole._inst.set_equip_color(0u);
			}
			bool flag2 = data.confdata.equip_type == 6 && SelfRole._inst != null;
			if (flag2)
			{
				switch (ModelBase<PlayerModel>.getInstance().profession)
				{
				case 2:
					SelfRole._inst.m_roleDta.m_Weapon_RID = 0;
					SelfRole._inst.m_roleDta.m_Weapon_RFXID = 0;
					SelfRole._inst.set_weaponr(0, 0);
					break;
				case 3:
					SelfRole._inst.m_roleDta.m_Weapon_LID = 0;
					SelfRole._inst.m_roleDta.m_Weapon_LID = 0;
					SelfRole._inst.set_weaponl(0, 0);
					break;
				case 5:
					SelfRole._inst.m_roleDta.m_Weapon_LID = 0;
					SelfRole._inst.m_roleDta.m_Weapon_LFXID = 0;
					SelfRole._inst.m_roleDta.m_Weapon_RID = 0;
					SelfRole._inst.m_roleDta.m_Weapon_RFXID = 0;
					SelfRole._inst.set_weaponl(0, 0);
					SelfRole._inst.set_weaponr(0, 0);
					break;
				}
			}
			bool flag3 = SelfRole._inst != null;
			if (flag3)
			{
				SelfRole._inst.clear_eff();
			}
			bool flag4 = this.active_eqp.Count >= 10 && SelfRole._inst != null;
			if (flag4)
			{
				SelfRole._inst.set_equip_eff(this.GetEqpIdbyType(3), false);
			}
		}

		public void equipColor_on(uint id)
		{
			SelfRole._inst.m_roleDta.m_EquipColorID = id;
			SelfRole._inst.set_equip_color(id);
		}

		public a3_BagItemData getEquipByAll(uint id)
		{
			bool flag = this.Equips.ContainsKey(id);
			a3_BagItemData result;
			if (flag)
			{
				result = this.Equips[id];
			}
			else
			{
				bool flag2 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(id);
				if (flag2)
				{
					result = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
				}
				else
				{
					result = default(a3_BagItemData);
				}
			}
			return result;
		}

		public a3_EquipData getEquipByItemId(uint id)
		{
			a3_EquipData result = default(a3_EquipData);
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + id);
			result.tpid = new uint?(id);
			result.color = sXML.getUint("colour");
			result.intensify_lv = sXML.getInt("intensify_lv");
			result.add_level = sXML.getInt("add_level");
			result.add_exp = sXML.getInt("add_exp");
			result.stage = sXML.getInt("stage");
			result.blessing_lv = sXML.getInt("blessing_lv");
			result.combpt = sXML.getInt("combpt");
			result.eqp_level = sXML.getInt("equip_level");
			result.eqp_type = sXML.getInt("equip_type");
			result.subjoin_att = new Dictionary<int, int>();
			result.gem_att = new Dictionary<int, int>();
			result.attribute = sXML.getInt("prefix_name");
			SXML sXML2 = XMLMgr.instance.GetSXML("item", "");
			SXML node = sXML2.GetNode("item", "id==" + id);
			SXML sXML3 = XMLMgr.instance.GetSXML("activate_fun.eqp", "eqp_type==" + node.getInt("equip_type"));
			SXML sXML4 = (sXML3 != null) ? sXML3.GetNode("prefix_fun", "id==" + sXML.getInt("prefix_name")) : null;
			result.att_type = ((sXML4 != null) ? sXML4.getInt("funtype") : 0);
			result.att_value = sXML.getInt("att_value");
			return result;
		}

		public List<a3_EquipData> GetEquipListByEquipType(int equipType)
		{
			List<a3_EquipData> list = new List<a3_EquipData>();
			List<SXML> list2 = null;
			bool flag = equipType != 0;
			if (flag)
			{
				list2 = XMLMgr.instance.GetSXML("item", "").GetNodeList("item", "equip_type==" + equipType);
			}
			else
			{
				if (list2 != null)
				{
					list2.Clear();
				}
				list2 = new List<SXML>();
				List<int> listPartIdx = A3_Smithy.Instance.listPartIdx;
				for (int i = 0; i < listPartIdx.Count; i++)
				{
					list2.AddRange(XMLMgr.instance.GetSXML("item", "").GetNodeList("item", "equip_type==" + listPartIdx[i]));
				}
			}
			for (int num = 0; num < ((list2 != null) ? new int?(list2.Count) : null); num++)
			{
				uint @uint = list2[num].getUint("id");
				list.Add(this.getEquipByItemId(@uint));
			}
			return list;
		}

		public Dictionary<bool, a3_EquipModel.EquipStrengthOption> CheckEquipStrengthAvailable()
		{
			Dictionary<bool, a3_EquipModel.EquipStrengthOption> expr_06 = new Dictionary<bool, a3_EquipModel.EquipStrengthOption>();
			expr_06[true] = a3_EquipModel.EquipStrengthOption.None;
			expr_06[false] = a3_EquipModel.EquipStrengthOption.None;
			Dictionary<bool, a3_EquipModel.EquipStrengthOption> dictionary = expr_06;
			Dictionary<bool, a3_EquipModel.EquipStrengthOption> dictionary2 = dictionary;
			bool key = this.CheckUp(this.Equips, a3_EquipModel.EquipStrengthOption.Intensify);
			dictionary2[key] |= a3_EquipModel.EquipStrengthOption.Intensify;
			dictionary2 = dictionary;
			key = this.CheckUp(this.Equips, a3_EquipModel.EquipStrengthOption.Stage);
			dictionary2[key] |= a3_EquipModel.EquipStrengthOption.Stage;
			dictionary2 = dictionary;
			key = this.CheckUp(this.Equips, a3_EquipModel.EquipStrengthOption.Add);
			dictionary2[key] |= a3_EquipModel.EquipStrengthOption.Add;
			return dictionary;
		}

		private bool CheckUp(Dictionary<uint, a3_BagItemData> equips, a3_EquipModel.EquipStrengthOption checkOption)
		{
			List<uint> list = new List<uint>(equips.Keys);
			bool result;
			switch (checkOption)
			{
			case a3_EquipModel.EquipStrengthOption.None:
			case a3_EquipModel.EquipStrengthOption.Intensify | a3_EquipModel.EquipStrengthOption.Stage:
			case a3_EquipModel.EquipStrengthOption.Intensify | a3_EquipModel.EquipStrengthOption.Add:
			case a3_EquipModel.EquipStrengthOption.Stage | a3_EquipModel.EquipStrengthOption.Add:
			case a3_EquipModel.EquipStrengthOption.Intensify | a3_EquipModel.EquipStrengthOption.Stage | a3_EquipModel.EquipStrengthOption.Add:
				IL_3B:
				break;
			case a3_EquipModel.EquipStrengthOption.Intensify:
				for (int i = 0; i < list.Count; i++)
				{
					uint key = (uint)(equips[list[i]].equipdata.intensify_lv + 1);
					bool flag = !ModelBase<a3_BagModel>.getInstance().EqpIntensifyLevelInfo.ContainsKey(key);
					if (!flag)
					{
						int intensifyCharge = ModelBase<a3_BagModel>.getInstance().EqpIntensifyLevelInfo[key].intensifyCharge;
						Dictionary<uint, int> intensifyMaterials = ModelBase<a3_BagModel>.getInstance().EqpIntensifyLevelInfo[key].intensifyMaterials;
						List<uint> list2 = new List<uint>(intensifyMaterials.Keys);
						bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)intensifyCharge);
						if (flag2)
						{
							int j;
							for (j = 0; j < list2.Count; j++)
							{
								bool flag3 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(list2[j]) < intensifyMaterials[list2[j]];
								if (flag3)
								{
									break;
								}
							}
							bool flag4 = list2.Count == j;
							if (flag4)
							{
								result = true;
								return result;
							}
						}
					}
				}
				goto IL_67B;
			case a3_EquipModel.EquipStrengthOption.Stage:
				for (int k = 0; k < list.Count; k++)
				{
					int num = equips[list[k]].equipdata.stage + 1;
					List<Dictionary<uint, EqpStageLvInfo>> eqpStageInfo = ModelBase<a3_BagModel>.getInstance().EqpStageInfo;
					bool flag5 = eqpStageInfo.Count < num + 1;
					if (!flag5)
					{
						EqpStageLvInfo eqpStageLvInfo = eqpStageInfo[equips[list[k]].equipdata.stage][equips[list[k]].tpid];
						bool flag6 = ModelBase<PlayerModel>.getInstance().up_lvl >= eqpStageLvInfo.reincarnation && ModelBase<PlayerModel>.getInstance().lvl >= eqpStageLvInfo.lv && ModelBase<PlayerModel>.getInstance().money >= eqpStageLvInfo.upstageCharge;
						if (flag6)
						{
							List<A3_CharacterAttribute> list3 = new List<A3_CharacterAttribute>(eqpStageLvInfo.equipLimit.Keys);
							bool flag7 = true;
							for (int l = 0; l < list3.Count; l++)
							{
								A3_CharacterAttribute a3_CharacterAttribute = list3[l];
								A3_CharacterAttribute a3_CharacterAttribute2 = a3_CharacterAttribute;
								switch (a3_CharacterAttribute2)
								{
								case A3_CharacterAttribute.Strength:
									flag7 &= ((ulong)ModelBase<PlayerModel>.getInstance().strength >= (ulong)((long)eqpStageLvInfo.equipLimit[a3_CharacterAttribute]));
									break;
								case A3_CharacterAttribute.Agility:
									flag7 &= ((ulong)ModelBase<PlayerModel>.getInstance().agility >= (ulong)((long)eqpStageLvInfo.equipLimit[a3_CharacterAttribute]));
									break;
								case A3_CharacterAttribute.Constitution:
									flag7 &= ((ulong)ModelBase<PlayerModel>.getInstance().constitution >= (ulong)((long)eqpStageLvInfo.equipLimit[a3_CharacterAttribute]));
									break;
								case A3_CharacterAttribute.Intelligence:
									flag7 &= ((ulong)ModelBase<PlayerModel>.getInstance().intelligence >= (ulong)((long)eqpStageLvInfo.equipLimit[a3_CharacterAttribute]));
									break;
								default:
									if (a3_CharacterAttribute2 != A3_CharacterAttribute.Wisdom)
									{
										Debug.LogError(string.Format("配置表信息错误<装备进阶>:未知的人物属性Id:{0}", (int)a3_CharacterAttribute));
										flag7 = false;
									}
									else
									{
										flag7 &= ((ulong)ModelBase<PlayerModel>.getInstance().wisdom >= (ulong)((long)eqpStageLvInfo.equipLimit[a3_CharacterAttribute]));
									}
									break;
								}
							}
							bool flag8 = flag7;
							if (flag8)
							{
								List<int> list4 = new List<int>(eqpStageLvInfo.upstageMaterials.Keys);
								int m;
								for (m = 0; m < list4.Count; m++)
								{
									bool flag9 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)list4[m]) < eqpStageLvInfo.upstageMaterials[list4[m]];
									if (flag9)
									{
										break;
									}
								}
								bool flag10 = list4.Count == m;
								if (flag10)
								{
									result = true;
									return result;
								}
							}
						}
					}
				}
				goto IL_67B;
			case a3_EquipModel.EquipStrengthOption.Add:
				for (int n = 0; n < list.Count; n++)
				{
					EqpStageLvInfo eqpStageLvInfo2 = ModelBase<a3_BagModel>.getInstance().EqpStageInfo[equips[list[n]].equipdata.stage][equips[list[n]].tpid];
					bool flag11 = equips[list[n]].equipdata.add_level >= eqpStageLvInfo2.maxAddLv;
					if (!flag11)
					{
						uint add_level = (uint)equips[list[n]].equipdata.add_level;
						bool flag12 = !ModelBase<a3_BagModel>.getInstance().EqpAddInfo.ContainsKey(add_level);
						if (!flag12)
						{
							EqpAddConfInfo eqpAddConfInfo = ModelBase<a3_BagModel>.getInstance().EqpAddInfo[add_level];
							bool flag13 = (ulong)ModelBase<PlayerModel>.getInstance().money <= (ulong)((long)eqpAddConfInfo.addCharge);
							if (!flag13)
							{
								bool flag14 = (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(eqpAddConfInfo.matId) <= (long)((ulong)eqpAddConfInfo.matNum);
								if (!flag14)
								{
									result = true;
									return result;
								}
							}
						}
					}
				}
				goto IL_67B;
			case a3_EquipModel.EquipStrengthOption.Gem:
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					a3_EquipData equipdata = equips[list[num2]].equipdata;
					List<EqpGemConfInfo> list5 = ModelBase<a3_BagModel>.getInstance().EqpGemInfo[equips[list[num2]].equipdata.stage][equips[list[num2]].tpid];
					try
					{
						for (int num3 = 0; num3 < list5.Count; num3++)
						{
							bool flag15 = (long)equipdata.gem_att[list5[num3].attType] < (long)((ulong)list5[num3].attMax) && (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(list5[num3].gemId) >= (long)((ulong)list5[num3].gemNeedNum);
							if (flag15)
							{
								result = true;
								return result;
							}
						}
					}
					catch (Exception)
					{
						Debug.LogError(string.Format("item.xml<宝石镶嵌>信息有误:物品id:{0}", equips[list[num2]].tpid));
					}
				}
				goto IL_67B;
			}
			goto IL_3B;
			IL_67B:
			result = false;
			return result;
		}
	}
}
