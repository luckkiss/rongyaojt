using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_get_mail : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 153u;
			}
		}

		public static on_get_mail create()
		{
			return new on_get_mail();
		}

		protected override void _onProcess()
		{
			LGGDMails g_mailsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_mailsCT;
			bool flag = this.msgData.ContainsKey("glbmail") && this.msgData["glbmail"] == 1;
			if (flag)
			{
				g_mailsCT.get_glbMail(this.msgData);
			}
			else
			{
				g_mailsCT.get_ptMail(this.msgData);
			}
		}
	}
}
