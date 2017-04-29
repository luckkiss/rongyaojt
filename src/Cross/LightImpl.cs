using System;
using UnityEngine;

namespace Cross
{
	public class LightImpl : GraphObject3DImpl, ILight, IGraphObject3D, IGraphObject
	{
		protected Light m_light;

		protected Vec4 m_color = new Vec4(1f, 1f, 1f, 1f);

		protected bool m_shadow;

		public bool enabled
		{
			get
			{
				return this.m_light.enabled;
			}
			set
			{
				this.m_light.enabled = value;
			}
		}

		public Vec4 color
		{
			get
			{
				return new Vec4(this.m_light.color.r, this.m_light.color.g, this.m_light.color.b, this.m_light.color.a);
			}
			set
			{
				this.m_color.set(value.x, value.y, value.z, value.w);
				this.m_light.color = new Color(value.x, value.y, value.z, value.w);
			}
		}

		public float intensity
		{
			get
			{
				return this.m_light.intensity;
			}
			set
			{
				this.m_light.intensity = value;
			}
		}

		public bool shadow
		{
			get
			{
				return this.m_shadow;
			}
			set
			{
				this.m_shadow = value;
				bool shadow = this.m_shadow;
				if (shadow)
				{
					this.m_light.shadows = LightShadows.Soft;
				}
				else
				{
					this.m_light.shadows = LightShadows.None;
				}
			}
		}

		public LightImpl()
		{
			this.m_light = this.m_u3dObj.AddComponent<Light>();
			this.color = new Vec4(1f, 1f, 1f, 1f);
			this.shadow = false;
			this.addLayer(0);
		}

		public void addLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				int cullingMask = this.m_light.cullingMask;
				this.m_light.cullingMask = cullingMask;
			}
		}

		public void removeLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				int cullingMask = this.m_light.cullingMask;
				this.m_light.cullingMask = cullingMask;
			}
		}

		public bool hasLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int cullingMask = this.m_light.cullingMask;
				result = (cullingMask != 0);
			}
			return result;
		}

		public void clearLayers()
		{
			this.m_light.cullingMask = 0;
		}
	}
}
