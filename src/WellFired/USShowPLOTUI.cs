using MuGame;
using System;

namespace WellFired
{
	[USequencerEvent("Qsmy/PL-UI"), USequencerFriendlyName("PL-UI")]
	public class USShowPLOTUI : USEventBase
	{
		public string m_strPlotUI = string.Empty;

		public override void FireEvent()
		{
			gameST.REV_PLOT_UI(this.m_strPlotUI);
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
