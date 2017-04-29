using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_use_uitem_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 68u;
			}
		}

		public static on_use_uitem_res create()
		{
			return new on_use_uitem_res();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"] == 1;
			if (flag)
			{
				Variant variant = ((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_get_item_by_id(this.msgData["id"]);
				Variant variant2 = ((this.session as ClientSession).g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(variant["tpid"]);
				bool flag2 = variant2["conf"].ContainsKey("cdtp");
				if (flag2)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.touch_cd(variant2["conf"]["cdtp"], (double)(this.msgData["cdtm"] / 10));
				}
				else
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.touch_cd(0u, 0.0);
				}
				bool flag3 = this.msgData.ContainsKey("cnt") && this.msgData["cnt"] > 0;
				if (flag3)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_mod_item_cnt(this.msgData["id"], this.msgData["cnt"], 0);
				}
				else
				{
					Variant variant3 = new Variant();
					variant3.pushBack(this.msgData["id"]);
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_remove_items(variant3);
				}
				LGIUIItems lGIUIItems = ((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("LGUIItemImpl") as LGIUIItems;
				int cdtp = 0;
				bool flag4 = variant2["conf"].ContainsKey("cdtp");
				if (flag4)
				{
					cdtp = variant2["conf"]["cdtp"]._int;
				}
				lGIUIItems.UseItemSuccess(this.msgData["id"], cdtp, (float)(this.msgData["cdtm"] / 10));
				LGIUIMessageBox lGIUIMessageBox = ((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("msgbox") as LGIUIMessageBox;
				lGIUIMessageBox.RefreshUplvlgift();
				bool flag5 = this.msgData.ContainsKey("pkgs");
				if (flag5)
				{
					((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.PkgsUseItem(this.msgData);
				}
			}
			else
			{
				(((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI).output_server_err(this.msgData["res"]);
				LGIUIItems lGIUIItems2 = ((this.session as ClientSession).g_mgr.g_uiM as muUIClient).getLGUI("LGUIItemImpl") as LGIUIItems;
				lGIUIItems2.UseItemError(this.msgData["res"]);
			}
		}
	}
}
