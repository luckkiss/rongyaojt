using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class TradwReqConfirmRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 142u;
			}
		}

		public static TradwReqConfirmRes create()
		{
			return new TradwReqConfirmRes();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["confirm"];
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(142u, this, this.msgData, false));
			}
		}
	}
}
