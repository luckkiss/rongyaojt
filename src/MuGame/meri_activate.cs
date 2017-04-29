using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class meri_activate : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 80u;
			}
		}

		public static meri_activate create()
		{
			return new meri_activate();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(80u, this, this.msgData, false));
		}
	}
}
