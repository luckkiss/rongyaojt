using System;

namespace Cross
{
	public interface ILightDir : ILight, IGraphObject3D, IGraphObject
	{
		Vec3 direction
		{
			get;
			set;
		}
	}
}
