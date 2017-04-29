using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onGetMisLineStateRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 114u;
			}
		}

		public static onGetMisLineStateRes create()
		{
			return new onGetMisLineStateRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(114u, this, this.msgData, false));
		}
	}
}
