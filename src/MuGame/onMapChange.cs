using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onMapChange : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 58u;
			}
		}

		public static onMapChange create()
		{
			return new onMapChange();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(58u, this, this.msgData, false));
		}
	}
}
