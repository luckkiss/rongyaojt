using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/BREAK_BRIDGE")]
internal class BreakBridge : MonoBehaviour
{
	public GameObject obj;

	private void Start()
	{
		if (this.obj != null)
		{
			TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
			triggerHanldePoint.type = 5;
			triggerHanldePoint.paramGo = new List<GameObject>
			{
				this.obj
			};
		}
		UnityEngine.Object.Destroy(this);
	}
}
