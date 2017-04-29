using System;

namespace GameFramework
{
	public class lgGDBase : GameEventDispatcher, IObjectPlugin
	{
		private gameManager _mgr;

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

		public virtual gameManager g_mgr
		{
			get
			{
				return this._mgr;
			}
		}

		public virtual void initGr(GRBaseImpls grBase, LGGRBaseImpls lggrbase)
		{
		}

		public lgGDBase(gameManager m)
		{
			this._mgr = m;
		}

		public virtual void init()
		{
		}
	}
}
