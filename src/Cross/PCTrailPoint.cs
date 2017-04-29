using System;
using UnityEngine;

namespace Cross
{
	public class PCTrailPoint
	{
		public Vector3 Forward;

		public Vector3 Position;

		public int PointNumber;

		private float _timeActive = 0f;

		private float _distance;

		public virtual void Update(float deltaTime)
		{
			this._timeActive += deltaTime;
		}

		public float TimeActive()
		{
			return this._timeActive;
		}

		public void SetDistanceFromStart(float distance)
		{
			this._distance = distance;
		}

		public float GetDistanceFromStart()
		{
			return this._distance;
		}
	}
}
