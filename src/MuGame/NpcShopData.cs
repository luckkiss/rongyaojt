using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class NpcShopData
	{
		public uint shop_id;

		public int npc_id;

		public string shop_name;

		public Dictionary<uint, uint> dicFloatList;

		public Dictionary<uint, uint> dicGoodsList;

		public int mapId;

		public int lastprice;

		public int nowprice;
	}
}
