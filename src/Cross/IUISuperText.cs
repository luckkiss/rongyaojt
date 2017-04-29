using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IUISuperText : IUIScrollPanel, IUIContainer, IUIBaseControl
	{
		int bottomMargin
		{
			get;
			set;
		}

		int offsetRightLayout
		{
			get;
			set;
		}

		int offsetCenterLayout
		{
			get;
			set;
		}

		int offsetWidth
		{
			get;
			set;
		}

		int verticalLinePadding
		{
			get;
			set;
		}

		Dictionary<string, Action<Variant, string>> superTextEvtReceive
		{
			get;
			set;
		}

		int maxLines
		{
			get;
			set;
		}

		int scrollBarHeight
		{
			set;
		}

		string defalutTextFormat
		{
			get;
			set;
		}

		string defalutTextStyle
		{
			get;
			set;
		}

		bool autoHeight
		{
			set;
		}

		string lineAlign
		{
			set;
		}

		string setLineAlign
		{
			set;
		}

		bool autoScroll
		{
			get;
			set;
		}

		string htmltext
		{
			get;
			set;
		}

		string text
		{
			get;
			set;
		}

		float textHeight
		{
			get;
		}

		float textWidth
		{
			get;
		}

		int fontSize
		{
			get;
			set;
		}

		string color
		{
			set;
		}

		float lineHeight
		{
			get;
			set;
		}

		uint getTrigerEventID(string eventName);

		void AddNewText(string strHtml);

		void removeAt(int index);
	}
}
