using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class pk_v_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 3u;
			}
		}

		public static pk_v_change create()
		{
			return new pk_v_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(3u, this, this.msgData, false));
		}
	}
}
