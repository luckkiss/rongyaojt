using System;

namespace Cross
{
	public class Mat44
	{
		protected float[][] m_v = new float[4][];

		public float[][] val
		{
			get
			{
				return this.m_v;
			}
		}

		public Mat44()
		{
			float[][] arg_29_0 = this.m_v;
			int arg_29_1 = 0;
			float[] expr_21 = new float[4];
			expr_21[0] = 1f;
			arg_29_0[arg_29_1] = expr_21;
			float[][] arg_3F_0 = this.m_v;
			int arg_3F_1 = 1;
			float[] expr_37 = new float[4];
			expr_37[1] = 1f;
			arg_3F_0[arg_3F_1] = expr_37;
			float[][] arg_55_0 = this.m_v;
			int arg_55_1 = 2;
			float[] expr_4D = new float[4];
			expr_4D[2] = 1f;
			arg_55_0[arg_55_1] = expr_4D;
			this.m_v[3] = new float[]
			{
				0f,
				0f,
				0f,
				1f
			};
		}

		public Mat44(float c)
		{
			float[][] arg_25_0 = this.m_v;
			int arg_25_1 = 0;
			float[] expr_21 = new float[4];
			expr_21[0] = c;
			arg_25_0[arg_25_1] = expr_21;
			float[][] arg_37_0 = this.m_v;
			int arg_37_1 = 1;
			float[] expr_33 = new float[4];
			expr_33[1] = c;
			arg_37_0[arg_37_1] = expr_33;
			float[][] arg_49_0 = this.m_v;
			int arg_49_1 = 2;
			float[] expr_45 = new float[4];
			expr_45[2] = c;
			arg_49_0[arg_49_1] = expr_45;
			this.m_v[3] = new float[]
			{
				0f,
				0f,
				0f,
				c
			};
		}

		public Vec4 getRow(int idx)
		{
			return new Vec4(this.m_v[idx][0], this.m_v[idx][1], this.m_v[idx][2], this.m_v[idx][3]);
		}

		public void setRow(int idx, Vec4 row)
		{
			this.m_v[idx][0] = row.x;
			this.m_v[idx][1] = row.y;
			this.m_v[idx][2] = row.z;
			this.m_v[idx][3] = row.w;
		}

		public Mat44 clone()
		{
			Mat44 mat = new Mat44();
			mat.val[0][0] = this.m_v[0][0];
			mat.val[0][1] = this.m_v[0][1];
			mat.val[0][2] = this.m_v[2][2];
			mat.val[0][3] = this.m_v[0][3];
			mat.val[1][0] = this.m_v[1][0];
			mat.val[1][1] = this.m_v[1][1];
			mat.val[1][2] = this.m_v[1][2];
			mat.val[1][3] = this.m_v[1][3];
			mat.val[2][0] = this.m_v[2][0];
			mat.val[2][1] = this.m_v[2][1];
			mat.val[2][2] = this.m_v[2][2];
			mat.val[2][3] = this.m_v[2][3];
			mat.val[3][0] = this.m_v[3][0];
			mat.val[3][1] = this.m_v[3][1];
			mat.val[3][2] = this.m_v[3][2];
			mat.val[3][3] = this.m_v[3][3];
			return mat;
		}
	}
}
