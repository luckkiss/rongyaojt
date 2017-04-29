using System;
using System.Collections.Generic;

namespace MuGame
{
	public class MapData
	{
		public int id;

		public int mapid;

		public string name;

		public int groupId;

		public int type;

		public int relive_type;

		public string plot;

		public int pvp_type;

		public int lv_limit;

		public int fighting;

		public int energy;

		public int lv_uplimit;

		public int stage_group;

		public int ui_group;

		public int return_map_id;

		public int return_map_x;

		public int return_map_y;

		public int time;

		public List<MapItemData> lItem;

		public long enterTime;

		public int cycleCount;

		private int killNum;

		public int limit_tm;

		public int total_enemyNum;

		public int bossId;

		public int money;

		public int exp;

		public int starNum;

		public int count;

		public int resetCount;

		public Action<int> OnKillNumChange;

		public int kmNum
		{
			get
			{
				return this.killNum;
			}
			set
			{
				this.killNum = value;
				bool flag = this.OnKillNumChange != null;
				if (flag)
				{
					this.OnKillNumChange(this.killNum);
				}
			}
		}

		public int getCanResetCount()
		{
			int value = VipMgr.getValue(VipMgr.PLOT_STAGE_RESET);
			bool flag = this.resetCount > value;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = value - this.resetCount;
			}
			return result;
		}
	}
}
