using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_beRequestFriend : Window
	{
		private class itemBeRequestFriend
		{
			public Transform root;

			private Text txtName;

			private Text txtZhuan;

			private uint cid = 0u;

			private string mName = string.Empty;

			private GameObject _go;

			public itemBeRequestFriend(Transform trans, itemFriendData data)
			{
				this._go = UnityEngine.Object.Instantiate<GameObject>(trans.gameObject);
				this.root = this._go.transform;
				this.txtName = this._go.transform.FindChild("txtName").GetComponent<Text>();
				this.txtZhuan = this._go.transform.FindChild("txtZhuan").GetComponent<Text>();
				BaseButton baseButton = new BaseButton(this._go.transform.FindChild("btnRefuse"), 1, 1);
				baseButton.onClick = new Action<GameObject>(this.onBtnRefuse);
				BaseButton baseButton2 = new BaseButton(this._go.transform.FindChild("btnAgreen"), 1, 1);
				baseButton2.onClick = new Action<GameObject>(this.onBtnAgreen);
				this.cid = data.cid;
				string name = data.name;
				this.mName = name;
				string arg = data.zhuan.ToString();
				string arg2 = data.lvl.ToString();
				this.txtName.text = name;
				this.txtZhuan.text = string.Format(this.txtZhuan.text, arg, arg2);
			}

			private void onBtnRefuse(GameObject go)
			{
				BaseProxy<FriendProxy>.getInstance().sendRefuseAddFriend(this.cid);
				UnityEngine.Object.Destroy(a3_beRequestFriend.mInstance.beRequestFriendList[this.cid].root.gameObject);
				a3_beRequestFriend.mInstance.beRequestFriendList.Remove(this.cid);
			}

			private void onBtnAgreen(GameObject go)
			{
				BaseProxy<FriendProxy>.getInstance().sendAgreeApplyFriend(this.cid);
				bool flag = BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Contains(this.mName);
				if (flag)
				{
					BaseProxy<FriendProxy>.getInstance().requestFriendListNoAgree.Remove(this.mName);
				}
				UnityEngine.Object.Destroy(a3_beRequestFriend.mInstance.beRequestFriendList[this.cid].root.gameObject);
				a3_beRequestFriend.mInstance.beRequestFriendList.Remove(this.cid);
			}
		}

		public static a3_beRequestFriend mInstance;

		private GameObject itemPrefab;

		private Dictionary<uint, a3_beRequestFriend.itemBeRequestFriend> beRequestFriendList;

		private RectTransform contentParent;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public override void init()
		{
			this.beRequestFriendList = new Dictionary<uint, a3_beRequestFriend.itemBeRequestFriend>();
			a3_beRequestFriend.mInstance = this;
			this.contentParent = base.transform.FindChild("body/main/content").GetComponent<RectTransform>();
			this.itemPrefab = base.transform.FindChild("prefabs/itemBeRequestFirend").gameObject;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("body/title/btnClose"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("body/bottom/btnRefuse"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnRefuseClick);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("body/bottom/btnAgreen"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtnAgreenClick);
		}

		public override void onClosed()
		{
			foreach (KeyValuePair<uint, a3_beRequestFriend.itemBeRequestFriend> current in this.beRequestFriendList)
			{
				UnityEngine.Object.Destroy(this.beRequestFriendList[current.Key].root.gameObject);
			}
			this.beRequestFriendList.Clear();
			BaseProxy<FriendProxy>.getInstance().requestFirendList.Clear();
		}

		private void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BEREQUESTFRIEND);
		}

		private void onBtnRefuseClick(GameObject go)
		{
			Dictionary<uint, itemFriendData> requestFirendList = BaseProxy<FriendProxy>.getInstance().requestFirendList;
			foreach (KeyValuePair<uint, itemFriendData> current in requestFirendList)
			{
				BaseProxy<FriendProxy>.getInstance().sendRefuseAddFriend(current.Key);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BEREQUESTFRIEND);
		}

		private void onBtnAgreenClick(GameObject go)
		{
			Dictionary<uint, itemFriendData> requestFirendList = BaseProxy<FriendProxy>.getInstance().requestFirendList;
			foreach (KeyValuePair<uint, itemFriendData> current in requestFirendList)
			{
				BaseProxy<FriendProxy>.getInstance().sendAgreeApplyFriend(current.Key);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BEREQUESTFRIEND);
		}

		public override void onShowed()
		{
			Dictionary<uint, itemFriendData> requestFirendList = BaseProxy<FriendProxy>.getInstance().requestFirendList;
			foreach (KeyValuePair<uint, itemFriendData> current in requestFirendList)
			{
				a3_beRequestFriend.itemBeRequestFriend itemBeRequestFriend = new a3_beRequestFriend.itemBeRequestFriend(this.itemPrefab.transform, requestFirendList[current.Key]);
				itemBeRequestFriend.root.SetParent(this.contentParent.transform);
				itemBeRequestFriend.root.localScale = Vector3.one;
				this.beRequestFriendList.Add(requestFirendList[current.Key].cid, itemBeRequestFriend);
			}
			this.contentParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(70 * requestFirendList.Count));
		}
	}
}
