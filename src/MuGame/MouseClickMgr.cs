using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MuGame
{
	internal class MouseClickMgr : GameEventDispatcher
	{
		public static uint EVENT_TOUCH_GAME_OBJECT = 1u;

		public static uint EVENT_TOUCH_UI = 2u;

		public Camera curScenceCamera;

		public static MouseClickMgr instance;

		public bool m_UpdateNearCamNow = false;

		public bool m_bTowTouchZoom = false;

		private float m_fTwoTouchDis = 0f;

		public static void init()
		{
			bool flag = MouseClickMgr.instance == null;
			if (flag)
			{
				MouseClickMgr.instance = new MouseClickMgr();
			}
		}

		public void onSelectNpc(LGAvatarNpc npc)
		{
			Variant viewInfo = npc.viewInfo;
		}

		public MouseClickMgr()
		{
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(new processStruct(new Action<float>(this.update), "MouseClickMgr", false, false), false);
		}

		public void setCurScenceCamera(Camera ca)
		{
			this.curScenceCamera = ca;
		}

		private void update(float tmSlice)
		{
			this.m_bTowTouchZoom = false;
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
			bool flag2 = flag;
			if (flag2)
			{
				float num = Input.GetAxis("Mouse ScrollWheel");
			}
			else
			{
				bool flag3 = 2 == Input.touchCount;
				if (flag3)
				{
					bool flag4 = !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId);
					if (flag4)
					{
						bool flag5 = Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved;
						if (flag5)
						{
							Vector2 vector = Input.GetTouch(0).position - Input.GetTouch(1).position;
							bool flag6 = 0f == this.m_fTwoTouchDis;
							if (flag6)
							{
								this.m_fTwoTouchDis = vector.magnitude;
							}
							else
							{
								float magnitude = vector.magnitude;
								float num = (magnitude - this.m_fTwoTouchDis) * 0.0125f;
								this.m_fTwoTouchDis = vector.magnitude;
								this.m_bTowTouchZoom = true;
							}
						}
					}
				}
				else
				{
					this.m_fTwoTouchDis = 0f;
				}
			}
			bool flag7 = Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
			if (flag7)
			{
				bool flag8 = (flag && EventSystem.current.IsPointerOverGameObject()) || (!flag && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
				if (flag8)
				{
					base.dispatchEvent(GameEvent.Create(MouseClickMgr.EVENT_TOUCH_UI, this, EventSystem.current.currentSelectedGameObject, false));
				}
				else
				{
					bool flag9 = GRMap.GAME_CAM_CAMERA != null && GRMap.GAME_CAM_CAMERA.gameObject.active;
					if (flag9)
					{
						Ray ray = GRMap.GAME_CAM_CAMERA.ScreenPointToRay(Input.mousePosition);
						RaycastHit raycastHit;
						bool flag10 = Physics.Raycast(ray, out raycastHit);
						if (flag10)
						{
							Object3DBehaviour component = raycastHit.transform.gameObject.GetComponent<Object3DBehaviour>();
							bool flag11 = component != null;
							if (flag11)
							{
								IGraphObject3D obj = component.obj;
								GREntity3D gREntity3D = obj.helper["$graphObj"] as GREntity3D;
								gREntity3D.dispathEvent(Define.EventType.RAYCASTED, null);
							}
						}
					}
					else
					{
						bool flag12 = this.curScenceCamera != null && this.curScenceCamera.gameObject.active;
						if (flag12)
						{
							Ray ray2 = this.curScenceCamera.ScreenPointToRay(Input.mousePosition);
							RaycastHit raycastHit2;
							bool flag13 = Physics.Raycast(ray2, out raycastHit2);
							if (flag13)
							{
								base.dispatchEvent(GameEvent.Create(MouseClickMgr.EVENT_TOUCH_GAME_OBJECT, this, raycastHit2.transform.gameObject, false));
							}
						}
					}
				}
			}
		}
	}
}
