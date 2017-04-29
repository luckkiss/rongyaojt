using Cross;
using System;

namespace GameFramework
{
	public class loadUIStruct
	{
		public Action<IUI, Variant> onLoadedCallback;

		public string uiname;

		public Variant data;
	}
}
