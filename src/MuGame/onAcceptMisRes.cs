using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onAcceptMisRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 110u;
			}
		}

		public static onAcceptMisRes create()
		{
			return new onAcceptMisRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(110u, this, this.msgData["mis"], false));
		}
	}
}
