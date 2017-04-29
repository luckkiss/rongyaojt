using System;

namespace Cross
{
	public class GRLight3D : GREntity3D
	{
		protected ILight m_light;

		protected bool m_enabled = false;

		protected Vec4 m_color = new Vec4();

		protected float m_intensity = 0f;

		protected bool m_shadow = false;

		protected int m_layer = 0;

		public bool enabled
		{
			get
			{
				return this.m_light.enabled;
			}
			set
			{
				this.m_enabled = value;
				bool flag = this.m_light != null;
				if (flag)
				{
					this.m_light.enabled = value;
				}
			}
		}

		public Vec4 color
		{
			get
			{
				return this.m_light.color;
			}
			set
			{
				this.m_color = value;
				bool flag = this.m_light != null;
				if (flag)
				{
					this.m_light.color = value;
				}
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
				this.m_intensity = value;
				bool flag = this.m_light != null;
				if (flag)
				{
					this.m_light.intensity = value;
				}
			}
		}

		public bool isCastShadow
		{
			get
			{
				return this.m_light.shadow;
			}
			set
			{
				this.m_shadow = value;
				bool flag = this.m_light != null;
				if (flag)
				{
					this.m_light.shadow = value;
				}
			}
		}

		public override int layer
		{
			get
			{
				return this.m_light.layer;
			}
			set
			{
				this.m_layer = value;
				bool flag = this.m_light != null;
				if (flag)
				{
					this.m_light.layer = value;
				}
			}
		}

		public GRLight3D(string id, GRWorld3D world) : base(id, world)
		{
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				base.load(conf, null, null);
				bool flag2 = this.m_conf.ContainsKey("enabled");
				if (flag2)
				{
					this.m_light.enabled = (this.m_conf["enabled"]._str == "enabled");
				}
				else
				{
					this.m_light.enabled = true;
				}
				bool flag3 = this.m_conf.ContainsKey("pos");
				if (flag3)
				{
					Variant variant = this.m_conf["pos"][0];
					this.m_light.pos = new Vec3(variant["x"]._float, variant["y"]._float, variant["z"]._float);
				}
				bool flag4 = this.m_conf.ContainsKey("rot");
				if (flag4)
				{
					Variant variant2 = this.m_conf["rot"][0];
					this.m_light.rot = new Vec3(variant2["x"]._float, variant2["y"]._float, variant2["z"]._float);
				}
				bool flag5 = this.m_conf.ContainsKey("scale");
				if (flag5)
				{
					Variant variant3 = this.m_conf["scale"][0];
					this.m_light.scale = new Vec3(variant3["x"]._float, variant3["y"]._float, variant3["z"]._float);
				}
				bool flag6 = this.m_conf.ContainsKey("color");
				if (flag6)
				{
					Variant variant4 = this.m_conf["color"][0];
					this.m_light.color = new Vec4(variant4["r"]._float / 255f, variant4["g"]._float / 255f, variant4["b"]._float / 255f, 1f);
				}
				bool flag7 = this.m_conf.ContainsKey("intensity");
				if (flag7)
				{
					Variant variant5 = this.m_conf["intensity"][0];
					this.m_light.intensity = variant5["val"]._float;
				}
				bool flag8 = this.m_conf.ContainsKey("shadow");
				if (flag8)
				{
					Variant variant6 = this.m_conf["shadow"][0];
					this.m_light.shadow = (variant6["enabled"]._str == "true");
				}
				this.m_loaded = true;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_light != null;
			if (flag)
			{
				this.m_light.dispose();
				this.m_light = null;
			}
			base.dispose();
			this.m_loaded = false;
		}

		public void addLayer(int layer)
		{
			this.m_light.addLayer(layer);
		}

		public void removeLayer(int layer)
		{
			this.m_light.removeLayer(layer);
		}

		public void clearLayers()
		{
			this.m_light.clearLayers();
		}
	}
}
