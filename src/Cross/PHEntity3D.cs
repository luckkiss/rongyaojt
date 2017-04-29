using System;

namespace Cross
{
	public class PHEntity3D : IPHEntity
	{
		protected string m_id;

		protected Variant m_conf = null;

		protected PhysicsManager m_phMgr = null;

		protected IPhysicsObject3D m_obj;

		protected IPhysicsObject m_physicsobj = null;

		protected bool m_loaded = false;

		public string id
		{
			get
			{
				return this.m_id;
			}
		}

		public bool isLoaded
		{
			get
			{
				return this.m_loaded;
			}
		}

		public Variant config
		{
			get
			{
				return this.m_conf;
			}
		}

		public IPhysicsObject physicsObject
		{
			get
			{
				return this.m_physicsobj;
			}
		}

		public Vec3 pos
		{
			get
			{
				return this.m_obj.pos;
			}
			set
			{
				this.m_obj.pos = value;
			}
		}

		public float x
		{
			get
			{
				return this.m_obj.x;
			}
			set
			{
				this.m_obj.x = value;
			}
		}

		public float y
		{
			get
			{
				return this.m_obj.y;
			}
			set
			{
				this.m_obj.y = value;
			}
		}

		public float z
		{
			get
			{
				return this.m_obj.z;
			}
			set
			{
				this.m_obj.z = value;
			}
		}

		public Vec3 rot
		{
			get
			{
				return this.m_obj.rot;
			}
			set
			{
				this.m_obj.rot = value;
			}
		}

		public float rotX
		{
			get
			{
				return this.m_obj.rotX;
			}
			set
			{
				this.m_obj.rotX = value;
			}
		}

		public float rotY
		{
			get
			{
				return this.m_obj.rotY;
			}
			set
			{
				this.m_obj.rotY = value;
			}
		}

		public float rotZ
		{
			get
			{
				return this.m_obj.rotZ;
			}
			set
			{
				this.m_obj.rotZ = value;
			}
		}

		public Vec3 scale
		{
			get
			{
				return this.m_obj.scale;
			}
			set
			{
				this.m_obj.scale = value;
			}
		}

		public float scaleX
		{
			get
			{
				return this.m_obj.scaleX;
			}
			set
			{
				this.m_obj.scaleX = value;
			}
		}

		public float scaleY
		{
			get
			{
				return this.m_obj.scaleY;
			}
			set
			{
				this.m_obj.scaleY = value;
			}
		}

		public float scaleZ
		{
			get
			{
				return this.m_obj.scaleZ;
			}
			set
			{
				this.m_obj.scaleZ = value;
			}
		}

		public Vec3 axisX
		{
			get
			{
				return this.m_obj.axisX;
			}
		}

		public Vec3 axisY
		{
			get
			{
				return this.m_obj.axisY;
			}
		}

		public Vec3 axisZ
		{
			get
			{
				return this.m_obj.axisZ;
			}
		}

		public PHEntity3D(string id, PhysicsManager phyMgr)
		{
			this.m_id = id;
			this.m_phMgr = phyMgr;
		}

		public virtual Vec3 rayCast(Vec3 origin, Vec3 dir)
		{
			return null;
		}

		public virtual void load(Variant conf, Action onFin)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				bool flag2 = this.m_conf.ContainsKey("pos");
				if (flag2)
				{
					Variant variant = this.m_conf["pos"][0];
					Variant variant2 = variant["x"];
					this.m_obj.pos = new Vec3(variant["x"]._float, variant["y"]._float, variant["z"]._float);
				}
				bool flag3 = this.m_conf.ContainsKey("rot");
				if (flag3)
				{
					Variant variant3 = this.m_conf["rot"][0];
					this.m_obj.rot = new Vec3(variant3["x"]._float, variant3["y"]._float, variant3["z"]._float);
				}
				bool flag4 = this.m_conf.ContainsKey("scale");
				if (flag4)
				{
					Variant variant4 = this.m_conf["scale"][0];
					this.m_obj.scale = new Vec3(variant4["x"]._float, variant4["y"]._float, variant4["z"]._float);
				}
			}
		}

		public virtual void dispose()
		{
			this.m_obj.dispose();
		}

		public void onPreRender(float tmSlice)
		{
		}

		public void onRender(float tmSlice)
		{
		}

		public void onPostRender(float tmSlice)
		{
		}

		public void onPreProcess(float tmSlice)
		{
		}

		public void onProcess(float tmSlice)
		{
		}

		public void onPostProcess(float tmSlice)
		{
		}
	}
}
