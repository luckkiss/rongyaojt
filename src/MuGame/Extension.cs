using System;
using System.Collections.Generic;

namespace MuGame
{
	public static class Extension
	{
		public static void BroadCast(this Dictionary<ELITE_MONSTER_PAGE_IDX, Action<bool>> fn, ELITE_MONSTER_PAGE_IDX idx)
		{
			int i = 0;
			List<ELITE_MONSTER_PAGE_IDX> list = new List<ELITE_MONSTER_PAGE_IDX>(fn.Keys);
			while (i < fn.Count)
			{
				fn[list[i]](idx == list[i]);
				i++;
			}
		}
	}
}
