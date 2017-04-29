using Cross;
using System;

namespace GameFramework
{
	public struct uiData
	{
		public IUIBaseControl evt;

		public Variant info;

		public void clear()
		{
			this.evt = null;
		}
	}
}
