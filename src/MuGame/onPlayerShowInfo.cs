using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onPlayerShowInfo : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 51u;
			}
		}

		public static onPlayerShowInfo create()
		{
			return new onPlayerShowInfo();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(51u, this, this.msgData, false));
		}
	}
}
