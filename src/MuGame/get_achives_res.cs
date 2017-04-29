using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class get_achives_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 35u;
			}
		}

		public static get_achives_res create()
		{
			return new get_achives_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(35u, this, this.msgData, false));
		}
	}
}
