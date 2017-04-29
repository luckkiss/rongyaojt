using System;
using UnityEngine;

namespace Cross
{
	internal class AssetRenderTextureImpl : AssetImpl, IAssetRenderTexture, IAsset
	{
		private RenderTexture m_renderTexture;

		public RenderTexture renderTexture
		{
			get
			{
				bool flag = !this.m_ready;
				RenderTexture result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_renderTexture;
				}
				return result;
			}
		}

		public int width
		{
			get
			{
				bool flag = !this.m_ready || this.m_renderTexture == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_renderTexture.width;
				}
				return result;
			}
		}

		public int height
		{
			get
			{
				bool flag = !this.m_ready;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_renderTexture.height;
				}
				return result;
			}
		}

		public AssetRenderTextureImpl()
		{
		}

		public AssetRenderTextureImpl(AssetRenderTextureImpl artt)
		{
			this.m_renderTexture = artt.renderTexture;
		}

		public override void dispose()
		{
			bool flag = this.m_renderTexture != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_renderTexture);
				this.m_renderTexture = null;
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
				bool flag2 = this.m_ready || this.m_loading;
				if (!flag2)
				{
					try
					{
						if (bSync)
						{
							Debug.Log("warnning::sync Sk renderTexture " + this.m_path);
							this.m_renderTexture = new RenderTexture(1024, 720, 18);
							this.m_loaded = true;
							bool flag3 = this.m_renderTexture != null && this.m_renderTexture.width != 0 && this.m_renderTexture.height != 0;
							if (flag3)
							{
								this.m_ready = true;
								(os.asset as AssetManagerImpl).readyAsset(this);
							}
							this._dispatchOnFins();
						}
					}
					catch (Exception var_4_DF)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, " AssetRenderTexture Load file[" + this.m_path + "] err!");
					}
				}
			}
		}
	}
}
