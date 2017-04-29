using MuGame;
using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Load_SoundRes"), USequencerFriendlyName("Load Sound")]
	public class USLoad_SoundRes : USEventBase
	{
		public GameObject m_LinkerObject;

		public int m_nPlotSoundID = -1;

		public override void FireEvent()
		{
			if (this.m_LinkerObject != null && this.m_nPlotSoundID > 0)
			{
				gameST.REV_SOUNDRES_LINKER(this.m_LinkerObject, this.m_nPlotSoundID);
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
