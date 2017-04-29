using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Res_PlayFX"), USequencerFriendlyName("Res PlayFX")]
	public class USResPlayFX : USEventBase
	{
		public GameObject[] m_LinkerObject;

		public override void FireEvent()
		{
			for (int i = 0; i < this.m_LinkerObject.Length; i++)
			{
				GameObject gameObject = this.m_LinkerObject[i];
				gameObject.SetActive(true);
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
