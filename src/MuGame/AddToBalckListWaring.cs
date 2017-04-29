using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class AddToBalckListWaring : toggleGropFriendBase
	{
		public static AddToBalckListWaring _instance;

		public new Transform root;

		private Text m_txtInfo;

		public uint cid;

		private Action _ac = null;

		public new void init()
		{
			AddToBalckListWaring._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/HidePanel/AddToBalckListWaring");
			BaseButton baseButton = new BaseButton(this.root.FindChild("title/btnClose"), 1, 1);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("btnOK"), 1, 1);
			BaseButton baseButton3 = new BaseButton(this.root.FindChild("btnCancel"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
			baseButton3.onClick = new Action<GameObject>(this.onBtnCloseClick);
			baseButton2.onClick = new Action<GameObject>(this.onBtnOkClick);
			this.m_txtInfo = this.root.FindChild("body/Text").GetComponent<Text>();
		}

		public void Show(itemFriendData itemData, Action ac = null)
		{
			bool flag = !this.root.gameObject.activeSelf;
			if (flag)
			{
				this.root.gameObject.SetActive(true);
			}
			this.m_txtInfo.text = string.Format(this.m_txtInfo.text, itemData.name, itemData.zhuan, itemData.lvl);
			this.cid = itemData.cid;
			bool flag2 = ac != null;
			if (flag2)
			{
				this._ac = ac;
			}
		}

		private void onBtnCloseClick(GameObject go)
		{
			this.root.gameObject.SetActive(false);
		}

		private void onBtnOkClick(GameObject go)
		{
			BaseProxy<FriendProxy>.getInstance().sendAddBlackList(this.cid, "");
			this.root.gameObject.SetActive(false);
			bool flag = this._ac != null;
			if (flag)
			{
				this._ac();
				this._ac = null;
			}
		}
	}
}
