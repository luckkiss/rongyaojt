using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LScrollPageMark : MonoBehaviour
{
	public LScrollPage scrollPage;

	public ToggleGroup toggleGroup;

	public Toggle togglePrefab;

	public List<Toggle> toggleList = new List<Toggle>();

	private void Awake()
	{
		this.scrollPage.OnPageChanged = new Action<int, int>(this.OnScrollPageChanged);
	}

	public void OnScrollPageChanged(int pageCount, int currentPageIndex)
	{
		bool flag = pageCount != this.toggleList.Count;
		if (flag)
		{
			bool flag2 = pageCount > this.toggleList.Count;
			if (flag2)
			{
				int num = pageCount - this.toggleList.Count;
				for (int i = 0; i < num; i++)
				{
					this.toggleList.Add(this.CreateToggle());
				}
			}
			else
			{
				bool flag3 = pageCount < this.toggleList.Count;
				if (flag3)
				{
					while (this.toggleList.Count > pageCount)
					{
						Toggle toggle = this.toggleList[this.toggleList.Count - 1];
						this.toggleList.Remove(toggle);
						UnityEngine.Object.DestroyImmediate(toggle.gameObject);
					}
				}
			}
		}
		bool flag4 = currentPageIndex >= 0;
		if (flag4)
		{
			this.toggleList[currentPageIndex].isOn = true;
		}
	}

	private Toggle CreateToggle()
	{
		Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.togglePrefab);
		toggle.gameObject.SetActive(true);
		toggle.transform.SetParent(this.toggleGroup.transform);
		toggle.transform.localScale = Vector3.one;
		toggle.transform.localPosition = Vector3.zero;
		return toggle;
	}

	private void OnDestroy()
	{
		this.scrollPage.OnPageChanged = null;
	}
}
