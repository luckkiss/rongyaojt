using System;
using UnityEngine;

internal class TronTrailSection
{
	public Vector3 point;

	public Vector3 upDir;

	public float time;

	public TronTrailSection()
	{
	}

	public TronTrailSection(Vector3 p, float t)
	{
		this.point = p;
		this.time = t;
	}
}
