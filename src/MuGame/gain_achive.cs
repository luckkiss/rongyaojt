using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class gain_achive : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 5u;
			}
		}

		public static gain_achive create()
		{
			return new gain_achive();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5u, this, this.msgData, false));
		}
	}
}
