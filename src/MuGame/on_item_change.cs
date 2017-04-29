using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_item_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 75u;
			}
		}

		public static on_item_change create()
		{
			return new on_item_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(75u, this, this.msgData, false));
		}
	}
}
