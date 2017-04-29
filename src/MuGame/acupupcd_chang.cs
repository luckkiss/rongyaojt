using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class acupupcd_chang : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 82u;
			}
		}

		public static acupupcd_chang create()
		{
			return new acupupcd_chang();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(82u, this, this.msgData, false));
		}
	}
}
