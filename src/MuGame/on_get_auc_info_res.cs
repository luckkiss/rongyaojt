using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_auc_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 205u;
			}
		}

		public static on_get_auc_info_res create()
		{
			return new on_get_auc_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(205u, this, this.msgData, false));
		}
	}
}
