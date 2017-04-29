using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onAttChange : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 26u;
			}
		}

		public static onAttChange create()
		{
			return new onAttChange();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(26u, this, this.msgData, false));
		}
	}
}
