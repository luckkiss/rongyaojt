using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_err_msg : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 239u;
			}
		}

		public static lvl_err_msg create()
		{
			return new lvl_err_msg();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(239u, this, GameTools.CreateSwitchData("on_lvl_err_msg", this.msgData), false));
		}
	}
}
