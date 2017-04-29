using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class MonsterConfig : configParser
	{
		public static MonsterConfig instance;

		public MonsterConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new MonsterConfig(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			MonsterConfig.instance = this;
			bool flag = conf.ContainsKey("monsters");
			if (flag)
			{
				conf["monsters"] = GameTools.array2Map(conf["monsters"], "id", 1u);
			}
			return conf;
		}

		public Variant getMonster(string id)
		{
			return base.conf["monsters"][id];
		}
	}
}
