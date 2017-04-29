using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_add_buddy_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 170u;
			}
		}

		public static on_add_buddy_res create()
		{
			return new on_add_buddy_res();
		}

		protected override void _onProcess()
		{
			lgGDBuddy g_buddyCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_buddyCT;
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				g_buddyCT.addBuddy(this.msgData);
			}
			else
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(100u, this, this.msgData["res"], false));
			}
		}
	}
}
