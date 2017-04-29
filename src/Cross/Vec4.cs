using System;

namespace Cross
{
	public class Vec4
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

		public float z
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

		public float w
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
				return (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1] + this.m_v[2] * this.m_v[2] + this.m_v[3] * this.m_v[3]));
			}
		}

		public Vec4()
		{
			this.m_v[0] = 0f;
			this.m_v[1] = 0f;
			this.m_v[2] = 0f;
			this.m_v[3] = 1f;
		}

		public Vec4(float x, float y, float z, float w)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = z;
			this.m_v[3] = w;
		}

		public Vec4(Vec3 src)
		{
			this.m_v[0] = src.x;
			this.m_v[1] = src.y;
			this.m_v[2] = src.z;
			this.m_v[3] = 1f;
		}

		public void set(float x, float y, float z, float w)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = z;
			this.m_v[3] = w;
		}

		public Vec4 normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1] + this.m_v[2] * this.m_v[2] + this.m_v[3] * this.m_v[3]));
			return new Vec4(this.m_v[0] * num, this.m_v[1] * num, this.m_v[2] * num, this.m_v[3] * num);
		}

		public float dot(Vec4 v)
		{
			return this.m_v[0] * v.m_v[0] + this.m_v[1] * v.m_v[1] + this.m_v[2] * v.m_v[2] + this.m_v[3] * v.m_v[3];
		}

		public static Vec4 operator +(Vec4 v0, Vec4 v1)
		{
			return new Vec4(v0.m_v[0] + v1.m_v[0], v0.m_v[1] + v1.m_v[1], v0.m_v[2] + v1.m_v[2], v0.m_v[3] + v1.m_v[3]);
		}

		public static Vec4 operator +(Vec4 v0, float v1)
		{
			return new Vec4(v0.m_v[0] + v1, v0.m_v[1] + v1, v0.m_v[2] + v1, v0.m_v[3] + v1);
		}

		public static Vec4 operator +(float v0, Vec4 v1)
		{
			return new Vec4(v0 + v1.m_v[0], v0 + v1.m_v[1], v0 + v1.m_v[2], v0 + v1.m_v[3]);
		}

		public static Vec4 operator -(Vec4 v0, Vec4 v1)
		{
			return new Vec4(v0.m_v[0] - v1.m_v[0], v0.m_v[1] - v1.m_v[1], v0.m_v[2] - v1.m_v[2], v0.m_v[3] - v1.m_v[3]);
		}

		public static Vec4 operator -(Vec4 v0, float v1)
		{
			return new Vec4(v0.m_v[0] - v1, v0.m_v[1] - v1, v0.m_v[2] - v1, v0.m_v[3] - v1);
		}

		public static Vec4 operator -(float v0, Vec4 v1)
		{
			return new Vec4(v0 - v1.m_v[0], v0 - v1.m_v[1], v0 - v1.m_v[2], v0 - v1.m_v[3]);
		}

		public static Vec4 operator *(Vec4 v0, Vec4 v1)
		{
			return new Vec4(v0.m_v[0] * v1.m_v[0], v0.m_v[1] * v1.m_v[1], v0.m_v[2] * v1.m_v[2], v0.m_v[3] * v1.m_v[3]);
		}

		public static Vec4 operator *(Vec4 v0, float v1)
		{
			return new Vec4(v0.m_v[0] * v1, v0.m_v[1] * v1, v0.m_v[2] * v1, v0.m_v[3] * v1);
		}

		public static Vec4 operator *(float v0, Vec4 v1)
		{
			return new Vec4(v0 * v1.m_v[0], v0 * v1.m_v[1], v0 * v1.m_v[2], v0 * v1.m_v[3]);
		}

		public static Vec4 operator /(Vec4 v0, Vec4 v1)
		{
			return new Vec4(v0.m_v[0] / v1.m_v[0], v0.m_v[1] / v1.m_v[1], v0.m_v[2] / v1.m_v[2], v0.m_v[3] / v1.m_v[3]);
		}

		public static Vec4 operator /(Vec4 v0, float v1)
		{
			return new Vec4(v0.m_v[0] / v1, v0.m_v[1] / v1, v0.m_v[2] / v1, v0.m_v[3] / v1);
		}

		public static Vec4 operator /(float v0, Vec4 v1)
		{
			return new Vec4(v0 / v1.m_v[0], v0 / v1.m_v[1], v0 / v1.m_v[2], v0 / v1.m_v[3]);
		}
	}
}
