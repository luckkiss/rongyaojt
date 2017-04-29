using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_LHlianjie : Window
	{
		public static a3_LHlianjie instance;

		private Dictionary<int, GameObject> tipObj = new Dictionary<int, GameObject>();

		private RectTransform con;

		public override void init()
		{
			a3_LHlianjie.instance = this;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("close"), 1, 1);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onClose);
			baseButton2.onClick = new Action<GameObject>(this.onClose);
			this.con = base.transform.FindChild("view/con").GetComponent<RectTransform>();
			this.initLianjie();
		}

		public override void onShowed()
		{
			base.transform.SetAsLastSibling();
			this.setCon();
		}

		public override void onClosed()
		{
		}

		public void initLianjie()
		{
			GameObject gameObject = base.transform.FindChild("view/item").gameObject;
			bool flag = this.con.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.con.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.con.GetChild(i).gameObject);
				}
			}
			SXML sXML = XMLMgr.instance.GetSXML("activate_fun.activate_num", "");
			List<SXML> nodeList = sXML.GetNodeList("num", "");
			foreach (SXML current in nodeList)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				Text component = gameObject2.transform.FindChild("count").GetComponent<Text>();
				component.text = current.getInt("cout").ToString();
				List<SXML> nodeList2 = current.GetNodeList("type", "");
				GameObject gameObject3 = gameObject2.transform.FindChild("scrollview/info_item").gameObject;
				RectTransform component2 = gameObject2.transform.FindChild("scrollview/con").GetComponent<RectTransform>();
				foreach (SXML current2 in nodeList2)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
					Text component3 = gameObject4.transform.FindChild("Text").GetComponent<Text>();
					component3.text = Globle.getAttrNameById(current2.getInt("att_type")) + "+" + current2.getInt("att_value");
					gameObject4.SetActive(true);
					gameObject4.transform.SetParent(component2, false);
				}
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.con, false);
				this.tipObj[current.getInt("cout")] = gameObject2;
			}
			float x = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x;
			float x2 = this.con.GetComponent<GridLayoutGroup>().spacing.x;
			Vector2 sizeDelta = new Vector2((float)nodeList.Count * (x + x2), this.con.sizeDelta.x);
			this.con.sizeDelta = sizeDelta;
		}

		private void setCon()
		{
			int count = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count;
			float x = this.con.GetComponent<GridLayoutGroup>().cellSize.x;
			bool flag = count <= 1;
			if (flag)
			{
				this.con.anchoredPosition = new Vector2(0f, this.con.anchoredPosition.y);
			}
			else
			{
				bool flag2 = count >= 7;
				if (flag2)
				{
					this.con.anchoredPosition = new Vector2(-(6f * x), this.con.anchoredPosition.y);
				}
				else
				{
					this.con.anchoredPosition = new Vector2(-((float)(count - 1) * x), this.con.anchoredPosition.y);
				}
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_LHLIANJIE);
		}
	}
}
