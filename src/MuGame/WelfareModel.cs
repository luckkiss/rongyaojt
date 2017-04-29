using Cross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MuGame
{
	internal class WelfareModel : ModelBase<WelfareModel>
	{
		public struct itemWelfareData
		{
			public string desc;

			public string strIcon;

			public string strToggle;

			public uint num;

			public uint id;

			public uint itemId;

			public uint cumulateNum;

			public uint timesId;

			public uint needTime;

			public uint rewardId;

			public uint stateId;

			public uint last;

			public uint zhuan;

			public uint lvl;

			public uint worth;

			public string awardName;
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly WelfareModel.<>c <>9 = new WelfareModel.<>c();

			public static Func<Variant, Dictionary<string, Variant>.ValueCollection> <>9__11_0;

			public static Func<Variant, Dictionary<string, Variant>.ValueCollection> <>9__15_0;

			internal Dictionary<string, Variant>.ValueCollection <for_dengjilibao>b__11_0(Variant i)
			{
				return i.Values;
			}

			internal Dictionary<string, Variant>.ValueCollection <common>b__15_0(Variant i)
			{
				return i.Values;
			}
		}

		private SXML itemsXMLList;

		private Dictionary<a3_ItemData, uint> itemDataList;

		public WelfareModel()
		{
			this.itemsXMLList = XMLMgr.instance.GetSXML("welfare", null);
			this.itemDataList = new Dictionary<a3_ItemData, uint>();
		}

		private Dictionary<uint, uint> getFirstChargeXml()
		{
			Dictionary<uint, uint> dictionary = new Dictionary<uint, uint>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("firstcharge", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("item", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("carr");
					dictionary.Add(@uint, uint2);
				}
			}
			return dictionary;
		}

		public Dictionary<a3_ItemData, uint> getFirstChargeDataList()
		{
			this.itemDataList.Clear();
			Dictionary<uint, uint> firstChargeXml = this.getFirstChargeXml();
			foreach (KeyValuePair<uint, uint> current in firstChargeXml)
			{
				uint key = current.Key;
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(key);
				this.itemDataList.Add(itemDataById, current.Value);
			}
			return this.itemDataList;
		}

		public List<WelfareModel.itemWelfareData> getDailyLogin()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("dailyreward", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("day", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("item_id");
					uint uint3 = nodeList2[j].getUint("num");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						itemId = uint2,
						num = uint3
					});
				}
			}
			return list;
		}

		public List<WelfareModel.itemWelfareData> getOLReward()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("olreward", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("times", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("need_time");
					uint uint3 = nodeList2[j].getUint("num");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						itemId = uint2,
						num = uint3
					});
				}
			}
			return list;
		}

		public List<WelfareModel.itemWelfareData> getLevelReward()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("level_reward", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("level", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("zhuan");
					uint uint3 = nodeList2[j].getUint("lvl");
					uint uint4 = nodeList2[j].getUint("item_id");
					uint uint5 = nodeList2[j].getUint("num");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						zhuan = uint2,
						lvl = uint3,
						itemId = uint4,
						num = uint5
					});
				}
			}
			return list;
		}

		public List<WelfareModel.itemWelfareData> getCumulateRechargeAward()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("charge_cumulate", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("charge", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("cumulate");
					uint uint3 = nodeList2[j].getUint("item_id");
					uint uint4 = nodeList2[j].getUint("num");
					uint uint5 = nodeList2[j].getUint("worth");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						cumulateNum = uint2,
						itemId = uint3,
						num = uint4,
						worth = uint5
					});
				}
			}
			return list;
		}

		public List<WelfareModel.itemWelfareData> getCumulateConsumption()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("consumption_cumulate", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("consumption", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("cumulate");
					uint uint3 = nodeList2[j].getUint("item_id");
					uint uint4 = nodeList2[j].getUint("num");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						cumulateNum = uint2,
						itemId = uint3,
						num = uint4
					});
				}
			}
			return list;
		}

		public List<WelfareModel.itemWelfareData> getDailyRecharge()
		{
			List<WelfareModel.itemWelfareData> list = new List<WelfareModel.itemWelfareData>();
			List<SXML> nodeList = this.itemsXMLList.GetNodeList("daily_charge", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				SXML sXML = nodeList[i];
				List<SXML> nodeList2 = nodeList[i].GetNodeList("charge", null);
				for (int j = 0; j < nodeList2.Count; j++)
				{
					uint @uint = nodeList2[j].getUint("id");
					uint uint2 = nodeList2[j].getUint("cumulate");
					uint uint3 = nodeList2[j].getUint("item_id");
					uint uint4 = nodeList2[j].getUint("num");
					list.Add(new WelfareModel.itemWelfareData
					{
						id = @uint,
						cumulateNum = uint2,
						itemId = uint3,
						num = uint4
					});
				}
			}
			return list;
		}

		public bool for_dengjilibao(List<Variant> lst_zhuan)
		{
			uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
			bool flag = lst_zhuan.Count > 0;
			bool result;
			if (flag)
			{
				Func<Variant, Dictionary<string, Variant>.ValueCollection> arg_3A_1;
				if ((arg_3A_1 = WelfareModel.<>c.<>9__11_0) == null)
				{
					arg_3A_1 = (WelfareModel.<>c.<>9__11_0 = new Func<Variant, Dictionary<string, Variant>.ValueCollection>(WelfareModel.<>c.<>9.<for_dengjilibao>b__11_0));
				}
				lst_zhuan.OrderBy(arg_3A_1);
				result = (up_lvl > lst_zhuan[lst_zhuan.Count - 1]);
			}
			else
			{
				result = (up_lvl >= this.getLevelReward()[0].zhuan);
			}
			return result;
		}

		public bool for_leijichongzhi(List<Variant> lst_leijichongzhi)
		{
			uint totalRecharge = welfareProxy.totalRecharge;
			return this.common(totalRecharge, lst_leijichongzhi, this.getCumulateRechargeAward());
		}

		public bool for_leixjixiaofei(List<Variant> lst_leijixiaofei)
		{
			uint totalXiaofei = welfareProxy.totalXiaofei;
			return this.common(totalXiaofei, lst_leijixiaofei, this.getCumulateConsumption());
		}

		public bool for_jinrichongzhi(List<Variant> lst_chongzhi_today)
		{
			uint todayTotal_recharge = welfareProxy.todayTotal_recharge;
			return this.common(todayTotal_recharge, lst_chongzhi_today, this.getDailyRecharge());
		}

		private bool common(uint num, List<Variant> lst, List<WelfareModel.itemWelfareData> data)
		{
			int num2 = 0;
			bool flag = lst.Count > 1;
			if (flag)
			{
				Func<Variant, Dictionary<string, Variant>.ValueCollection> arg_31_1;
				if ((arg_31_1 = WelfareModel.<>c.<>9__15_0) == null)
				{
					arg_31_1 = (WelfareModel.<>c.<>9__15_0 = new Func<Variant, Dictionary<string, Variant>.ValueCollection>(WelfareModel.<>c.<>9.<common>b__15_0));
				}
				lst.OrderBy(arg_31_1);
				num2 = lst[lst.Count - 1];
			}
			bool flag2 = num2 < 10;
			return flag2 && num >= data[num2 + 1].cumulateNum;
		}
	}
}
