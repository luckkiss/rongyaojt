using Cross;
using System;

namespace GameFramework
{
	public abstract class LGGRBaseImpls : GameEventDispatcher, IProcess, IObjectPlugin
	{
		private string _controlId;

		protected lgGDBase m_gameCtrl;

		protected GRBaseImpls m_drawBase;

		protected GRClient m_mgr;

		protected float m_x = 0f;

		protected float m_y = 0f;

		protected float m_z = 0f;

		public float m_xMoveto = 0f;

		public float m_yMoveto = 0f;

		public float m_zMoveto = 0f;

		protected float xReal = 0f;

		protected float yReal = 0f;

		private bool _initPos = false;

		private bool _pause = false;

		private bool _destory = false;

		private string _processName = "";

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

		public GRClient g_mgr
		{
			get
			{
				return this.m_mgr;
			}
		}

		public GRBaseImpls g_GRbase
		{
			get
			{
				return this.m_drawBase;
			}
		}

		public bool destroy
		{
			get
			{
				return this._destory;
			}
			set
			{
				this._destory = value;
			}
		}

		public bool pause
		{
			get
			{
				return this._pause;
			}
			set
			{
				this._pause = value;
			}
		}

		public string processName
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this._processName = value;
			}
		}

		public LGGRBaseImpls(GRClient m)
		{
			this.m_mgr = m;
		}

		public void setDrawBase(GRBaseImpls d)
		{
			this.m_drawBase = d;
			this.onSetDrawBase();
		}

		protected abstract void onSetDrawBase();

		public virtual void init()
		{
		}

		public void setGameCtrl(lgGDBase gct)
		{
			this.m_gameCtrl = gct;
			this.onSetGameCtrl();
		}

		protected abstract void onSetGameCtrl();

		public void dispose()
		{
			this.onDispose();
			this.m_drawBase.dispose();
			this.m_mgr = null;
			this.destroy = true;
		}

		protected virtual void onDispose()
		{
		}

		public void setPosX(float val)
		{
			this.m_x = val;
		}

		public void setPosY(float val)
		{
			this.m_y = val;
		}

		public void setPosZ(float val)
		{
			this.m_z = val;
		}

		public virtual void setPos(float x, float y)
		{
			bool flag = x == this.xReal && y == this.yReal;
			if (!flag)
			{
				Vec3 vec = this.g_mgr.trans3DPos(x, y);
				this.m_xMoveto = vec.x;
				this.m_yMoveto = vec.y;
				this.m_zMoveto = vec.z;
				this.xReal = x;
				this.yReal = y;
				bool flag2 = !this._initPos;
				if (flag2)
				{
					this._initPos = true;
					this.m_x = vec.x;
					this.m_y = vec.y;
					this.m_z = vec.z;
				}
			}
		}

		public Vec3 getPoss()
		{
			return new Vec3(this.m_x, this.m_z, this.m_y);
		}

		public virtual void updateProcess(float tmSlice)
		{
		}
	}
}
