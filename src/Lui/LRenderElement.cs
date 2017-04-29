using System;
using UnityEngine;

namespace Lui
{
	internal struct LRenderElement
	{
		public RichType type;

		public string strChar;

		public int width;

		public int height;

		public bool isOutLine;

		public bool isUnderLine;

		public Font font;

		public int fontSize;

		public Color color;

		public string data;

		public string path;

		public float fs;

		public bool isNewLine;

		public Vector2 pos;

		public RectTransform rect;

		public LRenderElement Clone()
		{
			LRenderElement result;
			result.type = this.type;
			result.strChar = this.strChar;
			result.width = this.width;
			result.height = this.height;
			result.isOutLine = this.isOutLine;
			result.isUnderLine = this.isUnderLine;
			result.font = this.font;
			result.fontSize = this.fontSize;
			result.color = this.color;
			result.data = this.data;
			result.path = this.path;
			result.fs = this.fs;
			result.isNewLine = this.isNewLine;
			result.pos = this.pos;
			result.rect = this.rect;
			return result;
		}
	}
}
