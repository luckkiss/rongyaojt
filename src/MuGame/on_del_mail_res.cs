using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_del_mail_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 158u;
			}
		}

		public static on_del_mail_res create()
		{
			return new on_del_mail_res();
		}

		protected override void _onProcess()
		{
			LGGDMails g_mailsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_mailsCT;
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				g_mailsCT.delMailSuccess(this.msgData["id"]);
			}
			else
			{
				string languageText = LanguagePack.getLanguageText("mail", "deleteFail");
				LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				lGIUIMainUI.systemmsg(GameTools.createGroup(new Variant[]
				{
					"0",
					languageText
				}), 1024u);
				((this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGUIMainUIImpl_NEED_REMOVE).output_server_err(this.msgData["res"]);
			}
		}
	}
}
