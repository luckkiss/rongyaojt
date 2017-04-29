using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lvl_fin : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 245u;
			}
		}

		public static lvl_fin create()
		{
			return new lvl_fin();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(245u, this, GameTools.CreateSwitchData("on_lvl_fin", this.msgData), false));
		}
	}
}
