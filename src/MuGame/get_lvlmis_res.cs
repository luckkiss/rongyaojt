using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_lvlmis_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 117u;
			}
		}

		public static get_lvlmis_res create()
		{
			return new get_lvlmis_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(117u, this, GameTools.CreateSwitchData("get_lvlmis_res", this.msgData), false));
		}
	}
}
