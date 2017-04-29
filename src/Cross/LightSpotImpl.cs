using System;
using UnityEngine;

namespace Cross
{
	public class LightSpotImpl : LightImpl, ILightSpot, ILight, IGraphObject3D, IGraphObject
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

		public float spotAngle
		{
			get
			{
				return this.m_light.spotAngle;
			}
			set
			{
				this.m_light.spotAngle = value;
			}
		}

		public LightSpotImpl()
		{
			this.m_u3dObj.name = "LightSpot";
			this.m_light.type = LightType.Spot;
		}
	}
}
