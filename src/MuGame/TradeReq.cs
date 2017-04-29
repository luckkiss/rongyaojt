using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeReq : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 141u;
			}
		}

		public static TradeReq create()
		{
			return new TradeReq();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(141u, this, this.msgData, false));
		}
	}
}
