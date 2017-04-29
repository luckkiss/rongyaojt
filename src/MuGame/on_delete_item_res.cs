using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_delete_item_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 71u;
			}
		}

		public static on_delete_item_res create()
		{
			return new on_delete_item_res();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_itemsCT.pkg_remove_items(GameTools.createArray(new Variant[]
			{
				this.msgData["id"]
			}));
		}
	}
}
