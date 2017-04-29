using GameFramework;
using System;

namespace MuGame
{
	internal class NbSkillOn : NewbieTeachItem
	{
		public static NbSkillOn create(string[] arr)
		{
			return new NbSkillOn();
		}

		public override void addListener()
		{
			UiEventCenter.getInstance().addEventListener(UiEventCenter.EVENT_SKILL_DRAWEND, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			UiEventCenter.getInstance().removeEventListener(UiEventCenter.EVENT_SKILL_DRAWEND, new Action<GameEvent>(base.onHanlde));
		}
	}
}
