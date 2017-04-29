using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientGuideConf : configParser
	{
		private Variant _guides;

		private Variant _uiConf;

		public ClientGuideConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientGuideConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("guide");
			if (flag)
			{
				this._guides = GameTools.array2Map(conf["guide"], "id", 1u);
			}
			bool flag2 = conf.ContainsKey("ui");
			if (flag2)
			{
				this._uiConf = conf["ui"][0];
			}
			return null;
		}

		public Variant GetGuideConf(string id)
		{
			return this._guides[id];
		}

		public Variant GetGuideUIConf()
		{
			return this._uiConf;
		}
	}
}
