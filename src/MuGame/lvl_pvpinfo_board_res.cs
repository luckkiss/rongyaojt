using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_pvpinfo_board_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 237u;
			}
		}

		public static lvl_pvpinfo_board_res create()
		{
			return new lvl_pvpinfo_board_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(237u, this, GameTools.CreateSwitchData("lvl_pvpinfo_board_msg", this.msgData), false));
		}
	}
}
