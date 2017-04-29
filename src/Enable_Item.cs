using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_ENABLE_ITEM")]
public class Enable_Item : MonoBehaviour
{
	public List<GameObject> item;

	private void Start()
	{
		if (this.item == null)
		{
			return;
		}
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 2;
		triggerHanldePoint.paramGo = this.item;
		UnityEngine.Object.Destroy(this);
	}
}
