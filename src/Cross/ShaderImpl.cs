using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class ShaderImpl : IShader
	{
		protected Material m_material;

		protected AssetShaderImpl m_asset;

		protected Dictionary<string, string> m_paraTexs = new Dictionary<string, string>();

		protected Dictionary<string, float> m_paraFloats = new Dictionary<string, float>();

		protected Dictionary<string, Vec4> m_paraVecs = new Dictionary<string, Vec4>();

		public IAssetShader asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				bool flag = this.m_asset != value;
				if (flag)
				{
					this.m_asset = (value as AssetShaderImpl);
					bool flag2 = !this.m_asset.isReady;
					if (flag2)
					{
						this.m_asset.load();
					}
					bool flag3 = this.m_material != null;
					if (flag3)
					{
						UnityEngine.Object.Destroy(this.m_material);
					}
					this.m_material = new Material(this.m_asset.shader);
				}
			}
		}

		public Material u3dMaterial
		{
			get
			{
				return this.m_material;
			}
		}

		public void dispose()
		{
			this.m_material = null;
			this.m_asset = null;
		}

		public void setProperty(string propName, float value)
		{
			this.m_paraFloats[propName] = value;
			bool flag = this.m_material != null;
			if (flag)
			{
				this.m_material.SetFloat(propName, value);
			}
		}

		public void setProperty(string propName, float x, float y, float z, float w)
		{
			this.m_paraVecs[propName] = new Vec4(x, y, z, w);
			bool flag = this.m_material != null;
			if (flag)
			{
				this.m_material.SetVector(propName, new Vector4(x, y, z, w));
			}
		}

		public void setTexture(string propName, string texPath)
		{
			this.m_paraTexs[propName] = texPath;
			bool flag = this.m_material != null;
			if (flag)
			{
				AssetTextureImpl assetTextureImpl = os.asset.getAsset<IAssetTexture>(texPath) as AssetTextureImpl;
				bool flag2 = assetTextureImpl == null;
				if (!flag2)
				{
					bool flag3 = !assetTextureImpl.isReady;
					if (flag3)
					{
						assetTextureImpl.load();
					}
					this.m_material.SetTexture(propName, assetTextureImpl.u3dTexture);
				}
			}
		}

		public void apply(Material u3dMtrl)
		{
			bool flag = u3dMtrl == null;
			if (!flag)
			{
				foreach (string current in this.m_paraFloats.Keys)
				{
					u3dMtrl.SetFloat(current, this.m_paraFloats[current]);
				}
				foreach (string current2 in this.m_paraVecs.Keys)
				{
					Vec4 vec = this.m_paraVecs[current2];
					u3dMtrl.SetVector(current2, new Vector4(vec.x, vec.y, vec.z, vec.w));
				}
			}
		}
	}
}
