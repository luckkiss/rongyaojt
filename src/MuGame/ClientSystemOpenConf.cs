using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientSystemOpenConf : configParser
	{
		public ClientSystemOpenConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientSystemOpenConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("option");
			if (flag)
			{
				Variant variant = new Variant();
				foreach (Variant current in conf["option"]._arr)
				{
					bool flag2 = current["oid"] != "";
					if (flag2)
					{
						variant[current["oid"]] = current;
					}
					bool flag3 = current.ContainsKey("tmchk");
					if (flag3)
					{
						foreach (Variant current2 in current["tmchk"]._arr)
						{
							bool flag4 = current2.ContainsKey("tb");
							if (flag4)
							{
								current2["tb"] = GameTools.GetTmchkAbs(current2["tb"]);
							}
							bool flag5 = current2.ContainsKey("te");
							if (flag5)
							{
								current2["te"] = GameTools.GetTmchkAbs(current2["te"]);
							}
						}
					}
				}
				conf = variant;
			}
			return conf;
		}

		public Variant GetSysOpenConf()
		{
			return this.m_conf;
		}
	}
}
