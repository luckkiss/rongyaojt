using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_dbmkt_itm : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 94u;
			}
		}

		public static on_get_dbmkt_itm create()
		{
			return new on_get_dbmkt_itm();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(94u, this, this.msgData, false));
		}
	}
}
