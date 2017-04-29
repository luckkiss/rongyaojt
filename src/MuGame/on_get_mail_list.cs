using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_mail_list : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 152u;
			}
		}

		public static on_get_mail_list create()
		{
			return new on_get_mail_list();
		}

		protected override void _onProcess()
		{
			LGGDMails g_mailsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_mailsCT;
			bool flag = this.msgData["glbmail"] != 0;
			if (flag)
			{
				g_mailsCT.set_glbMailList(this.msgData);
			}
			else
			{
				g_mailsCT.set_ptMailList(this.msgData);
			}
		}
	}
}
