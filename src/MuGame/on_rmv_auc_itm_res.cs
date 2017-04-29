using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_rmv_auc_itm_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 201u;
			}
		}

		public static on_rmv_auc_itm_res create()
		{
			return new on_rmv_auc_itm_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(201u, this, this.msgData, false));
		}
	}
}
