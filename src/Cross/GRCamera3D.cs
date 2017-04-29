using System;
using System.Collections.Generic;

namespace Cross
{
	public class GRCamera3D : GREntity3D
	{
		protected ICamera m_cam;

		protected bool m_perspective = false;

		protected bool m_background = false;

		public override int layer
		{
			get
			{
				return this.m_cam.layer;
			}
			set
			{
				this.m_cam.layer = value;
			}
		}

		public ICamera Camera
		{
			get
			{
				return this.m_cam;
			}
		}

		public bool openHdr
		{
			get
			{
				return this.m_cam.openHdr;
			}
			set
			{
				this.m_cam.openHdr = value;
			}
		}

		public bool openTonemapping
		{
			get
			{
				return this.m_cam.openTonemapping;
			}
			set
			{
				this.m_cam.openTonemapping = value;
			}
		}

		public bool openBloomPro
		{
			get
			{
				return this.m_cam.openBloomPro;
			}
			set
			{
				this.m_cam.openBloomPro = value;
			}
		}

		public float fov
		{
			get
			{
				return this.m_cam.fov;
			}
			set
			{
				this.m_cam.fov = value;
			}
		}

		public float near
		{
			get
			{
				return this.m_cam.near;
			}
			set
			{
				this.m_cam.near = value;
			}
		}

		public float far
		{
			get
			{
				return this.m_cam.far;
			}
			set
			{
				this.m_cam.far = value;
			}
		}

		public float viewWidth
		{
			get
			{
				return this.m_cam.viewWidth;
			}
			set
			{
				this.m_cam.viewWidth = value;
			}
		}

		public float viewHeight
		{
			get
			{
				return this.m_cam.viewHeight;
			}
			set
			{
				this.m_cam.viewHeight = value;
			}
		}

		public bool perspective
		{
			get
			{
				return this.m_perspective;
			}
			set
			{
				this.m_perspective = value;
				this.m_cam.perspective = this.m_perspective;
			}
		}

		public float orthographicSize
		{
			get
			{
				return this.m_cam.orthographicSize;
			}
			set
			{
				this.m_cam.orthographicSize = value;
			}
		}

		public bool background
		{
			get
			{
				return this.m_background;
			}
			set
			{
				this.m_background = value;
				this.m_cam.background = this.m_background;
			}
		}

		public int renderOrder
		{
			get
			{
				return this.m_cam.renderOrder;
			}
			set
			{
				this.m_cam.renderOrder = value;
			}
		}

		public Vec4 backColor
		{
			get
			{
				return this.m_cam.backColor;
			}
			set
			{
				this.m_cam.backColor = value;
			}
		}

		public Rect viewportRect
		{
			get
			{
				return this.m_cam.viewportRect;
			}
			set
			{
				this.m_cam.viewportRect = value;
			}
		}

		public GRCamera3D(string id, GRWorld3D world) : base(id, world)
		{
			this.m_cam = this.m_world.scene3d.createCamera();
			this.m_rootObj.addChild(this.m_cam);
		}

		public void addLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				this.m_cam.addLayer(layer);
			}
		}

		public void removeLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				this.m_cam.removeLayer(layer);
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
				this.m_cam.hasLayer(layer);
				result = false;
			}
			return result;
		}

		public void clearLayers()
		{
			bool flag = this.layer < 0 || this.layer > 7;
			if (!flag)
			{
				this.m_cam.clearLayers();
			}
		}

		public void lookAt(Vec3 pos)
		{
			this.m_cam.lookAt(pos);
		}

		public Vec3 screenToWorldPoint(Vec3 vec)
		{
			return this.m_cam.screenToWorldPoint(vec);
		}

		public Vec3 worldToScreenPoint(Vec3 vec)
		{
			return this.m_cam.worldToScreenPoint(vec);
		}

		public GREntity3D rayCast(Vec3 vec)
		{
			IGraphObject3D graphObject3D = this.m_cam.rayCast(vec);
			bool flag = graphObject3D != null && graphObject3D.helper.ContainsKey("$graphObj");
			GREntity3D result;
			if (flag)
			{
				GREntity3D gREntity3D = graphObject3D.helper["$graphObj"] as GREntity3D;
				gREntity3D.dispathEvent(Define.EventType.RAYCASTED, null);
				result = gREntity3D;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public List<GREntity3D> rayCastAll(Vec3 vec)
		{
			List<GREntity3D> list = new List<GREntity3D>();
			List<IGraphObject3D> list2 = this.m_cam.rayCastAll(vec);
			foreach (IGraphObject3D current in list2)
			{
				bool flag = current.helper.ContainsKey("$graphObj");
				if (flag)
				{
					GREntity3D gREntity3D = current.helper["$graphObj"] as GREntity3D;
					gREntity3D.dispathEvent(Define.EventType.RAYCASTED, null);
					list.Add(gREntity3D);
				}
			}
			return list;
		}

		public void obj_mask(Vec3 h_pos, Vec3 cam_pos)
		{
			this.m_cam.obj_mask(h_pos, cam_pos);
		}
	}
}
