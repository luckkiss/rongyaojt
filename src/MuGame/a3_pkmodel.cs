using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_pkmodel : Window
	{
		private GameObject[] objs = new GameObject[5];

		public static a3_pkmodel _instance;

		private ScrollControler scrollControer;

		public override void init()
		{
			this.scrollControer = new ScrollControler();
			ScrollRect component = base.transform.FindChild("scrollRect").GetComponent<ScrollRect>();
			this.scrollControer.create(component, 4);
			for (int i = 0; i < 3; i++)
			{
				this.objs[i] = base.transform.FindChild("scrollRect/contain/grid" + i).gameObject;
				BaseButton baseButton = new BaseButton(this.objs[i].transform.FindChild("bg/" + i), 1, 1);
				baseButton.onClick = new Action<GameObject>(this.onBtnClicks);
			}
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("closeBtn"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.close);
		}

		public override void onShowed()
		{
			a3_pkmodel._instance = this;
			this.ShowThisImage(ModelBase<PlayerModel>.getInstance().now_pkState);
		}

		public override void onClosed()
		{
			base.onClosed();
		}

		private void onBtnClicks(GameObject go)
		{
			BaseProxy<a3_PkmodelProxy>.getInstance().sendProxy(int.Parse(go.name));
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_PKMODEL);
			NewbieModel.getInstance().hide();
		}

		public void ShowThisImage(int state)
		{
			for (int i = 0; i < 3; i++)
			{
				bool flag = state == i;
				if (flag)
				{
					this.objs[i].transform.FindChild("bg/this").gameObject.SetActive(true);
				}
				else
				{
					this.objs[i].transform.FindChild("bg/this").gameObject.SetActive(false);
				}
			}
		}

		private void close(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_PKMODEL);
		}
	}
}
