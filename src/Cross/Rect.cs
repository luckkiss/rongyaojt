using System;

namespace Cross
{
	public class Rect
	{
		protected float[] m_v = new float[4];

		public float x
		{
			get
			{
				return this.m_v[0];
			}
			set
			{
				this.m_v[0] = value;
			}
		}

		public float y
		{
			get
			{
				return this.m_v[1];
			}
			set
			{
				this.m_v[1] = value;
			}
		}

		public float width
		{
			get
			{
				return this.m_v[2];
			}
			set
			{
				this.m_v[2] = value;
			}
		}

		public float height
		{
			get
			{
				return this.m_v[3];
			}
			set
			{
				this.m_v[3] = value;
			}
		}

		public float right
		{
			get
			{
				return this.m_v[0] + this.m_v[2];
			}
		}

		public float bottom
		{
			get
			{
				return this.m_v[1] + this.m_v[3];
			}
		}

		public Rect(float x, float y, float width, float height)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = width;
			this.m_v[3] = height;
		}

		public Rect clone()
		{
			return new Rect(this.m_v[0], this.m_v[1], this.m_v[2], this.m_v[3]);
		}

		public void copyFrom(Rect rt)
		{
			this.m_v[0] = rt.m_v[0];
			this.m_v[1] = rt.m_v[1];
			this.m_v[2] = rt.m_v[2];
			this.m_v[3] = rt.m_v[3];
		}

		public void set(float x, float y, float width, float height)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = width;
			this.m_v[3] = height;
		}

		public void intersect(Rect rect)
		{
			float num = this.m_v[0];
			float num2 = this.m_v[0] + this.m_v[2];
			float num3 = this.m_v[1];
			float num4 = this.m_v[1] + this.m_v[3];
			float num5 = rect.m_v[0];
			float num6 = rect.m_v[0] + rect.m_v[2];
			float num7 = rect.m_v[1];
			float num8 = rect.m_v[1] + rect.m_v[3];
			float num9 = (num > num5) ? num : num5;
			float num10 = (num2 < num6) ? num2 : num6;
			float num11 = (num3 > num7) ? num3 : num7;
			float num12 = (num4 < num8) ? num4 : num8;
			this.m_v[0] = num9;
			this.m_v[1] = num11;
			this.m_v[2] = ((num10 - num9 > 0f) ? (num10 - num9) : 0f);
			this.m_v[3] = ((num12 - num11 > 0f) ? (num12 - num11) : 0f);
		}
	}
}
