using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
	public class TabControl
	{
		public Action<TabControl> onClickHanle;

		private int _selectedIndex = -1;

		private List<Button> _tabs;

		public bool canClick = true;

		public int m_hideType = 0;

		public bool m_isClicked = false;

		private Dictionary<int, int> dUnable = new Dictionary<int, int>();

		public void create(GameObject tabs, GameObject main, int selectedIdx = 0, int hideType = 0, bool isClicked = false)
		{
			this.m_hideType = hideType;
			this.m_isClicked = isClicked;
			int childCount = tabs.transform.childCount;
			this._tabs = new List<Button>();
			for (int i = 0; i < childCount; i++)
			{
				this._tabs.Add(tabs.transform.GetChild(i).GetComponent<Button>());
				EventTriggerListener.Get(this._tabs[i].gameObject).onClick = new EventTriggerListener.VoidDelegate(this.onClick);
			}
			bool flag = selectedIdx >= 0;
			if (flag)
			{
				this.setSelectedIndex(selectedIdx, true);
			}
		}

		public void setSelectedIndex(int index, bool forceRunHandle = false)
		{
			bool flag = this._selectedIndex == index;
			if (flag)
			{
				bool flag2 = (forceRunHandle && this.onClickHanle != null) || (this.m_isClicked && this.onClickHanle != null);
				if (flag2)
				{
					this.onClickHanle(this);
				}
			}
			else
			{
				this.unSelectAll();
				Button button = this._tabs[index];
				bool flag3 = this.m_hideType == 1;
				if (flag3)
				{
					button.transform.localScale = Vector3.zero;
				}
				else
				{
					button.interactable = true;
				}
				this._selectedIndex = index;
				bool flag4 = this.onClickHanle != null;
				if (flag4)
				{
					this.onClickHanle(this);
				}
			}
		}

		public void setEnable(int idx, bool enable)
		{
			if (enable)
			{
				bool flag = this.dUnable.ContainsKey(idx);
				if (flag)
				{
					this.dUnable.Remove(idx);
				}
			}
			else
			{
				this.dUnable[idx] = 1;
			}
		}

		private void onClick(GameObject go)
		{
			bool flag = !this.canClick;
			if (!flag)
			{
				int num = this._tabs.IndexOf(go.GetComponent<Button>());
				bool flag2 = this.dUnable.ContainsKey(num);
				if (!flag2)
				{
					this.setSelectedIndex(num, false);
				}
			}
		}

		public void unSelectAll()
		{
			for (int i = 0; i < this._tabs.Count; i++)
			{
				bool flag = this.m_hideType == 1;
				if (flag)
				{
					this._tabs[i].transform.localScale = Vector3.one;
				}
				else
				{
					this._tabs[i].interactable = false;
				}
			}
		}

		public int getIndexByName(string name)
		{
			int result;
			for (int i = 0; i < this._tabs.Count; i++)
			{
				bool flag = this._tabs[i].gameObject.name == name;
				if (flag)
				{
					result = i;
					return result;
				}
			}
			result = 0;
			return result;
		}

		public int getSeletedIndex()
		{
			return this._selectedIndex;
		}

		public void dispose()
		{
			for (int i = 0; i < this._tabs.Count; i++)
			{
				EventTriggerListener.Get(this._tabs[i].gameObject).clearAllListener();
				this.onClickHanle = null;
			}
			this._tabs = null;
		}

		public void forEach(Action<int> fun)
		{
			bool flag = fun == null;
			if (!flag)
			{
				for (int i = 0; i < this._tabs.Count; i++)
				{
					fun(i);
				}
			}
		}
	}
}
