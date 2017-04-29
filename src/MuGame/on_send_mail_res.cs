using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_send_mail_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 155u;
			}
		}

		public static on_send_mail_res create()
		{
			return new on_send_mail_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (!flag)
			{
				string languageText = LanguagePack.getLanguageText("mail", "sendMailFail");
				LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				lGIUIMainUI.systemmsg(GameTools.createGroup(new Variant[]
				{
					"0",
					languageText
				}), 1024u);
			}
		}
	}
}
