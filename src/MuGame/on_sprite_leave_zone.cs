using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_sprite_leave_zone : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 56u;
			}
		}

		public static on_sprite_leave_zone create()
		{
			return new on_sprite_leave_zone();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(56u, this, this.msgData, false));
		}
	}
}
