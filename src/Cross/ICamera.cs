using System;
using System.Collections.Generic;

namespace Cross
{
	public interface ICamera : IGraphObject3D, IGraphObject
	{
		float fov
		{
			get;
			set;
		}

		float near
		{
			get;
			set;
		}

		float far
		{
			get;
			set;
		}

		float viewWidth
		{
			get;
			set;
		}

		float viewHeight
		{
			get;
			set;
		}

		bool openHdr
		{
			get;
			set;
		}

		bool openTonemapping
		{
			get;
			set;
		}

		bool openBloomPro
		{
			get;
			set;
		}

		bool perspective
		{
			get;
			set;
		}

		float orthographicSize
		{
			get;
			set;
		}

		Vec4 backColor
		{
			get;
			set;
		}

		bool background
		{
			get;
			set;
		}

		int renderOrder
		{
			get;
			set;
		}

		Rect viewportRect
		{
			get;
			set;
		}

		void addLayer(int layer);

		void removeLayer(int layer);

		bool hasLayer(int layer);

		void clearLayers();

		void lookAt(Vec3 pos);

		Vec3 screenToWorldPoint(Vec3 vec);

		Vec3 worldToScreenPoint(Vec3 vec);

		IGraphObject3D rayCast(Vec3 vec);

		List<IGraphObject3D> rayCastAll(Vec3 vec);

		void obj_mask(Vec3 h_pos, Vec3 cam_pos);
	}
}
