using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class itemNearbyListPrefab : toggleGropFriendBase
	{
		public static itemNearbyListPrefab instance;

		public new Transform root;

		public Toggle toggle;

		public Text txtNickName;

		public Text txtLvl;

		public Text txtTeam;

		public Text txtCombat;

		public Text txtPos;

		public uint cid;

		public string name;

		private Transform actionPanelPos;

		private BaseButton btnAction;

		public bool watch_avt;

		public new void init()
		{
			itemNearbyListPrefab.instance = this;
			Transform transform = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/itemNearbyFirend");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
			this.root = gameObject.transform;
			this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			this.txtNickName = this.toggle.transform.FindChild("containts/txtName").GetComponent<Text>();
			this.txtLvl = this.toggle.transform.FindChild("containts/txtLevel").GetComponent<Text>();
			this.txtTeam = this.toggle.transform.FindChild("containts/txtTeam").GetComponent<Text>();
			this.txtCombat = this.toggle.transform.FindChild("containts/txtcombat").GetComponent<Text>();
			this.txtPos = this.toggle.transform.FindChild("containts/txtpos").GetComponent<Text>();
			this.btnAction = new BaseButton(this.root.FindChild("btnAction"), 1, 1);
			this.actionPanelPos = this.root.FindChild("btnAction/actionPanelPos");
			this.btnAction.onClick = new Action<GameObject>(this.onBtnAction);
			gameObject.SetActive(true);
			this.root.SetParent(neighborList._instance.containt, false);
			actionNearybyPanelPrefab._instance.btnChat.onClick = new Action<GameObject>(this.onBtnChat);
			actionNearybyPanelPrefab._instance.btnWatch.onClick = new Action<GameObject>(this.onBtnWatch);
			actionNearybyPanelPrefab._instance.btnTeam.onClick = new Action<GameObject>(this.onBtnTeam);
			actionNearybyPanelPrefab._instance.btnAdd.onClick = new Action<GameObject>(this.onBtnAdd);
			actionNearybyPanelPrefab._instance.btnBlackList.onClick = new Action<GameObject>(this.onBtnBalckList);
			actionNearybyPanelPrefab._instance.btnCloseBg.onClick = new Action<GameObject>(this.onBtnCloseBg);
		}

		public void show(itemFriendData data, out Transform rootT)
		{
			this.txtNickName.text = data.name;
			this.name = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtTeam.text = data.clan_name;
			this.txtCombat.text = data.combpt.ToString();
			this.txtPos.text = friendList._instance.getMapNameById(data.map_id);
			this.cid = data.cid;
			data.root = this.root;
			rootT = this.root;
		}

		public void set(itemFriendData data)
		{
			this.root.gameObject.SetActive(true);
			this.txtNickName.text = data.name;
			this.name = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtTeam.text = data.clan_name;
			this.txtCombat.text = data.combpt.ToString();
			this.txtPos.text = friendList._instance.getMapNameById(data.map_id);
			this.cid = data.cid;
			data.root = this.root;
		}

		private void onBtnAction(GameObject go)
		{
			actionNearybyPanelPrefab._instance.cid = this.cid;
			actionNearybyPanelPrefab._instance.name = this.name;
			bool flag = !actionNearybyPanelPrefab._instance.root.gameObject.activeSelf;
			if (flag)
			{
				actionNearybyPanelPrefab._instance.root.position = this.actionPanelPos.position;
				actionNearybyPanelPrefab._instance.root.gameObject.SetActive(true);
			}
			else
			{
				bool flag2 = actionNearybyPanelPrefab._instance.root.position.y != this.actionPanelPos.position.y;
				if (flag2)
				{
					actionNearybyPanelPrefab._instance.root.position = this.actionPanelPos.position;
				}
				else
				{
					actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
				}
			}
		}

		private void onBtnChat(GameObject go)
		{
			uint num = actionNearybyPanelPrefab._instance.cid;
			string text = actionNearybyPanelPrefab._instance.name;
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
			a3_chatroom._instance.privateChat(text);
		}

		private void onBtnWatch(GameObject go)
		{
			this.watch_avt = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
			uint num = actionNearybyPanelPrefab._instance.cid;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(num);
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
		}

		private void onBtnTeam(GameObject go)
		{
			uint num = actionNearybyPanelPrefab._instance.cid;
			BaseProxy<TeamProxy>.getInstance().SendInvite(num);
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnAdd(GameObject go)
		{
			uint num = actionNearybyPanelPrefab._instance.cid;
			string text = actionNearybyPanelPrefab._instance.name;
			BaseProxy<FriendProxy>.getInstance().sendAddFriend(num, text, true);
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnBalckList(GameObject go)
		{
			uint key = actionNearybyPanelPrefab._instance.cid;
			bool flag = friendList._instance.BlackListDataDic.ContainsKey(key);
			if (flag)
			{
				string str = friendList._instance.BlackListDataDic[key].name;
				flytxt.instance.fly(str + "已存在黑名单中.", 0, default(Color), null);
			}
			else
			{
				BaseProxy<FriendProxy>.getInstance().sendAddBlackList(key, "");
			}
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnCloseBg(GameObject go)
		{
			actionNearybyPanelPrefab._instance.root.gameObject.SetActive(false);
		}
	}
}
