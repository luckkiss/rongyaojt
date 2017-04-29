using System;

namespace Cross
{
	public interface IUISliderbar : IUIBaseControl
	{
		float Value
		{
			get;
			set;
		}

		int SnapInterval
		{
			get;
			set;
		}

		int Maximum
		{
			get;
			set;
		}

		int Minimum
		{
			get;
			set;
		}

		int PageSize
		{
			get;
			set;
		}

		void move(float num);
	}
}
