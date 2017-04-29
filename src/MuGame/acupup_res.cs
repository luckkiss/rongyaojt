using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class acupup_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 83u;
			}
		}

		public static acupup_res create()
		{
			return new acupup_res();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(83u, this, this.msgData, false));
		}
	}
}
