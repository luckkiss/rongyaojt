using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/显示特效")]
public class Enable_Eff : MonoBehaviour
{
	public GameObject item;

	public float sec;

	private void Start()
	{
		if (this.item == null)
		{
			return;
		}
		if (this.sec <= 0f)
		{
			return;
		}
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 10;
		triggerHanldePoint.paramGo = new List<GameObject>
		{
			this.item
		};
		triggerHanldePoint.paramFloat = new List<float>
		{
			this.sec
		};
		UnityEngine.Object.Destroy(this);
	}
}
