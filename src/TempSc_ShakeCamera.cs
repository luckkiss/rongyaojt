using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_SHAKE_CAMERA")]
internal class TempSc_ShakeCamera : MonoBehaviour
{
	public float second = 1f;

	public int count = 5;

	public float strength = 3f;

	private void Start()
	{
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 9;
		triggerHanldePoint.paramFloat = new List<float>
		{
			this.second,
			this.strength
		};
		triggerHanldePoint.paramInts = new List<int>
		{
			this.count
		};
		UnityEngine.Object.Destroy(this);
	}
}
