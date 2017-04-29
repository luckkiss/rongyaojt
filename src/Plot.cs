using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/Plot")]
public class Plot : MonoBehaviour
{
	public bool activeUI = true;

	public GameObject tragetPlot;

	public float speed = 10f;

	private void Start()
	{
		TriggerHanldePoint triggerHanldePoint = base.gameObject.AddComponent<TriggerHanldePoint>();
		triggerHanldePoint.type = 6;
		if (this.tragetPlot == null)
		{
			Debug.LogError("Plot缺少参数tragetPlot::" + base.gameObject.name);
			UnityEngine.Object.Destroy(this);
			return;
		}
		Transform transform = this.tragetPlot.transform.FindChild("c");
		if (transform == null)
		{
			Debug.LogError("Plot缺少参数c::" + this.tragetPlot.name);
			UnityEngine.Object.Destroy(this);
			return;
		}
		Transform transform2 = this.tragetPlot.transform.FindChild("e");
		if (transform2 == null)
		{
			Debug.LogError("Plot缺少参数e::" + this.tragetPlot.name);
			UnityEngine.Object.Destroy(this);
			return;
		}
		Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
		int childCount = transform2.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = transform2.GetChild(i).gameObject;
			dictionary[gameObject.name] = gameObject;
		}
		GameObject gameObject2 = transform.transform.parent.gameObject;
		if (gameObject2.GetComponent<GameAniCamera>() != null)
		{
			return;
		}
		GameAniCamera gameAniCamera = gameObject2.AddComponent<GameAniCamera>();
		gameAniCamera.dEvt = dictionary;
		gameAniCamera.speed = this.speed;
		gameAniCamera.uiactive = this.activeUI;
		triggerHanldePoint.paramGo = new List<GameObject>
		{
			transform.gameObject,
			gameObject2
		};
		triggerHanldePoint.paramBool = this.activeUI;
		triggerHanldePoint.paramFloat = new List<float>
		{
			this.speed
		};
		UnityEngine.Object.Destroy(this);
	}

	public void shake(string pram)
	{
	}

	public void doit(string id)
	{
	}
}
