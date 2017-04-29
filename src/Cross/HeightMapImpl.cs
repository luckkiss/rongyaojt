using System;

namespace Cross
{
	public class HeightMapImpl : PhysicsObject3DImpl, IHeightMap, IPhysicsObject3D, IPhysicsObject
	{
		protected AssetHeightMapImpl m_asset;

		public IAssetHeightMap asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				this.m_asset = (value as AssetHeightMapImpl);
			}
		}

		public float width
		{
			get
			{
				return this.m_asset.width;
			}
		}

		public float height
		{
			get
			{
				return this.m_asset.height;
			}
		}

		public float heightMin
		{
			get
			{
				return this.m_asset.heightMin;
			}
		}

		public float heightMax
		{
			get
			{
				return this.m_asset.heightMax;
			}
		}

		public float pickHeight(float v, float h)
		{
			bool flag = !this.m_asset.isReady;
			if (flag)
			{
				this.m_asset.load();
			}
			int num = (int)(h / this.m_asset.proportion * (float)this.m_asset.pixelWidth + v / this.m_asset.proportion);
			bool flag2 = num < 0 || num > this.m_asset.pixelWidth * this.m_asset.pixelHeight;
			float result;
			if (flag2)
			{
				result = float.NaN;
			}
			else
			{
				result = this.m_asset.heightMin + (this.m_asset.heightMax - this.m_asset.heightMin) * ((float)this.m_asset.byt[num] / 255f);
			}
			return result;
		}

		public override Vec3 rayCast(Vec3 origin, Vec3 dir)
		{
			bool flag = !this.m_asset.isReady;
			if (flag)
			{
				this.m_asset.load();
			}
			float num = (this.m_u3dObj.transform.up.x * (this.m_u3dPos.x - origin.x) + this.m_u3dObj.transform.up.y * (this.m_u3dPos.y - origin.y) + this.m_u3dObj.transform.up.z * (this.m_u3dPos.z - origin.z)) / (this.m_u3dObj.transform.up.x * dir.x + this.m_u3dObj.transform.up.y * dir.y + this.m_u3dObj.transform.up.z * dir.z);
			float num2 = origin.x + dir.x * num;
			float num3 = origin.z + dir.z * num;
			int num4 = (int)(num3 / this.m_asset.proportion * (float)this.m_asset.pixelWidth + num2 / this.m_asset.proportion);
			bool flag2 = num4 < 0 || num4 > this.m_asset.pixelWidth * this.m_asset.pixelHeight;
			Vec3 result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				float y = this.m_asset.heightMin + (this.m_asset.heightMax - this.m_asset.heightMin) * ((float)this.m_asset.byt[num4] / 255f);
				result = new Vec3(num2, y, num3);
			}
			return result;
		}
	}
}
