using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class BaseButton : Skin
	{
		public BtnEventTriggerListener __listener;

		private int _soundType = 1;

		private int _effectType = 1;

		private Button btn;

		private float beginScale;

		private float endScale;

		private static Action<GameObject> _handler;

		private Action<GameObject> onClickhandle;

		private Action<GameObject> onClickFalseHandle;

		public bool interactable
		{
			get
			{
				bool flag = this.btn == null;
				return !flag && this.btn.interactable;
			}
			set
			{
				bool flag = this.btn != null;
				if (flag)
				{
					this.btn.interactable = value;
				}
			}
		}

		public bool active
		{
			get
			{
				return base.gameObject.active;
			}
		}

		public static Action<GameObject> Handler
		{
			set
			{
				bool flag = BaseButton._handler != null;
				if (flag)
				{
					BaseButton._handler = value;
				}
				else
				{
					BaseButton._handler = (Action<GameObject>)Delegate.Combine(BaseButton._handler, value);
				}
			}
		}

		public Action<GameObject> onClick
		{
			get
			{
				return this.onClickhandle;
			}
			set
			{
				bool flag = value == null && this.onClickhandle != null;
				if (flag)
				{
					bool flag2 = this.__listener != null;
					if (flag2)
					{
						this.__listener.onClick = null;
					}
				}
				this.onClickhandle = value;
				this.onClickhandle = (Action<GameObject>)Delegate.Combine(this.onClickhandle, BaseButton._handler);
			}
		}

		public Action<GameObject> onClickFalse
		{
			get
			{
				return this.onClickFalseHandle;
			}
			set
			{
				this.onClickFalseHandle = value;
			}
		}

		public BaseButton(Transform trans, int effectType = 1, int soundType = 1) : base(trans)
		{
			this._soundType = soundType;
			this._effectType = effectType;
			this.beginScale = this.recTransform.localScale.x;
			this.endScale = this.beginScale * 0.92f;
			this.btn = trans.GetComponent<Button>();
			this.addEvent();
		}

		private void doClick(GameObject go)
		{
			bool flag = go.transform.GetComponent<Button>();
			if (flag)
			{
				bool flag2 = !go.transform.GetComponent<Button>().interactable;
				if (flag2)
				{
					bool flag3 = this.onClickFalseHandle != null;
					if (flag3)
					{
						this.onClickFalseHandle(base.gameObject);
					}
					return;
				}
			}
			this.doClick();
		}

		public void doClick()
		{
			bool flag = this._soundType == 1;
			if (flag)
			{
				MediaClient.instance.PlaySoundUrl("audio/common/click_button", false, null);
			}
			bool flag2 = this.onClickhandle != null;
			if (flag2)
			{
				this.onClickhandle(base.gameObject);
			}
		}

		private void doDown(GameObject go)
		{
			bool flag = go.transform.GetComponent<Button>();
			if (flag)
			{
				bool flag2 = !go.transform.GetComponent<Button>().interactable;
				if (flag2)
				{
				}
			}
		}

		private void doUp(GameObject go)
		{
			bool flag = go.transform.GetComponent<Button>();
			if (flag)
			{
				bool flag2 = !go.transform.GetComponent<Button>().interactable;
				if (flag2)
				{
				}
			}
		}

		public void removeAllListener()
		{
			bool flag = this.__listener != null;
			if (flag)
			{
				this.__listener.onDown = null;
				this.__listener.onUp = null;
				this.__listener.onExit = null;
				this.__listener.onClick = null;
			}
			this.recTransform.localScale = Vector3.one;
		}

		public void addEvent()
		{
			this.__listener = BtnEventTriggerListener.Get(base.gameObject);
			bool flag = this._effectType == 1;
			if (flag)
			{
				this.__listener.onDown = new BtnEventTriggerListener.VoidDelegate(this.doDown);
				this.__listener.onUp = new BtnEventTriggerListener.VoidDelegate(this.doUp);
				this.__listener.onExit = new BtnEventTriggerListener.VoidDelegate(this.doUp);
			}
			this.__listener.onClick = new BtnEventTriggerListener.VoidDelegate(this.doClick);
		}

		public void dispose()
		{
			bool flag = this.__listener != null;
			if (flag)
			{
				this.__listener.clearAllListener();
				this.__listener = null;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
