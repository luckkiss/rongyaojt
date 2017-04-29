using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeErrMsg : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 146u;
			}
		}

		public static TradeErrMsg create()
		{
			return new TradeErrMsg();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(146u, this, this.msgData["res"], false));
		}
	}
}
