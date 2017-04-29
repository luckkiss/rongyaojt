using System;

namespace Cross
{
	public interface IText : IGraphObject2D, IGraphObject
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

		bool fontBold
		{
			get;
			set;
		}

		Vec3 textColor
		{
			get;
			set;
		}

		Define.TextAlign align
		{
			get;
			set;
		}

		bool displayAsPassword
		{
			get;
			set;
		}
	}
}
