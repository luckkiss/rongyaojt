using GameFramework;
using System;

namespace MuGame
{
	internal class HeroMgr : lgGDBase, IObjectPlugin
	{
		public static HeroMgr instacne;

		public HeroMgr(gameManager m) : base(m)
		{
			HeroMgr.instacne = this;
		}
	}
}
