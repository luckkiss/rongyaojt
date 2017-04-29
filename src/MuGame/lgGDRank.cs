using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDRank : lgGDBase
	{
		protected Dictionary<int, Variant> _rankData = new Dictionary<int, Variant>();

		public lgGDRank(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDRank(m as gameManager);
		}

		public override void init()
		{
		}

		public void setRankData(Variant msgData)
		{
		}

		public void get_rank_info(Variant data)
		{
		}

		private bool needLoadAgain(int type, int begin_idx, int end_idx)
		{
			bool flag = this._rankData[type] == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this._rankData[type]["infos"].Length < end_idx;
				if (flag2)
				{
					result = true;
				}
				else
				{
					float num = (float)(this._rankData[type]["expire_tm"] * 1000);
					float num2 = (float)(this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS;
					result = (num2 > num);
				}
			}
			return result;
		}
	}
}
