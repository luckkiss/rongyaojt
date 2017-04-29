using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Render/Change Objects Texture"), USequencerFriendlyName("Change Texture")]
	public class USChangeTexture : USEventBase
	{
		public Texture newTexture;

		private Texture previousTexture;

		public override void FireEvent()
		{
			if (!base.AffectedObject)
			{
				return;
			}
			if (!this.newTexture)
			{
				Debug.LogWarning("you've not given a texture to the USChangeTexture Event", this);
				return;
			}
			if (!Application.isPlaying && Application.isEditor)
			{
				this.previousTexture = base.AffectedObject.GetComponent<Renderer>().sharedMaterial.mainTexture;
				base.AffectedObject.GetComponent<Renderer>().sharedMaterial.mainTexture = this.newTexture;
			}
			else
			{
				this.previousTexture = base.AffectedObject.GetComponent<Renderer>().material.mainTexture;
				base.AffectedObject.GetComponent<Renderer>().material.mainTexture = this.newTexture;
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}

		public override void StopEvent()
		{
			this.UndoEvent();
		}

		public override void UndoEvent()
		{
			if (!base.AffectedObject)
			{
				return;
			}
			if (!this.previousTexture)
			{
				return;
			}
			if (!Application.isPlaying && Application.isEditor)
			{
				base.AffectedObject.GetComponent<Renderer>().sharedMaterial.mainTexture = this.previousTexture;
			}
			else
			{
				base.AffectedObject.GetComponent<Renderer>().material.mainTexture = this.previousTexture;
			}
			this.previousTexture = null;
		}
	}
}
