using System;
using System.Collections.Generic;

namespace MuGame
{
	public class TickMgr
	{
		public static int tickNum = 0;

		private List<TickItem> ticks = new List<TickItem>();

		private List<TickItem> ticksNeedAdd = new List<TickItem>();

		public static TickMgr instance = new TickMgr();

		public void update(float delta)
		{
			List<TickItem> list = null;
			foreach (TickItem current in this.ticks)
			{
				bool flag = !current.isTicking;
				if (flag)
				{
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<TickItem>();
					}
					list.Add(current);
				}
				else
				{
					current.tick(delta);
				}
			}
			bool flag3 = list != null;
			if (flag3)
			{
				foreach (TickItem current2 in list)
				{
					this.ticks.Remove(current2);
				}
			}
			bool flag4 = this.ticksNeedAdd.Count > 0;
			if (flag4)
			{
				foreach (TickItem current3 in this.ticksNeedAdd)
				{
					this.ticks.Add(current3);
				}
				this.ticksNeedAdd.Clear();
			}
		}

		public void updateAfterRender()
		{
			TickMgr.tickNum++;
			bool flag = TickMgr.tickNum > 60000;
			if (flag)
			{
				TickMgr.tickNum = 0;
			}
		}

		public void addTick(TickItem tick)
		{
			bool isTicking = tick.isTicking;
			if (!isTicking)
			{
				this.ticksNeedAdd.Add(tick);
				tick.isTicking = true;
			}
		}

		public void removeTick(TickItem tick)
		{
			tick.isTicking = false;
		}
	}
}
