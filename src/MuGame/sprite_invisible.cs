using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class sprite_invisible : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 30u;
			}
		}

		public static sprite_invisible create()
		{
			return new sprite_invisible();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(30u, this, this.msgData, false));
		}
	}
}
