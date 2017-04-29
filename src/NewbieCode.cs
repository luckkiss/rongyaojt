using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/新手脚本")]
public class NewbieCode : MonoBehaviour
{
	public List<string> codes;

	public List<string> waitCodes;

	private void Start()
	{
		if (this.codes != null && this.codes.Count > 0)
		{
			NewbieTeachMgr.getInstance().add(this.codes, -1);
		}
		if (this.waitCodes == null || this.waitCodes.Count == 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 13;
		triggerHanldePoint.paramStr = this.waitCodes;
		UnityEngine.Object.Destroy(this);
	}
}
