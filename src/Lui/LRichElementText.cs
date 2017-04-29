using System;
using UnityEngine;

namespace Lui
{
	internal class LRichElementText : LRichElement
	{
		public string txt
		{
			get;
			protected set;
		}

		public bool isUnderLine
		{
			get;
			protected set;
		}

		public bool isOutLine
		{
			get;
			protected set;
		}

		public int fontSize
		{
			get;
			protected set;
		}

		public LRichElementText(Color color, string txt, int fontSize, bool isUnderLine, bool isOutLine, string data)
		{
			base.type = RichType.TEXT;
			base.color = color;
			this.txt = txt;
			this.fontSize = fontSize;
			this.isUnderLine = isUnderLine;
			this.isOutLine = isOutLine;
			base.data = data;
		}
	}
}
