using System;
using UnityEngine;

namespace Cross
{
	public class LightPointImpl : LightImpl, ILightPoint, ILight, IGraphObject3D, IGraphObject
	{
		public float range
		{
			get
			{
				return this.m_light.range;
			}
			set
			{
				this.m_light.range = value;
			}
		}

		public LightPointImpl()
		{
			this.m_u3dObj.name = "LightPoint";
			this.m_light.type = LightType.Point;
		}
	}
}
