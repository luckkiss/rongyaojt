using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class beginloading : LoadingUI
	{
		private Text text;

		public static beginloading instance;

		public override void init()
		{
			beginloading.instance = this;
			this.text = base.getTransformByPath("Text").GetComponent<Text>();
			bool flag = Baselayer.cemaraRectTran == null;
			if (flag)
			{
				Baselayer.cemaraRectTran = GameObject.Find("canvas_main").GetComponent<RectTransform>();
			}
			RectTransform cemaraRectTran = Baselayer.cemaraRectTran;
			RectTransform component = base.getTransformByPath("bg").GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(cemaraRectTran.rect.width, cemaraRectTran.rect.height);
		}

		public void setText(string str)
		{
		}

		public override void dispose()
		{
			beginloading.instance = null;
			base.dispose();
		}
	}
}
