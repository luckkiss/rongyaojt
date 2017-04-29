using System;

namespace Cross
{
	public class Vec3
	{
		protected float[] m_v = new float[3];

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
				return (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1] + this.m_v[2] * this.m_v[2]));
			}
		}

		public Vec3()
		{
			this.m_v[0] = 0f;
			this.m_v[1] = 0f;
			this.m_v[2] = 0f;
		}

		public Vec3(float x, float y, float z)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = z;
		}

		public Vec3(Vec4 src)
		{
			this.m_v[0] = src.x;
			this.m_v[1] = src.y;
			this.m_v[2] = src.z;
		}

		public void set(float x, float y, float z)
		{
			this.m_v[0] = x;
			this.m_v[1] = y;
			this.m_v[2] = z;
		}

		public Vec3 normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.m_v[0] * this.m_v[0] + this.m_v[1] * this.m_v[1] + this.m_v[2] * this.m_v[2]));
			return new Vec3(this.m_v[0] * num, this.m_v[1] * num, this.m_v[2] * num);
		}

		public float distance(Vec3 v)
		{
			return (float)Math.Sqrt((double)((this.m_v[0] - v.m_v[0]) * (this.m_v[0] - v.m_v[0]) + (this.m_v[1] - v.m_v[1]) * (this.m_v[1] - v.m_v[1]) + (this.m_v[2] - v.m_v[2]) * (this.m_v[2] - v.m_v[2])));
		}

		public float dot(Vec3 v)
		{
			return this.m_v[0] * v.m_v[0] + this.m_v[1] * v.m_v[1] + this.m_v[2] * v.m_v[2];
		}

		public Vec3 cross(Vec3 v)
		{
			return new Vec3(this.m_v[1] * v.m_v[2] - this.m_v[2] * v.m_v[1], this.m_v[2] * v.m_v[0] - this.m_v[0] * v.m_v[2], this.m_v[0] * v.m_v[1] - this.m_v[1] * v.m_v[0]);
		}

		public static Vec3 operator +(Vec3 v0, Vec3 v1)
		{
			return new Vec3(v0.m_v[0] + v1.m_v[0], v0.m_v[1] + v1.m_v[1], v0.m_v[2] + v1.m_v[2]);
		}

		public static Vec3 operator +(Vec3 v0, float v1)
		{
			return new Vec3(v0.m_v[0] + v1, v0.m_v[1] + v1, v0.m_v[2] + v1);
		}

		public static Vec3 operator +(float v0, Vec3 v1)
		{
			return new Vec3(v0 + v1.m_v[0], v0 + v1.m_v[1], v0 + v1.m_v[2]);
		}

		public static Vec3 operator -(Vec3 v0, Vec3 v1)
		{
			return new Vec3(v0.m_v[0] - v1.m_v[0], v0.m_v[1] - v1.m_v[1], v0.m_v[2] - v1.m_v[2]);
		}

		public static Vec3 operator -(Vec3 v0, float v1)
		{
			return new Vec3(v0.m_v[0] - v1, v0.m_v[1] - v1, v0.m_v[2] - v1);
		}

		public static Vec3 operator -(float v0, Vec3 v1)
		{
			return new Vec3(v0 - v1.m_v[0], v0 - v1.m_v[1], v0 - v1.m_v[2]);
		}

		public static Vec3 operator *(Vec3 v0, Vec3 v1)
		{
			return new Vec3(v0.m_v[0] * v1.m_v[0], v0.m_v[1] * v1.m_v[1], v0.m_v[2] * v1.m_v[2]);
		}

		public static Vec3 operator *(Vec3 v0, float v1)
		{
			return new Vec3(v0.m_v[0] * v1, v0.m_v[1] * v1, v0.m_v[2] * v1);
		}

		public static Vec3 operator *(float v0, Vec3 v1)
		{
			return new Vec3(v0 * v1.m_v[0], v0 * v1.m_v[1], v0 * v1.m_v[2]);
		}

		public static Vec3 operator /(Vec3 v0, Vec3 v1)
		{
			return new Vec3(v0.m_v[0] / v1.m_v[0], v0.m_v[1] / v1.m_v[1], v0.m_v[2] / v1.m_v[2]);
		}

		public static Vec3 operator /(Vec3 v0, float v1)
		{
			return new Vec3(v0.m_v[0] / v1, v0.m_v[1] / v1, v0.m_v[2] / v1);
		}

		public static Vec3 operator /(float v0, Vec3 v1)
		{
			return new Vec3(v0 / v1.m_v[0], v0 / v1.m_v[1], v0 / v1.m_v[2]);
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(x:",
				this.m_v[0],
				" y:",
				this.m_v[1],
				" z:",
				this.m_v[2],
				")"
			});
		}
	}
}
