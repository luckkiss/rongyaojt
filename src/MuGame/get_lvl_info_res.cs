using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_lvl_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 244u;
			}
		}

		public static get_lvl_info_res create()
		{
			return new get_lvl_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(244u, this, GameTools.CreateSwitchData("get_lvl_info_res", this.msgData), false));
		}
	}
}
