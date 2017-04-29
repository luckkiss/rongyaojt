using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class on_achieve_res : RPCMsgProcesser
	{
		protected bool _isInit = false;

		public override uint msgID
		{
			get
			{
				return 106u;
			}
		}

		public static on_achieve_res create()
		{
			return new on_achieve_res();
		}

		protected override void _onProcess()
		{
			bool isInit = this._isInit;
			if (isInit)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(106u, this, this.msgData, false));
			}
			else
			{
				this._isInit = true;
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(106u, this, this.msgData, false));
			}
		}
	}
}
