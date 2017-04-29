using Cross;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class a3_BagModel : ModelBase<a3_BagModel>
	{
		public static uint EVENT_EQUIP_ADD = 0u;

		private SXML itemsXMl;

		public int curi = 0;

		public int house_curi = 0;

		public int m_all_curi = 150;

		public Dictionary<uint, a3_BagItemData> Items;

		private Dictionary<uint, a3_BagItemData> RunestoneItems;

		private Dictionary<uint, a3_BagItemData> UnEquips;

		private Dictionary<uint, a3_BagItemData> HouseItems;

		private Dictionary<uint, EqpIntensifyLvInfo> eqpIntensifyLevelInfo;

		private List<Dictionary<uint, EqpStageLvInfo>> eqpStageInfo;

		private Dictionary<uint, EqpAddConfInfo> eqpAddInfo;

		private List<Dictionary<uint, List<EqpGemConfInfo>>> eqpGemInfo;

		private TickItem process_cd;

		private Dictionary<int, float> item_cds;

		private List<int> item_remove_cds;

		private List<int> item_reduce_cds;

		public bool isFirstMark = true;

		public Dictionary<uint, a3_BagItemData> neweqp = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<uint, a3_BagItemData> newshow_item = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<uint, a3_BagItemData> item_num = new Dictionary<uint, a3_BagItemData>();

		public Dictionary<uint, EqpIntensifyLvInfo> EqpIntensifyLevelInfo
		{
			get
			{
				return this.eqpIntensifyLevelInfo;
			}
		}

		public List<Dictionary<uint, EqpStageLvInfo>> EqpStageInfo
		{
			get
			{
				return this.eqpStageInfo;
			}
		}

		public Dictionary<uint, EqpAddConfInfo> EqpAddInfo
		{
			get
			{
				return this.eqpAddInfo;
			}
		}

		public List<Dictionary<uint, List<EqpGemConfInfo>>> EqpGemInfo
		{
			get
			{
				return this.eqpGemInfo;
			}
		}

		public a3_BagModel()
		{
			this.itemsXMl = XMLMgr.instance.GetSXML("item", "");
			this.Items = new Dictionary<uint, a3_BagItemData>();
			this.RunestoneItems = new Dictionary<uint, a3_BagItemData>();
			this.UnEquips = new Dictionary<uint, a3_BagItemData>();
			this.HouseItems = new Dictionary<uint, a3_BagItemData>();
			this.item_cds = new Dictionary<int, float>();
			this.item_remove_cds = new List<int>();
			this.item_reduce_cds = new List<int>();
			this.eqpIntensifyLevelInfo = new Dictionary<uint, EqpIntensifyLvInfo>();
			this.eqpStageInfo = new List<Dictionary<uint, EqpStageLvInfo>>();
			this.eqpAddInfo = new Dictionary<uint, EqpAddConfInfo>();
			this.eqpGemInfo = new List<Dictionary<uint, List<EqpGemConfInfo>>>();
			this.InitEqpIntensifyLv();
			this.InitEqpStageInfo();
			this.InitEqpAddInfo();
			this.InitEqpGemInfo();
			this.allRunestoneData();
		}

		public SXML getItemXml(int tpid)
		{
			return this.itemsXMl.GetNode("item", "id==" + tpid);
		}

		private void InitEqpIntensifyLv()
		{
			List<SXML> nodeList = this.itemsXMl.GetNodeList("intensify", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
				string[] array = new string[]
				{
					nodeList[i].getString("intensify_material1"),
					nodeList[i].getString("intensify_material2"),
					nodeList[i].getString("intensify_material3")
				};
				for (int j = 0; j < array.Length; j++)
				{
					string[] array2 = array[j].Split(new char[]
					{
						','
					});
					bool flag = !dictionary.ContainsKey(uint.Parse(array2[0]));
					if (flag)
					{
						dictionary.Add(uint.Parse(array2[0]), int.Parse(array2[1]));
					}
					else
					{
						Debug.LogError("item.xml:配置表信息错误,intensify_material字段格式不正确!");
					}
				}
				this.eqpIntensifyLevelInfo.Add(nodeList[i].getUint("intensify_level"), new EqpIntensifyLvInfo
				{
					intensifyCharge = nodeList[i].getInt("intensify_money"),
					intensifyMaterials = dictionary
				});
			}
		}

		private void InitEqpStageInfo()
		{
			int num = Enum.GetNames(typeof(A3_CharacterAttribute)).Length;
			List<SXML> nodeList = this.itemsXMl.GetNodeList("stage", "");
			List<SXML> nodeList2 = this.itemsXMl.GetNodeList("item", "");
			Dictionary<uint, int> dictionary = new Dictionary<uint, int>();
			for (int i = 0; i < nodeList2.Count; i++)
			{
				uint @uint = nodeList2[i].getUint("id");
				bool flag = !dictionary.ContainsKey(@uint);
				if (flag)
				{
					dictionary.Add(@uint, nodeList2[i].getInt("add_basiclevel"));
				}
				else
				{
					Debug.LogError(string.Format("item.xml:配置表信息错误,物品id重复,重复的id为{0}!", @uint));
				}
			}
			for (int j = 0; j < nodeList.Count; j++)
			{
				this.eqpStageInfo.Add(new Dictionary<uint, EqpStageLvInfo>());
				List<SXML> nodeList3 = nodeList[j].GetNodeList("stage_info", "");
				for (int k = 0; k < nodeList3.Count; k++)
				{
					Dictionary<A3_CharacterAttribute, int> dictionary2 = new Dictionary<A3_CharacterAttribute, int>();
					for (int l = 1; l < num; l++)
					{
						string expr_126 = nodeList3[k].getString("equip_limit" + l);
						string[] array;
						bool flag2 = (array = ((expr_126 != null) ? expr_126.Split(new char[]
						{
							','
						}) : null)).Length == 2;
						if (!flag2)
						{
							break;
						}
						dictionary2.Add((A3_CharacterAttribute)Enum.Parse(typeof(A3_CharacterAttribute), (array != null) ? array[0] : null), int.Parse((array != null) ? array[1] : null));
					}
					Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
					List<SXML> nodeList4 = nodeList3[k].GetNodeList("stage_material", "");
					for (int m = 0; m < nodeList4.Count; m++)
					{
						int @int = nodeList4[m].getInt("item");
						bool flag3 = @int > 0;
						if (flag3)
						{
							bool flag4 = !dictionary3.ContainsKey(@int);
							if (flag4)
							{
								dictionary3.Add(@int, nodeList4[m].getInt("num"));
							}
							else
							{
								Dictionary<int, int> dictionary4 = dictionary3;
								int key = @int;
								dictionary4[key] += nodeList4[m].getInt("num");
							}
						}
					}
					uint uint2 = nodeList3[k].getUint("itemid");
					bool flag5 = !this.eqpStageInfo[j].ContainsKey(uint2);
					if (flag5)
					{
						this.eqpStageInfo[j].Add(uint2, new EqpStageLvInfo
						{
							reincarnation = nodeList3[k].getUint("zhuan"),
							lv = nodeList3[k].getUint("level"),
							upstageCharge = nodeList3[k].getUint("stage_money"),
							equipLimit = dictionary2,
							upstageMaterials = dictionary3,
							maxAddLv = dictionary[uint2] * j
						});
					}
					else
					{
						Debug.LogError(string.Format("item.xml:配置表信息不正确,装备等阶信息重复,重复id:{0}", uint2));
					}
				}
			}
		}

		private void InitEqpAddInfo()
		{
			List<SXML> nodeList = this.itemsXMl.GetNodeList("add_att", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				uint @uint = nodeList[i].getUint("add_level");
				bool flag = !this.eqpAddInfo.ContainsKey(@uint);
				if (flag)
				{
					this.eqpAddInfo.Add(@uint, new EqpAddConfInfo
					{
						addCharge = nodeList[i].getInt("money"),
						matId = nodeList[i].getUint("material_id"),
						matNum = nodeList[i].getUint("material_num")
					});
				}
				else
				{
					Debug.LogError(string.Format("item.xml:配置表信息错误,装备追加等级信息重复,重复的等级：{0}", @uint));
				}
			}
		}

		private void InitEqpGemInfo()
		{
			List<SXML> nodeList = this.itemsXMl.GetNodeList("gem", "");
			for (int i = 0; i < this.eqpStageInfo.Count; i++)
			{
				List<EqpGemConfInfo> list = new List<EqpGemConfInfo>();
				Dictionary<uint, List<EqpGemConfInfo>> dictionary = new Dictionary<uint, List<EqpGemConfInfo>>();
				for (int j = 0; j < nodeList.Count; j++)
				{
					uint @uint = nodeList[j].getUint("item_id");
					List<SXML> nodeList2 = nodeList[j].GetNodeList("gem_info.gem_att", "");
					for (int k = 0; k < nodeList2.Count; k++)
					{
						list.Add(new EqpGemConfInfo
						{
							attType = nodeList2[k].getInt("att_type"),
							attMax = nodeList2[k].getUint("att_max"),
							gemId = nodeList2[k].getUint("need_itemid"),
							gemNeedNum = nodeList2[k].getUint("need_value")
						});
					}
					dictionary.Add(@uint, list);
				}
				this.eqpGemInfo.Add(dictionary);
			}
		}

		public void initItemList(List<Variant> arr)
		{
			bool flag = this.Items == null;
			if (flag)
			{
				this.Items = new Dictionary<uint, a3_BagItemData>();
			}
			foreach (Variant current in arr)
			{
				this.initItemOne(current);
			}
		}

		public void addItemCd(int type, float time)
		{
			bool flag = this.process_cd == null;
			if (flag)
			{
				this.process_cd = new TickItem(new Action<float>(this.onUpdateCd));
				TickMgr.instance.addTick(this.process_cd);
			}
			this.item_cds[type] = time;
		}

		public void gheqpData(a3_BagItemData add, a3_BagItemData rem)
		{
			this.Items[add.id] = add;
			this.UnEquips[add.id] = add;
			this.removeItem(rem.id);
		}

		public Dictionary<int, float> getItemCds()
		{
			return this.item_cds;
		}

		private void onUpdateCd(float s)
		{
			foreach (int current in this.item_cds.Keys)
			{
				this.item_reduce_cds.Add(current);
				bool flag = this.item_cds[current] <= 0f;
				if (flag)
				{
					this.item_remove_cds.Add(current);
				}
			}
			foreach (int current2 in this.item_reduce_cds)
			{
				this.item_cds[current2] = this.item_cds[current2] - s;
			}
			foreach (int current3 in this.item_remove_cds)
			{
				this.item_cds.Remove(current3);
			}
			this.item_reduce_cds.Clear();
			this.item_remove_cds.Clear();
			bool flag2 = this.item_cds.Count == 0;
			if (flag2)
			{
				TickMgr.instance.removeTick(this.process_cd);
				this.process_cd = null;
			}
		}

		public void initItemOne(Variant data)
		{
			a3_BagItemData a3_BagItemData = default(a3_BagItemData);
			a3_BagItemData.id = data["id"];
			a3_BagItemData.tpid = data["tpid"];
			a3_BagItemData.num = data["cnt"];
			a3_BagItemData.bnd = data["bnd"];
			a3_BagItemData.isEquip = false;
			a3_BagItemData.isNew = false;
			bool flag = data.ContainsKey("intensify_lv");
			if (flag)
			{
				a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().equipData_read(a3_BagItemData, data);
			}
			ModelBase<a3_BagModel>.getInstance().addItem(a3_BagItemData);
		}

		public Dictionary<uint, a3_BagItemData> getItems(bool sort = false)
		{
			bool flag = !sort;
			Dictionary<uint, a3_BagItemData> result;
			if (flag)
			{
				result = this.Items;
			}
			else
			{
				List<a3_BagItemData> list = new List<a3_BagItemData>();
				list.AddRange(this.Items.Values);
				list.Sort();
				Dictionary<uint, a3_BagItemData> dictionary = new Dictionary<uint, a3_BagItemData>();
				foreach (a3_BagItemData current in list)
				{
					dictionary[current.id] = current;
				}
				result = dictionary;
			}
			return result;
		}

		public Dictionary<uint, a3_BagItemData> getHouseItems()
		{
			return this.HouseItems;
		}

		public Dictionary<uint, a3_BagItemData> getUnEquips()
		{
			return this.UnEquips;
		}

		public Dictionary<uint, a3_BagItemData> getRunestonrs()
		{
			return this.RunestoneItems;
		}

		public int get_item_num(uint id)
		{
			int result = 0;
			bool flag = this.item_num.ContainsKey(id);
			if (flag)
			{
				result = this.item_num[id].num;
			}
			return result;
		}

		public void addItem(a3_BagItemData data)
		{
			data.confdata = this.getItemDataById(data.tpid);
			bool flag = this.Items.ContainsKey(data.id);
			if (flag)
			{
				bool flag2 = data.num == 0;
				if (flag2)
				{
					this.Items.Remove(data.id);
				}
				else
				{
					this.Items[data.id] = data;
				}
			}
			else
			{
				this.Items.Add(data.id, data);
			}
			bool isEquip = data.isEquip;
			if (isEquip)
			{
				this.UnEquips[data.id] = data;
				bool autofighting = SelfRole.fsm.Autofighting;
				if (autofighting)
				{
					StatePick.Instance.AutoEquipProcess(data);
				}
				this.isgoodeqp(data);
			}
			bool flag3 = data.confdata.use_type == 13 || data.confdata.use_type == 20;
			if (flag3)
			{
				this.addlibao(data);
			}
			bool flag4 = a3_expbar.instance != null;
			if (flag4)
			{
				a3_expbar.instance.bag_Count();
			}
			bool flag5 = data.confdata.use_type == 22;
			if (flag5)
			{
				this.RunestoneItems[data.id] = data;
				bool flag6 = a3_runestone._instance != null;
				if (flag6)
				{
					a3_runestone._instance.addHaveRunestones(data);
				}
			}
			bool flag7 = data.tpid >= 1633u && data.tpid <= 1637u;
			if (flag7)
			{
				bool flag8 = a3_runestone._instance != null;
				if (flag8)
				{
					a3_runestone._instance.refreshHvaeMaterialNum();
				}
			}
			A3_BeStronger expr_19C = A3_BeStronger.Instance;
			if (expr_19C != null)
			{
				expr_19C.CheckUpItem();
			}
		}

		private void isgoodeqp(a3_BagItemData data)
		{
			bool flag = !data.isEquip;
			if (!flag)
			{
				bool flag2 = !data.isNew;
				if (!flag2)
				{
					bool flag3 = ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(data.confdata) && ModelBase<a3_EquipModel>.getInstance().checkCanEquip(data);
					if (flag3)
					{
						bool flag4 = !SelfRole.fsm.Autofighting;
						if (flag4)
						{
							bool flag5 = !ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(data.confdata.equip_type);
							if (flag5)
							{
								foreach (a3_BagItemData current in this.neweqp.Values)
								{
									bool flag6 = data.confdata.equip_type == current.confdata.equip_type;
									if (flag6)
									{
										bool flag7 = data.equipdata.combpt > current.equipdata.combpt;
										if (flag7)
										{
											this.neweqp[data.id] = data;
											this.neweqp.Remove(current.id);
											a3_equipup.instance.showUse();
											return;
										}
										return;
									}
								}
								this.neweqp[data.id] = data;
								a3_equipup.instance.showUse();
							}
							else
							{
								a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[data.confdata.equip_type];
								bool flag8 = data.equipdata.combpt > a3_BagItemData.equipdata.combpt;
								if (flag8)
								{
									foreach (a3_BagItemData current2 in this.neweqp.Values)
									{
										bool flag9 = data.confdata.equip_type == current2.confdata.equip_type;
										if (flag9)
										{
											bool flag10 = data.equipdata.combpt > current2.equipdata.combpt;
											if (flag10)
											{
												this.neweqp[data.id] = data;
												this.neweqp.Remove(current2.id);
												a3_equipup.instance.showUse();
												return;
											}
											return;
										}
									}
									this.neweqp[data.id] = data;
									a3_equipup.instance.showUse();
								}
							}
						}
						else
						{
							int eqpProc = ModelBase<AutoPlayModel>.getInstance().EqpProc;
							for (int i = 0; i < 5; i++)
							{
								bool flag11 = (eqpProc & 1 << i) == 0 && data.confdata.quality == i + 1;
								if (flag11)
								{
									bool flag12 = !ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(data.confdata.equip_type);
									if (flag12)
									{
										foreach (a3_BagItemData current3 in this.neweqp.Values)
										{
											bool flag13 = data.confdata.equip_type == current3.confdata.equip_type;
											if (flag13)
											{
												bool flag14 = data.equipdata.combpt > current3.equipdata.combpt;
												if (flag14)
												{
													this.neweqp[data.id] = data;
													this.neweqp.Remove(current3.id);
													a3_equipup.instance.showUse();
													return;
												}
												return;
											}
										}
										this.neweqp[data.id] = data;
										a3_equipup.instance.showUse();
									}
									else
									{
										a3_BagItemData a3_BagItemData2 = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[data.confdata.equip_type];
										bool flag15 = data.equipdata.combpt > a3_BagItemData2.equipdata.combpt;
										if (flag15)
										{
											foreach (a3_BagItemData current4 in this.neweqp.Values)
											{
												bool flag16 = data.confdata.equip_type == current4.confdata.equip_type;
												if (flag16)
												{
													bool flag17 = data.equipdata.combpt > current4.equipdata.combpt;
													if (flag17)
													{
														this.neweqp[data.id] = data;
														this.neweqp.Remove(current4.id);
														a3_equipup.instance.showUse();
														return;
													}
													return;
												}
											}
											this.neweqp[data.id] = data;
											a3_equipup.instance.showUse();
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void addlibao(a3_BagItemData data)
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)data.confdata.use_limit);
			if (flag)
			{
				this.newshow_item[data.id] = data;
			}
			bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)data.confdata.use_limit);
			if (flag2)
			{
				bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)data.confdata.use_lv);
				if (flag3)
				{
					this.newshow_item[data.id] = data;
				}
			}
			bool flag4 = a3_equipup.instance != null;
			if (flag4)
			{
				a3_equipup.instance.showUse();
			}
		}

		public void addHouseItem(a3_BagItemData data)
		{
			data.confdata = this.getItemDataById(data.tpid);
			this.HouseItems[data.id] = data;
		}

		public void removeItem(uint id)
		{
			bool flag = this.Items.ContainsKey(id);
			if (flag)
			{
				this.Items.Remove(id);
			}
			bool flag2 = this.UnEquips.ContainsKey(id);
			if (flag2)
			{
				this.UnEquips.Remove(id);
			}
			bool flag3 = this.neweqp.ContainsKey(id);
			if (flag3)
			{
				this.neweqp.Remove(id);
				bool flag4 = id == a3_equipup.instance.nowShow;
				if (flag4)
				{
					a3_equipup.instance.showUse();
				}
			}
			bool flag5 = this.newshow_item.ContainsKey(id);
			if (flag5)
			{
				this.newshow_item.Remove(id);
				bool flag6 = id == a3_equipup.instance.nowShow;
				if (flag6)
				{
					a3_equipup.instance.showUse();
				}
			}
			bool flag7 = a3_expbar.instance != null;
			if (flag7)
			{
				a3_expbar.instance.bag_Count();
			}
			bool flag8 = this.RunestoneItems.ContainsKey(id);
			if (flag8)
			{
				this.RunestoneItems.Remove(id);
				bool flag9 = a3_runestone._instance != null;
				if (flag9)
				{
					a3_runestone._instance.removeHaveRunestones(id);
				}
			}
			bool flag10 = this.getItemDataById(id).tpid >= 1633u && this.getItemDataById(id).tpid <= 1637u;
			if (flag10)
			{
				bool flag11 = a3_runestone._instance != null;
				if (flag11)
				{
					a3_runestone._instance.refreshHvaeMaterialNum();
				}
			}
		}

		public void removeHouseItem(uint id)
		{
			bool flag = this.HouseItems.ContainsKey(id);
			if (flag)
			{
				this.HouseItems.Remove(id);
			}
		}

		public a3_ItemData getItemDataById(uint tpid)
		{
			a3_ItemData result = default(a3_ItemData);
			result.tpid = tpid;
			SXML node = this.itemsXMl.GetNode("item", "id==" + tpid);
			bool flag = node != null;
			if (flag)
			{
				result.file = "icon/item/" + node.getString("icon_file");
				result.borderfile = "icon/itemborder/b039_0" + node.getString("quality");
				result.item_name = node.getString("item_name");
				result.quality = node.getInt("quality");
				result.desc = node.getString("desc");
				result.desc2 = node.getString("desc2");
				result.maxnum = node.getInt("maxnum");
				result.value = node.getInt("value");
				result.use_lv = node.getInt("use_lv");
				result.use_limit = node.getInt("use_limit");
				result.use_type = node.getInt("use_type");
				int @int = node.getInt("intensify_score");
				result.intensify_score = @int;
				result.item_type = node.getInt("item_type");
				bool flag2 = node.getInt("sort_type") < 0;
				if (flag2)
				{
					result.sortType = 9999;
				}
				else
				{
					result.sortType = node.getInt("sort_type");
				}
				result.equip_type = node.getInt("equip_type");
				result.equip_level = node.getInt("equip_level");
				result.job_limit = node.getInt("job_limit");
				result.modelId = node.getInt("model_id");
				result.on_sale = node.getInt("on_sale");
				result.cd_type = node.getInt("cd_type");
				result.cd_time = node.getFloat("cd");
				result.main_effect = node.getInt("main_effect");
				result.add_basiclevel = node.getInt("add_basiclevel");
				result.use_sum_require = node.getInt("use_sum_require");
			}
			return result;
		}

		public a3_ItemData getItemDataByName(string name)
		{
			a3_ItemData result = default(a3_ItemData);
			SXML node = this.itemsXMl.GetNode("item", "item_name==" + name);
			bool flag = node != null;
			if (flag)
			{
				result.file = "icon/item/" + node.getString("icon_file");
				result.borderfile = "icon/itemborder/b039_0" + node.getString("quality");
				result.item_name = node.getString("item_name");
				result.quality = node.getInt("quality");
				result.desc = node.getString("desc");
				result.desc2 = node.getString("desc2");
				result.value = node.getInt("value");
				result.use_lv = node.getInt("use_lv");
				result.use_limit = node.getInt("use_limit");
				result.use_type = node.getInt("use_type");
				int @int = node.getInt("intensify_score");
				result.intensify_score = @int;
				bool flag2 = node.getInt("sort_type") < 0;
				if (flag2)
				{
					result.sortType = 9999;
				}
				else
				{
					result.sortType = node.getInt("sort_type");
				}
				result.item_type = node.getInt("item_type");
				result.equip_type = node.getInt("equip_type");
				result.equip_level = node.getInt("equip_level");
				result.job_limit = node.getInt("job_limit");
				result.modelId = node.getInt("model_id");
				result.on_sale = node.getInt("on_sale");
				result.cd_type = node.getInt("cd_type");
				result.cd_time = node.getFloat("cd");
				result.tpid = node.getUint("id");
				result.main_effect = node.getInt("main_effect");
				result.add_basiclevel = node.getInt("add_basiclevel");
				result.use_sum_require = node.getInt("use_sum_require");
			}
			return result;
		}

		public bool isContainItem(uint tpid)
		{
			SXML node = this.itemsXMl.GetNode("item", "id==" + tpid);
			return node != null;
		}

		public int getItemNumByTpid(uint tpid)
		{
			int num = 0;
			foreach (a3_BagItemData current in this.Items.Values)
			{
				bool flag = current.tpid == tpid;
				if (flag)
				{
					num += current.num;
				}
			}
			return num;
		}

		public bool getHaveRoom()
		{
			return this.curi > this.Items.Count;
		}

		public bool getHaveOverLayRoom(uint tpid, int num)
		{
			int num2 = 0;
			int num3 = 0;
			foreach (a3_BagItemData current in this.Items.Values)
			{
				bool flag = current.tpid == tpid;
				if (flag)
				{
					num2 += current.num;
					num3++;
				}
			}
			SXML node = this.itemsXMl.GetNode("item", "id==" + tpid);
			int @int = node.getInt("maxnum");
			int num4 = this.curi - this.Items.Count + num3;
			return num4 * @int >= num2 + num;
		}

		public bool hasItem(uint tpid)
		{
			bool result;
			foreach (a3_BagItemData current in this.Items.Values)
			{
				bool flag = current.tpid == tpid;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public string getEquipNameInfo(a3_BagItemData data)
		{
			return this.getEquipNameInfo(data.tpid, data.equipdata.stage, data.equipdata.intensify_lv, data.equipdata.add_level, data.confdata.equip_level, data.equipdata.attribute);
		}

		public string getEquipNameInfo(uint tpid, int stage_lv, int intensify_ly, int add_level, int equip_level, int attribute)
		{
			string text = "";
			int quality = 1;
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
			bool flag = sXML != null;
			if (flag)
			{
				text = sXML.getString("item_name");
				quality = sXML.getInt("quality");
			}
			string str = "";
			switch (stage_lv)
			{
			case 0:
				str = "普通的";
				break;
			case 1:
				str = "强化的";
				break;
			case 2:
				str = "打磨的";
				break;
			case 3:
				str = "优良的";
				break;
			case 4:
				str = "珍惜的";
				break;
			case 5:
				str = "祝福的";
				break;
			case 6:
				str = "完美的";
				break;
			case 7:
				str = "卓越的";
				break;
			case 8:
				str = "传说的";
				break;
			case 9:
				str = "神话的";
				break;
			case 10:
				str = "创世的";
				break;
			}
			text = str + text;
			string str2 = "";
			switch (attribute)
			{
			case 1:
				str2 = "[风]";
				break;
			case 2:
				str2 = "[火]";
				break;
			case 3:
				str2 = "[光]";
				break;
			case 4:
				str2 = "[雷]";
				break;
			case 5:
				str2 = "[冰]";
				break;
			}
			text += str2;
			string str3 = (intensify_ly > 0) ? ("+" + intensify_ly) : string.Empty;
			string text2 = (add_level > 0) ? ("追" + add_level.ToString()) : string.Empty;
			str3 = "<color=#ff5555ff>" + str3 + "</color>";
			text2 = "<color=#ff9500ff>" + text2 + "</color>";
			return Globle.getColorStrByQuality(text, quality) + str3 + text2;
		}

		public string getEquipName(a3_BagItemData data)
		{
			string result = string.Empty;
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + data.tpid);
			bool flag = sXML != null;
			if (flag)
			{
				result = sXML.getString("item_name");
			}
			return result;
		}

		public string getEquipName(uint tpid)
		{
			string result = string.Empty;
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
			bool flag = sXML != null;
			if (flag)
			{
				result = sXML.getString("item_name");
			}
			return result;
		}

		public void useItemByTpid(uint tpid, int num)
		{
			a3_ItemData itemDataById = this.getItemDataById(tpid);
			bool flag = itemDataById.use_type < 0;
			if (flag)
			{
				flytxt.instance.fly("该物品不能使用！", 0, default(Color), null);
			}
			else
			{
				int itemNumByTpid = this.getItemNumByTpid(tpid);
				bool flag2 = itemNumByTpid < num;
				if (flag2)
				{
					flytxt.instance.fly("物品数量不足！", 0, default(Color), null);
				}
				else
				{
					List<a3_BagItemData> list = new List<a3_BagItemData>();
					int num2 = 999999;
					foreach (a3_BagItemData current in this.Items.Values)
					{
						bool flag3 = current.tpid == tpid;
						if (flag3)
						{
							bool flag4 = current.num < num2;
							if (flag4)
							{
								num2 = current.num;
								list.Insert(0, current);
							}
							else
							{
								list.Add(current);
							}
						}
					}
					int num3 = num;
					foreach (a3_BagItemData current2 in list)
					{
						bool flag5 = current2.num > num3;
						if (flag5)
						{
							BaseProxy<BagProxy>.getInstance().sendUseItems(current2.id, num3);
							break;
						}
						BaseProxy<BagProxy>.getInstance().sendUseItems(current2.id, current2.num);
						num3 = num - current2.num;
					}
					A3_BeStronger expr_17D = A3_BeStronger.Instance;
					if (expr_17D != null)
					{
						expr_17D.CheckUpItem();
					}
				}
			}
		}

		public bool isWorked(a3_BagItemData data)
		{
			bool flag = false;
			bool flag2 = data.equipdata.baoshi != null;
			if (flag2)
			{
				bool flag3 = data.equipdata.baoshi.Count <= 0;
				if (flag3)
				{
					flag = true;
				}
				foreach (int current in data.equipdata.baoshi.Keys)
				{
					bool flag4 = data.equipdata.baoshi[current] > 0;
					if (flag4)
					{
						flag = false;
						break;
					}
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			bool flag5 = false;
			bool flag6 = data.equipdata.gem_att != null;
			if (flag6)
			{
				foreach (int current2 in data.equipdata.gem_att.Keys)
				{
					bool flag7 = data.equipdata.gem_att[current2] == 0;
					bool flag8 = flag7;
					if (flag8)
					{
						flag5 = true;
					}
					else
					{
						bool flag9 = !flag7;
						if (flag9)
						{
							flag5 = false;
							break;
						}
					}
				}
			}
			return (data.equipdata.intensify_lv == 0 && data.equipdata.stage == 0 && data.equipdata.add_level == 0) & flag5 & flag;
		}

		public bool HasBaoshi(a3_BagItemData data)
		{
			bool flag = false;
			bool flag2 = data.equipdata.baoshi != null;
			bool result;
			if (flag2)
			{
				bool flag3 = data.equipdata.baoshi.Count <= 0;
				if (flag3)
				{
					result = false;
					return result;
				}
				foreach (int current in data.equipdata.baoshi.Keys)
				{
					bool flag4 = data.equipdata.baoshi[current] > 0;
					if (flag4)
					{
						flag = true;
						break;
					}
				}
			}
			result = flag;
			return result;
		}

		public void OnMoneyChange()
		{
			A3_BeStronger expr_05 = A3_BeStronger.Instance;
			if (expr_05 != null)
			{
				expr_05.CheckUpItem();
			}
		}

		public void SellItem()
		{
			A3_BeStronger expr_05 = A3_BeStronger.Instance;
			if (expr_05 != null)
			{
				expr_05.CheckUpItem();
			}
		}

		public Dictionary<int, a3_RunestoneData> allRunestoneData()
		{
			Dictionary<int, a3_RunestoneData> dictionary = new Dictionary<int, a3_RunestoneData>();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "use_type==22");
			bool flag = sXMLList != null;
			if (flag)
			{
				foreach (SXML current in sXMLList)
				{
					a3_RunestoneData a3_RunestoneData = default(a3_RunestoneData);
					a3_RunestoneData.id = current.getInt("id");
					a3_RunestoneData.item_name = current.getString("item_name");
					a3_RunestoneData.icon_file = current.getInt("icon_file");
					a3_RunestoneData.desc = current.getString("desc");
					a3_RunestoneData.quality = current.getInt("quality");
					a3_RunestoneData.position = current.getInt("position");
					a3_RunestoneData.stone_level = current.getInt("stone_level");
					a3_RunestoneData.name_type = current.getInt("name_type");
					a3_RunestoneData.compose_data = this.NeedorGetMaterial(a3_RunestoneData.id, 1);
					a3_RunestoneData.decompose_data = this.NeedorGetMaterial(a3_RunestoneData.id, 2);
					dictionary[a3_RunestoneData.id] = a3_RunestoneData;
				}
			}
			return dictionary;
		}

		public a3_RunestoneData getRunestoneDataByid(int stone_id)
		{
			return this.allRunestoneData()[stone_id];
		}

		public string getRunestoneLvByid(int stone_id)
		{
			return this.allRunestoneData()[stone_id].stone_level.ToString();
		}

		public List<a3_RunestonrnMaterial> NeedorGetMaterial(int stone_id, int type)
		{
			List<a3_RunestonrnMaterial> list = new List<a3_RunestonrnMaterial>();
			SXML sXML = XMLMgr.instance.GetSXML("item.rune_stone", "id==" + stone_id);
			List<SXML> nodeList = sXML.GetNodeList((type == 1) ? "compose" : "decompose", "");
			bool flag = sXML != null && nodeList != null;
			if (flag)
			{
				foreach (SXML current in nodeList)
				{
					list.Add(new a3_RunestonrnMaterial
					{
						id = current.getInt("item"),
						num = current.getInt("num")
					});
				}
			}
			return list;
		}
	}
}
