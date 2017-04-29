using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_SHOW_ABD_HIDE")]
internal class TempSc_ShowAndHide : MonoBehaviour
{
	public bool useAni;

	public float hideSec = 5f;

	public GameObject target;

	private void Start()
	{
		if (this.target != null)
		{
			this.target.SetActive(false);
			TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
			triggerHanldePoint.type = 8;
			triggerHanldePoint.paramGo = new List<GameObject>
			{
				this.target
			};
			HiddenItem hiddenItem = this.target.AddComponent<HiddenItem>();
			hiddenItem.useAni = this.useAni;
			hiddenItem.hideSec = this.hideSec;
		}
		UnityEngine.Object.Destroy(this);
	}
}
