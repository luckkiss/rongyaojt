using Cross;
using System;

namespace MuGame
{
	internal class ArenaModel : ModelBase<ArenaModel>
	{
		public int lastRank;

		public int rank;

		public int reputation;

		public int surplus_time;

		public int surplus_buy;

		public int award_rank;

		public int topRank = -1;

		public void changeRank(int r)
		{
			this.lastRank = this.rank;
			this.rank = r;
		}

		public void BaseInfoInit(Variant data)
		{
			this.lastRank = (this.rank = data["myrank"]._int32);
			this.reputation = data["nobpt"]._int32;
			this.surplus_time = data["left_times"]._int32;
			this.surplus_buy = data["left_buytimes"]._int32;
			this.award_rank = data["avaliable_reward"]._int32;
			bool flag = data.ContainsKey("top_rank");
			if (flag)
			{
				this.topRank = data["top_rank"];
			}
		}

		public void SetBuyTimeCount(int num)
		{
			this.surplus_buy = num;
		}
	}
}
