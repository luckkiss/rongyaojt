using Cross;
using System;

namespace GameFramework
{
	public struct V_EVENT
	{
		public Event evt;

		public IUIBaseControl Bcontrol;

		public IUIBaseControl btn;

		public Variant data;

		public uint labelIdx;

		public string text;

		public void setdefalt()
		{
			this.evt = null;
			this.btn = null;
			this.Bcontrol = null;
			this.data = null;
			this.labelIdx = 0u;
			this.text = "";
		}
	}
}
