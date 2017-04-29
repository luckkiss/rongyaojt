using System;

namespace Cross
{
	public class GRLightDir3D : GRLight3D
	{
		protected ILightDir m_lightDir;

		public Vec3 direction
		{
			get
			{
				return this.m_lightDir.direction;
			}
			set
			{
				bool flag = this.m_lightDir != null;
				if (flag)
				{
					this.m_lightDir.direction = value;
				}
			}
		}

		public GRLightDir3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_light = this.m_world.scene3d.createLightDir();
			this.m_lightDir = (this.m_light as ILightDir);
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
				bool flag2 = this.m_conf.ContainsKey("dir");
				if (flag2)
				{
					Variant variant = this.m_conf["dir"][0];
					this.direction = new Vec3(variant["x"]._float, variant["y"]._float, variant["z"]._float);
				}
				this.m_loaded = true;
			}
		}
	}
}
