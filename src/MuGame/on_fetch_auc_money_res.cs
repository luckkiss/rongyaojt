using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_fetch_auc_money_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 204u;
			}
		}

		public static on_fetch_auc_money_res create()
		{
			return new on_fetch_auc_money_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(204u, this, this.msgData, false));
		}
	}
}
