using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class create_lvl_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 241u;
			}
		}

		public static create_lvl_res create()
		{
			return new create_lvl_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(241u, this, GameTools.CreateSwitchData("on_create_lvl_res", this.msgData), false));
		}
	}
}
