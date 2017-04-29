using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class EffectKnifeLightImpl : EffectImpl, IEffectKnifeLight, IEffect, IGraphObject3D, IGraphObject
	{
		private AssetMaterialImpl m_asset;

		protected GameObject m_effectObj;

		protected Trail m_trail;

		protected bool m_assetLoaded = false;

		protected bool m_play;

		protected bool m_stretchToFit;

		protected float m_lifeTime;

		protected List<Vec3> m_colorOverLife = new List<Vec3>();

		protected List<Vec2> m_sizeOverLife = new List<Vec2>();

		protected float m_minVertexDistance;

		protected int m_maxNumberOfPoints;

		protected bool m_useForwardOveride;

		protected Vec3 m_forwardOverride = new Vec3();

		protected bool m_forwardOverideRelative;

		public override int layer
		{
			get
			{
				return this.m_layer;
			}
			set
			{
				bool flag = value < 0 || value > 7;
				if (!flag)
				{
					bool flag2 = this.m_effectObj != null;
					if (flag2)
					{
					}
				}
			}
		}

		public IAssetMaterial asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				this.m_asset = (value as AssetMaterialImpl);
				this.m_assetLoaded = false;
				bool isReady = this.m_asset.isReady;
				if (isReady)
				{
					this.onPreRender();
				}
				else
				{
					this.m_asset.addCallbacks(delegate(IAsset ast)
					{
						this.onPreRender();
					}, null, null);
				}
			}
		}

		public bool stretchToFit
		{
			get
			{
				return this.m_stretchToFit;
			}
			set
			{
				this.m_stretchToFit = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.TrailData.StretchToFit = this.m_stretchToFit;
				}
			}
		}

		public float lifetTime
		{
			get
			{
				return this.m_lifeTime;
			}
			set
			{
				this.m_lifeTime = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.TrailData.Lifetime = this.m_lifeTime;
				}
			}
		}

		public List<Vec3> colorOverLife
		{
			get
			{
				return this.m_colorOverLife;
			}
			set
			{
				this.m_colorOverLife = value;
				bool flag = this.m_colorOverLife.Count == 0;
				if (!flag)
				{
					GradientColorKey[] array = new GradientColorKey[2];
					array[0].color = new Color(this.m_colorOverLife[0].x / 255f, this.m_colorOverLife[0].y / 255f, this.m_colorOverLife[0].z / 255f);
					array[0].time = 0f;
					array[1].color = new Color(this.m_colorOverLife[1].x / 255f, this.m_colorOverLife[1].y / 255f, this.m_colorOverLife[1].z / 255f);
					array[1].time = 1f;
					GradientAlphaKey[] array2 = new GradientAlphaKey[2];
					array2[0].alpha = 1f;
					array2[0].time = 0f;
					array2[1].alpha = 0f;
					array2[1].time = 1f;
					bool flag2 = this.m_trail != null;
					if (flag2)
					{
						this.m_trail.TrailData.ColorOverLife.SetKeys(array, array2);
					}
				}
			}
		}

		public List<Vec2> sizeOverLife
		{
			get
			{
				return this.m_sizeOverLife;
			}
			set
			{
				this.m_sizeOverLife = value;
				bool flag = this.m_sizeOverLife.Count == 0;
				if (!flag)
				{
					bool flag2 = this.m_trail != null;
					if (flag2)
					{
						this.m_trail.TrailData.SizeOverLife = new AnimationCurve(new Keyframe[]
						{
							new Keyframe(this.m_sizeOverLife[0].x, this.m_sizeOverLife[0].y),
							new Keyframe(this.m_sizeOverLife[1].x, this.m_sizeOverLife[1].y)
						});
					}
				}
			}
		}

		public float minVertexDistance
		{
			get
			{
				return this.m_minVertexDistance;
			}
			set
			{
				this.m_minVertexDistance = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.MinVertexDistance = this.m_minVertexDistance;
				}
			}
		}

		public int maxNumberOfPoints
		{
			get
			{
				return this.m_maxNumberOfPoints;
			}
			set
			{
				this.m_maxNumberOfPoints = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.MaxNumberOfPoints = this.m_maxNumberOfPoints;
				}
			}
		}

		public bool useForwardOverride
		{
			get
			{
				return this.m_useForwardOveride;
			}
			set
			{
				this.m_useForwardOveride = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.TrailData.UseForwardOverride = this.m_useForwardOveride;
				}
			}
		}

		public Vec3 forwardOverride
		{
			get
			{
				return this.m_forwardOverride;
			}
			set
			{
				this.m_forwardOverride = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.TrailData.ForwardOverride = new Vector3(this.m_forwardOverride.x, this.m_forwardOverride.y, this.m_forwardOverride.z);
				}
			}
		}

		public bool forwardOverideRelative
		{
			get
			{
				return this.m_forwardOverideRelative;
			}
			set
			{
				this.m_forwardOverideRelative = value;
				bool flag = this.m_trail != null;
				if (flag)
				{
					this.m_trail.TrailData.ForwardOverideRelative = this.m_forwardOverideRelative;
				}
			}
		}

		public EffectKnifeLightImpl()
		{
			this.m_u3dObj.name = "Effect";
			this.m_u3dObj.AddComponent<MeshRenderer>();
			this.m_effectObj = new GameObject();
			this.m_effectObj.name = "Trail";
		}

		public override void dispose()
		{
			base.dispose();
		}

		public override void onPreRender()
		{
			bool flag = this.m_asset == null;
			if (!flag)
			{
				this.m_asset.visit();
				bool assetLoaded = this.m_assetLoaded;
				if (!assetLoaded)
				{
					bool flag2 = !this.m_asset.isReady;
					if (!flag2)
					{
						Vector3 localPosition = new Vector3(this.m_effectObj.transform.localPosition.x, this.m_effectObj.transform.localPosition.y, this.m_effectObj.transform.localPosition.z);
						Vector3 localEulerAngles = new Vector3(this.m_effectObj.transform.localEulerAngles.x, this.m_effectObj.transform.localEulerAngles.y, this.m_effectObj.transform.localEulerAngles.z);
						Vector3 localScale = new Vector3(this.m_effectObj.transform.localScale.x, this.m_effectObj.transform.localScale.y, this.m_effectObj.transform.localScale.z);
						this.m_effectObj.transform.parent = this.m_u3dObj.transform;
						this.m_effectObj.transform.localPosition = localPosition;
						this.m_effectObj.transform.localEulerAngles = localEulerAngles;
						this.m_effectObj.transform.localScale = localScale;
						bool flag3 = this.m_asset != null && this.m_asset.isReady;
						if (flag3)
						{
							this.m_trail = this.m_effectObj.AddComponent<Trail>();
							this.m_trail.Reset();
							this._updataTrail();
						}
						this.m_assetLoaded = true;
					}
				}
			}
		}

		private void _updataTrail()
		{
			bool flag = this.m_asset == null;
			if (!flag)
			{
				bool isReady = this.m_asset.isReady;
				if (isReady)
				{
					this.m_trail.TrailData.TrailMaterial = this.m_asset.material;
					this.stretchToFit = this.m_stretchToFit;
					this.lifetTime = this.m_lifeTime;
					this.colorOverLife = this.m_colorOverLife;
					this.sizeOverLife = this.m_sizeOverLife;
					this.maxNumberOfPoints = this.m_maxNumberOfPoints;
					this.useForwardOverride = this.m_useForwardOveride;
					this.forwardOverride = this.m_forwardOverride;
					this.forwardOverideRelative = this.m_forwardOverideRelative;
				}
			}
		}

		public void play()
		{
			this.m_play = true;
			bool flag = this.m_trail != null;
			if (flag)
			{
				this.m_trail.Emit = this.m_play;
			}
		}

		public void stop()
		{
			this.m_play = false;
			bool flag = this.m_trail != null;
			if (flag)
			{
				this.m_trail.Emit = this.m_play;
			}
		}
	}
}
