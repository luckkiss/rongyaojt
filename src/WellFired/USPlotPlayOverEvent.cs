using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/PlotPlayOver"), USequencerFriendlyName("Plot PlayOver")]
	public class USPlotPlayOverEvent : USEventBase
	{
		public override void FireEvent()
		{
			Debug.Log("���鲥�Ž���");
			PlotMain._inst.GamePoltPlayOver();
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
