using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/act")]
internal class Enable_act : MonoBehaviour
{
	public string act = string.Empty;

	public GameObject role;

	private void Start()
	{
		if (this.act != string.Empty)
		{
			TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
			triggerHanldePoint.type = 12;
			triggerHanldePoint.paramStr = new List<string>
			{
				this.act
			};
			if (this.role != null)
			{
				triggerHanldePoint.paramGo = new List<GameObject>
				{
					this.role
				};
			}
		}
		UnityEngine.Object.Destroy(this);
	}
}
