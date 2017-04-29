using System;
using UnityEngine;

namespace MuGame
{
	internal class actionEnemyPanel : toggleGropFriendBase
	{
		public static actionEnemyPanel _instance;

		private uint cid;

		private string name;

		private uint zhuan;

		private int lvl;

		public new void init()
		{
			actionEnemyPanel._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/actionEnemyPanel");
			BaseButton baseButton = new BaseButton(this.root.FindChild("buttons/btnDelEnemy"), 1, 1);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("buttons/btnAdd"), 1, 1);
			BaseButton baseButton3 = new BaseButton(this.root.FindChild("buttons/btnBlackList"), 1, 1);
			BaseButton baseButton4 = new BaseButton(this.root.FindChild("btnCloseBg"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnDelEnemyClick);
			baseButton2.onClick = new Action<GameObject>(this.onBtnAddClick);
			baseButton3.onClick = new Action<GameObject>(this.onBtnBlackListClick);
			baseButton4.onClick = new Action<GameObject>(this.onBtnCloseBgClick);
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

		private void onBtnDelEnemyClick(GameObject go)
		{
			enemyList._instance.deleteEnemyPanel.gameObject.SetActive(true);
			enemyList._instance.txtDInfo.text = string.Format("  此操作将清除{0}对你的击杀记录,是否确定删除?", this.name);
			enemyList._instance.cid = this.cid;
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}

		private void onBtnAddClick(GameObject go)
		{
			bool flag = BaseProxy<FriendProxy>.getInstance().checkAddFriend(this.name);
			bool flag2 = !flag;
			if (flag2)
			{
				BaseProxy<FriendProxy>.getInstance().sendAddFriend(this.cid, this.name, true);
			}
			bool activeSelf = this.root.gameObject.activeSelf;
			if (activeSelf)
			{
				this.root.gameObject.SetActive(false);
			}
		}

		private void onBtnBlackListClick(GameObject go)
		{
			bool flag = friendList._instance.BlackListDataDic.ContainsKey(this.cid);
			if (flag)
			{
				flytxt.instance.fly(this.name + "已存在黑名单中.", 0, default(Color), null);
			}
			else
			{
				BaseProxy<FriendProxy>.getInstance().sendAddBlackList(this.cid, this.name);
			}
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
