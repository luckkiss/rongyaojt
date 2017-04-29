using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/HANDLE_MONSTER_BORN")]
public class Monster_born : MonoBehaviour
{
	public int monsterId;

	private void Start()
	{
		if (this.monsterId == 0)
		{
			return;
		}
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 1;
		triggerHanldePoint.paramInts = new List<int>
		{
			this.monsterId
		};
		UnityEngine.Object.Destroy(this);
	}
}
