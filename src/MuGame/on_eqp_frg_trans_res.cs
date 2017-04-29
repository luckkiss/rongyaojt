using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_eqp_frg_trans_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 59u;
			}
		}

		public static on_eqp_frg_trans_res create()
		{
			return new on_eqp_frg_trans_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(59u, this, this.msgData, false));
		}
	}
}
