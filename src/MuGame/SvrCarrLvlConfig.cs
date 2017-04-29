using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class SvrCarrLvlConfig : configParser
	{
		public SvrCarrLvlConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrCarrLvlConfig(m as ClientConfig);
		}

		public Variant get_carr_lvl_desc(int carr, int lvl)
		{
			Variant result;
			foreach (Variant current in this.m_conf["carr"]._arr)
			{
				bool flag = current["carr"]._int == carr && current["lvl"]._int == lvl;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
