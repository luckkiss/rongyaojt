using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDBuddy : lgGDBase
	{
		protected Dictionary<uint, Variant> buddy_friend_list;

		protected Dictionary<uint, Variant> buddy_badFriend_list;

		protected Dictionary<uint, Variant> buddy_enemy_list;

		protected Variant becomeBuddy_list = new Variant();

		protected bool _bIsRegProcessForQueryOlBuddy = false;

		protected const uint _uintTimeInterval = 3000u;

		protected uint _uintTimeLeftToQueryOnlineBuddyData = 3000u;

		public lgGDBuddy(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDBuddy(m as gameManager);
		}

		public override void init()
		{
		}

		public void addBuddy(Variant buddyData)
		{
		}

		public void queryAllOnlineBuddy()
		{
			InGameBuddyMsgs inGameBuddyMsgs = this.g_mgr.g_netM.getObject("MSG_BUDDY") as InGameBuddyMsgs;
			inGameBuddyMsgs.get_buddy_ol(1u);
			inGameBuddyMsgs.get_buddy_ol(2u);
			inGameBuddyMsgs.get_buddy_ol(3u);
		}

		public void queryOnlineBuddyData(double tm)
		{
			bool flag = tm >= this._uintTimeLeftToQueryOnlineBuddyData;
			if (flag)
			{
				this.queryAllOnlineBuddy();
				this._uintTimeLeftToQueryOnlineBuddyData = 3000u;
			}
			else
			{
				this._uintTimeLeftToQueryOnlineBuddyData -= Convert.ToUInt32(tm);
			}
		}

		public void rmvBuddy(uint fcid, uint type)
		{
		}

		public void setBuddyList(Variant buddyData)
		{
			Variant variant = buddyData["buddy"];
			switch (buddyData["type"]._int)
			{
			case 1:
				this.buddy_friend_list = new Dictionary<uint, Variant>();
				break;
			case 2:
				this.buddy_enemy_list = new Dictionary<uint, Variant>();
				break;
			case 3:
				this.buddy_badFriend_list = new Dictionary<uint, Variant>();
				break;
			}
			for (int i = 0; i < variant.Count; i++)
			{
				variant[i]["type"] = buddyData["type"];
				switch (buddyData["type"]._int)
				{
				case 1:
					this.buddy_friend_list[variant[i]["fcid"]._uint] = variant[i];
					break;
				case 2:
					this.buddy_enemy_list[variant[i]["fcid"]._uint] = variant[i];
					break;
				case 3:
					this.buddy_badFriend_list[variant[i]["fcid"]._uint] = variant[i];
					break;
				}
			}
		}

		public void becomeBuddy(Variant becomeBuddyData)
		{
		}

		public void getOLBuddy(Variant buddyData)
		{
			Variant variant = buddyData["buddy"];
			Dictionary<uint, Variant> dictionary = new Dictionary<uint, Variant>();
			switch (buddyData["type"]._int)
			{
			case 1:
				dictionary = this.buddy_friend_list;
				break;
			case 2:
				dictionary = this.buddy_enemy_list;
				break;
			case 3:
				dictionary = this.buddy_badFriend_list;
				break;
			}
			for (int i = 0; i < variant.Count; i++)
			{
				bool flag = dictionary[variant[i]["fcid"]._uint] != null;
				if (flag)
				{
					bool flag2 = !dictionary[variant[i]["fcid"]._uint].ContainsKey("online") || variant[i]["online"] != dictionary[variant[i]["fcid"]._uint]["online"];
					if (flag2)
					{
						dictionary[variant[i]["fcid"]._uint]["online"] = variant[i]["online"];
					}
				}
			}
		}

		public uint getOLBuddyCnt(uint type)
		{
			Dictionary<uint, Variant> dictionary = new Dictionary<uint, Variant>();
			switch (type)
			{
			case 1u:
				dictionary = this.buddy_friend_list;
				break;
			case 2u:
				dictionary = this.buddy_enemy_list;
				break;
			case 3u:
				dictionary = this.buddy_badFriend_list;
				break;
			}
			uint num = 0u;
			foreach (Variant current in dictionary.Values)
			{
				bool flag = current["online"];
				if (flag)
				{
					num += 1u;
				}
			}
			return num;
		}

		public uint getBuddyCnt(uint type)
		{
			Dictionary<uint, Variant> dictionary = new Dictionary<uint, Variant>();
			switch (type)
			{
			case 1u:
				dictionary = this.buddy_friend_list;
				break;
			case 2u:
				dictionary = this.buddy_enemy_list;
				break;
			case 3u:
				dictionary = this.buddy_badFriend_list;
				break;
			}
			uint num = 0u;
			foreach (Variant current in dictionary.Values)
			{
				bool flag = current["type"] == type;
				if (flag)
				{
					num += 1u;
				}
			}
			return num;
		}

		public Dictionary<uint, Variant> getBuddyList(uint type)
		{
			Dictionary<uint, Variant> result;
			switch (type)
			{
			case 1u:
			{
				bool flag = this.buddy_friend_list != null;
				if (flag)
				{
					result = this.buddy_friend_list;
				}
				else
				{
					InGameBuddyMsgs inGameBuddyMsgs = this.g_mgr.g_netM.getObject("MSG_BUDDY") as InGameBuddyMsgs;
					inGameBuddyMsgs.get_buddy(1u);
					result = null;
				}
				break;
			}
			case 2u:
			{
				bool flag2 = this.buddy_enemy_list != null;
				if (flag2)
				{
					result = this.buddy_enemy_list;
				}
				else
				{
					InGameBuddyMsgs inGameBuddyMsgs2 = this.g_mgr.g_netM.getObject("MSG_BUDDY") as InGameBuddyMsgs;
					inGameBuddyMsgs2.get_buddy(2u);
					result = null;
				}
				break;
			}
			case 3u:
			{
				bool flag3 = this.buddy_badFriend_list != null;
				if (flag3)
				{
					result = this.buddy_badFriend_list;
				}
				else
				{
					InGameBuddyMsgs inGameBuddyMsgs3 = this.g_mgr.g_netM.getObject("MSG_BUDDY") as InGameBuddyMsgs;
					inGameBuddyMsgs3.get_buddy(3u);
					result = null;
				}
				break;
			}
			default:
				result = null;
				break;
			}
			return result;
		}

		public Variant getBecomeBuddyList()
		{
			int num = 0;
			foreach (Variant current in this.becomeBuddy_list.Values)
			{
				num++;
			}
			bool flag = num > 0;
			Variant result;
			if (flag)
			{
				result = this.becomeBuddy_list;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
