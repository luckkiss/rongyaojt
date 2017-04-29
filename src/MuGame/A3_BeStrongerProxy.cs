using GameFramework;
using System;

namespace MuGame
{
	internal class A3_BeStrongerProxy : BaseProxy<A3_BeStrongerProxy>
	{
		public A3_BeStrongerProxy()
		{
			base.addEventListener(65u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(63u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(68u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(75u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(48u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(60u, new Action<GameEvent>(this.CheckItem));
			base.addEventListener(86u, new Action<GameEvent>(this.CheckItem));
		}

		private void CheckItem(GameEvent e = null)
		{
			A3_BeStronger expr_05 = A3_BeStronger.Instance;
			if (expr_05 != null)
			{
				expr_05.CheckUpItem();
			}
		}
	}
}
