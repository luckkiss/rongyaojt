using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class toggleGropFriendBase
	{
		public static friendList mFriendList;

		public static blackList mBlackList;

		public static enemyList mEnemyList;

		public static neighborList mNeighborList;

		private static toggleGropFriend mToggleGropFriend;

		public ToggleFriend togFriendType = ToggleFriend.Friend;

		private static actionPanelPrefab app = new actionPanelPrefab();

		private static actionNearybyPanelPrefab anpp = new actionNearybyPanelPrefab();

		private static recommendList mRecommendList;

		private static AddToBalckListWaring addToBalckListWaring;

		private static actionBlackListPanelPrefab actionBlackListPanel;

		private static actionEnemyPanel _actionEnemyPanel;

		public Transform root;

		public static Transform mTransform;

		public bool isActive
		{
			get
			{
				return this.root.gameObject.activeSelf;
			}
		}

		public static void init()
		{
			toggleGropFriendBase.mFriendList = new friendList();
			toggleGropFriendBase.mBlackList = new blackList();
			toggleGropFriendBase.mEnemyList = new enemyList();
			toggleGropFriendBase.mNeighborList = new neighborList();
			toggleGropFriendBase.mToggleGropFriend = new toggleGropFriend();
			toggleGropFriendBase.mRecommendList = new recommendList();
			toggleGropFriendBase.addToBalckListWaring = new AddToBalckListWaring();
			toggleGropFriendBase.actionBlackListPanel = new actionBlackListPanelPrefab();
			toggleGropFriendBase._actionEnemyPanel = new actionEnemyPanel();
			toggleGropFriendBase._actionEnemyPanel.init();
			toggleGropFriendBase.actionBlackListPanel.init();
			toggleGropFriendBase.mRecommendList.init();
			toggleGropFriendBase.mFriendList.init();
			toggleGropFriendBase.mBlackList.init();
			toggleGropFriendBase.mEnemyList.init();
			toggleGropFriendBase.mNeighborList.init();
			toggleGropFriendBase.mToggleGropFriend.init();
			toggleGropFriendBase.app.init();
			toggleGropFriendBase.anpp.init();
			toggleGropFriendBase.addToBalckListWaring.init();
		}

		public virtual void show()
		{
			bool flag = !this.root.gameObject.activeSelf;
			if (flag)
			{
				this.root.gameObject.SetActive(true);
			}
			float num = 0f;
			Dictionary<uint, itemFriendData> nearbyListDataDic = friendList._instance.NearbyListDataDic;
			foreach (KeyValuePair<uint, itemFriendData> current in nearbyListDataDic)
			{
				current.Value.root.gameObject.SetActive(false);
			}
			foreach (KeyValuePair<uint, ProfessionRole> current2 in OtherPlayerMgr._inst.m_mapOtherPlayerSee)
			{
				itemFriendData itemFriendData = default(itemFriendData);
				itemFriendData.cid = current2.Value.m_unCID;
				itemFriendData.name = current2.Value.roleName;
				itemFriendData.carr = (uint)current2.Value.m_roleDta.carr;
				itemFriendData.lvl = current2.Value.lvl;
				itemFriendData.zhuan = (uint)current2.Value.zhuan;
				itemFriendData.clan_name = (string.IsNullOrEmpty(current2.Value.clanName) ? "无" : current2.Value.clanName);
				itemFriendData.combpt = (uint)current2.Value.combpt;
				itemFriendData.map_id = (int)ModelBase<PlayerModel>.getInstance().mapid;
				bool flag2 = nearbyListDataDic.ContainsKey(current2.Value.m_unCID);
				if (flag2)
				{
					itemFriendData itemFriendData2 = nearbyListDataDic[current2.Value.m_unCID];
					itemFriendData2.itemNListPrefab.set(itemFriendData);
				}
				else
				{
					itemFriendData itemFriendData3 = itemFriendData;
					itemFriendData3.itemNListPrefab = new itemNearbyListPrefab();
					itemFriendData3.itemNListPrefab.init();
					Transform transform;
					itemFriendData3.itemNListPrefab.show(itemFriendData, out transform);
					itemFriendData3.root = transform;
					friendList._instance.NearbyListDataDic[current2.Value.m_unCID] = itemFriendData3;
					bool flag3 = num == 0f;
					if (flag3)
					{
						num = itemFriendData3.itemNListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
					}
				}
			}
			neighborList._instance.nearbyListCount.text = OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
			neighborList._instance.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (num + 10f) * (float)OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count);
		}

		public virtual void close()
		{
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}
	}
}
