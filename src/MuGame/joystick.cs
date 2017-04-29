using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class joystick : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly joystick.<>c <>9 = new joystick.<>c();

			public static EventTriggerListener.VoidDelegate <>9__24_0;

			internal void <init>b__24_0(GameObject go)
			{
				SelfRole.fsm.manBeginPos = SelfRole._inst.m_curModel.transform.position;
			}
		}

		public static joystick instance;

		private RectTransform cv;

		private Animator ani;

		private Vector3 Origin;

		private Vector3 touchOrigin;

		private Vector3 _deltaPos;

		private bool _drag = false;

		private float dis;

		private float MoveMaxDistance = 80f;

		public Vector3 MovePosiNorm;

		private float ActiveMoveDistance = 20f;

		private float true_delta_x = 0f;

		private float true_delta_y = 0f;

		public bool moveing = false;

		private GameObject stick;

		private Transform stickTrans;

		private Transform stickBgTrans;

		private Image stick_ig;

		private Image stick_igbg;

		private GameObject normal;

		private GameObject slow;

		private Text ping;

		private Vector3 result;

		[SerializeField]
		private float MaxAllowedDistance = 3f;

		public GameObject Stick
		{
			get
			{
				return this.stick;
			}
		}

		public override void init()
		{
			joystick.instance = this;
			base.alain();
			this.stick = base.getGameObjectByPath("stick");
			GameObject gameObjectByPath = base.getGameObjectByPath("touch");
			this.stickTrans = this.stick.transform;
			this.stickBgTrans = base.getGameObjectByPath("Image").transform;
			this.stick_ig = this.stickTrans.GetComponent<Image>();
			this.stick_igbg = base.transform.FindChild("Image").GetComponent<Image>();
			this.Origin = this.stickTrans.localPosition;
			this.touchOrigin = this.Origin;
			bool flag = Baselayer.cemaraRectTran == null;
			if (flag)
			{
				Baselayer.cemaraRectTran = GameObject.Find("canvas_main").GetComponent<RectTransform>();
			}
			this.cv = Baselayer.cemaraRectTran;
			this.ani = base.transform.GetComponent<Animator>();
			EventTriggerListener.Get(this.stick).onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			EventTriggerListener.Get(this.stick).onDragOut = new EventTriggerListener.VoidDelegate(this.OnDragOut);
			bool flag2 = EventTriggerListener.Get(this.stick).onDown != null;
			if (flag2)
			{
				EventTriggerListener expr_134 = EventTriggerListener.Get(this.stick);
				expr_134.onDown = (EventTriggerListener.VoidDelegate)Delegate.Combine(expr_134.onDown, new EventTriggerListener.VoidDelegate(this.OnMoveStart));
			}
			else
			{
				EventTriggerListener.Get(this.stick).onDown = new EventTriggerListener.VoidDelegate(this.OnMoveStart);
			}
			BaseProxy<MoveProxy>.getInstance();
			BaseProxy<BattleProxy>.getInstance();
			this.normal = base.transform.FindChild("normal").gameObject;
			this.slow = base.transform.FindChild("slow").gameObject;
			this.ping = base.transform.FindChild("ping").GetComponent<Text>();
			EventTriggerListener expr_1DD = EventTriggerListener.Get(this.stick);
			Delegate arg_202_0 = expr_1DD.onDown;
			EventTriggerListener.VoidDelegate arg_202_1;
			if ((arg_202_1 = joystick.<>c.<>9__24_0) == null)
			{
				arg_202_1 = (joystick.<>c.<>9__24_0 = new EventTriggerListener.VoidDelegate(joystick.<>c.<>9.<init>b__24_0));
			}
			expr_1DD.onDown = (EventTriggerListener.VoidDelegate)Delegate.Combine(arg_202_0, arg_202_1);
		}

		public void onoffAni(bool onoff)
		{
			this.ani.SetBool("onoff", onoff);
		}

		private void Update()
		{
			bool flag = !Globle.A3_DEMO;
			if (flag)
			{
				bool flag2 = muNetCleint.instance.curServerPing < 500;
				if (flag2)
				{
					this.normal.SetActive(true);
					this.slow.SetActive(false);
					this.ping.text = Globle.getColorStrByQuality("ping:" + muNetCleint.instance.curServerPing.ToString(), 2);
				}
				else
				{
					this.normal.SetActive(false);
					this.slow.SetActive(true);
					this.ping.text = Globle.getColorStrByQuality("ping:" + muNetCleint.instance.curServerPing.ToString(), 6);
				}
				bool bTowTouchZoom = MouseClickMgr.instance.m_bTowTouchZoom;
				if (bTowTouchZoom)
				{
					this.moveing = false;
				}
				bool flag3 = this.moveing;
				if (flag3)
				{
					this.dis = Vector3.Distance(new Vector3(this.touchOrigin.x + this.true_delta_x, this.touchOrigin.y + this.true_delta_y, 0f), this.touchOrigin);
					bool flag4 = this.dis >= this.MoveMaxDistance;
					if (flag4)
					{
						Vector3 localPosition = this.touchOrigin + (new Vector3(this.touchOrigin.x + this.true_delta_x, this.touchOrigin.y + this.true_delta_y, 0f) - this.touchOrigin) * this.MoveMaxDistance / this.dis;
						this.stickTrans.localPosition = localPosition;
					}
					bool flag5 = Vector3.Distance(this.stickTrans.localPosition, this.touchOrigin) > this.ActiveMoveDistance;
					if (flag5)
					{
						this.MovePosiNorm = (this.stickTrans.localPosition - this.touchOrigin).normalized;
						this.MovePosiNorm = new Vector3(this.MovePosiNorm.x, 0f, this.MovePosiNorm.y);
					}
					else
					{
						this.MovePosiNorm = Vector3.zero;
					}
					Color color = this.stick_ig.color;
					color.a = 0.5f + this.dis / this.MoveMaxDistance / 2f;
					this.stick_ig.color = color;
					this.stick_igbg.color = color;
				}
				else
				{
					Color color2 = this.stick_ig.color;
					color2.a = 0.5f;
					this.stick_ig.color = color2;
					this.stick_igbg.color = color2;
				}
			}
			else
			{
				bool flag6 = this.moveing;
				if (flag6)
				{
					this.dis = Vector3.Distance(new Vector3(this.touchOrigin.x + this.true_delta_x, this.touchOrigin.y + this.true_delta_y, 0f), this.touchOrigin);
					bool flag7 = this.dis >= this.MoveMaxDistance;
					if (flag7)
					{
						Vector3 localPosition2 = this.touchOrigin + (new Vector3(this.touchOrigin.x + this.true_delta_x, this.touchOrigin.y + this.true_delta_y, 0f) - this.touchOrigin) * this.MoveMaxDistance / this.dis;
						this.stickTrans.localPosition = localPosition2;
					}
					bool flag8 = Vector3.Distance(this.stickTrans.localPosition, this.touchOrigin) > this.ActiveMoveDistance;
					if (flag8)
					{
						this.MovePosiNorm = (this.stickTrans.localPosition - this.touchOrigin).normalized;
						this.MovePosiNorm = new Vector3(this.MovePosiNorm.x, 0f, this.MovePosiNorm.y);
					}
					else
					{
						this.MovePosiNorm = Vector3.zero;
					}
					Color color3 = this.stick_ig.color;
					color3.a = 0.5f + this.dis / this.MoveMaxDistance / 2f;
					this.stick_ig.color = color3;
					this.stick_igbg.color = color3;
					bool flag9 = this.MovePosiNorm != Vector3.zero;
					if (flag9)
					{
						bool flag10 = !SelfRole._inst.isDead;
						if (flag10)
						{
							SelfRole._inst.StartMove(this.MovePosiNorm.x, this.MovePosiNorm.z);
						}
					}
				}
				else
				{
					Color color4 = this.stick_ig.color;
					color4.a = 0.5f;
					this.stick_ig.color = color4;
					this.stick_igbg.color = color4;
				}
			}
		}

		private void MiouseDown()
		{
			bool flag = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
			if (!flag)
			{
				this.touchOrigin = this.Origin;
				this.stickTrans.localPosition = this.Origin;
			}
		}

		private Vector3 _checkPosition(Vector3 movePos, Vector3 _offsetPos)
		{
			this.result = movePos + _offsetPos;
			return this.result;
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = !this.moveing;
			if (flag)
			{
				this.OnDragOut(null);
			}
			else
			{
				bool flag2 = !this._drag;
				if (flag2)
				{
					this._drag = true;
				}
				this._deltaPos = delta;
				this.true_delta_x += this._deltaPos.x;
				this.true_delta_y += this._deltaPos.y;
				this.stickTrans.localPosition += new Vector3(this._deltaPos.x, this._deltaPos.y, 0f);
				bool autofighting = SelfRole.fsm.Autofighting;
				if (autofighting)
				{
					SelfRole.fsm.Pause();
				}
				else
				{
					SelfRole.fsm.Stop();
				}
			}
		}

		public void OnDragOut(GameObject go = null)
		{
			this.stop();
			bool autofighting = SelfRole.fsm.Autofighting;
			if (autofighting)
			{
				bool flag = SelfRole.fsm.CheckJoystickMoveDis(SelfRole._inst.m_curModel.position, this.MaxAllowedDistance);
				if (flag)
				{
					SelfRole.fsm.Stop();
				}
				else
				{
					SelfRole.fsm.Resume();
				}
			}
			else
			{
				SelfRole.fsm.Stop();
			}
		}

		public void OnDragOut_wait(float t = 0.2f)
		{
			base.CancelInvoke("timeGo");
			base.Invoke("timeGo", t);
		}

		private void timeGo()
		{
			this.OnDragOut(null);
		}

		public void stop()
		{
			this._drag = false;
			this.stickTrans.localPosition = this.Origin;
			this.stickBgTrans.localPosition = this.Origin;
			this.touchOrigin = this.Origin;
			bool flag = !Globle.A3_DEMO;
			if (flag)
			{
				lgSelfPlayer.instance.onJoystickEnd(false);
			}
			else
			{
				SelfRole._inst.StopMove();
			}
			this.moveing = false;
			this.true_delta_x = 0f;
			this.true_delta_y = 0f;
		}

		private void OnMoveStart(GameObject go)
		{
			this.moveing = true;
			worldmap.Desmapimg();
			debug.Log("I'm moving");
			skillbar.canClick = false;
			bool dartHave = BaseProxy<a3_dartproxy>.getInstance().dartHave;
			if (dartHave)
			{
				BaseProxy<a3_dartproxy>.getInstance().gotoDart = false;
				a3_liteMinimap.instance.getGameObjectByPath("goonDart").SetActive(true);
			}
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.On_Btn_Down();
			}
			UiEventCenter.getInstance().onStartMove();
		}

		private void OnDragOut1(GameObject go)
		{
			this.stopDrag();
		}

		public void stopDrag()
		{
			this._drag = false;
			this.stickTrans.localPosition = this.Origin;
			this.stickBgTrans.localPosition = this.Origin;
			this.touchOrigin = this.Origin;
			bool flag = !Globle.A3_DEMO;
			if (flag)
			{
				lgSelfPlayer.instance.onJoystickEnd(false);
			}
			else
			{
				ProfessionRole expr_5A = SelfRole._inst;
				if (expr_5A != null)
				{
					expr_5A.StopMove();
				}
			}
			this.moveing = false;
			this.true_delta_x = 0f;
			this.true_delta_y = 0f;
		}

		private void OnDrag1(GameObject go, Vector2 delta)
		{
			bool flag = !this.moveing;
			if (flag)
			{
				this.OnDragOut1(null);
			}
			else
			{
				bool flag2 = !this._drag;
				if (flag2)
				{
					this._drag = true;
				}
				this._deltaPos = delta;
				this.true_delta_x += this._deltaPos.x;
				this.true_delta_y += this._deltaPos.y;
				this.stickTrans.localPosition += new Vector3(this._deltaPos.x, this._deltaPos.y, 0f);
			}
		}

		private void OnMoveStart1(GameObject go)
		{
			Vector3 mousePosition = Input.mousePosition;
			bool flag = Input.touchCount > 1;
			if (flag)
			{
				mousePosition.x = 99999.9f;
				for (int i = 0; i < Input.touchCount; i++)
				{
					Touch touch = Input.GetTouch(i);
					bool flag2 = mousePosition.x > touch.position.x;
					if (flag2)
					{
						mousePosition.x = touch.position.x;
						mousePosition.y = touch.position.y;
					}
				}
			}
			this.moveing = true;
			this.dis = Vector3.Distance(this.stickTrans.position, mousePosition);
			Vector3 localPosition = new Vector3(mousePosition.x / (float)Screen.width * this.cv.rect.width - this.cv.rect.width / 2f - base.transform.localPosition.x, mousePosition.y / (float)Screen.height * this.cv.rect.height - this.cv.rect.height / 2f - base.transform.localPosition.y);
			this.stickBgTrans.localPosition = localPosition;
			this.stickTrans.localPosition = localPosition;
			this.touchOrigin = localPosition;
			SelfRole.fsm.Pause();
		}
	}
}
