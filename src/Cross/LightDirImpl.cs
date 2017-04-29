using System;
using UnityEngine;

namespace Cross
{
	public class LightDirImpl : LightImpl, ILightDir, ILight, IGraphObject3D, IGraphObject
	{
		protected Vec3 m_dir;

		public Vec3 direction
		{
			get
			{
				return this.m_dir;
			}
			set
			{
				this.m_dir.set(value.x, value.y, value.z);
				this.m_u3dObj.transform.forward = new Vector3(value.x, value.y, value.z);
			}
		}

		public LightDirImpl()
		{
			this.m_u3dObj.name = "LightDir";
			this.m_light.type = LightType.Directional;
			this.m_dir = new Vec3(this.m_u3dObj.transform.forward.x, this.m_u3dObj.transform.forward.y, this.m_u3dObj.transform.forward.z);
		}
	}
}
