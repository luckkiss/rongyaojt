using MuGame;
using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/Trigger"), RequireComponent(typeof(BoxCollider))]
public class Trigger : MonoBehaviour
{
	public int triggerTimes = 1;

	private void Start()
	{
		ChangePoint changePoint = base.gameObject.AddComponent<ChangePoint>();
		changePoint.triggerTimes = this.triggerTimes;
		UnityEngine.Object.Destroy(this);
	}
}
