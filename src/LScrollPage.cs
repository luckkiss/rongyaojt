using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LScrollPage : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
{
	private ScrollRect rect;

	private List<float> pages = new List<float>();

	private int currentPageIndex = -1;

	public float smooting = 4f;

	private float targethorizontal = 0f;

	private bool isDrag = false;

	public Action<int, int> OnPageChanged;

	private float startime = 0f;

	private float delay = 0.1f;

	public int CurrentPageIndex
	{
		get
		{
			return this.currentPageIndex;
		}
	}

	private void Start()
	{
		this.rect = base.transform.GetComponent<ScrollRect>();
		this.startime = Time.time;
	}

	private void OnEnable()
	{
		this.pages.Clear();
		this.currentPageIndex = -1;
	}

	private void Update()
	{
		this.UpdatePages();
		bool flag = this.pages.Count > 0;
		if (flag)
		{
			this.rect.horizontalNormalizedPosition = Mathf.Lerp(this.rect.horizontalNormalizedPosition, this.targethorizontal, Time.deltaTime * this.smooting);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		float horizontalNormalizedPosition = this.rect.horizontalNormalizedPosition;
		int num = 0;
		bool flag = this.pages.Count <= 0;
		if (!flag)
		{
			float num2 = Mathf.Abs(this.pages[num] - horizontalNormalizedPosition);
			for (int i = 1; i < this.pages.Count; i++)
			{
				float num3 = Mathf.Abs(this.pages[i] - horizontalNormalizedPosition);
				bool flag2 = num3 < num2;
				if (flag2)
				{
					num = i;
					num2 = num3;
				}
			}
			bool flag3 = num != this.currentPageIndex;
			if (flag3)
			{
				this.currentPageIndex = num;
			}
			this.targethorizontal = this.pages[num];
		}
	}

	public void OnHardDrag(bool isLeftOrRight)
	{
		if (isLeftOrRight)
		{
			bool flag = this.currentPageIndex > 0;
			if (flag)
			{
				this.currentPageIndex--;
			}
		}
		else
		{
			bool flag2 = this.currentPageIndex < this.pages.Count - 1;
			if (flag2)
			{
				this.currentPageIndex++;
			}
		}
		this.targethorizontal = this.pages[this.currentPageIndex];
	}

	private void UpdatePages()
	{
		int num = this.rect.content.childCount;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			bool activeSelf = this.rect.content.GetChild(i).gameObject.activeSelf;
			if (activeSelf)
			{
				num2++;
			}
		}
		num = num2;
		bool flag = this.pages.Count != num;
		if (flag)
		{
			bool flag2 = num != 0;
			if (flag2)
			{
				this.pages.Clear();
				for (int j = 0; j < num; j++)
				{
					float item = 0f;
					bool flag3 = num != 1;
					if (flag3)
					{
						item = (float)j / (float)(num - 1);
					}
					this.pages.Add(item);
				}
			}
			this.OnEndDrag(null);
		}
		a3_chatroom._instance.textItemCurrentPage.text = string.Format("{0}/{1}", this.currentPageIndex + 1, this.pages.Count);
	}
}
