using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class monster_spawn : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 20u;
			}
		}

		public static monster_spawn create()
		{
			return new monster_spawn();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(20u, this, this.msgData, false));
		}
	}
}
