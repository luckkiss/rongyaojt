using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class pk_state_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 0u;
			}
		}

		public static pk_state_change create()
		{
			return new pk_state_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(0u, this, this.msgData, false));
		}
	}
}
