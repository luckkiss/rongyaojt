using GameFramework;
using System;

namespace MuGame
{
	internal class NbChangeWorld : NewbieTeachItem
	{
		public static NbChangeWorld create(string[] arr)
		{
			return new NbChangeWorld();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_MAP_CHANGED, new Action<GameEvent>(this.handle));
		}

		public void handle(GameEvent e)
		{
			base.onHanlde(e);
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_MAP_CHANGED, new Action<GameEvent>(this.handle));
		}
	}
}
