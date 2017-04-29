using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class itemFriendPrefab : toggleGropFriendBase
	{
		public static itemFriendPrefab instance;

		public new Transform root;

		public Toggle toggle;

		public Text txtNickName;

		public Text txtLvl;

		public Text txtTeam;

		public Text txtCombat;

		public Text txtPos;

		public BaseButton btnAction;

		private uint cid;

		private string name;

		private uint zhuan;

		private int lvl;

		public bool watch_avt;

		private Transform actionPanelPos;

		public new void init()
		{
			itemFriendPrefab.instance = this;
			Transform transform = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/itemFriend");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
			this.root = gameObject.transform;
			this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			this.txtNickName = this.toggle.transform.FindChild("containts/txtName").GetComponent<Text>();
			this.txtLvl = this.toggle.transform.FindChild("containts/txtLevel").GetComponent<Text>();
			this.txtTeam = this.toggle.transform.FindChild("containts/txtTeam").GetComponent<Text>();
			this.txtCombat = this.toggle.transform.FindChild("containts/txtcombat").GetComponent<Text>();
			this.txtPos = this.toggle.transform.FindChild("containts/txtpos").GetComponent<Text>();
			this.actionPanelPos = this.root.FindChild("btnAction/actionPanelPos");
			this.btnAction = new BaseButton(this.root.transform.FindChild("btnAction"), 1, 1);
			gameObject.SetActive(true);
			this.root.SetParent(friendList._instance.containt, false);
		}

		public void show(itemFriendData data, out Transform rootT)
		{
			this.txtNickName.text = data.name;
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
			this.name = data.name;
			this.zhuan = data.zhuan;
			this.lvl = data.lvl;
			data.root = this.root;
			rootT = this.root;
			this.btnAction.onClick = new Action<GameObject>(this.onBtnActionClick);
			actionPanelPrefab._instance.btnBlackList.onClick = new Action<GameObject>(this.onBtnBlackList);
			actionPanelPrefab._instance.btnDelete.onClick = new Action<GameObject>(this.onBtnDelete);
			actionPanelPrefab._instance.btnChat.onClick = new Action<GameObject>(this.onBtnChat);
			actionPanelPrefab._instance.btnWatch.onClick = new Action<GameObject>(this.onBtnWatch);
			actionPanelPrefab._instance.btnTeam.onClick = new Action<GameObject>(this.onBtnTeamClick);
			actionPanelPrefab._instance.btnCloseBg.onClick = new Action<GameObject>(this.onBtnCloseBg);
		}

		public void set(itemFriendData data)
		{
			this.root.gameObject.SetActive(true);
			this.txtNickName.text = data.name;
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
			this.name = data.name;
			this.zhuan = data.zhuan;
			this.lvl = data.lvl;
			data.root = this.root;
		}

		private void onBtnActionClick(GameObject go)
		{
			actionPanelPrefab._instance.cid = this.cid;
			actionPanelPrefab._instance.name = this.name;
			actionPanelPrefab._instance.zhuan = this.zhuan;
			actionPanelPrefab._instance.lvl = this.lvl;
			bool flag = !actionPanelPrefab._instance.root.gameObject.activeSelf;
			if (flag)
			{
				actionPanelPrefab._instance.root.position = this.actionPanelPos.position;
				actionPanelPrefab._instance.root.gameObject.SetActive(true);
			}
			else
			{
				bool flag2 = actionPanelPrefab._instance.root.position.y != this.actionPanelPos.position.y;
				if (flag2)
				{
					actionPanelPrefab._instance.root.position = this.actionPanelPos.position;
				}
				else
				{
					actionPanelPrefab._instance.root.gameObject.SetActive(false);
				}
			}
		}

		private void onBtnBlackList(GameObject go)
		{
			itemFriendData itemData = default(itemFriendData);
			uint num = actionPanelPrefab._instance.cid;
			string text = actionPanelPrefab._instance.name;
			uint num2 = actionPanelPrefab._instance.zhuan;
			int num3 = actionPanelPrefab._instance.lvl;
			itemData.cid = num;
			itemData.name = text;
			itemData.zhuan = num2;
			itemData.lvl = num3;
			AddToBalckListWaring._instance.Show(itemData, null);
			BaseProxy<FriendProxy>.getInstance().sendAddBlackList(num, "");
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnDelete(GameObject go)
		{
			uint num = actionPanelPrefab._instance.cid;
			BaseProxy<FriendProxy>.getInstance().sendDeleteFriend(num, "");
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnChat(GameObject go)
		{
			uint num = actionPanelPrefab._instance.cid;
			string text = actionPanelPrefab._instance.name;
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
			a3_chatroom._instance.privateChat(text);
		}

		private void onBtnWatch(GameObject go)
		{
			this.watch_avt = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
			uint num = actionPanelPrefab._instance.cid;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(num);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnTeamClick(GameObject go)
		{
			uint num = actionPanelPrefab._instance.cid;
			BaseProxy<TeamProxy>.getInstance().SendInvite(num);
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
		}

		private void onBtnCloseBg(GameObject go)
		{
			actionPanelPrefab._instance.root.gameObject.SetActive(false);
		}
	}
}
