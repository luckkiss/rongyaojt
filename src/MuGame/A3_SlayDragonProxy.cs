using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_SlayDragonProxy : BaseProxy<A3_SlayDragonProxy>
	{
		public static readonly uint REFRESH = 1u;

		public static readonly uint OPEN_LVL = 2u;

		public static readonly uint END_TIME = 3u;

		private const int SEND_UNLOCK_DRAGON = 3;

		private const int SEND_ENTER_DRAGONROOM = 2;

		private const int SEND_CREATE_DRAGONROOM = 1;

		private const int SEND_GET_DRAGONINFO = 4;

		private const int SEND_TRYUNLOCK_DRAGON = 5;

		public A3_SlayDragonProxy()
		{
			this.addProxyListener(180u, new Action<Variant>(this.OnSlayDragon));
		}

		private void OnSlayDragon(Variant data)
		{
			int num = data["res"];
			debug.Log("军团屠龙消息::::" + data.dump());
			switch (num)
			{
			case 1:
				base.dispatchEvent(GameEvent.Create(A3_SlayDragonProxy.REFRESH, this, data, false));
				break;
			case 2:
				base.dispatchEvent(GameEvent.Create(A3_SlayDragonProxy.OPEN_LVL, this, data, false));
				break;
			case 3:
				base.dispatchEvent(GameEvent.Create(A3_SlayDragonProxy.END_TIME, this, data, false));
				break;
			}
		}

		public void SendCreate(uint dragonId, int diffLv)
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["ltpid"] = dragonId;
			variant["diff_lvl"] = diffLv;
			this.sendRPC(180u, variant);
		}

		public void SendGo()
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			this.sendRPC(180u, variant);
		}

		public void SendUnlock(uint dragonId)
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["lvl_id"] = dragonId;
			this.sendRPC(180u, variant);
		}

		public void SendGetData()
		{
			Variant variant = new Variant();
			variant["op"] = 4;
			this.sendRPC(180u, variant);
		}

		public void SendGive(int num = 1)
		{
			Variant variant = new Variant();
			variant["op"] = 5;
			variant["num"] = num;
			this.sendRPC(180u, variant);
		}
	}
}
