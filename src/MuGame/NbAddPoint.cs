using GameFramework;
using System;

namespace MuGame
{
	internal class NbAddPoint : NewbieTeachItem
	{
		public static NbAddPoint create(string[] arr)
		{
			return new NbAddPoint();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_ADD_POINT, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_ADD_POINT, new Action<GameEvent>(base.onHanlde));
		}
	}
}
