using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class clanter_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 232u;
			}
		}

		public static clanter_res create()
		{
			return new clanter_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(232u, this, GameTools.CreateSwitchData("on_clanter_res", this.msgData), false));
		}
	}
}
