using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class enemyList : toggleGropFriendBase
	{
		public static enemyList _instance;

		public Transform containt;

		public Text enemyListCount;

		public RectTransform contains;

		private Transform searchEnemyPanel;

		private Text txtInifo;

		public Transform deleteEnemyPanel;

		public Text txtDInfo;

		public uint cid;

		private Dictionary<uint, itemFriendData> enemyDicTemp = new Dictionary<uint, itemFriendData>();

		private Dictionary<uint, itemFriendData> enemyDeleteDicTemp = new Dictionary<uint, itemFriendData>();

		public new void init()
		{
			enemyList._instance = this;
			this.root = toggleGropFriendBase.mTransform.FindChild("mainBody/enemyPanel");
			this.containt = this.root.FindChild("right/main/body/scroll/contains");
			this.contains = this.containt.GetComponent<RectTransform>();
			this.enemyListCount = this.root.FindChild("right/main/body/enemyListCount/count").GetComponent<Text>();
			BaseButton baseButton = new BaseButton(this.root.FindChild("right/bottom/btnSearchPos"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnSearchPos);
			this.searchEnemyPanel = this.root.FindChild("hidPanels/searchEnemyPanel");
			this.txtInifo = this.searchEnemyPanel.FindChild("main/Text").GetComponent<Text>();
			BaseButton baseButton2 = new BaseButton(this.searchEnemyPanel.FindChild("title/btnClose"), 1, 1);
			BaseButton baseButton3 = new BaseButton(this.searchEnemyPanel.FindChild("bottom/btnCancel"), 1, 1);
			BaseButton baseButton4 = new BaseButton(this.searchEnemyPanel.FindChild("bottom/btnSearch"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnSEPClose);
			baseButton3.onClick = new Action<GameObject>(this.onBtnSEPClose);
			baseButton4.onClick = new Action<GameObject>(this.onBtnSEPSearch);
			this.deleteEnemyPanel = this.root.FindChild("hidPanels/deleteEnemyPanel");
			this.txtDInfo = this.deleteEnemyPanel.FindChild("main/Text").GetComponent<Text>();
			BaseButton baseButton5 = new BaseButton(this.deleteEnemyPanel.FindChild("title/btnClose"), 1, 1);
			BaseButton baseButton6 = new BaseButton(this.deleteEnemyPanel.FindChild("bottom/btnCancel"), 1, 1);
			BaseButton baseButton7 = new BaseButton(this.deleteEnemyPanel.FindChild("bottom/btnOK"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onBtnDEPClose);
			baseButton6.onClick = new Action<GameObject>(this.onBtnDEPClose);
			baseButton7.onClick = new Action<GameObject>(this.onBtnDEPOk);
		}

		private void onBtnSearchPos(GameObject obj)
		{
			this.enemyDicTemp.Clear();
			Dictionary<uint, itemFriendData> enemyListDataDic = friendList._instance.EnemyListDataDic;
			foreach (KeyValuePair<uint, itemFriendData> current in enemyListDataDic)
			{
				Toggle component = current.Value.root.FindChild("Toggle").GetComponent<Toggle>();
				bool isOn = component.isOn;
				if (isOn)
				{
					this.enemyDicTemp.Add(current.Key, current.Value);
				}
			}
			bool flag = this.enemyDicTemp.Count > 1;
			if (flag)
			{
				flytxt.instance.fly("每次只能选择一个敌人进行查询!", 0, default(Color), null);
			}
			bool flag2 = this.enemyDicTemp.Count == 0;
			if (flag2)
			{
				flytxt.instance.fly("请选择一个敌人进行查询!", 0, default(Color), null);
			}
			bool flag3 = this.enemyDicTemp.Count == 1;
			if (flag3)
			{
				foreach (KeyValuePair<uint, itemFriendData> current2 in this.enemyDicTemp)
				{
					this.txtInifo.text = string.Format(this.txtInifo.text, current2.Value.name);
					this.searchEnemyPanel.gameObject.SetActive(true);
				}
			}
		}

		private void onBtnSEPClose(GameObject obj)
		{
			this.searchEnemyPanel.gameObject.SetActive(false);
		}

		private void onBtnSEPSearch(GameObject obj)
		{
			bool flag = this.enemyDicTemp.Count != 1;
			if (!flag)
			{
				foreach (KeyValuePair<uint, itemFriendData> current in this.enemyDicTemp)
				{
					BaseProxy<FriendProxy>.getInstance().sendEnemyPostion(current.Key);
					this.searchEnemyPanel.gameObject.SetActive(false);
				}
			}
		}

		private void onBtnDEPClose(GameObject go)
		{
			this.deleteEnemyPanel.gameObject.SetActive(false);
		}

		private void onBtnDEPOk(GameObject go)
		{
			this.deleteEnemyPanel.gameObject.SetActive(false);
			BaseProxy<FriendProxy>.getInstance().sendDeleteEnemy(this.cid);
		}
	}
}
