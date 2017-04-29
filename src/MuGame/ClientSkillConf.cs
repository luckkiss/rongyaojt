using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientSkillConf : configParser
	{
		public ClientSkillConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientSkillConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("skill");
			if (flag)
			{
				conf["skill"] = GameTools.array2Map(conf["skill"], "skid", 1u);
			}
			bool flag2 = conf.ContainsKey("state");
			if (flag2)
			{
				conf["state"] = GameTools.array2Map(conf["state"], "stateid", 1u);
			}
			bool flag3 = conf.ContainsKey("bstate");
			if (flag3)
			{
				conf["bstate"] = GameTools.array2Map(conf["bstate"], "bstateid", 1u);
			}
			return conf;
		}

		public string get_skill_icon_url(uint skid, int carr = 0)
		{
			bool flag = this.m_conf["skill"].ContainsKey(skid.ToString());
			string result;
			if (flag)
			{
				Variant variant = this.m_conf["skill"][skid.ToString()];
				bool flag2 = !variant.ContainsKey("carr");
				if (flag2)
				{
					result = variant["icon"];
					return result;
				}
				Variant variant2 = variant["carr"];
				for (int i = 0; i < variant2.Count; i++)
				{
					bool flag3 = variant2[i]["id"]._int == carr;
					if (flag3)
					{
						result = variant2[i]["icon"];
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public string get_state_icon_url(uint stateid)
		{
			bool flag = this.m_conf["state"].ContainsKey(stateid.ToString());
			string result;
			if (flag)
			{
				result = this.m_conf["state"][stateid.ToString()]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetBlessIconUrl(uint bstateid)
		{
			bool flag = this.m_conf["bstate"].ContainsKey((int)bstateid);
			string result;
			if (flag)
			{
				result = this.m_conf["bstate"][bstateid]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public Variant GetContentSkill(uint skid, uint lvl)
		{
			bool flag = this.m_conf["skill"][skid] && this.m_conf["skill"][skid]["sklvl"];
			Variant result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["skill"][skid]["sklvl"].Values)
				{
					bool flag2 = current["lvl"] == lvl;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}
	}
}
