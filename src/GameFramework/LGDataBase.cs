using Cross;
using System;

namespace GameFramework
{
	public abstract class LGDataBase : GameEventDispatcher, IObjectPlugin
	{
		private IClientBase _mgr;

		private Variant _data;

		private string _controlId;

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

		public NetClient g_mgr
		{
			get
			{
				return this._mgr as NetClient;
			}
		}

		public Variant m_data
		{
			get
			{
				bool flag = this._data == null;
				if (flag)
				{
					this._data = new Variant();
				}
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		public LGDataBase(IClientBase m)
		{
			this._mgr = m;
		}

		public abstract void init();
	}
}
