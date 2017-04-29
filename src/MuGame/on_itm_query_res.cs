using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_itm_query_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 104u;
			}
		}

		public static on_itm_query_res create()
		{
			return new on_itm_query_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(104u, this, this.msgData, false));
		}
	}
}
