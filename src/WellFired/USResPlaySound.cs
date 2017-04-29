using MuGame;
using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Res_PlaySound"), USequencerFriendlyName("Res PlaySound")]
	public class USResPlaySound : USEventBase
	{
		public GameObject m_LinkerObject;

		public float m_fSoundVolume = 1f;

		public override void FireEvent()
		{
			if (!GlobleSetting.SOUND_ON)
			{
				return;
			}
			if (this.m_LinkerObject != null)
			{
				AudioSource component = this.m_LinkerObject.GetComponent<AudioSource>();
				if (component != null)
				{
					component.Play();
					component.volume = this.m_fSoundVolume;
				}
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
