using MuGame;
using System;

namespace WellFired
{
	[USequencerEvent("Qsmy/TS"), USequencerFriendlyName("TS")]
	public class USTimeScale : USEventBase
	{
		public float m_ftimeScale = 1f;

		public override void FireEvent()
		{
			Globle.setTimeScale(this.m_ftimeScale);
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
