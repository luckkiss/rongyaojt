using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onSkillUPRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 87u;
			}
		}

		public static onSkillUPRes create()
		{
			return new onSkillUPRes();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(87u, this, this.msgData, false));
		}
	}
}
