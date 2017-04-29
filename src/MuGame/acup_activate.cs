using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class acup_activate : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 81u;
			}
		}

		public static acup_activate create()
		{
			return new acup_activate();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(81u, this, this.msgData, false));
		}
	}
}
