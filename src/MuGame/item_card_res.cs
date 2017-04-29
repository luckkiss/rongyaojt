using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class item_card_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 23u;
			}
		}

		public static item_card_res create()
		{
			return new item_card_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(23u, this, this.msgData, false));
		}
	}
}
