using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class A3_AchievementModel : ModelBase<A3_AchievementModel>
	{
		public static uint HAVE_REACH_ACHIEVEMENT = 0u;

		private List<SXML> achievementXML;

		private Dictionary<uint, AchievementData> dicAchievementData;

		public List<uint> listCategory;

		public List<uint> listAchievementChange = new List<uint>();

		public List<uint> listReachAchievent;

		private uint achievementPoint;

		public Action OnAchievementChange = null;

		public List<SXML> AchievementXML
		{
			get
			{
				bool flag = this.achievementXML == null;
				if (flag)
				{
					this.achievementXML = XMLMgr.instance.GetSXMLList("achievement.achievement", "");
				}
				return this.achievementXML;
			}
		}

		public uint GetAchievementID
		{
			get;
			set;
		}

		public uint AchievementPoint
		{
			get
			{
				return this.achievementPoint;
			}
			set
			{
				bool flag = this.achievementPoint > 0u && value - this.achievementPoint > 0u;
				if (flag)
				{
					flytxt.instance.AddDelayFlytxt(ContMgr.getCont("achieve_get", new List<string>
					{
						(value - this.achievementPoint).ToString()
					}));
					flytxt.instance.StartDelayFly(0.25f, 0.2f);
				}
				bool flag2 = this.achievementPoint == value;
				if (!flag2)
				{
					this.achievementPoint = value;
					bool flag3 = this.OnAchievementChange != null;
					if (flag3)
					{
						this.OnAchievementChange();
					}
				}
			}
		}

		public A3_AchievementModel()
		{
			this.InitAchievementDic();
		}

		private void InitAchievementDic()
		{
			this.dicAchievementData = new Dictionary<uint, AchievementData>();
			this.listCategory = new List<uint>();
			this.listCategory.Add(0u);
			int count = this.AchievementXML.Count;
			for (int i = 0; i < count; i++)
			{
				AchievementData achievementData = new AchievementData();
				achievementData.id = this.AchievementXML[i].getUint("achievement_id");
				achievementData.category = this.AchievementXML[i].getUint("category");
				achievementData.name = this.AchievementXML[i].getString("name");
				achievementData.type = this.AchievementXML[i].getUint("type");
				achievementData.bndyb = this.AchievementXML[i].getUint("bndyb");
				achievementData.point = this.AchievementXML[i].getUint("point");
				achievementData.condition = this.AchievementXML[i].getUint("param1");
				achievementData.value = this.AchievementXML[i].getUint("param2");
				achievementData.desc = this.AchievementXML[i].getString("desc");
				achievementData.degree = 0u;
				bool flag = !this.listCategory.Contains(achievementData.category);
				if (flag)
				{
					this.listCategory.Add(achievementData.category);
				}
				this.dicAchievementData[achievementData.id] = achievementData;
			}
		}

		public void SyncAchievementDataByServer(Variant v)
		{
			bool flag = v.ContainsKey("achives");
			if (flag)
			{
				List<Variant> arr = v["achives"]._arr;
				foreach (Variant current in arr)
				{
					uint key = current["id"];
					uint degree = current["reach_num"];
					uint state = current["state"];
					bool flag2 = this.dicAchievementData.ContainsKey(key);
					if (flag2)
					{
						this.dicAchievementData[key].degree = degree;
						this.dicAchievementData[key].state = (AchievementState)state;
					}
				}
			}
			bool flag3 = v.ContainsKey("ach_point");
			if (flag3)
			{
				this.AchievementPoint = v["ach_point"];
				ModelBase<PlayerModel>.getInstance().ach_point = v["ach_point"];
				a3_RankModel.nowexp = v["ach_point"];
			}
		}

		public string GetCategoryName(uint categoryId)
		{
			return ContMgr.getCont("achieve_tag" + categoryId, null);
		}

		public void OnAchievementChangeFromServer(Variant v)
		{
			bool flag = v.ContainsKey("achives");
			if (flag)
			{
				List<Variant> arr = v["achives"]._arr;
				foreach (Variant current in arr)
				{
					uint num = current["id"];
					uint degree = current["reach_num"];
					uint state = current["state"];
					bool flag2 = !this.listAchievementChange.Contains(num);
					if (flag2)
					{
						this.listAchievementChange.Add(num);
					}
					bool flag3 = this.dicAchievementData.ContainsKey(num);
					if (flag3)
					{
						this.dicAchievementData[num].degree = degree;
						this.dicAchievementData[num].state = (AchievementState)state;
					}
				}
			}
		}

		public void OnAchievementReachChange(Variant v)
		{
			bool flag = v.ContainsKey("changed");
			if (flag)
			{
				this.listReachAchievent = new List<uint>();
				List<Variant> arr = v["changed"]._arr;
				foreach (Variant current in arr)
				{
					uint num = current["id"];
					uint degree = current["reach_num"];
					uint state = current["state"];
					bool flag2 = !this.listAchievementChange.Contains(num);
					if (flag2)
					{
						this.listAchievementChange.Add(num);
					}
					this.listReachAchievent.Add(num);
					bool flag3 = this.dicAchievementData.ContainsKey(num);
					if (flag3)
					{
						this.dicAchievementData[num].degree = degree;
						this.dicAchievementData[num].state = (AchievementState)state;
					}
				}
			}
		}

		public void OnGetAchievePrize(Variant v)
		{
			this.AchievementPoint = v["ach_point"];
			this.GetAchievementID = v["ach_id"];
			this.dicAchievementData[this.GetAchievementID].state = AchievementState.RECEIVED;
		}

		public List<AchievementData> GetAchievenementDataByType(uint category)
		{
			List<AchievementData> list = new List<AchievementData>();
			List<AchievementData> list2 = new List<AchievementData>();
			List<AchievementData> list3 = new List<AchievementData>();
			List<AchievementData> list4 = new List<AchievementData>();
			foreach (uint current in this.dicAchievementData.Keys)
			{
				AchievementData achievementData = this.dicAchievementData[current];
				bool flag = category == 0u || achievementData.category == category;
				if (flag)
				{
					switch (achievementData.state)
					{
					case AchievementState.UNREACHED:
						list4.Add(achievementData);
						break;
					case AchievementState.REACHED:
						list2.Add(achievementData);
						break;
					case AchievementState.RECEIVED:
						list3.Add(achievementData);
						break;
					}
				}
			}
			list.AddRange(list2);
			list.AddRange(list4);
			list.AddRange(list3);
			return list;
		}

		public AchievementData GetAchievementDataByID(uint id)
		{
			AchievementData result = null;
			bool flag = this.dicAchievementData.ContainsKey(id);
			if (flag)
			{
				result = this.dicAchievementData[id];
			}
			return result;
		}
	}
}
