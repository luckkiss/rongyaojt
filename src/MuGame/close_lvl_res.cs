using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class close_lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 248u;
			}
		}

		public static close_lvl_res create()
		{
			return new close_lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(248u, this, GameTools.CreateSwitchData("on_close_lvl_res", this.msgData), false));
		}
	}
}
