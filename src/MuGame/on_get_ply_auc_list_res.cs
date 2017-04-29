using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_ply_auc_list_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 203u;
			}
		}

		public static on_get_ply_auc_list_res create()
		{
			return new on_get_ply_auc_list_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(203u, this, this.msgData, false));
		}
	}
}
