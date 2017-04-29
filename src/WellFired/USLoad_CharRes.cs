using MuGame;
using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Load_CharRes"), USequencerFriendlyName("Load Char")]
	public class USLoad_CharRes : USEventBase
	{
		public GameObject[] m_LinkerObject;

		public int m_nResID = -1;

		public string[] m_strResAnim;

		public PLOT_CHARRES_TYPE m_eType;

		public override void FireEvent()
		{
			if (this.m_LinkerObject.Length > 0 && this.m_nResID > 0)
			{
				gameST.REV_CHARRES_LINKER(this.m_LinkerObject, this.m_eType, this.m_nResID, this.m_strResAnim);
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
