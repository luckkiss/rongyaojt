using GameFramework;
using System;

namespace MuGame
{
	internal class NbWinClose : NewbieTeachItem
	{
		public string winId = "";

		public static NbWinClose create(string[] arr)
		{
			NbWinClose nbWinClose = new NbWinClose();
			bool flag = arr.Length > 1;
			if (flag)
			{
				nbWinClose.winId = arr[1];
			}
			return nbWinClose;
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_WIN_CLOSE, new Action<GameEvent>(this.dohandle));
		}

		private void dohandle(GameEvent e)
		{
			bool flag = this.winId == "" || this.winId == e.orgdata.ToString();
			if (flag)
			{
				base.onHanlde(e);
			}
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_WIN_CLOSE, new Action<GameEvent>(this.dohandle));
		}
	}
}
