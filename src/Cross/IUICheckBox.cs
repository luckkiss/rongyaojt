using System;

namespace Cross
{
	public interface IUICheckBox : IUIBaseControl
	{
		bool isChecked
		{
			get;
			set;
		}

		int value
		{
			get;
			set;
		}

		string labelText
		{
			get;
			set;
		}
	}
}
