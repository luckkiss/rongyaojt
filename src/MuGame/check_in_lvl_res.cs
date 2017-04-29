using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class check_in_lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 240u;
			}
		}

		public static check_in_lvl_res create()
		{
			return new check_in_lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(240u, this, GameTools.CreateSwitchData("on_check_in_lvl_res", this.msgData), false));
		}
	}
}
