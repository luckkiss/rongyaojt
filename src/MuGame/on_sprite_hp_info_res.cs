using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_sprite_hp_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 53u;
			}
		}

		public static on_sprite_hp_info_res create()
		{
			return new on_sprite_hp_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(53u, this, this.msgData, false));
		}
	}
}
