using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientOutGameConf : configParser
	{
		public ClientOutGameConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientOutGameConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("createPro");
			if (flag)
			{
				conf["createPro"] = GameTools.array2Map(conf["createPro"], "tp", 1u);
			}
			bool flag2 = conf.ContainsKey("ranName");
			if (flag2)
			{
				conf["ranName"] = GameTools.array2Map(conf["ranName"], "tp", 1u);
			}
			return conf;
		}

		public Variant get_createPro_conf()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["createPro"];
			}
			return result;
		}

		public Variant get_ranName_conf()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_conf["ranName"];
			}
			return result;
		}

		public int gerRanNameType()
		{
			bool flag = this.m_conf == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = this.m_conf["ranName"][0]["ranType"][0]["type"];
			}
			return result;
		}

		public Variant getFirstNameArr()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string str = this.m_conf["ranName"]["0"]["firstName"][0]["value"];
				Variant variant = GameTools.split(str, ",", 1u);
				result = variant;
			}
			return result;
		}

		public Variant getLastNameArr(int carr, int sex)
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["ranName"]["0"]["lastName"];
				Variant variant2 = new Variant();
				foreach (Variant current in variant._arr)
				{
					bool flag2 = carr == current["carr"] && sex == current["sex"];
					if (flag2)
					{
						variant2 = current;
						break;
					}
				}
				bool flag3 = variant2 == null;
				if (flag3)
				{
					variant2 = variant[0];
				}
				string str = variant2["value"];
				Variant variant3 = GameTools.split(str, ",", 1u);
				result = variant3;
			}
			return result;
		}
	}
}
