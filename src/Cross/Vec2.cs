using System;

namespace Cross
{
	public class Vec2
	{
		protected float[] m_v = new float[2];

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

		public float[] val
		{
			get
			{
				return this.m_v;
			}
		}

		public float length
		{
			get
			{
				return (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1]));
			}
		}

		public Vec2()
		{
			this.m_v[0] = 0f;
			this.m_v[1] = 0f;
		}

		public Vec2(float x, float y)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
		}

		public void set(float x, float y)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
		}

		public Vec2 normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1]));
			return new Vec2(this.m_v[0] * num, this.m_v[1] * num);
		}

		public float dot(Vec2 v)
		{
			return this.m_v[0] * v.m_v[0] + this.m_v[1] * v.m_v[1];
		}

		public float distance(Vec2 v)
		{
			return (float)Math.Sqrt((double)((this.m_v[0] - v.m_v[0]) * (this.m_v[0] - v.m_v[0]) + (this.m_v[1] - v.m_v[1]) * (this.m_v[1] - v.m_v[1])));
		}

		public double angleBetween(Vec2 v2)
		{
			Vec2 vec = this.normalize();
			v2 = v2.normalize();
			return Math.Acos((double)vec.dot(v2));
		}

		public static Vec2 operator +(Vec2 v0, Vec2 v1)
		{
			return new Vec2(v0.m_v[0] + v1.m_v[0], v0.m_v[1] + v1.m_v[1]);
		}

		public Vec2 clone()
		{
			return new Vec2(this.x, this.y);
		}

		public static Vec2 operator +(Vec2 v0, float v1)
		{
			return new Vec2(v0.m_v[0] + v1, v0.m_v[1] + v1);
		}

		public static Vec2 operator +(float v0, Vec2 v1)
		{
			return new Vec2(v0 + v1.m_v[0], v0 + v1.m_v[1]);
		}

		public static Vec2 operator -(Vec2 v0, Vec2 v1)
		{
			return new Vec2(v0.m_v[0] - v1.m_v[0], v0.m_v[1] - v1.m_v[1]);
		}

		public static Vec2 operator -(Vec2 v0, float v1)
		{
			return new Vec2(v0.m_v[0] - v1, v0.m_v[1] - v1);
		}

		public static Vec2 operator -(float v0, Vec2 v1)
		{
			return new Vec2(v0 - v1.m_v[0], v0 - v1.m_v[1]);
		}

		public static Vec2 operator *(Vec2 v0, Vec2 v1)
		{
			return new Vec2(v0.m_v[0] * v1.m_v[0], v0.m_v[1] * v1.m_v[1]);
		}

		public static Vec2 operator *(Vec2 v0, float v1)
		{
			return new Vec2(v0.m_v[0] * v1, v0.m_v[1] * v1);
		}

		public static Vec2 operator *(float v0, Vec2 v1)
		{
			return new Vec2(v0 * v1.m_v[0], v0 * v1.m_v[1]);
		}

		public static Vec2 operator /(Vec2 v0, Vec2 v1)
		{
			return new Vec2(v0.m_v[0] / v1.m_v[0], v0.m_v[1] / v1.m_v[1]);
		}

		public static Vec2 operator /(Vec2 v0, float v1)
		{
			return new Vec2(v0.m_v[0] / v1, v0.m_v[1] / v1);
		}

		public static Vec2 operator /(float v0, Vec2 v1)
		{
			return new Vec2(v0 / v1.m_v[0], v0 / v1.m_v[1]);
		}
	}
}
