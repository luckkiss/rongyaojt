using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_side_info : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 247u;
			}
		}

		public static lvl_side_info create()
		{
			return new lvl_side_info();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(247u, this, GameTools.CreateSwitchData("on_lvl_side_info", this.msgData), false));
		}
	}
}
