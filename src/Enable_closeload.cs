using MuGame;
using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/CLOSELOAD")]
internal class Enable_closeload : MonoBehaviour
{
	private void Start()
	{
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 17;
		UnityEngine.Object.Destroy(this);
	}
}
