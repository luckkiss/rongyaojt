using System;

namespace Cross
{
	public interface IUIBaseButton : IUIBaseControl
	{
		string fontName
		{
			get;
			set;
		}

		string fontColor
		{
			get;
			set;
		}

		int fontSize
		{
			get;
			set;
		}

		string text
		{
			get;
			set;
		}

		Define.ButtonState state
		{
			get;
			set;
		}

		bool spriteGray
		{
			get;
			set;
		}

		void changeBgFilter(string stylename, string filterID, string propname, string value);

		void clearBgFilter(string statusname);
	}
}
