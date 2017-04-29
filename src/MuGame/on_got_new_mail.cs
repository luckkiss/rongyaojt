using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_got_new_mail : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 154u;
			}
		}

		public static on_got_new_mail create()
		{
			return new on_got_new_mail();
		}

		protected override void _onProcess()
		{
			LGGDMails g_mailsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_mailsCT;
			bool flag = !this.msgData.ContainsKey("mail") && !this.msgData.ContainsKey("glbmail");
			if (!flag)
			{
				bool flag2 = this.msgData.ContainsKey("glbmail");
				if (flag2)
				{
				}
			}
		}
	}
}
