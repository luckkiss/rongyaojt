using System;
using System.Collections.Generic;

namespace Cross
{
	public class UISkin
	{
		protected Variant m_styleConf;

		protected Variant m_assetConf;

		protected Variant m_textformatConf;

		protected Variant m_prop;

		protected Variant m_eff;

		protected Dictionary<string, UISkin> m_subSkin;

		protected Dictionary<string, UISkin> m_statusSkin;

		public Variant styleConf
		{
			get
			{
				return this.m_styleConf;
			}
		}

		public Variant assetConf
		{
			get
			{
				return this.m_assetConf;
			}
		}

		public Variant textformatConf
		{
			get
			{
				return this.m_textformatConf;
			}
		}

		public Variant eff
		{
			get
			{
				return this.m_eff;
			}
		}

		public Variant prop
		{
			get
			{
				return this.m_prop;
			}
		}

		public Dictionary<string, UISkin> subSkins
		{
			get
			{
				return this.m_subSkin;
			}
		}

		public Dictionary<string, UISkin> statusSkins
		{
			get
			{
				return this.m_statusSkin;
			}
		}

		public void copyFrom(UISkin o)
		{
			this.copyFrom(o, false);
		}

		public void copyFrom(UISkin o, bool copySubSkin)
		{
			this.m_assetConf = o.assetConf;
			this.m_prop = o.prop;
			this.m_styleConf = o.m_styleConf;
			bool flag = copySubSkin && o.m_subSkin != null;
			if (flag)
			{
				this.m_subSkin = new Dictionary<string, UISkin>();
				foreach (string current in o.m_subSkin.Keys)
				{
					UISkin uISkin = new UISkin();
					uISkin.copyFrom(o.m_subSkin[current]);
					this.m_subSkin[current] = uISkin;
				}
			}
			bool flag2 = copySubSkin && o.m_statusSkin != null;
			if (flag2)
			{
				bool flag3 = this.m_statusSkin == null;
				if (flag3)
				{
					this.m_statusSkin = new Dictionary<string, UISkin>();
				}
				foreach (string current2 in o.m_statusSkin.Keys)
				{
					UISkin uISkin2 = new UISkin();
					uISkin2.copyFrom(o.m_statusSkin[current2]);
					this.m_statusSkin[current2] = uISkin2;
				}
			}
		}

		public UISkin getSubSkin(string name)
		{
			bool flag = this.m_subSkin == null;
			UISkin result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_subSkin.ContainsKey(name);
				if (flag2)
				{
					result = this.m_subSkin[name];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public UISkin getStatusSkin(string name)
		{
			bool flag = this.m_statusSkin == null;
			UISkin result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_statusSkin.ContainsKey(name);
				if (flag2)
				{
					result = this.m_statusSkin[name];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}
	}
}
