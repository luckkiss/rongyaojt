using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class fcm_notify : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 1u;
			}
		}

		public static fcm_notify create()
		{
			return new fcm_notify();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(1u, this, this.msgData, false));
		}
	}
}
