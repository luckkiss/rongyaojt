using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class UIbase : GameEventDispatcher, IUI, IGameEventDispatcher, IUIBase, IObjectPlugin
	{
		private string _controlId;

		public Variant userdata;

		protected Variant _openData;

		protected BaseLGUI _ctrl;

		protected IUIBaseControl _win;

		public UIClient g_mgr;

		protected Dictionary<string, Action<IUIBaseControl, Event>> _eventReceiver = new Dictionary<string, Action<IUIBaseControl, Event>>();

		public Dictionary<string, Action<IUIBaseControl, Event>> eventReceiver
		{
			get
			{
				return this._eventReceiver;
			}
		}

		public string controlId
		{
			get
			{
				return this._controlId;
			}
			set
			{
				this._controlId = value;
			}
		}

		public IUIBaseControl control
		{
			get
			{
				return this._win;
			}
		}

		public UIbase(UIClient m)
		{
			this.g_mgr = m;
			this._eventReceiver["onMouseClkBtnClose"] = new Action<IUIBaseControl, Event>(this.onMouseClkBtnClose);
		}

		public virtual void init()
		{
		}

		public void onOpen(Variant data)
		{
			this._openData = data;
			this._onOpen();
		}

		public void onClose()
		{
			this._onClose();
		}

		protected virtual void _onOpen()
		{
		}

		protected virtual void _onClose()
		{
		}

		public virtual void dispose()
		{
			bool flag = this._win != null;
			if (flag)
			{
				this._win.dispose();
			}
			this._eventReceiver = null;
			this.g_mgr = null;
			this._ctrl = null;
		}

		public void setCtrl(BaseLGUI ctrl)
		{
			this._ctrl = ctrl;
			this.onSetCtrl();
		}

		public BaseLGUI getCtrl()
		{
			return this._ctrl;
		}

		protected void eventAct(uint uiEventId, Variant data)
		{
			base.dispatchEvent(GameEvent.Create(uiEventId, this, data, false));
		}

		protected virtual void onSetCtrl()
		{
		}

		public void setBaseCtrl(IUIBaseControl ui, bool clickBack = false)
		{
			this._win = ui;
			if (clickBack)
			{
				ui.addEventListener(Define.EventType.MOUSE_DOWN, new Action<IUIBaseControl, Event>(this.clickBackground));
			}
			this._bindControl(this._win);
			this.onSetBaseCtrl();
			this.init();
		}

		private void clickBackground(IUIBaseControl ui, Event e)
		{
			this.onClickBackground();
		}

		protected virtual void onClickBackground()
		{
		}

		protected virtual void onSetBaseCtrl()
		{
		}

		protected virtual void _initControl(Dictionary<string, IUIBaseControl> host)
		{
			bool flag = host.ContainsKey("mainframe");
			if (flag)
			{
				(host["mainframe"] as IUIImageBox).mouseEnabled = true;
			}
		}

		protected void _bindControl(IUIBaseControl ctrl)
		{
			Dictionary<string, IUIBaseControl> dictionary = new Dictionary<string, IUIBaseControl>();
			bool flag = ctrl != null && ctrl is IUIContainer;
			if (flag)
			{
				this._bindChildControls(dictionary, ctrl);
			}
			this._initControl(dictionary);
		}

		protected void _bindChildControls(Dictionary<string, IUIBaseControl> ctrlHost, IUIBaseControl ctrl)
		{
			IUIContainer iUIContainer = ctrl as IUIContainer;
			bool flag = iUIContainer == null;
			if (!flag)
			{
				for (int i = 0; i < iUIContainer.numChildren; i++)
				{
					IUIBaseControl child = iUIContainer.getChild(i);
					bool flag2 = ctrlHost.ContainsKey(child.id);
					if (!flag2)
					{
						ctrlHost[child.id] = child;
						this._bindChildControls(ctrlHost, child);
					}
				}
			}
		}

		public IUIBaseControl getBaseCtrl()
		{
			return this._win;
		}

		public virtual void onMouseClkBtnClose(IUIBaseControl ui, Event evt)
		{
			this.getCtrl().close();
		}
	}
}
