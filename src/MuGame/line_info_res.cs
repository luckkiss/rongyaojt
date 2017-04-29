using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class line_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 49u;
			}
		}

		public static line_info_res create()
		{
			return new line_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(49u, this, this.msgData, false));
		}
	}
}
