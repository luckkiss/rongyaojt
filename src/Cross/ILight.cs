using System;

namespace Cross
{
	public interface ILight : IGraphObject3D, IGraphObject
	{
		bool enabled
		{
			get;
			set;
		}

		Vec4 color
		{
			get;
			set;
		}

		float intensity
		{
			get;
			set;
		}

		bool shadow
		{
			get;
			set;
		}

		void addLayer(int layer);

		void removeLayer(int layer);

		bool hasLayer(int layer);

		void clearLayers();
	}
}
