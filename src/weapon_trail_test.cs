using System;
using System.Collections.Generic;
using UnityEngine;

public class weapon_trail_test : MonoBehaviour
{
	public float height = 2f;

	public float time = 2f;

	public bool alwaysUp;

	public float minDistance = 0.1f;

	public float timeTransitionSpeed = 1f;

	public float desiredTime = 2f;

	public Color startColor = Color.white;

	public Color endColor = new Color(1f, 1f, 1f, 0f);

	private Vector3 position;

	private float now;

	private TronTrailSection currentSection;

	private Matrix4x4 localSpaceTransform;

	private Mesh mesh;

	private Vector3[] vertices;

	private Color[] colors;

	private Vector2[] uv;

	private MeshRenderer meshRenderer;

	private Material trailMaterial;

	private List<TronTrailSection> sections = new List<TronTrailSection>();

	private float tempT;

	protected float animationIncrement = 0.003f;

	private void Awake()
	{
		MeshFilter meshFilter = base.GetComponent(typeof(MeshFilter)) as MeshFilter;
		this.mesh = meshFilter.mesh;
		this.meshRenderer = (base.GetComponent(typeof(MeshRenderer)) as MeshRenderer);
		this.trailMaterial = this.meshRenderer.material;
	}

	protected virtual void LateUpdate()
	{
		float deltaTime = Time.deltaTime;
		if (this.time > 0f)
		{
			this.Itterate(Time.time - deltaTime + this.tempT);
		}
		else
		{
			this.ClearTrail();
		}
		this.UpdateTrail(Time.time, deltaTime);
	}

	public void StartTrail(float timeToTweenTo, float fadeInTime)
	{
		this.desiredTime = timeToTweenTo;
		if (this.time != this.desiredTime)
		{
			this.timeTransitionSpeed = Mathf.Abs(this.desiredTime - this.time) / fadeInTime;
		}
		if (this.time <= 0f)
		{
			this.time = 0.01f;
		}
	}

	public void SetTime(float trailTime, float timeToTweenTo, float tweenSpeed)
	{
		this.time = trailTime;
		this.desiredTime = timeToTweenTo;
		this.timeTransitionSpeed = tweenSpeed;
		if (this.time <= 0f)
		{
			this.ClearTrail();
		}
	}

	public void FadeOut(float fadeTime)
	{
		this.desiredTime = 0f;
		if (this.time > 0f)
		{
			this.timeTransitionSpeed = this.time / fadeTime;
		}
	}

	public void SetTrailColor(Color color)
	{
		this.trailMaterial.SetColor("_TintColor", color);
	}

	public void Itterate(float itterateTime)
	{
		this.position = base.transform.position;
		this.now = itterateTime;
		if (this.sections.Count == 0 || (this.sections[0].point - this.position).sqrMagnitude > this.minDistance * this.minDistance)
		{
			TronTrailSection tronTrailSection = new TronTrailSection();
			tronTrailSection.point = this.position;
			if (this.alwaysUp)
			{
				tronTrailSection.upDir = Vector3.up;
			}
			else
			{
				tronTrailSection.upDir = base.transform.TransformDirection(Vector3.up);
			}
			tronTrailSection.time = this.now;
			this.sections.Insert(0, tronTrailSection);
		}
	}

	public void UpdateTrail(float currentTime, float deltaTime)
	{
		this.mesh.Clear();
		while (this.sections.Count > 0 && currentTime > this.sections[this.sections.Count - 1].time + this.time)
		{
			this.sections.RemoveAt(this.sections.Count - 1);
		}
		if (this.sections.Count < 2)
		{
			return;
		}
		this.vertices = new Vector3[this.sections.Count * 2];
		this.colors = new Color[this.sections.Count * 2];
		this.uv = new Vector2[this.sections.Count * 2];
		this.currentSection = this.sections[0];
		this.localSpaceTransform = base.transform.worldToLocalMatrix;
		for (int i = 0; i < this.sections.Count; i++)
		{
			this.currentSection = this.sections[i];
			float num = 0f;
			if (i != 0)
			{
				num = Mathf.Clamp01((currentTime - this.currentSection.time) / this.time);
			}
			Vector3 upDir = this.currentSection.upDir;
			this.vertices[i * 2] = this.localSpaceTransform.MultiplyPoint(this.currentSection.point);
			this.vertices[i * 2 + 1] = this.localSpaceTransform.MultiplyPoint(this.currentSection.point + upDir * this.height);
			this.uv[i * 2] = new Vector2(num, 0f);
			this.uv[i * 2 + 1] = new Vector2(num, 1f);
			Color color = Color.Lerp(this.startColor, this.endColor, num);
			this.colors[i * 2] = color;
			this.colors[i * 2 + 1] = color;
		}
		int[] array = new int[(this.sections.Count - 1) * 2 * 3];
		for (int j = 0; j < array.Length / 6; j++)
		{
			array[j * 6] = j * 2;
			array[j * 6 + 1] = j * 2 + 1;
			array[j * 6 + 2] = j * 2 + 2;
			array[j * 6 + 3] = j * 2 + 2;
			array[j * 6 + 4] = j * 2 + 1;
			array[j * 6 + 5] = j * 2 + 3;
		}
		this.mesh.vertices = this.vertices;
		this.mesh.colors = this.colors;
		this.mesh.uv = this.uv;
		this.mesh.triangles = array;
		if (this.time > this.desiredTime)
		{
			this.time -= deltaTime * this.timeTransitionSpeed;
			if (this.time <= this.desiredTime)
			{
				this.time = this.desiredTime;
			}
		}
		else if (this.time < this.desiredTime)
		{
			this.time += deltaTime * this.timeTransitionSpeed;
			if (this.time >= this.desiredTime)
			{
				this.time = this.desiredTime;
			}
		}
	}

	public void ClearTrail()
	{
		this.desiredTime = 0f;
		this.time = 0f;
		if (this.mesh != null)
		{
			this.mesh.Clear();
			this.sections.Clear();
		}
	}
}
