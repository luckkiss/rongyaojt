using System;

namespace GameFramework
{
	public class UiEventCenter : GameEventDispatcher
	{
		public static uint EVENT_WIN_OPEN = 1u;

		public static uint EVENT_MAP_CHANGED = 2u;

		public static uint EVENT_WIN_CLOSE = 3u;

		public static uint EVENT_LOTTERY_DRAW = 4u;

		public static uint EVENT_HERO_3D_SKILL_OVER = 4u;

		public static uint EVENT_FB_INITED = 5u;

		public static uint EVENT_FB_WIPEOUT_OVER = 6u;

		public static uint EVENT_ACHIEVE_INITED = 7u;

		public static uint EVENT_NEW_SKILL = 8u;

		public static uint EVENT_SKILL_DRAWEND = 9u;

		public static uint EVENT_START_MOVE = 10u;

		public static uint EVENT_ADD_POINT = 11u;

		public static string lastClosedWinID;

		private static UiEventCenter _instance;

		public void onWinOpen(string winid)
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_WIN_OPEN, this, winid, false));
		}

		public void onWinClosed(string winid)
		{
			UiEventCenter.lastClosedWinID = winid;
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_WIN_CLOSE, this, winid, false));
		}

		public void onLotteryDrawed()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_LOTTERY_DRAW, this, null, false));
		}

		public void onMapChanged()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_MAP_CHANGED, this, null, false));
		}

		public void onHeroSkillPlayerOver()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_HERO_3D_SKILL_OVER, this, null, false));
		}

		public void onFbInited()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_FB_INITED, this, null, false));
		}

		public void onFbWipeoutOver()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_FB_WIPEOUT_OVER, this, null, false));
		}

		public void onAchiInited()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_ACHIEVE_INITED, this, null, false));
		}

		public void onNewSkill()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_NEW_SKILL, this, null, false));
		}

		public void onSkillDrawEnd()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_SKILL_DRAWEND, this, null, false));
		}

		public void onStartMove()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_START_MOVE, this, null, false));
		}

		public void onAddPoint()
		{
			base.dispatchEvent(GameEvent.Create(UiEventCenter.EVENT_ADD_POINT, this, null, false));
		}

		public static UiEventCenter getInstance()
		{
			bool flag = UiEventCenter._instance == null;
			if (flag)
			{
				UiEventCenter._instance = new UiEventCenter();
			}
			return UiEventCenter._instance;
		}
	}
}
