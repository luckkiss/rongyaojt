using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class teachline : Window
	{
		public static float remainCD = 1f;

		public static string desc = "";

		public Text txt;

		public RectTransform bg;

		public static teachline instance;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public static void show(string str, float sec)
		{
			teachline.remainCD = sec;
			teachline.desc = str;
			bool flag = teachline.instance != null;
			if (flag)
			{
				teachline.instance.showDesc();
			}
			InterfaceMgr.getInstance().open(InterfaceMgr.NEWBIE_LINE, null, false);
		}

		public override void init()
		{
			this.bg = base.getComponentByPath<RectTransform>("Image");
			this.txt = base.getComponentByPath<Text>("Text");
		}

		public override void onShowed()
		{
			this.showDesc();
			teachline.instance = this;
			base.onShowed();
		}

		public void showDesc()
		{
			this.txt.text = teachline.desc;
			this.bg.sizeDelta = new Vector2(this.txt.preferredWidth + 20f, this.bg.sizeDelta.y);
			base.CancelInvoke("close");
			base.Invoke("close", teachline.remainCD);
		}

		public override void onClosed()
		{
			teachline.instance = null;
		}

		private void close()
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.NEWBIE_LINE);
		}
	}
}
