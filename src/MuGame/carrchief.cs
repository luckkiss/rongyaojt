using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class carrchief : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 234u;
			}
		}

		public static carrchief create()
		{
			return new carrchief();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(234u, this, GameTools.CreateSwitchData("on_carrchief_res", this.msgData), false));
		}
	}
}
