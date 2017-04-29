using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class fb_lose : Window
	{
		private BaseButton bt;

		public override void init()
		{
			this.bt = new BaseButton(base.getTransformByPath("bt"), 1, 1);
			this.bt.onClick = new Action<GameObject>(this.onClick);
			base.init();
		}

		public override void onShowed()
		{
			this.bt.addEvent();
		}

		public override void onClosed()
		{
			this.bt.removeAllListener();
			base.onClosed();
		}

		private void onClick(GameObject go)
		{
			base.getEventTrigerByPath("bt").clearAllListener();
			BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
		}
	}
}
