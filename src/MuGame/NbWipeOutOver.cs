using GameFramework;
using System;

namespace MuGame
{
	internal class NbWipeOutOver : NewbieTeachItem
	{
		public static NbWipeOutOver create(string[] arr)
		{
			return new NbWipeOutOver();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_FB_WIPEOUT_OVER, new Action<GameEvent>(this.handle));
		}

		public void handle(GameEvent e)
		{
			base.onHanlde(e);
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_FB_WIPEOUT_OVER, new Action<GameEvent>(this.handle));
		}
	}
}
