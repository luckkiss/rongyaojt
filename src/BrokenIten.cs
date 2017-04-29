using System;
using UnityEngine;

internal class BrokenIten : MonoBehaviour
{
	private void Start()
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = 150f;
		base.Invoke("dispose", 3f);
	}

	private void dispose()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
