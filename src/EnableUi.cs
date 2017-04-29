using MuGame;
using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/ui控制")]
public class EnableUi : MonoBehaviour
{
	public bool floatUI = true;

	private void Start()
	{
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 14;
		triggerHanldePoint.paramBool = this.floatUI;
		UnityEngine.Object.Destroy(this);
	}
}
