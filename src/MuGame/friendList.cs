using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class friendList : toggleGropFriendBase
	{
		public static friendList _instance;

		public Transform containt;

		public Transform addFriend;

		private Transform addFriendsPanel;

		private Toggle showOnlineFriends;

		private InputField iptfAFPName;

		private Transform personalsPanel;

		public BaseButton btnAdd;

		private Text friendLimit;

		public RectTransform contains;

		public Dictionary<uint, itemFriendData> friendDataDic = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, itemFriendData> BlackListDataDic = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, itemFriendData> EnemyListDataDic = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, itemFriendData> NearbyListDataDic = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, itemFriendData> RecommendListDataDic = new Dictionary<uint, itemFriendData>();

		public Dictionary<uint, Time> cdPostionTimer = new Dictionary<uint, Time>();

		public new void init()
		{
			friendList._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/myFriendsPanel");
			BaseButton baseButton = new BaseButton(this.root.FindChild("right/bottom/btnClickOnceAddFriend"), 1, 1);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("right/bottom/btnAddFriend"), 1, 1);
			this.showOnlineFriends = this.root.FindChild("right/bottom/Toggle").GetComponent<Toggle>();
			this.showOnlineFriends.onValueChanged.AddListener(new UnityAction<bool>(this.onShowOnlineFriendsChanged));
			this.personalsPanel = this.root.FindChild("hidPanels/personalsPanel");
			BaseButton baseButton3 = new BaseButton(this.personalsPanel.FindChild("bottom/btnSelectAll"), 1, 1);
			this.btnAdd = new BaseButton(this.personalsPanel.FindChild("bottom/btnAdd"), 1, 1);
			BaseButton baseButton4 = new BaseButton(this.personalsPanel.FindChild("title/btnClose"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onPersonalPanelClose);
			this.btnAdd.onClick = new Action<GameObject>(this.onAddAllSelectClick);
			baseButton3.onClick = new Action<GameObject>(this.onSelectAllClick);
			this.addFriendsPanel = this.root.FindChild("hidPanels/addFriendsPanel");
			BaseButton baseButton5 = new BaseButton(this.addFriendsPanel.transform.FindChild("bottom/btnAdd"), 1, 1);
			this.iptfAFPName = this.addFriendsPanel.transform.FindChild("main/InputField").GetComponent<InputField>();
			BaseButton baseButton6 = new BaseButton(this.addFriendsPanel.transform.FindChild("bottom/btnCancel"), 1, 1);
			BaseButton baseButton7 = new BaseButton(this.addFriendsPanel.transform.FindChild("title/btnClose"), 1, 1);
			this.friendLimit = this.root.FindChild("right/main/body/firendsCount/count").GetComponent<Text>();
			this.containt = this.root.FindChild("right/main/body/scroll/contains");
			this.contains = this.containt.GetComponent<RectTransform>();
			this.addFriend = this.root.FindChild("hidPanels/personalsPanel/main/scroll/containts");
			baseButton.onClick = new Action<GameObject>(this.onBtnClickOnceAddFriendClick);
			baseButton2.onClick = new Action<GameObject>(this.onBtnAddFriendClick);
			baseButton5.onClick = new Action<GameObject>(this.onBtnAdd);
			baseButton6.onClick = new Action<GameObject>(this.onBtnAFPClose);
			baseButton7.onClick = new Action<GameObject>(this.onBtnAFPClose);
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_FRIENDLIST, new Action<GameEvent>(this.onFriendList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_BLACKLIST, new Action<GameEvent>(this.onBlackList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_ENEMYLIST, new Action<GameEvent>(this.onEnemyList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_RECOMMEND, new Action<GameEvent>(this.onRecommendList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_DELETEFRIEND, new Action<GameEvent>(this.onDeleteFriend));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_RECEIVEADDBLACKLIST, new Action<GameEvent>(this.onReceiveAddBlackList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_AGREEAPLYRFRIEND, new Action<GameEvent>(this.onAgreeAplyFriend));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_DELETEENEMY, new Action<GameEvent>(this.onDeleteEnemy));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_ENEMYPOSTION, new Action<GameEvent>(this.onEnemyPostion));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_DELETEBLACKLIST, new Action<GameEvent>(this.onDeleteBlackList));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_REMOVENEARYBY, new Action<GameEvent>(this.onRemoveNearybyLeave));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_ADDNEARYBY, new Action<GameEvent>(this.onAddNearbyPeople));
			BaseProxy<FriendProxy>.getInstance().addEventListener(FriendProxy.EVENT_REFRESHNEARYBY, new Action<GameEvent>(this.onFreshNearby));
		}

		public void onShow()
		{
			BaseProxy<FriendProxy>.getInstance().sendfriendlist(FriendProxy.FriendType.FRIEND);
			Dictionary<uint, itemFriendData> friendDataList = BaseProxy<FriendProxy>.getInstance().FriendDataList;
			this.friendLimit.text = friendDataList.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
		}

		private void onBtnClickOnceAddFriendClick(GameObject go)
		{
			bool flag = !this.personalsPanel.gameObject.activeSelf;
			if (flag)
			{
				this.personalsPanel.gameObject.SetActive(true);
			}
			bool flag2 = this.personalsPanel.transform.FindChild("main/scroll/containts").childCount > 0;
			if (flag2)
			{
				for (int i = 0; i < this.personalsPanel.transform.FindChild("main/scroll/containts").childCount; i++)
				{
					UnityEngine.Object.Destroy(this.personalsPanel.transform.FindChild("main/scroll/containts").GetChild(i).gameObject);
				}
			}
			BaseProxy<FriendProxy>.getInstance().sendOnlineRecommend();
		}

		private void onBtnAddFriendClick(GameObject go)
		{
			bool flag = !this.addFriendsPanel.gameObject.activeSelf;
			if (flag)
			{
				this.addFriendsPanel.gameObject.SetActive(true);
			}
		}

		private void onBtnAdd(GameObject go)
		{
			string text = this.iptfAFPName.text;
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				bool flag2 = BaseProxy<FriendProxy>.getInstance().checkAddFriend(text);
				bool flag3 = !flag2;
				if (flag3)
				{
					BaseProxy<FriendProxy>.getInstance().sendAddFriend(0u, text, true);
				}
				this.addFriendsPanel.gameObject.SetActive(false);
			}
		}

		private void onBtnAFPClose(GameObject go)
		{
			this.addFriendsPanel.gameObject.SetActive(false);
		}

		private void onPersonalPanelClose(GameObject go)
		{
			this.personalsPanel.gameObject.SetActive(false);
		}

		public void onShowOnlineFriendsChanged(bool b)
		{
			bool isOn = this.showOnlineFriends.isOn;
			if (isOn)
			{
				foreach (KeyValuePair<uint, itemFriendData> current in this.friendDataDic)
				{
					bool flag = !current.Value.online;
					if (flag)
					{
						current.Value.root.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				foreach (KeyValuePair<uint, itemFriendData> current2 in this.friendDataDic)
				{
					bool flag2 = !current2.Value.online;
					if (flag2)
					{
						current2.Value.root.gameObject.SetActive(true);
						current2.Value.root.SetAsLastSibling();
					}
				}
			}
		}

		private void onSelectAllClick(GameObject go)
		{
			foreach (KeyValuePair<uint, itemFriendData> current in this.RecommendListDataDic)
			{
				current.Value.root.FindChild("Toggle").GetComponent<Toggle>().isOn = true;
			}
		}

		private void onAddAllSelectClick(GameObject go)
		{
			this.personalsPanel.gameObject.SetActive(false);
			Dictionary<uint, itemFriendData> dictionary = new Dictionary<uint, itemFriendData>();
			dictionary.Clear();
			foreach (KeyValuePair<uint, itemFriendData> current in this.RecommendListDataDic)
			{
				bool isOn = current.Value.root.FindChild("Toggle").GetComponent<Toggle>().isOn;
				if (isOn)
				{
					dictionary[current.Key] = current.Value;
				}
			}
			foreach (KeyValuePair<uint, itemFriendData> current2 in dictionary)
			{
				bool flag = BaseProxy<FriendProxy>.getInstance().checkAddFriend(current2.Value.name);
				bool flag2 = !flag;
				if (flag2)
				{
					BaseProxy<FriendProxy>.getInstance().sendAddFriend(current2.Key, current2.Value.name, true);
				}
				bool flag3 = friendList._instance.friendDataDic.ContainsKey(current2.Key);
				if (!flag3)
				{
					bool flag4 = !BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Contains(current2.Value.name);
					if (flag4)
					{
						BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Add(current2.Value.name);
					}
				}
			}
		}

		private void onFriendList(GameEvent e)
		{
			float num = 60f;
			Dictionary<uint, itemFriendData> friendDataList = BaseProxy<FriendProxy>.getInstance().FriendDataList;
			foreach (KeyValuePair<uint, itemFriendData> current in friendDataList)
			{
				bool flag = this.friendDataDic.ContainsKey(current.Key);
				if (flag)
				{
					itemFriendData itemFriendData = this.friendDataDic[current.Key];
					itemFriendData.itemFPrefab.set(current.Value);
				}
				else
				{
					itemFriendData value = current.Value;
					value.itemFPrefab = new itemFriendPrefab();
					value.itemFPrefab.init();
					Transform root;
					value.itemFPrefab.show(current.Value, out root);
					value.root = root;
					this.friendDataDic[current.Key] = value;
					bool flag2 = num == 0f;
					if (flag2)
					{
						num = value.itemFPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
					}
				}
			}
			this.onShowOnlineFriendsChanged(true);
			this.friendLimit.text = friendDataList.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			this.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num * (float)friendDataList.Count);
		}

		private void onBlackList(GameEvent e)
		{
			float num = 60f;
			Dictionary<uint, itemFriendData> blackDataList = BaseProxy<FriendProxy>.getInstance().BlackDataList;
			foreach (KeyValuePair<uint, itemFriendData> current in blackDataList)
			{
				bool flag = this.BlackListDataDic.ContainsKey(current.Key);
				if (flag)
				{
					itemFriendData itemFriendData = this.BlackListDataDic[current.Key];
					itemFriendData.itemBListPrefab.set(current.Value);
				}
				else
				{
					itemFriendData value = current.Value;
					value.itemBListPrefab = new itemBlackListPrefab();
					value.itemBListPrefab.init();
					Transform root;
					value.itemBListPrefab.show(current.Value, out root);
					value.root = root;
					this.BlackListDataDic[current.Key] = value;
					bool flag2 = num == 0f;
					if (flag2)
					{
						num = value.itemBListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
					}
				}
			}
			blackList._instance.blackListCount.text = blackDataList.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			blackList._instance.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num * (float)blackDataList.Count);
		}

		private void onEnemyList(GameEvent e)
		{
			float num = 60f;
			Dictionary<uint, itemFriendData> enemyDataList = BaseProxy<FriendProxy>.getInstance().EnemyDataList;
			foreach (KeyValuePair<uint, itemFriendData> current in enemyDataList)
			{
				bool flag = this.EnemyListDataDic.ContainsKey(current.Key);
				if (flag)
				{
					itemFriendData itemFriendData = this.EnemyListDataDic[current.Key];
					bool isCdNow = itemFriendData.itemEListPrefab.isCdNow;
					if (isCdNow)
					{
						BaseProxy<FriendProxy>.getInstance().sendEnemyPostion(current.Key);
					}
					else
					{
						itemFriendData.itemEListPrefab.set(current.Value);
					}
				}
				else
				{
					itemFriendData value = current.Value;
					value.itemEListPrefab = new itemEnemyListPrefab();
					value.itemEListPrefab.init();
					Transform root;
					value.itemEListPrefab.show(current.Value, out root);
					value.root = root;
					this.EnemyListDataDic[current.Key] = value;
					bool flag2 = num == 0f;
					if (flag2)
					{
						num = value.itemEListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
					}
				}
			}
			enemyList._instance.enemyListCount.text = enemyDataList.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			enemyList._instance.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num * (float)enemyDataList.Count);
		}

		private void onRecommendList(GameEvent e)
		{
			int num = 0;
			this.RecommendListDataDic.Clear();
			float num2 = 60f;
			List<itemFriendData> recommendDataList = BaseProxy<FriendProxy>.getInstance().RecommendDataList;
			recommendDataList.Sort(new Comparison<itemFriendData>(this.SortItemFriendData));
			foreach (itemFriendData current in recommendDataList)
			{
				num++;
				itemFriendData itemFriendData = current;
				itemFriendData.itemECListPrefab = new itemRecommendListPrefab();
				itemFriendData.itemECListPrefab.init();
				Transform root;
				itemFriendData.itemECListPrefab.show(current, out root);
				itemFriendData.root = root;
				this.RecommendListDataDic[current.cid] = itemFriendData;
				bool flag = num % 2 == 0;
				if (flag)
				{
					itemFriendData.itemECListPrefab.root.transform.FindChild("bg2").gameObject.SetActive(false);
				}
				bool flag2 = num2 == 0f;
				if (flag2)
				{
					num2 = itemFriendData.itemECListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
				}
			}
			this.btnAdd.transform.GetComponent<Button>().interactable = true;
			RectTransform component = recommendList._instance.containt.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, num2 * (float)this.RecommendListDataDic.Count);
		}

		private void onAgreeAplyFriend(GameEvent e)
		{
			Dictionary<uint, itemFriendData> friendDataList = BaseProxy<FriendProxy>.getInstance().FriendDataList;
			foreach (KeyValuePair<uint, itemFriendData> current in friendDataList)
			{
				bool flag = this.friendDataDic.ContainsKey(current.Key);
				if (flag)
				{
					itemFriendData itemFriendData = this.friendDataDic[current.Key];
					itemFriendData.itemFPrefab.set(current.Value);
				}
				else
				{
					itemFriendData value = current.Value;
					value.itemFPrefab = new itemFriendPrefab();
					value.itemFPrefab.init();
					Transform root;
					value.itemFPrefab.show(current.Value, out root);
					value.root = root;
					this.friendDataDic[current.Key] = value;
				}
				bool flag2 = this.EnemyListDataDic.ContainsKey(current.Key);
				if (flag2)
				{
					this.EnemyListDataDic[current.Key].itemEListPrefab.UpdatePos(current.Value.map_id);
				}
			}
			this.onShowOnlineFriendsChanged(true);
			this.friendLimit.text = friendDataList.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			this.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(60 * friendDataList.Count));
		}

		private void onDeleteEnemy(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = friendList._instance.EnemyListDataDic.ContainsKey(@uint);
			if (flag)
			{
				UnityEngine.Object.Destroy(friendList._instance.EnemyListDataDic[@uint].root.gameObject);
				friendList._instance.EnemyListDataDic.Remove(@uint);
				enemyList._instance.enemyListCount.text = friendList._instance.EnemyListDataDic.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			}
		}

		private void onEnemyPostion(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = friendList._instance.EnemyListDataDic.ContainsKey(@uint);
			if (flag)
			{
				string mapNameById = this.getMapNameById(BaseProxy<FriendProxy>.getInstance().EnemyDataList[@uint].map_id);
				friendList._instance.EnemyListDataDic[@uint].root.FindChild("Toggle/containts/txtpos").GetComponent<Text>().text = mapNameById;
				itemFriendData itemFriendData = friendList._instance.EnemyListDataDic[@uint];
				itemFriendData.timer = 300f;
				friendList._instance.EnemyListDataDic[@uint] = itemFriendData;
				bool isCdNow = friendList._instance.EnemyListDataDic[@uint].itemEListPrefab.isCdNow;
				if (isCdNow)
				{
					friendList._instance.EnemyListDataDic[@uint].root.gameObject.SetActive(true);
				}
				else
				{
					itemFriendData.itemEListPrefab.refresShowPostion(300);
				}
			}
		}

		private void onDeleteBlackList(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = friendList._instance.BlackListDataDic.ContainsKey(@uint);
			if (flag)
			{
				UnityEngine.Object.Destroy(friendList._instance.BlackListDataDic[@uint].root.gameObject);
				friendList._instance.BlackListDataDic.Remove(@uint);
				blackList._instance.blackListCount.text = friendList._instance.BlackListDataDic.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			}
		}

		private void onRemoveNearybyLeave(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = this.NearbyListDataDic.ContainsKey(@uint);
			if (flag)
			{
				this.NearbyListDataDic[@uint].itemNListPrefab.root.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(this.NearbyListDataDic[@uint].itemNListPrefab.root.gameObject);
				this.NearbyListDataDic.Remove(@uint);
			}
		}

		private void onAddNearbyPeople(GameEvent e)
		{
			float num = 60f;
			uint @uint = e.data["iid"]._uint;
			Dictionary<uint, ProfessionRole> mapOtherPlayerSee = OtherPlayerMgr._inst.m_mapOtherPlayerSee;
			bool flag = mapOtherPlayerSee.ContainsKey(@uint);
			if (flag)
			{
				uint unCID = mapOtherPlayerSee[@uint].m_unCID;
			}
			ProfessionRole professionRole = mapOtherPlayerSee[@uint];
			itemFriendData itemFriendData = default(itemFriendData);
			itemFriendData.cid = professionRole.m_unCID;
			itemFriendData.name = professionRole.roleName;
			itemFriendData.carr = (uint)professionRole.m_roleDta.carr;
			itemFriendData.lvl = professionRole.lvl;
			itemFriendData.zhuan = (uint)professionRole.zhuan;
			itemFriendData.clan_name = (string.IsNullOrEmpty(professionRole.clanName) ? "无" : professionRole.clanName);
			itemFriendData.combpt = (uint)professionRole.combpt;
			itemFriendData.map_id = (int)ModelBase<PlayerModel>.getInstance().mapid;
			bool flag2 = this.NearbyListDataDic.ContainsKey(professionRole.m_unCID);
			if (flag2)
			{
				itemFriendData itemFriendData2 = this.NearbyListDataDic[professionRole.m_unCID];
				itemFriendData2.itemNListPrefab.set(itemFriendData);
				bool flag3 = num == 0f;
				if (flag3)
				{
					num = itemFriendData2.itemNListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
				}
			}
			else
			{
				itemFriendData itemFriendData3 = itemFriendData;
				itemFriendData3.itemNListPrefab = new itemNearbyListPrefab();
				itemFriendData3.itemNListPrefab.init();
				Transform root;
				itemFriendData3.itemNListPrefab.show(itemFriendData, out root);
				itemFriendData3.root = root;
				this.NearbyListDataDic[professionRole.m_unCID] = itemFriendData3;
				bool flag4 = num == 0f;
				if (flag4)
				{
					num = itemFriendData3.itemNListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
				}
				neighborList._instance.nearbyListCount.text = OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
				neighborList._instance.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num * (float)OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count);
			}
		}

		private void onFreshNearby(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			uint uint2 = e.data["combpt"]._uint;
			bool flag = this.NearbyListDataDic.ContainsKey(@uint);
			if (flag)
			{
				itemFriendData itemFriendData = this.NearbyListDataDic[@uint];
				itemFriendData.combpt = uint2;
				itemFriendData.itemNListPrefab.set(itemFriendData);
			}
		}

		private void onDeleteFriend(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = friendList._instance.friendDataDic.ContainsKey(@uint);
			if (flag)
			{
				UnityEngine.Object.Destroy(friendList._instance.friendDataDic[@uint].root.gameObject);
				bool flag2 = BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Contains(friendList._instance.friendDataDic[@uint].root.name);
				if (flag2)
				{
					BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Remove(friendList._instance.friendDataDic[@uint].root.name);
				}
				friendList._instance.friendDataDic.Remove(@uint);
				this.friendLimit.text = friendList._instance.friendDataDic.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
				this.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(60 * friendList._instance.friendDataDic.Count));
			}
			bool flag3 = friendList._instance.EnemyListDataDic.ContainsKey(@uint);
			if (flag3)
			{
				friendList._instance.EnemyListDataDic[@uint].itemEListPrefab.UpdatePos(-1);
			}
		}

		private void onReceiveAddBlackList(GameEvent e)
		{
			uint @uint = e.data["cid"]._uint;
			bool flag = friendList._instance.friendDataDic.ContainsKey(@uint);
			if (flag)
			{
				UnityEngine.Object.Destroy(friendList._instance.friendDataDic[@uint].root.gameObject);
				friendList._instance.friendDataDic.Remove(@uint);
			}
		}

		private int SortItemFriendData(itemFriendData a1, itemFriendData a2)
		{
			bool flag = a1.online.CompareTo(a2.online) != 0;
			int result;
			if (flag)
			{
				result = -a1.online.CompareTo(a2.online);
			}
			else
			{
				bool flag2 = a1.zhuan.CompareTo(a2.zhuan) != 0;
				if (flag2)
				{
					result = -a1.zhuan.CompareTo(a2.zhuan);
				}
				else
				{
					bool flag3 = a1.lvl.CompareTo(a2.lvl) != 0;
					if (flag3)
					{
						result = -a1.lvl.CompareTo(a2.lvl);
					}
					else
					{
						bool flag4 = a1.combpt.CompareTo(a2.combpt) != 0;
						if (flag4)
						{
							result = -a1.combpt.CompareTo(a2.combpt);
						}
						else
						{
							result = 1;
						}
					}
				}
			}
			return result;
		}

		public string getMapNameById(int id)
		{
			bool flag = id == -1;
			string result;
			if (flag)
			{
				result = "离线";
			}
			else
			{
				bool flag2 = SvrMapConfig.instance.getSingleMapConf((uint)id) == null;
				if (flag2)
				{
					result = "未知";
				}
				else
				{
					result = SvrMapConfig.instance.getSingleMapConf((uint)id)["map_name"]._str;
				}
			}
			return result;
		}

		public void hidActionPanel()
		{
			bool activeSelf = actionPanelPrefab._instance.root.gameObject.activeSelf;
			if (activeSelf)
			{
				actionPanelPrefab._instance.root.gameObject.SetActive(false);
			}
			bool activeSelf2 = actionNearybyPanelPrefab._instance.root.gameObject.activeSelf;
			if (activeSelf2)
			{
				actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
			}
		}
	}
}
