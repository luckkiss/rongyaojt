using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onGetSkillInfoRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 85u;
			}
		}

		public static onGetSkillInfoRes create()
		{
			return new onGetSkillInfoRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(85u, this, this.msgData, false));
		}
	}
}
