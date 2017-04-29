using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class AssetSkAniMeshImpl : AssetImpl, IAssetSkAniMesh, IAsset
	{
		protected GameObject m_asset = null;

		protected List<Mesh> m_meshes = new List<Mesh>();

		protected int m_numVertices = 0;

		protected int m_numTriangles = 0;

		protected bool m_useLightProbes = true;

		protected bool m_castShadows = true;

		protected bool m_receiveShadows = true;

		public GameObject assetObj
		{
			get
			{
				return this.m_asset;
			}
		}

		public List<Mesh> meshes
		{
			get
			{
				bool flag = !this.m_ready;
				List<Mesh> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_meshes;
				}
				return result;
			}
		}

		public int numVertices
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
					result = this.m_numVertices;
				}
				return result;
			}
		}

		public int numTriangles
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
					result = this.m_numTriangles;
				}
				return result;
			}
		}

		public bool receiveShadows
		{
			set
			{
				this.m_receiveShadows = value;
			}
		}

		public bool castShadows
		{
			set
			{
				this.m_castShadows = value;
			}
		}

		public bool useLightProbes
		{
			set
			{
				this.m_useLightProbes = value;
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
			this.m_meshes.Clear();
			this.m_numVertices = 0;
			this.m_numTriangles = 0;
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
					this.m_asset = null;
					try
					{
						if (bSync)
						{
							Debug.Log("warnning::sync Sk Mesh  " + this.m_path);
							this.m_asset = Resources.Load<GameObject>(this.m_path);
							bool flag3 = this.m_asset == null;
							if (flag3)
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "Load SkAniMesh failed: " + this.m_path);
							}
							this.loadedBuild();
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
								this.m_asset = (data as GameObject);
								this.loadedBuild();
								this.m_loading = false;
								this.m_loaded = true;
								this.m_ready = true;
								(os.asset as AssetManagerImpl).readyAsset(this);
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
					catch (Exception var_6_17A)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "Failed to Load AssetSkAniMesh: " + this.m_path);
					}
				}
			}
		}

		private void loadedBuild()
		{
			this.m_numVertices = 0;
			this.m_numTriangles = 0;
			bool flag = this.m_asset != null;
			if (flag)
			{
				Stack<GameObject> stack = new Stack<GameObject>();
				stack.Push(this.m_asset);
				while (stack.Count > 0)
				{
					GameObject gameObject = stack.Peek();
					stack.Pop();
					SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
					bool flag2 = component == null;
					if (flag2)
					{
						SkinnedMeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
							skinnedMeshRenderer.useLightProbes = this.m_useLightProbes;
							skinnedMeshRenderer.castShadows = this.m_castShadows;
							skinnedMeshRenderer.receiveShadows = this.m_receiveShadows;
						}
					}
					else
					{
						component.useLightProbes = this.m_useLightProbes;
						component.castShadows = this.m_castShadows;
						component.receiveShadows = this.m_receiveShadows;
					}
					bool flag3 = component != null && component.sharedMesh != null;
					if (flag3)
					{
						this.m_numVertices += component.sharedMesh.vertices.Length;
						this.m_numTriangles += component.sharedMesh.triangles.Length;
						this.m_meshes.Add(component.sharedMesh);
					}
					for (int j = 0; j < gameObject.transform.childCount; j++)
					{
						stack.Push(gameObject.transform.GetChild(j).gameObject);
					}
				}
			}
		}
	}
}
