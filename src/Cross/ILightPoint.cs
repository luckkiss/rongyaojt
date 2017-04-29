using System;

namespace Cross
{
	public interface ILightPoint : ILight, IGraphObject3D, IGraphObject
	{
		float range
		{
			get;
			set;
		}
	}
}
