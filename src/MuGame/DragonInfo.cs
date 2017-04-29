using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class DragonInfo
	{
		public uint clan_lv;

		public uint item_id;

		public int item_cost;

		public int diff_lvl;

		public int level_min;

		public int pre_min;

		public List<RewardInfo> rewardList = new List<RewardInfo>();

		public Dictionary<uint, string> dragonName;
	}
}
