using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_other_lottery_log : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 163u;
			}
		}

		public static on_other_lottery_log create()
		{
			return new on_other_lottery_log();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(163u, this, this.msgData, false));
		}
	}
}
