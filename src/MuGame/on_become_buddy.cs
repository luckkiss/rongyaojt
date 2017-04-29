using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_become_buddy : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 173u;
			}
		}

		public static on_become_buddy create()
		{
			return new on_become_buddy();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_buddyCT.becomeBuddy(this.msgData);
		}
	}
}
