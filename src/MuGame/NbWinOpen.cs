using GameFramework;
using System;

namespace MuGame
{
	internal class NbWinOpen : NewbieTeachItem
	{
		public static NbWinOpen create(string[] arr)
		{
			return new NbWinOpen();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_WIN_OPEN, new Action<GameEvent>(this.doHandle));
		}

		public void doHandle(GameEvent e)
		{
			base.onHanlde(e);
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_WIN_OPEN, new Action<GameEvent>(this.doHandle));
		}
	}
}
