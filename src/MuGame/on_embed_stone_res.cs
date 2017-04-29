using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_embed_stone_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 73u;
			}
		}

		public static on_embed_stone_res create()
		{
			return new on_embed_stone_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(73u, this, this.msgData, false));
		}
	}
}
