using System;

namespace MuGame
{
	internal class a3_rankingModel : ModelBase<a3_rankingModel>
	{
		public bool zhanli_frist = true;

		public bool lvl_frist = true;

		public bool chibang_frist = true;

		public bool juntuan_frist = true;

		public bool summon_frist = true;

		public float time = 0f;

		private TickItem process_3008;

		private float t = 0f;

		public void runTime(float t)
		{
			this.time = t;
			bool flag = this.process_3008 != null;
			if (flag)
			{
				TickMgr.instance.removeTick(this.process_3008);
				this.process_3008 = null;
			}
			this.process_3008 = new TickItem(new Action<float>(this.onUpdate_3008));
			TickMgr.instance.addTick(this.process_3008);
		}

		private void onUpdate_3008(float s)
		{
			this.t += s;
			bool flag = this.t >= 1f;
			if (flag)
			{
				this.time -= 1f;
				bool flag2 = a3_ranking.isshow;
				if (flag2)
				{
					a3_ranking.isshow.setTime(this.time);
				}
				this.t = 0f;
			}
			bool flag3 = this.time <= 0f;
			if (flag3)
			{
				this.zhanli_frist = true;
				this.lvl_frist = true;
				this.chibang_frist = true;
				this.juntuan_frist = true;
				this.summon_frist = true;
				BaseProxy<a3_rankingProxy>.getInstance().getTime();
				TickMgr.instance.removeTick(this.process_3008);
				this.process_3008 = null;
			}
		}
	}
}
