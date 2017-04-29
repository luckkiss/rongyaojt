using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class RoomObj : MonoBehaviour
	{
		protected bool disposed = false;

		public virtual void init()
		{
			BoxCollider boxCollider = base.gameObject.GetComponent<BoxCollider>();
			bool flag = boxCollider == null;
			if (flag)
			{
				boxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
			boxCollider.isTrigger = true;
			base.gameObject.layer = EnumLayer.LM_PT;
		}

		public void OnTriggerEnter(Collider other)
		{
			bool flag = GRMap.changeMapTimeSt == 0 || NetClient.instance.CurServerTimeStamp - GRMap.changeMapTimeSt < 2;
			if (!flag)
			{
				this.onTrigger();
			}
		}

		public virtual void dispose()
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		protected virtual void onTrigger()
		{
		}
	}
}
