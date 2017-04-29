using System;
using System.Collections.Generic;

namespace Cross
{
	public interface ILineObject
	{
		Vec3 startColor
		{
			get;
			set;
		}

		Vec3 endColor
		{
			get;
			set;
		}

		bool local
		{
			get;
			set;
		}

		float startWidth
		{
			get;
			set;
		}

		float endWidth
		{
			get;
			set;
		}

		List<Vec3> points
		{
			get;
			set;
		}

		Vec3 start
		{
			get;
			set;
		}

		Vec3 end
		{
			get;
			set;
		}

		float hAlign
		{
			get;
			set;
		}

		float vAlign
		{
			get;
			set;
		}

		void init(List<Vec3> points);

		void updateLine();

		void dispose(bool remove);
	}
}
