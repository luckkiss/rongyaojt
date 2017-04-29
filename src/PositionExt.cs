using System;
using UnityEngine;

public static class PositionExt
{
	public static Vector3 ConvertToGamePosition(this Vector3 transform)
	{
		return new Vector3(transform.x, 0f, transform.z);
	}
}
