using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_other_eqp_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 70u;
			}
		}

		public static on_other_eqp_change create()
		{
			return new on_other_eqp_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(70u, this, this.msgData, false));
		}
	}
}
