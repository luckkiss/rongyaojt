using System;

namespace Lui
{
	internal class LRichElementAnim : LRichElement
	{
		public string path
		{
			get;
			protected set;
		}

		public float fs
		{
			get;
			protected set;
		}

		public LRichElementAnim(string path, float fs, string data)
		{
			base.type = RichType.ANIM;
			this.path = path;
			base.data = data;
			this.fs = fs;
		}
	}
}
