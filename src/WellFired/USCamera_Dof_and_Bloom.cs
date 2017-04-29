using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Camera/Cam - Dof And Bloom"), USequencerFriendlyName("Cm_DB")]
	public class USCamera_Dof_and_Bloom : USEventBase
	{
		public GameObject m_LinkerCamera;

		public Dof34QualitySetting quality = Dof34QualitySetting.OnlyBackground;

		public DofResolution resolution_dof = DofResolution.Low;

		public bool simpleTweakMode = true;

		public float focalPoint = 1f;

		public float smoothness = 0.5f;

		public float focalZDistance;

		public float focalZStartCurve = 1f;

		public float focalZEndCurve = 1f;

		public Transform objectFocus;

		public float focalSize;

		public DofBlurriness bluriness = DofBlurriness.Low;

		public float maxBlurSpread = 1.75f;

		public float threshhold = 0.25f;

		public float intensity = 0.75f;

		public float blurSize = 1f;

		public FastBloom.Resolution resolution_bloom;

		public int blurIterations = 1;

		public FastBloom.BlurType blurType;

		public override void FireEvent()
		{
			if (this.m_LinkerCamera != null)
			{
				string path = "plot_c/plotcam_dof_and_bloom";
				GameObject gameObject = Resources.Load(path) as GameObject;
				if (gameObject != null)
				{
					Camera component = this.m_LinkerCamera.GetComponent<Camera>();
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(this.m_LinkerCamera.transform, false);
					Camera component2 = gameObject2.GetComponent<Camera>();
					component2.fieldOfView = component.fieldOfView;
					DepthOfField34 component3 = gameObject2.GetComponent<DepthOfField34>();
					component3.quality = this.quality;
					component3.resolution = this.resolution_dof;
					component3.simpleTweakMode = this.simpleTweakMode;
					component3.focalPoint = this.focalPoint;
					component3.smoothness = this.smoothness;
					component3.focalZDistance = this.focalZDistance;
					component3.focalZStartCurve = this.focalZStartCurve;
					component3.focalZEndCurve = this.focalZEndCurve;
					component3.objectFocus = this.objectFocus;
					component3.focalSize = this.focalSize;
					component3.bluriness = this.bluriness;
					component3.maxBlurSpread = this.maxBlurSpread;
					FastBloom component4 = gameObject2.GetComponent<FastBloom>();
					component4.threshhold = this.threshhold;
					component4.intensity = this.intensity;
					component4.blurSize = this.blurSize;
					component4.resolution = this.resolution_bloom;
					component4.blurIterations = this.blurIterations;
					component4.blurType = this.blurType;
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
