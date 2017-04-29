using GameFramework;
using System;

namespace MuGame
{
	internal class NbClick : NewbieTeachItem
	{
		public static NbClick create(string[] arr)
		{
			return new NbClick();
		}

		public override void addListener()
		{
			MouseClickMgr.instance.addEventListener(MouseClickMgr.EVENT_TOUCH_UI, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			MouseClickMgr.instance.removeEventListener(MouseClickMgr.EVENT_TOUCH_UI, new Action<GameEvent>(base.onHanlde));
		}
	}
}
