using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/Trigger/delay")]
public class Trigger_delay : MonoBehaviour
{
	public bool run;

	public float sec;

	private void Start()
	{
		GameEventTrigger gameEventTrigger = base.gameObject.AddComponent<GameEventTrigger>();
		gameEventTrigger.type = 3;
		gameEventTrigger.paramFloat = new List<float>
		{
			this.sec
		};
		if (this.run)
		{
			gameEventTrigger.onTriggerHanlde();
		}
		UnityEngine.Object.Destroy(this);
	}
}
