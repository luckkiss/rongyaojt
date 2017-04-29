using System;
using UnityEngine;

namespace MuGame
{
	public class fadeout : MonoBehaviour
	{
		private float _lifeTimer;

		private float startTime;

		public float lifeTime
		{
			set
			{
				bool flag = value <= 0f;
				if (flag)
				{
					this._lifeTimer = -1f;
				}
				this._lifeTimer = Time.time + value;
			}
		}

		private void Start()
		{
			this.startTime = Time.time;
		}

		private void Update()
		{
			bool flag = this._lifeTimer == -1f;
			if (!flag)
			{
				bool flag2 = Time.time > this._lifeTimer;
				if (flag2)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}
}
