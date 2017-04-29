using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class line_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 4u;
			}
		}

		public static line_change create()
		{
			return new line_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(4u, this, this.msgData, false));
		}
	}
}
