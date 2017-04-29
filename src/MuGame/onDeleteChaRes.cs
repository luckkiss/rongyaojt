using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onDeleteChaRes : TPKGMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 4u;
			}
		}

		public static TPKGMsgProcesser create()
		{
			return new onDeleteChaRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(2004u, this, this.msgData, false));
		}
	}
}
