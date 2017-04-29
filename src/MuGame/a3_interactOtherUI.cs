using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_interactOtherUI : FloatUi
	{
		private string playerName;

		private uint cid;

		private Text txt_name;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.transform.FindChild("bg"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCloseBgClick);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("btnClose"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnCloseBgClick);
			this.txt_name = base.transform.FindChild("txt_name").GetComponent<Text>();
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("buttons/btn_see"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtnSeePlayerInfo);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("buttons/btn_addFriend"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onBtnAddFriendClick);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("buttons/btn_pinvite"), 1, 1);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("buttons/btn_privateChat"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onPrivateChatClick);
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				this.txt_name.text = this.uiData[0].ToString();
				this.playerName = this.uiData[0].ToString();
				this.cid = (uint)this.uiData[1];
				bool flag2 = !base.transform.gameObject.activeSelf;
				if (flag2)
				{
					base.transform.gameObject.SetActive(true);
				}
			}
		}

		private void onBtnCloseBgClick(GameObject go)
		{
			bool activeSelf = base.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				base.transform.gameObject.SetActive(false);
			}
		}

		private void onBtnSeePlayerInfo(GameObject go)
		{
			bool activeSelf = base.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				base.transform.gameObject.SetActive(false);
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.cid);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
		}

		private void onBtnAddFriendClick(GameObject go)
		{
			bool activeSelf = base.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				base.transform.gameObject.SetActive(false);
			}
			BaseProxy<FriendProxy>.getInstance().sendAddFriend(this.cid, this.playerName, true);
		}

		private void onPrivateChatClick(GameObject go)
		{
			bool activeSelf = base.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				base.transform.gameObject.SetActive(false);
			}
			a3_chatroom._instance.privateChat(this.playerName);
		}
	}
}
