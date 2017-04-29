using System;

namespace GameFramework
{
	public interface IObjectPlugin
	{
		string controlId
		{
			get;
			set;
		}

		void init();
	}
}
