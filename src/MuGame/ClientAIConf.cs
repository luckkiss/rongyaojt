using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientAIConf : configParser
	{
		public Variant mpRecItms
		{
			get
			{
				string str = this.m_conf["ai"]["autobattle"]["mprecitms"]["tpids"]._str;
				return GameTools.split(str, ",", 1u);
			}
		}

		public Variant hpRecItms
		{
			get
			{
				string str = this.m_conf["ai"]["autobattle"]["hprecitms"]["tpids"]._str;
				return GameTools.split(str, ",", 1u);
			}
		}

		public ClientAIConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientAIConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("ai");
			if (flag)
			{
				conf["ai"] = conf["ai"][0];
				bool flag2 = conf["ai"].ContainsKey("autobattle");
				if (flag2)
				{
					conf["ai"]["autobattle"] = conf["ai"]["autobattle"][0];
					bool flag3 = conf["ai"]["autobattle"].ContainsKey("mprecitms");
					if (flag3)
					{
						conf["ai"]["autobattle"]["mprecitms"] = conf["ai"]["autobattle"]["mprecitms"][0];
					}
					bool flag4 = conf["ai"]["autobattle"].ContainsKey("hprecitms");
					if (flag4)
					{
						conf["ai"]["autobattle"]["hprecitms"] = conf["ai"]["autobattle"]["hprecitms"][0];
					}
				}
			}
			return conf;
		}
	}
}
