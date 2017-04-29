using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class SkillConf : configParser
	{
		public static SkillConf instance;

		public SkillConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SkillConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			SkillConf.instance = this;
			bool flag = conf.ContainsKey("skill");
			if (flag)
			{
				conf["skill"] = GameTools.array2Map(conf["skill"], "id", 1u);
				Variant variant = conf["skill"];
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current.ContainsKey("Level");
					if (flag2)
					{
						current["Level"] = GameTools.array2Map(current["Level"], "level", 1u);
					}
				}
			}
			return conf;
		}

		public Variant getSkillid(string id)
		{
			return base.conf["skill"][id];
		}
	}
}
