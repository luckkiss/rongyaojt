using System;
using System.Collections.Generic;

namespace Cross
{
	public class GREntity3D : IGREntity
	{
		protected GRWorld3D m_world = null;

		protected string m_id = null;

		protected Variant m_conf = null;

		protected IContainer3D m_rootObj = null;

		protected Dictionary<Define.EventType, Action<Event>> m_eventFunc = new Dictionary<Define.EventType, Action<Event>>();

		protected bool m_loaded = false;

		public bool visible
		{
			get
			{
				return this.m_rootObj.visible;
			}
			set
			{
				this.m_rootObj.visible = value;
			}
		}

		public IGRWorld world
		{
			get
			{
				return this.m_world;
			}
		}

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

		public IContainer3D rootObj
		{
			get
			{
				return this.m_rootObj;
			}
		}

		public virtual uint updateFlag
		{
			get
			{
				return 0u;
			}
		}

		public IGraphObject graphObject
		{
			get
			{
				return this.m_rootObj;
			}
		}

		public Vec3 pos
		{
			get
			{
				return this.m_rootObj.pos;
			}
			set
			{
				this.m_rootObj.pos = value;
			}
		}

		public float x
		{
			get
			{
				return this.m_rootObj.x;
			}
			set
			{
				this.m_rootObj.x = value;
			}
		}

		public float y
		{
			get
			{
				return this.m_rootObj.y;
			}
			set
			{
				this.m_rootObj.y = value;
			}
		}

		public float z
		{
			get
			{
				return this.m_rootObj.z;
			}
			set
			{
				this.m_rootObj.z = value;
			}
		}

		public Vec3 rot
		{
			get
			{
				return this.m_rootObj.rot;
			}
			set
			{
				this.m_rootObj.rot = value;
			}
		}

		public float rotX
		{
			get
			{
				return this.m_rootObj.rotX;
			}
			set
			{
				this.m_rootObj.rotX = value;
			}
		}

		public float rotY
		{
			get
			{
				return this.m_rootObj.rotY;
			}
			set
			{
				this.m_rootObj.rotY = value;
			}
		}

		public string name
		{
			get
			{
				return this.m_rootObj.name;
			}
			set
			{
				this.m_rootObj.name = value;
			}
		}

		public float rotZ
		{
			get
			{
				return this.m_rootObj.rotZ;
			}
			set
			{
				this.m_rootObj.rotZ = value;
			}
		}

		public Vec3 scale
		{
			get
			{
				return this.m_rootObj.scale;
			}
			set
			{
				this.m_rootObj.scale = value;
			}
		}

		public float scaleX
		{
			get
			{
				return this.m_rootObj.scaleX;
			}
			set
			{
				this.m_rootObj.scaleX = value;
			}
		}

		public float scaleY
		{
			get
			{
				return this.m_rootObj.scaleY;
			}
			set
			{
				this.m_rootObj.scaleY = value;
			}
		}

		public float scaleZ
		{
			get
			{
				return this.m_rootObj.scaleZ;
			}
			set
			{
				this.m_rootObj.scaleZ = value;
			}
		}

		public Vec3 axisX
		{
			get
			{
				return this.m_rootObj.axisX;
			}
		}

		public Vec3 axisY
		{
			get
			{
				return this.m_rootObj.axisY;
			}
		}

		public Vec3 axisZ
		{
			get
			{
				return this.m_rootObj.axisZ;
			}
		}

		public virtual int layer
		{
			get
			{
				return this.m_rootObj.layer;
			}
			set
			{
				this.m_rootObj.layer = value;
			}
		}

		public GREntity3D(string id, GRWorld3D world)
		{
			this.m_world = world;
			this.m_id = id;
			this.m_rootObj = this.m_world.scene3d.createContainer3D();
		}

		public virtual void load(Variant conf, IShader mtrl = null, Action onFin = null)
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
					this.m_rootObj.pos = new Vec3(variant["x"]._float, variant["y"]._float, variant["z"]._float);
				}
				bool flag3 = this.m_conf.ContainsKey("rot");
				if (flag3)
				{
					Variant variant3 = this.m_conf["rot"][0];
					this.m_rootObj.rot = new Vec3(variant3["x"]._float, variant3["y"]._float, variant3["z"]._float);
				}
				bool flag4 = this.m_conf.ContainsKey("scale");
				if (flag4)
				{
					Variant variant4 = this.m_conf["scale"][0];
					this.m_rootObj.scale = new Vec3(variant4["x"]._float, variant4["y"]._float, variant4["z"]._float);
				}
			}
		}

		public virtual void dispose()
		{
			this.m_rootObj.dispose();
		}

		public void addScript(string objname, string name)
		{
			this.m_rootObj.addScript(objname, name);
		}

		public void addenent(float tim, Action act)
		{
			this.m_rootObj.addevent(tim, act);
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

		public virtual void addEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Combine(eventFunc[eventType], cbFunc);
				}
				else
				{
					this.m_eventFunc[eventType] = cbFunc;
				}
			}
		}

		public virtual void removeEventListener(Define.EventType eventType, Action<Event> cbFunc)
		{
			bool flag = cbFunc == null;
			if (!flag)
			{
				bool flag2 = this.m_eventFunc.ContainsKey(eventType);
				if (flag2)
				{
					Dictionary<Define.EventType, Action<Event>> eventFunc = this.m_eventFunc;
					eventFunc[eventType] = (Action<Event>)Delegate.Remove(eventFunc[eventType], cbFunc);
				}
			}
		}

		public virtual void clearAllEventListeners()
		{
			this.m_eventFunc.Clear();
		}

		public virtual void dispathEvent(Define.EventType eventType, Event evt)
		{
			bool flag = this.m_eventFunc.ContainsKey(eventType);
			if (flag)
			{
				this.m_eventFunc[eventType](evt);
			}
		}
	}
}
