using System;

namespace Cross
{
	public interface IHeightMap : IPhysicsObject3D, IPhysicsObject
	{
		IAssetHeightMap asset
		{
			get;
			set;
		}

		float width
		{
			get;
		}

		float height
		{
			get;
		}

		float heightMin
		{
			get;
		}

		float heightMax
		{
			get;
		}

		float pickHeight(float h, float v);
	}
}
