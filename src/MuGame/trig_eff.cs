using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class trig_eff : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 7u;
			}
		}

		public static trig_eff create()
		{
			return new trig_eff();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(7u, this, this.msgData, false));
		}
	}
}
