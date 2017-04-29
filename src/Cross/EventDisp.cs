using System;
using System.Collections.Generic;

namespace Cross
{
	public class EventDisp
	{
		protected Dictionary<int, Dictionary<string, List<Action<EventDisp, Variant>>>> m_eventCBFuncMap = new Dictionary<int, Dictionary<string, List<Action<EventDisp, Variant>>>>();

		public void addEventListener(int eventID, Action<EventDisp, Variant> cb)
		{
			bool flag = this.m_eventCBFuncMap == null;
			if (flag)
			{
				this.m_eventCBFuncMap = new Dictionary<int, Dictionary<string, List<Action<EventDisp, Variant>>>>();
			}
			Dictionary<string, List<Action<EventDisp, Variant>>> dictionary = new Dictionary<string, List<Action<EventDisp, Variant>>>();
			bool flag2 = !this.m_eventCBFuncMap.ContainsKey(eventID);
			if (flag2)
			{
				dictionary["cbs"] = new List<Action<EventDisp, Variant>>();
				this.m_eventCBFuncMap[eventID] = dictionary;
			}
			else
			{
				dictionary = this.m_eventCBFuncMap[eventID];
			}
			dictionary["cbs"].Add(cb);
		}

		public void removeEventListener(int eventID, Action<EventDisp, Variant> cb)
		{
			bool flag = this.m_eventCBFuncMap == null;
			if (!flag)
			{
				bool flag2 = !this.m_eventCBFuncMap.ContainsKey(eventID);
				if (!flag2)
				{
					Dictionary<string, List<Action<EventDisp, Variant>>> dictionary = this.m_eventCBFuncMap[eventID];
					int num = -1;
					for (int i = 0; i < dictionary["cbs"].Count; i++)
					{
						bool flag3 = dictionary["cbs"][i] == cb;
						if (flag3)
						{
							num = i;
						}
					}
					bool flag4 = num < 0;
					if (!flag4)
					{
						dictionary["cbs"].RemoveAt(num);
						bool flag5 = dictionary["cbs"].Count <= 0;
						if (flag5)
						{
							this.m_eventCBFuncMap.Remove(eventID);
						}
					}
				}
			}
		}

		protected void _dispEvent(int eventID, Variant evtpar)
		{
			bool flag = this.m_eventCBFuncMap == null;
			if (!flag)
			{
				bool flag2 = !this.m_eventCBFuncMap.ContainsKey(eventID);
				if (!flag2)
				{
					for (int i = 0; i < this.m_eventCBFuncMap[eventID]["cbs"].Count; i++)
					{
						this.m_eventCBFuncMap[eventID]["cbs"][i](this, evtpar);
					}
				}
			}
		}

		public virtual void dispose()
		{
			this.m_eventCBFuncMap = null;
		}
	}
}
