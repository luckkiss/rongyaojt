using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_WALK_LAYER")]
public class Walk_Layer : MonoBehaviour
{
	public string layers = string.Empty;

	private void Start()
	{
		if (this.layers != string.Empty)
		{
			List<int> list = new List<int>();
			string[] array = this.layers.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(int.Parse(array[i]));
			}
			TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
			triggerHanldePoint.type = 4;
			triggerHanldePoint.paramInts = list;
		}
		UnityEngine.Object.Destroy(this);
	}
}
