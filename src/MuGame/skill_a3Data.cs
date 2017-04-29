using System;

namespace MuGame
{
	internal class skill_a3Data
	{
		public static uint itemId;

		public int max_lvl;

		public int skill_id;

		public int carr;

		public string skill_name;

		public int item_num;

		public float action_tm;

		public string des;

		public int now_lv;

		public int open_zhuan;

		public int open_lvl;

		public SXML xml;

		public int targetNum;

		public int range;

		public int skillType;

		public int skillType2;

		public float eff_last;

		public long endCD = 0L;

		public uint cd
		{
			get
			{
				bool flag = this.now_lv == 0;
				uint result;
				if (flag)
				{
					result = this.xml.GetNode("skill_att", "skill_lv==1").getUint("cd") * 100u;
				}
				else
				{
					result = this.xml.GetNode("skill_att", "skill_lv==" + this.now_lv).getUint("cd") * 100u;
				}
				return result;
			}
		}

		public int mp
		{
			get
			{
				bool s_bStandaloneScene = SelfRole.s_bStandaloneScene;
				int result;
				if (s_bStandaloneScene)
				{
					result = 0;
				}
				else
				{
					result = this.xml.GetNode("skill_att", "skill_lv==" + this.now_lv).getInt("mp");
				}
				return result;
			}
		}

		public int cdTime
		{
			get
			{
				long curServerTimeStampMS = muNetCleint.instance.CurServerTimeStampMS;
				bool flag = this.endCD < curServerTimeStampMS;
				int result;
				if (flag)
				{
					this.endCD = 0L;
					result = 0;
				}
				else
				{
					result = (int)(this.endCD - curServerTimeStampMS);
				}
				return result;
			}
		}

		public void doCD()
		{
			long num = muNetCleint.instance.CurServerTimeStampMS + (long)((ulong)this.cd);
			bool flag = this.endCD < num;
			if (flag)
			{
				this.endCD = num;
			}
		}
	}
}
