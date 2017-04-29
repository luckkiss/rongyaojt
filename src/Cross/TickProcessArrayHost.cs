using System;
using System.Collections.Generic;

namespace Cross
{
	public class TickProcessArrayHost
	{
		protected uint m_maxTimeSlice = 20u;

		protected List<Variant> m_tickObjVec;

		public uint maxTimeSlice
		{
			get
			{
				return this.m_maxTimeSlice;
			}
			set
			{
				this.m_maxTimeSlice = value;
			}
		}

		public TickProcessArrayHost()
		{
			this.m_tickObjVec = new List<Variant>();
		}

		public void pushObject(ITickProcessObject obj, Variant prop)
		{
		}
	}
}
