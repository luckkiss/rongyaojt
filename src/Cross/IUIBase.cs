using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IUIBase
	{
		string controlId
		{
			get;
		}

		Dictionary<string, Action<IUIBaseControl, Event>> eventReceiver
		{
			get;
		}
	}
}
