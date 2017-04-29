using MuGame;
using System;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/TriggerFB"), RequireComponent(typeof(BoxCollider))]
internal class TriggerFB : MonoBehaviour
{
	public int triggerTimes = 1;

	private void Start()
	{
		switch (MapModel.getInstance().curLevelId)
		{
		case 108u:
		{
			WdsyOpenDoor wdsyOpenDoor = base.gameObject.AddComponent<WdsyOpenDoor>();
			wdsyOpenDoor.triggerTimes = this.triggerTimes;
			UnityEngine.Object.Destroy(this);
			break;
		}
		case 109u:
		{
			OpenDoor109 openDoor = base.gameObject.AddComponent<OpenDoor109>();
			openDoor.triggerTimes = this.triggerTimes;
			UnityEngine.Object.Destroy(this);
			break;
		}
		case 110u:
		{
			OpenDoor110 openDoor2 = base.gameObject.AddComponent<OpenDoor110>();
			openDoor2.triggerTimes = this.triggerTimes;
			UnityEngine.Object.Destroy(this);
			break;
		}
		case 111u:
		{
			OpenDoor111 openDoor3 = base.gameObject.AddComponent<OpenDoor111>();
			openDoor3.triggerTimes = this.triggerTimes;
			UnityEngine.Object.Destroy(this);
			break;
		}
		}
	}
}
