using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	[AddComponentMenu("UI/Effects/DarkUI"), ExecuteInEditMode]
	public class UIDark : MonoBehaviour
	{
		public bool IsDark
		{
			get;
			set;
		}

		private void Start()
		{
		}

		[ContextMenu("重置操作")]
		public void UIReset()
		{
			this.REMO();
		}

		[ContextMenu("应用操作")]
		public void UIAppliy()
		{
			this.ADDO();
		}

		public void ADDO()
		{
			Material material = Resources.Load<Material>("uifx/uiGray");
			bool flag = material == null;
			if (flag)
			{
				Debug.LogError("找不到需求材质！ = uiGray.mat");
			}
			else
			{
				Image[] componentsInChildren = base.transform.GetComponentsInChildren<Image>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Image image = componentsInChildren[i];
					image.material = material;
					image.material.SetFloat("_grayScale", 0.3f);
				}
				this.IsDark = true;
			}
		}

		public void REMO()
		{
			Image[] componentsInChildren = base.transform.GetComponentsInChildren<Image>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Image image = componentsInChildren[i];
				image.material = null;
			}
			this.IsDark = false;
		}
	}
}
