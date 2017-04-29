using System;

namespace Cross
{
	public interface ILightSpot : ILight, IGraphObject3D, IGraphObject
	{
		float range
		{
			get;
			set;
		}

		float spotAngle
		{
			get;
			set;
		}
	}
}
