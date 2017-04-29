using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_item_droped : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 76u;
			}
		}

		public static on_item_droped create()
		{
			return new on_item_droped();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(76u, this, this.msgData, false));
		}
	}
}
