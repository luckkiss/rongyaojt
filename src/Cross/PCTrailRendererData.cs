using System;
using UnityEngine;

namespace Cross
{
	[Serializable]
	public class PCTrailRendererData
	{
		public Material TrailMaterial;

		public float Lifetime = 1f;

		public AnimationCurve SizeOverLife = new AnimationCurve();

		public Gradient ColorOverLife;

		public float MaterialTileLength = 0f;

		public bool StretchToFit;

		public bool UseForwardOverride;

		public Vector3 ForwardOverride;

		public bool ForwardOverideRelative;
	}
}
