using System;
using UnityEngine;

namespace Cross
{
	public class AssetMaterialImpl : AssetImpl, IAssetMaterial, IAsset
	{
		protected Material m_material;

		public Material material
		{
			get
			{
				bool flag = !this.m_ready;
				Material result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_material;
				}
				return result;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_material != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_material);
				this.m_material = null;
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
					this.dispose();
					this.m_material = Resources.Load<Material>(this.m_path);
					this.m_loaded = true;
					bool flag3 = this.m_material != null;
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
