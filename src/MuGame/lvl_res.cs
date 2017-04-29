using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 233u;
			}
		}

		public static lvl_res create()
		{
			return new lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(233u, this, GameTools.CreateSwitchData("on_lvl_res", this.msgData), false));
		}
	}
}
