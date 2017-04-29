using System;

namespace Cross
{
	public class BoundBox
	{
		protected Vec3 m_center = new Vec3();

		protected Vec3 m_extent = new Vec3();

		public Vec3 center
		{
			get
			{
				return this.m_center;
			}
			set
			{
				this.m_center.set(value.x, value.y, value.z);
			}
		}

		public Vec3 extent
		{
			get
			{
				return this.m_extent;
			}
			set
			{
				this.m_extent.set(value.x, value.y, value.z);
			}
		}

		public BoundBox()
		{
		}

		public BoundBox(Vec3 center, Vec3 ext)
		{
			this.m_center.set(center.x, center.y, center.z);
			this.m_extent.set(ext.x, ext.y, ext.z);
		}
	}
}
