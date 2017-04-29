using System;

namespace Lui
{
	internal class LRichElementImage : LRichElement
	{
		public string path
		{
			get;
			protected set;
		}

		public LRichElementImage(string path, string data)
		{
			base.type = RichType.IMAGE;
			this.path = path;
			base.data = data;
		}
	}
}
