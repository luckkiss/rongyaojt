using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class taskEvent
	{
		protected static LinkedList<taskEvent> m_pool = new LinkedList<taskEvent>();

		public GameEvent evt;

		public Action<GameEvent> listenerFun = null;

		public static taskEvent alloc()
		{
			bool flag = taskEvent.m_pool.Count > 0;
			taskEvent result;
			if (flag)
			{
				taskEvent value = taskEvent.m_pool.Last.Value;
				taskEvent.m_pool.RemoveLast();
				result = value;
			}
			else
			{
				result = new taskEvent();
			}
			return result;
		}

		public static void free(taskEvent te)
		{
			bool flag = te == null;
			if (!flag)
			{
				te.evt = null;
				te.listenerFun = null;
				taskEvent.m_pool.AddLast(te);
			}
		}
	}
}
