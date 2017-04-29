using System;
using System.Collections.Generic;

namespace Cross
{
	public class ConfigLoadingData
	{
		public string url = "";

		public List<Action<Variant>> onFinCBs = new List<Action<Variant>>();
	}
}
