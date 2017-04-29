using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class on_chat_msg_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 161u;
			}
		}

		public static on_chat_msg_res create()
		{
			return new on_chat_msg_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				lgGDChat g_chatCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_chatCT;
				g_chatCT.sendMsgSuccess();
			}
			else
			{
				LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				lGIUIMainUI.output_server_err(this.msgData["res"]);
			}
		}
	}
}
