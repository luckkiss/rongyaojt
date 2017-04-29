using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class SvrMonsterConfig : configParser
	{
		public Variant m_monsterConfig = new Variant();

		public SvrMonsterConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrMonsterConfig(m as ClientConfig);
		}

		public Variant resolve()
		{
			bool flag = this.m_conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = base.conf.ContainsKey("mon");
				if (flag2)
				{
					for (int i = 0; i < base.conf["mon"].Count; i++)
					{
						this.m_monsterConfig[base.conf["mon"][i]["id"]._str] = base.conf["mon"][i];
					}
					result = this.m_monsterConfig;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public Variant get_monster_data(int mid)
		{
			bool flag = this.m_monsterConfig != null;
			Variant result;
			if (flag)
			{
				bool flag2 = !this.m_monsterConfig.ContainsKey(mid.ToString());
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_monsterConfig[mid.ToString()];
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		protected override Variant _formatConfig(Variant conf)
		{
			return conf;
		}

		protected override void onData()
		{
			this.resolve();
		}
	}
}
