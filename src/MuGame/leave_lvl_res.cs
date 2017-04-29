using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class leave_lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 250u;
			}
		}

		public static leave_lvl_res create()
		{
			return new leave_lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(250u, this, GameTools.CreateSwitchData("on_leave_lvl", this.msgData), false));
		}
	}
}
