using System;
using UnityEngine;

namespace Cross
{
	public class AssetTextureImpl : AssetImpl, IAssetTexture, IAsset
	{
		protected Texture m_tex;

		public int width
		{
			get
			{
				bool flag = !this.m_ready || this.m_tex == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_tex.width;
				}
				return result;
			}
		}

		public int height
		{
			get
			{
				bool flag = !this.m_ready || this.m_tex == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_tex.height;
				}
				return result;
			}
		}

		public Texture u3dTexture
		{
			get
			{
				bool flag = !this.m_ready;
				Texture result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_tex;
				}
				return result;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_tex != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_tex);
				this.m_tex = null;
			}
			this.m_loaded = false;
			this.m_ready = false;
		}

		public override void loadImpl(bool bSync)
		{
			base.loadImpl(bSync);
			bool flag = this.m_path == null || this.m_path == "";
			if (!flag)
			{
				bool flag2 = this.m_ready || this.m_loaded;
				if (!flag2)
				{
					this.m_tex = Resources.Load<Texture>(this.m_path);
					this.m_loaded = true;
					bool flag3 = this.m_tex != null;
					if (flag3)
					{
						this.m_ready = true;
						(os.asset as AssetManagerImpl).readyAsset(this);
					}
					this._dispatchOnFins();
				}
			}
		}
	}
}
