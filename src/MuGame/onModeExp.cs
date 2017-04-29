using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onModeExp : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 61u;
			}
		}

		public static onModeExp create()
		{
			return new onModeExp();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(61u, this, this.msgData, false));
		}
	}
}
