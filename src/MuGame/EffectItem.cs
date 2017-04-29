using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class EffectItem
	{
		public static Dictionary<string, List<Action>> dLoading = new Dictionary<string, List<Action>>();

		public GameObject _goEffect;

		public string _id;

		public string _path;

		public bool _disposed = false;

		public Vector3 _pos = Vector3.zero;

		public Transform _parent;

		private float _scale = 1f;

		private float _len = -1f;

		public float scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				bool flag = this._scale == value;
				if (!flag)
				{
					this._scale = value;
					bool flag2 = this._goEffect != null;
					if (flag2)
					{
						this._goEffect.transform.localScale = new Vector3(this._scale, this._scale, this._scale);
					}
				}
			}
		}

		public bool isAutoRemove
		{
			get
			{
				return this._len > 0f;
			}
		}

		public Vector3 pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				bool disposed = this._disposed;
				if (!disposed)
				{
					this._pos = value;
					bool flag = this._goEffect != null;
					if (flag)
					{
						this._goEffect.transform.position = value;
					}
				}
			}
		}

		public EffectItem(string id, string path, float len = -1f)
		{
			this._path = path;
			this._id = id;
			bool flag = len > 0f;
			if (flag)
			{
				this._len = Time.time + len;
			}
			this.loadGo(path);
		}

		public void setParent(Transform trans)
		{
			this._parent = trans;
			bool flag = this._goEffect != null;
			if (flag)
			{
				this._goEffect.transform.SetParent(this._parent, false);
			}
		}

		public void loadGo(string path)
		{
			MapEffMgr.dGo[path] = Resources.Load<GameObject>(path);
			this.onLoaded();
		}

		private void onLoaded()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				this._goEffect = UnityEngine.Object.Instantiate<GameObject>(MapEffMgr.dGo[this._path]);
				this._goEffect.transform.position = this._pos;
				bool flag = this._parent != null;
				if (flag)
				{
					this._goEffect.transform.SetParent(this._parent, false);
				}
				bool flag2 = this._scale != 1f;
				if (flag2)
				{
					this._goEffect.transform.localScale = new Vector3(this._scale, this._scale, this._scale);
				}
			}
		}

		public void update(float s)
		{
			bool flag = this._len <= 0f;
			if (!flag)
			{
				bool disposed = this._disposed;
				if (!disposed)
				{
					bool flag2 = this._len <= Time.time;
					if (flag2)
					{
						this.dispose();
					}
				}
			}
		}

		public void dispose()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				bool flag = this._goEffect;
				if (flag)
				{
					UnityEngine.Object.Destroy(this._goEffect);
				}
				this._disposed = true;
				this._parent = null;
			}
		}
	}
}
