using Cross;
using System;

namespace GameFramework
{
	public class BaseLGUI : GameEventDispatcher, IObjectPlugin
	{
		public UIClient g_mgr;

		protected string m_uiName = "";

		protected IUI m_uiF;

		protected bool m_initFlag = false;

		protected bool m_bindFlag = false;

		protected bool m_openFlag = false;

		public bool g_regEventListenerFlag = false;

		private string _controlId;

		private Variant _singleInfo;

		private string _loadingUI = "";

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

		public string uiName
		{
			get
			{
				bool flag = this.m_uiName == "";
				if (flag)
				{
					this.m_uiName = this.controlId;
				}
				return this.m_uiName;
			}
			set
			{
				this.m_uiName = value;
			}
		}

		public IUI uiF
		{
			get
			{
				return this.m_uiF;
			}
		}

		public Variant singleInfo
		{
			get
			{
				return this._singleInfo;
			}
			set
			{
				this._singleInfo = value;
			}
		}

		public bool isOpen
		{
			get
			{
				return this.m_openFlag;
			}
		}

		public BaseLGUI(UIClient m)
		{
			this.g_mgr = m;
		}

		protected virtual void _regEventListener()
		{
		}

		protected virtual void _unRegEventListener()
		{
		}

		public void regEventListener()
		{
			this.g_regEventListenerFlag = true;
			this._regEventListener();
		}

		public void unRegEventListener()
		{
			this.g_regEventListenerFlag = false;
			this._unRegEventListener();
		}

		protected virtual void _open_(Variant data)
		{
			this.m_uiF.addEventListener(4002u, new Action<GameEvent>(this.onClickClose));
			this.regEventListener();
			this.onOpen(data);
		}

		public virtual void init()
		{
		}

		protected virtual void onOpen(Variant data)
		{
			this.g_mgr.addUiBase(this.m_uiF);
			this.m_uiF.onOpen(data);
		}

		protected virtual void onClose()
		{
			this.m_uiF.onClose();
			this.g_mgr.rmUiBase(this.m_uiF);
		}

		public void open(Variant data)
		{
			bool openFlag = this.m_openFlag;
			if (!openFlag)
			{
				this.m_openFlag = true;
				bool flag = this.m_uiF == null;
				if (flag)
				{
					bool flag2 = this.g_mgr.showLoading(this);
					if (flag2)
					{
						this.onLoading();
					}
					this.initOpen(data);
				}
				else
				{
					this._open_(data);
				}
			}
		}

		public void close()
		{
			this.unRegEventListener();
			bool flag = this.m_uiF == null;
			if (!flag)
			{
				this.m_uiF.removeEventListener(4002u, new Action<GameEvent>(this.onClickClose));
				this.onClose();
				this.m_openFlag = false;
			}
		}

		public virtual void dispose()
		{
			bool flag = this.m_uiF != null;
			if (flag)
			{
				this.m_uiF.dispose();
			}
			this.g_mgr = null;
			this.m_uiF = null;
		}

		public void containerEvent(uint uiEventId, Variant data)
		{
			base.dispatchEvent(GameEvent.Create(uiEventId, this, data, false));
		}

		public void broadCastEvent(uint uiEventId, object data)
		{
			this.g_mgr.dispatchEvent(GameEvent.Create(uiEventId, this, data, false));
		}

		public virtual void onLoading()
		{
			this._loadingUI = this.uiName;
			this.g_mgr.onLoadingUI(this);
		}

		public virtual void onLoadingEnd()
		{
			bool flag = this._loadingUI != this.uiName;
			if (!flag)
			{
				this._loadingUI = "";
				this.g_mgr.onLoadingUIEnd(this);
			}
		}

		private void initOpen(Variant data)
		{
			this.bindui(new Action<IUI, Variant>(this._initOpen), data);
		}

		private void _initOpen(IUI ui, Variant info)
		{
			this.onLoadingEnd();
			bool flag = !this.m_openFlag;
			if (!flag)
			{
				this._open_(info);
			}
		}

		public void bindui(Action<IUI, Variant> cb, Variant data)
		{
			bool bindFlag = this.m_bindFlag;
			if (!bindFlag)
			{
				bool flag = this.g_mgr.showLoading(this);
				if (flag)
				{
					this.onLoading();
				}
				BaseLGUI thisptr = this;
				this.m_bindFlag = true;
				this.g_mgr.getUI(this.uiName, delegate(IUI u, Variant info)
				{
					this.onLoadingEnd();
					bool flag2 = u == null;
					if (flag2)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, " bindui [" + this.uiName + "] Err!  ");
					}
					else
					{
						this.m_uiF = u;
						this.m_uiF.setCtrl(thisptr);
						this.init();
						cb(u, info);
					}
				}, data);
			}
		}

		private void onClickClose(GameEvent e)
		{
			this.close();
		}
	}
}
