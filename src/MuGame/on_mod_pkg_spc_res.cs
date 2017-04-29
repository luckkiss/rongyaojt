using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_mod_pkg_spc_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 37u;
			}
		}

		public static on_mod_pkg_spc_res create()
		{
			return new on_mod_pkg_spc_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(37u, this, this.msgData, false));
		}
	}
}
