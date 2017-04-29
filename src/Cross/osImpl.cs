using System;
using UnityEngine;

namespace Cross
{
	public class osImpl : os
	{
		protected bool m_init = false;

		protected GameObject m_mainObj = null;

		protected static osImpl m_singleton = null;

		protected int m_screenWidth;

		protected int m_screenHeight;

		protected static Vector3 m_tmpPosVec3 = new Vector3(0f, 0f, 0f);

		protected static Vector3 m_tmpRotVec3 = new Vector3(0f, 0f, 0f);

		protected static Vector3 m_tmpScaleVec3 = new Vector3(0f, 0f, 0f);

		public static osImpl singleton
		{
			get
			{
				return osImpl.m_singleton;
			}
		}

		public int screenWidth
		{
			get
			{
				return this.m_screenWidth;
			}
		}

		public int screenHeight
		{
			get
			{
				return this.m_screenHeight;
			}
		}

		public GameObject mainU3DObj
		{
			get
			{
				return this.m_mainObj;
			}
		}

		public static bool isPC
		{
			get
			{
				return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer;
			}
		}

		public osImpl()
		{
			osImpl.m_singleton = this;
		}

		public bool init(GameObject mainObj, int width, int height)
		{
			this.m_mainObj = mainObj;
			this.m_screenWidth = width;
			this.m_screenHeight = height;
			os.material = new ShaderInterfaceImpl();
			os.sys = new SysInterfaceImpl();
			os.physics = new PhysicsInterfaceImpl();
			os.media = new MediaInterfaceImpl();
			os.net = new NetInterfaceImpl();
			os.asset = new AssetManagerImpl();
			os.urlreq = new URLReqImpl();
			bool flag = mainObj;
			if (flag)
			{
				OsBehaviour osBehaviour = mainObj.AddComponent<OsBehaviour>();
			}
			DebugTrace.print = new Action<string>(this._print);
			DebugTrace.print1 = new Action<string>(this._print1);
			Input.multiTouchEnabled = true;
			(os.asset as AssetManagerImpl).preloadShaders();
			this.m_init = true;
			this.onResize(width, height);
			return true;
		}

		public void onProcess(float tmSlice)
		{
			bool flag = !this.m_init;
			if (!flag)
			{
				(os.asset as AssetManagerImpl).onProcess(tmSlice);
				(os.sys as SysInterfaceImpl).onProcess(tmSlice);
				bool flag2 = CrossApp.singleton != null;
				if (flag2)
				{
					CrossApp.singleton.process(tmSlice);
				}
			}
		}

		public void onRender(float tmSlice)
		{
			bool flag = CrossApp.singleton != null;
			if (flag)
			{
				CrossApp.singleton.render(tmSlice);
			}
		}

		public void onResize(int width, int height)
		{
			bool flag = !this.m_init;
			if (!flag)
			{
				bool flag2 = os.graph != null;
				if (flag2)
				{
					os.graph.onResize(width, height);
				}
				bool flag3 = CrossApp.singleton != null;
				if (flag3)
				{
					CrossApp.singleton.resize(width, height);
				}
			}
		}

		public void onMouseEvent(Define.EventType evtType, int id, Define.MouseButton button, Vector3 pt, bool execute)
		{
			(os.sys as SysInterfaceImpl).onMouseEvent(evtType, id, button, pt, execute);
		}

		protected void _print(string msg)
		{
		}

		protected void _print1(string msg)
		{
			Debug.Log(msg);
		}

		public static void linkU3dObj(GameObject parent, GameObject child)
		{
			osImpl.m_tmpPosVec3.Set(child.transform.localPosition.x, child.transform.localPosition.y, child.transform.localPosition.z);
			osImpl.m_tmpRotVec3.Set(child.transform.localEulerAngles.x, child.transform.localEulerAngles.y, child.transform.localEulerAngles.z);
			osImpl.m_tmpScaleVec3.Set(child.transform.localScale.x, child.transform.localScale.y, child.transform.localScale.z);
			child.transform.parent = parent.transform;
			child.transform.localPosition = osImpl.m_tmpPosVec3;
			child.transform.localEulerAngles = osImpl.m_tmpRotVec3;
			child.transform.localScale = osImpl.m_tmpScaleVec3;
			osImpl.m_tmpPosVec3.Set(0f, 0f, 0f);
			osImpl.m_tmpRotVec3.Set(0f, 0f, 0f);
			osImpl.m_tmpScaleVec3.Set(0f, 0f, 0f);
		}
	}
}
