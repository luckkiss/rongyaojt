using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class story_returnbt : StoryUI
	{
		public override void init()
		{
			base.alain();
		}

		public override void onShowed()
		{
			base.onShowed();
			base.getEventTrigerByPath("bt").onClick = new EventTriggerListener.VoidDelegate(this.onClick);
		}

		public override void onClosed()
		{
			base.onClosed();
			base.getEventTrigerByPath("bt").onClick = new EventTriggerListener.VoidDelegate(this.onClick);
		}

		private void onClick(GameObject go)
		{
			gameST.REQ_STOP_PLOT();
		}
	}
}
