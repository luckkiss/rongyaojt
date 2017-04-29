using System;
using UnityEngine;

public class static_z_roatate : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Rotate(0f, 0f, 0.2f);
	}
}
