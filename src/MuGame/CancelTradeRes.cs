using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class CancelTradeRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 143u;
			}
		}

		public static CancelTradeRes create()
		{
			return new CancelTradeRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(143u, this, null, false));
		}
	}
}
