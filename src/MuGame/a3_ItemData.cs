using System;

namespace MuGame
{
	public struct a3_ItemData
	{
		public uint tpid;

		public string file;

		public string borderfile;

		public string item_name;

		public string desc;

		public string desc2;

		public int maxnum;

		public int quality;

		public int value;

		public int use_type;

		public int sortType;

		public int use_lv;

		public int use_limit;

		public int intensify_score;

		public int item_type;

		public int equip_type;

		private int equipLevel;

		public int job_limit;

		public int modelId;

		public int on_sale;

		public int cd_type;

		public float cd_time;

		public int main_effect;

		public int add_basiclevel;

		public int use_sum_require;

		public int equip_level
		{
			get
			{
				return (this.equipLevel <= 0) ? 1 : this.equipLevel;
			}
			set
			{
				this.equipLevel = value;
			}
		}
	}
}
