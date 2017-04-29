using System;

namespace MuGame
{
	public class TickItem
	{
		public bool isTicking = false;

		public Action<float> tick;

		public TickItem(Action<float> tickHandle)
		{
			this.tick = tickHandle;
		}
	}
}
