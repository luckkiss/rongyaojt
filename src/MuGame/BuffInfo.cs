using System;

namespace MuGame
{
	internal class BuffInfo
	{
		public uint id;

		public uint par;

		public uint start_time;

		public uint end_time;

		public bool isfristShow = true;

		public long endCD = 0L;

		public string icon;

		public string name;

		public BUFF_TYPE buff_type = BUFF_TYPE.NULL;

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
			long num = muNetCleint.instance.CurServerTimeStampMS + (long)((ulong)(this.end_time - this.start_time));
			bool flag = this.endCD < num;
			if (flag)
			{
				this.endCD = num;
			}
		}

		public void update(long timestp)
		{
			bool flag = this.endCD == 0L;
			if (!flag)
			{
				bool flag2 = this.endCD < timestp;
				if (flag2)
				{
					this.endCD = 0L;
				}
			}
		}
	}
}
