using System;
using UnityEngine;

namespace Cross
{
	public class Trail : TrailRenderer_Base
	{
		public float MinVertexDistance = 0.1f;

		private Vector3 _lastPosition;

		private float _distanceMoved;

		protected override void Start()
		{
			base.Start();
			this._lastPosition = this._t.position;
		}

		protected override void LateUpdate()
		{
			bool emit = this._emit;
			if (emit)
			{
				this._distanceMoved += Vector3.Distance(this._t.position, this._lastPosition);
				bool flag = this._distanceMoved >= this.MinVertexDistance;
				if (flag)
				{
					base.AddPoint(new PCTrailPoint(), this._lastPosition);
					this._distanceMoved = 0f;
				}
				this._lastPosition = this._t.position;
			}
			base.LateUpdate();
		}

		protected override void OnStartEmit()
		{
			this._lastPosition = this._t.position;
			this._distanceMoved = 0f;
		}

		public override void Reset()
		{
			base.Reset();
			this.MinVertexDistance = 0.1f;
		}
	}
}
