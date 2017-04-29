using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_trans_repo_itm_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 33u;
			}
		}

		public static on_trans_repo_itm_res create()
		{
			return new on_trans_repo_itm_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(33u, this, this.msgData, false));
		}
	}
}
