using System;
using UnityEngine;

namespace Lui
{
	internal class LRichElement : UnityEngine.Object
	{
		public RichType type
		{
			get;
			protected set;
		}

		public Color color
		{
			get;
			protected set;
		}

		public string data
		{
			get;
			protected set;
		}
	}
}
