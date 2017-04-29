using GameFramework;
using System;

namespace MuGame
{
	internal class NbFBInit : NewbieTeachItem
	{
		public static NbFBInit create(string[] arr)
		{
			return new NbFBInit();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_FB_INITED, new Action<GameEvent>(this.handle));
		}

		public void handle(GameEvent e)
		{
			base.onHanlde(e);
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_FB_INITED, new Action<GameEvent>(this.handle));
		}
	}
}
