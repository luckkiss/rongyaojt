using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class DropItemUIMgr
	{
		private int showTime = 5;

		private static DropItemUIMgr instance;

		private List<DropItemUI> lPool;

		private Dictionary<INameObj, DropItemUI> dItem;

		private List<DropItemUI> lItem;

		private Transform dropItemUILayer;

		private TickItem process;

		public static DropItemUIMgr getInstance()
		{
			bool flag = DropItemUIMgr.instance == null;
			if (flag)
			{
				DropItemUIMgr.instance = new DropItemUIMgr();
			}
			return DropItemUIMgr.instance;
		}

		public DropItemUIMgr()
		{
			this.lPool = new List<DropItemUI>();
			this.dItem = new Dictionary<INameObj, DropItemUI>();
			this.lItem = new List<DropItemUI>();
			this.dropItemUILayer = GameObject.Find("dropItemLayer").transform;
			this.process = new TickItem(new Action<float>(this.onUpdate));
			TickMgr.instance.addTick(this.process);
		}

		public void show(INameObj dropObj, string name)
		{
			bool flag = !this.dItem.ContainsKey(dropObj);
			if (flag)
			{
				bool flag2 = this.lPool.Count == 0;
				DropItemUI dropItemUI;
				if (flag2)
				{
					GameObject original = Resources.Load("prefab/dropItemUI") as GameObject;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(this.dropItemUILayer, false);
					dropItemUI = new DropItemUI(gameObject.transform);
				}
				else
				{
					dropItemUI = this.lPool[0];
					dropItemUI.visiable = true;
					this.lPool.RemoveAt(0);
				}
				dropItemUI.refresh(dropObj, name);
				this.lItem.Add(dropItemUI);
				this.dItem[dropObj] = dropItemUI;
				bool flag3 = dropObj is DropItem;
				if (flag3)
				{
					dropItemUI.refresShowName(this.showTime);
				}
			}
			else
			{
				this.dItem[dropObj].refresh(dropObj, name);
				this.dItem[dropObj].refresShowName(this.showTime);
			}
		}

		public void hideAll()
		{
			this.dropItemUILayer.gameObject.SetActive(false);
		}

		public void showAll()
		{
			this.dropItemUILayer.gameObject.SetActive(true);
		}

		public void hideOne(INameObj dropItem)
		{
			bool flag = !this.dItem.ContainsKey(dropItem);
			if (!flag)
			{
				DropItemUI dropItemUI = this.dItem[dropItem];
				dropItemUI.clear();
			}
		}

		public void removeDropItem(DropItemUI diu)
		{
			this.lItem.Remove(diu);
		}

		public void hide(INameObj dropItem)
		{
			bool flag = !this.dItem.ContainsKey(dropItem);
			if (!flag)
			{
				DropItemUI dropItemUI = this.dItem[dropItem];
				dropItemUI.visiable = false;
				dropItemUI.clear();
				this.dItem.Remove(dropItem);
				this.lItem.Remove(dropItemUI);
				this.lPool.Add(dropItemUI);
			}
		}

		private void onUpdate(float s)
		{
			bool flag = this.lItem.Count > 0;
			if (flag)
			{
				foreach (DropItemUI current in this.lItem)
				{
					bool flag2 = current.gameObject == null;
					if (flag2)
					{
						this.lItem.Remove(current);
					}
				}
				foreach (DropItemUI current2 in this.lItem)
				{
					current2.update();
				}
			}
		}
	}
}
