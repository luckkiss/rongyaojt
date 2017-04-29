using System;

namespace Cross
{
	public class CCTime
	{
		protected static DateTime _TimestampStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

		public static long getTickMillisec()
		{
			return DateTime.Now.Ticks / 10000L;
		}

		public static int getCurTimestamp()
		{
			return (int)(DateTime.Now - CCTime._TimestampStartTime).TotalSeconds;
		}

		public static long getCurTimestampMS()
		{
			return (long)(DateTime.Now - CCTime._TimestampStartTime).TotalMilliseconds;
		}
	}
}
