using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class plot_linkui : StoryUI
	{
		public static plot_linkui instant = null;

		public static Dictionary<string, GameObject> s_plotui_Linker = new Dictionary<string, GameObject>();

		public static void show(string plotui = "")
		{
			bool flag = plot_linkui.instant == null;
			if (!flag)
			{
				bool flag2 = plot_linkui.s_plotui_Linker.ContainsKey(plotui);
				if (flag2)
				{
					UnityEngine.Object.Destroy(plot_linkui.s_plotui_Linker[plotui]);
					plot_linkui.s_plotui_Linker.Remove(plotui);
				}
				else
				{
					GameObject gameObject = Resources.Load("plotui/" + plotui) as GameObject;
					bool flag3 = gameObject != null;
					if (flag3)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						gameObject2.transform.SetParent(plot_linkui.instant.transform, false);
						plot_linkui.s_plotui_Linker.Add(plotui, gameObject2);
					}
				}
			}
		}

		public static void ClearAll()
		{
			plot_linkui.instant = null;
			plot_linkui.s_plotui_Linker.Clear();
		}

		public override void onShowed()
		{
			plot_linkui.instant = this;
			base.onShowed();
		}

		public override void onClosed()
		{
			plot_linkui.instant = null;
			base.onClosed();
		}
	}
}
