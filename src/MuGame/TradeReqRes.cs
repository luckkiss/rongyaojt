using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeReqRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 140u;
			}
		}

		public static TradeReqRes create()
		{
			return new TradeReqRes();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] != 1;
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(140u, this, this.msgData["res"], false));
			}
		}
	}
}
