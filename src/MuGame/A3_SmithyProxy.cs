using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class A3_SmithyProxy : BaseProxy<A3_SmithyProxy>
	{
		public static readonly uint ON_SMITHYOPT = 1u;

		public static readonly uint ON_SMITHYEXPCHANGE = 2u;

		public static readonly uint ON_SMITHYDATACHANGED = 3u;

		public A3_SmithyProxy()
		{
			this.addProxyListener(45u, new Action<Variant>(this.OnSmithyOpt));
		}

		private void OnSmithyOpt(Variant v)
		{
			debug.Log("铁匠铺协议:::::" + v.dump());
			int num = v["res"];
			int num2 = num;
			if (num2 != 1)
			{
				bool flag = num < 0;
				if (flag)
				{
					flytxt.instance.fly(ContMgr.getError(v["res"]), 0, default(Color), null);
				}
			}
			else
			{
				base.dispatchEvent(GameEvent.Create(A3_SmithyProxy.ON_SMITHYDATACHANGED, this, v, false));
			}
		}

		public void SendMake(uint tpid, uint way = 1u, int num = 1)
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["id"] = tpid;
			variant["way"] = way;
			variant["num"] = num;
			debug.Log("发送打造装备消息" + variant.dump());
			this.sendRPC(28u, variant);
		}

		public void SendMakeByScroll(int num = 1)
		{
			Variant variant = new Variant();
			variant["op"] = 4;
			variant["num"] = num;
			debug.Log("发送卷轴打造装备消息" + variant.dump());
			this.sendRPC(28u, variant);
		}

		public void SendChooseLearn(uint partId)
		{
			debug.Log("发送学习专精消息");
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["type"] = partId;
			this.sendRPC(28u, variant);
		}

		public void SendRefresh()
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			this.sendRPC(28u, variant);
		}

		public void SendRelearn(int type, int costWay)
		{
			Variant variant = new Variant();
			variant["op"] = 5;
			variant["cost"] = costWay;
			variant["type"] = type;
			debug.Log("发送重学消息:" + variant.dump());
			this.sendRPC(28u, variant);
		}
	}
}
