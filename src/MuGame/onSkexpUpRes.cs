using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onSkexpUpRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 90u;
			}
		}

		public static onSkexpUpRes create()
		{
			return new onSkexpUpRes();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"]._int == 1 || this.msgData["res"]._int == 2;
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(90u, this, this.msgData, false));
			}
		}
	}
}
