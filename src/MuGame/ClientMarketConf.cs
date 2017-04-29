using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientMarketConf : configParser
	{
		private bool isGetName = false;

		public ClientMarketConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientMarketConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("market");
			if (flag)
			{
				conf["market"] = GameTools.array2Map(conf["market"], "tp", 1u);
			}
			Variant variant = new Variant();
			variant["info"] = new Variant();
			variant["items"] = new Variant();
			foreach (Variant current in conf["market"].Values)
			{
				Variant variant2 = new Variant();
				variant2["id"] = current["tp"];
				variant2["name"] = current["name"];
				variant2["carr"] = current["carr"];
				variant["items"]._arr.Add(variant2);
				variant["info"]._arr.Add(current["items"]);
			}
			return variant;
		}

		public Variant getMarketConf()
		{
			bool flag = !this.isGetName;
			if (flag)
			{
				this.isGetName = true;
				foreach (Variant current in this.m_conf["items"]._arr)
				{
					current["name"] = LanguagePack.getLanguageText("market", current["name"]._str);
				}
				foreach (Variant current2 in this.m_conf["info"]._arr)
				{
					foreach (Variant current3 in current2._arr)
					{
						current3["name"] = LanguagePack.getLanguageText("market", current3["name"]._str);
					}
				}
			}
			return this.m_conf;
		}
	}
}
