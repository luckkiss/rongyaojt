using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class AssetMeshImpl : AssetImpl, IAssetMesh, IAsset
	{
		private GameObject m_assetObj = null;

		private List<GameObject> m_meshObjs = new List<GameObject>();

		protected bool m_infoReady = false;

		protected int m_numVertices = 0;

		protected int m_numTriangles = 0;

		private Stack<GameObject> stack = new Stack<GameObject>();

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
					result = this.m_assetObj;
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
					bool flag2 = !this.m_infoReady;
					if (flag2)
					{
						this._prepareMeshInfo();
					}
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
					bool flag2 = !this.m_infoReady;
					if (flag2)
					{
						this._prepareMeshInfo();
					}
					result = this.m_numTriangles;
				}
				return result;
			}
		}

		public List<GameObject> meshObjs
		{
			get
			{
				bool flag = !this.m_ready;
				List<GameObject> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !this.m_infoReady;
					if (flag2)
					{
						this._prepareMeshInfo();
					}
					result = this.m_meshObjs;
				}
				return result;
			}
		}

		public override void dispose()
		{
			this.m_meshObjs.Clear();
			bool flag = this.m_assetObj != null;
			if (flag)
			{
				Resources.UnloadAsset(this.m_assetObj);
				this.m_assetObj = null;
			}
			this.m_numVertices = 0;
			this.m_numTriangles = 0;
			this.m_loaded = false;
			this.m_ready = false;
			this.m_infoReady = false;
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
					this.dispose();
					try
					{
						if (bSync)
						{
							Debug.Log("warnning::sync Mesh " + this.m_path);
							this.m_loading = true;
							this.m_assetObj = Resources.Load<GameObject>(this.m_path);
							this.m_loading = false;
							this.m_loaded = true;
							bool flag3 = this.m_assetObj != null;
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
								url = (this.m_path.IndexOf(".res") < 0) ? (this.m_path + ".res") : this.m_path
							}.load(delegate(IURLReq r, object data)
							{
								this.m_assetObj = (data as GameObject);
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
								this.m_loading = false;
								this.m_loaded = true;
								this.m_ready = false;
								this._dispatchOnFails(err);
							});
						}
					}
					catch
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "Failed to Load Mesh: " + this.m_path);
					}
				}
			}
		}

		protected void _prepareMeshInfo()
		{
			this.m_numVertices = 0;
			this.m_numTriangles = 0;
			bool flag = this.m_assetObj == null;
			if (!flag)
			{
				this.stack.Clear();
				this.stack.Push(this.m_assetObj);
				while (this.stack.Count > 0)
				{
					GameObject gameObject = this.stack.Peek();
					this.stack.Pop();
					bool flag2 = gameObject == null;
					if (!flag2)
					{
						bool flag3 = gameObject.get_renderer() != null;
						if (flag3)
						{
							bool flag4 = gameObject.get_renderer() is MeshRenderer;
							if (flag4)
							{
								MeshFilter component = gameObject.GetComponent<MeshFilter>();
								bool flag5 = component != null;
								if (flag5)
								{
									Mesh mesh = component.mesh;
									bool flag6 = mesh != null;
									if (flag6)
									{
										this.m_meshObjs.Add(gameObject);
										this.m_numVertices += mesh.vertices.Length;
										this.m_numTriangles += mesh.triangles.Length;
									}
								}
							}
							else
							{
								bool flag7 = gameObject.get_renderer() is SkinnedMeshRenderer;
								if (flag7)
								{
									SkinnedMeshRenderer skinnedMeshRenderer = gameObject.get_renderer() as SkinnedMeshRenderer;
									Mesh sharedMesh = skinnedMeshRenderer.sharedMesh;
									bool flag8 = sharedMesh != null;
									if (flag8)
									{
										this.m_meshObjs.Add(gameObject);
										this.m_numVertices += sharedMesh.vertices.Length;
										this.m_numTriangles += sharedMesh.triangles.Length;
									}
								}
							}
						}
						for (int i = 0; i < gameObject.transform.childCount; i++)
						{
							this.stack.Push(gameObject.transform.GetChild(i).gameObject);
						}
					}
				}
				this.m_infoReady = true;
			}
		}
	}
}
