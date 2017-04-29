using GameFramework;
using System;

namespace MuGame
{
	internal class NbJoystick : NewbieTeachItem
	{
		public static NbJoystick create(string[] arr)
		{
			return new NbJoystick();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_START_MOVE, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_START_MOVE, new Action<GameEvent>(base.onHanlde));
		}
	}
}
