using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_buy_sold_item_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 64u;
			}
		}

		public static on_buy_sold_item_res create()
		{
			return new on_buy_sold_item_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				Variant variant = new Variant();
				variant.pushBack(this.msgData["id"]);
				Variant items = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.sold_remove_items(variant);
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_add_items(items, 0);
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_gold(this.msgData["cost"]);
			}
			else
			{
				LGIUIMainUI lGIUIMainUI = (this.session as ClientSession).g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				lGIUIMainUI.output_server_err(this.msgData["res"]);
			}
		}
	}
}
