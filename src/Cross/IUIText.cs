using System;

namespace Cross
{
	public interface IUIText : IUIBaseControl
	{
		string text
		{
			get;
			set;
		}

		string fontName
		{
			get;
			set;
		}

		int fontSize
		{
			get;
			set;
		}

		int lineMax
		{
			get;
			set;
		}

		string color
		{
			set;
		}

		Define.TextAlign fontAlign
		{
			get;
			set;
		}

		Vec3 textColor
		{
			get;
			set;
		}

		void fontBold(bool t);

		void appendText(string t);
	}
}
