using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_olawd_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 36u;
			}
		}

		public static on_get_olawd_res create()
		{
			return new on_get_olawd_res();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_olawardCT.setOlAwardInfo(this.msgData);
		}
	}
}
