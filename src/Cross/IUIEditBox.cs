using System;

namespace Cross
{
	public interface IUIEditBox : IUIBaseControl
	{
		string text
		{
			get;
			set;
		}

		string restrict
		{
			get;
			set;
		}

		int maxChars
		{
			get;
			set;
		}

		string appendText
		{
			get;
			set;
		}

		int fontSize
		{
			get;
			set;
		}

		string fontName
		{
			get;
			set;
		}

		int lineMax
		{
			get;
			set;
		}

		bool displayAsPassword
		{
			get;
			set;
		}

		void setFocus();

		void setSelection(int beginIndex, int endIndex);
	}
}
