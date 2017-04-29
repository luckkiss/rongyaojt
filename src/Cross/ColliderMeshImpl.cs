using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public class ColliderMeshImpl : PhysicsObject3DImpl, IColliderMesh, IPhysicsObject3D, IPhysicsObject
	{
		protected List<GameObject> m_colObjs = new List<GameObject>();

		protected AssetMeshImpl m_asset = null;

		protected bool m_assetLoaded = false;

		public IAssetMesh asset
		{
			get
			{
				return this.m_asset;
			}
			set
			{
				this.m_asset = (value as AssetMeshImpl);
				this.dispose();
				this.m_assetLoaded = false;
			}
		}

		public ColliderMeshImpl()
		{
			this.m_u3dObj.name = "ColliderMesh";
		}

		public override void dispose()
		{
			foreach (GameObject current in this.m_colObjs)
			{
				current.transform.parent = null;
				UnityEngine.Object.Destroy(current);
			}
			this.m_colObjs.Clear();
			this.m_assetLoaded = false;
		}

		public override Vec3 rayCast(Vec3 origin, Vec3 dir)
		{
			bool flag = !this.m_assetLoaded;
			Vec3 result;
			if (flag)
			{
				bool isReady = this.m_asset.isReady;
				if (!isReady)
				{
					result = null;
					return result;
				}
				this._applyAsset();
			}
			foreach (GameObject current in this.m_colObjs)
			{
				MeshCollider meshCollider = current.get_collider() as MeshCollider;
				bool flag2 = meshCollider.sharedMesh != null;
				if (flag2)
				{
					Ray ray = new Ray(new Vector3(origin.x, origin.y, origin.z), new Vector3(dir.x, dir.y, dir.z));
					RaycastHit raycastHit;
					bool flag3 = meshCollider.Raycast(ray, out raycastHit, 10000f);
					bool flag4 = !flag3;
					if (flag4)
					{
						result = null;
						return result;
					}
					Vector3 point = raycastHit.point;
					result = new Vec3(point.x, point.y, point.z);
					return result;
				}
			}
			result = null;
			return result;
		}

		protected void _applyAsset()
		{
			bool flag = !this.m_asset.isReady;
			if (!flag)
			{
				this.dispose();
				List<GameObject> meshObjs = this.m_asset.meshObjs;
				foreach (GameObject current in meshObjs)
				{
					GameObject gameObject = new GameObject(current.name);
					osImpl.linkU3dObj(this.m_u3dObj, gameObject);
					this.m_colObjs.Add(gameObject);
					gameObject.transform.localPosition = current.transform.position;
					gameObject.transform.localEulerAngles = current.transform.eulerAngles;
					gameObject.transform.localScale = current.transform.localScale;
					MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
					bool flag2 = current.get_renderer() is MeshRenderer;
					if (flag2)
					{
						MeshFilter component = current.GetComponent<MeshFilter>();
						meshCollider.sharedMesh = ((component.mesh != null) ? component.mesh : component.sharedMesh);
					}
					else
					{
						bool flag3 = current.get_renderer() is SkinnedMeshRenderer;
						if (flag3)
						{
							meshCollider.sharedMesh = (current.get_renderer() as SkinnedMeshRenderer).sharedMesh;
						}
					}
				}
			}
		}
	}
}
