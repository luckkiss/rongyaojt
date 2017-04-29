using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onAbordMisRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 115u;
			}
		}

		public static onAbordMisRes create()
		{
			return new onAbordMisRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(115u, this, this.msgData, false));
		}
	}
}
