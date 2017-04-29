using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_QHmaster : Window
	{
		public static a3_QHmaster instance;

		public override void init()
		{
			a3_QHmaster.instance = this;
			this.refreshDashi();
			BaseButton baseButton = new BaseButton(base.transform.FindChild("close"), 1, 1);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onCloseDashi);
			baseButton2.onClick = new Action<GameObject>(this.onCloseDashi);
		}

		public override void onShowed()
		{
			base.transform.SetAsLastSibling();
		}

		public override void onClosed()
		{
		}

		public void refreshDashi()
		{
			GameObject gameObject = base.transform.FindChild("view/item").gameObject;
			RectTransform component = base.transform.FindChild("view/con").GetComponent<RectTransform>();
			bool flag = component.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < component.childCount; i++)
				{
					UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				}
			}
			SXML sXML = XMLMgr.instance.GetSXML("intensifymaster.level", "lvl==" + ModelBase<PlayerModel>.getInstance().up_lvl);
			List<SXML> nodeList = sXML.GetNodeList("intensify", "");
			foreach (SXML current in nodeList)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				Text component2 = gameObject2.transform.FindChild("qh_lvl/Text").GetComponent<Text>();
				component2.text = "+" + current.getInt("qh");
				List<SXML> nodeList2 = current.GetNodeList("att", "");
				GameObject gameObject3 = gameObject2.transform.FindChild("scrollview/info_item").gameObject;
				RectTransform component3 = gameObject2.transform.FindChild("scrollview/con").GetComponent<RectTransform>();
				foreach (SXML current2 in nodeList2)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
					Text component4 = gameObject4.transform.FindChild("Text").GetComponent<Text>();
					component4.text = "+" + current2.getInt("value") + Globle.getAttrNameById(current2.getInt("type"));
					gameObject4.SetActive(true);
					gameObject4.transform.SetParent(component3, false);
				}
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(component, false);
			}
			float x = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x;
			float x2 = component.GetComponent<GridLayoutGroup>().spacing.x;
			Vector2 sizeDelta = new Vector2((float)nodeList.Count * (x + x2), component.sizeDelta.x);
			component.sizeDelta = sizeDelta;
		}

		private void onCloseDashi(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_QHMASTER);
		}
	}
}
