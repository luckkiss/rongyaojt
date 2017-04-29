using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class choose_server : LoadingUI
	{
		private GameObject goTemp;

		private List<ServerItem> lserver;

		private Transform transCon;

		private List<AreaItem> lArea;

		private Button btClose;

		private float w;

		private float h;

		private AreaItem lastArea;

		public override void init()
		{
			this.lArea = new List<AreaItem>();
			this.goTemp = base.getGameObjectByPath("tempAreaBt");
			this.btClose = base.getComponentByPath<Button>("btclose");
			this.btClose.onClick.AddListener(new UnityAction(this.onClose));
			RectTransform component = this.goTemp.GetComponent<RectTransform>();
			this.w = component.sizeDelta.x;
			this.h = component.sizeDelta.y;
			this.goTemp.SetActive(false);
			this.transCon = base.transform.FindChild("info/con");
			this.lserver = new List<ServerItem>();
			for (int i = 0; i < 10; i++)
			{
				ServerItem serverItem = new ServerItem(base.transform.FindChild("s/s" + i), new Action<ServerData>(this.onServerClick));
				this.lserver.Add(serverItem);
				serverItem.visiable = false;
			}
			this.initList();
			this.changeArea(0);
		}

		public void initList()
		{
			this.lArea.Clear();
			for (int i = 0; i < this.transCon.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transCon.GetChild(i).gameObject);
			}
			List<ServerData> lServer = Globle.lServer;
			bool flag = lServer.Count % 10 == 0;
			int num;
			if (flag)
			{
				num = lServer.Count / 10;
			}
			else
			{
				num = lServer.Count / 10 + 1;
			}
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goTemp);
				gameObject.transform.SetParent(this.transCon, false);
				gameObject.SetActive(true);
				AreaItem item = new AreaItem(gameObject.transform, new Action<int>(this.onAreaClick), j);
				this.lArea.Add(item);
			}
			RectTransform component = this.transCon.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, (float)num * this.h);
		}

		public void changeArea(int idx)
		{
			bool flag = this.lastArea != null;
			if (flag)
			{
				this.lastArea.bt.interactable = true;
			}
			this.lastArea = this.lArea[idx];
			this.lastArea.bt.interactable = false;
			this.refeshServers(idx * 10);
		}

		public void refeshServers(int begin)
		{
			List<ServerData> lServer = Globle.lServer;
			bool flag = begin >= lServer.Count;
			if (!flag)
			{
				for (int i = 0; i < 10; i++)
				{
					bool flag2 = begin + i >= lServer.Count;
					if (flag2)
					{
						this.lserver[i].visiable = false;
					}
					else
					{
						this.lserver[i].setData(lServer[begin + i]);
					}
				}
			}
		}

		private void onClose()
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.SERVE_CHOOSE);
		}

		public void onAreaClick(int idx)
		{
			this.changeArea(idx);
		}

		public void onServerClick(ServerData d)
		{
			login.instance.setServer(d);
			InterfaceMgr.getInstance().close(InterfaceMgr.SERVE_CHOOSE);
		}

		public override void dispose()
		{
			this.btClose.onClick.RemoveAllListeners();
			this.btClose = null;
			this.transCon = null;
			this.goTemp = null;
			foreach (ServerItem current in this.lserver)
			{
				current.dispose();
			}
			this.lserver.Clear();
			foreach (AreaItem current2 in this.lArea)
			{
				current2.dispose();
			}
			this.lArea.Clear();
			base.dispose();
		}
	}
}
