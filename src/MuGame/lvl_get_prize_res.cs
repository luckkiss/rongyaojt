using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_get_prize_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 246u;
			}
		}

		public static lvl_get_prize_res create()
		{
			return new lvl_get_prize_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(246u, this, GameTools.CreateSwitchData("lvl_get_prize_res", this.msgData), false));
		}
	}
}
