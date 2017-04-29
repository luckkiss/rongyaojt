using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LPageItem : MonoBehaviour
{
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		public static readonly LPageItem.<>c <>9 = new LPageItem.<>c();

		public static Action <>9__3_0;

		internal void <get_ResetPage>b__3_0()
		{
			LPageItem.currentPage = 0;
		}
	}

	public Transform prefabItemParent;

	private static int currentPage = 0;

	public static Action ResetPage
	{
		get
		{
			Action arg_20_0;
			if ((arg_20_0 = LPageItem.<>c.<>9__3_0) == null)
			{
				arg_20_0 = (LPageItem.<>c.<>9__3_0 = new Action(LPageItem.<>c.<>9.<get_ResetPage>b__3_0));
			}
			return arg_20_0;
		}
	}

	public void Init(List<GameObject> datas)
	{
		for (int i = 0; i < datas.Count; i++)
		{
			datas[i].transform.SetParent(this.prefabItemParent, false);
		}
	}

	public void Init(List<GameObject> datas, GameObject prefab, string path, string[,] itemInfo)
	{
		for (int i = 0; i < datas.Count; i++)
		{
			prefab.transform.GetChild(0).GetComponent<Text>().text = itemInfo[0, i + LPageItem.currentPage * 6];
			prefab.transform.GetChild(1).GetComponent<Text>().text = itemInfo[1, i + LPageItem.currentPage * 6];
			Transform transform = UnityEngine.Object.Instantiate<GameObject>(prefab).transform;
			transform.gameObject.SetActive(true);
			Transform parent = transform.FindChild(path);
			datas[i].transform.SetParent(parent, false);
			datas[i].transform.FindChild("ClickArea").SetParent(transform, false);
			transform.transform.SetParent(this.prefabItemParent, false);
		}
		LPageItem.currentPage++;
	}
}
