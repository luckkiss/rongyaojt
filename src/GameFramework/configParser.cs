using Cross;
using System;

namespace GameFramework
{
	public class configParser : GameEventDispatcher, IObjectPlugin
	{
		protected Variant m_conf;

		protected string m_file;

		protected bool m_preload;

		protected bool m_isloaded;

		protected IClientBase m_mgr;

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

		public Variant conf
		{
			get
			{
				return this.m_conf;
			}
		}

		public string file
		{
			get
			{
				return this.m_file;
			}
		}

		public bool preload
		{
			get
			{
				return this.m_preload;
			}
		}

		public bool isloaded
		{
			get
			{
				return this.m_isloaded;
			}
		}

		public configParser(ClientConfig m)
		{
			this.m_mgr = m;
		}

		public virtual void init()
		{
		}

		public void initSet(string url, bool preload)
		{
			this.m_file = url;
			this.m_preload = preload;
		}

		public void loadconfig(ConfigManager confmgr, Action<configParser> onfin)
		{
			confmgr.loadExtendConfig(this.m_file, delegate(Variant conf)
			{
				this.m_conf = this._formatConfig(conf);
				this.m_isloaded = true;
				this.onData();
				onfin(this);
			});
		}

		protected virtual Variant _formatConfig(Variant conf)
		{
			return conf;
		}

		protected virtual void onData()
		{
		}
	}
}
