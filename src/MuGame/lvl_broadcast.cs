using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_broadcast : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 236u;
			}
		}

		public static lvl_broadcast create()
		{
			return new lvl_broadcast();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(236u, this, GameTools.CreateSwitchData("on_lvl_broadcast_res", this.msgData), false));
		}
	}
}
