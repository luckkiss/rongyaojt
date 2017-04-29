using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_associate_lvls_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 243u;
			}
		}

		public static get_associate_lvls_res create()
		{
			return new get_associate_lvls_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(243u, this, GameTools.CreateSwitchData("get_associate_lvls_res", this.msgData), false));
		}
	}
}
