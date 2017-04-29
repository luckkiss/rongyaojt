using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientSystemConf : configParser
	{
		private Variant _defaultSet;

		private Variant _autoSet;

		private Variant _systemdata;

		public ClientSystemConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientSystemConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("defaultSet");
			if (flag)
			{
				conf["defaultSet"] = conf["defaultSet"][0];
			}
			bool flag2 = conf.ContainsKey("autoSet");
			if (flag2)
			{
				conf["autoSet"] = conf["autoSet"][0];
			}
			bool flag3 = conf.ContainsKey("system");
			if (flag3)
			{
				conf["system"] = conf["system"][0];
			}
			return conf;
		}

		public Variant defaultSet()
		{
			bool flag = this._defaultSet == null;
			if (flag)
			{
				this._defaultSet = new Variant();
				foreach (string current in this.m_conf["defaultSet"].Keys)
				{
					this._defaultSet[current] = this.m_conf["defaultSet"][current][0]["val"];
				}
			}
			return this._defaultSet;
		}

		public Variant autoSet()
		{
			bool flag = this._autoSet == null;
			if (flag)
			{
				this._autoSet = new Variant();
				foreach (string current in this.m_conf["autoSet"].Keys)
				{
					this._autoSet[current] = this.m_conf["autoSet"][current][0]["val"];
				}
			}
			return this._autoSet;
		}

		public Variant system()
		{
			bool flag = this._systemdata == null;
			if (flag)
			{
				this._systemdata = new Variant();
				foreach (string current in this.m_conf["system"].Keys)
				{
					this._systemdata[current] = this.m_conf["system"][current][0]["val"];
				}
			}
			return this._systemdata;
		}
	}
}
