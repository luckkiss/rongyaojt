using System;

namespace MuGame
{
	internal class NbDelay : NewbieTeachItem
	{
		public static double sec;

		public static NbDelay create(string[] arr)
		{
			NbDelay result = new NbDelay();
			NbDelay.sec = double.Parse(arr[1]) * 1000.0;
			return result;
		}

		public override void addListener()
		{
			ConfigUtil.SetTimeout(NbDelay.sec, new Action(this.doHandle));
		}

		public void doHandle()
		{
			base.onHanlde(null);
		}
	}
}
