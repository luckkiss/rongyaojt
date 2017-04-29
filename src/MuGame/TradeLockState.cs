using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradeLockState : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 145u;
			}
		}

		public static TradeLockState create()
		{
			return new TradeLockState();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(145u, this, this.msgData, false));
		}
	}
}
