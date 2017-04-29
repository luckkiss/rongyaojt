using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class CameraImpl : GraphObject3DImpl, ICamera, IGraphObject3D, IGraphObject
	{
		protected Camera m_cam;

		protected Vec4 m_backColor;

		protected bool m_background;

		protected bool m_openHdr;

		protected bool m_openTonemapping;

		protected bool m_openBloomPro;

		protected GameObject m_cube;

		protected GameObject m_sphere;

		protected bool m_createsphere = true;

		protected int m_renderOrder;

		private float m_cameraRotY;

		private Vec3 m_cameraRot;

		private Rect m_viewportRect = new Rect(0f, 0f, 0f, 0f);

		private UnityEngine.Rect m_u_viewportRect = new UnityEngine.Rect(0f, 0f, 0f, 0f);

		protected Dictionary<GameObject, GameObject> m_start = new Dictionary<GameObject, GameObject>();

		protected Dictionary<GameObject, GameObject> m_end = new Dictionary<GameObject, GameObject>();

		protected Dictionary<GameObject, Material> m_maskObjMtrl = new Dictionary<GameObject, Material>();

		protected static Vector3 h_pos = default(Vector3);

		protected static Vector3 c_pos = default(Vector3);

		protected static Material m_maskMtrl = null;

		public float fov
		{
			get
			{
				return this.m_cam.fieldOfView;
			}
			set
			{
				this.m_cam.fieldOfView = value;
			}
		}

		public float CameraRotY
		{
			get
			{
				return this.m_cameraRotY;
			}
			set
			{
				this.m_cameraRotY = value;
			}
		}

		public Vec3 CameraRot
		{
			get
			{
				return this.m_cameraRot;
			}
			set
			{
				this.m_cameraRot = value;
			}
		}

		public float near
		{
			get
			{
				return this.m_cam.nearClipPlane;
			}
			set
			{
				this.m_cam.nearClipPlane = value;
			}
		}

		public float far
		{
			get
			{
				return this.m_cam.farClipPlane;
			}
			set
			{
				this.m_cam.farClipPlane = value;
			}
		}

		public float viewWidth
		{
			get
			{
				return this.m_cam.pixelRect.width;
			}
			set
			{
				UnityEngine.Rect pixelRect = this.m_cam.pixelRect;
				pixelRect.width = value;
				this.m_cam.pixelRect = pixelRect;
			}
		}

		public float viewHeight
		{
			get
			{
				return this.m_cam.pixelRect.height;
			}
			set
			{
				UnityEngine.Rect pixelRect = this.m_cam.pixelRect;
				pixelRect.height = value;
				this.m_cam.pixelRect = pixelRect;
			}
		}

		public bool openHdr
		{
			get
			{
				return this.m_openHdr;
			}
			set
			{
				this.m_openHdr = value;
				this.m_cam.hdr = this.m_openHdr;
			}
		}

		public bool openTonemapping
		{
			get
			{
				return this.m_openTonemapping;
			}
			set
			{
				this.m_openTonemapping = value;
				bool openTonemapping = this.m_openTonemapping;
				if (openTonemapping)
				{
					this.m_u3dObj.AddComponent("Tonemapping");
				}
				else
				{
					bool flag = this.m_u3dObj.GetComponent("Tonemapping") != null;
					if (flag)
					{
						UnityEngine.Object.Destroy(this.m_u3dObj.GetComponent("Tonemapping"));
					}
				}
			}
		}

		public bool openBloomPro
		{
			get
			{
				return this.m_openBloomPro;
			}
			set
			{
				this.m_openBloomPro = value;
				bool openBloomPro = this.m_openBloomPro;
				if (openBloomPro)
				{
					this.m_u3dObj.AddComponent("BloomPro");
				}
				else
				{
					bool flag = this.m_u3dObj.GetComponent("BloomPro") != null;
					if (flag)
					{
						UnityEngine.Object.Destroy(this.m_u3dObj.GetComponent("BloomPro"));
					}
				}
			}
		}

		public bool perspective
		{
			get
			{
				return !this.m_cam.orthographic;
			}
			set
			{
				this.m_cam.orthographic = !value;
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
				bool orthographic = this.m_cam.orthographic;
				if (orthographic)
				{
					this.m_cam.orthographicSize = value;
				}
			}
		}

		public Vec4 backColor
		{
			get
			{
				return this.m_backColor;
			}
			set
			{
				this.m_backColor.set(value.x, value.y, value.z, value.w);
				this.m_cam.backgroundColor = new Color(value.x, value.y, value.z, value.w);
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
				bool background = this.m_background;
				if (background)
				{
					this.m_cam.clearFlags = CameraClearFlags.Color;
				}
				else
				{
					this.m_cam.clearFlags = CameraClearFlags.Depth;
				}
			}
		}

		public int renderOrder
		{
			get
			{
				return this.m_renderOrder;
			}
			set
			{
				this.m_renderOrder = value;
				this.m_cam.depth = (float)this.m_renderOrder;
			}
		}

		public Rect viewportRect
		{
			get
			{
				return this.m_viewportRect;
			}
			set
			{
				this.m_viewportRect.set(value.x, value.y, value.width, value.height);
				this.m_u_viewportRect.Set(value.x, value.y, value.width, value.height);
				bool flag = this.m_cam != null;
				if (flag)
				{
					this.m_cam.rect = this.m_u_viewportRect;
				}
			}
		}

		public CameraImpl()
		{
			this.m_u3dObj.name = "Camera";
			this.m_cam = this.m_u3dObj.AddComponent<Camera>();
			this.m_cam.tag = "MainCamera";
			this.m_cam.clearFlags = CameraClearFlags.Skybox;
			this.m_cam.backgroundColor = new Color(0f, 0f, 0f);
			this.m_cam.depth = 10f;
			this.m_background = true;
			this.m_backColor = new Vec4(0f, 0f, 0f, 0f);
		}

		public override void dispose()
		{
			base.dispose();
		}

		public void addLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				int cullingMask = this.m_cam.cullingMask;
				this.m_cam.cullingMask = cullingMask;
			}
		}

		public void removeLayer(int layer)
		{
			bool flag = layer < 0 || layer > 7;
			if (!flag)
			{
				int cullingMask = this.m_cam.cullingMask;
				this.m_cam.cullingMask = cullingMask;
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
				int cullingMask = this.m_cam.cullingMask;
				result = (cullingMask != 0);
			}
			return result;
		}

		public void clearLayers()
		{
			this.m_cam.cullingMask = 0;
		}

		public void lookAt(Vec3 pos)
		{
			this.m_cam.transform.LookAt(new Vector3(pos.x, pos.y, pos.z));
		}

		public Vec3 screenToWorldPoint(Vec3 vec)
		{
			Vector3 vector = this.m_cam.ScreenToWorldPoint(new Vector3(vec.x, vec.y, vec.z));
			return new Vec3(vector.x, vector.y, vector.z);
		}

		public Vec3 worldToScreenPoint(Vec3 vec)
		{
			Vector3 vector = this.m_cam.WorldToScreenPoint(new Vector3(vec.x, vec.y, vec.z));
			return new Vec3(vector.x, vector.y, vector.z);
		}

		public IGraphObject3D rayCast(Vec3 vec)
		{
			Variant variant = new Variant();
			Ray ray = this.m_cam.ScreenPointToRay(new Vector3(vec.x, vec.y, vec.z));
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(ray, out raycastHit);
			IGraphObject3D result;
			if (flag)
			{
				Object3DBehaviour component = raycastHit.transform.gameObject.GetComponent<Object3DBehaviour>();
				bool flag2 = component != null;
				if (flag2)
				{
					result = component.obj;
					return result;
				}
			}
			result = null;
			return result;
		}

		public List<IGraphObject3D> rayCastAll(Vec3 vec)
		{
			List<IGraphObject3D> list = new List<IGraphObject3D>();
			Ray ray = this.m_cam.ScreenPointToRay(new Vector3(vec.x, vec.y, vec.z));
			RaycastHit[] array = Physics.RaycastAll(ray);
			bool flag = array != null;
			if (flag)
			{
				RaycastHit[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					RaycastHit raycastHit = array2[i];
					Object3DBehaviour component = raycastHit.transform.gameObject.GetComponent<Object3DBehaviour>();
					bool flag2 = component != null;
					if (flag2)
					{
						list.Add(component.obj);
					}
				}
			}
			return list;
		}

		public void obj_mask(Vec3 heropos, Vec3 campos)
		{
			bool flag = CameraImpl.m_maskMtrl == null;
			if (flag)
			{
				CameraImpl.m_maskMtrl = new Material(Resources.Load<Shader>("mono/shaders/diffuse"));
			}
			CameraImpl.h_pos.Set(heropos.x, heropos.y, heropos.z);
			CameraImpl.c_pos.Set(campos.x, campos.y, campos.z);
			float maxDistance = Vector3.Distance(CameraImpl.h_pos, CameraImpl.c_pos) - 1f;
			RaycastHit[] array = Physics.RaycastAll(CameraImpl.c_pos, (CameraImpl.h_pos - CameraImpl.c_pos).normalized, maxDistance);
			this.m_start.Clear();
			RaycastHit[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				RaycastHit raycastHit = array2[i];
				GameObject gameObject = raycastHit.collider.gameObject;
				bool flag2 = gameObject == null || gameObject.get_renderer() == null || gameObject.get_renderer().material == null;
				if (!flag2)
				{
					Material material = gameObject.get_renderer().material;
					bool flag3 = material == CameraImpl.m_maskMtrl;
					if (!flag3)
					{
						bool flag4 = !this.m_start.ContainsKey(gameObject);
						if (flag4)
						{
							gameObject.get_renderer().material = CameraImpl.m_maskMtrl;
							bool flag5 = material.mainTexture != null;
							if (flag5)
							{
								gameObject.get_renderer().material.mainTexture = material.mainTexture;
							}
							bool flag6 = gameObject.get_renderer().material.HasProperty("_Color");
							if (flag6)
							{
								Color color = gameObject.get_renderer().material.color;
								color.a = 0.5f;
								gameObject.get_renderer().material.SetColor("_Color", color);
							}
							this.m_start[gameObject] = gameObject;
							this.m_maskObjMtrl[gameObject] = material;
						}
					}
				}
			}
			foreach (GameObject current in this.m_end.Keys)
			{
				bool flag7 = current == null || current.get_renderer() == null || current.get_renderer().material == null;
				if (!flag7)
				{
					bool flag8 = !this.m_start.ContainsKey(current);
					if (flag8)
					{
						current.get_renderer().material = this.m_maskObjMtrl[current];
						bool flag9 = current.get_renderer().material != null;
						if (flag9)
						{
							bool flag10 = current.get_renderer().material.HasProperty("_Color");
							if (flag10)
							{
								Color color2 = current.get_renderer().material.color;
								color2.a = 1f;
								current.get_renderer().material.SetColor("_Color", color2);
							}
						}
						this.m_maskObjMtrl.Remove(current);
					}
				}
			}
			this.m_end.Clear();
			foreach (GameObject current2 in this.m_start.Keys)
			{
				this.m_end[current2] = this.m_start[current2];
			}
		}
	}
}
