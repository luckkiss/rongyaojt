using System;
using UnityEngine;

namespace MuGame
{
	internal class actionBlackListPanelPrefab : toggleGropFriendBase
	{
		public static actionBlackListPanelPrefab _instance;

		private uint cid;

		private string name;

		private uint zhuan;

		private int lvl;

		public new void init()
		{
			actionBlackListPanelPrefab._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/actionBlackListPanel");
			BaseButton baseButton = new BaseButton(this.root.FindChild("buttons/btnAddFriend"), 1, 1);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("buttons/btnCancleBlackList"), 1, 1);
			BaseButton baseButton3 = new BaseButton(this.root.FindChild("btnCloseBg"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onAddFriendClick);
			baseButton2.onClick = new Action<GameObject>(this.onBtnCancleBlackListClick);
			baseButton3.onClick = new Action<GameObject>(this.onBtnCloseBgClick);
		}

		public void Show(itemFriendData ifd)
		{
			this.cid = ifd.cid;
			this.name = ifd.name;
			this.zhuan = ifd.zhuan;
			this.lvl = ifd.lvl;
			bool flag = !this.root.gameObject.activeSelf;
			if (flag)
			{
				this.root.gameObject.SetActive(true);
			}
		}

		private void onAddFriendClick(GameObject go)
		{
			BaseProxy<FriendProxy>.getInstance().sendAddFriend(this.cid, this.name, true);
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}

		private void onBtnCancleBlackListClick(GameObject go)
		{
			BaseProxy<FriendProxy>.getInstance().sendRemoveBlackList(this.cid);
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}

		private void onBtnCloseBgClick(GameObject go)
		{
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}
	}
}
