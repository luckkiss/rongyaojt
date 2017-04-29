using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onLvlMisChanged : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 116u;
			}
		}

		public static onLvlMisChanged create()
		{
			return new onLvlMisChanged();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(116u, this, this.msgData, false));
		}
	}
}
