using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_itm_merge_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 103u;
			}
		}

		public static on_itm_merge_res create()
		{
			return new on_itm_merge_res();
		}

		protected override void _onProcess()
		{
			LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.OnItemMergeRes(this.msgData);
			}
			else
			{
				lGIUIMainUI.output_server_err(this.msgData["res"]);
			}
		}
	}
}
