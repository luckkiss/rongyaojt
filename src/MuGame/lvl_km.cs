using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_km : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 249u;
			}
		}

		public static lvl_km create()
		{
			return new lvl_km();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(249u, this, GameTools.CreateSwitchData("on_lvl_km", this.msgData), false));
		}
	}
}
