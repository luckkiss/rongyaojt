using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_vip_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 46u;
			}
		}

		public static get_vip_res create()
		{
			return new get_vip_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(46u, this, this.msgData, false));
		}
	}
}
