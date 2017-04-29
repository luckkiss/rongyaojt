using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class a3_pet_desc : Window
	{
		public override void init()
		{
			base.getEventTrigerByPath("closeBtn").onClick = new EventTriggerListener.VoidDelegate(this.OnClose);
			base.getEventTrigerByPath("ig_bg_bg").onClick = new EventTriggerListener.VoidDelegate(this.OnClose);
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_PET_DESC);
		}
	}
}
