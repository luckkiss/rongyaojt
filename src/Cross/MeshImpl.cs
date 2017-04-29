using System;
using UnityEngine;

namespace Cross
{
	public class MeshImpl : GraphObject3DImpl, IMesh, IGraphObject3D, IGraphObject
	{
		protected AssetMeshImpl m_asset;

		protected GameObject m_meshObj = null;

		public IAssetMesh asset
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
					this.m_asset = (value as AssetMeshImpl);
					bool flag2 = value == null;
					if (flag2)
					{
						bool flag3 = this.m_meshObj != null;
						if (flag3)
						{
							UnityEngine.Object.Destroy(this.m_meshObj);
							this.m_meshObj = null;
						}
					}
					else
					{
						this.m_asset.addCallbacks(delegate(IAsset ast)
						{
							bool flag5 = this.m_meshObj != null;
							if (flag5)
							{
								UnityEngine.Object.Destroy(this.m_meshObj);
								this.m_meshObj = null;
							}
							bool flag6 = !ast.isReady;
							if (!flag6)
							{
								bool flag7 = this.m_u3dObj == null;
								if (!flag7)
								{
									AssetMeshImpl assetMeshImpl = ast as AssetMeshImpl;
									this.m_meshObj = (UnityEngine.Object.Instantiate(assetMeshImpl.assetObj) as GameObject);
									osImpl.linkU3dObj(this.m_u3dObj, this.m_meshObj);
								}
							}
						}, null, null);
						bool flag4 = !this.m_asset.isReady;
						if (flag4)
						{
							this.m_asset.load();
						}
					}
				}
			}
		}

		public MeshImpl()
		{
			this.m_u3dObj.name = "Mesh";
		}

		public override void dispose()
		{
			bool flag = this.m_meshObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_meshObj);
				this.m_meshObj = null;
			}
			base.dispose();
		}

		public override void onPreRender()
		{
			base.onPreRender();
			bool flag = this.m_asset != null;
			if (flag)
			{
				this.m_asset.visit();
			}
		}
	}
}
