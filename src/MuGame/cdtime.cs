using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class cdtime : Window
	{
		private static Action _handle;

		private Animator ani;

		private processStruct process;

		public static void show(Action handle)
		{
			cdtime._handle = handle;
			InterfaceMgr.getInstance().open(InterfaceMgr.CD_TIME, null, false);
		}

		public override void init()
		{
			this.ani = base.GetComponent<Animator>();
			this.process = new processStruct(new Action<float>(this.onUpdate), "cdtime", false, false);
			base.init();
		}

		public override void onShowed()
		{
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			base.onShowed();
		}

		public override void onClosed()
		{
			(CrossApp.singleton.getPlugin("processManager") as processManager).removeProcess(this.process, false);
			base.onClosed();
		}

		private void onUpdate(float s)
		{
			bool flag = this.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
			if (flag)
			{
				bool flag2 = cdtime._handle != null;
				if (flag2)
				{
					cdtime._handle();
				}
				InterfaceMgr.getInstance().close(InterfaceMgr.CD_TIME);
			}
		}
	}
}
