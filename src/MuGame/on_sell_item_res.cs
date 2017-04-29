using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_sell_item_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 63u;
			}
		}

		public static on_sell_item_res create()
		{
			return new on_sell_item_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				Variant items = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_remove_items(GameTools.createArray(new Variant[]
				{
					this.msgData["id"]
				}));
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.sold_add_items(items);
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.add_gold(this.msgData["earn"]);
			}
			else
			{
				(((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI).output_server_err(this.msgData["res"]);
			}
		}
	}
}
