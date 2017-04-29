using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class mod_lvl_selfpvpinfo : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 238u;
			}
		}

		public static mod_lvl_selfpvpinfo create()
		{
			return new mod_lvl_selfpvpinfo();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(238u, this, GameTools.CreateSwitchData("mod_lvl_selfpvpinfo", this.msgData), false));
		}
	}
}
