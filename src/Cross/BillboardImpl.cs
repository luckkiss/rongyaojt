using System;
using UnityEngine;

namespace Cross
{
	public class BillboardImpl : GraphObject3DImpl, IBillboard, IGraphObject3D, IGraphObject
	{
		protected AssetBitmapImpl m_asset = null;

		protected SpriteRenderer m_sprRenderer = null;

		protected bool m_assetLoaded = false;

		protected Sprite m_sprite;

		protected float m_width;

		protected float m_height;

		protected Transform cam;

		public Sprite sprite
		{
			get
			{
				bool flag = this.m_sprite == null;
				if (flag)
				{
					this.m_sprite = this.m_sprRenderer.sprite;
				}
				return this.m_sprite;
			}
			set
			{
				this.m_sprite = value;
				this.m_sprRenderer.sprite = this.m_sprite;
			}
		}

		public IAssetBitmap asset
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
					this.m_asset = (value as AssetBitmapImpl);
					this.m_assetLoaded = false;
				}
				bool flag2 = this.m_asset != null;
				if (flag2)
				{
					bool flag3 = this.m_asset.sprite;
					if (flag3)
					{
						this.m_sprRenderer.material.mainTexture = this.m_asset.sprite.texture;
						this.sprite = this.m_asset.sprite;
						base.y += 1f;
						this.m_u3dObj.AddComponent<BoxCollider>();
					}
				}
			}
		}

		public float setWidth
		{
			get
			{
				bool flag = this.m_asset == null;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = this.m_width;
				}
				return result;
			}
			set
			{
				bool flag = this.m_asset == null;
				if (!flag)
				{
					this.m_width = value;
					try
					{
						this.m_u3dObj.transform.localScale = new Vector3(this.m_width / (this.m_asset.sprite.bounds.size.x * 100f), this.m_u3dObj.transform.localScale.y, this.m_u3dObj.transform.localScale.z);
					}
					catch (Exception var_2_88)
					{
						Debug.Log("path = " + this.m_asset.path);
					}
				}
			}
		}

		public float setHeight
		{
			get
			{
				bool flag = this.m_asset == null;
				float result;
				if (flag)
				{
					result = 0f;
				}
				else
				{
					result = this.m_height;
				}
				return result;
			}
			set
			{
				bool flag = this.m_asset == null;
				if (!flag)
				{
					this.m_height = value;
					try
					{
						this.m_u3dObj.transform.localScale = new Vector3(this.m_u3dObj.transform.localScale.x, this.m_height / (this.m_asset.sprite.bounds.size.y * 100f), this.m_u3dObj.transform.localScale.z);
					}
					catch (Exception var_2_88)
					{
						Debug.Log("path = " + this.m_asset.path);
					}
				}
			}
		}

		public BillboardImpl()
		{
			this.m_id = base.id;
			this.m_u3dObj.name = "Billboard";
			this.m_sprRenderer = this.m_u3dObj.AddComponent<SpriteRenderer>();
			this.cam = GameObject.Find("3DScene/Container3D/Camera").transform;
		}

		public override void dispose()
		{
			bool flag = this.m_sprRenderer.sprite != null;
			if (flag)
			{
				this.m_sprRenderer.sprite = null;
				this.m_sprRenderer = null;
			}
			base.dispose();
			bool flag2 = base.parent != null;
			if (flag2)
			{
				(base.parent as Container3DImpl).removeChild(this);
			}
		}

		public override void onPreRender()
		{
			bool flag = this.m_asset == null;
			if (!flag)
			{
				this.m_asset.visit();
				this.lookAt();
				bool assetLoaded = this.m_assetLoaded;
				if (!assetLoaded)
				{
					bool flag2 = !this.m_asset.isReady;
					if (flag2)
					{
						this.m_asset.load();
					}
					else
					{
						this.m_assetLoaded = true;
					}
				}
			}
		}

		public void lookAt()
		{
			this.m_u3dObj.gameObject.transform.LookAt(this.cam);
		}
	}
}
