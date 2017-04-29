using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class onLvlUp : RPCMsgProcesser
	{
		public override uint msgID
		{
			get
			{
				return 60u;
			}
		}

		public static onLvlUp create()
		{
			return new onLvlUp();
		}

		protected override void _onProcess()
		{
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5019u, this, this.msgData, false));
			bool flag = this.msgData.ContainsKey("pinfo");
			if (flag)
			{
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5022u, this, null, false));
				(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5021u, this, null, false));
			}
			Variant variant = new Variant();
			(this.session as ClientSession).g_mgr.dispatchEvent(GameEvent.Create(5023u, this, this.msgData["lvlshare"], false));
		}
	}
}
