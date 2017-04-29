using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/DIALOG")]
internal class Enable_dialog : MonoBehaviour
{
	public List<string> desc;

	public GameObject npcrole;

	private void Start()
	{
		if (this.desc != null)
		{
			TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
			triggerHanldePoint.type = 11;
			triggerHanldePoint.paramStr = this.desc;
			triggerHanldePoint.paramGo = new List<GameObject>
			{
				this.npcrole
			};
		}
		UnityEngine.Object.Destroy(this);
	}
}
