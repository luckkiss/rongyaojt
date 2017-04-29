using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onCreateChaRes : TPKGMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 5u;
			}
		}

		public static TPKGMsgProcesser create()
		{
			return new onCreateChaRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(2005u, this, this.msgData, false));
		}
	}
}
