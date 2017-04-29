using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_UNENABLE_ITEM")]
public class Unenable_item : MonoBehaviour
{
	public List<GameObject> item;

	private void Start()
	{
		if (this.item == null)
		{
			return;
		}
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 3;
		triggerHanldePoint.paramGo = this.item;
		UnityEngine.Object.Destroy(this);
	}
}
