using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onFetchGmisAwdRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 43u;
			}
		}

		public static onGetGmisRes create()
		{
			return new onGetGmisRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(43u, this, this.msgData, false));
		}
	}
}
