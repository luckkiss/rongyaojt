using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_buy_item_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 62u;
			}
		}

		public static on_buy_item_res create()
		{
			return new on_buy_item_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				bool flag2 = this.msgData.ContainsKey("gift_cid") && this.msgData["gift_cid"] > 0;
				if (flag2)
				{
				}
				bool flag3 = this.msgData.ContainsKey("yb");
				if (flag3)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_yb(this.msgData["yb"], true);
				}
				bool flag4 = this.msgData.ContainsKey("gld");
				if (flag4)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_gold(this.msgData["gld"]);
				}
				bool flag5 = this.msgData.ContainsKey("bndyb");
				if (flag5)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT.sub_bndyb(this.msgData["bndyb"]);
				}
				bool flag6 = this.msgData.ContainsKey("hexp");
				if (flag6)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.RefreshBuyHexpItms(this.msgData["itms"]);
				}
				bool flag7 = this.msgData.ContainsKey("itms");
				if (flag7)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_add_items(this.msgData["itms"], 101);
				}
				bool flag8 = this.msgData.ContainsKey("clang") && this.msgData.ContainsKey("itms");
				if (flag8)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.RefreshBuyDmktItms(this.msgData["itms"]);
				}
			}
			else
			{
				(((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI).output_server_err(this.msgData["res"]._int);
			}
		}
	}
}
