using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_rect_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 45u;
			}
		}

		public static get_rect_res create()
		{
			return new get_rect_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(45u, this, this.msgData, false));
		}
	}
}
