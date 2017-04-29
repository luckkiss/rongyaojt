using MuGame;
using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Load_FX"), USequencerFriendlyName("Load FX")]
	public class USLoad_FXRes : USEventBase
	{
		public GameObject[] m_LinkerObject;

		public string m_strRFXRes;

		public override void FireEvent()
		{
			if (this.m_LinkerObject.Length > 0 && this.m_strRFXRes != null)
			{
				gameST.REV_FXRES_LINKER(this.m_LinkerObject, this.m_strRFXRes);
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
