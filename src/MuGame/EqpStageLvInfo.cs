using System;
using System.Collections.Generic;

namespace MuGame
{
	internal struct EqpStageLvInfo
	{
		public Dictionary<A3_CharacterAttribute, int> equipLimit;

		public uint lv;

		public uint reincarnation;

		public Dictionary<int, int> upstageMaterials;

		public uint upstageCharge;

		public int maxAddLv;
	}
}
