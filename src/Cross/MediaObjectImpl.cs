using System;
using UnityEngine;

namespace Cross
{
	public class MediaObjectImpl : IMediaObject
	{
		protected GameObject m_obj;

		public MediaObjectImpl()
		{
			this.m_obj = new GameObject();
		}

		public void dispose()
		{
			UnityEngine.Object.Destroy(this.m_obj);
			this.m_obj = null;
		}
	}
}
