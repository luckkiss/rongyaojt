using System;

namespace Cross
{
	public class TZDate
	{
		protected static DateTime m_dStart = new DateTime(1970, 1, 1);

		protected DateTime m_d;

		protected double m_timestamp;

		protected static double TIME_ZONE = -(DateTime.Now - DateTime.Now.ToLocalTime()).TotalMinutes;

		public double time
		{
			get
			{
				return this.m_timestamp;
			}
			set
			{
				this.m_timestamp = value;
				this.m_d = new DateTime((long)(this.m_timestamp * 1000.0 * 10.0 + (double)TZDate.m_dStart.ToLocalTime().Ticks));
			}
		}

		public int fullYear
		{
			get
			{
				return this.m_d.Year;
			}
		}

		public int month
		{
			get
			{
				return this.m_d.Month;
			}
		}

		public int date
		{
			get
			{
				return this.m_d.Day;
			}
		}

		public int hours
		{
			get
			{
				return this.m_d.Hour;
			}
		}

		public int minutes
		{
			get
			{
				return this.m_d.Minute;
			}
		}

		public int seconds
		{
			get
			{
				return this.m_d.Second;
			}
		}

		public int milliseconds
		{
			get
			{
				return this.m_d.Millisecond;
			}
		}

		public TZDate(double timestamp = 0.0)
		{
			this.m_d = new DateTime((long)(timestamp * 1000.0 * 10.0 + (double)TZDate.m_dStart.ToLocalTime().Ticks));
			bool flag = timestamp > 0.0;
			if (flag)
			{
				this.time = timestamp;
			}
			else
			{
				this.m_timestamp = 0.0;
			}
		}

		public static TZDate createByYMDHMS(int y = 0, int m = 0, int d = 0, int h = 0, int min = 0, int s = 0, int ms = 0)
		{
			DateTime d2 = new DateTime(y, m, d, h, min, s, ms);
			double totalMilliseconds = (d2 - TZDate.m_dStart).TotalMilliseconds;
			return new TZDate(totalMilliseconds - ((d2 - d2.ToLocalTime()).TotalMinutes + TZDate.TIME_ZONE) * 60.0 * 1000.0);
		}

		public int getDay()
		{
			return (int)this.m_d.DayOfWeek;
		}

		public int getDate()
		{
			return this.m_d.Day;
		}

		public double setHours(int h, int min, int s, int ms)
		{
			double totalMilliseconds = (this.m_d - TZDate.m_dStart).TotalMilliseconds;
			this.m_d = new DateTime(this.fullYear, this.month, this.date, h, min, s);
			double num = totalMilliseconds - (this.m_d - TZDate.m_dStart).TotalMilliseconds;
			this.m_timestamp -= num;
			return this.m_timestamp;
		}
	}
}
