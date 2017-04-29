using System;
using UnityEngine;

namespace Cross
{
	public class AssetAudioImpl : AssetImpl, IAssetAudio, IAsset
	{
		protected float m_length;

		protected AudioClip m_audioClip;

		public AudioClip audioClip
		{
			get
			{
				bool flag = !this.m_ready;
				AudioClip result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_audioClip;
				}
				return result;
			}
		}

		public float length
		{
			get
			{
				return this.m_length;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_audioClip != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_audioClip);
				this.m_audioClip = null;
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
							Debug.Log("sync::Asset Audio " + this.m_path);
							this.m_audioClip = Resources.Load<AudioClip>(this.m_path);
							bool flag3 = this.m_audioClip != null;
							if (flag3)
							{
								this.m_ready = true;
								(os.asset as AssetManagerImpl).readyAsset(this);
								this.m_length = this.m_audioClip.length;
								this.m_loaded = true;
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
								url = (this.m_path.IndexOf(".snd") < 0) ? (this.m_path + ".snd") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								this.m_audioClip = (data as AudioClip);
								this.m_loading = false;
								this.m_loaded = true;
								bool flag4 = this.m_audioClip != null;
								if (flag4)
								{
									this.m_ready = true;
									(os.asset as AssetManagerImpl).readyAsset(this);
									this.m_length = this.m_audioClip.length;
									this.m_loaded = true;
								}
								this._dispatchOnFins();
							}, delegate(IURLReq r, float progress)
							{
								bool flag4 = this.m_onProgs != null;
								if (flag4)
								{
									this.m_onProgs(this, progress);
								}
							}, delegate(IURLReq r, string err)
							{
								this._dispatchOnFails(err);
								this.m_loading = false;
								this.m_loaded = true;
								this.m_ready = false;
							});
						}
					}
					catch
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "Failed to Load Audio: " + this.m_path);
					}
				}
			}
		}
	}
}
