using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_battle_do_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 230u;
			}
		}

		public static on_battle_do_res create()
		{
			return new on_battle_do_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(230u, this, GameTools.CreateSwitchData("on_battle_do_res", this.msgData), false));
		}
	}
}
