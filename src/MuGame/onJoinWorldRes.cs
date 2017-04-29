using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onJoinWorldRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 50u;
			}
		}

		public static onJoinWorldRes create()
		{
			return new onJoinWorldRes();
		}

		protected override void _onProcess()
		{
			debug.Log(">>>>>>>>>>>>>>>onJoinWorldRes::" + this.msgData.dump());
			ModelBase<PlayerModel>.getInstance().init(this.msgData);
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(50u, this, this.msgData, false));
		}
	}
}
