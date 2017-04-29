using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/Trigger/MonsterDead")]
public class Trigger_MonsterDead : MonoBehaviour
{
	public int monId;

	public int killingNum;

	private void Start()
	{
		GameEventTrigger gameEventTrigger = base.gameObject.AddComponent<GameEventTrigger>();
		gameEventTrigger.type = 1;
		gameEventTrigger.paramInts = new List<int>
		{
			this.monId,
			this.killingNum
		};
		UnityEngine.Object.Destroy(this);
	}
}
