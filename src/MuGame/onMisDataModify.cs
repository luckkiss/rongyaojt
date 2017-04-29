using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onMisDataModify : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 113u;
			}
		}

		public static onMisDataModify create()
		{
			return new onMisDataModify();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(113u, this, this.msgData, false));
		}
	}
}
