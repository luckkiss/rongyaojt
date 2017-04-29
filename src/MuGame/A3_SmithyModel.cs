using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class A3_SmithyModel : ModelBase<A3_SmithyModel>
	{
		public int rlrnDiamondCost;

		public int rlrnMoneyCost;

		public Dictionary<uint, List<MatInfo>> dicSmithyInfo;

		public Dictionary<int, List<MatInfo>> smithyInfoDicUseScroll;

		public Dictionary<uint, SmithyEqpInfo> dicSmithyItemInfo;

		public List<SmithyInfo> smithyLevelInfoList;

		public int CurSmithyExp
		{
			get;
			set;
		}

		public int CurSmithyMaxExp
		{
			get;
			set;
		}

		public int CurSmithyLevel
		{
			get;
			set;
		}

		public SMITHY_PART CurSmithyType
		{
			get;
			set;
		}

		public A3_SmithyModel()
		{
			this.<CurSmithyType>k__BackingField = SMITHY_PART.NOT_LEARNT;
			base..ctor();
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("forge.forge_eqp", "");
			SXML sXML = XMLMgr.instance.GetSXML("forge.cost_way", "way==1");
			this.dicSmithyInfo = new Dictionary<uint, List<MatInfo>>();
			this.dicSmithyItemInfo = new Dictionary<uint, SmithyEqpInfo>();
			for (int i = 0; i < sXMLList.Count; i++)
			{
				uint @uint = sXMLList[i].getUint("eqp_id");
				int @int = sXMLList[i].getInt("forge_cost");
				List<SXML> nodeList = sXML.GetNode("cost", "forge_cost==" + @int).GetNodeList("item_cost", "");
				this.dicSmithyItemInfo[@uint] = new SmithyEqpInfo();
				this.dicSmithyItemInfo[@uint].tpid = @uint;
				this.dicSmithyItemInfo[@uint].forgeCostId = @int;
				this.dicSmithyItemInfo[@uint].money = sXMLList[i].getInt("money");
				this.dicSmithyItemInfo[@uint].exp = sXMLList[i].getInt("exp");
				this.dicSmithyItemInfo[@uint].forgeLvl = sXMLList[i].getInt("forge_lvl");
				this.dicSmithyInfo[@uint] = new List<MatInfo>();
				for (int j = 0; j < nodeList.Count; j++)
				{
					uint uint2 = nodeList[j].getUint("item_id");
					int int2 = nodeList[j].getInt("nums");
					bool flag = uint2 > 0u && int2 > 0;
					if (flag)
					{
						this.dicSmithyInfo[@uint].Add(new MatInfo
						{
							tpid = uint2,
							num = int2
						});
					}
				}
				this.dicSmithyItemInfo[@uint].matList = this.dicSmithyInfo[@uint];
			}
			sXML = XMLMgr.instance.GetSXML("forge.cost_way", "way==2");
			this.smithyInfoDicUseScroll = new Dictionary<int, List<MatInfo>>();
			List<SXML> list = XMLMgr.instance.GetSXMLList("forge.forge_type", "");
			for (int k = 0; k < list.Count; k++)
			{
				int int3 = list[k].getInt("type");
				bool flag2 = int3 > 0;
				if (flag2)
				{
					bool flag3 = !this.smithyInfoDicUseScroll.ContainsKey(int3);
					if (flag3)
					{
						this.smithyInfoDicUseScroll[int3] = new List<MatInfo>();
					}
					this.smithyInfoDicUseScroll[int3].Add(new MatInfo
					{
						tpid = list[k].getUint("item_id"),
						num = list[k].getInt("nums")
					});
					ScrollCostData scrollCostData = new ScrollCostData();
					scrollCostData.money = list[k].getInt("money");
					scrollCostData.costId = 1;
					SmithyEqpInfo.dicScrollCost[int3] = scrollCostData;
				}
			}
			list = sXML.GetNode("cost", "forge_cost==1").GetNodeList("item_cost", "");
			for (int l = 0; l < list.Count; l++)
			{
				uint uint3 = list[l].getUint("item_id");
				int int4 = list[l].getInt("nums");
				int int5 = list[l].getInt("type");
				bool flag4 = uint3 > 0u && int4 > 0;
				if (flag4)
				{
					int m = 0;
					List<int> list2 = new List<int>(this.smithyInfoDicUseScroll.Keys);
					while (m < list2.Count)
					{
						this.smithyInfoDicUseScroll[list2[m]].Add(new MatInfo
						{
							tpid = uint3,
							num = int4
						});
						SmithyEqpInfo.dicScrollCost[list2[m]].matList = this.smithyInfoDicUseScroll[list2[m]];
						m++;
					}
				}
			}
			List<SXML> sXMLList2 = XMLMgr.instance.GetSXMLList("forge.forge_lvl", "");
			this.smithyLevelInfoList = new List<SmithyInfo>();
			for (int n = 0; n < sXMLList2.Count; n++)
			{
				int int6 = sXMLList2[n].getInt("lvl");
				int int7 = sXMLList2[n].getInt("exp");
				int num = sXMLList2[n].getInt("max_set_lv");
				bool flag5 = num <= 0;
				if (flag5)
				{
					num = (int6 + 1) / 2;
				}
				bool flag6 = int6 > 0;
				if (flag6)
				{
					this.smithyLevelInfoList.Add(new SmithyInfo
					{
						Level = int6,
						ExpToNextLevel = int7,
						MaxAllowedSetLv = num
					});
				}
			}
			this.rlrnDiamondCost = XMLMgr.instance.GetSXML("forge.relearn", "").getInt("gem");
			this.rlrnMoneyCost = XMLMgr.instance.GetSXML("forge.relearn", "").getInt("gold");
		}

		public List<MatInfo> GetMatListById(uint tpid)
		{
			a3_EquipData equipByItemId = ModelBase<a3_EquipModel>.getInstance().getEquipByItemId(tpid);
			List<MatInfo> list = new List<MatInfo>();
			bool flag = this.dicSmithyInfo.ContainsKey(tpid);
			if (flag)
			{
				for (int i = 0; i < this.dicSmithyInfo[tpid].Count; i++)
				{
					list.Add(new MatInfo
					{
						tpid = this.dicSmithyInfo[tpid][i].tpid,
						num = this.dicSmithyInfo[tpid][i].num
					});
				}
			}
			return list;
		}

		public List<MatInfo> GetMatListUseScroll()
		{
			List<MatInfo> list = new List<MatInfo>();
			bool flag = this.smithyInfoDicUseScroll.ContainsKey((int)this.CurSmithyType);
			List<MatInfo> result;
			if (flag)
			{
				result = this.smithyInfoDicUseScroll[(int)this.CurSmithyType];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetMaxAllowedSetLevel(int level)
		{
			int result;
			for (int i = 0; i < this.smithyLevelInfoList.Count; i++)
			{
				bool flag = this.smithyLevelInfoList[i].Level == level;
				if (flag)
				{
					result = this.smithyLevelInfoList[i].MaxAllowedSetLv;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public int CalcMaxExp(int level)
		{
			this.CurSmithyMaxExp = this.smithyLevelInfoList[level - 1].ExpToNextLevel;
			return this.CurSmithyMaxExp;
		}

		public int GetMoneyCostById(uint tpid, int num = 1)
		{
			bool flag = this.dicSmithyItemInfo.ContainsKey(tpid);
			int result;
			if (flag)
			{
				result = this.dicSmithyItemInfo[tpid].money * num;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int GetMoneyCostByScroll(int num = 1)
		{
			bool flag = SmithyEqpInfo.dicScrollCost.ContainsKey((int)this.CurSmithyType);
			int result;
			if (flag)
			{
				result = SmithyEqpInfo.dicScrollCost[(int)this.CurSmithyType].money * num;
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
