using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MuGame
{
	internal class BgItem : Skin
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly BgItem.<>c <>9 = new BgItem.<>c();

			public static Action <>9__23_1;

			internal void <show>b__23_1()
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.GETTING);
				InterfaceMgr.getInstance().close(InterfaceMgr.UPLEVEL);
			}
		}

		private Text txt;

		private RectTransform rectTxtCon;

		private RectTransform rectUp;

		private RectTransform rectDown;

		private RectTransform rectRight;

		private RectTransform rectleft;

		private RectTransform rectMask;

		private RectTransform rect;

		private Transform transCon;

		private RectTransform bg;

		private GameObject goBg;

		private GameObject goMask;

		private Transform txtBg;

		private GameObject goNext;

		private Text goNextTxt;

		public bool showing = false;

		private bool _autoClose = true;

		private Action _clickMaskHandle;

		private string _clickItemName = "";

		private int curtype = -1;

		private GameObject m_Obj;

		private GameObject m_skmesh_camera;

		public List<Vector3> lAvPos = new List<Vector3>
		{
			new Vector3(-127.74f, -1.82f, -128f),
			new Vector3(-132.7f, -1.82f, -128f)
		};

		public List<Vector3> txtPos = new List<Vector3>
		{
			new Vector3(-167f, -12f, 0f),
			new Vector3(284f, -12f, 0f)
		};

		public BgItem(Transform trans, Transform con) : base(trans)
		{
			this.transCon = con;
			this.goBg = trans.gameObject;
			this.goBg.transform.SetParent(this.transCon, false);
			this.bg = base.transform.FindChild("bg").GetComponent<RectTransform>();
			this.goMask = this.bg.transform.FindChild("mask").gameObject;
			this.rect = this.goBg.GetComponent<RectTransform>();
			this.txt = this.goBg.transform.FindChild("txt").FindChild("txt").GetComponent<Text>();
			this.txtBg = this.goBg.transform.FindChild("txt").FindChild("bg");
			this.rectTxtCon = this.goBg.transform.FindChild("txt").GetComponent<RectTransform>();
			this.goNext = this.goBg.transform.FindChild("go_next").gameObject;
			this.goNextTxt = this.goNext.transform.FindChild("txt").GetComponent<Text>();
			this.goBg.SetActive(false);
			this.goMask.SetActive(true);
			this.rectUp = this.bg.transform.FindChild("up").GetComponent<RectTransform>();
			this.rectDown = this.bg.transform.FindChild("down").GetComponent<RectTransform>();
			this.rectRight = this.bg.transform.FindChild("right").GetComponent<RectTransform>();
			this.rectleft = this.bg.transform.FindChild("left").GetComponent<RectTransform>();
			this.rectMask = this.bg.transform.FindChild("mask").GetComponent<RectTransform>();
			EventTriggerListener.Get(this.rectMask.gameObject).onClick = new EventTriggerListener.VoidDelegate(this.onMaskClick);
			EventTriggerListener.Get(this.goNext.transform.FindChild("btn_next").gameObject).onClick = new EventTriggerListener.VoidDelegate(this.onGoNext);
		}

		public void onMaskClick(GameObject go)
		{
			string clickItemName = this._clickItemName;
			GameObject gameObject = GameObject.Find(this._clickItemName);
			bool flag = gameObject == null;
			if (flag)
			{
				Debug.Log("新手模块错误：" + this._clickItemName);
			}
			bool autoClose = this._autoClose;
			if (autoClose)
			{
				this.hide();
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.VIP_UP);
			InterfaceMgr.getInstance().close(InterfaceMgr.GETTING);
			InterfaceMgr.getInstance().close(InterfaceMgr.UPLEVEL);
			bool flag2 = gameObject != null;
			if (flag2)
			{
				bool flag3 = (gameObject.name == "bt0" || gameObject.name == "bt1") && gameObject.transform.parent.gameObject.name == "combat";
				if (flag3)
				{
					ExecuteEvents.Execute<IPointerDownHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
				}
				else
				{
					ExecuteEvents.Execute<IPointerClickHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
				}
				bool flag4 = clickItemName == "10051";
				if (flag4)
				{
				}
			}
			bool flag5 = this._clickMaskHandle != null;
			if (flag5)
			{
				Action clickMaskHandle = this._clickMaskHandle;
				this._clickMaskHandle = null;
				clickMaskHandle();
			}
		}

		public void onGoNext(GameObject go)
		{
			bool autoClose = this._autoClose;
			if (autoClose)
			{
				this.hide();
			}
			bool flag = this._clickMaskHandle != null;
			if (flag)
			{
				Action clickMaskHandle = this._clickMaskHandle;
				this._clickMaskHandle = null;
				clickMaskHandle();
			}
		}

		public void show(Vector3 pos, Vector2 size, string text = "", bool force = false, string clickItemName = "", Action clickMaskHandle = null, int cameraType = 0, bool autoClose = true)
		{
			a3_task_auto.instance.stopAuto = true;
			bool flag = this.showing;
			if (!flag)
			{
				this.showing = true;
				this.goBg.SetActive(true);
				this.goNext.SetActive(false);
				this.txt.gameObject.SetActive(true);
				this.txtBg.gameObject.SetActive(true);
				this.txt.text = text;
				this.showMarkClick();
				bool first_show = NewbieModel.getInstance().first_show;
				if (first_show)
				{
					this.bg.position = pos;
					NewbieModel.getInstance().first_show = false;
				}
				else
				{
					this.bg.DOMove(pos, 0.6f, false).OnComplete(delegate
					{
						this.goMask.SetActive(true);
					});
				}
				this._clickItemName = clickItemName;
				this._autoClose = autoClose;
				this._clickMaskHandle = clickMaskHandle;
				DoAfterMgr arg_FE_0 = DoAfterMgr.instacne;
				Action arg_FE_1;
				if ((arg_FE_1 = BgItem.<>c.<>9__23_1) == null)
				{
					arg_FE_1 = (BgItem.<>c.<>9__23_1 = new Action(BgItem.<>c.<>9.<show>b__23_1));
				}
				arg_FE_0.addAfterRender(arg_FE_1);
				Vector3 localPosition = this.rectUp.localPosition;
				localPosition.y = 1000f + size.y / 2f;
				this.rectUp.localPosition = localPosition;
				localPosition = this.rectDown.localPosition;
				localPosition.y = -1000f - size.y / 2f;
				this.rectDown.localPosition = localPosition;
				localPosition = this.rectRight.localPosition;
				localPosition.x = -1000f - size.x / 2f;
				this.rectRight.localPosition = localPosition;
				Vector2 sizeDelta = new Vector2(2000f, size.y);
				this.rectRight.sizeDelta = sizeDelta;
				this.rectleft.sizeDelta = sizeDelta;
				localPosition = this.rectleft.localPosition;
				localPosition.x = 1000f + size.x / 2f;
				this.rectleft.localPosition = localPosition;
				bool flag2 = pos.x > Baselayer.halfuiWidth;
				if (flag2)
				{
					this.createAvatar(true);
				}
				else
				{
					this.createAvatar(false);
				}
			}
		}

		public void showNext(Vector3 pos, Vector2 size, string text = "", int type = 0, Action clickMaskHandle = null)
		{
			a3_task_auto.instance.stopAuto = true;
			bool flag = this.showing;
			if (!flag)
			{
				this.showing = true;
				this.goBg.SetActive(true);
				this.goNext.SetActive(true);
				this.txt.gameObject.SetActive(false);
				this.txtBg.gameObject.SetActive(false);
				this.goNextTxt.text = text;
				this.hideMarkClick();
				this._clickMaskHandle = clickMaskHandle;
				bool first_show = NewbieModel.getInstance().first_show;
				if (first_show)
				{
					this.bg.position = pos;
					NewbieModel.getInstance().first_show = false;
				}
				else
				{
					this.bg.DOMove(pos, 0.6f, false).OnComplete(delegate
					{
						this.goMask.SetActive(true);
					});
				}
				this.goNext.transform.localPosition = new Vector3(-this.rect.localPosition.x, -this.rect.localPosition.y, 0f);
				Vector3 localPosition = this.rectUp.localPosition;
				localPosition.y = 1000f + size.y / 2f;
				this.rectUp.localPosition = localPosition;
				localPosition = this.rectDown.localPosition;
				localPosition.y = -1000f - size.y / 2f;
				this.rectDown.localPosition = localPosition;
				localPosition = this.rectRight.localPosition;
				localPosition.x = -1000f - size.x / 2f;
				this.rectRight.localPosition = localPosition;
				Vector2 sizeDelta = new Vector2(2000f, size.y);
				this.rectRight.sizeDelta = sizeDelta;
				this.rectleft.sizeDelta = sizeDelta;
				localPosition = this.rectleft.localPosition;
				localPosition.x = 1000f + size.x / 2f;
				this.rectleft.localPosition = localPosition;
				bool flag2 = pos.x > Baselayer.halfuiWidth;
				if (flag2)
				{
					this.createAvatar(true);
				}
				else
				{
					this.createAvatar(false);
				}
			}
		}

		public void showTittle(string clickItemName = "", Action clickMaskHandle = null)
		{
			this._clickMaskHandle = clickMaskHandle;
			GameObject findobj = GameObject.Find(clickItemName);
			bool flag = findobj == null;
			if (flag)
			{
				Debug.Log("新手模块错误：" + this._clickItemName);
			}
			this._clickItemName = clickItemName;
			bool flag2 = findobj != null;
			if (flag2)
			{
				findobj.transform.DOScale(Vector3.one, 0.1f).OnComplete(delegate
				{
					bool flag3 = (findobj.name == "bt0" || findobj.name == "bt1") && findobj.transform.parent.gameObject.name == "combat";
					if (flag3)
					{
						ExecuteEvents.Execute<IPointerDownHandler>(findobj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
					}
					else
					{
						ExecuteEvents.Execute<IPointerClickHandler>(findobj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
					}
					bool flag4 = this._clickMaskHandle != null;
					if (flag4)
					{
						Action clickMaskHandle2 = this._clickMaskHandle;
						this._clickMaskHandle = null;
						clickMaskHandle2();
					}
				});
			}
		}

		public void showWithoutAvatar(Vector3 pos, Vector2 size, string clickItemName = "", Action clickMaskHandle = null)
		{
			a3_task_auto.instance.stopAuto = true;
			bool flag = this.showing;
			if (!flag)
			{
				this.showing = true;
				this.goBg.SetActive(true);
				this.txt.gameObject.SetActive(false);
				this.txtBg.gameObject.SetActive(false);
				this.goNext.SetActive(false);
				this.showMarkClick();
				this._clickItemName = clickItemName;
				this._clickMaskHandle = clickMaskHandle;
				bool first_show = NewbieModel.getInstance().first_show;
				if (first_show)
				{
					this.bg.position = pos;
					NewbieModel.getInstance().first_show = false;
				}
				else
				{
					this.bg.DOMove(pos, 0.6f, false).OnComplete(delegate
					{
						this.goMask.SetActive(true);
					});
				}
				this.goNext.transform.localPosition = new Vector3(-this.rect.localPosition.x, -this.rect.localPosition.y, 0f);
				Vector3 localPosition = this.rectUp.localPosition;
				localPosition.y = 1000f + size.y / 2f;
				this.rectUp.localPosition = localPosition;
				localPosition = this.rectDown.localPosition;
				localPosition.y = -1000f - size.y / 2f;
				this.rectDown.localPosition = localPosition;
				localPosition = this.rectRight.localPosition;
				localPosition.x = -1000f - size.x / 2f;
				this.rectRight.localPosition = localPosition;
				Vector2 sizeDelta = new Vector2(2000f, size.y);
				this.rectRight.sizeDelta = sizeDelta;
				this.rectleft.sizeDelta = sizeDelta;
				localPosition = this.rectleft.localPosition;
				localPosition.x = 1000f + size.x / 2f;
				this.rectleft.localPosition = localPosition;
			}
		}

		public void hide()
		{
			a3_task_auto.instance.stopAuto = false;
			bool flag = !this.showing;
			if (!flag)
			{
				this.showing = false;
				this.goBg.SetActive(false);
				this._clickItemName = "";
				this.disposeAvatar();
			}
		}

		public void hideMarkClick()
		{
			this.rectMask.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}

		public void showMarkClick()
		{
			this.rectMask.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}

		public void createAvatar(bool left)
		{
			GameObject original = U3DAPI.U3DResLoad<GameObject>("camera/newbie_camera");
			this.m_skmesh_camera = UnityEngine.Object.Instantiate<GameObject>(original);
			this.m_skmesh_camera.transform.localPosition = new Vector3(-129.7f, 1.34f, -124.98f);
			bool flag = this.m_Obj == null;
			if (flag)
			{
				GameObject original2 = Resources.Load<GameObject>("npc/125");
				this.m_Obj = UnityEngine.Object.Instantiate<GameObject>(original2);
				UnityEngine.Object.Destroy(this.m_Obj.GetComponent<NavMeshAgent>());
				if (left)
				{
					this.m_Obj.transform.position = this.lAvPos[0];
					this.rectTxtCon.localPosition = new Vector3(this.txtPos[0].x - this.rect.localPosition.x, this.txtPos[0].y - this.rect.localPosition.y + 70f);
					this.txtBg.localScale = new Vector3(-1f, 1f, 1f);
				}
				else
				{
					this.m_Obj.transform.position = this.lAvPos[1];
					this.rectTxtCon.localPosition = new Vector3(this.txtPos[1].x - this.rect.localPosition.x, this.txtPos[1].y - this.rect.localPosition.y + 70f);
					this.txtBg.localScale = new Vector3(1f, 1f, 1f);
				}
				this.m_Obj.transform.eulerAngles = Vector3.zero;
				Transform[] componentsInChildren = this.m_Obj.GetComponentsInChildren<Transform>();
				Transform[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					Transform transform = array[i];
					transform.gameObject.layer = EnumLayer.LM_DEFAULT;
				}
			}
		}

		public void disposeAvatar()
		{
			bool flag = this.m_Obj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_Obj);
			}
			bool flag2 = this.m_skmesh_camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_skmesh_camera);
			}
			this.m_Obj = null;
			this.m_skmesh_camera = null;
		}
	}
}
