using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_meri_info_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 79u;
			}
		}

		public static get_meri_info_res create()
		{
			return new get_meri_info_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(79u, this, this.msgData, false));
		}
	}
}
