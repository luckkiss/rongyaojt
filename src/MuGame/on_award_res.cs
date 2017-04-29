using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_award_res : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 100u;
			}
		}

		public static on_award_res create()
		{
			return new on_award_res();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).lgGD_Award.AwardRes(this.msgData);
		}
	}
}
