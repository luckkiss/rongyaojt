using System;
using UnityEngine;

namespace Cross
{
	public class AssetParticlesImpl : AssetImpl, IAssetParticles, IAsset
	{
		private GameObject m_asset;

		public GameObject assetObj
		{
			get
			{
				bool flag = !this.m_ready;
				GameObject result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_asset;
				}
				return result;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_asset != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_asset);
				this.m_asset = null;
			}
			this.m_loaded = false;
			this.m_ready = false;
		}

		public override void loadImpl(bool bSync)
		{
			bool flag = this.m_path == null || this.m_path == "";
			if (!flag)
			{
				bool flag2 = this.m_ready || this.m_loading;
				if (!flag2)
				{
					base.loadImpl(bSync);
					try
					{
						if (bSync)
						{
							Debug.Log("sync::Asset Particles " + this.m_path);
							this.m_asset = null;
							this.m_asset = Resources.Load<GameObject>(this.m_path);
							bool flag3 = this.m_asset == null;
							if (flag3)
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "Load EffectParticles failed: " + this.m_path);
							}
							this.m_loaded = true;
							bool flag4 = this.m_asset != null;
							if (flag4)
							{
								this.m_ready = true;
								(os.asset as AssetManagerImpl).readyAsset(this);
							}
							this._dispatchOnFins();
						}
						else
						{
							this.m_loading = true;
							this.m_ready = false;
							this.m_loaded = false;
							new URLReqImpl
							{
								dataFormat = "assetbundle",
								url = (this.m_path.IndexOf(".res") < 0) ? (this.m_path + ".res") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								this.m_asset = null;
								this.m_asset = (data as GameObject);
								this.m_loading = false;
								this.m_loaded = true;
								bool flag5 = this.m_asset == null;
								if (flag5)
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, "Load EffectParticles failed: " + this.m_path);
								}
								bool flag6 = this.m_asset != null;
								if (flag6)
								{
									this.m_ready = true;
									(os.asset as AssetManagerImpl).readyAsset(this);
								}
								this._dispatchOnFins();
							}, delegate(IURLReq r, float progress)
							{
								bool flag5 = this.m_onProgs != null;
								if (flag5)
								{
									this.m_onProgs(this, progress);
								}
							}, delegate(IURLReq r, string err)
							{
								this.m_loading = false;
								this.m_loaded = true;
								this.m_ready = false;
								this._dispatchOnFails(err);
							});
						}
					}
					catch
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "Failed to Load Fx: " + this.m_path);
					}
				}
			}
		}
	}
}
