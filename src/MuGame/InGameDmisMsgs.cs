using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class InGameDmisMsgs : MsgProcduresBase
	{
		public InGameDmisMsgs(IClientBase m) : base(m)
		{
		}

		public static InGameDmisMsgs create(IClientBase m)
		{
			return new InGameDmisMsgs(m);
		}

		public override void init()
		{
		}

		public void GetDmis(Variant data)
		{
		}
	}
}
