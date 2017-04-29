using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_add_self_lottery_log : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 162u;
			}
		}

		public static on_add_self_lottery_log create()
		{
			return new on_add_self_lottery_log();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(162u, this, GameTools.CreateSwitchData("setSelfLogData", this.msgData), false));
		}
	}
}
