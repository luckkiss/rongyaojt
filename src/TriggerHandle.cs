using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/TriggerHanlde")]
public class TriggerHandle : MonoBehaviour
{
	public List<int> paramInts = new List<int>();

	private void Start()
	{
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.paramInts = this.paramInts;
		UnityEngine.Object.Destroy(this);
	}
}
