using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_arena : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 235u;
			}
		}

		public static on_arena create()
		{
			return new on_arena();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(235u, this, GameTools.CreateSwitchData("on_arena_res", this.msgData), false));
		}
	}
}
