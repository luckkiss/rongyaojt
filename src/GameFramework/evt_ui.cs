using Cross;
using System;

namespace GameFramework
{
	public struct evt_ui
	{
		public IUIBaseControl evt;

		public void clear()
		{
			this.evt = null;
		}
	}
}
