using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_lock_mail_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 156u;
			}
		}

		public static on_lock_mail_res create()
		{
			return new on_lock_mail_res();
		}

		protected override void _onProcess()
		{
			LGGDMails g_mailsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_mailsCT;
			g_mailsCT.lockMail(this.msgData);
		}
	}
}
