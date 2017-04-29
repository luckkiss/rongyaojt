using System;
using UnityEngine;

namespace Lui
{
	internal class LRichCacheElement : UnityEngine.Object
	{
		public bool isUse;

		public GameObject node;

		public LRichCacheElement(GameObject node)
		{
			this.node = node;
		}
	}
}
