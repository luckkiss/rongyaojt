using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class SubHeadNode
	{
		private Dictionary<uint, KeyValuePair<int, GameObject>> items;

		public static List<int> ListPartIdx;

		public RectTransform RectScroll;

		public Transform content;

		public GameObject HeadObj;

		public LayoutElement layoutElement;

		public SubHeadNode(GameObject go)
		{
			this.items = new Dictionary<uint, KeyValuePair<int, GameObject>>();
			this.HeadObj = go;
			this.content = go.transform.FindChild("scroll/content");
			this.layoutElement = go.GetComponent<LayoutElement>();
			Transform expr_43 = this.content;
			this.RectScroll = ((expr_43 != null) ? expr_43.GetComponent<RectTransform>() : null);
		}

		public void FixHeight()
		{
			this.layoutElement.minHeight = Mathf.Min(this.content.GetComponent<RectTransform>().sizeDelta.y, A3_Smithy.Instance.transEqpList.GetComponent<RectTransform>().sizeDelta.y);
		}

		public void Add(uint tpid, KeyValuePair<int, GameObject> item)
		{
			bool flag = !this.items.ContainsKey(tpid);
			if (flag)
			{
				this.items.Add(tpid, item);
				item.Value.transform.SetParent(this.content, false);
			}
		}

		public void ShowItemByPart(int part)
		{
			List<uint> list = new List<uint>(this.items.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				int equip_level = ModelBase<a3_BagModel>.getInstance().getItemDataById(list[i]).equip_level;
				bool flag = part == this.items[list[i]].Key || (part == 0 && SubHeadNode.ListPartIdx.Contains(this.items[list[i]].Key));
				flag = (flag && equip_level <= ModelBase<A3_SmithyModel>.getInstance().GetMaxAllowedSetLevel(ModelBase<A3_SmithyModel>.getInstance().CurSmithyLevel));
				this.items[list[i]].Value.SetActive(flag);
				this.items[list[i]].Value.transform.SetSiblingIndex(i);
			}
		}

		public float GetFixedHeight(int part)
		{
			int i = 0;
			uint num = 0u;
			List<uint> list = new List<uint>(this.items.Keys);
			while (i < list.Count)
			{
				bool flag = ((part == 0 && SubHeadNode.ListPartIdx.Contains(this.items[list[i]].Key)) || part == this.items[list[i]].Key) && this.items[list[i]].Value.activeSelf;
				if (flag)
				{
					num += 1u;
				}
				i++;
			}
			return num * this.content.GetComponent<VerticalLayoutGroup>().spacing;
		}

		public void FixContentHeight()
		{
			int num = 0;
			float? num2 = null;
			float? num3 = null;
			for (int i = 0; i < this.content.childCount; i++)
			{
				bool activeSelf = this.content.transform.GetChild(i).gameObject.activeSelf;
				if (activeSelf)
				{
					num++;
				}
			}
			for (int j = 0; j < this.content.childCount; j++)
			{
				bool activeSelf2 = this.content.transform.GetChild(j).gameObject.activeSelf;
				if (activeSelf2)
				{
					bool flag = !num2.HasValue;
					if (flag)
					{
						num2 = new float?(this.content.transform.GetChild(j).gameObject.GetComponent<RectTransform>().anchoredPosition.y);
					}
					else
					{
						bool flag2 = !num3.HasValue;
						if (!flag2)
						{
							break;
						}
						num3 = new float?(this.content.transform.GetChild(j).gameObject.GetComponent<RectTransform>().anchoredPosition.y);
					}
				}
			}
			bool flag3 = !num3.HasValue;
			if (!flag3)
			{
				this.content.GetComponent<LayoutElement>().minHeight = (num2.Value - num3.Value) * (float)num;
			}
		}
	}
}
