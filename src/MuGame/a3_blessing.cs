using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class a3_blessing : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_blessing.<>c <>9 = new a3_blessing.<>c();

			public static Action<GameObject> <>9__0_0;

			public static Action<GameObject> <>9__0_1;

			public static Action<GameObject> <>9__0_2;

			internal void <init>b__0_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_BLESSING);
			}

			internal void <init>b__0_1(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_BLESSING);
			}

			internal void <init>b__0_2(GameObject go)
			{
				int @int = SvrLevelConfig.instacne.get_level_data(MapModel.getInstance().curLevelId)["buy_state"][0]["state"][0]["yb_cost"]._int;
				bool flag = (long)@int > (long)((ulong)ModelBase<PlayerModel>.getInstance().gold);
				if (flag)
				{
					flytxt.instance.fly(ContMgr.getError("-1001"), 0, default(Color), null);
				}
				else
				{
					BaseProxy<A3_ActiveProxy>.getInstance().SendGetBlessing();
				}
			}
		}

		public override void init()
		{
			BaseButton arg_37_0 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			Action<GameObject> arg_37_1;
			if ((arg_37_1 = a3_blessing.<>c.<>9__0_0) == null)
			{
				arg_37_1 = (a3_blessing.<>c.<>9__0_0 = new Action<GameObject>(a3_blessing.<>c.<>9.<init>b__0_0));
			}
			arg_37_0.onClick = arg_37_1;
			BaseButton arg_73_0 = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			Action<GameObject> arg_73_1;
			if ((arg_73_1 = a3_blessing.<>c.<>9__0_1) == null)
			{
				arg_73_1 = (a3_blessing.<>c.<>9__0_1 = new Action<GameObject>(a3_blessing.<>c.<>9.<init>b__0_1));
			}
			arg_73_0.onClick = arg_73_1;
			BaseButton arg_AF_0 = new BaseButton(base.transform.FindChild("do"), 1, 1);
			Action<GameObject> arg_AF_1;
			if ((arg_AF_1 = a3_blessing.<>c.<>9__0_2) == null)
			{
				arg_AF_1 = (a3_blessing.<>c.<>9__0_2 = new Action<GameObject>(a3_blessing.<>c.<>9.<init>b__0_2));
			}
			arg_AF_0.onClick = arg_AF_1;
		}

		public override void onShowed()
		{
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(A3_ActiveProxy.EVENT_ONBLESS, new Action<GameEvent>(this.OnBless));
		}

		public override void onClosed()
		{
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(A3_ActiveProxy.EVENT_ONBLESS, new Action<GameEvent>(this.OnBless));
		}

		public void OnBless(GameEvent e)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_BLESSING);
		}
	}
}
