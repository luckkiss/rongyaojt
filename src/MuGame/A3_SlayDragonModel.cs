using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_SlayDragonModel : ModelBase<A3_SlayDragonModel>
	{
		public Dictionary<int, string> dicDragonName;

		public Dictionary<string, DragonData> dicDragonData;

		public Dictionary<int, DragonInfo> dicDragonInfo;

		public A3_SlayDragonModel()
		{
			this.dicDragonName = new Dictionary<int, string>();
			this.dicDragonData = new Dictionary<string, DragonData>();
			this.dicDragonInfo = new Dictionary<int, DragonInfo>();
			this.ReadDrgnInfo();
		}

		private void ReadDrgnInfo()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("clan.clan_boss", "");
			for (int i = 0; i < sXMLList.Count; i++)
			{
				DragonInfo dragonInfo = new DragonInfo();
				dragonInfo.clan_lv = sXMLList[i].getUint("clan_lv");
				dragonInfo.item_cost = sXMLList[i].getInt("item_cost");
				dragonInfo.item_id = sXMLList[i].getUint("item_id");
				dragonInfo.diff_lvl = sXMLList[i].getInt("diff_lvl");
				dragonInfo.pre_min = sXMLList[i].getInt("tm_ready");
				dragonInfo.level_min = sXMLList[i].getInt("tm");
				List<SXML> nodeList = sXMLList[i].GetNodeList("award", "");
				dragonInfo.dragonName = new Dictionary<uint, string>();
				for (int j = 0; j < nodeList.Count; j++)
				{
					dragonInfo.rewardList.Add(new RewardInfo
					{
						dragonId = nodeList[j].getUint("level_id"),
						itemId = nodeList[j].getUint("item_id")
					});
					dragonInfo.dragonName[nodeList[j].getUint("level_id")] = nodeList[j].getString("name");
				}
				this.dicDragonInfo[dragonInfo.diff_lvl] = dragonInfo;
			}
		}

		public void SyncData(Variant data)
		{
			List<Variant> arr = data["tulong_lvl_ary"]._arr;
			for (int i = 0; i < arr.Count; i++)
			{
				Variant variant = arr[i];
				string key = this.dicDragonName[i];
				DragonData dragonData = new DragonData();
				dragonData.isUnlcoked = variant["zhaohuan"];
				dragonData.isDead = variant["death"]._bool;
				dragonData.proc = variant["jindu"]._uint;
				dragonData.isCreated = variant["create_tm"]._bool;
				dragonData.isOpened = variant["open"]._bool;
				dragonData.dragonId = variant["lvl_id"]._uint;
				bool flag = this.dicDragonData.ContainsKey(key);
				if (flag)
				{
					bool flag2 = !this.dicDragonData[key].isUnlcoked && (this.dicDragonData[key].isUnlcoked ^ dragonData.isUnlcoked);
					if (flag2)
					{
						flytxt.instance.fly("已解除封印", 0, default(Color), null);
					}
				}
				this.dicDragonData[key] = dragonData;
			}
		}

		public int GetPreMin()
		{
			return this.dicDragonInfo[this.GetUnlockedDiffLv()].pre_min;
		}

		public int GetKillingTime()
		{
			return this.dicDragonInfo[this.GetUnlockedDiffLv()].level_min - this.dicDragonInfo[this.GetUnlockedDiffLv()].pre_min;
		}

		public int GetLvMin()
		{
			return this.dicDragonInfo[this.GetUnlockedDiffLv()].level_min;
		}

		public DragonData GetDataById(uint tpid)
		{
			List<string> list = new List<string>(this.dicDragonData.Keys);
			DragonData result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = this.dicDragonData[list[i]].dragonId == tpid;
				if (flag)
				{
					result = this.dicDragonData[list[i]];
					return result;
				}
			}
			result = null;
			return result;
		}

		public uint GetIdByName(string dragonName)
		{
			List<string> list = new List<string>(this.dicDragonData.Keys);
			uint result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = list[i].Equals(dragonName);
				if (flag)
				{
					result = this.dicDragonData[list[i]].dragonId;
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public string GetCurrentDragonName()
		{
			bool flag = this.dicDragonInfo[this.GetUnlockedDiffLv()].dragonName.ContainsKey(this.GetUnlockedDragonId());
			string result;
			if (flag)
			{
				result = this.dicDragonInfo[this.GetUnlockedDiffLv()].dragonName[this.GetUnlockedDragonId()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetCost(int diffLvl = 0)
		{
			bool flag = diffLvl == 0;
			if (flag)
			{
				diffLvl = this.GetUnlockedDiffLv();
			}
			bool flag2 = this.dicDragonInfo.ContainsKey(diffLvl);
			int result;
			if (flag2)
			{
				result = this.dicDragonInfo[diffLvl].item_cost;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public uint GetUnlockedDragonId()
		{
			List<string> list = new List<string>(this.dicDragonData.Keys);
			uint result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = this.dicDragonData[list[i]].isUnlcoked && !this.dicDragonData[list[i]].isDead;
				if (flag)
				{
					result = this.dicDragonData[list[i]].dragonId;
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public DragonData GetUnlockedDragonData()
		{
			List<string> list = new List<string>(this.dicDragonData.Keys);
			DragonData result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = this.dicDragonData[list[i]].isUnlcoked && !this.dicDragonData[list[i]].isDead;
				if (flag)
				{
					result = this.dicDragonData[list[i]];
					return result;
				}
			}
			result = null;
			return result;
		}

		public int GetUnlockedDiffLv()
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.id != 0;
			int result;
			if (flag)
			{
				List<int> list = new List<int>(this.dicDragonInfo.Keys);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					bool flag2 = (ulong)this.dicDragonInfo[list[i]].clan_lv <= (ulong)((long)ModelBase<A3_LegionModel>.getInstance().myLegion.lvl);
					if (flag2)
					{
						result = this.dicDragonInfo[list[i]].diff_lvl;
						return result;
					}
				}
			}
			result = 0;
			return result;
		}

		public uint GetDragonKeyId()
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.id != 0;
			uint result;
			if (flag)
			{
				List<int> list = new List<int>(this.dicDragonInfo.Keys);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					bool flag2 = (ulong)this.dicDragonInfo[list[i]].clan_lv <= (ulong)((long)ModelBase<A3_LegionModel>.getInstance().myLegion.lvl);
					if (flag2)
					{
						result = this.dicDragonInfo[list[i]].item_id;
						return result;
					}
				}
			}
			result = 0u;
			return result;
		}

		public DragonInfo GetCurDragonLvInfo()
		{
			bool flag = ModelBase<A3_LegionModel>.getInstance().myLegion.id != 0;
			DragonInfo result;
			if (flag)
			{
				List<int> list = new List<int>(this.dicDragonInfo.Keys);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					bool flag2 = (ulong)this.dicDragonInfo[list[i]].clan_lv <= (ulong)((long)ModelBase<A3_LegionModel>.getInstance().myLegion.lvl);
					if (flag2)
					{
						result = this.dicDragonInfo[list[i]];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public uint GetRewardIdByDragonId(uint dragonId)
		{
			List<RewardInfo> rewardList = this.GetCurDragonLvInfo().rewardList;
			uint result;
			for (int i = 0; i < rewardList.Count; i++)
			{
				bool flag = rewardList[i].dragonId == dragonId;
				if (flag)
				{
					result = rewardList[i].itemId;
					return result;
				}
			}
			result = 0u;
			return result;
		}

		public bool IsAbleToUnlock()
		{
			return ModelBase<A3_LegionModel>.getInstance().myLegion.clanc >= 3;
		}

		public string GetNameById(uint dragonId)
		{
			List<string> list = new List<string>(this.dicDragonData.Keys);
			string result;
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = this.dicDragonData[list[i]].dragonId == dragonId;
				if (flag)
				{
					result = list[i];
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
