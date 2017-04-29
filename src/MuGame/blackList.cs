using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class blackList : toggleGropFriendBase
	{
		public static blackList _instance;

		public Transform containt;

		public Text blackListCount;

		public RectTransform contains;

		private Transform addBlackListPanel;

		private InputField txtAddBlackListName;

		public new void init()
		{
			blackList._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/blackListPanel");
			this.containt = this.root.FindChild("right/main/body/scroll/contains");
			this.contains = this.containt.GetComponent<RectTransform>();
			this.blackListCount = this.root.FindChild("right/main/body/blackListCount/count").GetComponent<Text>();
			BaseButton baseButton = new BaseButton(this.root.FindChild("right/bottom/btnAddBlackList"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnAddBlackListClick);
			BaseButton baseButton2 = new BaseButton(this.root.FindChild("hidPanels/addBlackListPanel/bottom/btnAdd"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnAddToBlackListClick);
			BaseButton baseButton3 = new BaseButton(this.root.FindChild("hidPanels/addBlackListPanel/bottom/btnCancle"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtnCancleClick);
			BaseButton baseButton4 = new BaseButton(this.root.FindChild("hidPanels/addBlackListPanel/title/btnClose"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onBtnCancleClick);
			this.addBlackListPanel = this.root.FindChild("hidPanels/addBlackListPanel");
			this.txtAddBlackListName = this.root.FindChild("hidPanels/addBlackListPanel/main/InputField").GetComponent<InputField>();
		}

		private void onBtnAddBlackListClick(GameObject go)
		{
			bool flag = !this.addBlackListPanel.gameObject.activeSelf;
			if (flag)
			{
				this.addBlackListPanel.gameObject.SetActive(true);
			}
		}

		private void onBtnAddToBlackListClick(GameObject go)
		{
			string text = this.txtAddBlackListName.text;
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				foreach (KeyValuePair<uint, itemFriendData> current in BaseProxy<FriendProxy>.getInstance().FriendDataList)
				{
					bool flag2 = current.Value.name.Equals(text);
					if (flag2)
					{
						AddToBalckListWaring._instance.Show(current.Value, new Action(this.closeAddBlackListPanel));
						break;
					}
				}
			}
			else
			{
				flytxt.instance.fly("请输入玩家名称.", 0, default(Color), null);
			}
		}

		private void onBtnCancleClick(GameObject go)
		{
			bool activeSelf = this.addBlackListPanel.gameObject.activeSelf;
			if (activeSelf)
			{
				this.addBlackListPanel.gameObject.SetActive(false);
			}
		}

		private void closeAddBlackListPanel()
		{
			bool activeSelf = this.addBlackListPanel.gameObject.activeSelf;
			if (activeSelf)
			{
				this.addBlackListPanel.gameObject.SetActive(false);
			}
		}
	}
}
