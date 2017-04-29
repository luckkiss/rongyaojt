using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_buy_auc_itm_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 202u;
			}
		}

		public static on_buy_auc_itm_res create()
		{
			return new on_buy_auc_itm_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(202u, this, this.msgData, false));
		}
	}
}
