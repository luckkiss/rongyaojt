using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Time/Time Scale"), USequencerFriendlyName("Time Scale")]
	public class USTimeScaleEvent : USEventBase
	{
		public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f),
			new Keyframe(3f, 1f),
			new Keyframe(4f, 0f)
		});

		private float currentCurveSampleTime;

		private float prevTimeScale = 1f;

		public override void FireEvent()
		{
			this.prevTimeScale = Time.timeScale;
		}

		public override void ProcessEvent(float deltaTime)
		{
			this.currentCurveSampleTime = deltaTime;
			Time.timeScale = Mathf.Max(0f, this.scaleCurve.Evaluate(this.currentCurveSampleTime));
		}

		public override void EndEvent()
		{
			float time = this.scaleCurve.keys[this.scaleCurve.length - 1].time;
			Time.timeScale = Mathf.Max(0f, this.scaleCurve.Evaluate(time));
		}

		public override void StopEvent()
		{
			this.UndoEvent();
		}

		public override void UndoEvent()
		{
			this.currentCurveSampleTime = 0f;
			Time.timeScale = this.prevTimeScale;
		}
	}
}
