using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_ol_tm_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 44u;
			}
		}

		public static get_ol_tm_res create()
		{
			return new get_ol_tm_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(44u, this, this.msgData, false));
		}
	}
}
