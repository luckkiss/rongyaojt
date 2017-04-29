using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeDone : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 147u;
			}
		}

		public static TradeDone create()
		{
			return new TradeDone();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(147u, this, this.msgData, false));
		}
	}
}
