using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDAchieve : lgGDBase
	{
		private InGameAchieveMsgs _achieveMsg;

		private static int LOADING = 1;

		private static int LOAD_FIN = 2;

		private int _loadFlag = 0;

		private Variant _achieve = new Variant();

		public lgGDAchieve(gameManager m) : base(m)
		{
			this._achieveMsg = (this.g_mgr.g_netM as muNetCleint).igAchieveMsgs;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDAchieve(m as gameManager);
		}

		public override void init()
		{
		}

		public void GetAchieveList()
		{
			bool flag = this._loadFlag == 0;
			if (flag)
			{
				this._loadFlag = lgGDAchieve.LOADING;
				Variant variant = new Variant();
				variant["tp"] = 1;
				this._achieveMsg.GetAchieve(variant);
			}
		}

		public void GetAchieveAwd(int achieveID)
		{
			Variant variant = new Variant();
			variant["tp"] = 4;
			variant["id"] = achieveID;
			this._achieveMsg.GetAchieve(variant);
		}

		public void AchieveRes(Variant data)
		{
		}

		private void newAchieve(Variant data)
		{
		}

		public bool IsAchieveGetAwd(int id)
		{
			Variant hadAchieves = this.GetHadAchieves();
			bool result;
			foreach (Variant current in hadAchieves._arr)
			{
				bool flag = id == current["id"];
				if (flag)
				{
					result = (current.ContainsKey("awd") && current["awd"]);
					return result;
				}
			}
			result = false;
			return result;
		}

		public int GetCanAwdNum()
		{
			int num = 0;
			Variant hadAchieves = this.GetHadAchieves();
			foreach (Variant current in hadAchieves._arr)
			{
				bool flag = !current["awd"];
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public bool IsHadAchieve(int id)
		{
			Variant hadAchieves = this.GetHadAchieves();
			bool result;
			foreach (Variant current in hadAchieves._arr)
			{
				bool flag = id == current["id"];
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public Variant GetHadAchieves()
		{
			return this._achieve["ach"];
		}

		public Variant GetHadAchieve(int id)
		{
			Variant result;
			foreach (Variant current in this._achieve["ach"]._arr)
			{
				bool flag = id == current["id"];
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public uint GetAchieveTarCnt(int achieveid, uint tarid)
		{
			Variant currAchieve = this.GetCurrAchieve(achieveid);
			bool flag = currAchieve;
			uint result;
			if (flag)
			{
				foreach (Variant current in currAchieve["tar"]._arr)
				{
					bool flag2 = tarid == current["tid"];
					if (flag2)
					{
						result = current["cnt"]._uint;
						return result;
					}
				}
			}
			result = 0u;
			return result;
		}

		public Variant GetCurrAchieves()
		{
			return this._achieve["curr"];
		}

		public Variant GetCurrAchieve(int achieveid)
		{
			bool flag = this._achieve["curr"];
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._achieve["curr"]._arr)
				{
					bool flag2 = achieveid == current["id"];
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void JoinWorld()
		{
			this.GetAchieveList();
		}
	}
}
