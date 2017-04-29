using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class AssetAniBitmapImpl : AssetImpl, IAssetAniBitmap, IAsset
	{
		private List<Sprite> m_sprite = new List<Sprite>();

		private UnityEngine.Object[] m_tem;

		public List<Sprite> sprite
		{
			get
			{
				bool flag = !this.m_ready;
				List<Sprite> result;
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
					result = this.m_sprite[0].texture.width;
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
					result = this.m_sprite[0].texture.height;
				}
				return result;
			}
		}

		public override void dispose()
		{
			base.dispose();
			foreach (Sprite current in this.m_sprite)
			{
				bool flag = current != null;
				if (flag)
				{
					Resources.UnloadAsset(current);
				}
			}
			this.m_loaded = false;
			this.m_ready = false;
		}

		public override void loadImpl(bool bSync)
		{
			base.loadImpl(bSync);
			bool flag = this.m_path == "" || this.m_path == null;
			if (!flag)
			{
				bool flag2 = this.m_ready || this.m_loaded;
				if (!flag2)
				{
					try
					{
						if (bSync)
						{
							this.m_tem = Resources.LoadAll(this.m_path);
							for (int i = 0; i < this.m_tem.Length; i++)
							{
								bool flag3 = this.m_tem[i].GetType().ToString() == "UnityEngine.Sprite";
								if (flag3)
								{
									this.m_sprite.Add(this.m_tem[i] as Sprite);
								}
							}
							this.m_loaded = true;
							for (int j = 0; j < this.m_sprite.Count; j++)
							{
								bool flag4 = this.m_sprite[j] == null || this.m_sprite[j].texture == null || this.m_sprite[j].texture.width == 0 || this.m_sprite[j].texture.height == 0;
								if (flag4)
								{
									break;
								}
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
								url = (this.m_path.IndexOf(".anipic") < 0) ? (this.m_path + ".anipic") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								this.m_tem = (data as UnityEngine.Object[]);
								for (int k = 0; k < this.m_tem.Length; k++)
								{
									bool flag5 = this.m_tem[k].GetType().ToString() == "UnityEngine.Sprite";
									if (flag5)
									{
										this.m_sprite.Add(this.m_tem[k] as Sprite);
									}
								}
								this.m_loading = false;
								this.m_loaded = true;
								for (int l = 0; l < this.m_sprite.Count; l++)
								{
									bool flag6 = this.m_sprite[l] == null || this.m_sprite[l].texture == null || this.m_sprite[l].texture.width == 0 || this.m_sprite[l].texture.height == 0;
									if (flag6)
									{
										break;
									}
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
