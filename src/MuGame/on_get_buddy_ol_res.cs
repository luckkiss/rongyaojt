using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_buddy_ol_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 174u;
			}
		}

		public static on_get_buddy_ol_res create()
		{
			return new on_get_buddy_ol_res();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_buddyCT.getOLBuddy(this.msgData);
		}
	}
}
