using System;
using UnityEngine;

namespace Cross
{
	public class AssetSkAnimationImpl : AssetImpl, IAssetSkAnimation, IAsset
	{
		protected AnimationClip m_anim = null;

		public string name
		{
			get
			{
				bool flag = !this.m_ready;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = this.m_anim.name;
				}
				return result;
			}
		}

		public AnimationClip anim
		{
			get
			{
				bool flag = !this.m_ready;
				AnimationClip result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_anim;
				}
				return result;
			}
		}

		public override void dispose()
		{
			bool flag = this.m_anim != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_anim);
				this.m_anim = null;
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
					this.m_anim = null;
					try
					{
						if (bSync)
						{
							Debug.Log("warnning::sync Sk Anim  " + this.m_path);
							this.m_anim = (UnityEngine.Object.Instantiate(Resources.Load<AnimationClip>(this.m_path)) as AnimationClip);
							this.m_loaded = true;
							bool flag3 = this.m_anim != null;
							if (flag3)
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
								url = (this.m_path.IndexOf(".anim") < 0) ? (this.m_path + ".anim") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								this.m_anim = (data as AnimationClip);
								this.m_loading = false;
								this.m_loaded = true;
								this.m_ready = true;
								(os.asset as AssetManagerImpl).readyAsset(this);
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
					catch (Exception ex)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new string[]
						{
							"e = ",
							ex.Message,
							"  m_path = ",
							this.m_path,
							"  is error"
						}));
					}
				}
			}
		}
	}
}
