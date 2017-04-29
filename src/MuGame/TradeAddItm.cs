using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeAddItm : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 144u;
			}
		}

		public static TradeAddItm create()
		{
			return new TradeAddItm();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(144u, this, this.msgData, false));
		}
	}
}
