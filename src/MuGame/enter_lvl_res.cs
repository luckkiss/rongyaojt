using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class enter_lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 242u;
			}
		}

		public static enter_lvl_res create()
		{
			return new enter_lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(242u, this, GameTools.CreateSwitchData("on_enter_lvl_res", this.msgData), false));
		}
	}
}
