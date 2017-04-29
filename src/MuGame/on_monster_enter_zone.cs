using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_monster_enter_zone : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 55u;
			}
		}

		public static on_monster_enter_zone create()
		{
			return new on_monster_enter_zone();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(55u, this, this.msgData, false));
		}
	}
}
