using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class EquipModel : ModelBase<EquipModel>
	{
		private Dictionary<int, EquipData> equips;

		public EquipModel()
		{
			bool flag = this.equips == null;
			if (flag)
			{
				this.equips = new Dictionary<int, EquipData>();
			}
		}

		public Dictionary<int, EquipData> getEquip()
		{
			return this.equips;
		}

		public void addEquip(EquipData data)
		{
			data.equipConf = this.getEquipDataById(data.tpid);
			this.equips[data.id] = data;
		}

		public EquipConf getEquipDataById(int id)
		{
			return new EquipConf
			{
				tpid = id,
				quality = 1
			};
		}

		public void initEquipList(List<Variant> arr)
		{
			bool flag = this.equips == null;
			if (flag)
			{
				this.equips = new Dictionary<int, EquipData>();
			}
			foreach (Variant current in arr)
			{
				this.refreshEquip(new EquipData
				{
					id = current["equipid"],
					lv = current["level"]
				});
			}
		}

		public void refreshEquip(EquipData data)
		{
			data.equipConf = this.getEquipDataById(data.id);
			bool flag = this.equips.ContainsKey(data.equipConf.equipType);
			if (flag)
			{
				this.equips[data.equipConf.equipType] = data;
			}
			else
			{
				this.equips.Add(data.equipConf.equipType, data);
			}
		}

		public EquipData getNextLvEquip(int curId)
		{
			EquipData equipData = default(EquipData);
			SXML sXML = XMLMgr.instance.GetSXML("equip.equip_upgrade", "item_id==" + curId);
			bool flag = sXML != null;
			if (flag)
			{
				SXML sXML2 = XMLMgr.instance.GetSXML("equip.equip_info", "id==" + curId);
				int @int = sXML.getInt("target_item_id");
				equipData.id = @int;
				equipData.lv = sXML2.getInt("max_strength");
				equipData.equipConf = this.getEquipDataById(equipData.id);
			}
			return equipData;
		}

		public EquipStengthConf getEquipStengthConf(EquipData data)
		{
			SXML sXML = XMLMgr.instance.GetSXML("equip.equip_strengthen", "id==" + data.id.ToString());
			sXML = sXML.GetNode("strengthen", "level==" + (data.lv + 1).ToString());
			EquipStengthConf result = default(EquipStengthConf);
			result.id = data.id;
			result.lv = data.lv;
			bool flag = sXML != null;
			if (flag)
			{
				result.exp = sXML.getInt("strengthen_exp");
				SXML sXML2 = XMLMgr.instance.GetSXML("equip.equip_strengthen", "id==" + data.id.ToString());
				sXML2 = sXML2.GetNode("strengthen", "level==" + data.lv.ToString());
				bool flag2 = data.lv != 0;
				if (flag2)
				{
					result.add = sXML2.getInt("addition");
				}
				else
				{
					result.add = 0;
				}
			}
			SXML sXML3 = XMLMgr.instance.GetSXML("equip.equip_strengthen", "id==" + data.id.ToString());
			sXML3 = sXML3.GetNode("strengthen", null);
			bool flag3 = sXML3 != null;
			if (flag3)
			{
				int num = 0;
				do
				{
					int @int = sXML3.getInt("strengthen_exp");
					bool flag4 = sXML3.getInt("level") > data.lv;
					if (flag4)
					{
						num += @int;
					}
					result.maxlv = sXML3.getInt("level");
				}
				while (sXML3.nextOne());
				result.maxexp = num;
			}
			return result;
		}
	}
}
