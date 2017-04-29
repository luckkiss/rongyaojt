using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientTriggerConf : configParser
	{
		private Variant _triggers;

		public ClientTriggerConf(ClientConfig m) : base(m)
		{
			this._triggers = new Variant();
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientTriggerConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("trigger");
			if (flag)
			{
				foreach (Variant current in conf["trigger"]._arr)
				{
					bool flag2 = !current.ContainsKey("action");
					if (!flag2)
					{
						foreach (Variant current2 in current["action"]._arr)
						{
							current2["actdata"] = current2["actdata"][0];
						}
						this._triggers[current["id"]] = current;
					}
				}
			}
			return null;
		}

		public Variant GetTriggers(uint lvl)
		{
			Variant variant = new Variant();
			foreach (Variant current in this._triggers.Values)
			{
				Variant variant2 = current["lvl"];
				bool flag = variant2;
				if (flag)
				{
					bool flag2 = variant2[0]["min"] > lvl || variant2[0]["max"] < lvl;
					if (flag2)
					{
						continue;
					}
				}
				bool flag3 = current["condition"];
				if (flag3)
				{
					foreach (Variant current2 in current["condition"].Values)
					{
						bool flag4 = current2["hasmis"];
						if (flag4)
						{
							string str = current2["hasmis"];
							Variant variant3 = GameTools.split(str, ",", 1u);
							for (int i = 0; i < variant3.Length; i++)
							{
								variant3[i] = variant3[i];
							}
							current2["hasmis"] = variant3;
						}
					}
				}
				variant._arr.Add(current);
			}
			return variant;
		}
	}
}
