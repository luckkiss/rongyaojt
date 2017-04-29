using System;

namespace Cross
{
	public class Event
	{
		public object data = null;

		public object target = null;

		public object currentTarget = null;

		public bool stopPropogation = false;
	}
}
