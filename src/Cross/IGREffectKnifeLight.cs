using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IGREffectKnifeLight : IGREntity
	{
		IAssetMaterial asset
		{
			set;
		}

		float lifetTime
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

		bool useForwardOverride
		{
			get;
			set;
		}

		bool stretchToFit
		{
			get;
			set;
		}

		bool forwardOverideRelative
		{
			get;
			set;
		}

		Vec3 forwardOverride
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

		void play();

		void stop();
	}
}
