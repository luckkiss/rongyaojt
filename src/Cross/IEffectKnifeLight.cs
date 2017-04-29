using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IEffectKnifeLight : IEffect, IGraphObject3D, IGraphObject
	{
		IAssetMaterial asset
		{
			get;
			set;
		}

		float lifetTime
		{
			get;
			set;
		}

		List<Vec2> sizeOverLife
		{
			get;
			set;
		}

		List<Vec3> colorOverLife
		{
			get;
			set;
		}

		bool stretchToFit
		{
			get;
			set;
		}

		bool useForwardOverride
		{
			get;
			set;
		}

		Vec3 forwardOverride
		{
			get;
			set;
		}

		bool forwardOverideRelative
		{
			get;
			set;
		}

		float minVertexDistance
		{
			get;
			set;
		}

		int maxNumberOfPoints
		{
			get;
			set;
		}

		void play();

		void stop();
	}
}
