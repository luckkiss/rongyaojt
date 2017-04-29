using GameFramework;
using System;

namespace MuGame
{
	public class skillbartest : FloatUi
	{
		public static EventTriggerListener.VoidDelegate ondrag;

		public static EventTriggerListener.VoidDelegate ondragout;

		public override void init()
		{
			base.alain();
			EventTriggerListener eventTriggerListener = EventTriggerListener.Get(base.getGameObjectByPath("bt"));
			eventTriggerListener.onDragOut = skillbartest.ondragout;
			eventTriggerListener.onDown = skillbartest.ondrag;
			base.init();
		}
	}
}
