using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_SummonModel : ModelBase<A3_SummonModel>
	{
		private SXML itemsXMl;

		private Dictionary<uint, a3_BagItemData> _summons;

		public List<int> feedexplist = new List<int>();

		public List<int> feedsmlist = new List<int>();

		public uint lastatkID;

		public uint nowShowAttackID;

		public uint nowShowAttackModel;

		public bool canFindsum = true;

		private Dictionary<int, float> sum_cds = new Dictionary<int, float>();

		private TickItem process_cd;

		private List<int> sum_remove_cds = new List<int>();

		private List<int> sum_reduce_cds = new List<int>();

		private float sumcd = 0f;

		public int appraiseCost
		{
			get;
			set;
		}

		public A3_SummonModel()
		{
			this.itemsXMl = XMLMgr.instance.GetSXML("callbeast", "");
			this._summons = new Dictionary<uint, a3_BagItemData>();
			SXML node = this.itemsXMl.GetNode("appraise", "");
			this.appraiseCost = node.getInt("gold_cost");
			SXML node2 = this.itemsXMl.GetNode("feed", "");
			string @string = node2.getString("itm_order");
			string[] array = @string.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string s = array2[i];
				this.feedexplist.Add(int.Parse(s));
			}
			SXML node3 = this.itemsXMl.GetNode("life", "");
			string string2 = node3.getString("itm_id");
			string[] array3 = string2.Split(new char[]
			{
				','
			});
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string s2 = array4[j];
				this.feedsmlist.Add(int.Parse(s2));
			}
		}

		public Vector2 GetCombiningCost(int grand)
		{
			SXML node = this.itemsXMl.GetNode("combining", "quality==" + grand);
			int @int = node.getInt("item_id");
			int int2 = node.getInt("num");
			return new Vector2((float)@int, (float)int2);
		}

		public Vector4 GetTalentTypeMax(int talentid)
		{
			SXML node = this.itemsXMl.GetNode("talent", "talent_id==" + talentid);
			int @int = node.GetNode("att", "").getInt("max");
			int int2 = node.GetNode("def", "").getInt("max");
			int int3 = node.GetNode("agi", "").getInt("max");
			int int4 = node.GetNode("con", "").getInt("max");
			return new Vector4((float)@int, (float)int2, (float)int3, (float)int4);
		}

		public Dictionary<uint, a3_BagItemData> GetSummons()
		{
			return this._summons;
		}

		public a3_SummonData GetSummonData(uint id)
		{
			return this._summons.ContainsKey(id) ? this._summons[id].summondata : default(a3_SummonData);
		}

		public SXML GetItemXml(uint tpid)
		{
			return this.itemsXMl.GetNode("callbeast", "id==" + tpid);
		}

		public SXML GetAttributeXml(int level)
		{
			return this.itemsXMl.GetNode("attribute", "level==" + level);
		}

		public SXML GetItemFromBaby(uint babyid)
		{
			return this.itemsXMl.GetNode("callbeast", "need_item==" + babyid);
		}

		private a3_BagItemData SetDataFromXML(a3_BagItemData itemData)
		{
			SXML itemXml = this.GetItemXml((uint)itemData.summondata.tpid);
			bool flag = itemXml == null;
			a3_BagItemData result;
			if (flag)
			{
				result = itemData;
			}
			else
			{
				itemData.summondata.name = itemXml.getString("name");
				int @int = itemXml.getInt("mid");
				SXML sXML = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + @int);
				itemData.summondata.objid = sXML.getInt("obj");
				itemData.summondata.isSpecial = (itemData.summondata.tpid <= 4300);
				result = itemData;
			}
			return result;
		}

		private a3_BagItemData SetDataFromVariant(a3_BagItemData itemData, Variant item)
		{
			itemData.summondata.id = item["id"];
			itemData.summondata.tpid = item["tpid"];
			itemData.summondata.level = item["level"];
			itemData.summondata.currentexp = item["exp"];
			bool flag = item.ContainsKey("hp");
			if (flag)
			{
				itemData.summondata.currenthp = item["hp"];
			}
			itemData.summondata.lifespan = item["life"];
			itemData.summondata.power = item["combpt"];
			itemData.summondata.grade = item["quality"];
			itemData.summondata.isSpecial = (item["type"] == 2);
			itemData.summondata.naturaltype = item["speciality"];
			itemData.summondata.blood = item["bloodline"];
			itemData.summondata.luck = item["luckly"];
			itemData.summondata.talent_type = item["talent_type"];
			itemData.summondata.skillNum = item["skill_num"];
			itemData.summondata.attNatural = item["att"];
			itemData.summondata.defNatural = item["def"];
			itemData.summondata.agiNatural = item["agi"];
			itemData.summondata.conNatural = item["con"];
			itemData.summondata.star = item["talent"];
			bool flag2 = item.ContainsKey("status");
			if (flag2)
			{
				itemData.summondata.status = item["status"];
			}
			bool flag3 = itemData.summondata.status > 0;
			if (flag3)
			{
				this.nowShowAttackID = (uint)itemData.summondata.id;
			}
			Variant variant = item["battleAttrs"];
			itemData.summondata.maxhp = variant["max_hp"];
			itemData.summondata.max_attack = variant["max_attack"];
			itemData.summondata.min_attack = variant["min_attack"];
			itemData.summondata.physics_def = variant["physics_def"];
			itemData.summondata.magic_def = variant["magic_def"];
			itemData.summondata.physics_dmg_red = variant["physics_dmg_red"];
			itemData.summondata.magic_dmg_red = variant["magic_dmg_red"];
			itemData.summondata.double_damage_rate = variant["fatal_att"];
			itemData.summondata.reflect_crit_rate = variant["fatal_dodge"];
			itemData.summondata.fatal_damage = variant["fatal_damage"];
			itemData.summondata.hit = variant["hit"];
			itemData.summondata.dodge = variant["dodge"];
			bool flag4 = item.ContainsKey("skills");
			if (flag4)
			{
				Variant variant2 = item["skills"];
				bool flag5 = itemData.summondata.skills == null;
				if (flag5)
				{
					itemData.summondata.skills = new Dictionary<int, int>();
				}
				for (int i = 0; i < variant2.Count; i++)
				{
					itemData.summondata.skills[variant2[i]["index"]] = variant2[i]["skill_id"];
				}
			}
			return itemData;
		}

		private a3_BagItemData SetBagItemData(a3_BagItemData itemData, Variant item)
		{
			SXML itemXml = this.GetItemXml((uint)itemData.summondata.tpid);
			itemData.confdata.item_name = itemXml.getString("name");
			itemData.id = item["id"];
			itemData.tpid = item["tpid"];
			SXML itemXml2 = ModelBase<a3_BagModel>.getInstance().getItemXml(item["tpid"]);
			bool flag = itemXml2 != null;
			if (flag)
			{
				itemData.confdata.file = "icon/item/" + itemXml2.getString("icon_file");
				itemData.confdata.borderfile = "icon/itemborder/b039_0" + itemXml2.getString("quality");
				itemData.confdata.item_name = itemXml2.getString("item_name");
				itemData.confdata.quality = itemXml2.getInt("quality");
				itemData.confdata.desc = itemXml2.getString("desc");
				itemData.confdata.desc2 = itemXml2.getString("desc2");
				itemData.confdata.value = itemXml2.getInt("value");
				itemData.confdata.use_lv = itemXml2.getInt("use_lv");
				itemData.confdata.use_limit = itemXml2.getInt("use_limit");
				itemData.confdata.use_type = itemXml2.getInt("use_type");
				int @int = itemXml2.getInt("intensify_score");
				itemData.confdata.intensify_score = @int;
				itemData.confdata.item_type = itemXml2.getInt("item_type");
				itemData.confdata.equip_type = itemXml2.getInt("equip_type");
				itemData.confdata.equip_level = itemXml2.getInt("equip_level");
				itemData.confdata.job_limit = itemXml2.getInt("job_limit");
				itemData.confdata.modelId = itemXml2.getInt("model_id");
			}
			return itemData;
		}

		public a3_BagItemData GetSummonData(a3_BagItemData itemData, Variant item)
		{
			bool flag = !this.IsSummon(itemData, item);
			a3_BagItemData result;
			if (flag)
			{
				result = itemData;
			}
			else
			{
				itemData.isSummon = true;
				bool flag2 = this.IsBaby(itemData);
				if (flag2)
				{
					itemData.isSummon = false;
					SXML itemFromBaby = this.GetItemFromBaby(itemData.tpid);
					int @int = itemFromBaby.getInt("talent_id");
					bool flag3 = @int >= 0;
					if (flag3)
					{
						int[] summonTypeById = this.GetSummonTypeById(@int);
						bool flag4 = summonTypeById.Length >= 2;
						if (flag4)
						{
							itemData.summondata.isSpecial = true;
							itemData.summondata.grade = summonTypeById[1];
							itemData.summondata.naturaltype = summonTypeById[2];
						}
						else
						{
							bool flag5 = summonTypeById.Length >= 1;
							if (flag5)
							{
								itemData.summondata.isSpecial = false;
								itemData.summondata.grade = summonTypeById[1];
								itemData.summondata.naturaltype = summonTypeById[2];
							}
						}
					}
					else
					{
						itemData.summondata.isSpecial = false;
						itemData.summondata.grade = 0;
						itemData.summondata.naturaltype = 0;
					}
					result = itemData;
				}
				else
				{
					itemData = this.SetDataFromVariant(itemData, item);
					itemData = this.SetDataFromXML(itemData);
					result = itemData;
				}
			}
			return result;
		}

		public a3_BagItemData SetBabyData(a3_BagItemData itemData, Variant item)
		{
			bool flag = !this.IsBaby(itemData);
			a3_BagItemData result;
			if (flag)
			{
				result = itemData;
			}
			else
			{
				itemData.isSummon = true;
				SXML itemFromBaby = this.GetItemFromBaby(itemData.tpid);
				int @int = itemFromBaby.getInt("talent_id");
				int[] summonTypeById = this.GetSummonTypeById(@int);
				bool flag2 = summonTypeById.Length >= 2;
				if (flag2)
				{
					itemData.summondata.isSpecial = true;
					itemData.summondata.grade = summonTypeById[1];
					itemData.summondata.naturaltype = summonTypeById[2];
				}
				else
				{
					bool flag3 = summonTypeById.Length >= 1;
					if (flag3)
					{
						itemData.summondata.isSpecial = false;
						itemData.summondata.grade = summonTypeById[1];
						itemData.summondata.naturaltype = summonTypeById[2];
					}
				}
				result = itemData;
			}
			return result;
		}

		public bool IsSummon(a3_BagItemData itemData, Variant item)
		{
			return item.ContainsKey("talent_type") || (itemData.tpid >= 4000u && itemData.tpid <= 4400u);
		}

		public bool IsBaby(a3_BagItemData itemData)
		{
			return itemData.tpid >= 4000u && itemData.tpid <= 4200u;
		}

		public int[] GetSummonTypeById(int id)
		{
			int[] result = null;
			switch (id)
			{
			case 1:
				result = new int[]
				{
					0,
					1,
					1
				};
				break;
			case 2:
				result = new int[]
				{
					0,
					1,
					2
				};
				break;
			case 3:
				result = new int[]
				{
					0,
					1,
					3
				};
				break;
			case 4:
				result = new int[]
				{
					0,
					1,
					4
				};
				break;
			case 5:
				result = new int[]
				{
					0,
					2,
					1
				};
				break;
			case 6:
				result = new int[]
				{
					0,
					2,
					2
				};
				break;
			case 7:
				result = new int[]
				{
					0,
					2,
					3
				};
				break;
			case 8:
				result = new int[]
				{
					0,
					2,
					4
				};
				break;
			case 9:
				result = new int[]
				{
					0,
					3,
					1
				};
				break;
			case 10:
				result = new int[]
				{
					0,
					3,
					2
				};
				break;
			case 11:
				result = new int[]
				{
					0,
					3,
					3
				};
				break;
			case 12:
				result = new int[]
				{
					0,
					3,
					4
				};
				break;
			case 13:
				result = new int[]
				{
					1,
					1,
					1
				};
				break;
			case 14:
				result = new int[]
				{
					1,
					1,
					2
				};
				break;
			case 15:
				result = new int[]
				{
					1,
					1,
					3
				};
				break;
			case 16:
				result = new int[]
				{
					1,
					1,
					4
				};
				break;
			case 17:
				result = new int[]
				{
					1,
					2,
					1
				};
				break;
			case 18:
				result = new int[]
				{
					1,
					2,
					2
				};
				break;
			case 19:
				result = new int[]
				{
					1,
					2,
					3
				};
				break;
			case 20:
				result = new int[]
				{
					1,
					2,
					4
				};
				break;
			case 21:
				result = new int[]
				{
					1,
					3,
					1
				};
				break;
			case 22:
				result = new int[]
				{
					1,
					3,
					2
				};
				break;
			case 23:
				result = new int[]
				{
					1,
					3,
					3
				};
				break;
			case 24:
				result = new int[]
				{
					1,
					3,
					4
				};
				break;
			}
			return result;
		}

		public string IntGradeToStr(int i)
		{
			bool flag = i == 0;
			string result;
			if (flag)
			{
				result = "未知";
			}
			else
			{
				bool flag2 = i == 1;
				if (flag2)
				{
					result = "普通";
				}
				else
				{
					bool flag3 = i == 2;
					if (flag3)
					{
						result = "精英";
					}
					else
					{
						result = "领主";
					}
				}
			}
			return result;
		}

		public string IntLvlToStr(int i)
		{
			bool flag = i == 0;
			string result;
			if (flag)
			{
				result = "未知";
			}
			else
			{
				bool flag2 = i == 1;
				if (flag2)
				{
					result = "初级";
				}
				else
				{
					bool flag3 = i == 2;
					if (flag3)
					{
						result = "中级";
					}
					else
					{
						result = "高级";
					}
				}
			}
			return result;
		}

		public string IntNaturalToStr(int i)
		{
			bool flag = i == 0;
			string result;
			if (flag)
			{
				result = "未知";
			}
			else
			{
				bool flag2 = i == 1;
				if (flag2)
				{
					result = "攻击型";
				}
				else
				{
					bool flag3 = i == 2;
					if (flag3)
					{
						result = "防御型";
					}
					else
					{
						bool flag4 = i == 3;
						if (flag4)
						{
							result = "敏捷型";
						}
						else
						{
							result = "体力型";
						}
					}
				}
			}
			return result;
		}

		public void AddSummon(Variant item)
		{
			a3_BagItemData a3_BagItemData = default(a3_BagItemData);
			a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().SetDataFromVariant(a3_BagItemData, item);
			a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().SetDataFromXML(a3_BagItemData);
			a3_BagItemData = ModelBase<A3_SummonModel>.getInstance().SetBagItemData(a3_BagItemData, item);
			this._summons[a3_BagItemData.id] = a3_BagItemData;
		}

		public void RemoveSummon(int id)
		{
			bool flag = this._summons.ContainsKey((uint)id);
			if (flag)
			{
				this._summons.Remove((uint)id);
			}
		}

		public void SortSummon()
		{
			List<a3_SummonData> list = new List<a3_SummonData>();
			Dictionary<uint, a3_BagItemData> dictionary = new Dictionary<uint, a3_BagItemData>();
			foreach (a3_BagItemData current in this._summons.Values)
			{
				list.Add(current.summondata);
			}
			list.Sort();
			foreach (a3_SummonData current2 in list)
			{
				dictionary[(uint)current2.id] = this._summons[(uint)current2.id];
			}
			this._summons = dictionary;
		}

		public void addSumCD(int id, float time)
		{
			bool flag = this.process_cd == null;
			if (flag)
			{
				this.process_cd = new TickItem(new Action<float>(this.onUpdateCd));
				TickMgr.instance.addTick(this.process_cd);
			}
			this.sum_cds[id] = time;
		}

		public void sum_doCd(int time)
		{
			this.sumcd = (float)time;
			ModelBase<A3_SummonModel>.getInstance().canFindsum = false;
			bool flag = this.process_cd != null;
			if (flag)
			{
				TickMgr.instance.removeTick(this.process_cd);
				this.process_cd = null;
			}
			this.process_cd = new TickItem(new Action<float>(this.onUpdateCd));
			TickMgr.instance.addTick(this.process_cd);
		}

		public Dictionary<int, float> getSumCds()
		{
			return this.sum_cds;
		}

		private void onUpdateCd(float s)
		{
			foreach (int current in this.sum_cds.Keys)
			{
				this.sum_reduce_cds.Add(current);
				bool flag = this.sum_cds[current] <= 0f;
				if (flag)
				{
					this.sum_remove_cds.Add(current);
				}
			}
			foreach (int current2 in this.sum_reduce_cds)
			{
				this.sum_cds[current2] = this.sum_cds[current2] - s;
			}
			foreach (int current3 in this.sum_remove_cds)
			{
				this.sum_cds.Remove(current3);
			}
			this.sum_remove_cds.Clear();
			this.sum_reduce_cds.Clear();
			bool flag2 = this.sum_cds.Count == 0;
			if (flag2)
			{
				TickMgr.instance.removeTick(this.process_cd);
				this.process_cd = null;
			}
		}
	}
}
