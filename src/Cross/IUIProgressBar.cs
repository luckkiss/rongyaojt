using System;

namespace Cross
{
	public interface IUIProgressBar : IUIBaseControl
	{
		float num
		{
			get;
			set;
		}

		float maxNum
		{
			get;
			set;
		}

		Define.ProgressDirection direction
		{
			get;
			set;
		}

		bool filp
		{
			get;
			set;
		}

		float value
		{
			get;
			set;
		}
	}
}
