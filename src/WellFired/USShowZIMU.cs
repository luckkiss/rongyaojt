using MuGame;
using System;

namespace WellFired
{
	[USequencerEvent("Qsmy/ZI-MU"), USequencerFriendlyName("ZI-MU")]
	public class USShowZIMU : USEventBase
	{
		public string m_strZiMuText = "����";

		public override void FireEvent()
		{
			gameST.REV_ZIMU_TEXT(this.m_strZiMuText);
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
