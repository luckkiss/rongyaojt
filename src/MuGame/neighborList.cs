using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class neighborList : toggleGropFriendBase
	{
		public static neighborList _instance;

		public Transform containt;

		public Text nearbyListCount;

		public RectTransform contains;

		public new void init()
		{
			neighborList._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/neighborPanel");
			this.containt = this.root.FindChild("right/main/body/scroll/contains");
			this.contains = this.containt.GetComponent<RectTransform>();
			this.nearbyListCount = this.root.FindChild("right/main/body/nearbyListCount/count").GetComponent<Text>();
			BaseButton baseButton = new BaseButton(this.root.FindChild("right/bottom/btnRefresh"), 1, 1);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("right/bottom/btnAddFriend"), 1, 1);
			BaseButton baseButton3 = new BaseButton(this.root.FindChild("right/bottom/btnTeam"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnRefresh);
			baseButton2.onClick = new Action<GameObject>(this.onBtnAddFriend);
			baseButton3.onClick = new Action<GameObject>(this.onBtnTeam);
		}

		private void onBtnTeam(GameObject obj)
		{
		}

		private void onBtnAddFriend(GameObject obj)
		{
			Dictionary<uint, itemFriendData> nearbyListDataDic = friendList._instance.NearbyListDataDic;
			foreach (KeyValuePair<uint, itemFriendData> current in nearbyListDataDic)
			{
				Toggle component = current.Value.root.FindChild("Toggle").GetComponent<Toggle>();
				bool isOn = component.isOn;
				if (isOn)
				{
					bool flag = BaseProxy<FriendProxy>.getInstance().checkAddFriend(current.Value.name);
					bool flag2 = !flag;
					if (flag2)
					{
						BaseProxy<FriendProxy>.getInstance().sendAddFriend(current.Key, current.Value.name, true);
					}
				}
			}
		}

		private void onBtnRefresh(GameObject obj)
		{
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
				bool flag = nearbyListDataDic.ContainsKey(current2.Value.m_unCID);
				if (flag)
				{
					itemFriendData itemFriendData2 = nearbyListDataDic[current2.Value.m_unCID];
					itemFriendData2.itemNListPrefab.set(itemFriendData);
				}
				else
				{
					itemFriendData itemFriendData3 = itemFriendData;
					itemFriendData3.itemNListPrefab = new itemNearbyListPrefab();
					itemFriendData3.itemNListPrefab.init();
					Transform root;
					itemFriendData3.itemNListPrefab.show(itemFriendData, out root);
					itemFriendData3.root = root;
					friendList._instance.NearbyListDataDic[current2.Value.m_unCID] = itemFriendData3;
					bool flag2 = num == 0f;
					if (flag2)
					{
						num = itemFriendData3.itemNListPrefab.root.transform.FindChild("Toggle/Background").GetComponent<RectTransform>().sizeDelta.y;
					}
				}
				neighborList._instance.nearbyListCount.text = OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count.ToString() + "/" + (50u + 50u * ModelBase<PlayerModel>.getInstance().up_lvl);
				neighborList._instance.contains.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (num + 10f) * (float)OtherPlayerMgr._inst.m_mapOtherPlayerSee.Count);
			}
		}
	}
}
