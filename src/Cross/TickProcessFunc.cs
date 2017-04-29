using System;

namespace Cross
{
	public class TickProcessFunc : ITickProcessObject
	{
		protected Action<Variant> Func;

		public TickProcessFunc(Action<Variant> f)
		{
			this.Func = f;
		}

		public void tickCall(Variant prop)
		{
			this.Func(prop);
		}
	}
}
