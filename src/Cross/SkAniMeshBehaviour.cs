using System;
using UnityEngine;

namespace Cross
{
	internal class SkAniMeshBehaviour : MonoBehaviour
	{
		protected SkAniMeshImpl m_obj = null;

		public SkAniMeshImpl obj
		{
			get
			{
				return this.m_obj;
			}
			set
			{
				this.m_obj = value;
			}
		}

		private void Start()
		{
		}

		public void onEventAnim()
		{
			bool flag = this.m_obj == null;
			if (!flag)
			{
				this.m_obj.onAnimPlayEnd();
			}
		}
	}
}
