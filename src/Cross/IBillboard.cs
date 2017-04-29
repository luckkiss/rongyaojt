using System;

namespace Cross
{
	public interface IBillboard : IGraphObject3D, IGraphObject
	{
		IAssetBitmap asset
		{
			get;
			set;
		}

		float setWidth
		{
			get;
			set;
		}

		float setHeight
		{
			get;
			set;
		}

		void lookAt();
	}
}
