using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Camera/Cam - Fast Bloom"), USequencerFriendlyName("Cm_FB")]
	public class USCamera_FastBloom : USEventBase
	{
		public GameObject m_LinkerCamera;

		public float threshhold = 0.25f;

		public float intensity = 0.75f;

		public float blurSize = 1f;

		public FastBloom.Resolution resolution;

		public int blurIterations = 1;

		public FastBloom.BlurType blurType;

		public override void FireEvent()
		{
			if (this.m_LinkerCamera != null)
			{
				string path = "plot_c/plotcam_fastbloom";
				GameObject gameObject = Resources.Load(path) as GameObject;
				if (gameObject != null)
				{
					Camera component = this.m_LinkerCamera.GetComponent<Camera>();
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(this.m_LinkerCamera.transform, false);
					Camera component2 = gameObject2.GetComponent<Camera>();
					component2.fieldOfView = component.fieldOfView;
					FastBloom component3 = gameObject2.GetComponent<FastBloom>();
					component3.threshhold = this.threshhold;
					component3.intensity = this.intensity;
					component3.blurSize = this.blurSize;
					component3.resolution = this.resolution;
					component3.blurIterations = this.blurIterations;
					component3.blurType = this.blurType;
					if (component != null)
					{
						component.enabled = false;
					}
				}
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
