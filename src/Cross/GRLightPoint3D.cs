using System;

namespace Cross
{
	public class GRLightPoint3D : GRLight3D
	{
		protected ILightPoint m_lightPt;

		private float range
		{
			get
			{
				return this.m_lightPt.range;
			}
			set
			{
				this.m_lightPt.range = value;
			}
		}

		public GRLightPoint3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_light = this.m_world.scene3d.createLightDir();
			this.m_lightPt = (this.m_light as ILightPoint);
			this.m_light.enabled = this.m_enabled;
			this.m_light.color = this.m_color;
			this.m_light.intensity = this.m_intensity;
			this.m_light.shadow = this.m_shadow;
			this.m_light.layer = this.m_layer;
			this.m_light.enabled = true;
			this.m_rootObj.addChild(this.m_light);
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				base.load(conf, null, null);
				bool flag2 = this.m_conf.ContainsKey("range");
				if (flag2)
				{
					Variant variant = this.m_conf["range"][0];
					this.range = variant["val"]._float;
				}
			}
		}
	}
}
