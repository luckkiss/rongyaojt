using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cross
{
	public abstract class TrailRenderer_Base : MonoBehaviour
	{
		public PCTrailRendererData TrailData;

		public bool Emit = false;

		public int MaxNumberOfPoints = 50;

		protected bool _emit;

		private CircularBuffer<PCTrailPoint> _activeTrail;

		private List<CircularBuffer<PCTrailPoint>> _fadingTrails;

		private List<Mesh> _toCleanUp = new List<Mesh>();

		protected Transform _t;

		protected virtual void Awake()
		{
			this._activeTrail = new CircularBuffer<PCTrailPoint>(this.MaxNumberOfPoints);
			this._fadingTrails = new List<CircularBuffer<PCTrailPoint>>();
			this._t = base.transform;
			this._emit = this.Emit;
		}

		protected virtual void Start()
		{
		}

		protected virtual void LateUpdate()
		{
			bool flag = this._fadingTrails == null;
			if (flag)
			{
				this._fadingTrails = new List<CircularBuffer<PCTrailPoint>>();
			}
			foreach (Mesh current in this._toCleanUp)
			{
				UnityEngine.Object.Destroy(current);
			}
			this._toCleanUp.Clear();
			this.CheckEmitChange();
			bool flag2 = this._activeTrail != null;
			if (flag2)
			{
				this.UpdatePoints(Time.deltaTime, this._activeTrail);
				Mesh mesh = this.GenerateMesh(this._activeTrail);
				bool flag3 = mesh != null;
				if (flag3)
				{
					this.DrawMesh(mesh);
					this._toCleanUp.Add(mesh);
				}
			}
			for (int i = this._fadingTrails.Count - 1; i >= 0; i--)
			{
				CircularBuffer<PCTrailPoint> circularBuffer = this._fadingTrails[i];
				bool flag4 = circularBuffer == null || !circularBuffer.Any((PCTrailPoint a) => a.TimeActive() < this.TrailData.Lifetime);
				if (flag4)
				{
					this._fadingTrails.RemoveAt(i);
				}
				else
				{
					this.UpdatePoints(Time.deltaTime, circularBuffer);
					Mesh mesh2 = this.GenerateMesh(circularBuffer);
					bool flag5 = mesh2 != null;
					if (flag5)
					{
						this.DrawMesh(mesh2);
						this._toCleanUp.Add(mesh2);
					}
				}
			}
		}

		protected virtual void OnStopEmit()
		{
		}

		protected virtual void OnStartEmit()
		{
		}

		public virtual void Reset()
		{
			bool flag = this.TrailData == null;
			if (flag)
			{
				this.TrailData = new PCTrailRendererData();
			}
			this.TrailData.ColorOverLife = new Gradient();
			this.TrailData.Lifetime = 1f;
			this.TrailData.SizeOverLife = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 0f)
			});
			this.MaxNumberOfPoints = 50;
		}

		protected virtual void InitialiseNewPoint(PCTrailPoint newPoint)
		{
		}

		protected virtual void UpdatePoint(PCTrailPoint point, float deltaTime)
		{
		}

		protected void AddPoint(PCTrailPoint newPoint, Vector3 pos)
		{
			bool flag = this._activeTrail == null;
			if (!flag)
			{
				newPoint.Position = pos;
				newPoint.PointNumber = ((this._activeTrail.Count == 0) ? 0 : (this._activeTrail[this._activeTrail.Count - 1].PointNumber + 1));
				this.InitialiseNewPoint(newPoint);
				newPoint.SetDistanceFromStart((this._activeTrail.Count == 0) ? 0f : (this._activeTrail[this._activeTrail.Count - 1].GetDistanceFromStart() + Vector3.Distance(this._activeTrail[this._activeTrail.Count - 1].Position, pos)));
				bool useForwardOverride = this.TrailData.UseForwardOverride;
				if (useForwardOverride)
				{
					newPoint.Forward = (this.TrailData.ForwardOverideRelative ? this._t.TransformDirection(this.TrailData.ForwardOverride.normalized) : this.TrailData.ForwardOverride.normalized);
				}
				this._activeTrail.Add(newPoint);
			}
		}

		private Mesh GenerateMesh(CircularBuffer<PCTrailPoint> trail)
		{
			Vector3 vector = (Camera.main != null) ? Camera.main.transform.forward : Vector3.forward;
			bool useForwardOverride = this.TrailData.UseForwardOverride;
			if (useForwardOverride)
			{
				vector = this.TrailData.ForwardOverride.normalized;
			}
			Mesh mesh = new Mesh();
			int num = this.NumberOfActivePoints(trail);
			bool flag = num < 2;
			Mesh result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Vector3[] array = new Vector3[2 * num];
				Vector3[] array2 = new Vector3[2 * num];
				Vector2[] array3 = new Vector2[2 * num];
				Color[] array4 = new Color[2 * num];
				int[] array5 = new int[2 * num * 3];
				int num2 = 0;
				for (int i = 0; i < trail.Count; i++)
				{
					PCTrailPoint pCTrailPoint = trail[i];
					float time = pCTrailPoint.TimeActive() / this.TrailData.Lifetime;
					bool flag2 = pCTrailPoint.TimeActive() > this.TrailData.Lifetime;
					if (!flag2)
					{
						bool flag3 = this.TrailData.UseForwardOverride && this.TrailData.ForwardOverideRelative;
						if (flag3)
						{
							vector = pCTrailPoint.Forward;
						}
						Vector3 a = Vector3.zero;
						bool flag4 = i < trail.Count - 1;
						if (flag4)
						{
							a = Vector3.Cross((trail[i + 1].Position - pCTrailPoint.Position).normalized, vector).normalized;
						}
						else
						{
							a = Vector3.Cross((pCTrailPoint.Position - trail[i - 1].Position).normalized, vector).normalized;
						}
						Color color = this.TrailData.StretchToFit ? this.TrailData.ColorOverLife.Evaluate(1f - (float)num2 / (float)num / 2f) : this.TrailData.ColorOverLife.Evaluate(time);
						float d = this.TrailData.StretchToFit ? this.TrailData.SizeOverLife.Evaluate(1f - (float)num2 / (float)num / 2f) : this.TrailData.SizeOverLife.Evaluate(time);
						array[num2] = pCTrailPoint.Position + a * d;
						bool flag5 = this.TrailData.MaterialTileLength <= 0f;
						if (flag5)
						{
							array3[num2] = new Vector2((float)num2 / (float)num / 2f, 0f);
						}
						else
						{
							array3[num2] = new Vector2(pCTrailPoint.GetDistanceFromStart() / this.TrailData.MaterialTileLength, 0f);
						}
						array2[num2] = vector;
						array4[num2] = color;
						num2++;
						array[num2] = pCTrailPoint.Position - a * d;
						bool flag6 = this.TrailData.MaterialTileLength <= 0f;
						if (flag6)
						{
							array3[num2] = new Vector2((float)num2 / (float)num / 2f, 1f);
						}
						else
						{
							array3[num2] = new Vector2(pCTrailPoint.GetDistanceFromStart() / this.TrailData.MaterialTileLength, 1f);
						}
						array2[num2] = vector;
						array4[num2] = color;
						num2++;
					}
				}
				int num3 = 0;
				for (int j = 0; j < 2 * (num - 1); j++)
				{
					bool flag7 = j % 2 == 0;
					if (flag7)
					{
						array5[num3] = j;
						num3++;
						array5[num3] = j + 1;
						num3++;
						array5[num3] = j + 2;
					}
					else
					{
						array5[num3] = j + 2;
						num3++;
						array5[num3] = j + 1;
						num3++;
						array5[num3] = j;
					}
					num3++;
				}
				mesh.vertices = array;
				mesh.SetIndices(array5, MeshTopology.Triangles, 0);
				mesh.uv = array3;
				mesh.normals = array2;
				mesh.colors = array4;
				result = mesh;
			}
			return result;
		}

		private void DrawMesh(Mesh trailMesh)
		{
			Graphics.DrawMesh(trailMesh, Matrix4x4.identity, this.TrailData.TrailMaterial, base.gameObject.layer);
		}

		private void UpdatePoints(float deltaTime, CircularBuffer<PCTrailPoint> line)
		{
			foreach (PCTrailPoint current in line)
			{
				current.Update(deltaTime);
				this.UpdatePoint(current, deltaTime);
			}
		}

		private void CheckEmitChange()
		{
			bool flag = this._emit != this.Emit;
			if (flag)
			{
				this._emit = this.Emit;
				bool emit = this._emit;
				if (emit)
				{
					this.OnStartEmit();
					this._activeTrail = new CircularBuffer<PCTrailPoint>(this.MaxNumberOfPoints);
				}
				else
				{
					this.OnStopEmit();
					this._fadingTrails.Add(this._activeTrail);
					this._activeTrail = null;
				}
			}
		}

		private int NumberOfActivePoints(CircularBuffer<PCTrailPoint> line)
		{
			int num = 0;
			foreach (PCTrailPoint current in line)
			{
				bool flag = current.TimeActive() < this.TrailData.Lifetime;
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public void CreateTrail(Vector3 from, Vector3 to, float distanceBetweenPoints)
		{
			float num = Vector3.Distance(from, to);
			Vector3 normalized = (to - from).normalized;
			float num2 = 0f;
			CircularBuffer<PCTrailPoint> circularBuffer = new CircularBuffer<PCTrailPoint>(this.MaxNumberOfPoints);
			int num3 = 0;
			while (num2 < num)
			{
				PCTrailPoint pCTrailPoint = new PCTrailPoint();
				pCTrailPoint.PointNumber = num3;
				pCTrailPoint.Position = from + normalized * num2;
				circularBuffer.Add(pCTrailPoint);
				this.InitialiseNewPoint(pCTrailPoint);
				num3++;
				bool flag = distanceBetweenPoints <= 0f;
				if (flag)
				{
					break;
				}
				num2 += distanceBetweenPoints;
			}
			PCTrailPoint pCTrailPoint2 = new PCTrailPoint();
			pCTrailPoint2.PointNumber = num3;
			pCTrailPoint2.Position = to;
			circularBuffer.Add(pCTrailPoint2);
			this.InitialiseNewPoint(pCTrailPoint2);
			this._fadingTrails.Add(circularBuffer);
		}

		public void ClearSystem(bool emitState)
		{
			this._activeTrail = null;
			bool flag = this._fadingTrails != null;
			if (flag)
			{
				this._fadingTrails.Clear();
			}
			bool flag2 = this._toCleanUp != null;
			if (flag2)
			{
				foreach (Mesh current in this._toCleanUp)
				{
					UnityEngine.Object.Destroy(current);
				}
				this._toCleanUp.Clear();
			}
			this.Emit = emitState;
			this._emit = !emitState;
			this.CheckEmitChange();
		}

		public int NumSegments()
		{
			int num = 0;
			bool flag = this._activeTrail != null && this.NumberOfActivePoints(this._activeTrail) != 0;
			if (flag)
			{
				num++;
			}
			return num + this._fadingTrails.Count;
		}
	}
}
