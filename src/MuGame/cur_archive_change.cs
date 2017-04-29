using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class cur_archive_change : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 34u;
			}
		}

		public static cur_archive_change create()
		{
			return new cur_archive_change();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(34u, this, this.msgData, false));
		}
	}
}
