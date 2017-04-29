using System;

namespace Cross
{
	public interface ITextInput : IGraphObject2D, IGraphObject
	{
		bool isRead
		{
			get;
			set;
		}

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

		int maxnum
		{
			get;
			set;
		}

		int lineMax
		{
			get;
			set;
		}

		float textX
		{
			get;
			set;
		}

		float textY
		{
			get;
			set;
		}

		float textWidth
		{
			get;
		}

		float textHeight
		{
			get;
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

		bool focus
		{
			get;
			set;
		}

		bool displayAsPassword
		{
			get;
			set;
		}

		void input();
	}
}
