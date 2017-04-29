using System;

namespace Cross
{
	public interface IUIButton : IUIBaseButton, IUIBaseControl
	{
		float scales
		{
			get;
			set;
		}

		float times
		{
			get;
			set;
		}

		string mode
		{
			get;
			set;
		}
	}
}
