using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onGetUserCidRes : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 251u;
			}
		}

		public static onGetUserCidRes create()
		{
			return new onGetUserCidRes();
		}

		protected override void _onProcess()
		{
			((this.session as ClientSession).g_mgr.g_gameM as muLGClient).g_MgrPlayerInfoCT.on_get_user_cid_res(this.msgData);
		}
	}
}
