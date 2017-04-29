using MuGame;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CollectBox : CollectRole
{
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		public static readonly CollectBox.<>c <>9 = new CollectBox.<>c();

		public static Action <>9__1_2;

		internal void <onClick>b__1_2()
		{
			BaseProxy<MapProxy>.getInstance().sendStopCollectBox(true);
		}
	}

	public bool iscollect;

	public override void onClick()
	{
		bool flag = Vector3.Distance(this.m_curModel.transform.position, SelfRole._inst.m_curModel.transform.position) >= 0f;
		if (flag)
		{
			SelfRole.moveto(this.m_curModel.transform.position, delegate
			{
				BaseProxy<MapProxy>.getInstance().sendCollectBox(this.m_unIID);
				cd.updateHandle = new Action<cd>(base.onCD2);
				Action arg_5E_0 = delegate
				{
					this.m_curAni.SetBool("open", true);
					BaseProxy<MapProxy>.getInstance().sendStopCollectBox(false);
					InterfaceMgr.getInstance().close(InterfaceMgr.CD);
				};
				float arg_5E_1 = this.collectTime;
				bool arg_5E_2 = true;
				Action arg_5E_3;
				if ((arg_5E_3 = CollectBox.<>c.<>9__1_2) == null)
				{
					arg_5E_3 = (CollectBox.<>c.<>9__1_2 = new Action(CollectBox.<>c.<>9.<onClick>b__1_2));
				}
				cd.show(arg_5E_0, arg_5E_1, arg_5E_2, arg_5E_3, default(Vector3));
			}, true, 1f, true);
		}
	}
}
