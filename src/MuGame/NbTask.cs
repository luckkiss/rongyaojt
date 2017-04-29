using GameFramework;
using System;

namespace MuGame
{
	internal class NbTask : NewbieTeachItem
	{
		public static NbTask create(string[] arr)
		{
			return new NbTask();
		}

		public override void addListener()
		{
			BaseProxy<A3_TaskProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(base.onHanlde));
		}

		public override void removeListener()
		{
			BaseProxy<A3_TaskProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(base.onHanlde));
		}
	}
}
