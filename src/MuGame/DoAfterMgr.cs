using System;
using System.Collections.Generic;

namespace MuGame
{
	public class DoAfterMgr
	{
		private List<Action> listAfterRender = new List<Action>();

		public static DoAfterMgr instacne;

		public void addAfterRender(Action fun)
		{
			this.listAfterRender.Add(fun);
		}

		public void onAfterRender()
		{
			bool flag = this.listAfterRender.Count == 0;
			if (!flag)
			{
				foreach (Action current in this.listAfterRender)
				{
					current();
				}
				this.listAfterRender.Clear();
			}
		}

		public static DoAfterMgr init()
		{
			bool flag = DoAfterMgr.instacne == null;
			if (flag)
			{
				DoAfterMgr.instacne = new DoAfterMgr();
			}
			return DoAfterMgr.instacne;
		}
	}
}
