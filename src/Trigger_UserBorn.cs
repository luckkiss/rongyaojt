using MuGame;
using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/Trigger/Userborn")]
public class Trigger_UserBorn : MonoBehaviour
{
	public bool run;

	private void Start()
	{
		GameEventTrigger gameEventTrigger = base.gameObject.AddComponent<GameEventTrigger>();
		gameEventTrigger.type = 2;
		if (this.run)
		{
			gameEventTrigger.onTriggerHanlde();
		}
		UnityEngine.Object.Destroy(this);
	}
}
