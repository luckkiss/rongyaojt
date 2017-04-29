using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_eqp_dura_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 39u;
			}
		}

		public static on_eqp_dura_change create()
		{
			return new on_eqp_dura_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(39u, this, this.msgData, false));
		}
	}
}
