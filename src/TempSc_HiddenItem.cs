using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/ITEM_HIDDEN")]
internal class TempSc_HiddenItem : MonoBehaviour
{
	public bool useAni;

	public float hideSec = 5f;

	private void Start()
	{
		HiddenItem hiddenItem = base.gameObject.AddComponent<HiddenItem>();
		hiddenItem.useAni = this.useAni;
		hiddenItem.hideSec = this.hideSec;
		UnityEngine.Object.Destroy(this);
	}
}
