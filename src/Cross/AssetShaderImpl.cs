using System;
using UnityEngine;

namespace Cross
{
	public class AssetShaderImpl : AssetImpl, IAssetShader, IAsset
	{
		protected Shader m_shader;

		public Shader shader
		{
			get
			{
				bool flag = !this.m_ready;
				Shader result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_shader;
				}
				return result;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_shader != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_shader);
				this.m_shader = null;
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
					this.m_shader = Resources.Load<Shader>(this.m_path);
					this.m_loaded = true;
					bool flag3 = this.m_shader != null;
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
