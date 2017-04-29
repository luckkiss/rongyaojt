using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class FriendProxy : BaseProxy<FriendProxy>
	{
		public enum FriendType
		{
			NON,
			FRIEND,
			ADD_FRIEND,
			AGREEAPPLYFRIEND,
			BLACKLIST,
			REFUSEADDFRIEND,
			DELETEFRIEND,
			REMOVEBLACKLIST,
			ENEMYPOSTION = 9,
			NEARBY,
			SHOWTARGETINFO,
			DELETEENEMY,
			ONLINERECOMMEND,
			RECEIVEAPPLYFRIEND = 15,
			RECEIVEENEMY,
			BEREFUSE
		}

		public static uint EVENT_FRIENDLIST = 1701u;

		public static uint EVENT_BLACKLIST = 1702u;

		public static uint EVENT_ENEMYLIST = 1703u;

		public static uint EVENT_NEARBYLIST = 1704u;

		public static uint EVENT_RECOMMEND = 1705u;

		public static uint EVENT_LOOKFRIEND = 11u;

		public static uint EVENT_AGREEAPLYRFRIEND = 1706u;

		public static uint EVENT_DELETEFRIEND = 1707u;

		public static uint EVENT_RECEIVEADDBLACKLIST = 1708u;

		public static uint EVENT_DELETEENEMY = 1709u;

		public static uint EVENT_ENEMYPOSTION = 1710u;

		public static uint EVENT_DELETEBLACKLIST = 1711u;

		public static uint EVENT_REMOVENEARYBY = 1712u;

		public static uint EVENT_ADDNEARYBY = 1713u;

		public static uint EVENT_REFRESHNEARYBY = 1714u;

		public static uint EVENT_RECEIVEAPPLYFRIEND = 1715u;

		public Dictionary<uint, itemFriendData> FriendDataList = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, itemFriendData> requestFirendList = new Dictionary<uint, itemFriendData>();

		public List<string> requestFriendListNoAgree = new List<string>();

		public Dictionary<uint, itemFriendData> BlackDataList = new Dictionary<uint, itemFriendData>();

		public List<itemFriendData> NearbyDataList = new List<itemFriendData>();

		public Dictionary<uint, itemFriendData> EnemyDataList = new Dictionary<uint, itemFriendData>();

		public List<itemFriendData> RecommendDataList = new List<itemFriendData>();

		public FriendProxy()
		{
			this.addProxyListener(170u, new Action<Variant>(this.onfriendinfo));
		}

		public void onfriendinfo(Variant data)
		{
			debug.Log("好友信息：" + data.dump());
			FriendProxy.FriendType friendType = FriendProxy.FriendType.NON;
			bool flag = data.ContainsKey("res");
			if (flag)
			{
				int @int = data["res"]._int;
				bool flag2 = @int > 0;
				if (flag2)
				{
					friendType = (FriendProxy.FriendType)@int;
				}
				else
				{
					Globle.err_output(@int);
				}
			}
			switch (friendType)
			{
			case FriendProxy.FriendType.FRIEND:
				this.setBuddy(data);
				this.setBlackList(data);
				this.setEnemyList(data);
				break;
			case FriendProxy.FriendType.ADD_FRIEND:
				flytxt.instance.fly("好友申请已发送，等待对方同意。", 0, default(Color), null);
				break;
			case FriendProxy.FriendType.AGREEAPPLYFRIEND:
				this.setAgreeAplyFriend(data);
				break;
			case FriendProxy.FriendType.BLACKLIST:
				this.receiveAddToBlackList(data);
				break;
			case FriendProxy.FriendType.REFUSEADDFRIEND:
				this.setRefuseAddFriend(data);
				break;
			case FriendProxy.FriendType.DELETEFRIEND:
				this.setDeleteFriend(data);
				break;
			case FriendProxy.FriendType.REMOVEBLACKLIST:
				this.setRemoveBlackList(data);
				break;
			case FriendProxy.FriendType.ENEMYPOSTION:
				this.setEnemyPostion(data);
				break;
			case FriendProxy.FriendType.SHOWTARGETINFO:
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_LOOKFRIEND, this, data, false));
				break;
			case FriendProxy.FriendType.DELETEENEMY:
				this.setDeleteEnemy(data);
				break;
			case FriendProxy.FriendType.ONLINERECOMMEND:
				this.RecommendFriend(data);
				break;
			case FriendProxy.FriendType.RECEIVEAPPLYFRIEND:
				this.ReceiveApplyFriend(data);
				break;
			case FriendProxy.FriendType.RECEIVEENEMY:
				this.setReceiveEnemy(data);
				break;
			case FriendProxy.FriendType.BEREFUSE:
				this.setBeRefuse(data);
				break;
			}
		}

		private void setBuddy(Variant data)
		{
			bool flag = data.ContainsKey("buddy");
			if (flag)
			{
				List<Variant> arr = data["buddy"]._arr;
				this.FriendDataList.Clear();
				foreach (Variant current in arr)
				{
					itemFriendData itemFriendData = default(itemFriendData);
					itemFriendData.cid = current["cid"]._uint;
					itemFriendData.name = current["name"]._str;
					itemFriendData.carr = current["carr"]._uint;
					itemFriendData.lvl = current["lvl"]._int;
					itemFriendData.zhuan = current["zhuan"]._uint;
					itemFriendData.clan_name = (string.IsNullOrEmpty(current["clan_name"]._str) ? "无" : current["clan_name"]._str);
					itemFriendData.combpt = current["combpt"]._uint;
					itemFriendData.online = current["online"]._bool;
					itemFriendData.mlzd_lv = current["mlzd_diff"]._int;
					bool flag2 = !itemFriendData.online;
					if (flag2)
					{
						itemFriendData.map_id = -1;
					}
					bool flag3 = current.ContainsKey("map_id");
					if (flag3)
					{
						itemFriendData.map_id = current["map_id"]._int;
					}
					bool flag4 = current.ContainsKey("llid");
					if (flag4)
					{
						itemFriendData.llid = (uint)current["llid"]._int;
					}
					itemFriendData.isNew = false;
					this.FriendDataList[current["cid"]._uint] = itemFriendData;
				}
				List<uint> list = new List<uint>(this.FriendDataList.Keys);
				for (int i = 0; i < list.Count; i++)
				{
					for (int j = 0; j < list.Count; j++)
					{
						bool flag5 = i < j && !this.FriendDataList[list[i]].online && this.FriendDataList[list[j]].online;
						if (flag5)
						{
							itemFriendData value = this.FriendDataList[list[i]];
							this.FriendDataList[list[i]] = this.FriendDataList[list[j]];
							this.FriendDataList[list[j]] = value;
						}
					}
				}
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_FRIENDLIST, this, data, false));
			}
		}

		private void setBlackList(Variant data)
		{
			bool flag = data.ContainsKey("blacklist");
			if (flag)
			{
				List<Variant> arr = data["blacklist"]._arr;
				this.BlackDataList.Clear();
				foreach (Variant current in arr)
				{
					itemFriendData itemFriendData = default(itemFriendData);
					itemFriendData.cid = current["cid"]._uint;
					itemFriendData.name = current["name"]._str;
					itemFriendData.carr = current["carr"]._uint;
					itemFriendData.lvl = current["lvl"]._int;
					itemFriendData.zhuan = current["zhuan"]._uint;
					bool flag2 = current.ContainsKey("mlzd_diff");
					if (flag2)
					{
						itemFriendData.mlzd_lv = current["mlzd_diff"]._int;
					}
					itemFriendData.clan_name = (string.IsNullOrEmpty(current["clan_name"]._str) ? "无" : current["clan_name"]._str);
					itemFriendData.combpt = current["combpt"]._uint;
					bool flag3 = current.ContainsKey("online");
					if (flag3)
					{
						itemFriendData.online = current["online"]._bool;
						bool flag4 = !itemFriendData.online;
						if (flag4)
						{
							itemFriendData.map_id = -1;
						}
					}
					bool flag5 = current.ContainsKey("map_id");
					if (flag5)
					{
						itemFriendData.map_id = current["map_id"]._int;
					}
					this.BlackDataList[current["cid"]._uint] = itemFriendData;
				}
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_BLACKLIST, this, data, false));
			}
		}

		private void receiveAddToBlackList(Variant data)
		{
			itemFriendData itemFriendData = default(itemFriendData);
			itemFriendData.cid = data["cid"]._uint;
			itemFriendData.name = data["name"]._str;
			itemFriendData.carr = data["carr"]._uint;
			itemFriendData.lvl = data["lvl"]._int;
			itemFriendData.zhuan = data["zhuan"]._uint;
			bool flag = data.ContainsKey("mlzd_diff");
			if (flag)
			{
				itemFriendData.mlzd_lv = data["mlzd_diff"]._int;
			}
			itemFriendData.clan_name = (string.IsNullOrEmpty(data["clan_name"]._str) ? "无" : data["clan_name"]._str);
			itemFriendData.combpt = data["combpt"]._uint;
			bool flag2 = data.ContainsKey("online");
			if (flag2)
			{
				itemFriendData.online = data["online"]._bool;
				bool flag3 = !itemFriendData.online;
				if (flag3)
				{
					itemFriendData.map_id = -1;
				}
			}
			bool flag4 = data.ContainsKey("map_id");
			if (flag4)
			{
				itemFriendData.map_id = data["map_id"]._int;
			}
			this.BlackDataList[data["cid"]._uint] = itemFriendData;
			flytxt.instance.fly(itemFriendData.name + "已加入黑名单.", 0, default(Color), null);
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_RECEIVEADDBLACKLIST, this, data, false));
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_BLACKLIST, this, data, false));
			bool flag5 = this.FriendDataList.ContainsKey(itemFriendData.cid);
			if (flag5)
			{
				this.FriendDataList.Remove(itemFriendData.cid);
			}
		}

		private void setEnemyList(Variant data)
		{
			bool flag = data.ContainsKey("foes");
			if (flag)
			{
				List<Variant> arr = data["foes"]._arr;
				this.EnemyDataList.Clear();
				foreach (Variant current in arr)
				{
					itemFriendData itemFriendData = default(itemFriendData);
					itemFriendData.cid = current["cid"]._uint;
					itemFriendData.name = current["name"]._str;
					itemFriendData.carr = current["carr"]._uint;
					itemFriendData.lvl = current["lvl"]._int;
					itemFriendData.zhuan = current["zhuan"]._uint;
					bool flag2 = current.ContainsKey("mlzd_diff");
					if (flag2)
					{
						itemFriendData.mlzd_lv = current["mlzd_diff"]._int;
					}
					itemFriendData.clan_name = (string.IsNullOrEmpty(current["clan_name"]._str) ? "无" : current["clan_name"]._str);
					itemFriendData.combpt = current["combpt"]._uint;
					itemFriendData.hatred = current["hatred"]._uint;
					itemFriendData.kill_tm = current["kill_tm"]._uint;
					bool flag3 = current.ContainsKey("map_id");
					if (flag3)
					{
						itemFriendData.map_id = (int)current["map_id"]._uint;
					}
					else
					{
						itemFriendData.map_id = 0;
					}
					bool flag4 = current.ContainsKey("llid");
					if (flag4)
					{
						itemFriendData.llid = current["llid"]._uint;
					}
					bool flag5 = this.FriendDataList.ContainsKey(itemFriendData.cid);
					if (flag5)
					{
						bool flag6 = !this.FriendDataList[itemFriendData.cid].online;
						if (flag6)
						{
							itemFriendData.map_id = -1;
						}
						else
						{
							itemFriendData.map_id = this.FriendDataList[itemFriendData.cid].map_id;
						}
					}
					this.EnemyDataList[current["cid"]._uint] = itemFriendData;
				}
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_ENEMYLIST, this, data, false));
			}
		}

		private void setDeleteFriend(Variant data)
		{
			itemFriendData itemFriendData = default(itemFriendData);
			itemFriendData.cid = data["cid"]._uint;
			bool flag = this.FriendDataList.ContainsKey(itemFriendData.cid);
			if (flag)
			{
				this.FriendDataList.Remove(itemFriendData.cid);
			}
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_DELETEFRIEND, this, data, false));
		}

		private void setAgreeAplyFriend(Variant data)
		{
			itemFriendData itemFriendData = default(itemFriendData);
			itemFriendData.cid = data["cid"]._uint;
			itemFriendData.name = data["name"]._str;
			itemFriendData.carr = data["carr"]._uint;
			itemFriendData.lvl = data["lvl"]._int;
			itemFriendData.zhuan = data["zhuan"]._uint;
			bool flag = data.ContainsKey("mlzd_diff");
			if (flag)
			{
				itemFriendData.mlzd_lv = data["mlzd_diff"]._int;
			}
			itemFriendData.clan_name = (string.IsNullOrEmpty(data["clan_name"]._str) ? "无" : data["clan_name"]._str);
			itemFriendData.combpt = data["combpt"]._uint;
			itemFriendData.online = data["online"]._bool;
			bool flag2 = !itemFriendData.online;
			if (flag2)
			{
				itemFriendData.map_id = -1;
			}
			bool flag3 = data.ContainsKey("map_id");
			if (flag3)
			{
				itemFriendData.map_id = data["map_id"]._int;
			}
			bool flag4 = data.ContainsKey("llid");
			if (flag4)
			{
				itemFriendData.llid = (uint)data["llid"]._int;
			}
			itemFriendData.isNew = false;
			this.FriendDataList[data["cid"]._uint] = itemFriendData;
			bool flag5 = this.requestFriendListNoAgree.Contains(itemFriendData.name);
			if (flag5)
			{
				flytxt.instance.fly(itemFriendData.name + "已同意了您的好友申请.", 0, default(Color), null);
				this.requestFriendListNoAgree.Remove(itemFriendData.name);
			}
			else
			{
				flytxt.instance.fly(itemFriendData.name + "与您已成为好友.", 0, default(Color), null);
			}
			bool flag6 = this.BlackDataList.ContainsKey(itemFriendData.cid);
			if (flag6)
			{
				this.sendRemoveBlackList(itemFriendData.cid);
			}
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_AGREEAPLYRFRIEND, this, data, false));
			friendList expr_24D = friendList._instance;
			if (expr_24D != null)
			{
				expr_24D.onShowOnlineFriendsChanged(true);
			}
		}

		private void ReceiveApplyFriend(Variant data)
		{
			bool iGNORE_FRIEND_ADD_REMINDER = GlobleSetting.IGNORE_FRIEND_ADD_REMINDER;
			if (!iGNORE_FRIEND_ADD_REMINDER)
			{
				ArrayList arrayList = new ArrayList();
				bool flag = data.ContainsKey("cid");
				if (flag)
				{
					arrayList.Add(data["cid"]);
				}
				bool flag2 = data.ContainsKey("name");
				if (flag2)
				{
					arrayList.Add(data["name"]);
				}
				bool flag3 = data.ContainsKey("carr");
				if (flag3)
				{
					arrayList.Add(data["carr"]);
				}
				bool flag4 = data.ContainsKey("clan_name");
				if (flag4)
				{
					arrayList.Add(data["clan_name"]);
				}
				bool flag5 = data.ContainsKey("lvl");
				if (flag5)
				{
					arrayList.Add(data["lvl"]);
				}
				bool flag6 = data.ContainsKey("zhuan");
				if (flag6)
				{
					arrayList.Add(data["zhuan"]);
				}
				bool flag7 = data.ContainsKey("combpt");
				if (flag7)
				{
					arrayList.Add(data["combpt"]);
				}
				uint num = data.ContainsKey("cid") ? data["cid"]._uint : 0u;
				bool flag8 = num > 0u;
				if (flag8)
				{
					bool flag9 = this.BlackDataList.ContainsKey(num);
					if (!flag9)
					{
						itemFriendData itemFriendData = default(itemFriendData);
						itemFriendData.cid = data["cid"]._uint;
						itemFriendData.name = data["name"]._str;
						itemFriendData.carr = data["carr"]._uint;
						itemFriendData.lvl = data["lvl"]._int;
						itemFriendData.zhuan = data["zhuan"]._uint;
						bool flag10 = data.ContainsKey("mlzd_diff");
						if (flag10)
						{
							itemFriendData.mlzd_lv = data["mlzd_diff"]._int;
						}
						bool flag11 = data.ContainsKey("clan_name");
						if (flag11)
						{
							itemFriendData.clan_name = (string.IsNullOrEmpty(data["clan_name"]._str) ? "无" : data["clan_name"]._str);
						}
						itemFriendData.combpt = data["combpt"]._uint;
						bool flag12 = data.ContainsKey("online");
						if (flag12)
						{
							itemFriendData.online = data["online"]._bool;
							bool flag13 = !itemFriendData.online;
							if (flag13)
							{
								itemFriendData.map_id = -1;
							}
						}
						bool flag14 = data.ContainsKey("map_id");
						if (flag14)
						{
							itemFriendData.map_id = data["map_id"]._int;
						}
						bool flag15 = data.ContainsKey("llid");
						if (flag15)
						{
							itemFriendData.llid = (uint)data["llid"]._int;
						}
						itemFriendData.isNew = false;
						bool flag16 = !this.requestFriendListNoAgree.Contains(data["name"]);
						if (flag16)
						{
							bool flag17 = !this.requestFirendList.ContainsKey(num);
							if (flag17)
							{
								this.requestFirendList.Add(itemFriendData.cid, itemFriendData);
							}
							base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_RECEIVEAPPLYFRIEND, this, null, false));
						}
						else
						{
							this.requestFriendListNoAgree.Remove(itemFriendData.name);
							this.FriendDataList[data["cid"]._uint] = itemFriendData;
							base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_FRIENDLIST, this, data, false));
							flytxt.instance.fly(itemFriendData.name + "同意了您的好友申请.", 0, default(Color), null);
						}
					}
				}
			}
		}

		private void RecommendFriend(Variant data)
		{
			bool flag = data.ContainsKey("buddys");
			if (flag)
			{
				List<Variant> arr = data["buddys"]._arr;
				this.RecommendDataList.Clear();
				foreach (Variant current in arr)
				{
					itemFriendData itemFriendData = default(itemFriendData);
					itemFriendData.cid = current["cid"]._uint;
					itemFriendData.name = current["name"]._str;
					itemFriendData.carr = current["carr"]._uint;
					itemFriendData.lvl = current["lvl"]._int;
					itemFriendData.zhuan = current["zhuan"]._uint;
					bool flag2 = current.ContainsKey("mlzd_diff");
					if (flag2)
					{
						itemFriendData.mlzd_lv = current["mlzd_diff"]._int;
					}
					debug.Log(itemFriendData.name);
					this.RecommendDataList.Add(itemFriendData);
				}
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_RECOMMEND, this, data, false));
			}
		}

		private void setRefuseAddFriend(Variant data)
		{
		}

		public void setBeRefuse(Variant data)
		{
			string str = data["name"]._str;
			flytxt.instance.fly(str + "拒绝了您的好友申请!", 0, default(Color), null);
		}

		public void setDeleteEnemy(Variant data)
		{
			default(itemFriendData).cid = data["cid"]._uint;
			bool flag = this.EnemyDataList.ContainsKey(data["cid"]._uint);
			if (flag)
			{
				this.EnemyDataList.Remove(data["cid"]._uint);
			}
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_DELETEENEMY, this, data, false));
		}

		public void setEnemyPostion(Variant data)
		{
			bool flag = this.EnemyDataList.ContainsKey(data["cid"]._uint);
			if (flag)
			{
				itemFriendData value = this.EnemyDataList[data["cid"]._uint];
				value.cid = data["cid"]._uint;
				value.map_id = (int)data["map_id"]._uint;
				value.llid = data["llid"]._uint;
				this.EnemyDataList[data["cid"]._uint] = value;
			}
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_ENEMYPOSTION, this, data, false));
		}

		public void setRemoveBlackList(Variant data)
		{
			bool flag = this.BlackDataList.ContainsKey(data["cid"]._uint);
			if (flag)
			{
				this.BlackDataList.Remove(data["cid"]._uint);
			}
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_DELETEBLACKLIST, this, data, false));
		}

		public void setReceiveEnemy(Variant data)
		{
			itemFriendData value = default(itemFriendData);
			value.cid = data["cid"]._uint;
			value.name = data["name"]._str;
			value.carr = data["carr"]._uint;
			value.lvl = data["lvl"]._int;
			value.zhuan = data["zhuan"]._uint;
			bool flag = data.ContainsKey("mlzd_diff");
			if (flag)
			{
				value.mlzd_lv = data["mlzd_diff"]._int;
			}
			value.clan_name = (string.IsNullOrEmpty(data["clan_name"]._str) ? "无" : data["clan_name"]._str);
			value.combpt = data["combpt"]._uint;
			bool flag2 = data.ContainsKey("map_id");
			if (flag2)
			{
				value.map_id = (int)data["map_id"]._uint;
			}
			else
			{
				value.map_id = -1;
			}
			bool flag3 = data.ContainsKey("llid");
			if (flag3)
			{
				value.llid = data["llid"]._uint;
			}
			value.hatred += 1u;
			bool flag4 = this.EnemyDataList.ContainsKey(data["cid"]._uint);
			if (flag4)
			{
				value.hatred += this.EnemyDataList[data["cid"]._uint].hatred;
			}
			this.EnemyDataList[data["cid"]._uint] = value;
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_ENEMYLIST, this, data, false));
		}

		public void sendfriendlist(FriendProxy.FriendType ft)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = (uint)ft;
			this.sendRPC(170u, variant);
		}

		public void sendAddFriend(uint cid = 0u, string name = "", bool isNormal = true)
		{
			bool flag = this.FriendDataList.ContainsKey(cid);
			if (flag)
			{
				flytxt.instance.fly(name + "已是您的好友。", 0, default(Color), null);
			}
			else
			{
				Variant variant = new Variant();
				variant["buddy_cmd"] = 2u;
				bool flag2 = cid > 0u;
				if (flag2)
				{
					variant["cid"] = cid;
				}
				bool flag3 = !string.IsNullOrEmpty(name);
				if (flag3)
				{
					variant["name"] = name;
				}
				bool flag4 = !this.requestFriendListNoAgree.Contains(name) & isNormal;
				if (flag4)
				{
					this.requestFriendListNoAgree.Add(name);
				}
				this.sendRPC(170u, variant);
			}
		}

		public void sendAddBlackList(uint cid = 0u, string name = "")
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 4u;
			bool flag = cid > 0u;
			if (flag)
			{
				variant["cid"] = cid;
			}
			bool flag2 = !string.IsNullOrEmpty(name);
			if (flag2)
			{
				variant["name"] = name;
			}
			this.sendRPC(170u, variant);
		}

		public void sendDeleteFriend(uint cid = 0u, string name = "")
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 6u;
			bool flag = cid > 0u;
			if (flag)
			{
				variant["cid"] = cid;
			}
			bool flag2 = !string.IsNullOrEmpty(name);
			if (flag2)
			{
				variant["name"] = name;
			}
			this.sendRPC(170u, variant);
		}

		public void sendAgreeApplyFriend(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 3u;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void sendOnlineRecommend()
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 13u;
			this.sendRPC(170u, variant);
		}

		public void sendRemoveBlackList(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 7u;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void sendRefuseAddFriend(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 5u;
			variant["cid"] = cid;
			variant["refuse"] = true;
			this.sendRPC(170u, variant);
		}

		public void sendEnemyPostion(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 9u;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void sendgetplayerinfo(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 11;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public void sendDeleteEnemy(uint cid)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 12u;
			variant["cid"] = cid;
			this.sendRPC(170u, variant);
		}

		public bool checkAddFriend(string name = "")
		{
			int count = this.FriendDataList.Count;
			bool flag = (long)count >= (long)((ulong)(ModelBase<PlayerModel>.getInstance().up_lvl * 50u + 50u));
			bool result;
			if (flag)
			{
				flytxt.instance.fly("好友列表已经满了,请删除一些好友再添加", 0, default(Color), null);
				result = true;
			}
			else
			{
				foreach (KeyValuePair<uint, itemFriendData> current in this.FriendDataList)
				{
					bool flag2 = current.Value.name.Equals(name);
					if (flag2)
					{
						flytxt.instance.fly(name + "已存在好友列表中.", 0, default(Color), null);
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		public void removeNearyByLeave(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_REMOVENEARYBY, this, variant, false));
		}

		public void addNearyByPeople(uint iid)
		{
			Variant variant = new Variant();
			variant["iid"] = iid;
			base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_ADDNEARYBY, this, variant, false));
		}

		public void reFreshProfessionInfo(ArrayList arry)
		{
			Variant variant = new Variant();
			variant["cid"] = (uint)arry[0];
			bool flag = arry.Count == 2;
			if (flag)
			{
				variant["combpt"] = ((arry[1] == null) ? 0 : ((int)arry[1]));
				base.dispatchEvent(GameEvent.Create(FriendProxy.EVENT_REFRESHNEARYBY, this, variant, false));
			}
		}
	}
}
