using System;
using UnityEngine;

namespace GameFramework
{
	public class TabControler
	{
		private int _selectedIndex = 0;

		private GameObject[] _tabs;

		private GameObject[] _selTabs;

		private GameObject[] _panels;

		public void create(GameObject tabs, GameObject main)
		{
			int num = tabs.transform.childCount / 2;
			this._tabs = new GameObject[num];
			this._selTabs = new GameObject[num];
			this._panels = new GameObject[num];
			for (int i = 0; i < num; i++)
			{
				this._tabs[i] = tabs.transform.GetChild(i * 2).gameObject;
				this._selTabs[i] = tabs.transform.GetChild(i * 2 + 1).gameObject;
				this._panels[i] = main.transform.FindChild(tabs.transform.GetChild(i * 2).name.Replace("btn_", "panel_")).gameObject;
				this._tabs[i].SetActive(true);
				this._selTabs[i].SetActive(false);
				this._panels[i].SetActive(false);
			}
			bool flag = this._tabs[0] && this._panels[0];
			if (flag)
			{
				this._tabs[0].SetActive(false);
				this._selTabs[0].SetActive(true);
				this._panels[0].SetActive(true);
			}
		}

		public void setSelectedIndex(int index)
		{
			bool flag = this._selectedIndex == index;
			if (!flag)
			{
				bool flag2 = index != -1;
				if (flag2)
				{
					this._tabs[this._selectedIndex].SetActive(true);
					this._selTabs[this._selectedIndex].SetActive(false);
					bool flag3 = this._panels[this._selectedIndex];
					if (flag3)
					{
						this._panels[this._selectedIndex].SetActive(false);
					}
					this._selectedIndex = index;
					this._tabs[this._selectedIndex].SetActive(false);
					this._selTabs[this._selectedIndex].SetActive(true);
					bool flag4 = this._panels[this._selectedIndex];
					if (flag4)
					{
						this._panels[this._selectedIndex].SetActive(true);
					}
				}
			}
		}

		public int getIndexByName(string name)
		{
			int result;
			for (int i = 0; i < this._tabs.Length; i++)
			{
				bool flag = this._tabs[i].name == name;
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
	}
}
