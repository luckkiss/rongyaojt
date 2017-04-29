using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_combine_item_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 67u;
			}
		}

		public static on_combine_item_res create()
		{
			return new on_combine_item_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(67u, this, this.msgData, false));
		}
	}
}
