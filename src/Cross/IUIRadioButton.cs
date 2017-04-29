using System;

namespace Cross
{
	public interface IUIRadioButton : IUIBaseButton, IUIBaseControl
	{
		string groupName
		{
			get;
			set;
		}

		string selectRadioId
		{
			get;
		}

		int value
		{
			set;
		}
	}
}
