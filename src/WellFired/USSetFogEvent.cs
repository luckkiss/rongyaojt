using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Light/Set Fog"), USequencerFriendlyName("Set Fog")]
	public class USSetFogEvent : USEventBase
	{
		public bool fog_enable = true;

		public float fog_density = 0.01f;

		public Color fogColor = Color.white;

		public float fog_start;

		public float fog_end = 300f;

		public FogMode fog_mode = FogMode.Linear;

		public override void FireEvent()
		{
			RenderSettings.fog = this.fog_enable;
			RenderSettings.fogDensity = this.fog_density;
			RenderSettings.fogStartDistance = this.fog_start;
			RenderSettings.fogEndDistance = this.fog_end;
			RenderSettings.fogMode = this.fog_mode;
			RenderSettings.fogColor = this.fogColor;
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
