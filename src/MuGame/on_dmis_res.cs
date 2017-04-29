using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_dmis_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 109u;
			}
		}

		public static on_dmis_res create()
		{
			return new on_dmis_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(109u, this, this.msgData, false));
		}
	}
}
