using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/Res_PlayAnim"), USequencerFriendlyName("Res PlayAnim")]
	public class USResPlayAnim : USEventBase
	{
		public GameObject m_LinkerObject;

		public string m_strAnimName = "idle";

		public float m_fAnimSpeed = 1f;

		public WrapMode m_wmAnimWrapMode = WrapMode.Loop;

		public override void FireEvent()
		{
			if (this.m_LinkerObject != null)
			{
				int childCount = this.m_LinkerObject.transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					GameObject gameObject = this.m_LinkerObject.transform.GetChild(i).gameObject;
					Animation component = gameObject.GetComponent<Animation>();
					if (component != null && component.GetClip(this.m_strAnimName) != null)
					{
						component[this.m_strAnimName].speed = this.m_fAnimSpeed;
						component.Play(this.m_strAnimName);
						component.wrapMode = this.m_wmAnimWrapMode;
					}
				}
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
