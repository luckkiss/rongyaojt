using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class on_line_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 49u;
			}
		}

		public static on_line_info_res create()
		{
			return new on_line_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(49u, this, this.msgData, false));
		}
	}
}
