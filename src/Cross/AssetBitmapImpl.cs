using System;
using UnityEngine;

namespace Cross
{
	public class AssetBitmapImpl : AssetImpl, IAssetBitmap, IAsset
	{
		private Sprite m_sprite;

		public Sprite sprite
		{
			get
			{
				bool flag = !this.m_ready;
				Sprite result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_sprite;
				}
				return result;
			}
		}

		public Texture texture
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
					result = this.m_sprite.texture;
				}
				return result;
			}
		}

		public int width
		{
			get
			{
				bool flag = !this.m_ready || this.m_sprite == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.m_sprite.texture.width;
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
					result = this.m_sprite.texture.height;
				}
				return result;
			}
		}

		public AssetBitmapImpl()
		{
		}

		public AssetBitmapImpl(AssetBitmapImpl abi)
		{
			this.m_sprite = abi.m_sprite;
		}

		public override void dispose()
		{
			bool flag = this.m_sprite != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_sprite);
				this.m_sprite = null;
			}
			this.m_loaded = false;
			this.m_ready = false;
		}

		public override void loadImpl(bool bSync)
		{
			base.loadImpl(bSync);
			bool flag = this.m_path == null || this.m_path == "";
			if (flag)
			{
				this.m_loaded = true;
				this.m_loading = false;
				this.m_ready = false;
				this._dispatchOnFails("Url is null!");
			}
			else
			{
				bool flag2 = this.m_ready || this.m_loading;
				if (!flag2)
				{
					try
					{
						if (bSync)
						{
							Debug.Log("warnning::sync Sk Bitmap " + this.m_path);
							try
							{
								this.m_sprite = Resources.Load<Sprite>(this.m_path);
							}
							catch
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "path is " + this.m_path + " type is error");
							}
							this.m_loaded = true;
							bool flag3 = this.m_sprite != null && this.m_sprite.texture.width != 0 && this.m_sprite.texture.height != 0;
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
								url = (this.m_path.IndexOf(".pic") < 0) ? (this.m_path + ".pic") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								try
								{
									this.m_sprite = (data as Sprite);
								}
								catch
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, "path is " + this.m_path + " type is error");
								}
								this.m_loading = false;
								this.m_loaded = true;
								bool flag4 = this.m_sprite != null && this.m_sprite.texture.width != 0 && this.m_sprite.texture.height != 0;
								if (flag4)
								{
									this.m_ready = true;
									(os.asset as AssetManagerImpl).readyAsset(this);
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
					catch (Exception)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, " AssetBitmap Load file[" + this.m_path + "] err!");
					}
				}
			}
		}
	}
}
