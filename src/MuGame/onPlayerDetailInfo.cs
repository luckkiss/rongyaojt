using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onPlayerDetailInfo : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 52u;
			}
		}

		public static onPlayerDetailInfo create()
		{
			return new onPlayerDetailInfo();
		}

		protected override void _onProcess()
		{
			bool flag = this.msgData["res"]._int == 1;
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(52u, this, this.msgData["pinfo"], false));
			}
			else
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(51u, this, this.msgData["res"], false));
			}
		}
	}
}
