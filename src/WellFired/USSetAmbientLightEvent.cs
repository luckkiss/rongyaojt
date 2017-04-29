using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Light/Set Ambient Light"), USequencerFriendlyName("Set Ambient Light")]
	public class USSetAmbientLightEvent : USEventBase
	{
		public Color lightColor = Color.red;

		public override void FireEvent()
		{
			RenderSettings.ambientLight = this.lightColor;
		}

		public override void ProcessEvent(float deltaTime)
		{
		}

		public override void StopEvent()
		{
		}

		public override void UndoEvent()
		{
		}
	}
}
