using System;
using UnityEngine;

public class static_plot_camera : MonoBehaviour
{
	private void FixedUpdate()
	{
		if (base.isActiveAndEnabled && base.transform.childCount > 0)
		{
			GameObject gameObject = base.transform.GetChild(0).gameObject;
			gameObject.SetActive(true);
		}
	}
}
