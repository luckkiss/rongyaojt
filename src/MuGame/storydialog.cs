using GameFramework;
using System;
using System.Collections;
using UnityEngine.UI;

namespace MuGame
{
	internal class storydialog : StoryUI
	{
		public static storydialog instance;

		public Text txt;

		public static void show(string dialog = "")
		{
			bool flag = dialog == "";
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.STORY_DIALOG);
			}
			else
			{
				bool flag2 = storydialog.instance == null;
				if (flag2)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(dialog);
					InterfaceMgr.getInstance().open(InterfaceMgr.STORY_DIALOG, arrayList, false);
				}
				else
				{
					storydialog.instance.txt.text = dialog;
				}
			}
		}

		public override void init()
		{
			base.alain();
			this.txt = base.getComponentByPath<Text>("txt");
		}

		public override void onShowed()
		{
			storydialog.instance = this;
			this.txt.text = (this.uiData[0] as string);
			base.onShowed();
			bool flag = !GRMap.playingPlot && gameST._bUntestPlot;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.STORY_DIALOG);
			}
		}

		public override void onClosed()
		{
			storydialog.instance = null;
			base.onClosed();
		}
	}
}
