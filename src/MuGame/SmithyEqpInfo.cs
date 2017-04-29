using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class SmithyEqpInfo
	{
		public uint tpid;

		public List<MatInfo> matList = new List<MatInfo>();

		public int money;

		public int exp;

		public int forgeLvl;

		public int forgeCostId;

		public static Dictionary<int, ScrollCostData> dicScrollCost = new Dictionary<int, ScrollCostData>();
	}
}
