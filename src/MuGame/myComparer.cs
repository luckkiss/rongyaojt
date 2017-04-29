using System;
using System.Collections.Generic;

namespace MuGame
{
	public class myComparer : IComparer<A3_LegionData>
	{
		public int Compare(A3_LegionData y, A3_LegionData x)
		{
			int num = x.lvl.CompareTo(y.lvl);
			bool flag = num == 0;
			if (flag)
			{
				num = x.combpt.CompareTo(y.combpt);
				bool flag2 = num == 0;
				if (flag2)
				{
					num = x.huoyue.CompareTo(y.huoyue);
					bool flag3 = num == 0;
					if (flag3)
					{
						num = x.plycnt.CompareTo(y.plycnt);
					}
				}
			}
			return num;
		}
	}
}
