using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_mapname : FloatUi
	{
		public static a3_mapname instance;

		public override void init()
		{
			a3_mapname.instance = this;
			base.gameObject.SetActive(false);
		}

		public void refreshInfo()
		{
			base.gameObject.SetActive(true);
			Image component = base.transform.FindChild("ig").GetComponent<Image>();
			component.enabled = false;
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf((uint)GRMap.instance.m_nCurMapID);
			bool flag = singleMapConf.ContainsKey("map_title");
			if (flag)
			{
				bool flag2 = singleMapConf["map_title"] == 1;
				if (flag2)
				{
					component.enabled = true;
				}
				bool flag3 = singleMapConf["map_title"] == 0;
				if (flag3)
				{
					component.enabled = false;
				}
			}
			string path = "icon/map_pic/" + GRMap.instance.m_nCurMapID;
			component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			base.CancelInvoke("timeGo");
			base.Invoke("timeGo", 3f);
		}

		private void timeGo()
		{
			base.gameObject.SetActive(false);
		}
	}
}
