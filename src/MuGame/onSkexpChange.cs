using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onSkexpChange : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 41u;
			}
		}

		public static onSkexpChange create()
		{
			return new onSkexpChange();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(41u, this, this.msgData, false));
		}
	}
}
