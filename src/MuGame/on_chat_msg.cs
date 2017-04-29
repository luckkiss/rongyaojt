using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class on_chat_msg : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 160u;
			}
		}

		public static on_chat_msg create()
		{
			return new on_chat_msg();
		}

		protected override void _onProcess()
		{
			lgGDChat g_chatCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_chatCT;
			bool flag = g_chatCT == null;
			if (!flag)
			{
				g_chatCT.on_chat_msg(this.msgData);
			}
		}
	}
}
