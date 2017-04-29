using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onGetFinedMisStateRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 112u;
			}
		}

		public static onGetFinedMisStateRes create()
		{
			return new onGetFinedMisStateRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(112u, this, this.msgData, false));
		}
	}
}
