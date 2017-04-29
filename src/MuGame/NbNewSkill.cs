using GameFramework;
using System;

namespace MuGame
{
	internal class NbNewSkill : NewbieTeachItem
	{
		public static NbNewSkill create(string[] arr)
		{
			return new NbNewSkill();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_NEW_SKILL, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_NEW_SKILL, new Action<GameEvent>(base.onHanlde));
		}
	}
}
