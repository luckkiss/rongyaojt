using DG.Tweening;
using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace MuGame
{
	internal class pk_notify : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly pk_notify.<>c <>9 = new pk_notify.<>c();

			public static TweenCallback <>9__6_0;

			internal void <refesh>b__6_0()
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.PK_NOTIFY);
			}
		}

		private Text txt;

		public static pk_notify instance;

		private Sequence sq;

		public override void init()
		{
			this.txt = base.getComponentByPath<Text>("txt");
			base.gameObject.SetActive(false);
		}

		public override void onShowed()
		{
			pk_notify.instance = this;
			this.refesh();
		}

		public override void onClosed()
		{
			pk_notify.instance = null;
			bool flag = this.sq != null;
			if (flag)
			{
				this.sq.Kill(false);
			}
			this.sq = null;
		}

		public void refesh()
		{
		}
	}
}
