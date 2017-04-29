using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class a3_legion_active : a3BaseLegion
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_legion_active.<>c <>9 = new a3_legion_active.<>c();

			public static Action <>9__1_2;

			internal void <init>b__1_2()
			{
				SelfRole.moveToNPc(10, 1003, null, null);
			}
		}

		public a3_legion_active(BaseShejiao win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			base.init();
			new BaseButton(base.getTransformByPath("rect_mask/rect_scroll/clanDart"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("close").SetActive(true);
			};
			new BaseButton(base.getTransformByPath("close/dart/Button"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("close").SetActive(false);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
				bool flag = ModelBase<PlayerModel>.getInstance().mapid == 10u;
				if (flag)
				{
					SelfRole.moveToNPc(10, 1003, null, null);
				}
				else
				{
					int arg_6F_0 = 1001;
					Action arg_6F_1;
					if ((arg_6F_1 = a3_legion_active.<>c.<>9__1_2) == null)
					{
						arg_6F_1 = (a3_legion_active.<>c.<>9__1_2 = new Action(a3_legion_active.<>c.<>9.<init>b__1_2));
					}
					SelfRole.Transmit(arg_6F_0, arg_6F_1, false, false);
				}
			};
			new BaseButton(base.getTransformByPath("close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("close").SetActive(false);
			};
		}

		public override void onShowed()
		{
			base.onShowed();
		}

		public override void onClose()
		{
			base.onClose();
		}
	}
}
