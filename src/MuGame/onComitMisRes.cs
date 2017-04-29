using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onComitMisRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 111u;
			}
		}

		public static onComitMisRes create()
		{
			return new onComitMisRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(111u, this, this.msgData, false));
		}
	}
}
