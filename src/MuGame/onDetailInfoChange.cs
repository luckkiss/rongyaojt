using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onDetailInfoChange : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 40u;
			}
		}

		public static onDetailInfoChange create()
		{
			return new onDetailInfoChange();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(40u, this, this.msgData, false));
		}
	}
}
