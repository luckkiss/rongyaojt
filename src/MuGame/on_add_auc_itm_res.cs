using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_add_auc_itm_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 200u;
			}
		}

		public static on_add_auc_itm_res create()
		{
			return new on_add_auc_itm_res();
		}

		protected override void _onProcess()
		{
			Variant variant = new Variant();
			variant.pushBack(this.msgData["itmid"]);
			lgGDItems g_itemsCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT;
			lgGDGeneral g_generalCT = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_generalCT;
			bool flag = this.msgData.ContainsKey("gld_cost");
			if (flag)
			{
				g_generalCT.sub_gold(this.msgData["gld_cost"]);
				g_itemsCT.set_gold((uint)g_generalCT.gold);
			}
			bool flag2 = this.msgData.ContainsKey("yb_cost");
			if (flag2)
			{
				g_generalCT.sub_yb(this.msgData["yb_cost"], true);
				g_itemsCT.set_yb((uint)g_generalCT.yb);
			}
			bool flag3 = this.msgData.ContainsKey("readd") && this.msgData["readd"]._bool;
			if (flag3)
			{
				((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_vendorCT.ReaddVendorItem(this.msgData);
				g_itemsCT.pshop_updexpire_items(variant);
			}
			else
			{
				g_itemsCT.pshop_add_items(this.msgData);
			}
		}
	}
}
