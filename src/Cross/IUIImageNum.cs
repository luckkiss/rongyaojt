using System;

namespace Cross
{
	public interface IUIImageNum : IUIBaseControl
	{
		string assetDesc
		{
			get;
			set;
		}

		string num
		{
			get;
			set;
		}

		float numSpace
		{
			get;
			set;
		}

		string value
		{
			get;
			set;
		}

		float movedistance
		{
			get;
			set;
		}

		void PlayTextToText(string v, Action fun);
	}
}
